using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace ServerBrowser;

/// <summary>
/// A base class for clients which use an online API to lookup geo-ip info and store it in a local cache.
/// </summary>
public abstract class GeoIpApiClientBase : IGeoIpClient
{
  private readonly Dictionary<uint, object> cache = new Dictionary<uint, object>();

  private readonly BlockingCollection<IPAddress> queue = new ();
  private string cacheFile;

  protected abstract int GetMaxIpsPerRequest();

  public GeoIpApiClientBase()
  {
    var baseDir = Path.GetDirectoryName(this.GetType().Assembly.Location) ?? ".";
    this.cacheFile = Path.Combine(baseDir, "locations.txt");
    MoveConfigFilesFromOldLocation();

    ThreadPool.QueueUserWorkItem(_ => this.ProcessLoop());
  }

  #region MoveConfigFilesFromOldLocation()
  private void MoveConfigFilesFromOldLocation()
  {
    if (File.Exists(this.cacheFile)) 
      return;

    try
    {
      var geoCache = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "locations.txt");
      if (File.Exists(geoCache))
        File.Move(geoCache, this.cacheFile);
    }
    catch
    {
      // ignore
    }
  }
  #endregion

  #region ProcessLoop()
  private void ProcessLoop()
  {
    using var client = new XWebClient(5000);
    int sleepMillis = 1000;
    while (true)
    {
      Thread.Sleep(sleepMillis);
      var count = Math.Max(1, Math.Min(GetMaxIpsPerRequest(), this.queue.Count));
      var ips = new IPAddress[count];
      for (int i = 0; i < count; i++)
        ips[i] = this.queue.Take();
      if (ips[ips.Length - 1] == null)
        break;

      Dictionary<uint,GeoInfo> geoInfos = null;
      try
      {
        geoInfos = this.RequestGeoInfo(client, ips, ref sleepMillis);
      }
      catch
      {
        // ignore
      }

      foreach (var ip in ips)
      {
        var ipInt = Ip4Utils.ToInt(ip);
        Action<GeoInfo> callbacks = null;
        GeoInfo geoInfo = null;
        lock (cache)
        {
          bool isSet = cache.TryGetValue(ipInt, out var o);
          if (geoInfos == null || !geoInfos.TryGetValue(ipInt, out geoInfo))
          {
            //this.cache.Remove(ipInt);
          }
          else
          {
            callbacks = o as Action<GeoInfo>;
            if (geoInfo != null || !isSet)
              cache[ipInt] = geoInfo;
          }
        }

        if (callbacks != null && geoInfo != null)
        {
          //ThreadPool.QueueUserWorkItem(ctx => callbacks(geoInfo));
          callbacks(geoInfo);
        }
      }
    }
  }
  #endregion

  protected abstract Dictionary<uint, GeoInfo> RequestGeoInfo(XWebClient client,IPAddress[] ips, ref int sleepMillis);

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
        if (cached == null)
        {
          cache.Add(ipInt, callback);
          this.queue.Add(ip);
        }
        else
        {
          var callbacks = (Action<GeoInfo>)cached;
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
    while (this.queue.TryTake(out _))
    {
    }

    lock (cache)
    {
      var keys = cache.Keys.ToList();
      foreach (var key in keys)
      {
        if (cache[key] is not GeoInfo)
          cache.Remove(key);
      }
    }
  }
  #endregion

  #region LoadCache()
  public bool LoadCache()
  {
    if (!File.Exists(this.cacheFile))
      return true;
    lock (cache)
    {
      foreach (var line in File.ReadAllLines(this.cacheFile))
      {
        try
        {
          var parts = line.Split(new[] { '=' }, 2);
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

    return true;
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
    catch
    {
      // ignore
    }
  }
  #endregion

  #region Dispose()
  public virtual void Dispose()
  {
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
