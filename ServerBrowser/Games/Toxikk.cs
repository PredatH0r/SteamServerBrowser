using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using QueryMaster;
using ServerBrowser.Games;

namespace ServerBrowser
{
  // TODO: implement steam://rungameid/324810//+connect%20138.201.84.93:7777
  // then find window with title "Allow game launch?", Class USurface_801274921, Style 960F0000, Rect 420x220
  // next window "Steam", prev window "Steam", parent "SteamSteam", owner "SteamSteamSteam"

  public class Toxikk : GameExtension
  {
    private const string MinCombatants = "p268435703";
    private const string ScoreLimit = "p268435704";
    private const string TimeLimit = "p268435705";
    private const string Mutators = "p1073741828";
    private const string OldOfficialNewRanked = "s15";
    private const string NewOfficial = "s18";
    private const string ModdingLevel = "s19";
    private const string GameVersion = "p1073741839";

    private const int SecondsToWaitForMainWindowAfterLaunch = 45;
    private bool useKeystrokesToConnect;
    private bool useSteamIdToConnect;
    private Keys consoleKey;
    private int skillClassFilter;
    private ServerRow serverForPlayerInfos;
    private long dataTimestamp;
    private volatile Dictionary<string,ToxikkPlayerInfo> playerInfos = new Dictionary<string, ToxikkPlayerInfo>();


    public Toxikk()
    {
      this.supportsPlayersQuery = true;
      this.supportsRulesQuery = true;
      this.supportsConnectAsSpectator = true;
      this.BotsIncludedInPlayerCount = false;
      this.BotsIncludedInPlayerList = false;
      this.OptionMenuCaption = "TOXIKK...";
    }

    #region LoadConfig()
    public override void LoadConfig(IniFile ini)
    {
      var sec = ini.GetSection("Toxikk", true);
      var version = sec.GetString("version");
      this.consoleKey = (Keys)sec.GetInt("consoleKey");
      this.useKeystrokesToConnect = sec.GetBool("useKeystrokesToConnect", true);
      this.useSteamIdToConnect = version != null && sec.GetBool("useSteamIdToConnect", false);
      this.skillClassFilter = sec.GetInt("skillClass", 0);
    }
    #endregion

    #region SaveConfig()
    public override void SaveConfig(StringBuilder ini)
    {
      ini.AppendLine();
      ini.AppendLine("[Toxikk]");
      ini.AppendLine($"version=1.0.5");
      ini.AppendLine($"consoleKey={(int)this.consoleKey}");
      ini.AppendLine($"useKeystrokesToConnect={this.useKeystrokesToConnect}");
      ini.AppendLine($"useSteamIdToConnect={this.useSteamIdToConnect}");
      ini.AppendLine($"skillClass={this.skillClassFilter}");
    }
    #endregion

    #region OnOptionMenuClick()
    public override void OnOptionMenuClick()
    {
      using (var dlg = new ToxikkOptionsDialog())
      {
        dlg.UseKeystrokes = this.useKeystrokesToConnect;
        dlg.ConsoleKey = this.consoleKey;
        dlg.UseSteamId = this.useSteamIdToConnect;
        dlg.SkillClass = this.skillClassFilter;
        if (dlg.ShowDialog(Form.ActiveForm) != DialogResult.OK)
          return;
        this.useKeystrokesToConnect = dlg.UseKeystrokes;
        this.consoleKey = dlg.ConsoleKey;
        this.useSteamIdToConnect = dlg.UseSteamId;
        this.skillClassFilter = dlg.SkillClass;
      }
    }
    #endregion

    #region CustomizeServerGridColumns()
    public override void CustomizeServerGridColumns(GridView view)
    {
      var colDescription = view.Columns["ServerInfo.Description"];
      var idx = colDescription.VisibleIndex;
      colDescription.Visible = false;
      AddColumn(view, "_gametype", "GT", "Gametype", 40, idx)
        .OptionsFilter.AutoFilterCondition = AutoFilterCondition.Default;
      
      view.Columns["ServerInfo.Extra.Keywords"].Visible = false;

      idx = view.Columns["ServerInfo.Map"].VisibleIndex;
      AddColumn(view, Mutators, "Mutators", "Game modifications", 100, ++idx);

      idx = view.Columns["ServerInfo.Ping"].VisibleIndex;
      AddColumn(view, "_skillclass", "Skill", "Skill Class: Min-Max", 45, idx, UnboundColumnType.Integer);
      AddColumn(view, "_best", "Best", "Best player's Skill Class", 40, ++idx, UnboundColumnType.Integer);
      AddColumn(view, ScoreLimit, "GS", "Goal Score", 30, ++idx, UnboundColumnType.Integer);
      AddColumn(view, TimeLimit, "TL", "Time Limit", 30, ++idx, UnboundColumnType.Integer);
      AddColumn(view, NewOfficial, "Ofcl", "Official Server managed by REAKKTOR", 35, ++idx, UnboundColumnType.Boolean);
      AddColumn(view, OldOfficialNewRanked, "Ranked", "MXP/SC saved", 35, ++idx, UnboundColumnType.Boolean);
      AddColumn(view, ModdingLevel, "Modded", "unmodded, server-only, server+client", 35, ++idx, UnboundColumnType.String);
      AddColumn(view, GameVersion, "Ver", "Game Version", 40);
    }
    #endregion

