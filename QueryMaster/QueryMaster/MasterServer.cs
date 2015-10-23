using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace QueryMaster
{
  /// <summary>
  ///   Encapsulates a method that has a parameter of type ReadOnlyCollection which accepts IPEndPoint instances.
  ///   Invoked when a reply from Master Server is received.
  /// </summary>
  /// <param name="endPoints">Server Sockets</param>
  public delegate void MasterIpCallback(ReadOnlyCollection<IPEndPoint> endPoints);

  /// <summary>
  ///   Provides methods to query master server.
  ///   An instance can only be used for a single request and is automatically disposed when the request completes or times out
  /// </summary>
  public class MasterServer
  {
    private static readonly IPEndPoint SeedEndpoint = new IPEndPoint(IPAddress.Any, 0);    
    private readonly IPEndPoint endPoint;

    public MasterServer(IPEndPoint endPoint)
    {
      this.endPoint = endPoint;
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

    /// <summary>
    /// Gets a server list from the Steam master server.
    /// The callback is invoked from a background thread every time a batch of servers is received.
    /// The end of the list is marked with an IPEndPoint of 0.0.0.0:0
    /// In case of a timeout or an exception, the callback is invoked with a NULL parameter value.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the object is still busy handling a previous call to GetAddresses</exception>
    public void GetAddresses(Region region, MasterIpCallback callback, IpFilter filter)
    {
      ThreadPool.QueueUserWorkItem(x =>
      {
        var udpSocket = new Socket(AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, ProtocolType.Udp);
        udpSocket.SendTimeout = 500;
        udpSocket.ReceiveTimeout = 500;
        udpSocket.Connect(endPoint);
        byte[] recvData = new byte[1400];

        try
        {
          var nextSeed = SeedEndpoint;
          int totalCount = 0;
          do
          {
            var curSeed = nextSeed;
            var endpoints = Util.RunWithRetries(() => SendAndReceive(udpSocket, recvData, region, filter, curSeed), this.Retries);
            if (endpoints == null)
            {
              callback(null);
              break;
            }
            ThreadPool.QueueUserWorkItem(y => callback(endpoints));
            totalCount += endpoints.Count;
            nextSeed = endpoints.Last();
          } while (!nextSeed.Equals(SeedEndpoint) && totalCount < GetAddressesLimit);
        }
        catch (Exception)
        {
          callback(null);
        }
        finally
        {
          try { udpSocket.Close(); }
          catch { }
        }
      });
    }

    private ReadOnlyCollection<IPEndPoint> SendAndReceive(Socket udpSocket, byte[] recvData, Region region, IpFilter filter, IPEndPoint seed)
    {
      var msg = MasterUtil.BuildPacket(seed.ToString(), region, filter);
      udpSocket.Send(msg);

      int len = udpSocket.Receive(recvData, 0, recvData.Length, SocketFlags.None);
      byte[] data = new byte[len];
      Array.Copy(recvData, data, data.Length);
      return MasterUtil.ProcessPacket(data);
    }

  }
}