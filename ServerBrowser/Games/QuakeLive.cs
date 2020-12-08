using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting.Native;
using QueryMaster;
using ServerBrowser.Games;

#if ZMQ
using System.Net;
using ZeroMQ;
using ZeroMQ.Monitoring;
#endif

namespace ServerBrowser
{
  public class QuakeLive : GameExtension
  {
    private const int SecondsToWaitForMainWindowAfterLaunch = 20;

    private static readonly Dictionary<int, string> gameTypeName = new Dictionary<int, string>
    {
      {0, "FFA"},
      {1, "Duel"},
      {2, "Race"},
      {3, "TDM"},
      {4, "CA"},
      {5, "CTF"},
      {6, "1Flag"},
      {8, "Harv"},
      {9, "FT"},
      {10, "Dom"},
      {11, "A&D"},
      {12, "RR"}
    };

    private static readonly string[] TeamNames = { null, "Play", "Red", "Blue", "Spec" };

    private static readonly Regex NameColors = new Regex("\\^[0-9]");
    private readonly Game steamAppId;
    private bool useKeystrokesToConnect;
    private bool startExtraQL;
    private string extraQlPath;
    private static readonly DataContractJsonSerializer serverSkillJsonParser = new DataContractJsonSerializer(typeof(QlStatsSkillInfo[]));
    private static readonly DataContractJsonSerializer personalSkillJsonParser = new DataContractJsonSerializer(typeof(QlstatsGlickoRating));
    private static readonly DataContractJsonSerializer playerListJsonParser = new DataContractJsonSerializer(typeof(QlstatsPlayerList));
    private Dictionary<string,QlStatsSkillInfo> skillInfo = new Dictionary<string, QlStatsSkillInfo>();
    private GridColumn colSkill;
    private const string SkillTooltip = "QLStats.net skill rating max/avg/min (*100)";
    private readonly Dictionary<ServerRow, QlstatsPlayerList> qlstatsPlayerlists = new Dictionary<ServerRow, QlstatsPlayerList>();

    #region ctor()

    public QuakeLive(Game steamAppId)
    {
      this.steamAppId = steamAppId;
      this.BotsIncludedInPlayerCount = true;
      this.BotsIncludedInPlayerList = true;
      this.OptionMenuCaption = "Quake Live...";
    }

    #endregion

    #region LoadConfig()
    public override void LoadConfig(IniFile ini)
    {
      var sec = ini.GetSection("QuakeLive", true);
      this.useKeystrokesToConnect = sec.GetBool("useKeystrokesToConnect");
      this.startExtraQL = sec.GetBool("startExtraQL");
      this.extraQlPath = sec.GetString("extraQlPath");
    }
    #endregion

    #region SaveConfig()
    public override void SaveConfig(StringBuilder ini)
    {
      ini.AppendLine();
      ini.AppendLine("[QuakeLive]");
      ini.AppendLine($"useKeystrokesToConnect={this.useKeystrokesToConnect}");
      ini.AppendLine($"startExtraQL={this.startExtraQL}");
      ini.AppendLine($"extraQlPath={this.extraQlPath}");
    }
    #endregion

    #region OnOptionMenuClick()
    public override void OnOptionMenuClick()
    {
      using (var dlg = new QuakeLiveOptionsDialog())
      {
        dlg.UseKeystrokes = this.useKeystrokesToConnect;
        dlg.StartExtraQL = this.startExtraQL;
        if (dlg.ShowDialog(Form.ActiveForm) != DialogResult.OK)
          return;
        this.useKeystrokesToConnect = dlg.UseKeystrokes;
        this.startExtraQL = dlg.StartExtraQL;
      }
    }
    #endregion

    #region CustomizeServerGridColumns

