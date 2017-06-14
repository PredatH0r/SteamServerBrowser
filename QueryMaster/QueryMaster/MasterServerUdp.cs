using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace QueryMaster
{
  /// <summary>
  ///   Provides methods to query master server.
  ///   An instance can only be used for a single request and is automatically disposed when the request completes or times out
  /// </summary>
  public class MasterServerUdp : MasterServer
  {
    private readonly IPEndPoint endPoint;
    private readonly IPEndPoint SeedEndpoint = new IPEndPoint(0, 0);

    public MasterServerUdp(IPEndPoint endPoint)
    {
      this.endPoint = endPoint;
    }

    /// <summary>
    /// Gets a server list from the Steam master server.
    /// The callback is invoked from a background thread every time a batch of servers is received.
    /// The end of the list is marked with an IPEndPoint of 0.0.0.0:0
    /// In case of a timeout or an exception, the callback is invoked with a NULL parameter value.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the object is still busy handling a previous call to GetAddresses</exception>
    public override void GetAddresses(Region region, MasterIpCallback callback, IpFilter filter)
    {
      ThreadPool.QueueUserWorkItem(x =>
      {
        var udpSocket = new Socket(AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, ProtocolType.Udp);
        udpSocket.SendTimeout = 500;
        udpSocket.ReceiveTimeout = 1000;
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
              callback(null, null);
              break;
            }
            ThreadPool.QueueUserWorkItem(y => callback(endpoints, null));
            totalCount += endpoints.Count;
            nextSeed = endpoints.Last().Item1;
          } while (!nextSeed.Equals(SeedEndpoint) && totalCount < GetAddressesLimit);
        }
        catch (Exception ex)
        {
          callback(null, ex);
        }
        finally
        {
          try { udpSocket.Close(); }
          catch { }
        }
      });
    }

    private ReadOnlyCollection<Tuple<IPEndPoint, ServerInfo>> SendAndReceive(Socket udpSocket, byte[] recvData, Region region, IpFilter filter, IPEndPoint seed)
    {
      var msg = MasterUtil.BuildPacket(seed.ToString(), region, filter);
      udpSocket.Send(msg);

      int len = udpSocket.Receive(recvData, 0, recvData.Length, SocketFlags.None);
      byte[] data = new byte[len];
      Array.Copy(recvData, data, data.Length);
      var endpoints = MasterUtil.ProcessPacket(data);
      return new ReadOnlyCollection<Tuple<IPEndPoint, ServerInfo>>(endpoints.Select(ep => new Tuple<IPEndPoint, ServerInfo>(ep, null)).ToList());
    }
  }
}