    #region GetServerCellValue()
    public override object GetServerCellValue(ServerRow row, string fieldName)
    {
      switch (fieldName)
      {
        case "_skillclass":
          return row.GetRule(ToxikkSkillInfo.MinSkillClass) + "-" + row.GetRule(ToxikkSkillInfo.MaxSkillClass);
          //return new ToxikkSkillInfo(row, this);
        case "_best":
          return Math.Round(this.GetBestPlayerSC(row), 1, MidpointRounding.AwayFromZero);
        case NewOfficial:
        {
          var ver = row.GetRule(GameVersion);
          if (ver != null && CompareVersion(ver, "1.1.71") > 0)
            return row.GetRule(NewOfficial) == "1";
          return row.GetRule(OldOfficialNewRanked) == "1";
        }
        case OldOfficialNewRanked:
        {
          var ver = row.GetRule(GameVersion);
          if (ver == null || CompareVersion(ver, "1.1.71") <= 0)
            return null;
          var val = row.GetRule(fieldName);
          return val == null ? (object)null : val == "1";
        }
        case ModdingLevel:
          var level = row.GetRule(ModdingLevel);
          return level == "0" ? "-" : level == "1" ? "S" : level == "2" ? "S+C" : "";
        case "_gametype":
        { 
          var gt = row.ServerInfo.Extra.Keywords;
          return gt == null ? null : 
            gt == "CRZEntryGame" ? "Menu" :
            gt == "CRZBloodLust" ? "BL" : 
            gt == "CRZTeamGame" ? "SA" : 
            gt == "CRZCellCapture" ? "CC" :
            gt == "CRZAreaDomination" ? "AD" :
            gt == "CRZTimeTrial" ? "TT" :
            gt == "CRZArchRivals" ? "AR" :
            gt == "CRZSquadSurvival" ? "SS" :
            gt == "InfekktedGame" ? "Inf" :
            gt == "TAGame" ? "TA" :
            gt == "TTGame" ? "TR" :
            gt == "D2DGame" ? "2D" :
            gt =="STBGame" ? "SB" : 
            gt == "Comp2v2" ? "2v2" :
            gt == "Comp4v4" ? "4v4" :
            gt;
        }
        case Mutators:
          var mods = (row.GetRule(fieldName) ?? "").ToLower();
          var buff = new StringBuilder();
          foreach (var mod in mods.Split('\x1c'))
          {
            if (mod.Length == 0) continue;
            if (buff.Length > 0) buff.Append(", ");
            foreach (var word in mod.Split(' '))
              buff.Append(char.ToUpper(word[0])).Append(word.Substring(1));
          }
          return buff.ToString();
      }
      return base.GetServerCellValue(row, fieldName);
    }
    #endregion

    #region CompareVersion()
    /// <summary>
    /// Compares 2 version strings in the form of a.b.c
    /// a and b are compared numerically, c alphanumerical so that .8 > .71
    /// </summary>
    public static int CompareVersion(string v1, string v2)
    {
      var p1 = v1.Split('.');
      var p2 = v2.Split('.');
      for (int i = 0; i < p1.Length; i++)
      {
        if (i > p2.Length)
          return +1;
        var isNumeric = int.TryParse(p1[i], out var a) & int.TryParse(p2[i], out var b);
        var c = i <= 1 && isNumeric ? Comparer<int>.Default.Compare(a, b) : StringComparer.InvariantCulture.Compare(p1[i], p2[i]);
        if (c != 0)
          return c;
      }
      if (p2.Length > p1.Length)
        return -1;
      return 0;
    }
    #endregion

