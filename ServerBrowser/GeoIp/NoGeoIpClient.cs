using System;
using System.Net;

namespace ServerBrowser.GeoIp
{
  internal class NoGeoIpClient : IGeoIpClient
  {
    public bool LoadCache()
    {
      return true;
    }

    public void Lookup(IPAddress ip, Action<GeoInfo> callback)
    {
      callback(null);
    }

    public void CancelPendingRequests()
    {
    }

    public void SaveCache()
    {
    }
    public void Dispose()
    {
    }

  }
}
