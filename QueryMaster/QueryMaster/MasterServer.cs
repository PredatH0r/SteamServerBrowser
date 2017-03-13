using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml.Serialization;

namespace QueryMaster
{
  /// <summary>
  ///   Encapsulates a method that has a parameter of type ReadOnlyCollection which accepts IPEndPoint instances.
  ///   Invoked when a reply from Master Server is received.
  /// </summary>
  /// <param name="endPoints">Server Sockets</param>
  public delegate void MasterIpCallback(ReadOnlyCollection<Tuple<IPEndPoint,ServerInfo>> endPoints, Exception error);

  #region WebAPI XML response mappings: class Response, Message
  public class Message
  {
    /// <summary>
    /// IP:port
    /// </summary>
    public string addr { get; set; }
    public ushort gameport { get; set; }
    public long steamid { get; set; }
    public string name { get; set; }
    public int appid { get; set; }
    public string gamedir { get; set; }
    public string version { get; set; }
    public string product { get; set; }
    public string region { get; set; }
    public int players { get; set; }
    public int max_players { get; set; }
    public int bots { get; set; }
    public string map { get; set; }
    public bool secure { get; set; }
    public bool dedicated { get; set; }
    public string os { get; set; }
    public string gametype { get; set; }
  }

  [XmlRoot("response")]
  public class Response
  {
    [XmlArray("servers")]
    [XmlArrayItem("message")]
    public Message[] Servers;
  }

  #endregion

  class XWebClient : WebClient
  {
    public XWebClient()
    {
      this.Proxy = null;
    }

    protected override WebRequest GetWebRequest(Uri address)
    {
      var req = base.GetWebRequest(address);
      req.Timeout = 5000;
      return req;
    }
  }

  /// <summary>
  ///   Provides methods to query master server.
  ///   An instance can only be used for a single request and is automatically disposed when the request completes or times out
  /// </summary>
  public class MasterServer
  {
    const string SteamWebApiKey = "B7D245299F6F990504A86FF91EC9D6BD"; // create an account and get a steam web api key at http://steamcommunity.com/dev/apikey

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
#if true
        try
        {
          using (var cli = new XWebClient())
          {
            var filters = MasterUtil.ProcessFilter(filter);
            var url = $"https://api.steampowered.com/IGameServersService/GetServerList/v1/?key={SteamWebApiKey}&format=xml&filter={filters}&limit={GetAddressesLimit}";
            var xml = cli.DownloadString(url);
            var ser = new XmlSerializer(typeof (Response));
            var resp = (Response) ser.Deserialize(new StringReader(xml));

            var endpoints = new List<Tuple<IPEndPoint,ServerInfo>>();
            foreach (var msg in resp.Servers)
            {
              try
              {
                int i = msg.addr.IndexOf(':');
                if (i > 0)
                {
                  var info = ConvertToServerInfo(msg);
                  endpoints.Add(new Tuple<IPEndPoint, ServerInfo>(info.EndPoint, info));
                }
              }
              catch
              {
              }
            }
            callback(new ReadOnlyCollection<Tuple<IPEndPoint,ServerInfo>>(endpoints), null);
          }
        }
        catch(Exception ex)
        {
          callback(null, ex);
        }
#else
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
            ThreadPool.QueueUserWorkItem(y => callback(endpoints, null));
            totalCount += endpoints.Count;
            nextSeed = endpoints.Last();
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
#endif
      });
    }

    private ServerInfo ConvertToServerInfo(Message msg)
    {
      var si = new ServerInfo();
      si.Address = msg.addr;
      si.Bots = (byte)msg.bots;
      si.Description = msg.gametype;
      si.Directory = msg.gamedir;
      si.Environment = msg.os == "w" ? "Windows" : msg.os == "l" ? "Linux" : msg.os;
      si.Extra.GameId = msg.appid;
      si.Extra.Port = msg.gameport;
      si.Extra.SteamID = msg.steamid;
      si.GameVersion = msg.version;
      si.Id = msg.appid < 0x10000 ? (ushort) msg.appid : (ushort)0;
      si.IsSecure = msg.secure;
      si.Map = msg.map;
      si.MaxPlayers = (byte)msg.max_players;
      si.Name = msg.name;
      si.Ping = 0;
      si.Players = msg.players;
      si.ServerType = msg.dedicated ? "Dedicated" : "Listen";
      return si;
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