    #region GetServerCellToolTip()
    public override string GetServerCellToolTip(ServerRow row, string fieldName)
    {
      if (fieldName == "_gametype")
      {
        var gt = row.ServerInfo?.Description;
        return gt == null ? null :
          gt == "CRZBloodLust" ? "Bloodlust" :
          gt == "CRZTeamGame" ? "Squad Assault" :
          gt == "CRZCellCapture" ? "Cell Capture" :
          gt == "CRZAreaDomination" ? "Area Domination" :
          gt == "CRZTimeTrial" ? "Tutorial" :
          gt == "CRZArchRivals" ? "Arch Rivals" :
          gt == "CRZSquadSurvival" ? "Squad Survival" :
          gt == "InfekktedGame" ? "InfeKKted" :
          gt == "TAGame" ? "Team Arena" :
          gt == "TTGame" ? "Trials" :
          gt == "D2DGame" ? "Toxikk 2D" :
          gt == "STBGame" ? "Score The Banshee" :
          null;
      }

      return base.GetServerCellToolTip(row, fieldName);
    }
    #endregion

    #region FilterServerRow()
    public override bool FilterServerRow(ServerRow row)
    {
      if (this.skillClassFilter == 0 || row.Rules == null)
        return false;

      // remove server rows which are outside the skill class
      var min = int.Parse(row.GetRule(ToxikkSkillInfo.MinSkillClass));
      var max = int.Parse(row.GetRule(ToxikkSkillInfo.MaxSkillClass));
      return this.skillClassFilter < min || this.skillClassFilter > max;
    }
    #endregion

    #region IsValidPlayer()
    public override bool IsValidPlayer(ServerRow server, Player player)
    {
      // hack to remove ghost players which are not really on the server
      this.UpdatePlayerInfos(server);
      return server.Rules != null && !string.IsNullOrEmpty(player.Name) && this.playerInfos.ContainsKey(player.Name);
    }
    #endregion

    #region GetBotCount()
    public override int? GetBotCount(ServerRow row)
    {
      if (row.ServerInfo == null)
        return null;
      int bots = row.ServerInfo.Bots;
      if (bots != 0)
        return bots;
      if (row.Rules != null && int.TryParse(row.GetRule(MinCombatants), out bots))
        return bots;
      return 0;
    }
    #endregion

    #region GetRealPlayerCount()
    public override int? GetRealPlayerCount(ServerRow row)
    {
      if (row.Rules == null)
        return base.GetRealPlayerCount(row);

      var strSteamIds = (row.GetRule("p1073741829") ?? "") + "," + (row.GetRule("p1073741830") ?? "") + "," + (row.GetRule("p1073741831") ?? "");
      return strSteamIds == "" ? 0 : strSteamIds.Split(',', ';').Count(id => id != "");
    }
    #endregion

    #region GetMaxPlayers()
    public override int? GetMaxPlayers(ServerRow row)
    {
      var val = row.GetRule("NumPublicConnections");
      if (val != null)
        return int.Parse(val);
      return base.GetMaxPlayers(row);
    }
    #endregion
    
    #region GetPrivateClients()
    public override int? GetPrivateClients(ServerRow row)
    {
      return row.ServerInfo.MaxPlayers - (GetMaxPlayers(row) ?? 0);
    }
    #endregion

    #region GetBestPlayerSC()
    private decimal GetBestPlayerSC(ServerRow row)
    {
      if (row.Players == null || row.Rules == null)
        return 0;
      decimal maxSc = 0;
      foreach (var player in row.Players)
      {
        var sc = this.GetPlayerCellValue(row, player, "SC");
        if (sc is decimal)
          maxSc = Math.Max(maxSc, (decimal)sc);
      }
      return maxSc;
    }
    #endregion

    #region Connect()

    public override bool Connect(ServerRow server, string password, bool spectate)
    {
      if (consoleKey == Keys.None && (this.useKeystrokesToConnect || spectate))
      {
        using (var dlg = new KeyBindForm("Please press your TOXIKK console key..."))
        {
          if (dlg.ShowDialog(Application.OpenForms[0]) == DialogResult.Cancel)
            return false;
          this.consoleKey = dlg.Key;
        }
      }

      if (this.useKeystrokesToConnect || spectate)
      {
        // don't use the ThreadPool b/c it might be full with waiting server update requests
        new Thread(ctx => { ConnectInBackground(server, password, spectate); }).Start();
        return true;
      }

      return base.Connect(server, password, false);
    }
    #endregion

