using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using QueryMaster;

namespace ServerBrowser
{
  public partial class ServerBrowserForm : XtraForm
  {
    private const string Version = "2.52";
    private const string DevExpressVersion = "v20.1";
    private const string SteamWebApiText = "<Steam Web API>";
    private const string CustomDetailColumnPrefix = "ServerInfo.";
    private const string CustomRuleColumnPrefix = "custRule.";
    private static readonly Game[] DefaultGames = {Game.QuakeLive, Game.Reflex, Game.Toxikk, Game.CounterStrike_Global_Offensive, Game.Team_Fortress_2};
    internal static Color LinkControlColor;

    private readonly GameExtensionPool extenders = new GameExtensionPool();
    private readonly GameExtension unknownGame = new GameExtension();
    private int ignoreUiEvents;
    private readonly List<Game> gameIdForComboBoxIndex = new List<Game>();
    private readonly PasswordForm passwordForm = new PasswordForm();
    private int showAddressMode;
    private readonly ServerQueryLogic queryLogic;
    private readonly string geoIpCachePath;
    private readonly GeoIpClient geoIpClient;
    private readonly Steamworks steam;
    private readonly MemoryStream defaultLayout = new MemoryStream();
    private int geoIpModified;
    private readonly Dictionary<IPEndPoint, string> favServers = new Dictionary<IPEndPoint, string>();
    private TabViewModel viewModel;
    private readonly string xmlLayoutPath;
    private readonly string iniPath;
    private readonly IniFile iniFile;
    private XtraTabPage dragPage;
    private bool isSingleRowUpdate = true;
    private const int PredefinedTabCount = 2;
    private int iniVersion;
    private string iniMasterServers;

    #region ctor()
    public ServerBrowserForm(IniFile ini)
    {
      InitializeComponent();

      this.iniFile = ini;
      var baseDir = Path.GetDirectoryName(this.GetType().Assembly.Location) ?? ".";
      this.iniPath = ini.FileName;
      this.xmlLayoutPath = Path.Combine(baseDir, "WindowLayout.xml");
      this.geoIpCachePath = Path.Combine(baseDir, "locations.txt");
      this.MoveConfigFilesFromOldLocation();
      this.geoIpClient = new GeoIpClient(this.geoIpCachePath);

      this.steam = new Steamworks();
      this.steam.Init();

      this.InitGameInfoExtenders(this.iniFile);
      this.queryLogic = new ServerQueryLogic(this.extenders);

      if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
        return;

      base.Text += " " + Version;

      this.queryLogic.UpdateStatus += (s, e) => this.BeginInvoke((Action)(() => queryLogic_SetStatusMessage(s, e)));
      this.queryLogic.ServerListReceived += (s, e) => this.BeginInvoke((Action) queryLogic_ServerListReceived);
      this.queryLogic.ReloadServerListComplete += (s, e) => this.BeginInvoke((Action)(() => { queryLogic_ReloadServerListComplete(e.Rows); }));
      this.queryLogic.RefreshSingleServerComplete += (s, e) => this.BeginInvoke((Action)(() => { queryLogic_RefreshSingleServerComplete(e); }));

      // make the server list panel fill the remaining space (this can't be done in the Forms Designer)
      this.panelServerList.Parent.Controls.Remove(this.panelServerList);
      this.panelServerList.Dock = DockingStyle.Fill;
      this.Controls.Add(this.panelServerList);
      this.panelStaticList.Height = this.panelQuery.Height;
      UserLookAndFeel.Default.StyleChanged += LookAndFeel_StyleChanged;

      var vm = new TabViewModel();
      vm.Source = TabViewModel.SourceType.Favorites;
      vm.Caption = this.tabFavorites.Text;
      this.tabFavorites.Tag = vm;
    }
    #endregion

    #region MoveConfigFilesFromOldLocation()
    private void MoveConfigFilesFromOldLocation()
    {
      if (!File.Exists(this.iniPath))
      {
        try
        {
          var oldIniPath = Path.Combine(Application.LocalUserAppDataPath, "ServerBrowser.ini");
          if (File.Exists(oldIniPath))
            File.Move(oldIniPath, this.iniPath);
        }
        catch
        {
        }
      }

      if (!File.Exists(this.geoIpCachePath))
      { 
        try
        {
          var geoCache = Path.Combine(Application.LocalUserAppDataPath, "locations.txt");
          if (File.Exists(geoCache))
            File.Move(geoCache, this.geoIpCachePath);
        }
        catch
        {
        }
      }
    }
    #endregion

    #region InitGameInfoExtenders()

    private void InitGameInfoExtenders(IniFile ini)
    {
      extenders.Steamworks = this.steam;
      extenders.Add(Game.Toxikk, new Toxikk());
      extenders.Add(Game.Reflex, new Reflex());
      extenders.Add(Game.QuakeLive, new QuakeLive(Game.QuakeLive));
      extenders.Add(Game.CounterStrike_Global_Offensive, new CounterStrikeGO());

      // some games include bot count also in players count. This hardcoded list can be extended through the INI
      foreach (var game in new[] {Game.Team_Fortress_2})
        extenders.Get(game).BotsIncludedInPlayerCount = true;

      // some games include bots also in player list. This hardcoded list can be extended through the INI
      foreach (var game in new[] { Game.Team_Fortress_2})
        extenders.Get(game).BotsIncludedInPlayerList = true;

      // add menu items for game specific option dialogs
      foreach (var item in extenders.Select(item => item.Value).OrderBy(item => item.OptionMenuCaption))
      {
        item.LoadConfig(ini);
        var menuCaption = item.OptionMenuCaption;
        if (menuCaption == null) continue;
        var menuItem = new BarButtonItem(this.barManager1, menuCaption);
        menuItem.ItemClick += (sender, args) => item.OnOptionMenuClick();
        this.mnuGameOptions.AddItem(menuItem);
      }
    }

    #endregion

    #region OnLoad()
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
        return;

      this.dockManager1.SaveLayoutToStream(this.defaultLayout);

      ++this.ignoreUiEvents;
      this.FillCountryFlags();
      this.FillGameCombo();
      this.InitBranding();
      LoadBonusSkins(this.BonusSkinDllPath);

      this.geoIpClient.LoadCache();

      this.LoadViewModelsFromIniFile(iniFile);
      this.ApplyAppSettings(iniFile);
      this.ApplyGameSettings(iniFile);

      LookAndFeel_StyleChanged(null, null);
      --this.ignoreUiEvents;

      this.ReloadServerList();
    }
    #endregion  
   
    #region FillCountryFlags()
    private void FillCountryFlags()
    {
      int i = 0;
      foreach (var key in this.imgFlags.Images.Keys)
      {
        var country = key.Replace(".png", "").ToUpper();
        this.riCountryFlagEdit.Items.Add(new ImageComboBoxItem(country, i++));
      }
    }
    #endregion

    #region FillGameCombo()
    private void FillGameCombo()
    {
      List<Tuple<string, Game>> list = new List<Tuple<string, Game>>();
      foreach (Game game in Enum.GetValues(typeof(Game)))
        list.Add(new Tuple<string, Game>(this.GetGameCaption(game), game));
      list.Sort();
      foreach (var tuple in list)
      {
        this.comboGames.Properties.Items.Add(tuple.Item1);
        this.gameIdForComboBoxIndex.Add(tuple.Item2);
      }
    }
    #endregion

    #region InitBranding()
    protected virtual void InitBranding()
    {
    }
    #endregion

    #region LoadBonusSkins()
    /// <summary>
    /// Load the BonusSkin DLL dynamically so that the application can be executed without it being present
    /// </summary>
    internal static bool LoadBonusSkins(string dllPath)
    {
      try
      {
        if (!File.Exists(dllPath))
          return false;
        var ass = Assembly.LoadFrom(dllPath);
        var type = ass.GetType("DevExpress.UserSkins.BonusSkins");
        if (type == null)
          return false;
        var method = type.GetMethod("Register", BindingFlags.Static | BindingFlags.Public);
        method?.Invoke(null, null);
        return true;
      }
      catch
      {
        // it's a pity, but life goes on
        try
        {
          File.Delete(dllPath);
        }
        catch { }
        return false;
      }
    }
    #endregion

    #region BonusSkinDllPath
    private string BonusSkinDllPath
    {
      get
      {
        const string dllName = "DevExpress.BonusSkins." + DevExpressVersion + ".dll";
        return Path.GetDirectoryName(Application.ExecutablePath) + "\\" + dllName;        
      }
    }
    #endregion

    #region LoadViewModelsFromIniFile()
    private void LoadViewModelsFromIniFile(IniFile ini)
    {
      bool hasFavTab = false;
      int i = 0;
      bool ignoreMasterServer = (ini.GetSection("Options")?.GetInt("ConfigVersion") ?? 0) < 2;
      foreach (var section in ini.Sections)
      {
        if (System.Text.RegularExpressions.Regex.IsMatch(section.Name, "^Tab[0-9]+$"))
        {
          var vm = new TabViewModel();
          vm.LoadFromIni(ini, section, this.extenders, ignoreMasterServer);
          var page = new XtraTabPage();
          page.Text = section.GetString("TabName") ?? this.GetGameCaption((Game) vm.InitialGameID);
          page.Tag = vm;
          page.ImageIndex = vm.ImageIndex;
          this.tabControl.TabPages.Insert(i++, page);
          hasFavTab |= vm.Source == TabViewModel.SourceType.Favorites;
        }
      }

      // add default games
      if (i == 0)
      {
        foreach (var game in DefaultGames)
        {
          var vm = new TabViewModel();
          vm.InitialGameID = (int)game;
          var page = new XtraTabPage();
          page.Text = this.GetGameCaption(game);
          page.Tag = vm;
          page.ImageIndex = vm.ImageIndex;
          this.tabControl.TabPages.Insert(i++, page);
        }
      }

      if (hasFavTab)
      {
        this.tabControl.TabPages.Remove(this.tabFavorites);
        this.tabFavorites.Dispose();
      }

      if (this.tabControl.TabPages.Count > 2)
        this.tabControl.TabPages.Remove(this.tabGame);
      else
        this.tabGame.Tag = new TabViewModel();
    }
    #endregion

    #region AddNewTab()
    private void AddNewTab(string name, TabViewModel.SourceType sourceType)
    {
      var vm = new TabViewModel();
      vm.Source = sourceType;
      vm.Servers = new List<ServerRow>();
      vm.gameExtension = unknownGame;

      var page = new XtraTabPage();
      page.Text = name;
      page.Tag = vm;
      page.ImageIndex = vm.ImageIndex;
      this.tabControl.TabPages.Insert(this.tabControl.TabPages.Count - 1, page);
      this.tabControl.SelectedTabPage = page;
    }
    #endregion