    public override void CustomizeServerGridColumns(GridView view)
    {
      var colDescription = view.Columns["ServerInfo.Description"];
      var idx = colDescription.VisibleIndex;
      colDescription.Visible = false;
      AddColumn(view, "g_factoryTitle", "Factory", "g_factoryTitle", 100, idx)
        .OptionsFilter.AutoFilterCondition = AutoFilterCondition.Default;

      AddColumn(view, "_gametype", "GT", "Gametype", 40, idx)
        .OptionsFilter.AutoFilterCondition = AutoFilterCondition.Default;

      idx = view.Columns["PlayerCount"].VisibleIndex;
      this.colSkill = AddColumn(view, "_skill", "Skill", SkillTooltip, 60, ++idx, UnboundColumnType.Integer);
      AddColumn(view, "_score", "Score", "Current score", 50, ++idx, UnboundColumnType.Integer);
      //AddColumn(view, "_time", "Time", "Match time", 30, ++idx, UnboundColumnType.Integer);
      AddColumn(view, "_teamsize", "TS", "Team Size", 30, ++idx, UnboundColumnType.Integer);

      idx = view.Columns["ServerInfo.Ping"].VisibleIndex;
      AddColumn(view, "_goalscore", "SL", "Score Limit", 30, idx, UnboundColumnType.Integer);
      AddColumn(view, "timelimit", "TL", "Time Limit", 30, ++idx, UnboundColumnType.Integer);
      //AddColumn(view, "g_instaGib", "Insta", "Instagib", 35, ++idx, UnboundColumnType.Boolean);

      var col = AddColumn(view, "g_loadout", "Lo", "Loadout", 20, ++idx, UnboundColumnType.Boolean);
      var ed = view.GridControl.RepositoryItems["riLoadout"] as RepositoryItemImageComboBox;
      if (ed == null)
      {
        ed = new RepositoryItemImageComboBox();
        ed.BeginInit();
        ed.Name = "riLoadout";
        ed.SmallImages = view.Images;
        ed.Items.Add(new ImageComboBoxItem("No Loadouts", false, -1));
        ed.Items.Add(new ImageComboBoxItem("Loadouts", true, 28));
        ed.EndInit();
        view.GridControl.RepositoryItems.Add(ed);
      }
      col.ColumnEdit = ed;

      col = AddColumn(view, "g_itemTimers", "Ti", "Item Timers", 20, ++idx, UnboundColumnType.Boolean);
      ed = view.GridControl.RepositoryItems["riItemTimer"] as RepositoryItemImageComboBox;
      if (ed == null)
      {
        ed = new RepositoryItemImageComboBox();
        ed.BeginInit();
        ed.Name = "riItemTimer";
        ed.SmallImages = view.Images;
        ed.Items.Add(new ImageComboBoxItem("No Item Timers", false, -1));
        ed.Items.Add(new ImageComboBoxItem("Item Timers", true, 26));
        ed.EndInit();
        view.GridControl.RepositoryItems.Add(ed);
      }
      col.ColumnEdit = ed;
    }

    #endregion

    #region CustomizePlayerGridColumns()
    public override void CustomizePlayerGridColumns(GridView view)
    {
      base.CustomizePlayerGridColumns(view);

      AddColumn(view, "_team", "Team", "", 30, 0);
      AddColumn(view, "_skill", "Skill", SkillTooltip, 30, 2, UnboundColumnType.Integer);
      view.Columns["Time"].Visible = false;
      AddColumn(view, "_time", "Time", "Play time since map start", 50);
    }
    #endregion

    #region Refresh()
    public override void Refresh(ServerRow row = null, Action callback = null)
    {
      LoadQlstatsPersonalRating();
      LoadQlstatsServerRatings();
      if (row == null)
        this.qlstatsPlayerlists.Clear();
      else
        this.LoadQlstatsPlayerList(row, callback);
    }
    #endregion