    #region ConnectInBackground()
    private void ConnectInBackground(ServerRow server, string password, bool spectate)
    {
      var procList = Process.GetProcessesByName("toxikk");
      bool mustStart = procList.All(p => p.MainWindowTitle.Contains("players)") || p.Threads.Count == 0);
      if (mustStart)
        StartToxikk();

      IntPtr hWnd = IntPtr.Zero;
      for (int i = 0; i < SecondsToWaitForMainWindowAfterLaunch; i++)
      {
        hWnd = FindGameWindow();
        if (hWnd != IntPtr.Zero)
          break;
        Thread.Sleep(1000);
      }

      if (hWnd == IntPtr.Zero)
        return;

      if (mustStart)
        SkipIntro(hWnd);
     
      // open the console command line
      Win32.PostMessage(hWnd, Win32.WM_KEYDOWN, (int)consoleKey, 0);
      Win32.PostMessage(hWnd, Win32.WM_KEYUP, (int)consoleKey, 0);

      Thread.Sleep(250);

      // hack: prevent WM_DEADCHAR quirks that might be a side-effect of the console key
      Win32.PostMessage(hWnd, Win32.WM_CHAR, ' ', 0);
      for (int i = 0; i < 3; i++)
      {
        Win32.PostMessage(hWnd, Win32.WM_KEYDOWN, (int)Keys.Back, 0);
        Win32.PostMessage(hWnd, Win32.WM_CHAR, 8, 0);
        Win32.PostMessage(hWnd, Win32.WM_KEYUP, (int)Keys.Back, 0);
      }

      // send the command string
      var msg = "open ";
      bool steamSockets = server.Rules == null || !string.IsNullOrEmpty(server.GetRule("SteamServerId"));
      if ((useSteamIdToConnect && steamSockets || server.Rules == null || server.Players == null) && server.ServerInfo.Extra.SteamID != 0)
        msg += "steam." + server.ServerInfo.Extra.SteamID;
      else
        msg += server.EndPoint.Address + ":" + server.ServerInfo.Extra.Port;
      if (!string.IsNullOrEmpty(password))
        msg += "?password=" + password;
      if (spectate)
        msg += "?spectatoronly=1";
      foreach (var c in msg)
        Win32.PostMessage(hWnd, Win32.WM_CHAR, c, 0);

      Thread.Sleep(750);

      // and press Enter
      Win32.PostMessage(hWnd, Win32.WM_KEYDOWN, (int)Keys.Return, 0);
      Win32.PostMessage(hWnd, Win32.WM_KEYUP, (int)Keys.Return, 0);

      this.ActivateGameWindow(hWnd);
    }
    #endregion

    #region StartToxikk()
    private static void StartToxikk()
    {
      Process.Start("steam://rungameid/324810");
    }
    #endregion

    #region FindGameWindow()
    protected override IntPtr FindGameWindow()
    {
      // when TOXIKK is started with the "-log" option, it first creates a log window which would be returned by Process.AppMainWindow
      // so instead we have to iterate through all top level windows in the system, and test if it is the real TOXIKK main window

      // check if there is a toxikk process running and get its process id
      var procList = Process.GetProcessesByName("toxikk");
      if (procList.Length == 0)
        return IntPtr.Zero;
      int toxikkProcessId = 0;
      foreach (var proc in procList)
      {
        if (!proc.MainWindowTitle.Contains("players)") && proc.Threads.Count > 0) // ignore locally running dedicated servers and zombie processes
        {
          toxikkProcessId = proc.Id;
          break;
        }
      }
      if (toxikkProcessId == 0)
        return IntPtr.Zero;

      // iterate through all top level windows in the system
      foreach (var hWnd in Win32.GetTopLevelWindows())
      {
        // ignore windows which don't belong to the toxikk process
        int procId;
        Win32.GetWindowThreadProcessId(hWnd, out procId);
        if (procId != toxikkProcessId)
          continue;     

        // ignore the log window (only care about the main window with title "TOXIKK (32-bit, DX9)"
        var len = Win32.GetWindowTextLength(hWnd) + 1;
        StringBuilder winTitle = new StringBuilder(len);
        Win32.GetWindowText(hWnd, winTitle, len);
        if (!winTitle.ToString().StartsWith("TOXIKK (32-bit, DX"))
          continue;

        Win32.RECT rect;

        // ignore the small log window and splash screen, but return the large main window
        Win32.GetWindowRect(hWnd, out rect);
        if (rect.Height >= 600)
          return hWnd;

        // when the window is minimized, it can't be the splash screen
        var placement = Win32.GetWindowPlacement(hWnd);
        if (placement.showCmd == Win32.ShowWindowCommands.Minimized && rect.Width > 160)
          return hWnd;
      }
      return IntPtr.Zero;
    }
    #endregion

