using MaxMind.GeoIP2;
using System;
using System.IO;
using System.Net;

namespace ServerBrowser.GeoIp
{

  internal class MaxMindGeoIP2Client : IGeoIpClient
  {
    private DatabaseReader dbReader;

    public void LoadCache()
    {
      this.dbReader = new DatabaseReader(Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "GeoIp\\GeoLite2-City.mmdb"));
    }

    public void Lookup(IPAddress ip, Action<GeoInfo> callback)
    {
      GeoInfo info = null;
      if (dbReader.TryCity(ip, out var response) && response != null)
      {
        var c = response.Country;
        var region = response.Subdivisions;
        info = new GeoInfo(c.IsoCode, c.Name,
          region.Count > 0 ? region[0].Name : "",
          region.Count > 1 ? region[1].Name : "",
          response.City.Name,
          (decimal)(response.Location.Latitude ?? 0), 
          (decimal)(response.Location.Longitude ?? 0));
      }
      callback(info);
    }

    public void CancelPendingRequests()
    {
    }

    public void SaveCache()
    {
    }

    public void Dispose()
    {
      dbReader?.Dispose();
      dbReader = null;
    }
  }
}