    #region LoadQlstatsPersonalRating()
    private void LoadQlstatsPersonalRating()
    {
      if (this.Steamworks == null) return;
      var steamid = this.Steamworks.GetUserID();
      if (steamid == 0) return;
      using (var client = new XWebClient(2000))
      {
        client.DownloadStringCompleted += (sender, args) =>
        {
          try
          {
            if (args.Error != null)
              return;
            using (var strm = new MemoryStream(Encoding.UTF8.GetBytes(args.Result)))
            {
              var result = (QlstatsGlickoRating)personalSkillJsonParser.ReadObject(strm);
              var text = "\n\nYour personal rating: estimate ± uncertainty:";
              foreach (var player in result.players)
              {
                var gametypes = new[] { "ffa", "ca", "duel", "ctf", "tdm", "ft" };
                var ratings = new[] { player.ffa, player.ca, player.duel, player.ctf, player.tdm, player.ft };
                for (int i = 0; i < gametypes.Length; i++)
                {
                  var rating = ratings[i];
                  if (rating == null) continue;
                  text += $"\n{gametypes[i].ToUpper()}: {rating.r} ± {rating.rd} ({rating.games} games)";
                }
                if (gametypes.Length == 0)
                  text = "";
              }

              if (this.colSkill?.View?.GridControl == null)
                return;
              this.colSkill.View.GridControl.BeginInvoke((Action) (() =>
              {
                this.colSkill.ToolTip = SkillTooltip + text;
              }));
            }
          }
          catch
          {
          }
        };
        client.DownloadStringAsync(new Uri("http://qlstats.net/glicko/" + steamid));
      }
    }

    #endregion

    #region LoadQlstatsServerRatings()
    private void LoadQlstatsServerRatings()
    {
      using (var client = new XWebClient(2000))
      {
        client.DownloadStringCompleted += (sender, args) =>
        {
          try
          {
            if (args.Error != null)
              return;
            using (var strm = new MemoryStream(Encoding.UTF8.GetBytes(args.Result)))
            {
              var servers = (QlStatsSkillInfo[])serverSkillJsonParser.ReadObject(strm);
              var dict = new Dictionary<string, QlStatsSkillInfo>();
              foreach (var server in servers)
                dict[server.server] = server;
              this.skillInfo = dict;

              var view = this.colSkill?.View;
              var grid = view?.GridControl;
              if (grid == null)
                return;
              grid.BeginInvoke((Action) (() =>
              {
                for (int i = 0, c = view.RowCount; i < c; i++)
                {
                  var row = (ServerRow) view.GetRow(i);
                  var info = dict.GetValueOrDefault(row.EndPoint.ToString(), null);
                  if (info != null)
                    row.PlayerCount.Update();
                }
              }));
            }
          }
          catch
          {
          }
        };
        client.DownloadStringAsync(new Uri("http://api.qlstats.net/api/server/skillrating"));
      }
    }
    #endregion

    #region LoadQlstatsPlayerList()
    private void LoadQlstatsPlayerList(ServerRow row, Action callback = null)
    {
      this.qlstatsPlayerlists[row] = null;
      using (var client = new XWebClient(2000))
      {
        client.Encoding = Encoding.UTF8;
        
        client.DownloadStringCompleted += (sender, args) =>
        {
          try
          {
            if (args.Error == null)
            {
              using (var strm = new MemoryStream(Encoding.UTF8.GetBytes(args.Result)))
              {
                var playerList = (QlstatsPlayerList) playerListJsonParser.ReadObject(strm);
                this.qlstatsPlayerlists[row] = playerList;
                if (playerList.serverinfo != null)
                  this.skillInfo[row.EndPoint.ToString()] = playerList.serverinfo;
              }
              callback?.Invoke();
            }
          }
          catch
          {
          }
        };

        client.DownloadStringAsync(new Uri("http://api.qlstats.net/api/server/" + row.EndPoint + "/players"));
      }
    }
    #endregion


    #region GetServerCellValue()

