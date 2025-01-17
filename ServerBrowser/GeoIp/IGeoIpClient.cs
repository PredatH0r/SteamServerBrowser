using System;
using System.Net;

namespace ServerBrowser
{
  internal interface IGeoIpClient : IDisposable
  {
    bool LoadCache();
    void Lookup(IPAddress ip, Action<GeoInfo> callback);
    void CancelPendingRequests();
    void SaveCache();
  }
}