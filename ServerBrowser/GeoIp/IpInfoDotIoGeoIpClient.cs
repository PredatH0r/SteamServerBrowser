using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ServerBrowser.GeoIp;

internal class IpInfoDotIoGeoIpClient : GeoIpApiClientBase
{
  private const string DefaultServiceUrlFormat = "http://ipinfo.io/{0}/json";

  protected override int GetMaxIpsPerRequest() => 1;

  protected override Dictionary<uint, GeoInfo> RequestGeoInfo(XWebClient client, IPAddress[] ips, ref int sleepMillis)
  {
    try
    {
      var url = string.Format(DefaultServiceUrlFormat, ips[0]);
      var result = client.DownloadString(url);
      var geoInfos = this.HandleResult(ips, result);
      sleepMillis = 0; // no delay needed, throttling happens per client-IP
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
    var ser = new DataContractJsonSerializer(typeof(IpInfoResponse));
    var info = (IpInfoResponse)ser.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(result)));

    var ipInt = Ip4Utils.ToInt(ips[0]);

    decimal lat = 0, lon = 0;
    var parts = (info.loc ?? "").Split(',');
    if (parts.Length == 2)
    {
      decimal.TryParse(parts[0], NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out lat);
      decimal.TryParse(parts[1], NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out lon);
    }

    var data = new Dictionary<uint, GeoInfo>();
    var geoInfo = new GeoInfo(info.country, info.country, info.region, info.region, info.city, lat, lon);
    data[ipInt] = geoInfo;
    return data;
  }
  #endregion

}

#region class IpInfoResponse
/// <summary>
/// Mapping for json response
/// </summary>
public class IpInfoResponse
{
  public string ip;
  public string hostname;
  public string city;
  public string region;
  public string country;
  public string loc;
  public string org;
  public string postal;
  public string timezone;
}
#endregion