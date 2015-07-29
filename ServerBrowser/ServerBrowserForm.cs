using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
using QueryMaster;

namespace ServerBrowser
{
  public partial class ServerBrowserForm : XtraForm
  {
    #region Steam Regions
    private static readonly object[] steamRegions = 
    {
      "Worldwide", QueryMaster.Region.Rest_of_the_world,
      "Africa", QueryMaster.Region.Africa,
      "Asia", QueryMaster.Region.Asia,
      "Australia", QueryMaster.Region.Australia,
      "Asia", QueryMaster.Region.Asia,
      "Europe", QueryMaster.Region.Europe,
      "Middle East", QueryMaster.Region.Middle_East,
      "South America", QueryMaster.Region.South_America,
      "US East Coast", QueryMaster.Region.US_East_coast,
      "US West Coast", QueryMaster.Region.US_West_coast
    };
    #endregion

    private const string DevExpressVersion = "v15.1";
    private const string CustomNumericRuleColumnPrefix = "castRule.";
    private volatile Game steamAppId;
    private readonly GameExtensionPool extenders = new GameExtensionPool();
    private GameExtension gameExtension;
    private int ignoreUiEvents;
    private readonly CheckEdit[] favGameRadioButtons;
    private readonly List<Game> gameIdForComboBoxIndex = new List<Game>();
    private const int MaxResults = 500;
    private readonly PasswordForm passwordForm = new PasswordForm();
    private int showAddressMode;
    private readonly ServerQueryLogic queryLogic;
    private readonly GeoIpClient geoIpClient = new GeoIpClient();
    private List<ServerRow> servers;
    private ServerRow lastSelectedServer;
    private ServerRow currentServer;
    private IServerSource serverSource;
    private int geoIpModified;
    private readonly HashSet<IPEndPoint> favServers = new HashSet<IPEndPoint>();

    #region ctor()
    public ServerBrowserForm()
    {
      InitializeComponent();

      this.favGameRadioButtons = new[] {rbFavGame1, rbFavGame2, rbFavGame3, rbFavGame4 };

      this.InitGameInfoExtenders();
      this.queryLogic = new ServerQueryLogic(this.extenders);

      if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
        return;

      this.queryLogic.UpdateStatus += (s, e) => this.BeginInvoke((Action)(() => queryLogic_SetStatusMessage(s, e)));
      this.queryLogic.ReloadServerListComplete += (s, e) => this.BeginInvoke((Action)(() => { queryLogic_ReloadServerListComplete(e.Rows); }));
      this.queryLogic.RefreshSingleServerComplete += (s, e) => this.BeginInvoke((Action)(() => { queryLogic_RefreshSingleServerComplete(e); }));

      // make the server list panel fill the remaining space (this can't be done in the Forms Designer)
      this.panelServerList.Parent.Controls.Remove(this.panelServerList);
      this.panelServerList.Dock = DockingStyle.Fill;
      this.Controls.Add(this.panelServerList);
      UserLookAndFeel.Default.StyleChanged += LookAndFeel_StyleChanged;
    }
    #endregion

    #region InitGameInfoExtenders()

    private void InitGameInfoExtenders()
    {
      extenders.Add(Game.Toxikk, new Toxikk());
      extenders.Add(Game.Reflex, new Reflex());
      extenders.Add(Game.QuakeLive_Testing, new QuakeLive());
      extenders.Add(Game.CounterStrike_Global_Offensive, new CounterStrikeGO());
    }

    #endregion

    #region OnLoad()
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
        return;

      ++this.ignoreUiEvents;

      this.FillCountryFlags();
      this.FillGameCombo();
      this.FillSteamServerRegions();
      this.InitGameInfoExtenders();
      this.InitBranding();
      LoadBonusSkins(this.BonusSkinDllPath);
      this.InitAppSettings();
      LookAndFeel_StyleChanged(null, null);
      --this.ignoreUiEvents;
      this.serverSource = this.CreateServerSource();

      this.ReloadServerList();      
    }
    #endregion

