using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ServerBrowser
{
  class GeoIpClient
  {
    internal const int ThreadCount = 7;
    private const string DefaultServiceUrlFormat = "http://freegeoip.net/csv/{0}";
    /// <summary>
    /// the cache holds either a GeoInfo object, or a multicast callback delegate waiting for a GeoInfo object
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
      using (var client = new XWebClient(5000))
      {
        while (true)
        {
          var ip = this.queue.Take();
          if (ip == null)
            break;

          bool err = true;
          var ipInt = Ip4Utils.ToInt(ip);
          try
          {
            var url = string.Format(this.ServiceUrlFormat, ip);
            var result = client.DownloadString(url);
            if (result != null)
            {
              object o;
              Action<GeoInfo> callbacks;
              lock (cache)
                callbacks = cache.TryGetValue(ipInt, out o) ? (Action<GeoInfo>)o : null;
              var geoInfo = this.HandleResult(ipInt, result);
              if (callbacks != null)
                ThreadPool.QueueUserWorkItem(ctx => callbacks(geoInfo));
              err = false;
            }
          }
          catch
          {
          }

          if (err)
          { 
            lock (this.cache)
              this.cache.Remove(ipInt);
          }
        }
      }
    }
    #endregion

    #region HandleResult()
    private GeoInfo HandleResult(uint ip, string result)
    {
      var parts = result.Split(',');
      if (parts.Length < 2)
        return null;
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

    #region CancelPendingRequests()
    public void CancelPendingRequests()
    {
      IPAddress item;
      while (this.queue.TryTake(out item))
      {        
      }

      lock (cache)
      {
        var keys = cache.Keys.ToList();
        foreach (var key in keys)
        {
          if (!(cache[key] is GeoInfo))
            cache.Remove(key);
        }
      }
    }
    #endregion

    private string CacheFile => Path.Combine(Application.LocalUserAppDataPath, "locations.txt");

    #region LoadCache()
    public void LoadCache()
    {
      var path = this.CacheFile;
      if (!File.Exists(path))
        return;
      foreach (var line in File.ReadAllLines(path))
      {
        try
        {
          var parts = line.Split(new [] {'='}, 2);
          uint ipInt = 0;
          var octets = parts[0].Split('.');
          foreach (var octet in octets)
            ipInt = (ipInt << 8) + uint.Parse(octet);
          var loc = parts[1].Split('|');
          var geoInfo = new GeoInfo(loc[0], loc[1], loc[2], loc[3], loc[4], decimal.Parse(loc[5], NumberFormatInfo.InvariantInfo), decimal.Parse(loc[6], NumberFormatInfo.InvariantInfo));
          lock (cache)
            cache[ipInt] = geoInfo;
        }
        catch
        {
        }
      }
    }
    #endregion

    #region SaveCache()
    public void SaveCache()
    {
      try
      {
        var sb = new StringBuilder();
        lock (this.cache)
        {
          foreach (var entry in this.cache)
          {
            var ip = entry.Key;
            var info = entry.Value as GeoInfo;
            if (info == null) continue;
            sb.AppendFormat("{0}.{1}.{2}.{3}={4}|{5}|{6}|{7}|{8}|{9}|{10}\n",
              ip >> 24, (ip >> 16) & 0xFF, (ip >> 8) & 0xFF, ip & 0xFF,
              info.Iso2, info.Country, info.State, info.Region, info.City, info.Latitude, info.Longitude);
          }
        }
        File.WriteAllText(this.CacheFile, sb.ToString());
      }
      catch { }
    }
    #endregion
  }

  #region class GeoInfo
  public class GeoInfo
  {
    public string Iso2 { get; }
    public string Country { get; }
    public string State { get; }
    public string Region { get; }
    public string City { get; }
    public decimal Longitude { get; }
    public decimal Latitude { get; }

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
