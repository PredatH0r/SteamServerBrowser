using System.Net;
using QueryMaster;

namespace ServerBrowser
{
  public class MasterServerClient : IServerSource
  {
    private readonly IPEndPoint masterServerEndPoint;

    public MasterServerClient(IPEndPoint masterServerEndpoint)
    {
      this.masterServerEndPoint = masterServerEndpoint;
    }

    public void GetAddresses(Region region, IpFilter filter, int maxResults, MasterIpCallback callback)
    {
      var master = masterServerEndPoint.Port == 0 ? (MasterServer)new MasterServerWebApi() : new MasterServerUdp(masterServerEndPoint);
      master.GetAddressesLimit = maxResults;
      master.GetAddresses(region, callback, filter);
    }
  }
}