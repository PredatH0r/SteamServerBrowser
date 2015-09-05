using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading;

namespace ServerBrowser
{
  class GeoIpClient
  {
    private const int ThreadCount = 7;
    private const string DefaultServiceUrlFormat = "http://freegeoip.net/csv/{0}";
    /// <summary>
    /// the cache holds either a string with the 2-letter country ISO code, a simple callback delegate, or a multicast callback delegate
    /// </summary>
    private readonly Dictionary<uint,object> cache = new Dictionary<uint, object>();
    private readonly BlockingCollection<IPAddress> queue = new BlockingCollection<IPAddress>();

    public string ServiceUrlFormat { get; set; }

    public GeoIpClient()
    {
      this.ServiceUrlFormat = DefaultServiceUrlFormat;
      for (int i=0; i<ThreadCount; i++)
        ThreadPool.QueueUserWorkItem(context => this.ProcessLoop());
    }

    #region ProcessLoop()
    private void ProcessLoop()
    {
      while (true)
      {
        var ip = this.queue.Take();
        if (ip == null)
          break;
        var ipInt = Ip4Utils.ToInt(ip);
        try
        {
          var url = string.Format(this.ServiceUrlFormat, ip);
          using (var client = new XWebClient(1000))
          {
            var result = client.DownloadString(url);
            var callbacks = (Action<GeoInfo>)cache[ipInt];
            var geoInfo = this.HandleResult(ipInt, result);
            ThreadPool.QueueUserWorkItem(ctx => callbacks(geoInfo));
          }
        }
        catch
        {
          lock (this.cache)
            this.cache.Remove(ipInt);
        }        
      }
    }
    #endregion

    #region HandleResult()
    private GeoInfo HandleResult(uint ip, string result)
    {
      var parts = result.Split(',');
      if (parts.Length >= 2)
      {
        decimal latitude, longitude;
        decimal.TryParse(TryGet(parts, 8), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, NumberFormatInfo.InvariantInfo, out latitude);
        decimal.TryParse(TryGet(parts, 9), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, NumberFormatInfo.InvariantInfo, out longitude);
        var geoInfo = new GeoInfo(parts[1], TryGet(parts, 2), TryGet(parts, 3), TryGet(parts, 4), TryGet(parts, 5), latitude, longitude);
        lock (cache)
        {
          cache[ip] = geoInfo;
        }
        return geoInfo;
      }
      return null;
    }
    #endregion

    #region TryGet()
    private string TryGet(string[] parts, int index)
    {
      return index < parts.Length ? parts[index] : null;
    }
    #endregion

    #region Lookup()
    public void Lookup(IPAddress ip, Action<GeoInfo> callback)
    {
      uint ipInt = Ip4Utils.ToInt(ip);
      GeoInfo geoInfo;
      lock (cache)
      {
        object cached;
        if (!cache.TryGetValue(ipInt, out cached))
        {
          cache.Add(ipInt, callback);
          this.queue.Add(ip);
          return;
        }
        geoInfo = cached as GeoInfo;
        if (geoInfo == null)
        {
          var callbacks = (Action<GeoInfo>) cached;
          callbacks += callback;
          cache[ipInt] = callbacks;
          return;
        }
      }
      callback(geoInfo);    
    }
    #endregion

  }

  #region class GeoInfo
  public class GeoInfo
  {
    public string Iso2 { get; private set; }
    public string Country { get; private set; }
    public string State { get; private set; }
    public string Region { get; private set; }
    public string City { get; private set; }
    public decimal Longitude { get; private set; }
    public decimal Latitude { get; private set; }

    public GeoInfo(string iso2, string country, string state, string region, string city, decimal latitude, decimal longitude)
    {
      this.Iso2 = iso2;
      this.Country = country;
      this.State = state;
      this.Region = region;
      this.City = city;
      this.Longitude = longitude;
      this.Latitude = latitude;
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append(Country);
      if (!string.IsNullOrEmpty(State))
        sb.Append(", ").Append(State);
      if (!string.IsNullOrEmpty(Region))
        sb.Append(", ").Append(Region);
      if (!string.IsNullOrEmpty(City))
        sb.Append(", ").Append(City);
      return sb.ToString();
    }
  }
  #endregion
}