    #region OnFormClosing()
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
      this.SaveAppSettings();
      this.queryLogic.Cancel();
      base.OnFormClosing(e);
    }
    #endregion

    #region SaveAppSettings()
    protected virtual void SaveAppSettings()
    {
      Properties.Settings.Default.MasterServer = this.comboMasterServer.Text;
      Properties.Settings.Default.InitialGameID = (int) this.SteamAppID;
      Properties.Settings.Default.FilterMod = this.txtMod.Text;
      Properties.Settings.Default.FilterMap = this.txtMap.Text;
      Properties.Settings.Default.TagsInclude = this.txtTagInclude.Text;
      Properties.Settings.Default.TagsExclude = this.txtTagExclude.Text;
      Properties.Settings.Default.GetEmptyServers = this.cbGetEmpty.Checked;
      Properties.Settings.Default.GetFullServers = this.cbGetFull.Checked;

      Properties.Settings.Default.Skin = DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName;      
      Properties.Settings.Default.ShowOptions = this.cbAdvancedOptions.Checked;
      Properties.Settings.Default.ShowAddressMode = this.showAddressMode;
      Properties.Settings.Default.RefreshInterval = Convert.ToInt32(this.spinRefreshInterval.EditValue);
      Properties.Settings.Default.RefreshSelected = this.cbRefreshSelectedServer.Checked;

      var sb = new StringBuilder();
      foreach (var fav in this.favServers)
      {
        if (sb.Length > 0) sb.Append(",");
        sb.Append(fav);
      }
      Properties.Settings.Default.FavServers = sb.ToString();
      Properties.Settings.Default.KeepFavServersOnTop = this.cbFavServersOnTop.Checked;

      Properties.Settings.Default.Save();
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

    #region FillSteamServerRegions()
    private void FillSteamServerRegions()
    {
      for (int i = 0; i < steamRegions.Length; i += 2)
        this.comboRegion.Properties.Items.Add(steamRegions[i]);
      this.comboRegion.SelectedIndex = 0;
      this.comboRegion.Properties.DropDownRows = steamRegions.Length / 2;
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
        if (method != null)
          method.Invoke(null, null);
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

    #region InitAppSettings()
    protected virtual void InitAppSettings()
    {
      InitFavGameRadioButtons();
      this.SteamAppID = (Game)Properties.Settings.Default.InitialGameID;

      // fill master server combobox
      var masterServers = Properties.Settings.Default.MasterServerList.Split(',');
      foreach (var master in masterServers)
        this.comboMasterServer.Properties.Items.Add(master);
      var info = Properties.Settings.Default.MasterServer;
      if (string.IsNullOrEmpty(info))
        info = "hl2master.steampowered.com:27011";
      this.comboMasterServer.Text = info;

      this.cbGetEmpty.Checked = Properties.Settings.Default.GetEmptyServers;
      this.cbGetFull.Checked = Properties.Settings.Default.GetFullServers;
      this.rbAddressHidden.Checked = Properties.Settings.Default.ShowAddressMode == 0;
      this.rbAddressQueryPort.Checked = Properties.Settings.Default.ShowAddressMode == 1;
      this.rbAddressGamePort.Checked = Properties.Settings.Default.ShowAddressMode == 2;
      this.spinRefreshInterval.EditValue = (decimal)Properties.Settings.Default.RefreshInterval;
      this.cbAdvancedOptions.Checked = Properties.Settings.Default.ShowOptions;
      this.cbRefreshSelectedServer.Checked = Properties.Settings.Default.RefreshSelected;
      this.txtTagInclude.Text = Properties.Settings.Default.TagsInclude;
      this.txtTagExclude.Text = Properties.Settings.Default.TagsExclude;
      this.txtMod.Text = Properties.Settings.Default.FilterMod;
      this.txtMap.Text = Properties.Settings.Default.FilterMap;

      // load favorite servers
      foreach (var server in Properties.Settings.Default.FavServers.Split(','))
      {
        if (server == "") continue;
        var parts = server.Split(':');
        this.favServers.Add(new IPEndPoint(IPAddress.Parse(parts[0]), int.Parse(parts[1])));
      }
      this.cbFavServersOnTop.Checked = Properties.Settings.Default.KeepFavServersOnTop;
    }
    #endregion

    #region CreateServerSource()
    protected virtual IServerSource CreateServerSource()
    {
      return new MasterServerClient(this.MasterServerEndpoint);
    }
    #endregion

    #region InitFavGameRadioButtons()
    private void InitFavGameRadioButtons()
    {
      var favGameIds = Properties.Settings.Default.FavGameIDs.Split(',');
      CheckEdit prevRadio = null;
      for (int i = 0; i < favGameRadioButtons.Length; i++)
      {
        var radio = favGameRadioButtons[i];
        int id = 0;
        if (i < favGameIds.Length && int.TryParse(favGameIds[i], out id))
          radio.Text = this.GetGameCaption((Game) id);
        else
          radio.Text = "Empty";
          
        radio.Tag = (Game) id;
        if (prevRadio != null)
          radio.Left = prevRadio.Right + 20;
        prevRadio = radio;
      }
    }

    #endregion

    #region GetGameCaption()
    private string GetGameCaption(Game game)
    {
      return game.ToString().Replace("_", " ");
    }
    #endregion
    
    #region SteamAppId
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Game SteamAppID
    {
      get { return this.steamAppId; }
      set
      {
        if (value == this.steamAppId)
          return;

        this.gcServers.DataSource = null;
        this.gcDetails.DataSource = null;
        this.gcPlayers.DataSource = null;
        this.gcRules.DataSource = null;
        this.lastSelectedServer = null;
        this.currentServer = null;

        this.steamAppId = value;
        this.InitGameExtension();
        
        // show/hide the Rules panel but don't bring it to the front
        var parentPanel = this.panelRules.SavedParent;
        var curTopmost = parentPanel == null ? -1 : parentPanel.ActiveChildIndex;
        this.panelPlayers.Visibility = this.gameExtension.SupportsPlayersQuery(null) ? DockVisibility.Visible : DockVisibility.Hidden;
        this.panelRules.Visibility = this.gameExtension.SupportsRulesQuery(null) ? DockVisibility.Visible : DockVisibility.Hidden;
        parentPanel = this.panelRules.ParentPanel;
        if (parentPanel != null && curTopmost >= 0)
          parentPanel.ActiveChildIndex = curTopmost;

        ++this.ignoreUiEvents;
        foreach (var rbFav in this.favGameRadioButtons)
          rbFav.Checked = rbFav.Tag != null && (Game) rbFav.Tag == this.steamAppId;
        int index = this.gameIdForComboBoxIndex.IndexOf(value);
        if (index >= 0)
          this.comboGames.SelectedIndex = index;
        else
          this.comboGames.Text = ((int)value).ToString();
        --this.ignoreUiEvents;
      }
    }
    #endregion

    #region MasterServerEndpoint
    private IPEndPoint MasterServerEndpoint
    {
      get
      {
        string info = this.comboMasterServer.Text;
        var parts = info.Split(':');
        int port;
        if (parts.Length < 2 || !int.TryParse(parts[1], out port))
          port = 27011;
        var ips = Dns.GetHostAddresses(parts[0]);
        return ips.Length == 0 ? new IPEndPoint(IPAddress.None, 0) : new IPEndPoint(ips[0], port);
      }
    }
    #endregion

    #region InitGameExtension()
    private void InitGameExtension()
    {
      this.gameExtension = this.extenders.Get(this.SteamAppID);

      this.gvServers.BeginUpdate();
      this.ResetGridColumns(this.gvServers);
      this.gameExtension.CustomizeServerGridColumns(gvServers);
      this.gvServers.EndUpdate();

      this.gvPlayers.BeginUpdate();
      this.ResetGridColumns(this.gvPlayers);
      this.gameExtension.CustomizePlayerGridColumns(gvPlayers);
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

      var appId = this.SteamAppID;
      if (appId == 0) // this would result in a truncated list of all games
        return;

      this.SetStatusMessage("Requesting server list from master server...");

      var region = (QueryMaster.Region)steamRegions[this.comboRegion.SelectedIndex * 2 + 1];

      IpFilter filter = new IpFilter();
      filter.App = appId;
      filter.IsNotEmpty = !this.cbGetEmpty.Checked;
      filter.IsNotFull = !this.cbGetFull.Checked;
      filter.GameDirectory = this.txtMod.Text;
      filter.Map = this.txtMap.Text;
      filter.Sv_Tags = this.ParseTags(this.txtTagInclude.Text);
      if (this.txtTagExclude.Text != "")
      {
        filter.Nor = new IpFilter();
        filter.Nor.Sv_Tags = this.ParseTags(this.txtTagExclude.Text);
      }
      this.CustomizeFilter(filter);
      queryLogic.ReloadServerList(this.serverSource, 500, MaxResults, region, filter);
    }
    #endregion

    #region CustomizeFilter()
    protected virtual void CustomizeFilter(IpFilter filter)
    {
      this.gameExtension.CustomizeServerFilter(filter);      
    }
    #endregion

    #region ParseTags()
    private string ParseTags(string text)
    {
      return text.Replace("\\", "");
    }
    #endregion

    #region PreFilterServers()
    protected virtual List<ServerRow> PreFilterServers(List<ServerRow> allServers)
    {
      return allServers;
    }
    #endregion

    #region UpdateViews()
    protected void UpdateViews()
    {
      this.servers = this.PreFilterServers(this.queryLogic.Servers);
      ++ignoreUiEvents;
      this.gvServers.BeginDataUpdate();
      this.gcServers.DataSource = servers;
      this.gvServers.EndDataUpdate();

      if (this.lastSelectedServer != null)
      {
        int i = 0;
        foreach (var server in servers)
        {
          if (server.EndPoint.Equals(this.lastSelectedServer.EndPoint))
          {
            gvServers.FocusedRowHandle = gvServers.GetRowHandle(i);
            gvServers.MakeRowVisible(gvServers.FocusedRowHandle);
            break;
          }
          ++i;
        }
      }
      else if (this.gvServers.FocusedRowHandle > 0)
        this.gvServers.FocusedRowHandle = 0;

      --ignoreUiEvents;

      var row = (ServerRow)this.gvServers.GetFocusedRow();
      if (row != null && row.GetAndResetIsModified())
        this.UpdateGridDataSources();
    }
    #endregion

    #region UpdateGridDataSources()
    private void UpdateGridDataSources()
    {
      var row = (ServerRow)this.gvServers.GetFocusedRow();
      this.currentServer = row;

      this.gvDetails.BeginDataUpdate();
      if (row == null)
        this.gcDetails.DataSource = null;
      else
      {
        this.gcDetails.DataSource = EnumerateProps(
          row.ServerInfo,
          row.ServerInfo == null ? null : row.ServerInfo.Extra,
          row.ServerInfo == null ? null : row.ServerInfo.ShipInfo);
      }
      this.gvDetails.EndDataUpdate();

      this.gvPlayers.BeginDataUpdate();
      this.gcPlayers.DataSource = row == null ? null : row.Players;
      this.gvPlayers.EndDataUpdate();

      this.gvRules.BeginDataUpdate();
      this.gcRules.DataSource = row == null ? null : row.Rules;
      this.gvRules.EndDataUpdate();

      this.UpdateServerContextMenu();
    }
    #endregion

    #region EnumerateProps()
    private List<Tuple<string, object>> EnumerateProps(params object[] objects)
    {
      var result = new List<Tuple<string, object>>();
      foreach (var obj in objects)
      {
        if (obj == null) continue;
        var props = new List<PropertyInfo>(obj.GetType().GetProperties());
        props.Sort((a, b) => StringComparer.InvariantCultureIgnoreCase.Compare(a.Name, b.Name));
        foreach (var prop in props)
        {
          if (prop.Name != "Extra" && prop.Name != "Item" && prop.Name != "ShipInfo")
            result.Add(new Tuple<string, object>(prop.Name.ToLower(), prop.GetValue(obj, null)));
        }
      }
      return result;
    }
    #endregion

    #region GetServerAddress()
    private string GetServerAddress(ServerRow row)
    {
      return this.showAddressMode == 2 && row.ServerInfo != null && row.ServerInfo.Extra != null
        ? row.EndPoint.Address + ":" + row.ServerInfo.Extra.Port
        : row.EndPoint.ToString();
    }
    #endregion

    #region UpdateServerContextMenu()
    private void UpdateServerContextMenu()
    {
      var canSpec = this.currentServer != null && this.currentServer.GameExtension.SupportsConnectAsSpectator(this.currentServer);
      this.miConnectSpectator.Visibility = canSpec ? BarItemVisibility.Always : BarItemVisibility.Never;
    }
    #endregion

    #region ConnectToGameServer()
    private void ConnectToGameServer(ServerRow row, bool spectate)
    {
      if (row == null || row.ServerInfo == null)
        return;

      string password = null;
      if (row.ServerInfo.IsPrivate)
      {
        password = this.PromptForServerPassword(row);
        if (password == null)
          return;
      }

      this.Cursor = Cursors.WaitCursor;
      this.SetStatusMessage("Connecting to game server " + row.ServerInfo.Name + " ...");
      row.GameExtension.Connect(row, password, spectate);
      this.Cursor = Cursors.Default;
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
      menu.Add(new PlayerContextMenuItem("Copy Name to Clipboard", () => { Clipboard.SetText(player.Name); }));
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

    #region AddColumnForRuleToServerGrid()
    private void AddColumnForRuleToServerGrid(string prefix, UnboundColumnType unboundColumnType)
    {
      var rule = (Rule)this.gvRules.GetFocusedRow();
      var col = this.gvServers.Columns[prefix + rule.Name];
      if (col == null)
        this.gameExtension.AddColumn(this.gvServers, prefix + rule.Name, rule.Name, rule.Name, 70, this.gvServers.VisibleColumns.Count, unboundColumnType);
      else
      {
        XtraMessageBox.Show(string.Format("Rule {0} is already shown in column {1}.", rule.Name, col.Caption), this.Text,
          MessageBoxButtons.OK, MessageBoxIcon.Information);
        this.gvServers.FocusedColumn = col;
      }
    }
    #endregion

    #region LookupGeoIps()
    private void LookupGeoIps()
    {
      foreach (var server in this.servers)
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

    // general components

    #region queryLogic_SetStatusMessage
    protected virtual void queryLogic_SetStatusMessage(object sender, TextEventArgs e)
    {
      { this.SetStatusMessage(e.Text); }
    }
    #endregion

    #region queryLogic_ReloadServerListComplete()
    protected virtual void queryLogic_ReloadServerListComplete(List<ServerRow> rows)
    {
      this.UpdateViews();
      this.SetStatusMessage("Update of " + this.servers.Count + " servers complete");
      if (this.gvServers.RowCount > 0 && this.cbAlert.Checked)
      {
        this.cbAlert.Checked = false;
        SystemSounds.Asterisk.Play();
        this.alertControl1.Show(this, "Steam Server Browser", DateTime.Now.ToString("HH:mm:ss") + ": Found " + this.gvServers.RowCount + " server(s) matching your criteria.", 
          this.imageCollection.Images[2]);
      }

      this.LookupGeoIps();
    }
    #endregion

    #region queryLogic_RefreshSingleServerComplete()
    protected virtual void queryLogic_RefreshSingleServerComplete(ServerEventArgs e)
    {
      if (this.gvServers.GetFocusedRow() == e.Server)
      {
        this.gvServers.RefreshRow(this.gvServers.FocusedRowHandle);
        this.UpdateGridDataSources();
      }
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
        color = this.panelOptions.ForeColor;
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

    // option elements

    #region comboGames_SelectedIndexChanged
    private void comboGames_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.ignoreUiEvents > 0) return;
      var idx = this.comboGames.SelectedIndex;
      if (idx < 0)
        return;
      this.SteamAppID = this.gameIdForComboBoxIndex[idx];
      this.ReloadServerList();
    }
    #endregion

    #region rbFavGame_CheckedChanged
    private void rbFavGame_CheckedChanged(object sender, EventArgs e)
    {
      var radio = (CheckEdit) sender; 
      if (!radio.Checked) // ignore the "uncheck" event
        return;

      if (ModifierKeys == Keys.Control)
      {
        // redefine a favorite
        var idx = Array.IndexOf(this.favGameRadioButtons, radio);
        var ids = Properties.Settings.Default.FavGameIDs.Split(',').ToList();
        for (int i=ids.Count; i<=idx; i++)
          ids.Add("0");
        ids[idx] = ((int)this.SteamAppID).ToString();
        Properties.Settings.Default.FavGameIDs = string.Join(",", ids);
        this.InitFavGameRadioButtons();
      }
      else
      {
        // update server list for selected favorite
        var game = (Game) radio.Tag;
        if (game != 0)
        {
          this.SteamAppID = game;
          this.ReloadServerList();
        }
      }
    }
    #endregion

    #region comboRegion_SelectedIndexChanged
    private void comboRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.ReloadServerList();
    }
    #endregion

    #region btnQueryMaster_Click
    private void btnQueryMaster_Click(object sender, EventArgs e)
    {
      if (this.comboGames.SelectedIndex < 0)
      {
        int id;
        if (!int.TryParse(this.comboGames.Text, out id))
          return;
        this.SteamAppID = (Game) id;
      }

      this.timerReloadServers.Stop();
      ReloadServerList();
      this.timerReloadServers.Start();
    }
    #endregion

    #region btnQuickRefresh_Click
    private void btnQuickRefresh_Click(object sender, EventArgs e)
    {
      if (this.queryLogic.IsUpdating)
        return;
      this.timerReloadServers.Stop();
      this.queryLogic.RefreshAllServers(this.servers);
      this.timerReloadServers.Start();
    }
    #endregion

    #region linkFilter_HyperlinkClick
    private void linkFilter_HyperlinkClick(object sender, HyperlinkClickEventArgs e)
    {
      this.gvServers.ShowFilterEditor(this.colHumanPlayers);
    }
    #endregion

    #region txtTag_ButtonClick
    private void txtTag_ButtonClick(object sender, ButtonPressedEventArgs e)
    {
      ((TextEdit)sender).Text = "";
    }
    #endregion

    #region cbAdvancedOptions_CheckedChanged
    private void cbAdvancedOptions_CheckedChanged(object sender, EventArgs e)
    {
      this.panelAdvancedOptions.Visible = this.cbAdvancedOptions.Checked;
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
        foreach (var row in this.servers)
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
          this.servers.Add(serverRow);
          this.gvServers.EndDataUpdate();
          this.gvServers.FocusedRowHandle = this.gvServers.GetRowHandle(this.servers.Count - 1);
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

    #region cbAlert_CheckedChanged
    private void cbAlert_CheckedChanged(object sender, EventArgs e)
    {
      this.cbAlert.ImageIndex = this.cbAlert.Checked ? 2 : 5;
      if (!this.cbAlert.Checked || !string.IsNullOrEmpty(this.gvServers.ActiveFilterString))
        return;
      this.gvServers.ActiveFilterString = "[PlayerCount.RealPlayers]>=1";
    }
    #endregion

    #region spinRefreshInterval_EditValueChanged
    private void spinRefreshInterval_EditValueChanged(object sender, EventArgs e)
    {
      this.timerReloadServers.Stop();
      int mins = Convert.ToInt32(this.spinRefreshInterval.EditValue);
      if (mins > 0)
      {
        this.timerReloadServers.Interval = mins * 60000;
        this.timerReloadServers.Start();
      }
    }
    #endregion

    #region btnSkin_Click
    private void btnSkin_Click(object sender, EventArgs e)
    {
      using (var dlg = new SkinPicker(this.BonusSkinDllPath))
        dlg.ShowDialog(this);
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

    // Servers grid

    #region timerReloadServers_Tick
    private void timerReloadServers_Tick(object sender, EventArgs e)
    {
      this.ReloadServerList();
    }
    #endregion

    #region timerUpdateServerList_Tick
    private void timerUpdateServerList_Tick(object sender, EventArgs e)
    {
      int geoIpMod = Interlocked.Exchange(ref this.geoIpModified, 0);
      if (!this.queryLogic.GetAndResetDataModified() && geoIpMod == 0)
        return;

      this.timerUpdateServerList.Stop();
      try
      {
        this.UpdateViews();
      }
      finally
      {
        this.timerUpdateServerList.Start();
      }
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
            this.favServers.Add(server.EndPoint);
          else
            this.favServers.Remove(server.EndPoint);
        }
        return;
      }

      var row = (ServerRow) e.Row;
      if (e.Column == this.colFavServer)
        e.Value = this.favServers.Contains(row.EndPoint);
      else if (e.Column == this.colEndPoint)
        e.Value = GetServerAddress(row);
      else if (e.Column == this.colName)
        e.Value = row.ServerInfo == null ? (showAddressMode == 0 ? GetServerAddress(row) : null) : row.ServerInfo.Name.Trim();
      else if (e.Column.FieldName.StartsWith(CustomNumericRuleColumnPrefix))
      {
        var fieldName = e.Column.FieldName.Substring(CustomNumericRuleColumnPrefix.Length);
        e.Value = row.GetExtenderCellValue(fieldName);
        try { e.Value = Convert.ToDecimal(e.Value); }
        catch
        {
        }
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
        if (e.ListSourceRowIndex < 0 || e.ListSourceRowIndex >= this.servers.Count)
          return;
        var row = this.servers[e.ListSourceRowIndex];
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
          if (row != null && row.GeoInfo != null)
            e.Info = new ToolTipControlInfo(row.EndPoint + "-" + hit.Column.FieldName, row.GeoInfo.ToString());
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

        var row = (ServerRow)this.gvServers.GetFocusedRow();
        this.lastSelectedServer = row;
        if (row != this.currentServer)
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

    #region gvServers_ColumnFilterChanged
    private void gvServers_ColumnFilterChanged(object sender, EventArgs e)
    {
      this.UpdateGridDataSources();
    }
    #endregion

    #region gvServers_DoubleClick
    private void gvServers_DoubleClick(object sender, EventArgs e)
    {
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
      if (hit.InDataRow && e.Button == MouseButtons.Right)
      {
        this.gcServers.Focus();
        this.gvServers.FocusedRowHandle = hit.RowHandle;
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
    }
    #endregion

    #region miUpdateServerInfo_ItemClick
    private void miUpdateServerInfo_ItemClick(object sender, ItemClickEventArgs e)
    {
      this.queryLogic.RefreshSingleServer((ServerRow)this.gvServers.GetFocusedRow());
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

    #region miCopyAddress_ItemClick
    private void miCopyAddress_ItemClick(object sender, ItemClickEventArgs e)
    {
      var addr = this.GetServerAddress((ServerRow) this.gvServers.GetFocusedRow());
      Clipboard.SetText(addr);
    }
    #endregion

    #region riFavServer_EditValueChanged
    private void riFavServer_EditValueChanged(object sender, EventArgs e)
    {
      this.gvServers.PostEditor();
    }
    #endregion

    #region miFavServer_DownChanged
    private void miFavServer_DownChanged(object sender, ItemClickEventArgs e)
    {
      var row = (ServerRow)this.gvServers.GetFocusedRow();
      if (row == null) return;
      if (this.miFavServer.Down)
        this.favServers.Add(row.EndPoint);
      else
        this.favServers.Remove(row.EndPoint);
      this.gvServers.RefreshRow(this.gvServers.FocusedRowHandle);
    }
    #endregion

    // Players grid

    #region gvPlayers_CustomUnboundColumnData
    private void gvPlayers_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
    {
      if (!e.IsGetData) return;
      var server = (ServerRow) this.gvServers.GetFocusedRow();
      var player = (Player) e.Row;
      if (server != null && player != null)
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

    // Rules grid

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
      AddColumnForRuleToServerGrid(CustomNumericRuleColumnPrefix, UnboundColumnType.Decimal);
    }
    #endregion

    // main menu

    #region miReloadServers_ItemClick
    private void miReloadServers_ItemClick(object sender, ItemClickEventArgs e)
    {
      this.ReloadServerList();
    }
    #endregion

    // remote console

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

    private void SendRconCommand(ServerRow row, int port, string pass, string text)
    {
      if (row.GameExtension != null)
        row.GameExtension.Rcon(row, port, pass, text);
    }

  }
}