    #region SetViewModel()
    private void SetViewModel(TabViewModel vm)
    {
      if (vm == this.viewModel)
        return;
      this.queryLogic.Cancel();

      this.viewModel = vm;
      if (vm.Source == TabViewModel.SourceType.Favorites)
      {
        vm.Servers = new List<ServerRow>();
        foreach (var fav in this.favServers)
        {
          var row = new ServerRow(fav.Key, this.extenders.Get(0));
          row.CachedName = fav.Value;
          vm.Servers.Add(row);
        }
      }

      var info = vm.MasterServer;
      if (string.IsNullOrEmpty(info))
        info = SteamWebApiText;
      this.comboMasterServer.Text = info;
      this.SetSteamAppId(vm.InitialGameID);
      this.txtTagIncludeServer.Text = vm.TagsIncludeServer;
      this.txtTagExcludeServer.Text = vm.TagsExcludeServer;
      this.txtMod.Text = vm.FilterMod;
      this.txtMap.Text = vm.FilterMap;
      this.cbGetEmpty.Checked = vm.GetEmptyServers;
      this.cbGetFull.Checked = vm.GetFullServers;
      this.comboQueryLimit.Text = vm.MasterServerQueryLimit.ToString();

      this.gvServers.ActiveFilterString = vm.GridFilter;
      this.comboMinPlayers.Text = vm.MinPlayers == 0 ? "" : vm.MinPlayers.ToString();
      this.cbMinPlayersBots.Checked = vm.MinPlayersInclBots;
      this.comboMaxPing.Text = vm.MaxPing == 0 ? "" : vm.MaxPing.ToString();
      this.btnTagIncludeClient.Text = vm.TagsIncludeClient;
      this.btnTagExcludeClient.Text = vm.TagsExcludeClient;
      this.txtVersion.Text = vm.VersionMatch;

      UpdatePanelVisibility();
      this.isSingleRowUpdate = false;
      UpdateViews(true);
      this.miFindServers.Enabled = vm.Source == TabViewModel.SourceType.MasterServer;
    }
    #endregion

    #region UpdatePanelVisibility()
    private void UpdatePanelVisibility()
    {
      this.SuspendLayout();
      this.panelOptions.Visible = this.miShowOptions.Down;
      this.panelQuery.Visible = this.miShowServerQuery.Down && this.viewModel?.Source == TabViewModel.SourceType.MasterServer;
      this.panelStaticList.Visible = this.miShowServerQuery.Down && this.viewModel?.Source == TabViewModel.SourceType.CustomList;
      this.panelTop.Size = new Size(this.panelTop.ClientSize.Width, 
        this.panelTop.ControlContainer.Top
        + (this.panelOptions.Visible ? this.panelOptions.Height : 0)
        + this.panelTabs.Height
        + (this.panelQuery.Visible ? this.panelQuery.Height : 0)
        + (this.panelStaticList.Visible ? this.panelStaticList.Height : 0)
        );
      this.ResumeLayout();
    }
    #endregion

    #region ApplyAppSettings()

    protected virtual void ApplyAppSettings(IniFile ini)
    {
      string[] masterServers = {};

      this.favServers.Clear();

      int tabIndex = 0;
      var options = ini?.GetSection("Options");
      if (options != null)
        tabIndex = ApplyAppSettingsFromIni(ini, options, out masterServers);

      // fill master server combobox
      this.comboMasterServer.Properties.Items.Clear();
      foreach (var master in masterServers)
        this.comboMasterServer.Properties.Items.Add(master);

      // select tab page
      var idx = tabIndex < this.tabControl.TabPages.Count - 1 ? tabIndex : 0;
      this.SetViewModel((TabViewModel)this.tabControl.TabPages[idx].Tag);
      this.tabControl.SelectedTabPageIndex = idx;

      // fill FindPlayers list
      var sec = ini?.GetSection("FindPlayers");
      if (sec != null)
      {
        this.riFindPlayer.Items.Clear();
        foreach (var key in sec.Keys)
          this.riFindPlayer.Items.Add(sec.GetString(key));
      }
    }
    #endregion

    #region ApplyAppSettingsFromIni()
    private int ApplyAppSettingsFromIni(IniFile ini, IniFile.Section options, out string[] masterServers)
    {
      UserLookAndFeel.Default.SkinName = options.GetString("Skin") ?? "Office 2010 Black";
      iniMasterServers = options.GetString("MasterServers") ?? "";
      if (iniMasterServers == "")
        iniMasterServers = SteamWebApiText + ",hl2master.steampowered.com:27011";
      masterServers = iniMasterServers.Split(',');
      this.iniVersion = options.GetInt("ConfigVersion", 1);
      this.miShowOptions.Down = options.GetBool("ShowOptions", true);
      this.miShowServerQuery.Down = options.GetBool("ShowServerQuery", true);
      this.miShowFilter.Down = options.GetBool("ShowFilter", true);
      this.rbAddressHidden.Checked = options.GetInt("ShowAddressMode") == 0;
      this.rbAddressQueryPort.Checked = options.GetInt("ShowAddressMode") == 1;
      this.rbAddressGamePort.Checked = options.GetInt("ShowAddressMode") == 2;
      this.cbRefreshSelectedServer.Checked = options.GetBool("RefreshSelected", true);
      this.spinRefreshInterval.EditValue = options.GetDecimal("RefreshInterval", 2);
      this.rbUpdateDisabled.Checked = true;
      this.rbUpdateListAndStatus.Checked = options.GetBool("AutoUpdateList", true);
      this.rbUpdateStatusOnly.Checked = options.GetBool("AutoUpdateInfo");
      this.cbNoUpdateWhilePlaying.Checked = !options.GetBool("AutoUpdateWhilePlaying");
      this.cbUseSteamApi.Checked = options.GetBool("UseSteamAPI");
      this.cbFavServersOnTop.Checked = options.GetBool("KeepFavServersOnTop", true);
      this.cbHideUnresponsiveServers.Checked = options.GetBool("HideUnresponsiveServers", true);
      this.cbShowFilterPanelInfo.Checked = options.GetBool("ShowFilterPanelInfo", true);
      this.cbShowCounts.Checked = options.GetBool("ShowServerCounts", true);
      this.cbConnectOnDoubleClick.Checked = options.GetBool("ConnectOnDoubleClick", true);
      this.Size = new Size(options.GetInt("WindowWidth", 1600), options.GetInt("WindowHeight", 840));
      this.cbHideGhosts.Checked = options.GetBool("HideGhostPlayers");

      if (File.Exists(this.xmlLayoutPath))
      {
        try
        {
          using (var stream = new FileStream(this.xmlLayoutPath, FileMode.Open))
            this.dockManager1.RestoreFromStream(stream);
        }
        catch
        {
        }
      }

      // load favorite servers
      var favs = ini.GetSection("FavoriteServers");
      if (favs != null)
      {
        foreach (var server in favs.Keys)
        {
          if (server == "") continue;
          var parts = server.Split(':');
          this.favServers.Add(new IPEndPoint(IPAddress.Parse(parts[0]), int.Parse(parts[1])), favs.GetString(server));
        }
      }
      return options.GetInt("TabIndex");
    }

    #endregion

    #region ApplyGameSettings()
    protected virtual void ApplyGameSettings(IniFile ini)
    {
      var sec = ini?.GetSection("GameSpecific");
      if (sec == null) return;

      foreach (var appId in (sec.GetString("BotsIncludedInPlayerCount") ?? "").Split(' ', ','))
      {
        int id;
        if (int.TryParse(appId.Trim(), out id))
          this.extenders.Get((Game)id).BotsIncludedInPlayerCount = true;
      }

      foreach (var appId in (sec.GetString("BotsIncludedInPlayerList") ?? "").Split(' ', ','))
      {
        int id;
        if (int.TryParse(appId.Trim(), out id))
          this.extenders.Get((Game)id).BotsIncludedInPlayerList = true;
      }
    }
    #endregion


    #region OnFormClosing()
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
      var sb = new StringBuilder();
      this.SaveAppSettings(sb);
      this.SaveGameSettings(sb);
      this.SaveViewModelsToIniFile(sb);
      this.SaveGameOptions(sb);
      File.WriteAllText(this.iniPath, sb.ToString());

