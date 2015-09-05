using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using QueryMaster;

namespace ServerBrowser
{
  public class Toxikk : GameExtension
  {
    private const string MinCombatants = "p268435703";
    private const string ScoreLimit = "p268435704";
    private const string TimeLimit = "p268435705";
    private const string Mutators = "p1073741828";
    private const string IsOfficial = "s15";

    private const int SecondsToWaitForMainWindowAfterLaunch = 45;
    private Keys consoleKey;
    private ServerRow serverForPlayerInfos;
    private long dataTimestamp;
    private readonly Dictionary<string,ToxikkPlayerInfo> playerInfos = new Dictionary<string, ToxikkPlayerInfo>();


    public Toxikk()
    {
      consoleKey = (Keys)Properties.Settings.Default.ToxikkConsoleKey;
      this.supportsPlayersQuery = true;
      this.supportsRulesQuery = true;
      this.supportsConnectAsSpectator = true;
    }

    #region CustomizeServerGridColumns()
    public override void CustomizeServerGridColumns(GridView view)
    {
      var colDescription = view.Columns["ServerInfo.Description"];
      var idx = colDescription.VisibleIndex;
      colDescription.Visible = false;
      AddColumn(view, "_gametype", "GT", "Gametype", 40, idx);

      view.Columns["ServerInfo.Extra.Keywords"].Visible = false;

      idx = view.Columns["ServerInfo.Map"].VisibleIndex;
      AddColumn(view, Mutators, "Mutators", "Game modifications", 100, ++idx);

      idx = view.Columns["ServerInfo.Ping"].VisibleIndex;
      AddColumn(view, "_skillclass", "Skill", "Skill Class: Min-Max", 45, idx, UnboundColumnType.Integer);
      AddColumn(view, "_best", "Best", "Best player's Skill Class", 40, ++idx, UnboundColumnType.Integer);
      AddColumn(view, ScoreLimit, "GS", "Goal Score", 30, ++idx, UnboundColumnType.Integer);
      AddColumn(view, TimeLimit, "TL", "Time Limit", 30, ++idx, UnboundColumnType.Integer);
      AddColumn(view, IsOfficial, "Ofcl", "Official Server managed by REAKKTOR", 35, ++idx, UnboundColumnType.Boolean);
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
        case IsOfficial:
          return row.GetRule(fieldName) == "1";
        case "_gametype":
          var gt = row.ServerInfo.Description;
          return gt == null ? null : gt.Contains("BloodLust") ? "BL" : gt.Contains("TeamGame") ? "SA" : gt.Contains("Cell") ? "CC" : gt;
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
      // servers with skill restrictions neither allow connections with "steam://connect" nor with an "open ip:port" console command
      if ((string) this.GetServerCellValue(server, "_skillclass") != "1-12")
      {
        var res = XtraMessageBox.Show("This server has a skill restriction.\nOnly the in-game browser can connect to such servers.\nLaunch TOXIKK anyway?",
          "Toxikk Server", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        if (res == DialogResult.No)
          return false;
        if (FindToxikkWindow() == IntPtr.Zero)
        {
          var hWnd = StartToxikk();
          if (hWnd == IntPtr.Zero)
            return false;
          SkipIntro(hWnd);
        }
        return true;
      }
        

      if (consoleKey == Keys.None)
      {
        using (var dlg = new KeyBindForm("Please press your Toxikk console key..."))
        {
          if (dlg.ShowDialog(Application.OpenForms[0]) == DialogResult.Cancel)
            return false;
          consoleKey = dlg.Key;
          Properties.Settings.Default.ToxikkConsoleKey = (int) consoleKey;
          Properties.Settings.Default.Save();
        }
      }

      ThreadPool.QueueUserWorkItem(context => ConnectInBackground(server, password, spectate), null);
      return true;
    }
    #endregion

    #region ConnectInBackground()
    private bool ConnectInBackground(ServerRow server, string password, bool spectate)
    {
      var win = FindToxikkWindow();
      if (win == IntPtr.Zero)
      {
        win = StartToxikk();
        if (win == IntPtr.Zero)
          return false;
        SkipIntro(win);
      }

      Win32.PostMessage(win, Win32.WM_KEYDOWN, (int)consoleKey, 0);
      Win32.PostMessage(win, Win32.WM_KEYUP, (int)consoleKey, 0);

      // hack: prevent WM_DEADCHAR quirks that might be a side-effect of the console key
      Win32.PostMessage(win, Win32.WM_CHAR, ' ', 0);
      for (int i = 0; i < 3; i++)
      {
        Win32.PostMessage(win, Win32.WM_KEYDOWN, (int)Keys.Back, 0);
        Win32.PostMessage(win, Win32.WM_CHAR, 8, 0);
        Win32.PostMessage(win, Win32.WM_KEYUP, (int)Keys.Back, 0);
      }

      var msg = "open " + server.EndPoint.Address + ":" + server.ServerInfo.Extra.Port;
      if (!string.IsNullOrEmpty(password))
        msg += "?password=" + password;
      if (spectate)
        msg += "?spectatoronly=1";
      foreach (var c in msg)
        Win32.PostMessage(win, Win32.WM_CHAR, c, 0);

      Win32.PostMessage(win, Win32.WM_KEYDOWN, (int)Keys.Return, 0);
      Win32.PostMessage(win, Win32.WM_KEYUP, (int)Keys.Return, 0);

      return true;
    }
    #endregion

    #region FindToxikkWindow()
    private static IntPtr FindToxikkWindow()
    {
      foreach (Process proc in Process.GetProcessesByName("toxikk"))
      {
        var hWnd = proc.MainWindowHandle;
        Win32.RECT rect;

        // wait for the main window and ignore the smaller splash screen
        Win32.GetWindowRect(hWnd, out rect);
        if (rect.Height >= 600)
          return hWnd;

        // when the window is minimized, it can't be the splash screen
        var placement = Win32.GetWindowPlacement(hWnd);
        if (placement.showCmd == Win32.ShowWindowCommands.Minimized)
          return hWnd;
      }
      return IntPtr.Zero;
    }
    #endregion

    #region StartToxikk()
    private static IntPtr StartToxikk()
    {
      Process.Start("steam://rungameid/324810");
      for (int i = 0; i < SecondsToWaitForMainWindowAfterLaunch; i++)
      {
        Thread.Sleep(1000);
        var hWnd = FindToxikkWindow();
        if (hWnd != IntPtr.Zero)
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
      if (server == this.serverForPlayerInfos && server.RequestTimestamp == this.dataTimestamp && playerInfos.Count > 0)
        return;

      this.serverForPlayerInfos = server;
      this.dataTimestamp = server.RequestTimestamp;
      this.playerInfos.Clear();

      var strNames = (server.GetRule("p1073741832") ?? "") + (server.GetRule("p1073741833") ?? "") + (server.GetRule("p1073741834") ?? "");
      if (string.IsNullOrEmpty(strNames))
        return;
      var gameType = (string)server.GetExtenderCellValue("_gametype");
      bool isTeamGame = gameType != "BL";
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
        int rank;
        if (i < ranks.Length && int.TryParse(ranks[i], out rank))
          info.Rank = rank;
        this.playerInfos.Add(name, info);
        ++i;
      }
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