using System;
using System.Net;

namespace ServerBrowser
{
  internal static class Ip4Utils
  {
    internal static int IpAsInt(IPAddress ip)
    {
      return IpAsInt(ip.GetAddressBytes());
    }

    internal static int IpAsInt(params byte[] bytes)
    {
      if (bytes.Length != 4)
        throw new ArgumentException("expected 4 bytes, got " + bytes.Length, "bytes");
      return (bytes[0] << 24) | (bytes[1] << 16) | (bytes[2] << 8) | bytes[3];
    }

    internal static bool IsInSubnet(int ip, int bits, byte subnetA, byte subnetB, byte subnetC, byte subnetD)
    {
      var mask = -1 << bits;
      return (ip & mask) == (IpAsInt(subnetA, subnetB, subnetC, subnetD) & mask);
    }
  }
}