      this.queryLogic.Cancel();
      this.geoIpClient.SaveCache();
      base.OnFormClosing(e);
    }
    #endregion

    #region SaveAppSettings()

    protected virtual void SaveAppSettings(StringBuilder sb)
    {
      sb.AppendLine("[Options]");
      sb.AppendLine("ConfigVersion=2");
      sb.AppendLine($"MasterServers={iniMasterServers}");
      sb.AppendLine($"ShowOptions={this.miShowOptions.Down}");
      sb.AppendLine($"ShowServerQuery={this.miShowServerQuery.Down}");
      sb.AppendLine($"ShowFilter={this.miShowFilter.Down}");
      sb.AppendLine($"ShowAddressMode={this.showAddressMode}");
      sb.AppendLine($"RefreshInterval={Convert.ToInt32(this.spinRefreshInterval.EditValue)}");
      sb.AppendLine($"RefreshSelected={this.cbRefreshSelectedServer.Checked}");
      sb.AppendLine($"KeepFavServersOnTop={this.cbFavServersOnTop.Checked}");
      sb.AppendLine($"HideUnresponsiveServers={this.cbHideUnresponsiveServers.Checked}");
      sb.AppendLine($"AutoUpdateList={this.rbUpdateListAndStatus.Checked}");
      sb.AppendLine($"AutoUpdateInfo={this.rbUpdateStatusOnly.Checked}");
      sb.AppendLine($"AutoUpdateWhilePlaying={!this.cbNoUpdateWhilePlaying.Checked}");
      sb.AppendLine($"UseSteamAPI={this.cbUseSteamApi.Checked}");
      sb.AppendLine($"Skin={UserLookAndFeel.Default.SkinName}");
      sb.AppendLine("FontSize=" + AppearanceObject.DefaultFont.SizeInPoints.ToString("n2", NumberFormatInfo.InvariantInfo));
      sb.AppendLine($"TabIndex={this.tabControl.SelectedTabPageIndex}");
      sb.AppendLine($"ShowFilterPanelInfo={this.cbShowFilterPanelInfo.Checked}");
      sb.AppendLine($"ShowServerCounts={this.cbShowCounts.Checked}");
      sb.AppendLine($"ConnectOnDoubleClick={this.cbConnectOnDoubleClick.Checked}");
      sb.AppendLine($"WindowWidth={this.Width}");
      sb.AppendLine($"WindowHeight={this.Height}");
      sb.AppendLine($"HideGhostPlayers={this.cbHideGhosts.Checked}");

      sb.AppendLine();
      sb.AppendLine("[FavoriteServers]");
      foreach (var fav in this.favServers)
        sb.AppendLine($"{fav.Key}={fav.Value}");

      sb.AppendLine();
      sb.AppendLine("[FindPlayers]");
      for (int i = 0; i < this.riFindPlayer.Items.Count; i++)
        sb.AppendLine($"{i}={this.riFindPlayer.Items[i]}");

      try
      {
        using (var stream = new FileStream(this.xmlLayoutPath, FileMode.Create))
          this.dockManager1.SaveLayoutToStream(stream);
      }
      catch
      {
      }
    }

    #endregion

    #region SaveGameSettings()
    private void SaveGameSettings(StringBuilder sb)
    {
#if false

      var sbCount = new StringBuilder();
      var sbList = new StringBuilder();

      foreach (var entry in this.extenders)
      {
        if (entry.Value.BotsIncludedInPlayerCount)
          sbCount.Append((int)entry.Key).Append(' ');
        if (entry.Value.BotsIncludedInPlayerList)
          sbList.Append((int)entry.Key).Append(' ');
      }

      sb.AppendLine();
      sb.AppendLine("[GameSpecific]");
      sb.Append("BotsIncludedInPlayerCount=").AppendLine(sbCount.ToString());
      sb.Append("BotsIncludedInPlayerList=").AppendLine(sbList.ToString());
#endif
    }
    #endregion

    #region SaveViewModelsToIniFile()
    private void SaveViewModelsToIniFile(StringBuilder sb)
    {
      this.UpdateViewModel();

      int pageNr = 0;
      foreach (XtraTabPage page in this.tabControl.TabPages)
      {
        if (page == this.tabAdd) continue;
        ++pageNr;
        var opt = (TabViewModel)page.Tag;
        opt.WriteToIni(sb, "Tab" + pageNr, page.Text);
      }
    }
    #endregion

    #region SaveGameOptions()
    private void SaveGameOptions(StringBuilder sb)
    {
      foreach (var item in this.extenders)
        item.Value.SaveConfig(sb);
    }
    #endregion


    #region UpdateViewModel()
    protected virtual void UpdateViewModel()
    {
      var vm = this.viewModel;
      if (vm == null)
        return;

      vm.MasterServer = this.comboMasterServer.Text;
      if (vm.Source == TabViewModel.SourceType.MasterServer && !this.queryLogic.IsCancelled)
        vm.Servers = this.queryLogic.Servers;

      if (this.comboGames.SelectedIndex < 0)
      {
        int id;
        if (int.TryParse(this.comboGames.Text, out id))
          vm.InitialGameID = id;
      }
      else
        vm.InitialGameID = (int)this.gameIdForComboBoxIndex[this.comboGames.SelectedIndex];

      vm.FilterMod = this.txtMod.Text;
      vm.FilterMap = this.txtMap.Text;
      vm.TagsIncludeServer = this.txtTagIncludeServer.Text;
      vm.TagsExcludeServer = this.txtTagExcludeServer.Text;
      vm.GetEmptyServers = this.cbGetEmpty.Checked;
      vm.GetFullServers = this.cbGetFull.Checked;
      vm.MasterServerQueryLimit = Convert.ToInt32(this.comboQueryLimit.Text);
      int num;
      int.TryParse(this.comboMinPlayers.Text, out num);
      vm.MinPlayers = num;
      vm.MinPlayersInclBots = this.cbMinPlayersBots.Checked;
      int.TryParse(this.comboMaxPing.Text, out num);
      vm.MaxPing = num;
      vm.TagsIncludeClient = this.btnTagIncludeClient.Text;
      vm.TagsExcludeClient = this.btnTagExcludeClient.Text;
      vm.VersionMatch = this.txtVersion.Text;

      vm.GridFilter = this.gvServers.ActiveFilterString;
      var strm = new MemoryStream();
      this.gvServers.SaveLayoutToStream(strm);
      vm.ServerGridLayout = strm;
      vm.serverSource = this.CreateServerSource(vm.MasterServer);
      vm.gameExtension = this.extenders.Get((Game)vm.InitialGameID);

      // remember hidden default columns and remove no longer visible custom columns
      vm.HideColumns.Clear();
      foreach (GridColumn col in this.gvServers.Columns)
      {
        if (col.Tag == null && !col.Visible)
          vm.HideColumns.Add(col.FieldName);
        if (col.VisibleIndex >= 0) continue;
        if (col.Tag != null && col.FieldName.StartsWith(CustomDetailColumnPrefix))
          vm.CustomDetailColumns.Remove(col.FieldName);
        else if (col.Tag != null && col.FieldName.StartsWith(CustomRuleColumnPrefix))
          vm.CustomRuleColumns.Remove(col.FieldName);
      }
    }
    #endregion

    #region CreateServerSource()
    protected virtual IServerSource CreateServerSource(string addressAndPort)
    {
      return new MasterServerClient(Ip4Utils.ParseEndpoint(addressAndPort));
    }
    #endregion

    #region GetGameCaption()
    private string GetGameCaption(Game game)
    {
      return game.ToString().Replace("_", " ");
    }
    #endregion

    #region SetSteamAppId()

    private void SetSteamAppId(int appId)
    { 
      this.gcServers.DataSource = null;
      this.gcDetails.DataSource = null;
      this.gcPlayers.DataSource = null;
      this.gcRules.DataSource = null;
      this.viewModel.lastSelectedServer = null;
      this.viewModel.currentServer = null;

      this.viewModel.InitialGameID = appId;
      this.InitGameExtension();
        
      // show/hide the Rules panel but don't bring it to the front
      var parentPanel = this.panelRules.SavedParent;
      var curTopmost = parentPanel?.ActiveChildIndex ?? -1;
      this.panelPlayers.Visibility = this.viewModel.gameExtension.SupportsPlayersQuery(null) ? DockVisibility.Visible : DockVisibility.Hidden;
      this.panelRules.Visibility = this.viewModel.gameExtension.SupportsRulesQuery(null) ? DockVisibility.Visible : DockVisibility.Hidden;
      parentPanel = this.panelRules.ParentPanel;
      if (parentPanel != null && curTopmost >= 0)
        parentPanel.ActiveChildIndex = curTopmost;

      ++this.ignoreUiEvents;
      int index = this.gameIdForComboBoxIndex.IndexOf((Game)appId);
      if (index >= 0)
        this.comboGames.SelectedIndex = index;
      else if (appId != 0)
        this.comboGames.Text = appId.ToString();
      else
        this.comboGames.EditValue = null;        
      
      --this.ignoreUiEvents;      
    }
    #endregion

    #region InitGameExtension()
    private void InitGameExtension()
    {
      var ext = this.extenders.Get((Game) this.viewModel.InitialGameID);
      if (ext != this.viewModel.gameExtension)
      {
        this.viewModel.ServerGridLayout = null;
        this.viewModel.gameExtension = ext;
      }

      this.gvServers.BeginUpdate();
      this.ResetGridColumns(this.gvServers);
      this.viewModel.gameExtension.CustomizeServerGridColumns(gvServers);
      foreach (var custCol in this.viewModel.CustomDetailColumns)
      {
        var col = this.viewModel.gameExtension.AddColumn(this.gvServers, CustomDetailColumnPrefix + custCol, custCol, null);
        col.UnboundType = UnboundColumnType.Bound;
      }
      foreach (var custCol in this.viewModel.CustomRuleColumns)
        this.viewModel.gameExtension.AddColumn(this.gvServers, CustomRuleColumnPrefix + custCol, custCol, null);
      this.gvServers.EndUpdate();

      if (viewModel.ServerGridLayout != null)
      {
        viewModel.ServerGridLayout.Seek(0, SeekOrigin.Begin);
        this.gvServers.RestoreLayoutFromStream(viewModel.ServerGridLayout);
      }

      this.gvPlayers.BeginUpdate();
      this.ResetGridColumns(this.gvPlayers);
      this.viewModel.gameExtension.CustomizePlayerGridColumns(gvPlayers);
      this.gvPlayers.EndUpdate();
    }
    #endregion

    #region ResetGridColumns()
    private void ResetGridColumns(GridView gview)
    {
      var cols = new List<GridColumn>(gview.Columns);
      foreach (var col in cols)
      {
        if (col.Tag != null)
          gview.Columns.Remove(col);
        else if (col == colHumanPlayers || col == colBots || col == colTotalPlayers || col == colMaxPlayers)
          col.Visible = false;
        else
          col.Visible = col != this.colEndPoint || !this.rbAddressHidden.Checked;
      }
    }
    #endregion


    #region ReloadServerList()
    protected void ReloadServerList()
    {
      if (this.ignoreUiEvents > 0)
        return;

      if (this.viewModel.Source != TabViewModel.SourceType.MasterServer)
      {
        this.RefreshServerInfo();
        return;
      }

      this.UpdateViewModel();
      if (this.viewModel.InitialGameID == 0) // this would result in a truncated list of all games
        return;

      this.isSingleRowUpdate = false;
      this.SetStatusMessage("Requesting server list from master server...");
      this.miStopUpdate.Enabled = true;
      this.timerReloadServers.Stop();
      IpFilter filter = new IpFilter();
      filter.App = (Game)this.viewModel.InitialGameID;
      filter.IsNotEmpty = !this.viewModel.GetEmptyServers;
      filter.IsNotFull = !this.viewModel.GetFullServers;
      filter.GameDirectory = this.viewModel.FilterMod;
      filter.Map = this.viewModel.FilterMap;
      filter.Sv_Tags = this.ParseTags(this.viewModel.TagsIncludeServer);
      filter.VersionMatch = this.viewModel.VersionMatch;
      if (this.viewModel.TagsExcludeServer != "")
      {
        filter.Nor = new IpFilter();
        filter.Nor.Sv_Tags = this.ParseTags(this.viewModel.TagsExcludeServer);
      }
      this.CustomizeFilter(filter);
      this.RefreshGameExtensions();
      this.queryLogic.ReloadServerList(this.viewModel.serverSource, 750, this.viewModel.MasterServerQueryLimit, QueryMaster.Region.Rest_of_the_world, filter);
    }
    #endregion

    #region RefreshServerInfo()
    private void RefreshServerInfo()
    {
      if (this.queryLogic.IsUpdating)
        return;
      this.isSingleRowUpdate = false;
      this.miStopUpdate.Enabled = true;
      this.timerReloadServers.Stop();
      this.SetStatusMessage("Updating status of " + this.viewModel.Servers.Count + " servers...");
      this.RefreshGameExtensions();
      this.queryLogic.RefreshAllServers(this.viewModel.Servers);
    }
    #endregion

    #region RefreshGameExtensions()
    private void RefreshGameExtensions()
    {
      // give game extenders a chance to update their data as well (i.e. from external sources)
      foreach (var ext in this.extenders)
        ext.Value.Refresh();
    }
    #endregion

    #region FilterServerRow()

    private bool FilterServerRow(ServerRow row)
    {
      if (this.cbHideUnresponsiveServers.Checked)
      {
        // behind-NAT servers will have ServerInfo from the master server API, but timeout on A2S_PLAYERS and A2S_RULES and 0 ping
        if (row.ServerInfo == null || (row.ServerInfo.Ping != 0 && row.Status != null && row.Status.StartsWith("timeout")))
          return true;
      }

      if (this.btnTagIncludeClient.Text != "")
      {
        if (!MatchTagCriteria(row.ServerInfo?.Extra.Keywords, this.btnTagIncludeClient.Text))
          return true;
      }

      if (this.btnTagExcludeClient.Text != "")
      {
        if (MatchTagCriteria(row.ServerInfo?.Extra.Keywords, this.btnTagExcludeClient.Text))
          return true;
      }


      int minPlayers;
      int.TryParse(this.comboMinPlayers.Text, out minPlayers);
      if (minPlayers > 0)
      {
        var val = this.cbMinPlayersBots.Checked ? row.PlayerCount.TotalPlayers : row.PlayerCount.RealPlayers;
        if (val.HasValue && val.Value < minPlayers)
          return true;
      }

      int ping;
      int.TryParse(this.comboMaxPing.Text, out ping);
      if (ping != 0)
      {
        if ((row.ServerInfo?.Ping ?? 0) > ping)
          return true;
      }

      return row.GameExtension.FilterServerRow(row);
    }

    #endregion

    #region MatchTagCriteria()
    private bool MatchTagCriteria(string tags, string condition)
    {
      tags = ',' + tags + ',';
      var orParts = condition.Split(';');
      foreach (var orPart in orParts)
      {
        var match = true;
        var andParts = orPart.Split(' ', ',');
        foreach (var andPart in andParts)
        {
          if (!tags.Contains(',' + andPart.Trim() + ','))
          {
            match = false;
            break;
          }
        }
        if (match)
          return true;
      }
      return false;
    }
    #endregion

    #region CheckAlertCondition()
    private void CheckAlertCondition()
    {
      if (this.gvServers.RowCount > 0 && this.cbAlert.Checked)
      {
        this.cbAlert.Checked = false;
        SystemSounds.Asterisk.Play();
        this.alertControl1.Show(this, "Steam Server Browser", DateTime.Now.ToString("HH:mm:ss") + ": Found " + this.gvServers.RowCount + " server(s) matching your criteria.",
          this.imageCollection.Images[2]);
      }
    }
    #endregion

    #region CustomizeFilter()
    protected virtual void CustomizeFilter(IpFilter filter)
    {
      this.viewModel.gameExtension.CustomizeServerFilter(filter);      
    }
    #endregion

    #region ParseTags()
    private string ParseTags(string text)
    {
      return text.Replace("\\", "").Replace(' ', ',');
    }
    #endregion

    #region UpdateViews()
    protected void UpdateViews(bool forceUpdateDetails = false)
    {
      if (this.viewModel == null) return;

      ++ignoreUiEvents;

      // special logic for single-row updates to prevent reordering of rows
      if (this.isSingleRowUpdate)
      {
        this.RefreshDataInSelectedRows();
        --ignoreUiEvents;
        return;
      }

      var topRow = this.gvServers.TopRowIndex;
      this.gvServers.BeginDataUpdate();

      this.LookupGeoIps();
      this.UpdateCachedServerNames();
      
      this.gcServers.DataSource = this.viewModel.Servers;
      this.gvServers.EndDataUpdate();
      this.gvServers.TopRowIndex = topRow;
      
      if (this.viewModel.lastSelectedServer != null)
      {
        int i = 0;
        foreach (var server in this.viewModel.Servers)
        {
          if (server.EndPoint.Equals(this.viewModel.lastSelectedServer.EndPoint))
          {
            var h = gvServers.GetRowHandle(i);
            this.gvServers.FocusedRowHandle = h;
            this.gvServers.ClearSelection();
            this.gvServers.SelectRow(h);
            //this.gvServers.MakeRowVisible(gvServers.FocusedRowHandle);
            break;
          }
          ++i;
        }
      }
      else if (this.gvServers.FocusedRowHandle > 0)
      {
        this.gvServers.FocusedRowHandle = 0;
        this.gvServers.ClearSelection();
        this.gvServers.SelectRow(0);
      }

      --ignoreUiEvents;

      var row = (ServerRow)this.gvServers.GetFocusedRow();
      if (forceUpdateDetails || row != null && row.GetAndResetIsModified())
        this.UpdateGridDataSources();
    }
    #endregion

    #region RefreshDataInSelectedRows()
    private void RefreshDataInSelectedRows()
    {
      var sel = this.gvServers.GetSelectedRows();
      foreach (var handle in sel)
        this.gvServers.RefreshRow(handle);
    }
    #endregion

    #region UpdateCachedServerNames()
    private void UpdateCachedServerNames()
    {
      if (this.viewModel?.Servers != null)
      {
        foreach (var server in this.viewModel.Servers)
        {
          if (server.ServerInfo?.Name != null && this.favServers.ContainsKey(server.EndPoint))
            this.favServers[server.EndPoint] = server.CachedName = server.ServerInfo.Name;
        }
      }
    }
    #endregion

    #region UpdateGridDataSources()
    private void UpdateGridDataSources(bool isCallback = false)
    {
      var row = (ServerRow)this.gvServers.GetFocusedRow();
      this.viewModel.currentServer = row;

      this.gvDetails.BeginDataUpdate();
      if (row == null)
        this.gcDetails.DataSource = null;
      else
      {
        this.gcDetails.DataSource = EnumerateProps(
          row.ServerInfo,
          row.ServerInfo?.Extra,
          row.ServerInfo?.ShipInfo.Mode != null ? row.ServerInfo?.ShipInfo : null);
      }
      this.gvDetails.EndDataUpdate();

      this.gvPlayers.BeginDataUpdate();
      var curSelName = (gvPlayers.GetFocusedRow() as Player)?.Name;
      this.gcPlayers.DataSource = row?.Players;
      this.gvPlayers.EndDataUpdate();
      this.gvPlayers.ExpandAllGroups();
      if (curSelName != null)
      {
        int idx = row?.Players?.FindIndex(p => p.Name == curSelName) ?? -1;
        if (idx >= 0)
          this.gvPlayers.FocusedRowHandle = this.gvPlayers.GetRowHandle(idx);
      }

      this.gvRules.BeginDataUpdate();
      this.gcRules.DataSource = row?.Rules;
      this.gvRules.EndDataUpdate();

      this.UpdateServerContextMenu();

      if (row != null && !isCallback)
      {
        row.GameExtension.Refresh(row, () =>
        {
          this.BeginInvoke((Action) (() =>
          {
            row.PlayerCount.Update();
            this.UpdateGridDataSources(true);
          }));
        });
      }
    }
    #endregion

    #region EnumerateProps()
    private List<Tuple<string, object, string>> EnumerateProps(params object[] objects)
    {
      var result = new List<Tuple<string, object, string>>();
      foreach (var obj in objects)
      {
        if (obj == null) continue;
        var props = new List<PropertyInfo>(obj.GetType().GetProperties());
        props.Sort((a, b) => StringComparer.InvariantCultureIgnoreCase.Compare(a.Name, b.Name));
        foreach (var prop in props)
        {
          if (!"Extra,Item,ShipInfo,EndPoint,IsObsolete,SpecInfo,ModInfo".Contains(prop.Name))
            result.Add(new Tuple<string, object, string>(prop.Name.ToLower(), prop.GetValue(obj, null)?.ToString(), prop.Name));
        }
      }
      return result;
    }
    #endregion

    #region GetServerAddress()
    private string GetServerAddress(ServerRow row)
    {
      return this.showAddressMode == 2 && row.ServerInfo?.Extra != null
        ? row.EndPoint.Address + ":" + row.ServerInfo.Extra.Port
        : row.EndPoint.ToString();
    }
    #endregion

    #region UpdateServerContextMenu()
    private void UpdateTabContextMenu(XtraTabPage page)
    {
      this.miRenameTab.Enabled = page != this.tabAdd;
    }
    #endregion

    #region UpdateServerContextMenu()
    private void UpdateServerContextMenu()
    {
      var canSpec = this.viewModel.currentServer != null && this.viewModel.currentServer.GameExtension.SupportsConnectAsSpectator(this.viewModel.currentServer);
      this.miConnectSpectator.Visibility = canSpec ? BarItemVisibility.Always : BarItemVisibility.Never;
      var selCount = this.gvServers.SelectedRowsCount;
      this.miConnect.Enabled = selCount == 1;
      this.miConnectSpectator.Enabled = selCount == 1;
      this.miCopyAddress.Enabled = selCount > 0;
      this.miPasteAddress.Enabled = this.viewModel.Source == TabViewModel.SourceType.CustomList;
      this.miDelete.Enabled = selCount > 0 && this.viewModel.Source == TabViewModel.SourceType.CustomList;
      this.miFavServer.Enabled = selCount > 0;
      this.miUnfavServer.Enabled = selCount > 0;
    }
    #endregion

    #region ConnectToGameServer()
    private void ConnectToGameServer(ServerRow row, bool spectate)
    {
      if (row?.ServerInfo == null)
        return;

      string password = null;
      if (row.ServerInfo.IsPrivate)
      {
        password = this.PromptForServerPassword(row);
        if (password == null)
          return;
      }

      this.SetStatusMessage("Connecting to " + row.EndPoint + "    " + row.ServerInfo.Name);
      if (this.cbNoUpdateWhilePlaying.Checked && !this.cbUseSteamApi.Checked)
      {
        this.queryLogic.Cancel();
        this.timerReloadServers.Stop();
      }
      if (this.splashScreenManager1.IsSplashFormVisible)
        this.splashScreenManager1.CloseWaitForm();
      this.splashScreenManager1.ShowWaitForm();
      this.splashScreenManager1.SetWaitFormCaption("Connecting to " + row.EndPoint);
      this.splashScreenManager1.SetWaitFormDescription(row.Name);
      row.GameExtension.Connect(row, password, spectate);
      this.timerHideWaitForm.Start();
    }
    #endregion

    #region PromptForServerPassword()
    protected virtual string PromptForServerPassword(ServerRow row)
    {
      if (this.passwordForm.ShowDialog(this) == DialogResult.Cancel)
        return null;
      return this.passwordForm.Password;      
    }
    #endregion

    #region GetPlayerListContextMenuItems()
    private List<PlayerContextMenuItem> GetPlayerListContextMenuItems()
    {
      var server = (ServerRow)this.gvServers.GetFocusedRow();
      var player = (Player)this.gvPlayers.GetFocusedRow();

      var menu = new List<PlayerContextMenuItem>();
      menu.Add(new PlayerContextMenuItem("Add to Buddy list", () => { AddBuddy(server.GameExtension.GetCleanPlayerName(player)); this.UpdateBuddyCount(server); }));
      menu.Add(new PlayerContextMenuItem("Copy Name to Clipboard", () => 
      {
        if (string.IsNullOrEmpty(player.Name))
          Clipboard.Clear();
        else
          Clipboard.SetText(player.Name);
      }));
      server.GameExtension.CustomizePlayerContextMenu(server, player, menu);
      return menu;
    }
    #endregion

    #region SetStatusMessage()
    protected virtual void SetStatusMessage(string message)
    {
      this.txtStatus.Text = DateTime.Now.ToString("G") + " | " + message;
    }
    #endregion
    
    #region AddColumnForDetailToServerGrid()
    private void AddColumnForDetailToServerGrid()
    {
      var info = (Tuple<string,object,string>)this.gvDetails.GetFocusedRow();
      var col = this.gvServers.Columns[CustomDetailColumnPrefix + info.Item3];
      if (col == null)
      {
        this.viewModel.CustomDetailColumns.Add(info.Item3);
        col = this.viewModel.gameExtension.AddColumn(this.gvServers, CustomDetailColumnPrefix + info.Item3, info.Item3, info.Item3, 70, this.gvServers.VisibleColumns.Count);
        col.UnboundType = UnboundColumnType.Bound;
      }
      else
      {
        XtraMessageBox.Show($"Detail {info.Item1} is already shown in column {col.Caption}.", this.Text,
          MessageBoxButtons.OK, MessageBoxIcon.Information);
        this.gvServers.FocusedColumn = col;
      }
    }
    #endregion

    #region AddColumnForRuleToServerGrid()
    private void AddColumnForRuleToServerGrid(string prefix, UnboundColumnType unboundColumnType)
    {
      var rule = (Rule)this.gvRules.GetFocusedRow();
      var col = this.gvServers.Columns[prefix + rule.Name];
      if (col == null)
      {
        this.viewModel.CustomRuleColumns.Add(rule.Name);
        this.viewModel.gameExtension.AddColumn(this.gvServers, prefix + rule.Name, rule.Name, rule.Name, 70, this.gvServers.VisibleColumns.Count, unboundColumnType);
      }
      else
      {
        XtraMessageBox.Show($"Rule {rule.Name} is already shown in column {col.Caption}.", this.Text,
          MessageBoxButtons.OK, MessageBoxIcon.Information);
        this.gvServers.FocusedColumn = col;
      }
    }
    #endregion

    #region LookupGeoIps()
    private void LookupGeoIps()
    {
      if (this.viewModel?.Servers == null) return;
      foreach (var server in this.viewModel.Servers)
      {
        if (server.GeoInfo != null)
          continue;

        var safeServer = server;
        this.geoIpClient.Lookup(safeServer.EndPoint.Address, geo => GeoIpReceived(safeServer, geo));          
      }
    }
    #endregion

    #region GeoIpReceived()
    private void GeoIpReceived(ServerRow server, GeoInfo geoInfo)
    {
      server.GeoInfo = geoInfo;
      Interlocked.Exchange(ref this.geoIpModified, 1);      
    }
    #endregion

    #region FindNextPlayer()
    private void FindNextPlayer(string name)
    {
      if (string.IsNullOrEmpty(name) || name.Length < 3 || this.gvServers.RowCount <= 1)
        return;

      int startHandle = this.gvServers.FocusedRowHandle;
      for (var handle = startHandle + 1; handle != startHandle; )
      {
        var row = (ServerRow)this.gvServers.GetRow(handle);
        if (row == null)
        {
          handle = 0;
          continue;
        }

      
        var index = row.Players?.FindIndex(NameFinder(row.GameExtension, name));
        if (index >= 0)
        {
          this.gvServers.ClearSelection();
          this.gvServers.FocusedRowHandle = handle;
          this.gvServers.SelectRow(handle);
          this.gvServers.MakeRowVisible(handle);
          this.UpdateGridDataSources();
          this.gvPlayers.FocusedRowHandle = this.gvPlayers.GetRowHandle(index.Value);
          return;
        }
        ++handle;
      }
    }
    #endregion

    #region NameFinder()
    private Predicate<Player> NameFinder(GameExtension game, string name)
    {
      bool contains = name.StartsWith("*");
      if (contains)
        name = name.Substring(1);
      name = name.ToLower().TrimEnd('*');

      return p => contains ? game.GetCleanPlayerName(p).ToLower().Contains(name) : game.GetCleanPlayerName(p).ToLower().StartsWith(name);
    }
    #endregion

    #region UpdateBuddyCount()

    private void UpdateBuddyCount()
    {
      if (this.riFindPlayer.Items.Count > 0)
      {
        foreach (var row in this.queryLogic.Servers)
          UpdateBuddyCount(row);
      }
    }

    private void UpdateBuddyCount(ServerRow row)
    {
      if (row.Players == null)
        return;
      int count = 0;

      long bitmask = 0; // this 64bit bitmask is used to prevent players from being counted multiple times when they match multiple filters

      foreach (string buddy in this.riFindPlayer.Items)
      {
        var nf = NameFinder(row.GameExtension, buddy);
        for (int i = 0, n = row.Players.Count; i < n; i++)
        {
          if ((bitmask & (1L << i)) == 0 && nf(row.Players[i]))
          {
            ++count;
            bitmask |= (1L << i);
          }
        }
          
      }

      int? buddyCount = count == 0 ? null : (int?)count;
      if (row.BuddyCount != buddyCount)
      {
        row.BuddyCount = buddyCount;
        row.SetModified();
      }
    }
    #endregion

    #region AddBuddy()
    private void AddBuddy(string text)
    {
      if (string.IsNullOrWhiteSpace(text))
        return;
      var list = this.riFindPlayer.Items.OfType<string>().ToList();
      if (list.Contains(text))
        return;
      list.Add(text);
      list.Sort();
      this.riFindPlayer.Items.Clear();
      this.riFindPlayer.Items.AddRange(list);
      this.miFindPlayer.EditValue = text;
    }
    #endregion


    // general components

    #region queryLogic_SetStatusMessage
    protected virtual void queryLogic_SetStatusMessage(object sender, TextEventArgs e)
    {
      this.SetStatusMessage(e.Text);
    }
    #endregion

    #region queryLogic_ServerListReceived
    private void queryLogic_ServerListReceived()
    {
      this.viewModel.Servers = this.queryLogic.Servers;
      this.UpdateViews();
    }
    #endregion

    #region queryLogic_ReloadServerListComplete()
    protected virtual void queryLogic_ReloadServerListComplete(List<ServerRow> rows)
    {
      this.UpdateBuddyCount();
      this.UpdateViews();
      this.SetStatusMessage("Update of " + this.viewModel.Servers.Count + " servers complete");
      this.miStopUpdate.Enabled = false;
      this.CheckAlertCondition();
      if (this.spinRefreshInterval.Value > 0)
        this.timerReloadServers.Start();
      this.isSingleRowUpdate = true;
    }

    #endregion

    #region queryLogic_RefreshSingleServerComplete()
    protected virtual void queryLogic_RefreshSingleServerComplete(ServerEventArgs e)
    {
      this.UpdateBuddyCount(e.Server);
      var idx = this.viewModel?.Servers?.IndexOf(e.Server) ?? -1;
      if (idx >= 0)      
        this.gvServers.RefreshRow(this.gvServers.GetRowHandle(idx));
      this.UpdateGridDataSources();
    }
    #endregion

    #region LookAndFeel_StyleChanged
    private void LookAndFeel_StyleChanged(object sender, EventArgs eventArgs)
    {
      var skin = DevExpress.Skins.CommonSkins.GetSkin(UserLookAndFeel.Default);
      DevExpress.Skins.SkinElement label = skin[DevExpress.Skins.CommonSkins.SkinLabel];
      var color = label.Color.ForeColor;      
      color = skin.TranslateColor(color);
      if (color == Color.Transparent)
        color = this.panelQuery.ForeColor;
      LinkControlColor = color;
      this.linkFilter1.Appearance.LinkColor = this.linkFilter1.Appearance.PressedColor = color;

      this.miConnect.ItemAppearance.Normal.Font = new Font(this.miConnect.ItemAppearance.Normal.Font, FontStyle.Bold);
    }
    #endregion

    #region dockManager1_StartDocking
    private void dockManager1_StartDocking(object sender, DockPanelCancelEventArgs e)
    {
      if (e.Panel == this.panelServerList)
        e.Cancel = true;
    }
    #endregion

    #region dockManager1_BeforeLoadLayout
    private void dockManager1_BeforeLoadLayout(object sender, LayoutAllowEventArgs e)
    {
      int old;
      int.TryParse(e.PreviousVersion, out old);
      e.Allow = old == int.Parse(this.dockManager1.LayoutVersion);
    }
    #endregion

    #region alertControl1_AlertClick
    private void alertControl1_AlertClick(object sender, DevExpress.XtraBars.Alerter.AlertClickEventArgs e)
    {
      this.BringToFront();
      if (this.alertControl1.AlertFormList.Count > 0)
      {
        var win = this.alertControl1.AlertFormList[0];
        win.Dispose();
        this.alertControl1.AlertFormList.Remove(win);
      }
    }
    #endregion

    #region timerHideWaitForm_Tick
    private void timerHideWaitForm_Tick(object sender, EventArgs e)
    {
      this.timerHideWaitForm.Stop();
      this.splashScreenManager1.CloseWaitForm();
    }
    #endregion

    // main menu

    #region mi*_DownChanged / ItemClick

    private void miShowOptions_DownChanged(object sender, ItemClickEventArgs e)
    {
      this.UpdatePanelVisibility();
    }

    private void miServerQuery_DownChanged(object sender, ItemClickEventArgs e)
    {
      this.UpdatePanelVisibility();
    }

    private void miShowFilter_DownChanged(object sender, ItemClickEventArgs e)
    {
      this.grpQuickFilter.Visible = this.miShowFilter.Down;
    }
    #endregion

    #region mi*_ItemClick
    private void miFindServers_ItemClick(object sender, ItemClickEventArgs e)
    {
      this.ReloadServerList();
    }

    private void miQuickRefresh_ItemClick(object sender, ItemClickEventArgs e)
    {
      this.RefreshServerInfo();
    }

    private void miStopUpdate_ItemClick(object sender, ItemClickEventArgs e)
    {
      this.queryLogic.Cancel();
      this.SetStatusMessage("Server status update cancelled");
      this.miStopUpdate.Enabled = false;
      if (this.spinRefreshInterval.Value > 0)
        this.timerReloadServers.Start();
    }

    #endregion

    #region miRestoreStandardLayout_ItemClick
    private void miRestoreStandardLayout_ItemClick(object sender, ItemClickEventArgs e)
    {
      try
      {
        this.defaultLayout.Seek(0, SeekOrigin.Begin);
        this.dockManager1.RestoreFromStream(this.defaultLayout);
      }
      catch
      {
      }
    }
    #endregion

    #region miRenameTab_ItemClick
    private void miRenameTab_ItemClick(object sender, ItemClickEventArgs e)
    {
      using (var dlg = new RenameTabForm())
      {
        dlg.Caption = this.tabControl.SelectedTabPage.Text;
        if (dlg.ShowDialog(this) == DialogResult.OK)
          this.tabControl.SelectedTabPage.Text = dlg.Caption;
      }
    }
    #endregion

    #region riFindPlayer_ButtonPressed
    private void riFindPlayer_ButtonPressed(object sender, ButtonPressedEventArgs e)
    {
      var text = (barManager1.ActiveEditor as ComboBoxEdit)?.EditValue as string;

      if (e.Button.Kind == ButtonPredefines.Plus)
      {
        AddBuddy(text);
      }
      else if (e.Button.Kind == ButtonPredefines.Minus)
      {
        var list = this.riFindPlayer.Items.OfType<string>().Where(i => i != text).ToList();
        this.riFindPlayer.Items.Clear();
        this.riFindPlayer.Items.AddRange(list);
        this.miFindPlayer.EditValue = "";
      }
      else if (e.Button.Kind == ButtonPredefines.Search)
        this.FindNextPlayer(text);
    }
    #endregion

    #region riFindPlayer_KeyDown

    private void riFindPlayer_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
        this.FindNextPlayer((barManager1.ActiveEditor as ComboBoxEdit)?.EditValue as string);

    }
    #endregion

    #region miAbout*_ItemClick
    private void miAboutGithub_ItemClick(object sender, ItemClickEventArgs e)
    {
      Process.Start("https://github.com/PredatH0r/SteamServerBrowser/wiki");
    }

    private void miAboutSteamWorkshop_ItemClick(object sender, ItemClickEventArgs e)
    {
      Process.Start("http://steamcommunity.com/sharedfiles/filedetails/?id=543312745");
    }

    private void miAboutVersionHistory_ItemClick(object sender, ItemClickEventArgs e)
    {
      Process.Start("https://github.com/PredatH0r/SteamServerBrowser/releases");
    }

    #endregion

    // option elements

    #region spinRefreshInterval_EditValueChanged
    private void spinRefreshInterval_EditValueChanged(object sender, EventArgs e)
    {
      this.timerReloadServers.Stop();
      int mins;
      int.TryParse(this.spinRefreshInterval.Value.ToString(), out mins);
      if (mins > 0)
      {
        this.timerReloadServers.Interval = mins * 60000;
        this.timerReloadServers.Start();
      }
    }
    #endregion

    #region cbShowGamePort_CheckedChanged
    private void rbAddress_CheckedChanged(object sender, EventArgs e)
    {
      this.colEndPoint.Visible = !this.rbAddressHidden.Checked;
      this.showAddressMode = this.rbAddressQueryPort.Checked ? 1 : this.rbAddressGamePort.Checked ? 2 : 0;
      if (this.colEndPoint.Visible)
      {
        this.gvServers.BeginUpdate();
        this.gvServers.EndUpdate();
      }
    }
    #endregion

    #region cbFavServersOnTop_CheckedChanged
    private void cbFavServersOnTop_CheckedChanged(object sender, EventArgs e)
    {
      this.gvServers.BeginSort();
      if (this.cbFavServersOnTop.Checked)
        gvServers_StartSorting(null, null);
      else
        this.colFavServer.SortOrder = ColumnSortOrder.None;
      this.gvServers.EndSort();
    }
    #endregion

    #region cbHideUnresponsiveServers_CheckedChanged
    private void cbHideUnresponsiveServers_CheckedChanged(object sender, EventArgs e)
    {
      this.UpdateViews();
    }
    #endregion

    #region btnSkin_Click
    private void btnSkin_Click(object sender, EventArgs e)
    {
      using (var dlg = new SkinPicker(this.BonusSkinDllPath))
        dlg.ShowDialog(this);
    }
    #endregion

    #region cbShowFilterPanelInfo_CheckedChanged
    private void cbShowFilterPanelInfo_CheckedChanged(object sender, EventArgs e)
    {
      this.txtFilterInfoClient.Visible = this.txtFilterInfoMaster.Visible = this.cbShowFilterPanelInfo.Checked;
    }
    #endregion

    #region cbShowCounts_CheckedChanged
    private void cbShowCounts_CheckedChanged(object sender, EventArgs e)
    {
      this.gvServers.OptionsView.ShowFooter = this.cbShowCounts.Checked;
    }
    #endregion

    #region cbHideGhosts_CheckedChanged
    private void cbHideGhosts_CheckedChanged(object sender, EventArgs e)
    {
      this.gvPlayers.RefreshData();
    }
    #endregion


    // server-side filters

    #region comboGames_SelectedIndexChanged
    private void comboGames_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.ignoreUiEvents > 0) return;
      var idx = this.comboGames.SelectedIndex;
      if (idx < 0)
        return;
      this.UpdateViewModel();
    }
    #endregion

    #region btnUpdateList_Click
    private void btnUpdateList_Click(object sender, EventArgs e)
    {
      ReloadServerList();
    }
    #endregion

    #region btnUpdateStatus_Click
    private void btnUpdateStatus_Click(object sender, EventArgs e)
    {
      this.RefreshServerInfo();
    }
    #endregion

    #region txtTag_ButtonClick
    private void txtTag_ButtonClick(object sender, ButtonPressedEventArgs e)
    {
      ((TextEdit)sender).Text = "";
    }
    #endregion

    #region txtGameServer_ButtonClick
    private void txtGameServer_ButtonClick(object sender, ButtonPressedEventArgs e)
    {
      try
      {
        string[] parts = this.txtGameServer.Text.Split(':');
        if (parts[0].Length == 0) return;
        // get IPv4 address
        var addr = Dns.GetHostAddresses(parts[0]);
        int i;
        for (i = 0; i < addr.Length; i++)
        {
          if (addr[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            break;
        }
        if (i >= addr.Length) return;
        var endpoint = new IPEndPoint(addr[i], parts.Length > 1 ? int.Parse(parts[1]) : 27015);
        if (endpoint.Address.ToString() == "0.0.0.0") return;
        ServerRow serverRow = null;
        foreach (var row in this.viewModel.Servers)
        {
          if (row.EndPoint.Equals(endpoint))
          {
            serverRow = row;
            break;
          }
        }

        if (serverRow == null)
        {
          this.gvServers.BeginDataUpdate();
          serverRow = new ServerRow(endpoint, this.extenders.Get(0));
          this.viewModel.Servers.Add(serverRow);
          this.gvServers.EndDataUpdate();
          serverRow.SetModified();
          var handle = this.gvServers.GetRowHandle(this.viewModel.Servers.Count - 1);
          this.gvServers.FocusedRowHandle = handle;
          this.gvServers.SelectRow(handle);          
        }
        this.queryLogic.RefreshSingleServer(serverRow);
      }
      catch
      {
      }
    }
    #endregion

    #region txtGameServer_KeyDown
    private void txtGameServer_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyData == Keys.Enter || e.KeyData == Keys.Return)
        this.txtGameServer_ButtonClick(null, null);
    }
    #endregion

    // client-side filters

    #region linkFilter_HyperlinkClick
    private void linkFilter_HyperlinkClick(object sender, HyperlinkClickEventArgs e)
    {
      this.gvServers.ShowFilterEditor(this.colHumanPlayers);
    }
    #endregion

    #region comboMinPlayers, cbMinPlayersBots, comboMaxPing
    private void comboMinPlayers_EditValueChanged(object sender, EventArgs e)
    {
      this.btnApplyFilter_Click(null, null);
    }

    private void cbMinPlayersBots_CheckedChanged(object sender, EventArgs e)
    {
      this.btnApplyFilter_Click(null, null);
    }

    private void comboMaxPing_EditValueChanged(object sender, EventArgs e)
    {
      this.btnApplyFilter_Click(null, null);
    }
    #endregion

    #region btnApplyFilter_Click
    private void btnApplyFilter_Click(object sender, EventArgs e)
    {
      this.gvServers.BeginSort();
      this.gvServers.EndSort();
    }
    #endregion

    #region cbAlert_CheckedChanged
    private void cbAlert_CheckedChanged(object sender, EventArgs e)
    {
      this.cbAlert.ImageIndex = this.cbAlert.Checked ? 2 : 5;
      this.CheckAlertCondition();
    }
    #endregion

    // Servers grid

    #region timerReloadServers_Tick
    private void timerReloadServers_Tick(object sender, EventArgs e)
    {
      if (this.spinRefreshInterval.Value == 0)
        return;

      // CS:GO and other huge games may return more data than can be queried in one interval
      if (this.queryLogic.IsUpdating)
        return;

      if (this.cbNoUpdateWhilePlaying.Checked && this.cbUseSteamApi.Checked)
      {
        if (steam.IsInGame())
          return;
      }

      if (this.rbUpdateListAndStatus.Checked)
        this.ReloadServerList();
      else if (this.rbUpdateStatusOnly.Checked)
        this.RefreshServerInfo();
    }
    #endregion

    #region timerUpdateServerList_Tick
    private void timerUpdateServerList_Tick(object sender, EventArgs e)
    {
      this.timerUpdateServerList.Stop();
      try
      {
        int geoIpMod = Interlocked.Exchange(ref this.geoIpModified, 0);
        if (!this.queryLogic.GetAndResetDataModified() && geoIpMod == 0)
          return;

        this.UpdateViews();
      }
      finally
      {
        this.timerUpdateServerList.Start();
      }
    }
    #endregion

    #region gvServers_CustomRowFilter
    private void gvServers_CustomRowFilter(object sender, RowFilterEventArgs e)
    {
      var row = this.viewModel.Servers[e.ListSourceRow];

      if (FilterServerRow(row))
      {
        e.Visible = false;
        e.Handled = true;
      }
      else
        e.Handled = false;
    }
    #endregion

    #region gvServers_CustomUnboundColumnData
    private void gvServers_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
    {
      if (e.IsSetData)
      {
        if (e.Column == this.colFavServer)
        {
          var server = (ServerRow) e.Row;
          if ((bool) e.Value)
            this.favServers[server.EndPoint] = server.Name;
          else
            this.favServers.Remove(server.EndPoint);
        }
        return;
      }

      var row = (ServerRow) e.Row;
      if (e.Column == this.colFavServer)
        e.Value = this.favServers.ContainsKey(row.EndPoint);
      else if (e.Column == this.colEndPoint)
        e.Value = GetServerAddress(row);
      else if (e.Column == this.colName)
        e.Value = row.ServerInfo?.Name.Trim() ?? (showAddressMode == 0 ? GetServerAddress(row) : null);
      else if (e.Column.FieldName.StartsWith(CustomRuleColumnPrefix))
      {
        var fieldName = e.Column.FieldName.Substring(CustomRuleColumnPrefix.Length);
        e.Value = row.GetExtenderCellValue(fieldName);
        decimal val;
        if (e.Value != null && decimal.TryParse(e.Value.ToString(), out val))
          e.Value = val;
      }
      else
        e.Value = row.GetExtenderCellValue(e.Column.FieldName);
    }
    #endregion

    #region gvServers_CustomColumnDisplayText
    private void gvServers_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
    {
      if (e.Column == this.colLocation)
      {
        if (e.ListSourceRowIndex < 0 || e.ListSourceRowIndex >= this.viewModel.Servers.Count)
          return;
        var row = this.viewModel.Servers[e.ListSourceRowIndex];
        var geoInfo = row.GeoInfo;
        if (geoInfo != null && geoInfo.Iso2 == "US" && !string.IsNullOrEmpty(geoInfo.State))
          e.DisplayText = geoInfo.State;
      }
    }
    #endregion

    #region toolTipController_GetActiveObjectInfo
    private void toolTipController_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
    {
      if (e.Info == null && e.SelectedControl == this.gcServers)
      {
        var hit = this.gvServers.CalcHitInfo(e.ControlMousePosition);
        if (hit.InRowCell && hit.Column == this.colLocation)
        {
          var row = (ServerRow)this.gvServers.GetRow(hit.RowHandle);
          if (row?.GeoInfo != null)
            e.Info = new ToolTipControlInfo(row.EndPoint + "-" + hit.Column.FieldName, row.GeoInfo.ToString());
        }
        else if (hit.InRowCell)
        {
          var row = (ServerRow)this.gvServers.GetRow(hit.RowHandle);
          var tip = row?.GameExtension.GetServerCellToolTip(row, hit.Column.FieldName);
          if (tip != null)
            e.Info = new ToolTipControlInfo(row.EndPoint + "-" + hit.Column.FieldName, tip);
        }
      }
      else if (e.Info == null && e.SelectedControl == this.gcRules)
      {
        var hit = this.gvRules.CalcHitInfo(e.ControlMousePosition);
        if (hit.InRowCell && hit.Column == this.colRuleName)
        {
          var rule = (Rule)this.gvRules.GetRow(hit.RowHandle);
          var server = (ServerRow) this.gvServers.GetFocusedRow();
          var pretty = server?.GameExtension?.GetPrettyNameForRule(server, rule.Name);
          if (pretty != null)
            e.Info = new ToolTipControlInfo(rule.Name, rule.Name);
        }
      }
    }
    #endregion

    #region gvServers_FocusedRowChanged
    private void gvServers_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
    {
      try
      {
        if (this.ignoreUiEvents > 0) return;
        this.UpdateServerContextMenu();
        var row = (ServerRow)this.gvServers.GetFocusedRow();
        this.viewModel.lastSelectedServer = row;
        if (row != this.viewModel.currentServer)
          this.UpdateGridDataSources();

        if (row != null && this.cbRefreshSelectedServer.Checked && !this.queryLogic.IsUpdating)
        {
          this.queryLogic.RefreshSingleServer(row);
          if (row.GeoInfo == null)
            this.geoIpClient.Lookup(row.EndPoint.Address, geoInfo => this.GeoIpReceived(row, geoInfo));
        }
      }
      catch (Exception ex)
      {
        this.SetStatusMessage(ex.Message);
      }
    }
    #endregion

    #region gvServers_SelectionChanged
    private void gvServers_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (this.ignoreUiEvents == 0)
        this.UpdateServerContextMenu();
    }
    #endregion

    #region gvServers_ColumnFilterChanged
    private void gvServers_ColumnFilterChanged(object sender, EventArgs e)
    {
      if (ignoreUiEvents == 0)
        this.UpdateGridDataSources();
    }
    #endregion

    #region gvServers_DoubleClick
    private void gvServers_DoubleClick(object sender, EventArgs e)
    {
      if (!this.cbConnectOnDoubleClick.Checked)
        return;
      try
      {
        var hit = this.gvServers.CalcHitInfo(this.gcServers.PointToClient(MousePosition));
        if (!hit.InRow)
          return;
        var row = (ServerRow) this.gvServers.GetRow(hit.RowHandle);
        this.ConnectToGameServer(row, (ModifierKeys & Keys.Shift) != 0);
      }
      catch (Exception ex)
      {
        this.SetStatusMessage(ex.Message);
      }
    }
    #endregion

    #region gvServers_MouseDown
    private void gvServers_MouseDown(object sender, MouseEventArgs e)
    {
      var hit = this.gvServers.CalcHitInfo(e.Location);
      if ((hit.InDataRow || hit.HitTest == GridHitTest.EmptyRow) && e.Button == MouseButtons.Right)
      {
        this.gcServers.Focus();
        this.gvServers.FocusedRowHandle = hit.RowHandle;
        if (!this.gvServers.IsRowSelected(hit.RowHandle))
        {
          this.gvServers.ClearSelection();
          this.gvServers.SelectRow(hit.RowHandle);
        }
        this.UpdateServerContextMenu();
        this.menuServers.ShowPopup(this.gcServers.PointToScreen(e.Location));
      }
    }
    #endregion

    #region gvServers_StartSorting
    private void gvServers_StartSorting(object sender, EventArgs e)
    {
      if (this.cbFavServersOnTop.Checked)
      {
        if (this.gvServers.SortInfo[this.colFavServer] == null)
          this.gvServers.SortInfo.Insert(0, this.colFavServer, ColumnSortOrder.Descending);
        this.colFavServer.SortOrder = ColumnSortOrder.Descending;
        this.colFavServer.SortIndex = 0;
      }

      // always add Ping as a last sorting criteria
      if (this.gvServers.SortInfo[this.colPing] == null)
      {
        this.gvServers.SortInfo.Add(this.colPing, ColumnSortOrder.Ascending);
        this.colPing.SortOrder = ColumnSortOrder.Ascending;
        this.colPing.SortIndex = this.gvServers.SortInfo.Count - 1;
      }
    }
    #endregion

    #region gvServers_CustomColumnSort
    private void gvServers_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
    {
      if (e.Column == this.colPing)
      {
        object val1 = this.gvServers.GetListSourceRowCellValue(e.ListSourceRowIndex1, colPing);
        object val2 = this.gvServers.GetListSourceRowCellValue(e.ListSourceRowIndex1, colPing);
        if (val1 == null)
          e.Result = val2 == null ? 0 : 1;
        else if (val2 == null)
          e.Result = -1;
        else
          e.Result = Comparer<int>.Default.Compare((int) val1, (int) val2);
        e.Handled = true;
      }
    }
    #endregion

    #region miUpdateServerInfo_ItemClick
    private void miUpdateServerInfo_ItemClick(object sender, ItemClickEventArgs e)
    {
      this.RefreshGameExtensions();
      if (this.gvServers.SelectedRowsCount == 1)
      {
        this.queryLogic.RefreshSingleServer((ServerRow) this.gvServers.GetFocusedRow());
      }
      else
      {
        var list = new List<ServerRow>();
        foreach (var handle in this.gvServers.GetSelectedRows())
          list.Add((ServerRow) this.gvServers.GetRow(handle));
        this.queryLogic.RefreshAllServers(list);
      }
    }
    #endregion

    #region miConnect_ItemClick
    private void miConnect_ItemClick(object sender, ItemClickEventArgs e)
    {
      this.ConnectToGameServer((ServerRow)this.gvServers.GetFocusedRow(), false);
    }
    #endregion

    #region miConnectSpectator_ItemClick
    private void miConnectSpectator_ItemClick(object sender, ItemClickEventArgs e)
    {
      this.ConnectToGameServer((ServerRow)this.gvServers.GetFocusedRow(), true);
    }
    #endregion

    #region miDelete_ItemClick
    private void miDelete_ItemClick(object sender, ItemClickEventArgs e)
    {
      if (this.viewModel.Source != TabViewModel.SourceType.CustomList)
        return;
      var rowHandles = this.gvServers.GetSelectedRows();
      var indices = rowHandles.Select(h => this.gvServers.GetDataSourceRowIndex(h)).OrderBy(i => i).ToList();

      int offset = 0;
      foreach (var index in indices)
        this.viewModel.Servers.RemoveAt(index - offset++);
      this.UpdateViews();
    }
    #endregion

    #region miCopyAddress_ItemClick
    private void miCopyAddress_ItemClick(object sender, ItemClickEventArgs e)
    {
      var sb = new StringBuilder();
      foreach (var handle in this.gvServers.GetSelectedRows())
      {
        var row = (ServerRow) this.gvServers.GetRow(handle);
        var addr = this.GetServerAddress(row);
        if (sb.Length > 0)
          sb.AppendLine();
        sb.Append(addr);
      }
      
      Clipboard.SetText(sb.ToString());
    }
    #endregion

    #region miSteamUrl_ItemClick
    private void miSteamUrl_ItemClick(object sender, ItemClickEventArgs e)
    {
      var sb = new StringBuilder();
      foreach (var handle in this.gvServers.GetSelectedRows())
      {
        var row = (ServerRow)this.gvServers.GetRow(handle);
        if (row.ServerInfo == null)
          continue;
        if (sb.Length > 0)
          sb.AppendLine();
        sb.Append($"steam://connect/{row.ServerInfo.Address}\n");
      }

      if (sb.Length > 0)
        Clipboard.SetText(sb.ToString());
    }
    #endregion

    #region miPasteAddress_ItemClick
    private void miPasteAddress_ItemClick(object sender, ItemClickEventArgs e)
    {
      if (this.viewModel.Source != TabViewModel.SourceType.CustomList)
        return;
      this.gvServers.BeginDataUpdate();
      
      try
      {
        var text = Clipboard.GetText();
        var regex = new System.Text.RegularExpressions.Regex(@"^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}:[0-9]{1,5}$");
        foreach (var line in text.Split('\n', '\r', ' ', ',', ';','|'))
        {
          var addr = line.Trim();
          if (regex.IsMatch(addr))
          {
            var endpoint = Ip4Utils.ParseEndpoint(addr);
            var row = this.viewModel.Servers.FirstOrDefault(r => r.EndPoint.Equals(endpoint));
            if (row == null)
            {
              row = new ServerRow(endpoint, unknownGame);
              row.SetModified();
              this.viewModel.Servers.Add(row);
              this.queryLogic.RefreshSingleServer(row);
            }
          }
        }
      }
      catch
      {
      }
      this.gvServers.EndDataUpdate();
    }
    #endregion

    #region riFavServer_EditValueChanged
    private void riFavServer_EditValueChanged(object sender, EventArgs e)
    {
      this.gvServers.PostEditor();
    }
    #endregion

    #region miFavServer_ItemClick
    private void miFavServer_ItemClick(object sender, ItemClickEventArgs e)
    {
      foreach (var handle in this.gvServers.GetSelectedRows())
      {
        var row = (ServerRow)this.gvServers.GetRow(handle);
        this.favServers[row.EndPoint] = row.Name;
      }
      this.UpdateViews();
    }
    #endregion

    #region miUnfavServer_ItemClick
    private void miUnfavServer_ItemClick(object sender, ItemClickEventArgs e)
    {
      foreach (var handle in this.gvServers.GetSelectedRows())
      {
        var row = (ServerRow)this.gvServers.GetRow(handle);
        this.favServers.Remove(row.EndPoint);
      }
      this.UpdateViews();
    }
    #endregion

    #region btnPasteAddresses_Click
    private void btnPasteAddresses_Click(object sender, EventArgs e)
    {
      this.miPasteAddress_ItemClick(null, null);
    }
    #endregion

    // Players grid

    #region gvPlayers_CustomRowFilter
    private void gvPlayers_CustomRowFilter(object sender, RowFilterEventArgs e)
    {
      if (!this.cbHideGhosts.Checked)
        return;
      // hide players based on GameExtension.IsValidPlayer() - e.g. ghost connections
      var server = (ServerRow)this.gvServers.GetFocusedRow();
      if (server == null) return;
      var players = server.Players;
      if (players == null) return;
      if (e.ListSourceRow >= players.Count) return;
      var player = players[e.ListSourceRow];
      if (!server.GameExtension.IsValidPlayer(server, player))
      {
        e.Visible = false;
        e.Handled = true;
      }
    }
    #endregion

    #region gvPlayers_CustomUnboundColumnData
    private void gvPlayers_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
    {
      if (!e.IsGetData) return;
      var server = (ServerRow) this.gvServers.GetFocusedRow();
      if (server == null) return;
      var player = (Player) e.Row;
      if (e.Column.FieldName == "CleanName")
        e.Value = server.GameExtension.GetCleanPlayerName(player);
      else if (player != null)
        e.Value = server.GameExtension.GetPlayerCellValue(server, player, e.Column.FieldName);
    }
    #endregion

    #region gvPlayers_MouseDown
    private void gvPlayers_MouseDown(object sender, MouseEventArgs e)
    {
      var hit = this.gvPlayers.CalcHitInfo(e.Location);
      if (hit.InDataRow && e.Button == MouseButtons.Right)
      {
        this.gcPlayers.Focus();
        this.gvPlayers.FocusedRowHandle = hit.RowHandle;

        // clear old menu items
        var oldItemList = new List<BarItem>();
        foreach (BarItemLink oldLink in this.menuPlayers.ItemLinks)
          oldItemList.Add(oldLink.Item);
        foreach (var oldItem in oldItemList)
        {
          this.barManager1.Items.Remove(oldItem);
          oldItem.Dispose();
        }
        this.menuPlayers.ClearLinks();

        // add new menu items
        var items = GetPlayerListContextMenuItems();
        foreach (var item in items)
        {
          var menuItem = new BarButtonItem(this.barManager1, item.Text);
          if (item.IsDefaultAction)
            menuItem.Appearance.Font = new Font(menuItem.Appearance.GetFont(), FontStyle.Bold);
          var safeItemRef = item;
          menuItem.ItemClick += (o, args) => safeItemRef.Handler();
          this.menuPlayers.AddItem(menuItem);
        }

        this.menuPlayers.ShowPopup(this.gcPlayers.PointToScreen(e.Location));
      }
    }
    #endregion

    #region gvPlayers_DoubleClick
    private void gvPlayers_DoubleClick(object sender, EventArgs e)
    {
      var menu = this.GetPlayerListContextMenuItems();
      foreach (var item in menu)
      {
        if (item.IsDefaultAction)
        {
          item.Handler();
          return;
        }
      }
    }
    #endregion

    #region gvPlayers_RowCellStyle
    private void gvPlayers_RowCellStyle(object sender, RowCellStyleEventArgs e)
    {
      if (this.cbHideGhosts.Checked)
        return;
      var player = (Player)this.gvPlayers.GetRow(e.RowHandle);
      var server = (ServerRow)this.gvServers.GetFocusedRow();
      if (server != null && !server.GameExtension.IsValidPlayer(server, player))
        e.Appearance.ForeColor = Color.Silver;
    }
    #endregion

    // Details grid

    #region gvDetails_MouseDown
    private void gvDetails_MouseDown(object sender, MouseEventArgs e)
    {
      var hit = this.gvDetails.CalcHitInfo(e.Location);
      if (hit.InDataRow && e.Button == MouseButtons.Right)
      {
        this.gcDetails.Focus();
        this.gvDetails.FocusedRowHandle = hit.RowHandle;
        this.menuDetails.ShowPopup(this.gcDetails.PointToScreen(e.Location));
      }
    }
    #endregion

    #region miAddDetailColumnString_ItemClick
    private void miAddDetailColumnString_ItemClick(object sender, ItemClickEventArgs e)
    {
      AddColumnForDetailToServerGrid();
    }
    #endregion

    // Rules grid

    #region gvRules_CustomColumnDisplayText
    private void gvRules_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
    {
      var server = (ServerRow)this.gvServers.GetFocusedRow();
      var text = server?.GameExtension?.GetPrettyNameForRule(server, e.Value as string);
      if (text != null)
        e.DisplayText = text;
    }
    #endregion

    #region gvRules_MouseDown
    private void gvRules_MouseDown(object sender, MouseEventArgs e)
    {
      var hit = this.gvRules.CalcHitInfo(e.Location);
      if (hit.InDataRow && e.Button == MouseButtons.Right)
      {
        this.gcRules.Focus();
        this.gvRules.FocusedRowHandle = hit.RowHandle;
        this.menuRules.ShowPopup(this.gcRules.PointToScreen(e.Location));
      }
    }
    #endregion

    #region miAddRulesColumnString_ItemClick
    private void miAddRulesColumnString_ItemClick(object sender, ItemClickEventArgs e)
    {
      AddColumnForRuleToServerGrid("", UnboundColumnType.String);
    }
    #endregion

    #region miAddRulesColumnNumeric_ItemClick
    private void miAddRulesColumnNumeric_ItemClick(object sender, ItemClickEventArgs e)
    {
      AddColumnForRuleToServerGrid(CustomRuleColumnPrefix, UnboundColumnType.Decimal);
    }
    #endregion

    // remote console

    #region txtRconCommand_KeyDown
    private void txtRconCommand_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyData == Keys.Enter || e.KeyData == Keys.Return)
      {
        int port;
        if (!int.TryParse(this.txtRconPort.Text, out port))
          return;
        var row = (ServerRow)this.gvServers.GetFocusedRow();
        if (row != null)
        {
          this.SendRconCommand(row, port, this.txtRconPassword.Text, this.txtRconCommand.Text);
          this.txtRconCommand.Text = "";
        }
        e.Handled = true;
      }
    }
    #endregion

    #region SendRconCommand()
    private void SendRconCommand(ServerRow row, int port, string pass, string text)
    {
      row.GameExtension?.Rcon(row, port, pass, text);
    }

    #endregion

    // tabs

    #region tabControl_SelectedPageChanging
    private void tabControl_SelectedPageChanging(object sender, TabPageChangingEventArgs e)
    {
      if (e.Page == this.tabAdd)
      {
        e.Cancel = true;
        this.menuAddTab.ShowPopup(MousePosition);
        return;
      }

      this.UpdateViewModel();
      this.geoIpClient.CancelPendingRequests();
    }
    #endregion

    #region CloneTab()
    private void CloneTab(XtraTabPage source)
    {
      var page = new XtraTabPage();
      page.Text = source.Text + " #2";
      page.ShowCloseButton = DefaultBoolean.True;
      var vm = new TabViewModel();
      vm.AssignFrom(this.viewModel);
      page.Tag = vm;
      page.ImageIndex = vm.ImageIndex;
      var idx = this.tabControl.TabPages.IndexOf(source);
      this.tabControl.TabPages.Insert(idx + 1, page);
      this.tabControl.SelectedTabPage = page;
    }
    #endregion

    #region tabControl_SelectedPageChanged
    private void tabControl_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
    {
      if (this.ignoreUiEvents > 0) return;
      this.SetViewModel((TabViewModel)e.Page.Tag);

      if (this.viewModel.Servers == null)
        this.ReloadServerList();
      else if (this.viewModel.Source != TabViewModel.SourceType.MasterServer)
        this.RefreshServerInfo();
    }
    #endregion

    #region tabControl_CloseButtonClick
    private void tabControl_CloseButtonClick(object sender, EventArgs e)
    {
      var args = e as ClosePageButtonEventArgs;
      if (args == null || this.tabControl.TabPages.Count <= PredefinedTabCount)
        return;

      var idx = this.tabControl.TabPages.IndexOf((XtraTabPage)args.Page);
      if (args.Page == tabControl.SelectedTabPage)
      {
        if (idx > 0)
          this.tabControl.SelectedTabPageIndex = idx - 1;
      }
      this.tabControl.TabPages.RemoveAt(idx);
    }
    #endregion

    #region tabControl_MouseDown
    private void tabControl_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        var page = tabControl.CalcHitInfo(e.Location).Page;
        if (page != this.tabAdd)
          this.dragPage = page;
      }
      else if (e.Button == MouseButtons.Right)
      {
        Point pt = e.Location;
        XtraTabHitInfo info = this.tabControl.CalcHitInfo(pt);
        if (info.HitTest == XtraTabHitTest.PageHeader)
        {
          if (info.Page == this.tabAdd)
            return;
          tabControl.SelectedTabPage = info.Page;
          this.UpdateTabContextMenu(info.Page);
          menuTab.ShowPopup(tabControl.PointToScreen(pt));
        }
      }
    }
    #endregion

    #region tabControl_MouseMove
    private void tabControl_MouseMove(object sender, MouseEventArgs e)
    {
      if (dragPage == null)
        return;
      this.tabControl.Cursor = Cursors.VSplit;
      var dropPage = tabControl.CalcHitInfo(e.Location).Page;
      if (dragPage == dropPage)
        return;
      var dragPageIndex = tabControl.TabPages.IndexOf(dragPage);
      var dropPageIndex = tabControl.TabPages.IndexOf(dropPage);
      if (dropPageIndex < 0)
        return;
      var newIndex = dragPageIndex > dropPageIndex ? dropPageIndex : dropPageIndex + 1;
      if (dropPage == tabAdd)
        newIndex = dropPageIndex;
      this.tabControl.TabPages.Move(newIndex, dragPage);
    }
    #endregion

    #region tabControl_MouseUp
    private void tabControl_MouseUp(object sender, MouseEventArgs e)
    {
      this.tabControl.Cursor = Cursors.Default;
      dragPage = null;
    }
    #endregion

    #region miCloneTab_ItemClick
    private void miCloneTab_ItemClick(object sender, ItemClickEventArgs e)
    {
      this.CloneTab(this.tabControl.SelectedTabPage);
    }
    #endregion

    #region miCreateSnapshot_ItemClick
    private void miCreateSnapshot_ItemClick(object sender, ItemClickEventArgs e)
    {
      var srcPage = this.tabControl.SelectedTabPage;

      var vm = new TabViewModel();
      vm.AssignFrom(this.viewModel);
      vm.Source = TabViewModel.SourceType.CustomList;
      vm.GridFilter = null;

      var page = new XtraTabPage();
      page.Text = srcPage.Text + " #2";
      page.ShowCloseButton = DefaultBoolean.True;
      page.Tag = vm;
      page.ImageIndex = vm.ImageIndex;

      vm.Servers = new List<ServerRow>();
      for(int i=0, c= this.gvServers.RowCount; i<c; i++)
        vm.Servers.Add((ServerRow)this.gvServers.GetRow(i));

      this.tabControl.TabPages.Insert(this.tabControl.TabPages.Count - 1, page);
      this.tabControl.SelectedTabPage = page;
    }
    #endregion

    #region miAdd*Tab_ItemClick
    private void miAddMasterServerTab_ItemClick(object sender, ItemClickEventArgs e)
    {
      this.AddNewTab("New Master Query", TabViewModel.SourceType.MasterServer);
    }

    private void miAddCustomServerTab_ItemClick(object sender, ItemClickEventArgs e)
    {
      this.AddNewTab("New Custom List", TabViewModel.SourceType.CustomList);
    }

    private void miNewFavoritesTab_ItemClick(object sender, ItemClickEventArgs e)
    {
      this.AddNewTab("New Favorites", TabViewModel.SourceType.Favorites);
    }
    #endregion
  }
}