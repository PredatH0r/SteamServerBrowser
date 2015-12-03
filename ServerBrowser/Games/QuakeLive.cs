using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
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

    private static readonly Regex NameColors = new Regex("\\^[0-9]");
    private readonly Game steamAppId;
    private bool useKeystrokesToConnect;
    private bool startExtraQL;
    private string extraQlPath;

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

    #region GetCleanPlayerName()
    public override string GetCleanPlayerName(Player player)
    {
      return NameColors.Replace(player.Name, "");
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