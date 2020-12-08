using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace QueryMaster
{
  /// <summary>
  ///   Represents the connected server.Provides methods to query,listen to server logs and control the server
  /// </summary>
  public abstract class Server : IDisposable
  {
    private readonly IPEndPoint ServerEndPoint;
    private readonly EngineType Type;
    private bool IsDisposed;
    private bool IsPlayerChallengeId;
    private bool IsRuleChallengeId;
    private long Latency;
    private Logs logs;
    private byte[] PlayerChallengeId;
    internal Rcon RConObj;
    private byte[] RuleChallengeId;
    internal UdpQuery socket;

    internal Server(IPEndPoint address, EngineType type, bool? isObsolete, int sendTimeOut, int receiveTimeOut)
    {
      ServerEndPoint = address;
      Retries = 3;
      socket = new UdpQuery(address, sendTimeOut, receiveTimeOut);
      IsDisposed = false;
      Type = type;
      if (isObsolete == null)
      {
        try
        {
          if (socket.GetResponse(QueryMsg.ObsoleteInfoQuery, Type)[0] == 0x6D)
            IsObsolete = true;
        }
        catch (SocketException e)
        {
          if (e.ErrorCode == 10060)
            IsObsolete = false;
        }
      }
      else
        IsObsolete = isObsolete == true;
    }

    /// <summary>
    /// Number or (re)tries when requests time out
    /// </summary>
    public int Retries { get; set; }

    /// <summary>
    /// On some systems the first outgoing UDP packet per socket gets dropped.
    /// Enabling this workaround will send the first outgoing UDP packet twice.
    /// </summary>
    public bool SendFirstPacketTwice { set { socket.SendFirstPacketTwice = value; } }

    /// <summary>
    ///   Returns true if server replies only to half life protocol messages.
    /// </summary>
    public bool IsObsolete { get; private set; }

    /// <summary>
    ///   Disposes all the resources used by the server object
    /// </summary>
    public void Dispose()
    {
      if (IsDisposed)
        return;
      if (socket != null)
        socket.Dispose();
      if (RConObj != null)
        RConObj.Dispose();
      if (logs != null)
        logs.Dispose();
      IsDisposed = true;
    }

    public ServerInfo GetInfo(Action<int> failedAttemptCallback = null)
    {
      return Util.RunWithRetries(() => GetInfoCore(), this.Retries, failedAttemptCallback);
    }

    /// <summary>
    ///   Retrieves information about the server
    /// </summary>
    /// <returns>Instance of ServerInfo class</returns>
    protected virtual ServerInfo GetInfoCore(byte[] s2cChallenge = null)
    {
      // in order to prevent reflection/amplification DDoS attacks, Valve decided in Dec 2020 to change the A2S_INFO protocol:
      // https://steamcommunity.com/discussions/forum/14/2974028351344359625/
      // The first request is sent without a challenge, the server may reply with the data directly (as before) or with an S2C_CHALLENGE including a 4 byte challenge.
      // In this case the client resends the A2S_INFO query with the challenge appended
      var Query = QueryMsg.InfoQuery;
      if (IsObsolete)
        Query = QueryMsg.ObsoleteInfoQuery;
      if (s2cChallenge != null) 
      {
        Query = new byte[QueryMsg.InfoQuery.Length + 4];
        Array.Copy(QueryMsg.InfoQuery, Query, QueryMsg.InfoQuery.Length);
        Array.Copy(s2cChallenge, 1, Query, QueryMsg.InfoQuery.Length, 4);
      }

      var sw = new Stopwatch();
      var recvData = socket.GetResponse(Query, Type, sw);
      Latency = sw.ElapsedMilliseconds;
      try
      {
        switch (recvData[0])
        {
          case 0x49: return DataReceived(recvData);
          case 0x6D: return Obsolete(recvData);
          case 0x41: 
            if (s2cChallenge == null) // prevent endless loop/recursion
              return ChallengeReceived(recvData);
            throw new InvalidHeaderException("A2S_INFO failed after challenge");
          default: throw new InvalidHeaderException("packet header is not valid");
        }
      }
      catch (Exception e)
      {
        e.Data.Add("ReceivedData", recvData);
        throw;
      }
    }

    private ServerInfo ChallengeReceived(byte[] data)
    {
      if (data.Length < 1 + 4)
        throw new InvalidHeaderException("S2C_CHALLENGE packet header is not valid");
      return GetInfoCore(data);
    }

    private ServerInfo DataReceived(byte[] data)
    {
      var parser = new Parser(data);
      if (parser.ReadByte() != (byte) ResponseMsgHeader.A2S_INFO)
        throw new InvalidHeaderException("A2S_INFO message header is not valid");
      var server = new ServerInfo();
      server.IsObsolete = false;
      server.Protocol = parser.ReadByte();
      server.Name = parser.ReadString();
      server.Map = parser.ReadString();
      server.Directory = parser.ReadString();
      server.Description = parser.ReadString();
      server.Id = parser.ReadShort();
      server.Players = parser.ReadByte();
      server.MaxPlayers = parser.ReadByte();
      server.Bots = parser.ReadByte();
      server.ServerType = (new Func<string>(() =>
      {
        switch ((char) parser.ReadByte())
        {
          case 'l': return "Listen";
          case 'd': return "Dedicated";
          case 'p': return "SourceTV";
        }
        return "";
      }))();
      server.Environment = (new Func<string>(() =>
      {
        switch ((char) parser.ReadByte())
        {
          case 'l': return "Linux";
          case 'w': return "Windows";
          case 'm': return "Mac";
        }
        return "";
      }))();
      server.IsPrivate = Convert.ToBoolean(parser.ReadByte());
      server.IsSecure = Convert.ToBoolean(parser.ReadByte());
      if (server.Id >= 2400 && server.Id <= 2412)
      {
        var ship = new TheShip();
        switch (parser.ReadByte())
        {
          case 0: ship.Mode = "Hunt"; break;
          case 1: ship.Mode = "Elimination"; break;
          case 2: ship.Mode = "Duel"; break;
          case 3: ship.Mode = "Deathmatch"; break;
          case 4: ship.Mode = "VIP Team"; break;
          case 5: ship.Mode = "Team Elimination"; break;
          default: ship.Mode = ""; break;
        }
        ship.Witnesses = parser.ReadByte();
        ship.Duration = parser.ReadByte();
        server.ShipInfo = ship;
      }

      server.GameVersion = parser.ReadString();
      if (parser.HasMore)
      {
        var edf = parser.ReadByte();
        var info = new ExtraInfo();
        info.Port = (edf & 0x80) > 0 ? parser.ReadShort() : (ushort)0;
        info.SteamID = (edf & 0x10) > 0 ? parser.ReadLong() : 0;
        if ((edf & 0x40) > 0)
          info.SpecInfo = new SourceTVInfo {Port = parser.ReadShort(), Name = parser.ReadString()};
        info.Keywords = (edf & 0x20) > 0 ? parser.ReadString() : string.Empty;
        info.GameId = (edf & 0x01) > 0 ? parser.ReadLong() : 0;
        server.Extra = info;
      }
      server.Address = socket.Address.Address + ":" + socket.Address.Port;
      server.Ping = Latency;
      return server;
    }

    private ServerInfo Obsolete(byte[] data)
    {
      var parser = new Parser(data);
      if (parser.ReadByte() != (byte) ResponseMsgHeader.A2S_INFO_Obsolete)
        throw new InvalidHeaderException("A2S_INFO(obsolete) message header is not valid");
      var server = new ServerInfo();
      server.IsObsolete = true;
      server.Address = parser.ReadString();
      server.Name = parser.ReadString();
      server.Map = parser.ReadString();
      server.Directory = parser.ReadString();
      server.Id = Util.GetGameId(parser.ReadString());
      server.Players = parser.ReadByte();
      server.MaxPlayers = parser.ReadByte();
      server.Protocol = parser.ReadByte();
      server.ServerType = (new Func<string>(() =>
      {
        switch ((char) parser.ReadByte())
        {
          case 'L': return "non-dedicated server";
          case 'D': return "dedicated";
          case 'P': return "HLTV server";
        }
        return "";
      }))();
      server.Environment = (new Func<string>(() =>
      {
        switch ((char) parser.ReadByte())
        {
          case 'L': return "Linux";
          case 'W': return "Windows";
        }
        return "";
      }))();
      server.IsPrivate = Convert.ToBoolean(parser.ReadByte());
      var mod = parser.ReadByte();
      server.IsModded = mod > 0;
      if (server.IsModded)
      {
        var modinfo = new Mod();
        modinfo.Link = parser.ReadString();
        modinfo.DownloadLink = parser.ReadString();
        parser.ReadByte(); //0x00
        modinfo.Version = parser.ReadInt();
        modinfo.Size = parser.ReadInt();
        modinfo.IsOnlyMultiPlayer = parser.ReadByte() > 0;
        modinfo.IsHalfLifeDll = parser.ReadByte() == 0;
        server.ModInfo = modinfo;
      }
      server.IsSecure = Convert.ToBoolean(parser.ReadByte());
      server.Bots = parser.ReadByte();
      server.GameVersion = "server is obsolete,does not provide this information";
      server.Ping = Latency;
      return server;
    }

    /// <summary>
    ///   Retrieves information about the players currently on the server
    /// </summary>
    /// <returns>ReadOnlyCollection of Player instances</returns>
    public virtual ReadOnlyCollection<Player> GetPlayers(Action<int> failedAttemptCallback = null, Stopwatch sw = null)
    {
      return Util.RunWithRetries(() => GetPlayersCore(sw), this.Retries, failedAttemptCallback);
    }

    protected virtual ReadOnlyCollection<Player> GetPlayersCore(Stopwatch sw = null)
    {
      byte[] recvData = null;

      if (IsObsolete)
      {
        recvData = socket.GetResponse(QueryMsg.ObsoletePlayerQuery, Type);
      }
      else
      {
        if (PlayerChallengeId == null)
        {
          recvData = GetPlayerChallengeId(sw);
          if (IsPlayerChallengeId)
            PlayerChallengeId = recvData;
        }
        if (IsPlayerChallengeId)
          recvData = socket.GetResponse(Util.MergeByteArrays(QueryMsg.PlayerQuery, PlayerChallengeId), Type);
      }
      try
      {
        var parser = new Parser(recvData);
        if (parser.ReadByte() != (byte) ResponseMsgHeader.A2S_PLAYER)
          throw new InvalidHeaderException("A2S_PLAYER message header is not valid");
        int playerCount = parser.ReadByte();
        var players = new List<Player>();
        for (var i = 0; i < playerCount && parser.HasMore; i++)
        {
          parser.ReadByte(); //index,always equal to 0
          players.Add(new Player
          {
            Name = parser.ReadString(),
            Score = parser.ReadInt(),
            Time = TimeSpan.FromSeconds(parser.ReadFloat())
          });
        }
        //playerCount = players.Count; // some servers report more players than it really returns
        //if (playerCount == 1 && players[0].Name == "Max Players")
        //  return null;
        return players.AsReadOnly();
      }
      catch (Exception e)
      {
        e.Data.Add("ReceivedData", recvData);
        throw;
      }
    }

    /// <summary>
    ///   Retrieves  server rules
    /// </summary>
    /// <returns>ReadOnlyCollection of Rule instances</returns>
    public ReadOnlyCollection<Rule> GetRules(Action<int> failedAttemptCallback = null)
    {
      return Util.RunWithRetries(GetRulesCore, this.Retries, failedAttemptCallback);
    }

    protected virtual ReadOnlyCollection<Rule> GetRulesCore()
    {
      byte[] recvData = null;
      if (IsObsolete)
      {
        recvData = socket.GetResponse(QueryMsg.ObsoleteRuleQuery, Type);
      }
      else
      {
        if (RuleChallengeId == null)
        {
          recvData = GetRuleChallengeId();
          if (IsRuleChallengeId)
            RuleChallengeId = recvData;
        }
        if (IsRuleChallengeId)
          recvData = socket.GetResponse(Util.MergeByteArrays(QueryMsg.RuleQuery, RuleChallengeId), Type);
      }
      try
      {
        var parser = new Parser(recvData);
        if (parser.ReadByte() != (byte) ResponseMsgHeader.A2S_RULES)
          throw new InvalidHeaderException("A2S_RULES message header is not valid");

        int count = parser.ReadShort(); //number of rules
        var rules = new List<Rule>(count);
        for (var i = 0; i < count; i++)
        {
          rules.Add(new Rule {Name = parser.ReadString(), Value = parser.ReadString()});
        }
        return rules.AsReadOnly();
      }
      catch (Exception e)
      {
        e.Data.Add("ReceivedData", recvData);
        throw;
      }
    }

    private byte[] GetPlayerChallengeId(Stopwatch sw = null)
    {
      var recvBytes = socket.GetResponse(QueryMsg.PlayerChallengeQuery, Type, sw);
      try
      {
        var parser = new Parser(recvBytes);
        var header = parser.ReadByte();
        switch (header)
        {
          case (byte) ResponseMsgHeader.A2S_SERVERQUERY_GETCHALLENGE:
            IsPlayerChallengeId = true;
            return parser.GetUnParsedData();
          case (byte) ResponseMsgHeader.A2S_PLAYER:
            IsPlayerChallengeId = false;
            return recvBytes;
          default:
            throw new InvalidHeaderException("A2S_SERVERQUERY_GETCHALLENGE message header is not valid");
        }
      }
      catch (Exception e)
      {
        e.Data.Add("ReceivedData", recvBytes);
        throw;
      }
    }

    private byte[] GetRuleChallengeId()
    {
      var recvBytes = socket.GetResponse(QueryMsg.RuleChallengeQuery, Type);
      try
      {
        var parser = new Parser(recvBytes);
        var header = parser.ReadByte();

        switch (header)
        {
          case (byte) ResponseMsgHeader.A2S_SERVERQUERY_GETCHALLENGE:
            IsRuleChallengeId = true;
            return BitConverter.GetBytes(parser.ReadInt());
          case (byte) ResponseMsgHeader.A2S_RULES:
            IsRuleChallengeId = false;
            return recvBytes;
          default:
            throw new InvalidHeaderException("A2S_SERVERQUERY_GETCHALLENGE message header is not valid");
        }
      }
      catch (Exception e)
      {
        e.Data.Add("ReceivedData", recvBytes);
        throw;
      }
    }

    /// <summary>
    ///   Listen to server logs.
    /// </summary>
    /// <param name="port">Local port</param>
    /// <returns>Instance of Log class</returns>
    /// <remarks>Receiver's socket address must be added to server's logaddress list before listening</remarks>
    public Logs GetLogs(int port)
    {
      logs = new Logs(Type, port, ServerEndPoint);
      return logs;
    }

    /// <summary>
    ///   Gets valid rcon object that can be used to send rcon commands to server
    /// </summary>
    /// <param name="pass">Rcon password of server</param>
    /// <returns>Instance of Rcon class</returns>
    public abstract Rcon GetControl(string pass);

    /// <summary>
    ///   Gets Round-trip delay time
    /// </summary>
    /// <returns>Elapsed milliseconds</returns>
    public long Ping()
    {
      Stopwatch sw;
      if (IsObsolete)
      {
        sw = Stopwatch.StartNew();
        socket.GetResponse(QueryMsg.ObsoletePingQuery, Type);
        sw.Stop();
      }
      else
      {
        sw = Stopwatch.StartNew();
        socket.GetResponse(QueryMsg.InfoQuery, Type);
        sw.Stop();
      }
      return sw.ElapsedMilliseconds;
    }
  }
}