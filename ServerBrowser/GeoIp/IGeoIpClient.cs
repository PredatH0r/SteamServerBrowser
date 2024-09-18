using System;
using System.Net;

namespace ServerBrowser
{
  internal interface IGeoIpClient : IDisposable
  {
    void Lookup(IPAddress ip, Action<GeoInfo> callback);
    void CancelPendingRequests();
    void LoadCache();
    void SaveCache();
  }
}