using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace ServerBrowser
{
  class GeoIpClient
  {
    private const int ThreadCount = 10;
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
          using (var client = new XWebClient())
          {
            var result = client.DownloadString(url);
            var callbacks = (Action<string>)cache[ipInt];
            string country = this.HandleResult(ipInt, result);
            ThreadPool.QueueUserWorkItem(ctx => callbacks(country));
          }
        }
        catch
        {
        }        
      }
    }
    #endregion

    #region HandleResult()
    private string HandleResult(uint ip, string result)
    {
      var parts = result.Split(',');
      if (parts.Length >= 2)
      {
        lock (cache)
        {
          cache[ip] = parts[1];
          return parts[1];
        }
      }
      return null;
    }
    #endregion

    #region Lookup()
    public void Lookup(IPAddress ip, Action<string> callback)
    {
      uint ipInt = Ip4Utils.ToInt(ip);
      string country;
      lock (cache)
      {
        object cached;
        if (!cache.TryGetValue(ipInt, out cached))
        {
          cache.Add(ipInt, callback);
          this.queue.Add(ip);
          return;
        }
        country = cached as string;
        if (country == null)
        {
          var callbacks = (Action<string>) cached;
          callbacks += callback;
          cache[ipInt] = callbacks;
          return;
        }
      }
      callback(country);    
    }
    #endregion

  }
}
