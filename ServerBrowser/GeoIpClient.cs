using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;

namespace ServerBrowser
{
  class GeoIpClient
  {
    internal const int ThreadCount = 7;
    private const string DefaultServiceUrlFormat = "http://api.ipapi.com/{0}?access_key=9c5fc4375488ed26aa2f26b613324f4a&language=en&output=json";
    private DateTime usageExceeded = DateTime.MinValue;

    /// <summary>
    /// the cache holds either a GeoInfo object, or a multicast callback delegate waiting for a GeoInfo object
    /// </summary>
    private readonly Dictionary<uint,object> cache = new Dictionary<uint, object>();
    private readonly BlockingCollection<IPAddress> queue = new BlockingCollection<IPAddress>();
    private readonly string cacheFile;

    public string ServiceUrlFormat { get; set; }

    public GeoIpClient(string cacheFile)
    {
      this.cacheFile = cacheFile;
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
                callbacks = cache.TryGetValue(ipInt, out o) ? o as Action<GeoInfo> : null;
              var geoInfo = this.HandleResult(ipInt, result);
              if (callbacks != null && geoInfo != null)
                ThreadPool.QueueUserWorkItem(ctx => callbacks(geoInfo));
              err = false;
            }
          }
          catch
          {
            // ignore
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
      var ser = new DataContractJsonSerializer(typeof(NekudoGeopIpFullResponse));
      var info = (NekudoGeopIpFullResponse)ser.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(result)));

      if (!info.success)
      {
        if (info.error?.code == 104)
          this.usageExceeded = DateTime.UtcNow.Date;
        lock (cache)
          this.cache.Remove(ip);
        return null;
      }

      var geoInfo = new GeoInfo(info.country_code, info.country_name, info.region_code, info.region_name, info.city, info.latitude, info.longitude);
      lock (cache)
      {
        cache[ip] = geoInfo;
      }
      return geoInfo;
    }
    #endregion

    #region TryGet()
    private string TryGet(IDictionary<string,string> dict, string key)
    {
      string ret;
      return dict != null && dict.TryGetValue(key, out ret) ? ret : null;
    }
    #endregion

    #region Lookup()
    public void Lookup(IPAddress ip, Action<GeoInfo> callback)
    {
      uint ipInt = Ip4Utils.ToInt(ip);
      GeoInfo geoInfo = null;
      lock (cache)
      {
        if (cache.TryGetValue(ipInt, out var cached))
          geoInfo = cached as GeoInfo;

        if (geoInfo == null)
        {
          if (this.usageExceeded == DateTime.UtcNow.Date)
            return;

          if (cached == null)
          {
            cache.Add(ipInt, callback);
            this.queue.Add(ip);
          }
          else
          {
            var callbacks = (Action<GeoInfo>) cached;
            callbacks += callback;
            cache[ipInt] = callbacks;
          }
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

    #region LoadCache()
    public void LoadCache()
    {
      if (!File.Exists(this.cacheFile))
        return;
      lock (cache)
      {
        foreach (var line in File.ReadAllLines(this.cacheFile))
        {
          try
          {
            var parts = line.Split(new[] {'='}, 2);
            uint ipInt = 0;
            var octets = parts[0].Split('.');
            foreach (var octet in octets)
              ipInt = (ipInt << 8) + uint.Parse(octet);
            var loc = parts[1].Split('|');
            var countryCode = loc[0];
            var latitude = decimal.Parse(loc[5], NumberFormatInfo.InvariantInfo);
            var longitude = decimal.Parse(loc[6], NumberFormatInfo.InvariantInfo);
            if (countryCode != "")
            {
              var geoInfo = new GeoInfo(countryCode, loc[1], loc[2], loc[3], loc[4], latitude, longitude);
              cache[ipInt] = geoInfo;
            }
          }
          catch
          {
            // ignore
          }
        }

        // override wrong geo-IP information (MS Azure IPs list Washington even for NL/EU servers)
        cache[Ip4Utils.ToInt(104, 40, 134, 97)] = new GeoInfo("NL", "Netherlands", null, null, null, 0, 0);
        cache[Ip4Utils.ToInt(104, 40, 213, 215)] = new GeoInfo("NL", "Netherlands", null, null, null, 0, 0);

        // Vultr also spreads their IPs everywhere
        cache[Ip4Utils.ToInt(45, 32, 153, 115)] = new GeoInfo("DE", "Germany", null, null, null, 0, 0); // listed as NL, but is DE
        cache[Ip4Utils.ToInt(45, 32, 205, 149)] = new GeoInfo("US", "United States", "TX", null, null, 0, 0); // listed as NL, but is TX

        // i3d.net
        cache[Ip4Utils.ToInt(185, 179, 200, 69)] = new GeoInfo("ZA", "South Africa", null, null, null, 0, 0); // listed as NL, but is ZA
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
        File.WriteAllText(this.cacheFile, sb.ToString());
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

#if false
    private class NekudoGeopIpShortResponse
    {
      public class Country
      {
        public string name;
        public string code;
      }

      public class Location
      {
        public decimal latitude;
        public decimal longitude;
        public string time_zone;
      }

      public string city;
      public Country country;
      public Location location;
      public string ip;
    }
#endif

  #region class NekudoGeopIpFullResponse
  public class NekudoGeopIpFullResponse
  {
    public class NekudoGeoIpError
    {
      public int code;
      public string type;
      public string info;
    }

    public bool success;
    public NekudoGeoIpError error;

    public string ip;
    public string country_code;
    public string country_name;
    public string region_code;
    public string region_name;
    public string city;
    public decimal latitude;
    public decimal longitude;
  }
  #endregion

}
