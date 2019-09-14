using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace QueryMaster
{
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

  /// <summary>
  ///   Provides methods to query master server.
  ///   An instance can only be used for a single request and is automatically disposed when the request completes or times out
  /// </summary>
  public class MasterServerWebApi : MasterServer
  {
    const string SteamWebApiKey = "B7D245299F6F990504A86FF91EC9D6BD"; // create an account and get a steam web api key at http://steamcommunity.com/dev/apikey

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
        try
        {
          using (var cli = new XWebClient())
          {
            var filters = MasterUtil.ProcessFilter(filter);
            var url = $"https://api.steampowered.com/IGameServersService/GetServerList/v1/?key={SteamWebApiKey}&format=xml&filter={filters}&limit={GetAddressesLimit}";
            var xml = cli.DownloadString(url);
            var ser = new XmlSerializer(typeof (Response));

            // replace invalid XML chars ( < 32 ) with char reference
            var sb = new StringBuilder(xml);
            for (int i = 0, c = xml.Length; i < c; i++)
            {
              if (sb[i] < 32 && !char.IsWhiteSpace(sb[i]))
                //{
                //  sb.Insert(i+1, "#" + ((int)sb[i]).ToString() + ";");
                //  sb[i] = '&';
                //}
                sb[i] = ' ';
            }
            xml = sb.ToString();

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
            callback(new ReadOnlyCollection<Tuple<IPEndPoint,ServerInfo>>(endpoints), null, false);
          }
        }
        catch(Exception ex)
        {
          callback(null, ex, false);
        }
      });
    }

    private ServerInfo ConvertToServerInfo(Message msg)
    {
      var si = new ServerInfo();
      si.Address = msg.addr;
      si.Bots = (byte)msg.bots;
      si.Description = msg.gametype; // description has no exact match in the XML; msg.product has no match in A2S_INFO
      si.Directory = msg.gamedir;
      si.Environment = msg.os == "w" ? "Windows" : msg.os == "l" ? "Linux" : msg.os;
      si.Extra.GameId = msg.appid;
      si.Extra.Keywords = msg.gametype;
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
  }
}