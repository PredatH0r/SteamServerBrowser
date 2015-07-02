using QueryMaster;

namespace ServerBrowser
{
  public interface IServerSource
  {
    void GetAddresses(Region region, IpFilter filter, int maxResults, MasterIpCallback callback);
  }
}