    public override object GetServerCellValue(ServerRow row, string fieldName)
    {
      switch (fieldName)
      {
        case "g_factoryTitle":
        {
          var factory = row.GetRule("g_factoryTitle");
          return string.IsNullOrEmpty(factory) ? row.ServerInfo?.Description : factory;
        }
        case "_goalscore":
        {
          var gt = row.GetRule("g_gametype");
          if (gt == "1" || gt == "2") //  DUEL, RACE
            return null;
          if (gt == "4" || gt == "9" || gt == "12") // CA, FT, RR
            return row.GetRule("roundlimit");
          if (gt == "5" || gt == "6") // CTF, 1FLAG
            return row.GetRule("capturelimit");
          if (gt == "8" || gt == "10" || gt == "11") // HAR, DOM, A&D
            return row.GetRule("scorelimit");
          return row.GetRule("fraglimit"); // 0=FFA, 3=TDM
        }
        case "_gametype":
        {
          var gt = row.GetRule("g_gametype");
          var instaPrefix = row.GetRule("g_instaGib") == "1" ? "i" : "";
          int num;
          string name;
          if (int.TryParse(gt, out num) && gameTypeName.TryGetValue(num, out name))
            return instaPrefix + name;
          return instaPrefix + gt;
        }
        case "_teamsize":
        {
          int ts;
          int.TryParse(row.GetRule("teamsize") ?? "0", out ts);
          return ts == 0 ? null : (object) ts;
        }
        case "_skill":
        {
          QlStatsSkillInfo skill;
          if (!this.skillInfo.TryGetValue(row.ServerInfo.Address, out skill))
            return null;
          return "" + (skill.max + 50)/100 + "/" + (skill.avg + 50)/100 + "/" + (skill.min + 50)/100;
        }
        case "_score":
        {
          var state = row.GetRule("g_gameState");
          if (state == "PRE_GAME")
            return null;
          var red = row.GetRule("g_redScore");
          var blue = row.GetRule("g_blueScore");
          int ired, iblue;
          if (int.TryParse(red, out ired) && int.TryParse(blue, out iblue) && iblue > ired)
            return "" + iblue + ":" + ired;
          return "" + red + ":" + blue;
        }
        case "_time":
        {
          var time = row.GetRule("g_levelStartTime");
          if (time == null) return null;
          int itime;
          if (!int.TryParse(time, out itime)) return null;
          var dt = new DateTime(1970, 1, 1).AddSeconds(itime);
          var span = DateTime.UtcNow - dt;
          var str = span.Minutes.ToString("d2") + ":" + span.Seconds.ToString("d2");
          if (span.TotalHours >= 1)
            str = span.Hours.ToString("d") + "h " + str;
          if (span.TotalDays >= 1)
            str = ((int)span.TotalDays) + "d " + str;
          return str;
        }
        case "g_instaGib":
          return row.GetRule(fieldName) == "1";
        case "g_loadout":
          return row.GetRule(fieldName) == "1";
        case "g_itemTimers":
          return row.GetRule(fieldName) == "1";
      }
      return base.GetServerCellValue(row, fieldName);
    }

    #endregion


    #region IsValidPlayer()
    public override bool IsValidPlayer(ServerRow server, Player player)
    {
      var list = qlstatsPlayerlists.GetValueOrDefault(server, null);
      if (list?.players != null)
      {
        var cleanName = this.GetCleanPlayerName(player.Name, true);
        return list.players.Count(p => this.GetCleanPlayerName(p.name) == cleanName) > 0;
      }

      // minqlx bots
      if (player.Name.StartsWith("(Bot)"))
        return false;

      // hack to remove ghost players which are not really on the server
      var status = server.GetRule("g_gameStatus");
      return status != "IN_PROGRESS" || player.Score > 0 || player.Time < TimeSpan.FromHours(1);
    }
    #endregion

    #region GetRealPlayerCount()
    public override int? GetRealPlayerCount(ServerRow row)
    {
      var info = this.skillInfo.GetValueOrDefault(row.EndPoint.ToString(), null);
      if (info != null)
        return info.pc + info.sc;

      // minqlx bots are not correctly counted as bots in the server info
      if (row.Players != null)
      {
        var c = row.Players.Count(p => p.Name.StartsWith("(Bot)"));
        if (c > 0)
          return row.Players.Count - c;
      }

      return base.GetRealPlayerCount(row);
    }
    #endregion

    #region GetSpectatorCount()
    public override int GetSpectatorCount(ServerRow row)
    {
      var info = this.skillInfo.GetValueOrDefault(row.EndPoint.ToString(), null);
      if (info != null)
        return info.sc;
      return 0;
    }
    #endregion

    #region GetBotCount()
    public override int? GetBotCount(ServerRow row)
    {
      var info = this.skillInfo.GetValueOrDefault(row.EndPoint.ToString(), null);
      if (info != null)
        return info.bc;

      // minqlx bots are not correctly counted as bots in the server info
      if (row.Players != null)
      {
        var c = row.Players.Count(p => p.Name.StartsWith("(Bot)"));
        if (c > 0)
          return c;
      }

      return base.GetRealPlayerCount(row);
    }
    #endregion