    #region SkipIntro()
    private void SkipIntro(IntPtr win)
    {
      for (int i = 0; i < 3; i++)
      {
        Win32.PostMessage(win, Win32.WM_LBUTTONDOWN, 1, 0);
        Win32.PostMessage(win, Win32.WM_LBUTTONUP, 0, 0);
        Thread.Sleep(500);
      }
    }
    #endregion


    #region CustomizePlayerGridColumns()
    public override void CustomizePlayerGridColumns(GridView view)
    {
      AddColumn(view, "Team", "Team", "Team", 45, 0);
      AddColumn(view, "Rank", "Rank", "Rank", 35, 1, UnboundColumnType.Integer);
      AddColumn(view, "SC", "SC", "Skill Class", 35, 2, UnboundColumnType.Decimal);
    }
    #endregion

    #region GetPlayerCellValue()
    public override object GetPlayerCellValue(ServerRow server, Player player, string fieldName)
    {
      this.UpdatePlayerInfos(server);
      ToxikkPlayerInfo info;
      if (!this.playerInfos.TryGetValue(player.Name, out info))
        return null;

      if (fieldName == "Team")
        return info.Team;
      if (fieldName == "SC")
        return Math.Round(info.SkillClass, 1, MidpointRounding.AwayFromZero);
      if (fieldName == "Rank")
        return info.Rank;
      return null;
    }
    #endregion

    #region CustomizePlayerContextMenu()
    public override void CustomizePlayerContextMenu(ServerRow server, Player player, List<PlayerContextMenuItem> menu)
    {
      this.UpdatePlayerInfos(server);
      ToxikkPlayerInfo info;
      if (!this.playerInfos.TryGetValue(player.Name, out info))
        return;

      menu.Insert(0, new PlayerContextMenuItem("Open Steam Chat", () => { Process.Start("steam://friends/message/" + info.SteamId); }, true));
      menu.Insert(1, new PlayerContextMenuItem("Show Steam Profile", () => { Process.Start("http://steamcommunity.com/profiles/" + info.SteamId + "/"); }));
      menu.Insert(2, new PlayerContextMenuItem("Add to Steam Friends", () => { Process.Start("steam://friends/add/" + info.SteamId); }));
      menu.Add(new PlayerContextMenuItem("Copy Steam-ID to Clipboard", () => { Clipboard.SetText(info.SteamId); }));
    }
    #endregion

    #region UpdatePlayerInfos()
    private void UpdatePlayerInfos(ServerRow server)
    {
      // no need for update if it's the same server and update timestamp
      if (server == this.serverForPlayerInfos && server.RequestTimestamp == this.dataTimestamp && this.playerInfos.Count > 0)
        return;

      this.serverForPlayerInfos = server;
      this.dataTimestamp = server.RequestTimestamp;
      var newPlayerInfos = new Dictionary<string, ToxikkPlayerInfo>();

      var strNames = (server.GetRule("p1073741832") ?? "") + (server.GetRule("p1073741833") ?? "") + (server.GetRule("p1073741834") ?? "");
      if (string.IsNullOrEmpty(strNames))
        return;
      var gameType = (string)server.GetExtenderCellValue("_gametype");
      bool isTeamGame = "SA,CC,AD,SS,TA,SB,IB,2v2,4v4".Contains(gameType);
      var strSteamIds = (server.GetRule("p1073741829") ?? "") + (server.GetRule("p1073741830") ?? "") + (server.GetRule("p1073741831") ?? "");
      var strSkill = server.GetRule("p1073741837") ?? "";
      var strRank = server.GetRule("p1073741838") ?? "";
      int teamSepIdx = strNames.IndexOf(';');
      var names = strNames.Split(',', ';');
      var steamIds = strSteamIds.Split(',', ';');
      var skills = strSkill.Split(',',';');
      var ranks = strRank.Split(',', ';');
      int i = 0;
      foreach (var name in names)
      {
        var info = new ToxikkPlayerInfo();
        info.Team = !isTeamGame ? "Player" : teamSepIdx < 0 || strNames.IndexOf(name) < teamSepIdx ? "Red" : "Blue";
        if (i < steamIds.Length) 
          info.SteamId = steamIds[i];

        decimal sc;
        if (i < skills.Length && decimal.TryParse(skills[i], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out sc))
          info.SkillClass = sc;
        if (i < ranks.Length && ranks[i].Length > 0)
        {
          int rank;
          if (!char.IsDigit(ranks[i][0]))
          {
            var teamInfo = ranks[i][0];
            if (teamInfo == 29 || teamInfo == 'S')
              info.Team = "Spec";
            else if (teamInfo == 30 || teamInfo == 'Q')
              info.Team = "Queue";
            int.TryParse(ranks[i].Substring(1), out rank);
          }
          else
            int.TryParse(ranks[i], out rank);
          info.Rank = rank;
        }
        newPlayerInfos.Add(name, info);
        ++i;
      }
      this.playerInfos = newPlayerInfos;
      server.PlayerCount.Update();
    }
    #endregion

