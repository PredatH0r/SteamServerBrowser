using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ServerBrowser;

/// <summary>
/// Geo-IP client using the free https://ip-api.com/docs/api:batch service
/// </summary>
class IpDashApiDotComGeoIpClient : GeoIpApiClientBase
{
  private const string DefaultServiceUrlFormat = "http://ip-api.com/batch?fields=223&lang=en";


  public string ServiceUrlFormat { get; set; }

  public IpDashApiDotComGeoIpClient()
  {
    this.ServiceUrlFormat = DefaultServiceUrlFormat;
  }

  protected override int GetMaxIpsPerRequest() => 100;

  protected override Dictionary<uint, GeoInfo> RequestGeoInfo(XWebClient client, IPAddress[] ips, ref int sleepMillis)
  {
    var req = new StringBuilder(ips.Length * 18);
    req.Append("[");
    foreach (var ip in ips)
      req.Append('"').Append(ip).Append("\",");
    req[req.Length - 1] = ']';

    try
    {
      var result = client.UploadString(ServiceUrlFormat, req.ToString());
      var rateLimit = client.ResponseHeaders["X-Rl"];
      sleepMillis = rateLimit == "0" ? (int.TryParse(client.ResponseHeaders["X-Ttl"], out var sec) ? sec * 1000 : 4000) : 0;
      var geoInfos = this.HandleResult(ips, result);
      return geoInfos;
    }
    catch
    {
      return null;
    }
  }

  #region HandleResult()
  private Dictionary<uint, GeoInfo> HandleResult(IList<IPAddress> ips, string result)
  {
    var ser = new DataContractJsonSerializer(typeof(IpApiResponse[]));
    var infoArray = (IpApiResponse[])ser.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(result)));

    var ok = ips.Count == infoArray.Length;
    var data = new Dictionary<uint, GeoInfo>();
    for (int i = 0; i < ips.Count; i++)
    {
      var ipInt = Ip4Utils.ToInt(ips[i]);
      var info = infoArray[i];
      var geoInfo = ok ? new GeoInfo(info.countryCode, info.country, info.region, info.regionName, info.city, info.lat, info.lon) : null;
      data[ipInt] = geoInfo;
    }

    return data;
  }
  #endregion
}

#region class IpApiResponse
public class IpApiResponse
{
  public string country;
  public string countryCode;
  public string region;
  public string regionName;
  public string city;
  public decimal lat;
  public decimal lon;
}
#endregion
