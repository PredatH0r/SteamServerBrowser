using System;
using System.Collections.ObjectModel;
using System.Net;

namespace QueryMaster
{
  /// <summary>
  ///   Encapsulates a method that has a parameter of type ReadOnlyCollection which accepts IPEndPoint instances.
  ///   Invoked when a reply from Master Server is received.
  /// </summary>
  public delegate void MasterIpCallback(ReadOnlyCollection<Tuple<IPEndPoint,ServerInfo>> endPoints, Exception error, bool isPartialResult);

  /// <summary>
  ///   Provides methods to query master server.
  ///   An instance can only be used for a single request and is automatically disposed when the request completes or times out
  /// </summary>
  public abstract class MasterServer
  {
    public MasterServer()
    {
      this.Retries = 5;
      this.GetAddressesLimit = 1000;
    }

    /// <summary>
    /// Limit GetAddresses to stop requesting more servers once this number is reached or exceeded.
    /// The steam master server throttles communication with clients to 30 UDP packets per minute.
    /// As a result, trying to get more servers than can fit in those packets will only cause timeouts.
    /// This throtteling happens per client machine. Once reached, Steam won't answer any further
    /// queries from that machine for the next minute.
    /// In a single UDP packet of 1392 bytes the master server will return 231 IP addresses, which
    /// results in an absolute maximum of 6930 servers to be retrieved (if no other requests happened in the same minute).
    /// The default value is set to 1000.
    /// </summary>
    public int GetAddressesLimit { get; set; }

    /// <summary>
    /// Gets/sets the number of send+receive attempts that will be made in case of timeouts.
    /// The default value is 5 (1 try + 4 retries)
    /// </summary>
    public int Retries { get; set; }

    public abstract void GetAddresses(Region region, MasterIpCallback callback, IpFilter filter);

  }
}