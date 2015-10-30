using System;
using System.Net;
using System.Net.Sockets;

namespace QueryMaster
{
  internal class ServerSocket : IDisposable
  {
    internal static readonly int UdpBufferSize = 1400;
    internal static readonly int TcpBufferSize = 4110;
    internal IPEndPoint Address;
    protected internal int BufferSize;
    protected bool IsDisposed;
    private readonly byte[] recvData;

    internal ServerSocket(SocketType type)
    {
      switch (type)
      {
        case SocketType.Tcp:
          socket = new Socket(AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, ProtocolType.Tcp);
          BufferSize = TcpBufferSize;
          break;
        case SocketType.Udp:
          socket = new Socket(AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, ProtocolType.Udp);
          BufferSize = UdpBufferSize;
          break;
        default:
          throw new ArgumentException("An invalid SocketType was specified.");
      }
      socket.SendTimeout = 3000;
      socket.ReceiveTimeout = 3000;
      recvData = new byte[BufferSize];
      IsDisposed = false;
    }

    internal Socket socket { set; get; }

    public virtual void Dispose()
    {
      if (IsDisposed)
        return;
      if (socket != null)
        socket.Close();
      IsDisposed = true;
    }

    internal void Connect(IPEndPoint address)
    {
      Address = address;
      socket.Connect(Address);
    }

    internal int SendData(byte[] data)
    {
      return socket.Send(data);
    }

    internal byte[] ReceiveData()
    {
      var recv = socket.Receive(recvData);
      var data = new byte[recv];
      Array.Copy(recvData, 0, data, 0, recv);
      return data;
    }
  }
}