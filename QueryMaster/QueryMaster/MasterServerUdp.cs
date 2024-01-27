using System;
using System.Collections.Generic;
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
        udpSocket.ReceiveTimeout = 500;
        udpSocket.Connect(endPoint);
        byte[] recvData = new byte[1400];

        try
        {
          var nextSeed = SeedEndpoint;
          int totalCount = 0;
          bool atEnd = false;
          while (!atEnd)
          {
            var curSeed = nextSeed;
            var endpoints = Util.RunWithRetries(() => SendAndReceive(udpSocket, recvData, region, filter, curSeed), this.Retries);
            if (endpoints == null)
            {
              callback(null, null, false);
              break;
            }

            nextSeed = endpoints.Last();

            atEnd = nextSeed.Equals(SeedEndpoint);
            var serverList = endpoints.Select(ep => new Tuple<IPEndPoint, ServerInfo>(ep, null)).ToList();
            if (atEnd)
              serverList.RemoveAt(serverList.Count - 1); // remove the 0.0.0.0:0 end-of-list marker

            ThreadPool.QueueUserWorkItem(y => callback(new ReadOnlyCollection<Tuple<IPEndPoint, ServerInfo>>(serverList), null, !atEnd));
            totalCount += endpoints.Count;

            if (GetAddressesLimit != 0)
              atEnd |= totalCount >= GetAddressesLimit;
          } //while (!nextSeed.Equals(SeedEndpoint) && totalCount < GetAddressesLimit);
        }
        catch (Exception ex)
        {
          callback(null, ex, false);
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