using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
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

    private const string Version = "1.10";
    private const string DevExpressVersion = "v15.1";
    private string brandingUrl;
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
    private List<ServerRow> servers;
    private ServerRow lastSelectedServer;
    private ServerRow currentServer;

    #region ctor()
    public ServerBrowserForm()
    {
      // change font before InitializeComponent() for correct auto-scaling
      if (Properties.Settings.Default.FontSizeDelta != 0)
      {
        AppearanceObject.DefaultFont = new Font(
          Properties.Settings.Default.FontName,
          AppearanceObject.DefaultFont.Size + Properties.Settings.Default.FontSizeDelta);
      }

      InitializeComponent();
      this.favGameRadioButtons = new[] {rbFavGame1, rbFavGame2, rbFavGame3};
      // ReSharper disable AssignNullToNotNullAttribute
      this.Icon = new Icon(this.GetType().Assembly.GetManifestResourceStream("ServerBrowser.rocket.ico"));
      // ReSharper restore AssignNullToNotNullAttribute

      // make the server list panel fill the remaining space (this can't be done in the Forms Designer)
      this.panelServerList.Parent.Controls.Remove(this.panelServerList);
      this.panelServerList.Dock = DockingStyle.Fill;
      this.Controls.Add(this.panelServerList);

      UserLookAndFeel.Default.StyleChanged += LookAndFeel_StyleChanged;

      base.Text += " " + Version;

      this.InitGameInfoExtenders();
      this.queryLogic = new ServerQueryLogic(this.extenders);
      this.queryLogic.UpdateStatus += (s, e) => this.BeginInvoke((Action)(() => { this.txtStatus.Text = e.Text; }));
      this.queryLogic.ReloadServerListComplete += (s,e) => this.BeginInvoke((Action)(() => { queryLogic_ReloadServerListComplete(e.Rows); }));
      this.queryLogic.RefreshSingleServerComplete += (s, e) => this.BeginInvoke((Action) (() => { queryLogic_RefreshSingleServerComplete(e); }));    
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

      ++this.ignoreUiEvents;

      this.FillGameCombo();
      this.FillSteamServerRegions();
      this.InitGameInfoExtenders();
      this.InitBranding();
      this.LoadBonusSkins();
      this.InitAppSettings();
      LookAndFeel_StyleChanged(null, null);
      --this.ignoreUiEvents;
      this.ReloadServerList();
    }
    #endregion

    #region OnFormClosing()
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
      Properties.Settings.Default.InitialGameID = (int) this.SteamAppID;
      Properties.Settings.Default.MasterServer = this.comboMasterServer.Text;
      Properties.Settings.Default.RefreshInterval = Convert.ToInt32(this.spinRefreshInterval.EditValue);
      Properties.Settings.Default.RefreshSelected = this.cbRefreshSelectedServer.Checked;
      Properties.Settings.Default.ShowOptions = this.cbAdvancedOptions.Checked;
      Properties.Settings.Default.GetEmptyServers = this.cbGetEmpty.Checked;
      Properties.Settings.Default.GetFullServers = this.cbGetFull.Checked;
      Properties.Settings.Default.ShowAddressMode = this.showAddressMode;
      Properties.Settings.Default.TagsInclude = this.txtTagInclude.Text;
      Properties.Settings.Default.TagsExclude = this.txtTagExclude.Text;
      Properties.Settings.Default.Save();

      this.queryLogic.Cancel();
      base.OnFormClosing(e);
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
    private void InitBranding()
    {
      int topHeight = Properties.Settings.Default.LogoHeight;
      if (topHeight == 0)
        topHeight = this.panelTop.Height;
      else
        this.panelTop.Height = Properties.Settings.Default.LogoHeight;

      if (Properties.Settings.Default.Branding == "phgp")
      {
        this.brandingUrl = "http://www.phgp.tv/";

        var img = new Bitmap(Properties.Resources.phgp);
        this.picLogo.Image = img;
        this.picLogo.Width = img.Width * topHeight / img.Height;
        this.picLogo.Visible = true;

        Properties.Settings.Default.InitialGameID = (int)Game.Reflex;

        UserLookAndFeel.Default.SkinName = "Visual Studio 2013 Dark";
        this.btnSkin.Visible = false;
        return;
      }

      UserLookAndFeel.Default.SkinName = Properties.Settings.Default.Skin;
    }
    #endregion

    #region LoadBonusSkins()

    /// <summary>
    /// Load the BonusSkin DLL dynamically so that the application can be executed without it being present
    /// </summary>
    private bool LoadBonusSkins()
    {
      var dllPath = this.BonusSkinDllPath;
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

        this.SetBonusSkinLinkState(false);
        return true;
      }
      catch
      {
        // it's a pity, but life goes on
        try
        {
          File.Delete(dllPath);
          this.SetBonusSkinLinkState(true);
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

    #region SetBonusSkinLinkState()
    private void SetBonusSkinLinkState(bool visible)
    {
      if (visible)
      {
        this.linkDownloadSkins.Text = "Download <href>Bonus Skins</href>";
        this.linkDownloadSkins.Visible = true;
        this.linkDownloadSkins.Enabled = true;
      }
      else
        this.linkDownloadSkins.Visible = false;
    }
    #endregion

    #region InitAppSettings()
    private void InitAppSettings()
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
        int id;
        if (i < favGameIds.Length && int.TryParse(favGameIds[i], out id))
        {
          radio.Text = this.GetGameCaption((Game) id);
          radio.Tag = (Game) id;
          if (prevRadio != null)
            radio.Left = prevRadio.Right + 20;
          prevRadio = radio;
        }
        else
        {
          radio.Visible = false;
          radio.Tag = (Game)0;
        }
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
    private Game SteamAppID
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
          rbFav.Checked = (Game) rbFav.Tag == this.steamAppId;
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
    private void ReloadServerList()
    {
      if (this.ignoreUiEvents > 0)
        return;

      var appId = this.SteamAppID;
      if (appId == 0) // this would result in a truncated list of all games
        return;

      this.txtStatus.Text = "Requesting server list from master server...";

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
      this.gameExtension.CustomizeServerFilter(filter);
      var source = new MasterServerClient(this.MasterServerEndpoint);
      //var source = new ServerListFromUrl(new Uri(Environment.CurrentDirectory + "\\serverlist.txt"));
      queryLogic.ReloadServerList(source, 500, MaxResults, region, filter);
    }
    #endregion

    #region ParseTags()
    private string ParseTags(string text)
    {
      return text.Replace("\\", "");
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
      if (row.ServerInfo == null)
        return;

      string password = null;
      if (row.ServerInfo.IsPrivate)
      {
        if (this.passwordForm.ShowDialog(this) == DialogResult.Cancel)
          return;
        password = this.passwordForm.Password;
      }

      this.Cursor = Cursors.WaitCursor;
      row.GameExtension.Connect(row, password, spectate);
      this.Cursor = Cursors.Default;
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


    // general components

    #region queryLogic_ReloadServerListComplete()
    private void queryLogic_ReloadServerListComplete(List<ServerRow> rows)
    {
      this.txtStatus.Text = "Update of " + rows.Count + " servers complete";

      this.timerUpdateServerList_Tick(null, null);
      if (this.gvServers.RowCount > 0 && this.cbAlert.Checked)
      {
        this.cbAlert.Checked = false;
        SystemSounds.Asterisk.Play();
        this.alertControl1.Show(this, "Steam Server Browser", "Found " + this.gvServers.RowCount + " server(s) matching your criteria.", 
          this.sharedImageCollection1.ImageSource.Images[2]);
      }
    }
    #endregion

    #region queryLogic_RefreshSingleServerComplete()
    private void queryLogic_RefreshSingleServerComplete(ServerEventArgs e)
    {
      if (this.gvServers.GetFocusedRow() == e.Server)
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
        color = this.panelOptions.ForeColor;
      this.linkFilter1.Appearance.LinkColor = this.linkFilter1.Appearance.PressedColor = color;
      this.linkDownloadSkins.Appearance.LinkColor = this.linkDownloadSkins.Appearance.PressedColor = color;

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
    }
    #endregion

    // option elements

    #region picLogo_Click
    private void picLogo_Click(object sender, EventArgs e)
    {
      try
      {
        if (!string.IsNullOrEmpty(this.brandingUrl))
          Process.Start(this.brandingUrl);
      }
      catch { /* just ignore it */ }
    }
    #endregion

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
        var ids = Properties.Settings.Default.FavGameIDs.Split(',');
        ids[idx] = ((int)this.SteamAppID).ToString();
        Properties.Settings.Default.FavGameIDs = string.Join(",", ids);
        this.InitFavGameRadioButtons();
      }
      else
      {
        // update server list for selected favorite
        this.SteamAppID = (Game)radio.Tag;
        this.ReloadServerList();
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

      ReloadServerList();
    }
    #endregion

    #region linkFilter_HyperlinkClick
    private void linkFilter_HyperlinkClick(object sender, HyperlinkClickEventArgs e)
    {
      this.gvServers.ShowFilterEditor(this.colHumanPlayers);
    }
    #endregion

    #region txtTag_ButtonClick
    private void txtTag_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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
    private void txtGameServer_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
    {
      try
      {
        string[] parts = this.txtGameServer.Text.Split(':');

        var addr = Dns.GetHostAddresses(parts[0]);
        if (addr.Length == 0) return;
        var endpoint = new IPEndPoint(addr[0], parts.Length > 1 ? int.Parse(parts[1]) : 25785);
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
      this.gvServers.ActiveFilterString = "[ServerInfo.Players]>=1";
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
      using (var dlg = new SkinPicker())
        dlg.ShowDialog(this);
    }
    #endregion

    #region linkDownloadSkins_HyperlinkClick
    private void linkDownloadSkins_HyperlinkClick(object sender, HyperlinkClickEventArgs e)
    {
      this.linkDownloadSkins.Text = "<href></href>Downloading Bonus Skins...";
      this.linkDownloadSkins.Enabled = false;

      var client = new WebClient();
      client.Proxy = null;
      client.DownloadFileCompleted += delegate(object o, AsyncCompletedEventArgs args)
      {
        ((WebClient)o).Dispose();
        if (args.Error != null)
        {
          XtraMessageBox.Show(this, "Failed to download skin pack:\n" + args.Error, "Skin Pack", MessageBoxButtons.OK, MessageBoxIcon.Error);
          this.SetBonusSkinLinkState(true);
          return;
        }
        if (this.LoadBonusSkins())
          this.btnSkin_Click(null, null);
      };

      var dllPath = this.BonusSkinDllPath;
      client.DownloadFileAsync(new Uri("https://github.com/PredatH0r/SteamServerBrowser/raw/master/ServerBrowser/DLL/" + Path.GetFileName(dllPath)), dllPath);      
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
      if (!this.queryLogic.GetAndResetDataModified())
        return;

      this.timerUpdateServerList.Stop();
      try
      {
        this.servers = this.queryLogic.Servers;
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

        var row = (ServerRow) this.gvServers.GetFocusedRow();
        if (row != null && row.GetAndResetIsModified())
          this.UpdateGridDataSources();
      }
      finally
      {
        this.timerUpdateServerList.Start();
      }
    }
    #endregion

    #region gvServers_CustomUnboundColumnData
    private void gvServers_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
    {
      if (!e.IsGetData) return;
      var row = (ServerRow) e.Row;
      if (e.Column == this.colEndPoint)
        e.Value = GetServerAddress(row);
      else if (e.Column == this.colName)
        e.Value = row.ServerInfo == null ? (showAddressMode == 0 ? GetServerAddress(row) : null) : row.ServerInfo.Name;
      else
        e.Value = row.GetExtenderCellValue(e.Column.FieldName);
    }
    #endregion

    #region gvServers_FocusedRowChanged
    private void gvServers_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
    {
      try
      {
        if (this.ignoreUiEvents > 0) return;

        var row = (ServerRow)this.gvServers.GetFocusedRow();
        this.lastSelectedServer = row;
        if (row != this.currentServer)
          this.UpdateGridDataSources();

        if (row != null && this.cbRefreshSelectedServer.Checked && !this.queryLogic.IsUpdating)
          this.queryLogic.RefreshSingleServer(row);
      }
      catch (Exception ex)
      {
        this.txtStatus.Text = ex.Message;
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
        this.txtStatus.Text = ex.Message;
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

    // Players grid

    #region gvPlayers_CustomUnboundColumnData
    private void gvPlayers_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
    {
      if (!e.IsGetData) return;
      var server = (ServerRow)this.gvServers.GetFocusedRow();
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
  }
}