    #region GetMaxClients()
    public override int? GetMaxClients(ServerRow row)
    {
      var cli = row.GetRule("sv_maxclients");
      int val;
      if (!string.IsNullOrEmpty(cli) && int.TryParse(cli, out val))
        return val;

      // QL currently returns the sv_maxclients in MaxPlayers (instead of teamsize * 2)
      return row.ServerInfo?.MaxPlayers;
    }
    #endregion

    #region GetMaxPlayers()
    public override int? GetMaxPlayers(ServerRow row)
    {
      var gt = row.GetRule("g_gametype");
      if (gt == "1") // Duel
        return 2;

      var ts = row.GetRule("teamsize");
      if (!string.IsNullOrEmpty(ts))
      {
        int n;
        int.TryParse(ts, out n);
        if (n != 0)
          return gt == "0" || gt == "2" ? n : n*2; // FFA, Race
      }

      // QL currently returns the sv_maxclients in MaxPlayers (instead of teamsize * 2)
      return row.ServerInfo?.MaxPlayers;
    }
    #endregion

    #region GetPrivateClients()
    public override int? GetPrivateClients(ServerRow row)
    {
      var cli = row.GetRule("sv_privateclients");
      int val;
      if (!string.IsNullOrEmpty(cli) && int.TryParse(cli, out val))
        return val;

      return 0;
    }
    #endregion


    #region GetPlayerCellValue()
    public override object GetPlayerCellValue(ServerRow server, Player player, string fieldName)
    {
      if (fieldName[0] != '_')
        return base.GetPlayerCellValue(server, player, fieldName);

      QlstatsPlayerList list;
      QlstatsPlayerList.Player playerInfo = null;
      this.qlstatsPlayerlists.TryGetValue(server, out list);
      if (list?.players != null)
      {
        var cleanName = this.GetCleanPlayerName(player.Name, true);
        playerInfo = list.players.FirstOrDefault(p => this.GetCleanPlayerName(p.name) == cleanName);
      }
      if (playerInfo == null)
      {
        if (fieldName == "_time")
          return player.Time.ToString("hh\\:mm\\:ss");
        return null;
      }

      if (fieldName == "_team")
        return TeamNames[playerInfo.team + 1];
      if (fieldName == "_skill")
        return playerInfo.rating; //(playerInfo.rating+50)/100;
      if (fieldName == "_time")
        return (DateTime.UtcNow - new DateTime(1970, 1, 1).AddMilliseconds(playerInfo.time)).ToString("hh\\:mm\\:ss");

      return null;
    }
    #endregion

    #region GetCleanPlayerName()
    public override string GetCleanPlayerName(Player player)
    {
      return this.GetCleanPlayerName(player.Name);
    }

    private string GetCleanPlayerName(string name, bool removeNonZmqChars = false)
    {
      if (name == null) return null;
      name = NameColors.Replace(name, "");
      if (removeNonZmqChars)
      {
        foreach (var ch in "'\"<>") // some special characters which QL returns in the server query but strips out of ZMQ names
          name = name.Replace(ch.ToString(), "");
        name = new Regex(@"\s{2,}").Replace(name, " "); // ZMQ names seem to replace 2 spaces with 1 space, but 5 spaces still leaves 3
      }
      return name;
    }
    #endregion

    #region CustomizePlayerContextMenu()
    public override void CustomizePlayerContextMenu(ServerRow server, Player player, List<PlayerContextMenuItem> menu)
    {
      QlstatsPlayerList list;
      if (!this.qlstatsPlayerlists.TryGetValue(server, out list) || list.players == null)
        return;
      var cleanName = this.GetCleanPlayerName(player);
      var info = list.players.FirstOrDefault(p => this.GetCleanPlayerName(p.name) == cleanName);
      if (info == null)
        return;

      menu.Insert(0, new PlayerContextMenuItem("Open Steam Chat", () => { Process.Start("steam://friends/message/" + info.steamid); }, true));
      menu.Insert(1, new PlayerContextMenuItem("Show Steam Profile", () => { Process.Start("http://steamcommunity.com/profiles/" + info.steamid + "/"); }));
      menu.Insert(2, new PlayerContextMenuItem("Show QLStats Profile", () => { Process.Start("http://qlstats.net/player/" + info.steamid); }));
      menu.Insert(3, new PlayerContextMenuItem("Add to Steam Friends", () => { Process.Start("steam://friends/add/" + info.steamid); }));
      menu.Add(new PlayerContextMenuItem("Copy Steam-ID to Clipboard", () => { Clipboard.SetText(info.steamid); }));
    }
    #endregion


