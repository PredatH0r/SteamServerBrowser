using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace ServerBrowser
{
  internal static class Ip4Utils
  {
    public static uint ToInt(IPAddress ip)
    {
      return ToInt(ip.GetAddressBytes());
    }

    internal static uint ToInt(params byte[] bytes)
    {
      if (bytes.Length != 4)
        throw new ArgumentException("expected 4 bytes, got " + bytes.Length, "bytes");
      return (uint)((bytes[0] << 24) | (bytes[1] << 16) | (bytes[2] << 8) | bytes[3]);
    }

    internal static bool IsInSubnet(int ip, int bits, byte subnetA, byte subnetB, byte subnetC, byte subnetD)
    {
      var mask = -1 << bits;
      return (ip & mask) == (ToInt(subnetA, subnetB, subnetC, subnetD) & mask);
    }

    public static IPEndPoint ParseEndpoint(string addressAndPort)
    {
      string info = addressAndPort;
      var parts = info.Split(':');
      int port;
      if (parts.Length < 2 || !int.TryParse(parts[1], out port))
        port = 27011;

      try
      {
        var ip = Dns.GetHostAddresses(parts[0]).FirstOrDefault(p => p.AddressFamily == AddressFamily.InterNetwork);
        if (ip != null)
          return new IPEndPoint(ip, port);
      }
      catch
      {
      }

      return new IPEndPoint(IPAddress.None, 0);
    }
  }
}