    #region GetPrettyNameForRule()
    public override string GetPrettyNameForRule(ServerRow row, string ruleName)
    {
      switch (ruleName)
      {
        case MinCombatants: return "min. Combatants";
        case ScoreLimit: return "Score Limit";
        case TimeLimit: return "Time Limit";
        case Mutators: return "Mutators";
        case GameVersion: return "GameVersion";
        case OldOfficialNewRanked: return "Ranked (pre-1.1.8: Official)";
        case NewOfficial: return "Official (since 1.1.8)";
        case ModdingLevel: return "Modding level";
        case ToxikkSkillInfo.MinSkillClass: return "Min SC";
        case ToxikkSkillInfo.MaxSkillClass: return "Max SC";
        case "p1073741825": return "Map";
        case "p1073741826": return "Gametype Class";
        case "p1073741827": return "Server Description";
        case "p1073741829": return "Player IDs #1";
        case "p1073741830": return "Player IDs #2";
        case "p1073741831": return "Player IDs #3";
        case "p1073741832": return "Player names #1";
        case "p1073741833": return "Player names #2";
        case "p1073741834": return "Player names #3";
        case "p1073741837": return "Player SCs";
        case "p1073741838": return "Player ranks";
        case "s0": return "Bot difficulty";
        case "p268435706": return "Max. players";
        case "p1073741840": return "Map list";
      }
      return null;
    }
    #endregion
  }

  #region class ToxikkPlayerInfo
  class ToxikkPlayerInfo
  {
    public string SteamId;
    public decimal SkillClass;
    public int Rank;
    public string Team;
  }
  #endregion

  #region class ToxikkSkillInfo
  class ToxikkSkillInfo : IComparable
  {
    internal const string MinSkillClass = "p268435708";
    internal const string MaxSkillClass = "p268435709";

    private readonly ServerRow row;
    private readonly Toxikk extension;

    public decimal HighestPlayerSkill { get; private set; }
    public int Min { get; private set; }
    public int Max { get; private set; }

    public ToxikkSkillInfo(ServerRow row, Toxikk extension)
    {
      this.row = row;
      this.extension = extension;
      this.Update();
    }

    public void Update()
    {
      this.HighestPlayerSkill = 0;
      this.Min = 0;
      this.Max = 0;

      int num;
      if (int.TryParse(row.GetRule(MinSkillClass), out num))
        this.Min = num;
      if (int.TryParse(row.GetRule(MaxSkillClass), out num))
        this.Max = num;

      if (row.Rules == null || row.Players == null)
        return;

      decimal maxSc = 0;
      foreach (var player in row.Players)
      {
        var sc = this.extension.GetPlayerCellValue(row, player, "SC");
        if (sc is decimal)
          maxSc = Math.Max(maxSc, (decimal) sc);
      }
      this.HighestPlayerSkill = maxSc;
    }

    public int CompareTo(object b)
    {
      ToxikkSkillInfo other = (ToxikkSkillInfo)b;
      if (this.HighestPlayerSkill < other.HighestPlayerSkill) return -1;
      if (this.HighestPlayerSkill > other.HighestPlayerSkill) return +1;
      if (this.Min < other.Min) return -1;
      if (this.Min > other.Min) return +1;
      if (this.Max < other.Max) return -1;
      if (this.Max > other.Max) return +1;
      return 0;
    }

    public override string ToString()
    {
      var str = this.HighestPlayerSkill == 0 ? "" : Math.Round(this.HighestPlayerSkill, MidpointRounding.AwayFromZero) + "/";
      return str + this.Min + "-" + this.Max;
    }
  }
#endregion
}