    #region Connect()

    public override bool Connect(ServerRow server, string password, bool spectate)
    {
      if (this.startExtraQL)
      {
        var extraQlExe = this.GetExtaQlPath();
        if (extraQlExe != null)
          Process.Start(extraQlExe, "-background");
      }

      if (this.useKeystrokesToConnect)
      {
        // don't use the ThreadPool b/c it might be full with waiting server update requests
        new Thread(ctx => { ConnectInBackground(server, password, spectate); }).Start();       
        return true;
      }

      return base.Connect(server, password, spectate);
    }

    #endregion

    #region GetExtraQlPath()
    private string GetExtaQlPath()
    {
      if (!string.IsNullOrEmpty(this.extraQlPath) && File.Exists(this.extraQlPath))
        return this.extraQlPath;
      var extraQlExe = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(this.GetType().Assembly.Location)), @"539252269\extraQL.exe");
      return File.Exists(extraQlExe) ? extraQlExe : null;
    }
    #endregion

    #region ConnectInBackground()

    private void ConnectInBackground(ServerRow server, string password, bool spectate)
    {
      var hWnd = FindGameWindow();
      if (hWnd == IntPtr.Zero)
      {
        hWnd = StartQuakeLive();
        if (hWnd == IntPtr.Zero)
          return;
        SkipIntro(hWnd);
      }

      // console key
      Win32.PostMessage(hWnd, Win32.WM_KEYDOWN, 0, 0x29 << 16);
      Win32.PostMessage(hWnd, Win32.WM_KEYUP, 0, 0x29 << 16);

      Thread.Sleep(100);

      var msg = "/";
      if (!string.IsNullOrEmpty(password))
        msg += "set password \"" + password + "\";";
      msg += "connect " + server.EndPoint.Address + ":" + server.ServerInfo.Extra.Port;
      foreach (var c in msg)
      {
        Win32.PostMessage(hWnd, Win32.WM_CHAR, c, 0);
        Thread.Sleep(10);
      }

      Win32.PostMessage(hWnd, Win32.WM_KEYDOWN, (int) Keys.Return, 0x1C << 16);
      Win32.PostMessage(hWnd, Win32.WM_KEYUP, (int) Keys.Return, 0x1C << 16);

      this.ActivateGameWindow(hWnd);
    }

    #endregion

    #region FindGameWindow()

    protected override IntPtr FindGameWindow()
    {
      foreach (var proc in Process.GetProcessesByName("quakelive_steam"))
      {
        // ignore zombie processes
        if (proc.Threads.Count == 0)
          continue;

        var hWnd = proc.MainWindowHandle;
        return hWnd;
      }
      return IntPtr.Zero;
    }

    #endregion

    #region StartQuakeLive()

    private IntPtr StartQuakeLive()
    {
      Process.Start("steam://rungameid/" + (int) steamAppId);
      for (var i = 0; i < SecondsToWaitForMainWindowAfterLaunch; i++)
      {
        Thread.Sleep(1000);
        var hWnd = FindGameWindow();
        if (hWnd != IntPtr.Zero)
          return hWnd;
      }
      return IntPtr.Zero;
    }

    #endregion

    #region SkipIntro()

    private void SkipIntro(IntPtr win)
    {
      for (var i = 0; i < 3; i++)
      {
        Thread.Sleep(500);
        Win32.PostMessage(win, Win32.WM_LBUTTONDOWN, 1, 0);
        Win32.PostMessage(win, Win32.WM_LBUTTONUP, 0, 0);
      }
    }

    #endregion



    #region class QlStatsSkillInfo
    public class QlStatsSkillInfo
    {
      public string server; // ip:port
      public string gt; // game type
      public int min; // glicko rating
      public int avg;
      public int max;
      public int pc; // player count
      public int sc; // spectator count
      public int bc; // bot count
    }
    #endregion

    #region class QlstatsGlickoRating

    public class QlstatsGlickoRating
    {
      public class GametypeRating
      {
        public int games;
        public int r;
        public int rd;
        public int r_rd;
      }
      public class Players
      {
        public string steamid;
        public GametypeRating ffa, ca, duel, ctf, tdm, ft;
      }

      public Players[] players;
    }
    #endregion

    #region class QlstatsPlayerList
    public class QlstatsPlayerList
    {
      public class Player
      {
        public string steamid;
        public string name;
        public int team;
        public long time;
        public int rating;
      }

      public bool ok;
      public Player[] players;
      public QlStatsSkillInfo serverinfo;
    }
    #endregion


    // ZeroMQ rcon stuff
