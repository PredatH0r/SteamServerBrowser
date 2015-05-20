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
  public class MasterServer : IDisposable
  {
    private enum State { Idle, Busy, Disposed }
    private static readonly IPEndPoint SeedEndpoint = new IPEndPoint(IPAddress.Any, 0);
    private const int RetriesPerRequest = 5;
    private readonly Socket udpSocket;
    private readonly byte[] recvData = new byte[1400];
    private State state;

    internal MasterServer(IPEndPoint endPoint)
    {
      udpSocket = new Socket(AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, ProtocolType.Udp);
      udpSocket.SendTimeout = 500;
      udpSocket.ReceiveTimeout = 500;
      udpSocket.Connect(endPoint);
    }

    /// <summary>
    ///   Disposes all the resources used MasterServer instance
    /// </summary>
    public void Dispose()
    {
      lock (this)
      {
        if (state != State.Disposed)
          udpSocket.Close();
        state = State.Disposed;
      }
    }
   
    public void GetAddresses(Region region, MasterIpCallback callback, IpFilter filter)
    {
      lock (this)
      {
        if (state != State.Idle)
          throw new InvalidOperationException("MasterServer.GetAddresses() can only be called once per instance");
        state = State.Busy;
      }

      ThreadPool.QueueUserWorkItem(x =>
      {
        using (this)
        {
          try
          {
            var nextSeed = SeedEndpoint;
            int totalCount = 0;
            do
            {
              var curSeed = nextSeed;
              var endpoints = this.RunWithRetries(() => SendAndReceive(region, filter, curSeed), RetriesPerRequest);
              ThreadPool.QueueUserWorkItem(y => callback(endpoints));
              totalCount += endpoints.Count;
              nextSeed = endpoints.Last();
            } while (!nextSeed.Equals(SeedEndpoint) && totalCount < 1000);
          }
          catch (Exception)
          {
            callback(null);
            //if (!(ex is TimeoutException))
            //  throw;
          }
        }
      });
    }

    private ReadOnlyCollection<IPEndPoint> SendAndReceive(Region region, IpFilter filter, IPEndPoint seed)
    {
      var msg = MasterUtil.BuildPacket(seed.ToString(), region, filter);
      udpSocket.Send(msg);

      int len = udpSocket.Receive(recvData, 0, recvData.Length, SocketFlags.None);
      byte[] data = new byte[len];
      Array.Copy(recvData, data, data.Length);
      return MasterUtil.ProcessPacket(data);
    }

    private T RunWithRetries<T>(Func<T> action, int tries)
    {
      while (true)
      {
        try
        {
          return action();
        }
        catch (SocketException ex)
        {
          if (ex.SocketErrorCode != SocketError.TimedOut)
            throw;
          
          if (--tries <= 0)
            throw new TimeoutException();
        }
      }
    }
  }
}