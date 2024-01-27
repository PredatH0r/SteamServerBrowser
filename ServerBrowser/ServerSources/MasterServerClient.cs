using System.Net;
using QueryMaster;

namespace ServerBrowser
{
  public class MasterServerClient : IServerSource
  {
    private readonly IPEndPoint masterServerEndPoint;
    private readonly string steamWebApiKey;

    public MasterServerClient(IPEndPoint masterServerEndpoint, string steamWebApiKey)
    {
      this.masterServerEndPoint = masterServerEndpoint;
      this.steamWebApiKey = steamWebApiKey;
    }

    public void GetAddresses(Region region, IpFilter filter, int maxResults, MasterIpCallback callback)
    {
      var master = masterServerEndPoint.Port == 0 ? (MasterServer)new MasterServerWebApi(steamWebApiKey) : new MasterServerUdp(masterServerEndPoint);
      master.GetAddressesLimit = maxResults;
      master.GetAddresses(region, callback, filter);
    }
  }
}