#if ZMQ
    #region Rcon()

    public override void Rcon(ServerRow row, int port, string password, string command)
    {
      using (var ctx = ZContext.Create())
      {
        ZSocket client, monitor;
        var endpoint = new IPEndPoint(row.EndPoint.Address, port);
        this.CreateClientAndMonitorSockets(ctx, endpoint, password, out client, out monitor);
        using (client)
        using (monitor)
        {
          while (true)
          {
            ZMessage msg = new ZMessage();
            ZError err;
            var poll = ZPollItem.CreateReceiver();
            var ev = client.Poll(poll, ZPoll.In | ZPoll.Out | ZPoll.Err, ref msg, out err, TimeSpan.FromMilliseconds(500));
            if (err == ZError.ETERM)
              break;

            var evMonitor = CheckMonitor(monitor);
            if (evMonitor != null)
            {
              if (evMonitor.Item1 == ZMonitorEvents.Connected)
              {
                client.Send(new ZFrame("register"));
                client.Send(new ZFrame(command));
                return;
              }
              if (evMonitor.Item1 == ZMonitorEvents.Closed || evMonitor.Item1 == ZMonitorEvents.Disconnected)
                return;
            }

            if (!ev)
              continue;

            while (true)
            {
              client.ReceiveMessage(ref msg, ZSocketFlags.DontWait, out err);
              if (err != ZError.None)
              {
                if (err != ZError.EAGAIN)
                  Console.WriteLine(err);
                break;
              }
              Console.WriteLine(msg);
            }
          }
        }
      }
    }
    #endregion

    #region CreateClientAndMonitorSockets()
    private void CreateClientAndMonitorSockets(ZContext ctx, IPEndPoint endPoint, string password, out ZSocket client, out ZSocket monitor)
    {
      client = new ZSocket(ctx, ZSocketType.DEALER);
      monitor = new ZSocket(ctx, ZSocketType.PAIR);
      client.Monitor("inproc://monitor-client", ZMonitorEvents.AllEvents);
      monitor.Connect("inproc://monitor-client");

      if (!string.IsNullOrEmpty(password))
      {
        client.PlainUserName = "rcon";
        client.PlainPassword = password;
        client.ZAPDomain = "rcon";
      }

      var ident = new Guid().ToByteArray();
      client.Identity = ident;
      client.SetOption(ZSocketOption.IDENTITY, ident);
      client.Connect("tcp://" + endPoint);      
    }
    #endregion

    #region CheckMonitor()
    private Tuple<ZMonitorEvents,object> CheckMonitor(ZSocket monitor)
    {
      try
      {
        ZMessage msg = new ZMessage();
        ZError err;
        monitor.ReceiveMessage(ref msg, ZSocketFlags.DontWait, out err);
        if (err == ZError.EAGAIN)
          return null;

        var id = msg[0].ReadUInt16();
        var val = msg[0].ReadUInt32();

        var data = new byte[msg[1].Length];
        msg[1].Read(data, 0, data.Length);

        return new Tuple<ZMonitorEvents, object>((ZMonitorEvents)id, val);
      }
      catch (ZException)
      {
        return null;
      }
    }
    #endregion
#endif
  }
}