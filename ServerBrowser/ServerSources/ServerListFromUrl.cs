using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using QueryMaster;

namespace ServerBrowser
{
  public class ServerListFromUrl : IServerSource
  {
    private readonly Uri url;

    public ServerListFromUrl(Uri url)
    {
      this.url = url;
    }

    public void GetAddresses(Region region, IpFilter filter, int maxResults, MasterIpCallback callback)
    {
      try
      {
        using (var client = new XWebClient())
        {
          var text = client.DownloadString(this.url);
          if (text == null)
          {
            callback(null);
            return;
          }

          var lines = text.Split('\n');
          var endpoints = new List<IPEndPoint>(lines.Length);
          int i = 0;
          foreach (var line in lines)
          {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var parts = line.Split(':');
            if (parts.Length != 2) continue;

            IPAddress addr;
            int port;
            if (IPAddress.TryParse(parts[0], out addr) && int.TryParse(parts[1].TrimEnd(), out port))
            {
              endpoints.Add(new IPEndPoint(addr, port));
              if (++i == maxResults)
                break;
            }
          }
          endpoints.Add(new IPEndPoint(IPAddress.Any, 0));

          callback(new ReadOnlyCollection<IPEndPoint>(endpoints));
        }
      }
      catch
      {
        callback(null);
      }
    }
  }
}