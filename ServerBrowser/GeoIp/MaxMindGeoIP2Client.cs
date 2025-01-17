using MaxMind.GeoIP2;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;

namespace ServerBrowser.GeoIp;

internal class MaxMindGeoIP2Client : IGeoIpClient
{
  private DatabaseReader dbReader;

  public bool LoadCache()
  {
    var path = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location) ?? ".", "GeoIp", "GeoLite2-City.mmdb");
    if (!File.Exists(path))
      path = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location) ?? ".", "GeoLite2-City.mmdb");
    if (!File.Exists(path))
    {
      if (XtraMessageBox.Show(
            "MaxMind GeoLite2 database file \"GeoLite2-City.mmdb\" could not be found.\n" +
            "You can sign-up for free at https://www.maxmind.com/en/geolite2/signup,\n" + 
            "download the file and copy it into the SteamServerBrowser folder.\n" +
            "\nDo you want to open the URL?", "GeoLite2-City database not found", 
            MessageBoxButtons.YesNo, DefaultBoolean.False) == DialogResult.Yes)
      {
        Process.Start("https://www.maxmind.com/en/geolite2/signup");
      }

      return false;
    }

    try
    {
      this.dbReader = new DatabaseReader(path);
      dbReader.TryCity(new IPAddress([131, 130, 1, 11]), out _);
      return true;
    }
    catch (Exception ex)
    {
      XtraMessageBox.Show("There was a problem with the MaxMind GeoLite2 database file:\n" + ex.Message);
    }

    return false;
  }

  public void Lookup(IPAddress ip, Action<GeoInfo> callback)
  {
    GeoInfo info = null;
    if (this.dbReader != null)
    {
      try
      {
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
      }
      catch
      {
        this.dbReader = null;
      }
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