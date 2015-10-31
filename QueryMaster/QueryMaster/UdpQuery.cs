using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using Ionic.BZip2;
using Ionic.Crc;

namespace QueryMaster
{
  internal class UdpQuery : ServerSocket
  {
    private const int SinglePacket = -1;
    private const int MultiPacket = -2;
    private bool isFirstPacket = true;
    private EngineType Type;

    internal UdpQuery(IPEndPoint address, int sendTimeOut, int receiveTimeOut)
      : base(SocketType.Udp)
    {
      Connect(address);
      socket.SendTimeout = sendTimeOut;
      socket.ReceiveTimeout = receiveTimeOut;
    }

    public bool SendFirstPacketTwice { get; set; }

    internal byte[] GetResponse(byte[] msg, EngineType type, Stopwatch sw = null)
    {
      Type = type;
      byte[] recvData = null;
      if (this.isFirstPacket && this.SendFirstPacketTwice)
      {
        this.isFirstPacket = false;
        SendData(msg);
      }

      try
      {
        sw?.Start();
        SendData(msg);
        recvData = ReceiveData();
        sw?.Stop();
        if (sw != null)
          Thread.Yield(); // improve accuracy of Ping for other threads

        var header = BitConverter.ToInt32(recvData, 0);
        switch (header)
        {
          case SinglePacket:
            return ParseSinglePkt(recvData);
          case MultiPacket:
            return ParseMultiPkt(recvData);
          default:
            throw new InvalidHeaderException("Protocol header is not valid");
        }
      }
      catch (Exception e)
      {
        e.Data.Add("ReceivedData", recvData);
        throw;
      }
    }

    private byte[] ParseSinglePkt(byte[] data)
    {
      return data.Skip(4).ToArray();
    }

    private byte[] ParseMultiPkt(byte[] data)
    {
      switch (Type)
      {
        case EngineType.Source:
          return SourcePackets(data);
        case EngineType.GoldSource:
          return GoldSourcePackets(data);
        default:
          throw new ArgumentException("An invalid EngineType was specified.");
      }
    }

    private byte[] GoldSourcePackets(byte[] data)
    {
      var pktCount = data[8] & 0x0F;
      var pktList = new List<KeyValuePair<int, byte[]>>(pktCount);
      pktList.Add(new KeyValuePair<int, byte[]>(data[8] >> 4, data));

      byte[] recvData;
      for (var i = 1; i < pktCount; i++)
      {
        recvData = new byte[BufferSize];
        recvData = ReceiveData();
        pktList.Add(new KeyValuePair<int, byte[]>(recvData[8] >> 4, recvData));
      }

      pktList.Sort((x, y) => x.Key.CompareTo(y.Key));
      var byteList = new List<byte>();
      byteList.AddRange(pktList[0].Value.Skip(13));

      for (var i = 1; i < pktList.Count; i++)
      {
        byteList.AddRange(pktList[i].Value.Skip(9));
      }
      return byteList.ToArray<byte>();
    }

    private byte[] SourcePackets(byte[] data)
    {
      var pktCount = data[8];
      var pktList = new List<KeyValuePair<byte, byte[]>>(pktCount);
      pktList.Add(new KeyValuePair<byte, byte[]>(data[9], data));

      byte[] recvData;
      for (var i = 1; i < pktCount; i++)
      {
        recvData = ReceiveData();
        pktList.Add(new KeyValuePair<byte, byte[]>(recvData[9], recvData));
      }

      pktList.Sort((x, y) => x.Key.CompareTo(y.Key));
      Parser parser = null;
      var isCompressed = false;
      var checksum = 0;
      var recvList = new List<byte>();
      parser = new Parser(pktList[0].Value);
      parser.Skip(4); //header
      if (parser.ReadInt() < 0) //ID
        isCompressed = true;
      parser.ReadByte(); //total
      int pktId = parser.ReadByte(); // packet id
      parser.ReadShort(); //size
      if (isCompressed)
      {
        parser.Skip(2); //[this is not equal to decompressed length of data]
        checksum = parser.ReadInt(); //Checksum
      }
      recvList.AddRange(parser.GetUnParsedData());

      for (var i = 1; i < pktList.Count; i++)
      {
        parser = new Parser(pktList[i].Value);
        parser.Skip(12); //multipacket header only
        recvList.AddRange(parser.GetUnParsedData());
      }
      recvData = recvList.ToArray<byte>();
      if (isCompressed)
      {
        recvData = Decompress(recvData);
        if (!IsValid(recvData, checksum))
          throw new InvalidPacketException("packet's checksum value does not match with the calculated checksum");
      }
      return recvData.Skip(4).ToArray();
    }

    private byte[] Decompress(byte[] data)
    {
      using (var input = new MemoryStream(data))
      using (var output = new MemoryStream())
      using (var unZip = new BZip2InputStream(input))
      {
        var ch = unZip.ReadByte();

        while (ch != -1)
        {
          output.WriteByte((byte) ch);
          ch = unZip.ReadByte();
        }
        output.Flush();
        return output.ToArray();
      }
    }

    private bool IsValid(byte[] data, int Checksum)
    {
      using (var Input = new MemoryStream(data))
      {
        if (Checksum == new CRC32().GetCrc32(Input))
          return true;
        return false;
      }
    }
  }
}