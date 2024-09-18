using DevExpress.XtraEditors;

namespace ServerBrowser
{
  partial class ServerBrowserForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      this.steam.Dispose();
      this.geoIpClient?.Dispose();
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerBrowserForm));
      DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
      DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
      DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
      DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
      DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
      DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
      DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
      DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
      DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
      DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
      DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
      DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject9 = new DevExpress.Utils.SerializableAppearanceObject();
      DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject10 = new DevExpress.Utils.SerializableAppearanceObject();
      DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject11 = new DevExpress.Utils.SerializableAppearanceObject();
      DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject12 = new DevExpress.Utils.SerializableAppearanceObject();
      this.riCheckEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
      this.gcDetails = new DevExpress.XtraGrid.GridControl();
      this.gvDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
      this.colKey = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colValue = new DevExpress.XtraGrid.Columns.GridColumn();
      this.gcPlayers = new DevExpress.XtraGrid.GridControl();
      this.dsPlayer = new System.Windows.Forms.BindingSource(this.components);
      this.gvPlayers = new DevExpress.XtraGrid.Views.Grid.GridView();
      this.colPlayerName = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colScore = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colTime = new DevExpress.XtraGrid.Columns.GridColumn();
      this.gcServers = new DevExpress.XtraGrid.GridControl();
      this.dsServers = new System.Windows.Forms.BindingSource(this.components);
      this.gvServers = new DevExpress.XtraGrid.Views.Grid.GridView();
      this.colLocation = new DevExpress.XtraGrid.Columns.GridColumn();
      this.riCountryFlagEdit = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
      this.imgFlags = new DevExpress.Utils.ImageCollection(this.components);
      this.colBuddyCount = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colFavServer = new DevExpress.XtraGrid.Columns.GridColumn();
      this.riFavServer = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
      this.imageCollection = new DevExpress.Utils.ImageCollection(this.components);
      this.colEndPoint = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colPrivate = new DevExpress.XtraGrid.Columns.GridColumn();
      this.riPrivate = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
      this.colDedicated = new DevExpress.XtraGrid.Columns.GridColumn();
      this.riDedicated = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
      this.colJoinStatus = new DevExpress.XtraGrid.Columns.GridColumn();
      this.riJoinStatus = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
      this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colTags = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colPlayerCount = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colHumanPlayers = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colBots = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colTotalPlayers = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colMaxPlayers = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colMap = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colPing = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
      this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
      this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
      this.panelRcon = new DevExpress.XtraBars.Docking.DockPanel();
      this.controlContainer3 = new DevExpress.XtraBars.Docking.ControlContainer();
      this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
      this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
      this.txtRconPort = new DevExpress.XtraEditors.ButtonEdit();
      this.barManager1 = new ServerBrowser.XBarManager(this.components);
      this.barMenu = new DevExpress.XtraBars.Bar();
      this.mnuView = new DevExpress.XtraBars.BarSubItem();
      this.miShowOptions = new DevExpress.XtraBars.BarButtonItem();
      this.miShowServerQuery = new DevExpress.XtraBars.BarButtonItem();
      this.miShowFilter = new DevExpress.XtraBars.BarButtonItem();
      this.miRestoreStandardLayout = new DevExpress.XtraBars.BarButtonItem();
      this.mnuGameOptions = new DevExpress.XtraBars.BarLinkContainerItem();
      this.mnuGeoIpImpl = new DevExpress.XtraBars.BarSubItem();
      this.miGeoLite2 = new DevExpress.XtraBars.BarButtonItem();
      this.miIpApiCom = new DevExpress.XtraBars.BarButtonItem();
      this.mnuTabs = new DevExpress.XtraBars.BarSubItem();
      this.miRenameTab = new DevExpress.XtraBars.BarButtonItem();
      this.miCloneTab = new DevExpress.XtraBars.BarButtonItem();
      this.miCreateSnapshot = new DevExpress.XtraBars.BarButtonItem();
      this.miAddMasterServerTab = new DevExpress.XtraBars.BarButtonItem();
      this.miAddCustomServerTab = new DevExpress.XtraBars.BarButtonItem();
      this.miNewFavoritesTab = new DevExpress.XtraBars.BarButtonItem();
      this.mnuUpdate = new DevExpress.XtraBars.BarSubItem();
      this.miFindServers = new DevExpress.XtraBars.BarButtonItem();
      this.miQuickRefresh = new DevExpress.XtraBars.BarButtonItem();
      this.miUpdateServerInfo = new DevExpress.XtraBars.BarButtonItem();
      this.miStopUpdate = new DevExpress.XtraBars.BarButtonItem();
      this.mnuServer = new DevExpress.XtraBars.BarSubItem();
      this.miConnect = new DevExpress.XtraBars.BarButtonItem();
      this.miConnectSpectator = new DevExpress.XtraBars.BarButtonItem();
      this.miCopyAddress = new DevExpress.XtraBars.BarButtonItem();
      this.miPasteAddress = new DevExpress.XtraBars.BarButtonItem();
      this.miFavServer = new DevExpress.XtraBars.BarButtonItem();
      this.miUnfavServer = new DevExpress.XtraBars.BarButtonItem();
      this.miDelete = new DevExpress.XtraBars.BarButtonItem();
      this.miFindPlayer = new DevExpress.XtraBars.BarEditItem();
      this.riFindPlayer = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
      this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
      this.miAboutGithub = new DevExpress.XtraBars.BarButtonItem();
      this.miAboutSteamWorkshop = new DevExpress.XtraBars.BarButtonItem();
      this.miAboutVersionHistory = new DevExpress.XtraBars.BarButtonItem();
      this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
      this.miAddRulesColumnText = new DevExpress.XtraBars.BarButtonItem();
      this.miAddRulesColumnNumeric = new DevExpress.XtraBars.BarButtonItem();
      this.miAddDetailColumn = new DevExpress.XtraBars.BarButtonItem();
      this.miSteamUrl = new DevExpress.XtraBars.BarButtonItem();
      this.txtRconConsole = new DevExpress.XtraEditors.MemoEdit();
      this.txtRconCommand = new DevExpress.XtraEditors.ButtonEdit();
      this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
      this.txtRconPassword = new DevExpress.XtraEditors.ButtonEdit();
      this.panelTop = new DevExpress.XtraBars.Docking.DockPanel();
      this.controlContainer4 = new DevExpress.XtraBars.Docking.ControlContainer();
      this.panelStaticList = new DevExpress.XtraEditors.PanelControl();
      this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
      this.btnPasteAddresses = new DevExpress.XtraEditors.SimpleButton();
      this.txtGameServer = new DevExpress.XtraEditors.ButtonEdit();
      this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
      this.panelQuery = new DevExpress.XtraEditors.PanelControl();
      this.txtVersion = new DevExpress.XtraEditors.ButtonEdit();
      this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
      this.txtFilterInfoMaster = new DevExpress.XtraEditors.LabelControl();
      this.btnUpdateStatus = new DevExpress.XtraEditors.SimpleButton();
      this.comboQueryLimit = new DevExpress.XtraEditors.ComboBoxEdit();
      this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
      this.cbGetFull = new DevExpress.XtraEditors.CheckEdit();
      this.txtMod = new DevExpress.XtraEditors.ButtonEdit();
      this.comboGames = new DevExpress.XtraEditors.ComboBoxEdit();
      this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
      this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
      this.btnUpdateList = new DevExpress.XtraEditors.SimpleButton();
      this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
      this.txtMap = new DevExpress.XtraEditors.ButtonEdit();
      this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
      this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
      this.cbGetEmpty = new DevExpress.XtraEditors.CheckEdit();
      this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
      this.txtTagExcludeServer = new DevExpress.XtraEditors.ButtonEdit();
      this.comboMasterServer = new DevExpress.XtraEditors.ComboBoxEdit();
      this.txtTagIncludeServer = new DevExpress.XtraEditors.ButtonEdit();
      this.panelTabs = new DevExpress.XtraEditors.PanelControl();
      this.tabControl = new DevExpress.XtraTab.XtraTabControl();
      this.tabGame = new DevExpress.XtraTab.XtraTabPage();
      this.tabFavorites = new DevExpress.XtraTab.XtraTabPage();
      this.tabAdd = new DevExpress.XtraTab.XtraTabPage();
      this.panelOptions = new DevExpress.XtraEditors.PanelControl();
      this.cbHideGhosts = new DevExpress.XtraEditors.CheckEdit();
      this.cbConnectOnDoubleClick = new DevExpress.XtraEditors.CheckEdit();
      this.cbUseSteamApi = new DevExpress.XtraEditors.CheckEdit();
      this.cbShowCounts = new DevExpress.XtraEditors.CheckEdit();
      this.cbShowFilterPanelInfo = new DevExpress.XtraEditors.CheckEdit();
      this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
      this.cbNoUpdateWhilePlaying = new DevExpress.XtraEditors.CheckEdit();
      this.cbHideUnresponsiveServers = new DevExpress.XtraEditors.CheckEdit();
      this.rbUpdateStatusOnly = new DevExpress.XtraEditors.CheckEdit();
      this.cbFavServersOnTop = new DevExpress.XtraEditors.CheckEdit();
      this.rbAddressGamePort = new DevExpress.XtraEditors.CheckEdit();
      this.rbAddressQueryPort = new DevExpress.XtraEditors.CheckEdit();
      this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
      this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
      this.btnSkin = new DevExpress.XtraEditors.SimpleButton();
      this.cbRefreshSelectedServer = new DevExpress.XtraEditors.CheckEdit();
      this.rbAddressHidden = new DevExpress.XtraEditors.CheckEdit();
      this.spinRefreshInterval = new DevExpress.XtraEditors.SpinEdit();
      this.rbUpdateListAndStatus = new DevExpress.XtraEditors.CheckEdit();
      this.rbUpdateDisabled = new DevExpress.XtraEditors.CheckEdit();
      this.panelContainer1 = new DevExpress.XtraBars.Docking.DockPanel();
      this.panelRules = new DevExpress.XtraBars.Docking.DockPanel();
      this.controlContainer2 = new DevExpress.XtraBars.Docking.ControlContainer();
      this.gcRules = new DevExpress.XtraGrid.GridControl();
      this.dsRules = new System.Windows.Forms.BindingSource(this.components);
      this.gvRules = new DevExpress.XtraGrid.Views.Grid.GridView();
      this.colRuleName = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colRuleValue = new DevExpress.XtraGrid.Columns.GridColumn();
      this.panelPlayers = new DevExpress.XtraBars.Docking.DockPanel();
      this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
      this.panelServerDetails = new DevExpress.XtraBars.Docking.DockPanel();
      this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
      this.panelServerList = new DevExpress.XtraBars.Docking.DockPanel();
      this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
      this.grpQuickFilter = new DevExpress.XtraEditors.GroupControl();
      this.txtFilterInfoClient = new DevExpress.XtraEditors.LabelControl();
      this.comboMinPlayers = new DevExpress.XtraEditors.ComboBoxEdit();
      this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
      this.btnTagExcludeClient = new DevExpress.XtraEditors.ButtonEdit();
      this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
      this.btnTagIncludeClient = new DevExpress.XtraEditors.ButtonEdit();
      this.linkFilter1 = new DevExpress.XtraEditors.HyperlinkLabelControl();
      this.cbMinPlayersBots = new DevExpress.XtraEditors.CheckEdit();
      this.btnApplyFilter = new DevExpress.XtraEditors.SimpleButton();
      this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
      this.comboMaxPing = new DevExpress.XtraEditors.ComboBoxEdit();
      this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
      this.cbAlert = new DevExpress.XtraEditors.CheckButton();
      this.timerUpdateServerList = new System.Windows.Forms.Timer(this.components);
      this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
      this.txtStatus = new DevExpress.XtraEditors.LabelControl();
      this.menuServers = new DevExpress.XtraBars.PopupMenu(this.components);
      this.menuPlayers = new DevExpress.XtraBars.PopupMenu(this.components);
      this.timerReloadServers = new System.Windows.Forms.Timer(this.components);
      this.alertControl1 = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
      this.menuRules = new DevExpress.XtraBars.PopupMenu(this.components);
      this.menuTab = new DevExpress.XtraBars.PopupMenu(this.components);
      this.menuAddTab = new DevExpress.XtraBars.PopupMenu(this.components);
      this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
      this.menuDetails = new DevExpress.XtraBars.PopupMenu(this.components);
      this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::ServerBrowser.ConnectingWaitForm), false, true);
      this.timerHideWaitForm = new System.Windows.Forms.Timer(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.riCheckEdit)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.gcDetails)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.gcPlayers)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dsPlayer)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.gvPlayers)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.gcServers)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dsServers)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.gvServers)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.riCountryFlagEdit)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.imgFlags)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.riFavServer)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.riPrivate)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.riDedicated)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.riJoinStatus)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
      this.panelRcon.SuspendLayout();
      this.controlContainer3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtRconPort.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.riFindPlayer)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtRconConsole.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtRconCommand.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtRconPassword.Properties)).BeginInit();
      this.panelTop.SuspendLayout();
      this.controlContainer4.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.panelStaticList)).BeginInit();
      this.panelStaticList.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtGameServer.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.panelQuery)).BeginInit();
      this.panelQuery.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtVersion.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboQueryLimit.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbGetFull.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtMod.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboGames.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtMap.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbGetEmpty.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtTagExcludeServer.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboMasterServer.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtTagIncludeServer.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.panelTabs)).BeginInit();
      this.panelTabs.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
      this.tabControl.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.panelOptions)).BeginInit();
      this.panelOptions.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.cbHideGhosts.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbConnectOnDoubleClick.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbUseSteamApi.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbShowCounts.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbShowFilterPanelInfo.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbNoUpdateWhilePlaying.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbHideUnresponsiveServers.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbUpdateStatusOnly.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbFavServersOnTop.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbAddressGamePort.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbAddressQueryPort.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbRefreshSelectedServer.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbAddressHidden.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.spinRefreshInterval.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbUpdateListAndStatus.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbUpdateDisabled.Properties)).BeginInit();
      this.panelContainer1.SuspendLayout();
      this.panelRules.SuspendLayout();
      this.controlContainer2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gcRules)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dsRules)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.gvRules)).BeginInit();
      this.panelPlayers.SuspendLayout();
      this.dockPanel1_Container.SuspendLayout();
      this.panelServerDetails.SuspendLayout();
      this.dockPanel2_Container.SuspendLayout();
      this.panelServerList.SuspendLayout();
      this.controlContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grpQuickFilter)).BeginInit();
      this.grpQuickFilter.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.comboMinPlayers.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.btnTagExcludeClient.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.btnTagIncludeClient.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbMinPlayersBots.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboMaxPing.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
      this.panelControl1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.menuServers)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.menuPlayers)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.menuRules)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.menuTab)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.menuAddTab)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.menuDetails)).BeginInit();
      this.SuspendLayout();
      // 
      // riCheckEdit
      // 
      this.riCheckEdit.AutoHeight = false;
      this.riCheckEdit.Name = "riCheckEdit";
      // 
      // gcDetails
      // 
      this.gcDetails.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gcDetails.Location = new System.Drawing.Point(0, 0);
      this.gcDetails.MainView = this.gvDetails;
      this.gcDetails.Name = "gcDetails";
      this.gcDetails.Size = new System.Drawing.Size(353, 386);
      this.gcDetails.TabIndex = 13;
      this.gcDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetails});
      // 
      // gvDetails
      // 
      this.gvDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colKey,
            this.colValue});
      this.gvDetails.GridControl = this.gcDetails;
      this.gvDetails.Name = "gvDetails";
      this.gvDetails.OptionsView.ShowGroupPanel = false;
      this.gvDetails.OptionsView.ShowIndicator = false;
      this.gvDetails.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvDetails_MouseDown);
      // 
      // colKey
      // 
      this.colKey.Caption = "Setting";
      this.colKey.FieldName = "Item1";
      this.colKey.Name = "colKey";
      this.colKey.OptionsColumn.ReadOnly = true;
      this.colKey.Visible = true;
      this.colKey.VisibleIndex = 0;
      this.colKey.Width = 100;
      // 
      // colValue
      // 
      this.colValue.Caption = "Value";
      this.colValue.FieldName = "Item2";
      this.colValue.Name = "colValue";
      this.colValue.OptionsColumn.ReadOnly = true;
      this.colValue.UnboundType = DevExpress.Data.UnboundColumnType.Object;
      this.colValue.Visible = true;
      this.colValue.VisibleIndex = 1;
      this.colValue.Width = 150;
      // 
      // gcPlayers
      // 
      this.gcPlayers.DataSource = this.dsPlayer;
      this.gcPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gcPlayers.Location = new System.Drawing.Point(0, 0);
      this.gcPlayers.MainView = this.gvPlayers;
      this.gcPlayers.Name = "gcPlayers";
      this.gcPlayers.Size = new System.Drawing.Size(353, 386);
      this.gcPlayers.TabIndex = 0;
      this.gcPlayers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPlayers});
      // 
      // dsPlayer
      // 
      this.dsPlayer.DataSource = typeof(QueryMaster.Player);
      // 
      // gvPlayers
      // 
      this.gvPlayers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPlayerName,
            this.colScore,
            this.colTime});
      this.gvPlayers.GridControl = this.gcPlayers;
      this.gvPlayers.Name = "gvPlayers";
      this.gvPlayers.OptionsBehavior.Editable = false;
      this.gvPlayers.OptionsView.ShowGroupPanel = false;
      this.gvPlayers.OptionsView.ShowIndicator = false;
      this.gvPlayers.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colScore, DevExpress.Data.ColumnSortOrder.Descending)});
      this.gvPlayers.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvPlayers_RowCellStyle);
      this.gvPlayers.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.gvPlayers_CustomUnboundColumnData);
      this.gvPlayers.CustomRowFilter += new DevExpress.XtraGrid.Views.Base.RowFilterEventHandler(this.gvPlayers_CustomRowFilter);
      this.gvPlayers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvPlayers_MouseDown);
      this.gvPlayers.DoubleClick += new System.EventHandler(this.gvPlayers_DoubleClick);
      // 
      // colPlayerName
      // 
      this.colPlayerName.Caption = "Name";
      this.colPlayerName.FieldName = "CleanName";
      this.colPlayerName.Name = "colPlayerName";
      this.colPlayerName.OptionsColumn.ReadOnly = true;
      this.colPlayerName.UnboundType = DevExpress.Data.UnboundColumnType.String;
      this.colPlayerName.Visible = true;
      this.colPlayerName.VisibleIndex = 0;
      this.colPlayerName.Width = 131;
      // 
      // colScore
      // 
      this.colScore.FieldName = "Score";
      this.colScore.Name = "colScore";
      this.colScore.OptionsColumn.AllowEdit = false;
      this.colScore.OptionsColumn.FixedWidth = true;
      this.colScore.OptionsColumn.ReadOnly = true;
      this.colScore.Visible = true;
      this.colScore.VisibleIndex = 1;
      this.colScore.Width = 48;
      // 
      // colTime
      // 
      this.colTime.FieldName = "Time";
      this.colTime.Name = "colTime";
      this.colTime.OptionsColumn.AllowEdit = false;
      this.colTime.OptionsColumn.FixedWidth = true;
      this.colTime.OptionsColumn.ReadOnly = true;
      this.colTime.Visible = true;
      this.colTime.VisibleIndex = 2;
      // 
      // gcServers
      // 
      this.gcServers.DataSource = this.dsServers;
      this.gcServers.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gcServers.Location = new System.Drawing.Point(0, 66);
      this.gcServers.MainView = this.gvServers;
      this.gcServers.Name = "gcServers";
      this.gcServers.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riCountryFlagEdit,
            this.riFavServer,
            this.riJoinStatus,
            this.riPrivate,
            this.riDedicated});
      this.gcServers.Size = new System.Drawing.Size(1288, 305);
      this.gcServers.TabIndex = 0;
      this.gcServers.ToolTipController = this.toolTipController;
      this.gcServers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvServers});
      // 
      // dsServers
      // 
      this.dsServers.DataSource = typeof(ServerBrowser.ServerRow);
      // 
      // gvServers
      // 
      this.gvServers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLocation,
            this.colBuddyCount,
            this.colFavServer,
            this.colEndPoint,
            this.colPrivate,
            this.colDedicated,
            this.colJoinStatus,
            this.colName,
            this.colDescription,
            this.colTags,
            this.colPlayerCount,
            this.colHumanPlayers,
            this.colBots,
            this.colTotalPlayers,
            this.colMaxPlayers,
            this.colMap,
            this.colPing,
            this.colStatus});
      this.gvServers.GridControl = this.gcServers;
      this.gvServers.Images = this.imageCollection;
      this.gvServers.Name = "gvServers";
      this.gvServers.OptionsBehavior.ImmediateUpdateRowPosition = false;
      this.gvServers.OptionsDetail.EnableMasterViewMode = false;
      this.gvServers.OptionsFilter.DefaultFilterEditorView = DevExpress.XtraEditors.FilterEditorViewMode.VisualAndText;
      this.gvServers.OptionsSelection.MultiSelect = true;
      this.gvServers.OptionsView.ColumnAutoWidth = false;
      this.gvServers.OptionsView.ShowAutoFilterRow = true;
      this.gvServers.OptionsView.ShowFooter = true;
      this.gvServers.OptionsView.ShowGroupPanel = false;
      this.gvServers.OptionsView.ShowIndicator = false;
      this.gvServers.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colPlayerCount, DevExpress.Data.ColumnSortOrder.Descending)});
      this.gvServers.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gvServers_SelectionChanged);
      this.gvServers.StartSorting += new System.EventHandler(this.gvServers_StartSorting);
      this.gvServers.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvServers_FocusedRowChanged);
      this.gvServers.ColumnFilterChanged += new System.EventHandler(this.gvServers_ColumnFilterChanged);
      this.gvServers.CustomColumnSort += new DevExpress.XtraGrid.Views.Base.CustomColumnSortEventHandler(this.gvServers_CustomColumnSort);
      this.gvServers.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.gvServers_CustomUnboundColumnData);
      this.gvServers.CustomRowFilter += new DevExpress.XtraGrid.Views.Base.RowFilterEventHandler(this.gvServers_CustomRowFilter);
      this.gvServers.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gvServers_CustomColumnDisplayText);
      this.gvServers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvServers_MouseDown);
      this.gvServers.DoubleClick += new System.EventHandler(this.gvServers_DoubleClick);
      // 
      // colLocation
      // 
      this.colLocation.Caption = "Location";
      this.colLocation.ColumnEdit = this.riCountryFlagEdit;
      this.colLocation.FieldName = "GeoInfo.Iso2";
      this.colLocation.Name = "colLocation";
      this.colLocation.OptionsColumn.AllowEdit = false;
      this.colLocation.ToolTip = "Country";
      this.colLocation.Visible = true;
      this.colLocation.VisibleIndex = 2;
      this.colLocation.Width = 49;
      // 
      // riCountryFlagEdit
      // 
      this.riCountryFlagEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.riCountryFlagEdit.Name = "riCountryFlagEdit";
      this.riCountryFlagEdit.SmallImages = this.imgFlags;
      // 
      // imgFlags
      // 
      this.imgFlags.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgFlags.ImageStream")));
      this.imgFlags.Images.SetKeyName(0, "ad.png");
      this.imgFlags.Images.SetKeyName(1, "ae.png");
      this.imgFlags.Images.SetKeyName(2, "af.png");
      this.imgFlags.Images.SetKeyName(3, "ag.png");
      this.imgFlags.Images.SetKeyName(4, "ai.png");
      this.imgFlags.Images.SetKeyName(5, "al.png");
      this.imgFlags.Images.SetKeyName(6, "am.png");
      this.imgFlags.Images.SetKeyName(7, "an.png");
      this.imgFlags.Images.SetKeyName(8, "ao.png");
      this.imgFlags.Images.SetKeyName(9, "ar.png");
      this.imgFlags.Images.SetKeyName(10, "as.png");
      this.imgFlags.Images.SetKeyName(11, "at.png");
      this.imgFlags.Images.SetKeyName(12, "au.png");
      this.imgFlags.Images.SetKeyName(13, "aw.png");
      this.imgFlags.Images.SetKeyName(14, "ax.png");
      this.imgFlags.Images.SetKeyName(15, "az.png");
      this.imgFlags.Images.SetKeyName(16, "ba.png");
      this.imgFlags.Images.SetKeyName(17, "bb.png");
      this.imgFlags.Images.SetKeyName(18, "bd.png");
      this.imgFlags.Images.SetKeyName(19, "be.png");
      this.imgFlags.Images.SetKeyName(20, "bf.png");
      this.imgFlags.Images.SetKeyName(21, "bg.png");
      this.imgFlags.Images.SetKeyName(22, "bh.png");
      this.imgFlags.Images.SetKeyName(23, "bi.png");
      this.imgFlags.Images.SetKeyName(24, "bj.png");
      this.imgFlags.Images.SetKeyName(25, "bm.png");
      this.imgFlags.Images.SetKeyName(26, "bn.png");
      this.imgFlags.Images.SetKeyName(27, "bo.png");
      this.imgFlags.Images.SetKeyName(28, "br.png");
      this.imgFlags.Images.SetKeyName(29, "bs.png");
      this.imgFlags.Images.SetKeyName(30, "bt.png");
      this.imgFlags.Images.SetKeyName(31, "bv.png");
      this.imgFlags.Images.SetKeyName(32, "bw.png");
      this.imgFlags.Images.SetKeyName(33, "by.png");
      this.imgFlags.Images.SetKeyName(34, "bz.png");
      this.imgFlags.Images.SetKeyName(35, "ca.png");
      this.imgFlags.Images.SetKeyName(36, "cc.png");
      this.imgFlags.Images.SetKeyName(37, "cd.png");
      this.imgFlags.Images.SetKeyName(38, "cf.png");
      this.imgFlags.Images.SetKeyName(39, "cg.png");
      this.imgFlags.Images.SetKeyName(40, "ch.png");
      this.imgFlags.Images.SetKeyName(41, "ci.png");
      this.imgFlags.Images.SetKeyName(42, "ck.png");
      this.imgFlags.Images.SetKeyName(43, "cl.png");
      this.imgFlags.Images.SetKeyName(44, "cm.png");
      this.imgFlags.Images.SetKeyName(45, "cn.png");
      this.imgFlags.Images.SetKeyName(46, "co.png");
      this.imgFlags.Images.SetKeyName(47, "cr.png");
      this.imgFlags.Images.SetKeyName(48, "cs.png");
      this.imgFlags.Images.SetKeyName(49, "cu.png");
      this.imgFlags.Images.SetKeyName(50, "cv.png");
      this.imgFlags.Images.SetKeyName(51, "cx.png");
      this.imgFlags.Images.SetKeyName(52, "cy.png");
      this.imgFlags.Images.SetKeyName(53, "cz.png");
      this.imgFlags.Images.SetKeyName(54, "de.png");
      this.imgFlags.Images.SetKeyName(55, "dj.png");
      this.imgFlags.Images.SetKeyName(56, "dk.png");
      this.imgFlags.Images.SetKeyName(57, "dm.png");
      this.imgFlags.Images.SetKeyName(58, "do.png");
      this.imgFlags.Images.SetKeyName(59, "dz.png");
      this.imgFlags.Images.SetKeyName(60, "ec.png");
      this.imgFlags.Images.SetKeyName(61, "ee.png");
      this.imgFlags.Images.SetKeyName(62, "eg.png");
      this.imgFlags.Images.SetKeyName(63, "eh.png");
      this.imgFlags.Images.SetKeyName(64, "er.png");
      this.imgFlags.Images.SetKeyName(65, "es.png");
      this.imgFlags.Images.SetKeyName(66, "et.png");
      this.imgFlags.Images.SetKeyName(67, "fi.png");
      this.imgFlags.Images.SetKeyName(68, "fj.png");
      this.imgFlags.Images.SetKeyName(69, "fk.png");
      this.imgFlags.Images.SetKeyName(70, "fm.png");
      this.imgFlags.Images.SetKeyName(71, "fo.png");
      this.imgFlags.Images.SetKeyName(72, "fr.png");
      this.imgFlags.Images.SetKeyName(73, "ga.png");
      this.imgFlags.Images.SetKeyName(74, "gb.png");
      this.imgFlags.Images.SetKeyName(75, "gd.png");
      this.imgFlags.Images.SetKeyName(76, "ge.png");
      this.imgFlags.Images.SetKeyName(77, "gf.png");
      this.imgFlags.Images.SetKeyName(78, "gh.png");
      this.imgFlags.Images.SetKeyName(79, "gi.png");
      this.imgFlags.Images.SetKeyName(80, "gl.png");
      this.imgFlags.Images.SetKeyName(81, "gm.png");
      this.imgFlags.Images.SetKeyName(82, "gn.png");
      this.imgFlags.Images.SetKeyName(83, "gp.png");
      this.imgFlags.Images.SetKeyName(84, "gq.png");
      this.imgFlags.Images.SetKeyName(85, "gr.png");
      this.imgFlags.Images.SetKeyName(86, "gs.png");
      this.imgFlags.Images.SetKeyName(87, "gt.png");
      this.imgFlags.Images.SetKeyName(88, "gu.png");
      this.imgFlags.Images.SetKeyName(89, "gw.png");
      this.imgFlags.Images.SetKeyName(90, "gy.png");
      this.imgFlags.Images.SetKeyName(91, "hk.png");
      this.imgFlags.Images.SetKeyName(92, "hm.png");
      this.imgFlags.Images.SetKeyName(93, "hn.png");
      this.imgFlags.Images.SetKeyName(94, "hr.png");
      this.imgFlags.Images.SetKeyName(95, "ht.png");
      this.imgFlags.Images.SetKeyName(96, "hu.png");
      this.imgFlags.Images.SetKeyName(97, "id.png");
      this.imgFlags.Images.SetKeyName(98, "ie.png");
      this.imgFlags.Images.SetKeyName(99, "il.png");
      this.imgFlags.Images.SetKeyName(100, "in.png");
      this.imgFlags.Images.SetKeyName(101, "io.png");
      this.imgFlags.Images.SetKeyName(102, "iq.png");
      this.imgFlags.Images.SetKeyName(103, "ir.png");
      this.imgFlags.Images.SetKeyName(104, "is.png");
      this.imgFlags.Images.SetKeyName(105, "it.png");
      this.imgFlags.Images.SetKeyName(106, "jm.png");
      this.imgFlags.Images.SetKeyName(107, "jo.png");
      this.imgFlags.Images.SetKeyName(108, "jp.png");
      this.imgFlags.Images.SetKeyName(109, "ke.png");
      this.imgFlags.Images.SetKeyName(110, "kg.png");
      this.imgFlags.Images.SetKeyName(111, "kh.png");
      this.imgFlags.Images.SetKeyName(112, "ki.png");
      this.imgFlags.Images.SetKeyName(113, "km.png");
      this.imgFlags.Images.SetKeyName(114, "kn.png");
      this.imgFlags.Images.SetKeyName(115, "kp.png");
      this.imgFlags.Images.SetKeyName(116, "kr.png");
      this.imgFlags.Images.SetKeyName(117, "kw.png");
      this.imgFlags.Images.SetKeyName(118, "ky.png");
      this.imgFlags.Images.SetKeyName(119, "kz.png");
      this.imgFlags.Images.SetKeyName(120, "la.png");
      this.imgFlags.Images.SetKeyName(121, "lb.png");
      this.imgFlags.Images.SetKeyName(122, "lc.png");
      this.imgFlags.Images.SetKeyName(123, "li.png");
      this.imgFlags.Images.SetKeyName(124, "lk.png");
      this.imgFlags.Images.SetKeyName(125, "lr.png");
      this.imgFlags.Images.SetKeyName(126, "ls.png");
      this.imgFlags.Images.SetKeyName(127, "lt.png");
      this.imgFlags.Images.SetKeyName(128, "lu.png");
      this.imgFlags.Images.SetKeyName(129, "lv.png");
      this.imgFlags.Images.SetKeyName(130, "ly.png");
      this.imgFlags.Images.SetKeyName(131, "ma.png");
      this.imgFlags.Images.SetKeyName(132, "mc.png");
      this.imgFlags.Images.SetKeyName(133, "md.png");
      this.imgFlags.Images.SetKeyName(134, "me.png");
      this.imgFlags.Images.SetKeyName(135, "mg.png");
      this.imgFlags.Images.SetKeyName(136, "mh.png");
      this.imgFlags.Images.SetKeyName(137, "mk.png");
      this.imgFlags.Images.SetKeyName(138, "ml.png");
      this.imgFlags.Images.SetKeyName(139, "mm.png");
      this.imgFlags.Images.SetKeyName(140, "mn.png");
      this.imgFlags.Images.SetKeyName(141, "mo.png");
      this.imgFlags.Images.SetKeyName(142, "mp.png");
      this.imgFlags.Images.SetKeyName(143, "mq.png");
      this.imgFlags.Images.SetKeyName(144, "mr.png");
      this.imgFlags.Images.SetKeyName(145, "ms.png");
      this.imgFlags.Images.SetKeyName(146, "mt.png");
      this.imgFlags.Images.SetKeyName(147, "mu.png");
      this.imgFlags.Images.SetKeyName(148, "mv.png");
      this.imgFlags.Images.SetKeyName(149, "mw.png");
      this.imgFlags.Images.SetKeyName(150, "mx.png");
      this.imgFlags.Images.SetKeyName(151, "my.png");
      this.imgFlags.Images.SetKeyName(152, "mz.png");
      this.imgFlags.Images.SetKeyName(153, "na.png");
      this.imgFlags.Images.SetKeyName(154, "nc.png");
      this.imgFlags.Images.SetKeyName(155, "ne.png");
      this.imgFlags.Images.SetKeyName(156, "nf.png");
      this.imgFlags.Images.SetKeyName(157, "ng.png");
      this.imgFlags.Images.SetKeyName(158, "ni.png");
      this.imgFlags.Images.SetKeyName(159, "nl.png");
      this.imgFlags.Images.SetKeyName(160, "no.png");
      this.imgFlags.Images.SetKeyName(161, "np.png");
      this.imgFlags.Images.SetKeyName(162, "nr.png");
      this.imgFlags.Images.SetKeyName(163, "nu.png");
      this.imgFlags.Images.SetKeyName(164, "nz.png");
      this.imgFlags.Images.SetKeyName(165, "om.png");
      this.imgFlags.Images.SetKeyName(166, "pa.png");
      this.imgFlags.Images.SetKeyName(167, "pe.png");
      this.imgFlags.Images.SetKeyName(168, "pf.png");
      this.imgFlags.Images.SetKeyName(169, "pg.png");
      this.imgFlags.Images.SetKeyName(170, "ph.png");
      this.imgFlags.Images.SetKeyName(171, "pk.png");
      this.imgFlags.Images.SetKeyName(172, "pl.png");
      this.imgFlags.Images.SetKeyName(173, "pm.png");
      this.imgFlags.Images.SetKeyName(174, "pn.png");
      this.imgFlags.Images.SetKeyName(175, "pr.png");
      this.imgFlags.Images.SetKeyName(176, "ps.png");
      this.imgFlags.Images.SetKeyName(177, "pt.png");
      this.imgFlags.Images.SetKeyName(178, "pw.png");
      this.imgFlags.Images.SetKeyName(179, "py.png");
      this.imgFlags.Images.SetKeyName(180, "qa.png");
      this.imgFlags.Images.SetKeyName(181, "re.png");
      this.imgFlags.Images.SetKeyName(182, "ro.png");
      this.imgFlags.Images.SetKeyName(183, "rs.png");
      this.imgFlags.Images.SetKeyName(184, "ru.png");
      this.imgFlags.Images.SetKeyName(185, "rw.png");
      this.imgFlags.Images.SetKeyName(186, "sa.png");
      this.imgFlags.Images.SetKeyName(187, "sb.png");
      this.imgFlags.Images.SetKeyName(188, "sc.png");
      this.imgFlags.Images.SetKeyName(189, "sd.png");
      this.imgFlags.Images.SetKeyName(190, "se.png");
      this.imgFlags.Images.SetKeyName(191, "sg.png");
      this.imgFlags.Images.SetKeyName(192, "sh.png");
      this.imgFlags.Images.SetKeyName(193, "si.png");
      this.imgFlags.Images.SetKeyName(194, "sj.png");
      this.imgFlags.Images.SetKeyName(195, "sk.png");
      this.imgFlags.Images.SetKeyName(196, "sl.png");
      this.imgFlags.Images.SetKeyName(197, "sm.png");
      this.imgFlags.Images.SetKeyName(198, "sn.png");
      this.imgFlags.Images.SetKeyName(199, "so.png");
      this.imgFlags.Images.SetKeyName(200, "sr.png");
      this.imgFlags.Images.SetKeyName(201, "st.png");
      this.imgFlags.Images.SetKeyName(202, "sv.png");
      this.imgFlags.Images.SetKeyName(203, "sy.png");
      this.imgFlags.Images.SetKeyName(204, "sz.png");
      this.imgFlags.Images.SetKeyName(205, "tc.png");
      this.imgFlags.Images.SetKeyName(206, "td.png");
      this.imgFlags.Images.SetKeyName(207, "tf.png");
      this.imgFlags.Images.SetKeyName(208, "tg.png");
      this.imgFlags.Images.SetKeyName(209, "th.png");
      this.imgFlags.Images.SetKeyName(210, "tj.png");
      this.imgFlags.Images.SetKeyName(211, "tk.png");
      this.imgFlags.Images.SetKeyName(212, "tl.png");
      this.imgFlags.Images.SetKeyName(213, "tm.png");
      this.imgFlags.Images.SetKeyName(214, "tn.png");
      this.imgFlags.Images.SetKeyName(215, "to.png");
      this.imgFlags.Images.SetKeyName(216, "tr.png");
      this.imgFlags.Images.SetKeyName(217, "tt.png");
      this.imgFlags.Images.SetKeyName(218, "tv.png");
      this.imgFlags.Images.SetKeyName(219, "tw.png");
      this.imgFlags.Images.SetKeyName(220, "tz.png");
      this.imgFlags.Images.SetKeyName(221, "ua.png");
      this.imgFlags.Images.SetKeyName(222, "ug.png");
      this.imgFlags.Images.SetKeyName(223, "um.png");
      this.imgFlags.Images.SetKeyName(224, "us.png");
      this.imgFlags.Images.SetKeyName(225, "uy.png");
      this.imgFlags.Images.SetKeyName(226, "uz.png");
      this.imgFlags.Images.SetKeyName(227, "va.png");
      this.imgFlags.Images.SetKeyName(228, "vc.png");
      this.imgFlags.Images.SetKeyName(229, "ve.png");
      this.imgFlags.Images.SetKeyName(230, "vg.png");
      this.imgFlags.Images.SetKeyName(231, "vi.png");
      this.imgFlags.Images.SetKeyName(232, "vn.png");
      this.imgFlags.Images.SetKeyName(233, "vu.png");
      this.imgFlags.Images.SetKeyName(234, "wf.png");
      this.imgFlags.Images.SetKeyName(235, "ws.png");
      this.imgFlags.Images.SetKeyName(236, "ye.png");
      this.imgFlags.Images.SetKeyName(237, "yt.png");
      this.imgFlags.Images.SetKeyName(238, "za.png");
      this.imgFlags.Images.SetKeyName(239, "zm.png");
      this.imgFlags.Images.SetKeyName(240, "zw.png");
      // 
      // colBuddyCount
      // 
      this.colBuddyCount.Caption = "Bdy";
      this.colBuddyCount.CustomizationCaption = "BuddyCount";
      this.colBuddyCount.FieldName = "BuddyCount";
      this.colBuddyCount.Name = "colBuddyCount";
      this.colBuddyCount.OptionsColumn.AllowEdit = false;
      this.colBuddyCount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "BuddyCount", "{0:d}")});
      this.colBuddyCount.ToolTip = "Number of players which you have in your \"Find Buddy\" list.";
      this.colBuddyCount.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
      this.colBuddyCount.Visible = true;
      this.colBuddyCount.VisibleIndex = 1;
      this.colBuddyCount.Width = 30;
      // 
      // colFavServer
      // 
      this.colFavServer.Caption = "Favorite";
      this.colFavServer.ColumnEdit = this.riFavServer;
      this.colFavServer.FieldName = "IsFavorite";
      this.colFavServer.Name = "colFavServer";
      this.colFavServer.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
      this.colFavServer.ToolTip = "Favorite Server";
      this.colFavServer.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
      this.colFavServer.Visible = true;
      this.colFavServer.VisibleIndex = 0;
      this.colFavServer.Width = 22;
      // 
      // riFavServer
      // 
      this.riFavServer.AutoHeight = false;
      this.riFavServer.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;
      this.riFavServer.ImageOptions.ImageIndexChecked = 3;
      this.riFavServer.ImageOptions.ImageIndexGrayed = 10;
      this.riFavServer.ImageOptions.Images = this.imageCollection;
      this.riFavServer.Name = "riFavServer";
      this.riFavServer.EditValueChanged += new System.EventHandler(this.riFavServer_EditValueChanged);
      // 
      // imageCollection
      // 
      this.imageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection.ImageStream")));
      this.imageCollection.Images.SetKeyName(0, "000.png");
      this.imageCollection.Images.SetKeyName(1, "001.png");
      this.imageCollection.Images.SetKeyName(2, "002.png");
      this.imageCollection.Images.SetKeyName(3, "003.png");
      this.imageCollection.Images.SetKeyName(4, "004.png");
      this.imageCollection.Images.SetKeyName(5, "005.png");
      this.imageCollection.Images.SetKeyName(6, "006.png");
      this.imageCollection.Images.SetKeyName(7, "007.png");
      this.imageCollection.Images.SetKeyName(8, "008.png");
      this.imageCollection.Images.SetKeyName(9, "009.png");
      this.imageCollection.Images.SetKeyName(10, "010.png");
      this.imageCollection.Images.SetKeyName(11, "011.png");
      this.imageCollection.Images.SetKeyName(12, "012.png");
      this.imageCollection.Images.SetKeyName(13, "013.png");
      this.imageCollection.Images.SetKeyName(14, "014.png");
      this.imageCollection.Images.SetKeyName(15, "015.png");
      this.imageCollection.Images.SetKeyName(16, "016.png");
      this.imageCollection.Images.SetKeyName(17, "017.png");
      this.imageCollection.Images.SetKeyName(18, "018.png");
      this.imageCollection.Images.SetKeyName(19, "019.png");
      this.imageCollection.Images.SetKeyName(20, "020.png");
      this.imageCollection.Images.SetKeyName(21, "021.png");
      this.imageCollection.Images.SetKeyName(22, "022.png");
      this.imageCollection.Images.SetKeyName(23, "023.png");
      this.imageCollection.Images.SetKeyName(24, "024.png");
      this.imageCollection.Images.SetKeyName(25, "025.png");
      this.imageCollection.Images.SetKeyName(26, "026.png");
      this.imageCollection.Images.SetKeyName(27, "027.png");
      this.imageCollection.Images.SetKeyName(28, "028.png");
      // 
      // colEndPoint
      // 
      this.colEndPoint.Caption = "Address";
      this.colEndPoint.FieldName = "Address";
      this.colEndPoint.Name = "colEndPoint";
      this.colEndPoint.OptionsColumn.ReadOnly = true;
      this.colEndPoint.UnboundType = DevExpress.Data.UnboundColumnType.String;
      this.colEndPoint.Visible = true;
      this.colEndPoint.VisibleIndex = 3;
      this.colEndPoint.Width = 132;
      // 
      // colPrivate
      // 
      this.colPrivate.Caption = "Pr";
      this.colPrivate.ColumnEdit = this.riPrivate;
      this.colPrivate.FieldName = "ServerInfo.IsPrivate";
      this.colPrivate.Name = "colPrivate";
      this.colPrivate.OptionsColumn.AllowEdit = false;
      this.colPrivate.Visible = true;
      this.colPrivate.VisibleIndex = 5;
      this.colPrivate.Width = 20;
      // 
      // riPrivate
      // 
      this.riPrivate.AutoHeight = false;
      this.riPrivate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.riPrivate.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Public", false, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Private", true, 24)});
      this.riPrivate.Name = "riPrivate";
      this.riPrivate.SmallImages = this.imageCollection;
      // 
      // colDedicated
      // 
      this.colDedicated.Caption = "De";
      this.colDedicated.ColumnEdit = this.riDedicated;
      this.colDedicated.FieldName = "Dedicated";
      this.colDedicated.Name = "colDedicated";
      this.colDedicated.ToolTip = "Dedicated / Listen";
      this.colDedicated.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
      this.colDedicated.Visible = true;
      this.colDedicated.VisibleIndex = 4;
      this.colDedicated.Width = 20;
      // 
      // riDedicated
      // 
      this.riDedicated.AutoHeight = false;
      this.riDedicated.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.riDedicated.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Dedicated Server", true, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Listen Server (Home)", false, 27)});
      this.riDedicated.Name = "riDedicated";
      this.riDedicated.SmallImages = this.imageCollection;
      // 
      // colJoinStatus
      // 
      this.colJoinStatus.Caption = "Jn";
      this.colJoinStatus.ColumnEdit = this.riJoinStatus;
      this.colJoinStatus.CustomizationCaption = "JoinStatus";
      this.colJoinStatus.FieldName = "JoinStatus";
      this.colJoinStatus.Name = "colJoinStatus";
      this.colJoinStatus.OptionsColumn.AllowEdit = false;
      this.colJoinStatus.ToolTip = "Joinable";
      this.colJoinStatus.Visible = true;
      this.colJoinStatus.VisibleIndex = 6;
      this.colJoinStatus.Width = 20;
      // 
      // riJoinStatus
      // 
      this.riJoinStatus.AutoHeight = false;
      this.riJoinStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.riJoinStatus.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Vacancy", 0, 20),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Full Teams", 1, 21),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Full Server", 2, 22)});
      this.riJoinStatus.Name = "riJoinStatus";
      this.riJoinStatus.SmallImages = this.imageCollection;
      // 
      // colName
      // 
      this.colName.FieldName = "Name";
      this.colName.Name = "colName";
      this.colName.OptionsColumn.AllowEdit = false;
      this.colName.OptionsColumn.ReadOnly = true;
      this.colName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
      this.colName.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Name", "Servers passing filter: {0:d}")});
      this.colName.UnboundType = DevExpress.Data.UnboundColumnType.String;
      this.colName.Visible = true;
      this.colName.VisibleIndex = 7;
      this.colName.Width = 260;
      // 
      // colDescription
      // 
      this.colDescription.Caption = "Description";
      this.colDescription.FieldName = "ServerInfo.Description";
      this.colDescription.Name = "colDescription";
      this.colDescription.OptionsColumn.AllowEdit = false;
      this.colDescription.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
      this.colDescription.Visible = true;
      this.colDescription.VisibleIndex = 8;
      this.colDescription.Width = 101;
      // 
      // colTags
      // 
      this.colTags.Caption = "Tags";
      this.colTags.FieldName = "ServerInfo.Extra.Keywords";
      this.colTags.Name = "colTags";
      this.colTags.OptionsColumn.AllowEdit = false;
      this.colTags.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
      this.colTags.Visible = true;
      this.colTags.VisibleIndex = 9;
      this.colTags.Width = 127;
      // 
      // colPlayerCount
      // 
      this.colPlayerCount.Caption = "Players";
      this.colPlayerCount.FieldName = "PlayerCount";
      this.colPlayerCount.Name = "colPlayerCount";
      this.colPlayerCount.OptionsColumn.AllowEdit = false;
      this.colPlayerCount.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
      this.colPlayerCount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PlayerCount.RealPlayers", "Sum: {0:d}")});
      this.colPlayerCount.ToolTip = "Humans+Bots / MaxPlayers+Specs";
      this.colPlayerCount.Visible = true;
      this.colPlayerCount.VisibleIndex = 10;
      // 
      // colHumanPlayers
      // 
      this.colHumanPlayers.Caption = "Humans";
      this.colHumanPlayers.FieldName = "PlayerCount.RealPlayers";
      this.colHumanPlayers.Name = "colHumanPlayers";
      this.colHumanPlayers.OptionsColumn.AllowEdit = false;
      this.colHumanPlayers.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PlayerCount.RealPlayers", "{0:d}")});
      this.colHumanPlayers.ToolTip = "Human Players";
      this.colHumanPlayers.Visible = true;
      this.colHumanPlayers.VisibleIndex = 11;
      this.colHumanPlayers.Width = 30;
      // 
      // colBots
      // 
      this.colBots.Caption = "Bots";
      this.colBots.FieldName = "PlayerCount.Bots";
      this.colBots.Name = "colBots";
      this.colBots.OptionsColumn.AllowEdit = false;
      this.colBots.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PlayerCount.Bots", "{0:d}")});
      this.colBots.Visible = true;
      this.colBots.VisibleIndex = 12;
      this.colBots.Width = 30;
      // 
      // colTotalPlayers
      // 
      this.colTotalPlayers.Caption = "Total";
      this.colTotalPlayers.FieldName = "PlayerCount.TotalPlayers";
      this.colTotalPlayers.Name = "colTotalPlayers";
      this.colTotalPlayers.OptionsColumn.AllowEdit = false;
      this.colTotalPlayers.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PlayerCount.TotalPlayers", "{0:d}")});
      this.colTotalPlayers.ToolTip = "Total Players (Humans + Bots)";
      this.colTotalPlayers.Visible = true;
      this.colTotalPlayers.VisibleIndex = 13;
      this.colTotalPlayers.Width = 30;
      // 
      // colMaxPlayers
      // 
      this.colMaxPlayers.Caption = "Max";
      this.colMaxPlayers.FieldName = "PlayerCount.MaxPlayers";
      this.colMaxPlayers.Name = "colMaxPlayers";
      this.colMaxPlayers.OptionsColumn.AllowEdit = false;
      this.colMaxPlayers.ToolTip = "Maximum Players";
      this.colMaxPlayers.Visible = true;
      this.colMaxPlayers.VisibleIndex = 14;
      this.colMaxPlayers.Width = 30;
      // 
      // colMap
      // 
      this.colMap.Caption = "Map";
      this.colMap.FieldName = "ServerInfo.Map";
      this.colMap.Name = "colMap";
      this.colMap.OptionsColumn.AllowEdit = false;
      this.colMap.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
      this.colMap.Visible = true;
      this.colMap.VisibleIndex = 15;
      this.colMap.Width = 110;
      // 
      // colPing
      // 
      this.colPing.Caption = "Ping";
      this.colPing.DisplayFormat.FormatString = "d";
      this.colPing.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
      this.colPing.FieldName = "ServerInfo.Ping";
      this.colPing.Name = "colPing";
      this.colPing.OptionsColumn.AllowEdit = false;
      this.colPing.Visible = true;
      this.colPing.VisibleIndex = 16;
      this.colPing.Width = 32;
      // 
      // colStatus
      // 
      this.colStatus.Caption = "Status";
      this.colStatus.FieldName = "Status";
      this.colStatus.Name = "colStatus";
      this.colStatus.OptionsColumn.AllowEdit = false;
      this.colStatus.Visible = true;
      this.colStatus.VisibleIndex = 17;
      this.colStatus.Width = 61;
      // 
      // toolTipController
      // 
      this.toolTipController.AutoPopDelay = 7000;
      this.toolTipController.InitialDelay = 100;
      this.toolTipController.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(this.toolTipController_GetActiveObjectInfo);
      // 
      // dockManager1
      // 
      this.dockManager1.Form = this;
      this.dockManager1.HiddenPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.panelRcon});
      this.dockManager1.LayoutVersion = "2";
      this.dockManager1.MenuManager = this.barManager1;
      this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.panelTop,
            this.panelContainer1,
            this.panelServerList});
      this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraTab.XtraTabControl",
            "DevExpress.XtraEditors.PanelControl"});
      this.dockManager1.BeforeLoadLayout += new DevExpress.Utils.LayoutAllowEventHandler(this.dockManager1_BeforeLoadLayout);
      this.dockManager1.StartDocking += new DevExpress.XtraBars.Docking.DockPanelCancelEventHandler(this.dockManager1_StartDocking);
      // 
      // panelRcon
      // 
      this.panelRcon.Controls.Add(this.controlContainer3);
      this.panelRcon.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
      this.panelRcon.ID = new System.Guid("7b6df7b6-b112-4685-a1f4-c23ae548835f");
      this.panelRcon.Location = new System.Drawing.Point(0, 648);
      this.panelRcon.Name = "panelRcon";
      this.panelRcon.OriginalSize = new System.Drawing.Size(200, 136);
      this.panelRcon.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
      this.panelRcon.SavedIndex = 1;
      this.panelRcon.Size = new System.Drawing.Size(1103, 136);
      this.panelRcon.Text = "Remote Console";
      this.panelRcon.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
      // 
      // controlContainer3
      // 
      this.controlContainer3.Controls.Add(this.labelControl13);
      this.controlContainer3.Controls.Add(this.labelControl15);
      this.controlContainer3.Controls.Add(this.txtRconPort);
      this.controlContainer3.Controls.Add(this.txtRconConsole);
      this.controlContainer3.Controls.Add(this.txtRconCommand);
      this.controlContainer3.Controls.Add(this.labelControl14);
      this.controlContainer3.Controls.Add(this.txtRconPassword);
      this.controlContainer3.Location = new System.Drawing.Point(4, 23);
      this.controlContainer3.Name = "controlContainer3";
      this.controlContainer3.Size = new System.Drawing.Size(1095, 109);
      this.controlContainer3.TabIndex = 0;
      // 
      // labelControl13
      // 
      this.labelControl13.Location = new System.Drawing.Point(4, 6);
      this.labelControl13.Name = "labelControl13";
      this.labelControl13.Size = new System.Drawing.Size(22, 13);
      this.labelControl13.TabIndex = 0;
      this.labelControl13.Text = "Port:";
      // 
      // labelControl15
      // 
      this.labelControl15.Location = new System.Drawing.Point(406, 6);
      this.labelControl15.Name = "labelControl15";
      this.labelControl15.Size = new System.Drawing.Size(50, 13);
      this.labelControl15.TabIndex = 4;
      this.labelControl15.Text = "Command:";
      // 
      // txtRconPort
      // 
      this.txtRconPort.Location = new System.Drawing.Point(49, 3);
      this.txtRconPort.MenuManager = this.barManager1;
      this.txtRconPort.Name = "txtRconPort";
      this.txtRconPort.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
      this.txtRconPort.Properties.Mask.EditMask = "\\d+";
      this.txtRconPort.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
      this.txtRconPort.Size = new System.Drawing.Size(83, 20);
      this.txtRconPort.TabIndex = 1;
      // 
      // barManager1
      // 
      this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barMenu});
      this.barManager1.Categories.AddRange(new DevExpress.XtraBars.BarManagerCategory[] {
            new DevExpress.XtraBars.BarManagerCategory("Server", new System.Guid("b1e08833-8d08-415c-9522-c31e9bf3c2de")),
            new DevExpress.XtraBars.BarManagerCategory("Player", new System.Guid("9969d11d-8a54-4afd-8aea-1c952f049e03")),
            new DevExpress.XtraBars.BarManagerCategory("Menu", new System.Guid("dce44941-9e20-4803-823c-6829c76924c5")),
            new DevExpress.XtraBars.BarManagerCategory("Rules", new System.Guid("e05a9c22-c1cf-4165-8204-c6e46b056a41")),
            new DevExpress.XtraBars.BarManagerCategory("Tabs", new System.Guid("158c025f-2f73-461e-bcf0-81b692caae52")),
            new DevExpress.XtraBars.BarManagerCategory("Details", new System.Guid("3381e451-df21-43f2-ab17-14610febee1a"))});
      this.barManager1.DockControls.Add(this.barDockControlTop);
      this.barManager1.DockControls.Add(this.barDockControlBottom);
      this.barManager1.DockControls.Add(this.barDockControlLeft);
      this.barManager1.DockControls.Add(this.barDockControlRight);
      this.barManager1.DockManager = this.dockManager1;
      this.barManager1.Form = this;
      this.barManager1.Images = this.imageCollection;
      this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.miUpdateServerInfo,
            this.miConnect,
            this.miConnectSpectator,
            this.miDelete,
            this.miCopyAddress,
            this.miQuickRefresh,
            this.miAddRulesColumnText,
            this.miAddRulesColumnNumeric,
            this.miPasteAddress,
            this.miFavServer,
            this.miShowOptions,
            this.miFindServers,
            this.miShowServerQuery,
            this.miRenameTab,
            this.miCloneTab,
            this.miCreateSnapshot,
            this.miUnfavServer,
            this.miAddMasterServerTab,
            this.miAddCustomServerTab,
            this.miNewFavoritesTab,
            this.mnuView,
            this.mnuTabs,
            this.mnuServer,
            this.mnuUpdate,
            this.miFindPlayer,
            this.miAddDetailColumn,
            this.miShowFilter,
            this.mnuGameOptions,
            this.miStopUpdate,
            this.barSubItem1,
            this.miAboutGithub,
            this.miAboutSteamWorkshop,
            this.miAboutVersionHistory,
            this.miRestoreStandardLayout,
            this.miSteamUrl,
            this.mnuGeoIpImpl,
            this.miGeoLite2,
            this.miIpApiCom});
      this.barManager1.MaxItemId = 39;
      this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riFindPlayer});
      // 
      // barMenu
      // 
      this.barMenu.BarItemHorzIndent = 5;
      this.barMenu.BarName = "barMenu";
      this.barMenu.DockCol = 0;
      this.barMenu.DockRow = 0;
      this.barMenu.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
      this.barMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.mnuView),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.miShowOptions, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.miShowServerQuery, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(this.miShowFilter),
            new DevExpress.XtraBars.LinkPersistInfo(this.mnuTabs, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.mnuUpdate, true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.miFindServers, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.miQuickRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.miStopUpdate),
            new DevExpress.XtraBars.LinkPersistInfo(this.mnuServer, true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.miConnect, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.miFindPlayer, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1)});
      this.barMenu.OptionsBar.AllowQuickCustomization = false;
      this.barMenu.OptionsBar.DisableClose = true;
      this.barMenu.OptionsBar.DisableCustomization = true;
      this.barMenu.OptionsBar.DrawDragBorder = false;
      this.barMenu.OptionsBar.UseWholeRow = true;
      this.barMenu.Text = "barMenu";
      // 
      // mnuView
      // 
      this.mnuView.Caption = "Options";
      this.mnuView.CategoryGuid = new System.Guid("dce44941-9e20-4803-823c-6829c76924c5");
      this.mnuView.Id = 21;
      this.mnuView.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.miShowOptions, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.miShowServerQuery, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.miShowFilter),
            new DevExpress.XtraBars.LinkPersistInfo(this.mnuGeoIpImpl),
            new DevExpress.XtraBars.LinkPersistInfo(this.miRestoreStandardLayout),
            new DevExpress.XtraBars.LinkPersistInfo(this.mnuGameOptions, true)});
      this.mnuView.Name = "mnuView";
      // 
      // miShowOptions
      // 
      this.miShowOptions.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
      this.miShowOptions.Caption = "General Preferences";
      this.miShowOptions.CategoryGuid = new System.Guid("dce44941-9e20-4803-823c-6829c76924c5");
      this.miShowOptions.Id = 9;
      this.miShowOptions.ImageOptions.ImageIndex = 4;
      this.miShowOptions.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P));
      this.miShowOptions.Name = "miShowOptions";
      this.miShowOptions.DownChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.miShowOptions_DownChanged);
      // 
      // miShowServerQuery
      // 
      this.miShowServerQuery.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
      this.miShowServerQuery.Caption = "Master Server Query/List Edit";
      this.miShowServerQuery.CategoryGuid = new System.Guid("dce44941-9e20-4803-823c-6829c76924c5");
      this.miShowServerQuery.Down = true;
      this.miShowServerQuery.Hint = "Show/hide steam master server query fields";
      this.miShowServerQuery.Id = 11;
      this.miShowServerQuery.ImageOptions.ImageIndex = 9;
      this.miShowServerQuery.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q));
      this.miShowServerQuery.Name = "miShowServerQuery";
      this.miShowServerQuery.DownChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.miServerQuery_DownChanged);
      // 
      // miShowFilter
      // 
      this.miShowFilter.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
      this.miShowFilter.Caption = "Quick Filter / Alert";
      this.miShowFilter.CategoryGuid = new System.Guid("dce44941-9e20-4803-823c-6829c76924c5");
      this.miShowFilter.Down = true;
      this.miShowFilter.Id = 27;
      this.miShowFilter.ImageOptions.ImageIndex = 13;
      this.miShowFilter.Name = "miShowFilter";
      this.miShowFilter.DownChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.miShowFilter_DownChanged);
      // 
      // miRestoreStandardLayout
      // 
      this.miRestoreStandardLayout.Caption = "Restore Standard Layout";
      this.miRestoreStandardLayout.Id = 34;
      this.miRestoreStandardLayout.Name = "miRestoreStandardLayout";
      this.miRestoreStandardLayout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miRestoreStandardLayout_ItemClick);
      // 
      // mnuGameOptions
      // 
      this.mnuGameOptions.Caption = "Game options";
      this.mnuGameOptions.CategoryGuid = new System.Guid("dce44941-9e20-4803-823c-6829c76924c5");
      this.mnuGameOptions.Id = 28;
      this.mnuGameOptions.Name = "mnuGameOptions";
      // 
      // mnuGeoIpImpl
      // 
      this.mnuGeoIpImpl.Caption = "GeoIP Service";
      this.mnuGeoIpImpl.Id = 36;
      this.mnuGeoIpImpl.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.miGeoLite2),
            new DevExpress.XtraBars.LinkPersistInfo(this.miIpApiCom)});
      this.mnuGeoIpImpl.Name = "mnuGeoIpImpl";
      // 
      // miGeoLite2
      // 
      this.miGeoLite2.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
      this.miGeoLite2.Caption = "MaxMind GeoLite2 offline database";
      this.miGeoLite2.GroupIndex = 1;
      this.miGeoLite2.Id = 37;
      this.miGeoLite2.Name = "miGeoLite2";
      this.miGeoLite2.Tag = "geolite2";
      this.miGeoLite2.DownChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.miGeoIpImpl_DownChanged);
      // 
      // miIpApiCom
      // 
      this.miIpApiCom.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
      this.miIpApiCom.Caption = "ip-api.com online API";
      this.miIpApiCom.GroupIndex = 1;
      this.miIpApiCom.Id = 38;
      this.miIpApiCom.Name = "miIpApiCom";
      this.miIpApiCom.Tag = "ip-api.com";
      this.miIpApiCom.DownChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.miGeoIpImpl_DownChanged);
      // 
      // mnuTabs
      // 
      this.mnuTabs.Caption = "Tabs";
      this.mnuTabs.CategoryGuid = new System.Guid("dce44941-9e20-4803-823c-6829c76924c5");
      this.mnuTabs.Id = 22;
      this.mnuTabs.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.miRenameTab, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.miCloneTab),
            new DevExpress.XtraBars.LinkPersistInfo(this.miCreateSnapshot),
            new DevExpress.XtraBars.LinkPersistInfo(this.miAddMasterServerTab, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.miAddCustomServerTab),
            new DevExpress.XtraBars.LinkPersistInfo(this.miNewFavoritesTab)});
      this.mnuTabs.Name = "mnuTabs";
      // 
      // miRenameTab
      // 
      this.miRenameTab.Caption = "Rename Tab";
      this.miRenameTab.CategoryGuid = new System.Guid("158c025f-2f73-461e-bcf0-81b692caae52");
      this.miRenameTab.Id = 12;
      this.miRenameTab.ImageOptions.ImageIndex = 11;
      this.miRenameTab.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2);
      this.miRenameTab.Name = "miRenameTab";
      this.miRenameTab.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miRenameTab_ItemClick);
      // 
      // miCloneTab
      // 
      this.miCloneTab.Caption = "Duplicate";
      this.miCloneTab.CategoryGuid = new System.Guid("158c025f-2f73-461e-bcf0-81b692caae52");
      this.miCloneTab.Id = 13;
      this.miCloneTab.ImageOptions.ImageIndex = 6;
      this.miCloneTab.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N));
      this.miCloneTab.Name = "miCloneTab";
      this.miCloneTab.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miCloneTab_ItemClick);
      // 
      // miCreateSnapshot
      // 
      this.miCreateSnapshot.Caption = "Copy to Custom List";
      this.miCreateSnapshot.CategoryGuid = new System.Guid("158c025f-2f73-461e-bcf0-81b692caae52");
      this.miCreateSnapshot.Hint = "Copies the currently visible servers to a new custom editable list";
      this.miCreateSnapshot.Id = 14;
      this.miCreateSnapshot.ImageOptions.ImageIndex = 12;
      this.miCreateSnapshot.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9);
      this.miCreateSnapshot.Name = "miCreateSnapshot";
      this.miCreateSnapshot.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miCreateSnapshot_ItemClick);
      // 
      // miAddMasterServerTab
      // 
      this.miAddMasterServerTab.Caption = "New Master Server Query";
      this.miAddMasterServerTab.CategoryGuid = new System.Guid("158c025f-2f73-461e-bcf0-81b692caae52");
      this.miAddMasterServerTab.Id = 18;
      this.miAddMasterServerTab.ImageOptions.ImageIndex = 0;
      this.miAddMasterServerTab.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F10);
      this.miAddMasterServerTab.Name = "miAddMasterServerTab";
      this.miAddMasterServerTab.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miAddMasterServerTab_ItemClick);
      // 
      // miAddCustomServerTab
      // 
      this.miAddCustomServerTab.Caption = "New Custom Server List";
      this.miAddCustomServerTab.CategoryGuid = new System.Guid("158c025f-2f73-461e-bcf0-81b692caae52");
      this.miAddCustomServerTab.Id = 19;
      this.miAddCustomServerTab.ImageOptions.ImageIndex = 12;
      this.miAddCustomServerTab.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F11);
      this.miAddCustomServerTab.Name = "miAddCustomServerTab";
      this.miAddCustomServerTab.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miAddCustomServerTab_ItemClick);
      // 
      // miNewFavoritesTab
      // 
      this.miNewFavoritesTab.Caption = "New Favorites Tab";
      this.miNewFavoritesTab.CategoryGuid = new System.Guid("158c025f-2f73-461e-bcf0-81b692caae52");
      this.miNewFavoritesTab.Id = 20;
      this.miNewFavoritesTab.ImageOptions.ImageIndex = 3;
      this.miNewFavoritesTab.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12);
      this.miNewFavoritesTab.Name = "miNewFavoritesTab";
      this.miNewFavoritesTab.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miNewFavoritesTab_ItemClick);
      // 
      // mnuUpdate
      // 
      this.mnuUpdate.Caption = "Server List";
      this.mnuUpdate.CategoryGuid = new System.Guid("dce44941-9e20-4803-823c-6829c76924c5");
      this.mnuUpdate.Id = 24;
      this.mnuUpdate.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.miFindServers, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.miQuickRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.miUpdateServerInfo),
            new DevExpress.XtraBars.LinkPersistInfo(this.miStopUpdate)});
      this.mnuUpdate.Name = "mnuUpdate";
      // 
      // miFindServers
      // 
      this.miFindServers.Caption = "Find Servers";
      this.miFindServers.CategoryGuid = new System.Guid("dce44941-9e20-4803-823c-6829c76924c5");
      this.miFindServers.Hint = "Get new server list from Steam Master Server ";
      this.miFindServers.Id = 10;
      this.miFindServers.ImageOptions.ImageIndex = 0;
      this.miFindServers.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4);
      this.miFindServers.Name = "miFindServers";
      this.miFindServers.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miFindServers_ItemClick);
      // 
      // miQuickRefresh
      // 
      this.miQuickRefresh.Caption = "Update Status";
      this.miQuickRefresh.CategoryGuid = new System.Guid("dce44941-9e20-4803-823c-6829c76924c5");
      this.miQuickRefresh.Hint = "Update status of all server in the list";
      this.miQuickRefresh.Id = 4;
      this.miQuickRefresh.ImageOptions.ImageIndex = 1;
      this.miQuickRefresh.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5);
      this.miQuickRefresh.Name = "miQuickRefresh";
      this.miQuickRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miQuickRefresh_ItemClick);
      // 
      // miUpdateServerInfo
      // 
      this.miUpdateServerInfo.Caption = "Update selected Servers";
      this.miUpdateServerInfo.CategoryGuid = new System.Guid("b1e08833-8d08-415c-9522-c31e9bf3c2de");
      this.miUpdateServerInfo.Hint = "Update status of currently selected servers";
      this.miUpdateServerInfo.Id = 3;
      this.miUpdateServerInfo.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6);
      this.miUpdateServerInfo.Name = "miUpdateServerInfo";
      this.miUpdateServerInfo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miUpdateServerInfo_ItemClick);
      // 
      // miStopUpdate
      // 
      this.miStopUpdate.Caption = "Stop Update";
      this.miStopUpdate.CategoryGuid = new System.Guid("dce44941-9e20-4803-823c-6829c76924c5");
      this.miStopUpdate.Enabled = false;
      this.miStopUpdate.Id = 29;
      this.miStopUpdate.ImageOptions.ImageIndex = 23;
      this.miStopUpdate.Name = "miStopUpdate";
      this.miStopUpdate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miStopUpdate_ItemClick);
      // 
      // mnuServer
      // 
      this.mnuServer.Caption = "Server";
      this.mnuServer.CategoryGuid = new System.Guid("dce44941-9e20-4803-823c-6829c76924c5");
      this.mnuServer.Id = 23;
      this.mnuServer.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.miConnect),
            new DevExpress.XtraBars.LinkPersistInfo(this.miConnectSpectator),
            new DevExpress.XtraBars.LinkPersistInfo(this.miCopyAddress, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.miPasteAddress),
            new DevExpress.XtraBars.LinkPersistInfo(this.miFavServer),
            new DevExpress.XtraBars.LinkPersistInfo(this.miUnfavServer),
            new DevExpress.XtraBars.LinkPersistInfo(this.miDelete)});
      this.mnuServer.Name = "mnuServer";
      // 
      // miConnect
      // 
      this.miConnect.Caption = "Connect";
      this.miConnect.CategoryGuid = new System.Guid("b1e08833-8d08-415c-9522-c31e9bf3c2de");
      this.miConnect.Id = 0;
      this.miConnect.ImageOptions.ImageIndex = 7;
      this.miConnect.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Enter));
      this.miConnect.Name = "miConnect";
      this.miConnect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miConnect_ItemClick);
      // 
      // miConnectSpectator
      // 
      this.miConnectSpectator.Caption = "Connect as Spectator";
      this.miConnectSpectator.CategoryGuid = new System.Guid("b1e08833-8d08-415c-9522-c31e9bf3c2de");
      this.miConnectSpectator.Id = 1;
      this.miConnectSpectator.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Enter));
      this.miConnectSpectator.Name = "miConnectSpectator";
      this.miConnectSpectator.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miConnectSpectator_ItemClick);
      // 
      // miCopyAddress
      // 
      this.miCopyAddress.Caption = "Copy Addresses to Clipboard";
      this.miCopyAddress.CategoryGuid = new System.Guid("b1e08833-8d08-415c-9522-c31e9bf3c2de");
      this.miCopyAddress.Id = 2;
      this.miCopyAddress.ImageOptions.ImageIndex = 6;
      this.miCopyAddress.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C));
      this.miCopyAddress.Name = "miCopyAddress";
      this.miCopyAddress.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miCopyAddress_ItemClick);
      // 
      // miPasteAddress
      // 
      this.miPasteAddress.Caption = "Paste Addresses from Clipboard";
      this.miPasteAddress.CategoryGuid = new System.Guid("b1e08833-8d08-415c-9522-c31e9bf3c2de");
      this.miPasteAddress.Id = 16;
      this.miPasteAddress.ImageOptions.ImageIndex = 17;
      this.miPasteAddress.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V));
      this.miPasteAddress.Name = "miPasteAddress";
      this.miPasteAddress.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miPasteAddress_ItemClick);
      // 
      // miFavServer
      // 
      this.miFavServer.Caption = "Add to Favorites";
      this.miFavServer.CategoryGuid = new System.Guid("b1e08833-8d08-415c-9522-c31e9bf3c2de");
      this.miFavServer.Id = 8;
      this.miFavServer.ImageOptions.ImageIndex = 3;
      this.miFavServer.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Add));
      this.miFavServer.Name = "miFavServer";
      this.miFavServer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miFavServer_ItemClick);
      // 
      // miUnfavServer
      // 
      this.miUnfavServer.Caption = "Remove from Favorites";
      this.miUnfavServer.CategoryGuid = new System.Guid("b1e08833-8d08-415c-9522-c31e9bf3c2de");
      this.miUnfavServer.Id = 15;
      this.miUnfavServer.ImageOptions.ImageIndex = 10;
      this.miUnfavServer.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Subtract));
      this.miUnfavServer.Name = "miUnfavServer";
      this.miUnfavServer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miUnfavServer_ItemClick);
      // 
      // miDelete
      // 
      this.miDelete.Caption = "Remove from List";
      this.miDelete.CategoryGuid = new System.Guid("b1e08833-8d08-415c-9522-c31e9bf3c2de");
      this.miDelete.Id = 17;
      this.miDelete.ImageOptions.ImageIndex = 18;
      this.miDelete.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D));
      this.miDelete.Name = "miDelete";
      this.miDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miDelete_ItemClick);
      // 
      // miFindPlayer
      // 
      this.miFindPlayer.Caption = "Find Buddy:";
      this.miFindPlayer.CategoryGuid = new System.Guid("dce44941-9e20-4803-823c-6829c76924c5");
      this.miFindPlayer.Edit = this.riFindPlayer;
      this.miFindPlayer.EditWidth = 244;
      this.miFindPlayer.Id = 25;
      this.miFindPlayer.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3);
      this.miFindPlayer.Name = "miFindPlayer";
      this.miFindPlayer.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
      // 
      // riFindPlayer
      // 
      this.riFindPlayer.AutoHeight = false;
      this.riFindPlayer.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "Add to list", null, null, DevExpress.Utils.ToolTipAnchor.Default),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Minus, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "Remove from list", null, null, DevExpress.Utils.ToolTipAnchor.Default),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search, "", -1, true, true, false, editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject9, serializableAppearanceObject10, serializableAppearanceObject11, serializableAppearanceObject12, "Find", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
      this.riFindPlayer.Name = "riFindPlayer";
      this.riFindPlayer.NullValuePrompt = "min 3 chars, * as placeholder";
      this.riFindPlayer.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riFindPlayer_ButtonPressed);
      this.riFindPlayer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.riFindPlayer_KeyDown);
      // 
      // barSubItem1
      // 
      this.barSubItem1.Caption = "About";
      this.barSubItem1.CategoryGuid = new System.Guid("dce44941-9e20-4803-823c-6829c76924c5");
      this.barSubItem1.Id = 30;
      this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.miAboutGithub),
            new DevExpress.XtraBars.LinkPersistInfo(this.miAboutSteamWorkshop),
            new DevExpress.XtraBars.LinkPersistInfo(this.miAboutVersionHistory)});
      this.barSubItem1.Name = "barSubItem1";
      // 
      // miAboutGithub
      // 
      this.miAboutGithub.Caption = "Project Page on github.com";
      this.miAboutGithub.CategoryGuid = new System.Guid("dce44941-9e20-4803-823c-6829c76924c5");
      this.miAboutGithub.Id = 31;
      this.miAboutGithub.Name = "miAboutGithub";
      this.miAboutGithub.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miAboutGithub_ItemClick);
      // 
      // miAboutSteamWorkshop
      // 
      this.miAboutSteamWorkshop.Caption = "Steam Workshop Item Page";
      this.miAboutSteamWorkshop.CategoryGuid = new System.Guid("dce44941-9e20-4803-823c-6829c76924c5");
      this.miAboutSteamWorkshop.Id = 32;
      this.miAboutSteamWorkshop.Name = "miAboutSteamWorkshop";
      this.miAboutSteamWorkshop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miAboutSteamWorkshop_ItemClick);
      // 
      // miAboutVersionHistory
      // 
      this.miAboutVersionHistory.Caption = "Version History";
      this.miAboutVersionHistory.Id = 33;
      this.miAboutVersionHistory.Name = "miAboutVersionHistory";
      this.miAboutVersionHistory.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miAboutVersionHistory_ItemClick);
      // 
      // barDockControlTop
      // 
      this.barDockControlTop.CausesValidation = false;
      this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
      this.barDockControlTop.Manager = this.barManager1;
      this.barDockControlTop.Size = new System.Drawing.Size(1658, 29);
      // 
      // barDockControlBottom
      // 
      this.barDockControlBottom.CausesValidation = false;
      this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.barDockControlBottom.Location = new System.Drawing.Point(0, 802);
      this.barDockControlBottom.Manager = this.barManager1;
      this.barDockControlBottom.Size = new System.Drawing.Size(1658, 0);
      // 
      // barDockControlLeft
      // 
      this.barDockControlLeft.CausesValidation = false;
      this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
      this.barDockControlLeft.Location = new System.Drawing.Point(0, 29);
      this.barDockControlLeft.Manager = this.barManager1;
      this.barDockControlLeft.Size = new System.Drawing.Size(0, 773);
      // 
      // barDockControlRight
      // 
      this.barDockControlRight.CausesValidation = false;
      this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
      this.barDockControlRight.Location = new System.Drawing.Point(1658, 29);
      this.barDockControlRight.Manager = this.barManager1;
      this.barDockControlRight.Size = new System.Drawing.Size(0, 773);
      // 
      // miAddRulesColumnText
      // 
      this.miAddRulesColumnText.Caption = "Add as text column to Servers table";
      this.miAddRulesColumnText.CategoryGuid = new System.Guid("e05a9c22-c1cf-4165-8204-c6e46b056a41");
      this.miAddRulesColumnText.Id = 5;
      this.miAddRulesColumnText.Name = "miAddRulesColumnText";
      this.miAddRulesColumnText.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miAddRulesColumnString_ItemClick);
      // 
      // miAddRulesColumnNumeric
      // 
      this.miAddRulesColumnNumeric.Caption = "Add as numeric column to Servers table";
      this.miAddRulesColumnNumeric.CategoryGuid = new System.Guid("e05a9c22-c1cf-4165-8204-c6e46b056a41");
      this.miAddRulesColumnNumeric.Id = 6;
      this.miAddRulesColumnNumeric.Name = "miAddRulesColumnNumeric";
      this.miAddRulesColumnNumeric.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miAddRulesColumnNumeric_ItemClick);
      // 
      // miAddDetailColumn
      // 
      this.miAddDetailColumn.Caption = "Add as column to server list";
      this.miAddDetailColumn.CategoryGuid = new System.Guid("3381e451-df21-43f2-ab17-14610febee1a");
      this.miAddDetailColumn.Id = 26;
      this.miAddDetailColumn.Name = "miAddDetailColumn";
      this.miAddDetailColumn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miAddDetailColumnString_ItemClick);
      // 
      // miSteamUrl
      // 
      this.miSteamUrl.Caption = "Copy steam://... URL to Clipboard";
      this.miSteamUrl.Id = 35;
      this.miSteamUrl.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U));
      this.miSteamUrl.Name = "miSteamUrl";
      this.miSteamUrl.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miSteamUrl_ItemClick);
      // 
      // txtRconConsole
      // 
      this.txtRconConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtRconConsole.Location = new System.Drawing.Point(-2, 26);
      this.txtRconConsole.MenuManager = this.barManager1;
      this.txtRconConsole.Name = "txtRconConsole";
      this.txtRconConsole.Properties.ReadOnly = true;
      this.txtRconConsole.Size = new System.Drawing.Size(1094, 81);
      this.txtRconConsole.TabIndex = 6;
      // 
      // txtRconCommand
      // 
      this.txtRconCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtRconCommand.Location = new System.Drawing.Point(481, 3);
      this.txtRconCommand.MenuManager = this.barManager1;
      this.txtRconCommand.Name = "txtRconCommand";
      this.txtRconCommand.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
      this.txtRconCommand.Size = new System.Drawing.Size(611, 20);
      this.txtRconCommand.TabIndex = 5;
      this.txtRconCommand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRconCommand_KeyDown);
      // 
      // labelControl14
      // 
      this.labelControl14.Location = new System.Drawing.Point(161, 6);
      this.labelControl14.Name = "labelControl14";
      this.labelControl14.Size = new System.Drawing.Size(49, 13);
      this.labelControl14.TabIndex = 2;
      this.labelControl14.Text = "Password:";
      // 
      // txtRconPassword
      // 
      this.txtRconPassword.Location = new System.Drawing.Point(230, 3);
      this.txtRconPassword.MenuManager = this.barManager1;
      this.txtRconPassword.Name = "txtRconPassword";
      this.txtRconPassword.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
      this.txtRconPassword.Size = new System.Drawing.Size(143, 20);
      this.txtRconPassword.TabIndex = 3;
      // 
      // panelTop
      // 
      this.panelTop.Controls.Add(this.controlContainer4);
      this.panelTop.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top;
      this.panelTop.ID = new System.Guid("0a630ddd-189d-4595-a405-8929fe2a1442");
      this.panelTop.Location = new System.Drawing.Point(0, 29);
      this.panelTop.Name = "panelTop";
      this.panelTop.Options.AllowDockFill = false;
      this.panelTop.Options.AllowDockLeft = false;
      this.panelTop.Options.AllowDockRight = false;
      this.panelTop.Options.AllowFloating = false;
      this.panelTop.Options.FloatOnDblClick = false;
      this.panelTop.Options.ResizeDirection = DevExpress.XtraBars.Docking.Helpers.ResizeDirection.None;
      this.panelTop.Options.ShowCloseButton = false;
      this.panelTop.Options.ShowMaximizeButton = false;
      this.panelTop.OriginalSize = new System.Drawing.Size(200, 307);
      this.panelTop.Size = new System.Drawing.Size(1658, 307);
      this.panelTop.Text = "Options";
      // 
      // controlContainer4
      // 
      this.controlContainer4.Controls.Add(this.panelStaticList);
      this.controlContainer4.Controls.Add(this.panelQuery);
      this.controlContainer4.Controls.Add(this.panelTabs);
      this.controlContainer4.Controls.Add(this.panelOptions);
      this.controlContainer4.Location = new System.Drawing.Point(4, 25);
      this.controlContainer4.Name = "controlContainer4";
      this.controlContainer4.Size = new System.Drawing.Size(1650, 278);
      this.controlContainer4.TabIndex = 0;
      // 
      // panelStaticList
      // 
      this.panelStaticList.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
      this.panelStaticList.Controls.Add(this.labelControl18);
      this.panelStaticList.Controls.Add(this.btnPasteAddresses);
      this.panelStaticList.Controls.Add(this.txtGameServer);
      this.panelStaticList.Controls.Add(this.labelControl6);
      this.panelStaticList.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelStaticList.Location = new System.Drawing.Point(0, 203);
      this.panelStaticList.Name = "panelStaticList";
      this.panelStaticList.Size = new System.Drawing.Size(1650, 64);
      this.panelStaticList.TabIndex = 3;
      this.panelStaticList.Visible = false;
      // 
      // labelControl18
      // 
      this.labelControl18.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.labelControl18.Appearance.Options.UseFont = true;
      this.labelControl18.Location = new System.Drawing.Point(17, 37);
      this.labelControl18.Name = "labelControl18";
      this.labelControl18.Size = new System.Drawing.Size(751, 15);
      this.labelControl18.TabIndex = 18;
      this.labelControl18.Text = "You can copy servers addresses (ip:value) from the other tabs or any text editor." +
    " Separate them as lines, with space, comma, semicolon or pipe.";
      // 
      // btnPasteAddresses
      // 
      this.btnPasteAddresses.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.btnPasteAddresses.Appearance.Options.UseFont = true;
      this.btnPasteAddresses.ImageOptions.ImageIndex = 17;
      this.btnPasteAddresses.ImageOptions.ImageList = this.imageCollection;
      this.btnPasteAddresses.Location = new System.Drawing.Point(401, 6);
      this.btnPasteAddresses.Name = "btnPasteAddresses";
      this.btnPasteAddresses.Size = new System.Drawing.Size(213, 25);
      this.btnPasteAddresses.TabIndex = 17;
      this.btnPasteAddresses.Text = "Paste Servers from Clipboard";
      this.btnPasteAddresses.ToolTip = "Paste a list of ip:port values from the clipboard.";
      this.btnPasteAddresses.Click += new System.EventHandler(this.btnPasteAddresses_Click);
      // 
      // txtGameServer
      // 
      this.txtGameServer.Location = new System.Drawing.Point(117, 8);
      this.txtGameServer.MenuManager = this.barManager1;
      this.txtGameServer.Name = "txtGameServer";
      this.txtGameServer.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.txtGameServer.Properties.Appearance.Options.UseFont = true;
      this.txtGameServer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)});
      this.txtGameServer.Size = new System.Drawing.Size(268, 22);
      this.txtGameServer.TabIndex = 1;
      this.txtGameServer.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtGameServer_ButtonClick);
      this.txtGameServer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGameServer_KeyDown);
      // 
      // labelControl6
      // 
      this.labelControl6.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.labelControl6.Appearance.Options.UseFont = true;
      this.labelControl6.Location = new System.Drawing.Point(17, 11);
      this.labelControl6.Name = "labelControl6";
      this.labelControl6.Size = new System.Drawing.Size(94, 15);
      this.labelControl6.TabIndex = 0;
      this.labelControl6.Text = "Add Game Server:";
      // 
      // panelQuery
      // 
      this.panelQuery.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
      this.panelQuery.Controls.Add(this.txtVersion);
      this.panelQuery.Controls.Add(this.labelControl20);
      this.panelQuery.Controls.Add(this.txtFilterInfoMaster);
      this.panelQuery.Controls.Add(this.btnUpdateStatus);
      this.panelQuery.Controls.Add(this.comboQueryLimit);
      this.panelQuery.Controls.Add(this.labelControl16);
      this.panelQuery.Controls.Add(this.cbGetFull);
      this.panelQuery.Controls.Add(this.txtMod);
      this.panelQuery.Controls.Add(this.comboGames);
      this.panelQuery.Controls.Add(this.labelControl2);
      this.panelQuery.Controls.Add(this.labelControl12);
      this.panelQuery.Controls.Add(this.btnUpdateList);
      this.panelQuery.Controls.Add(this.labelControl8);
      this.panelQuery.Controls.Add(this.txtMap);
      this.panelQuery.Controls.Add(this.labelControl4);
      this.panelQuery.Controls.Add(this.labelControl11);
      this.panelQuery.Controls.Add(this.cbGetEmpty);
      this.panelQuery.Controls.Add(this.labelControl7);
      this.panelQuery.Controls.Add(this.txtTagExcludeServer);
      this.panelQuery.Controls.Add(this.comboMasterServer);
      this.panelQuery.Controls.Add(this.txtTagIncludeServer);
      this.panelQuery.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelQuery.Location = new System.Drawing.Point(0, 139);
      this.panelQuery.Name = "panelQuery";
      this.panelQuery.Size = new System.Drawing.Size(1650, 64);
      this.panelQuery.TabIndex = 2;
      // 
      // txtVersion
      // 
      this.txtVersion.Location = new System.Drawing.Point(1086, 37);
      this.txtVersion.MenuManager = this.barManager1;
      this.txtVersion.Name = "txtVersion";
      this.txtVersion.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.txtVersion.Properties.Appearance.Options.UseFont = true;
      this.txtVersion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
      this.txtVersion.Size = new System.Drawing.Size(53, 22);
      this.txtVersion.TabIndex = 20;
      this.txtVersion.ToolTip = "Tags are filtered by the Master Server to shorten the list of game servers.";
      // 
      // labelControl20
      // 
      this.labelControl20.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.labelControl20.Appearance.Options.UseFont = true;
      this.labelControl20.Location = new System.Drawing.Point(1010, 39);
      this.labelControl20.Name = "labelControl20";
      this.labelControl20.Size = new System.Drawing.Size(42, 15);
      this.labelControl20.TabIndex = 19;
      this.labelControl20.Text = "Version:";
      // 
      // txtFilterInfoMaster
      // 
      this.txtFilterInfoMaster.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.txtFilterInfoMaster.Appearance.Options.UseFont = true;
      this.txtFilterInfoMaster.Location = new System.Drawing.Point(1292, 5);
      this.txtFilterInfoMaster.Name = "txtFilterInfoMaster";
      this.txtFilterInfoMaster.Size = new System.Drawing.Size(342, 52);
      this.txtFilterInfoMaster.TabIndex = 18;
      this.txtFilterInfoMaster.Text = resources.GetString("txtFilterInfoMaster.Text");
      // 
      // btnUpdateStatus
      // 
      this.btnUpdateStatus.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.btnUpdateStatus.Appearance.Options.UseFont = true;
      this.btnUpdateStatus.ImageOptions.ImageIndex = 1;
      this.btnUpdateStatus.ImageOptions.ImageList = this.imageCollection;
      this.btnUpdateStatus.Location = new System.Drawing.Point(1160, 36);
      this.btnUpdateStatus.Name = "btnUpdateStatus";
      this.btnUpdateStatus.Size = new System.Drawing.Size(115, 25);
      this.btnUpdateStatus.TabIndex = 17;
      this.btnUpdateStatus.Text = "Update Status";
      this.btnUpdateStatus.ToolTip = "Update status of the servers in the current list";
      this.btnUpdateStatus.Click += new System.EventHandler(this.btnUpdateStatus_Click);
      // 
      // comboQueryLimit
      // 
      this.comboQueryLimit.EditValue = "1000";
      this.comboQueryLimit.Location = new System.Drawing.Point(1086, 8);
      this.comboQueryLimit.MenuManager = this.barManager1;
      this.comboQueryLimit.Name = "comboQueryLimit";
      this.comboQueryLimit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.comboQueryLimit.Properties.Appearance.Options.UseFont = true;
      this.comboQueryLimit.Properties.Appearance.Options.UseTextOptions = true;
      this.comboQueryLimit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
      this.comboQueryLimit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.comboQueryLimit.Properties.Items.AddRange(new object[] {
            "500",
            "1000",
            "2000",
            "6930",
            "10000",
            "100000"});
      this.comboQueryLimit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
      this.comboQueryLimit.Size = new System.Drawing.Size(53, 22);
      this.comboQueryLimit.TabIndex = 15;
      this.comboQueryLimit.ToolTip = "The Steam Master Server throttles each client IP to 30 data packets per minute (m" +
    "ax 6930 servers).\r\nWhen you reach that limit, the server will ignore your reques" +
    "ts for the next 60 sec.";
      // 
      // labelControl16
      // 
      this.labelControl16.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.labelControl16.Appearance.Options.UseFont = true;
      this.labelControl16.Location = new System.Drawing.Point(1010, 12);
      this.labelControl16.Name = "labelControl16";
      this.labelControl16.Size = new System.Drawing.Size(70, 15);
      this.labelControl16.TabIndex = 14;
      this.labelControl16.Text = "Limit Results:";
      // 
      // cbGetFull
      // 
      this.cbGetFull.EditValue = true;
      this.cbGetFull.Location = new System.Drawing.Point(871, 38);
      this.cbGetFull.Name = "cbGetFull";
      this.cbGetFull.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.cbGetFull.Properties.Appearance.Options.UseFont = true;
      this.cbGetFull.Properties.AutoWidth = true;
      this.cbGetFull.Properties.Caption = "Get full servers";
      this.cbGetFull.Size = new System.Drawing.Size(99, 19);
      this.cbGetFull.TabIndex = 13;
      // 
      // txtMod
      // 
      this.txtMod.Location = new System.Drawing.Point(436, 7);
      this.txtMod.MenuManager = this.barManager1;
      this.txtMod.Name = "txtMod";
      this.txtMod.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.txtMod.Properties.Appearance.Options.UseFont = true;
      this.txtMod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
      this.txtMod.Size = new System.Drawing.Size(178, 22);
      this.txtMod.TabIndex = 5;
      this.txtMod.ToolTip = "Matches the \"directory\" field of the server details";
      this.txtMod.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtTag_ButtonClick);
      // 
      // comboGames
      // 
      this.comboGames.Location = new System.Drawing.Point(117, 36);
      this.comboGames.Name = "comboGames";
      this.comboGames.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.comboGames.Properties.Appearance.Options.UseFont = true;
      this.comboGames.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.comboGames.Properties.DropDownRows = 30;
      this.comboGames.Properties.NullValuePrompt = "Select game or enter numeric Steam AppID";
      this.comboGames.Size = new System.Drawing.Size(268, 22);
      this.comboGames.TabIndex = 3;
      this.comboGames.SelectedIndexChanged += new System.EventHandler(this.comboGames_SelectedIndexChanged);
      // 
      // labelControl2
      // 
      this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.labelControl2.Appearance.Options.UseFont = true;
      this.labelControl2.Location = new System.Drawing.Point(640, 12);
      this.labelControl2.Name = "labelControl2";
      this.labelControl2.Size = new System.Drawing.Size(69, 15);
      this.labelControl2.TabIndex = 8;
      this.labelControl2.Text = "Include Tags:";
      // 
      // labelControl12
      // 
      this.labelControl12.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.labelControl12.Appearance.Options.UseFont = true;
      this.labelControl12.Location = new System.Drawing.Point(401, 11);
      this.labelControl12.Name = "labelControl12";
      this.labelControl12.Size = new System.Drawing.Size(28, 15);
      this.labelControl12.TabIndex = 4;
      this.labelControl12.Text = "Mod:";
      // 
      // btnUpdateList
      // 
      this.btnUpdateList.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.btnUpdateList.Appearance.Options.UseFont = true;
      this.btnUpdateList.ImageOptions.ImageIndex = 0;
      this.btnUpdateList.ImageOptions.ImageList = this.imageCollection;
      this.btnUpdateList.Location = new System.Drawing.Point(1160, 7);
      this.btnUpdateList.Name = "btnUpdateList";
      this.btnUpdateList.Size = new System.Drawing.Size(115, 25);
      this.btnUpdateList.TabIndex = 16;
      this.btnUpdateList.Text = "Find Servers";
      this.btnUpdateList.ToolTip = "Get new server list from Valve master server";
      this.btnUpdateList.Click += new System.EventHandler(this.btnUpdateList_Click);
      // 
      // labelControl8
      // 
      this.labelControl8.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.labelControl8.Appearance.Options.UseFont = true;
      this.labelControl8.Location = new System.Drawing.Point(639, 41);
      this.labelControl8.Name = "labelControl8";
      this.labelControl8.Size = new System.Drawing.Size(71, 15);
      this.labelControl8.TabIndex = 10;
      this.labelControl8.Text = "Exclude Tags:";
      // 
      // txtMap
      // 
      this.txtMap.Location = new System.Drawing.Point(436, 36);
      this.txtMap.MenuManager = this.barManager1;
      this.txtMap.Name = "txtMap";
      this.txtMap.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.txtMap.Properties.Appearance.Options.UseFont = true;
      this.txtMap.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
      this.txtMap.Size = new System.Drawing.Size(178, 22);
      this.txtMap.TabIndex = 7;
      this.txtMap.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtTag_ButtonClick);
      // 
      // labelControl4
      // 
      this.labelControl4.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.labelControl4.Appearance.Options.UseFont = true;
      this.labelControl4.Location = new System.Drawing.Point(77, 39);
      this.labelControl4.Name = "labelControl4";
      this.labelControl4.Size = new System.Drawing.Size(34, 15);
      this.labelControl4.TabIndex = 2;
      this.labelControl4.Text = "Game:";
      this.labelControl4.ToolTip = "If a game is not listed here, you can enter it\'s Steam ApplicationID here";
      // 
      // labelControl11
      // 
      this.labelControl11.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.labelControl11.Appearance.Options.UseFont = true;
      this.labelControl11.Location = new System.Drawing.Point(401, 40);
      this.labelControl11.Name = "labelControl11";
      this.labelControl11.Size = new System.Drawing.Size(27, 15);
      this.labelControl11.TabIndex = 6;
      this.labelControl11.Text = "Map:";
      // 
      // cbGetEmpty
      // 
      this.cbGetEmpty.EditValue = true;
      this.cbGetEmpty.Location = new System.Drawing.Point(871, 9);
      this.cbGetEmpty.Name = "cbGetEmpty";
      this.cbGetEmpty.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.cbGetEmpty.Properties.Appearance.Options.UseFont = true;
      this.cbGetEmpty.Properties.AutoWidth = true;
      this.cbGetEmpty.Properties.Caption = "Get empty servers";
      this.cbGetEmpty.Size = new System.Drawing.Size(116, 19);
      this.cbGetEmpty.TabIndex = 12;
      // 
      // labelControl7
      // 
      this.labelControl7.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.labelControl7.Appearance.Options.UseFont = true;
      this.labelControl7.Location = new System.Drawing.Point(38, 11);
      this.labelControl7.Name = "labelControl7";
      this.labelControl7.Size = new System.Drawing.Size(73, 15);
      this.labelControl7.TabIndex = 0;
      this.labelControl7.Text = "Master server:";
      this.labelControl7.ToolTip = "ip:port of a master server \r\nor a 32 hex digit Steam Web API key";
      // 
      // txtTagExcludeServer
      // 
      this.txtTagExcludeServer.Location = new System.Drawing.Point(716, 37);
      this.txtTagExcludeServer.MenuManager = this.barManager1;
      this.txtTagExcludeServer.Name = "txtTagExcludeServer";
      this.txtTagExcludeServer.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.txtTagExcludeServer.Properties.Appearance.Options.UseFont = true;
      this.txtTagExcludeServer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
      this.txtTagExcludeServer.Properties.NullValuePrompt = "space/comma=AND";
      this.txtTagExcludeServer.Size = new System.Drawing.Size(133, 22);
      this.txtTagExcludeServer.TabIndex = 11;
      this.txtTagExcludeServer.ToolTip = "Tags are filtered by the Master Server to shorten the list of game servers.";
      this.txtTagExcludeServer.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtTag_ButtonClick);
      // 
      // comboMasterServer
      // 
      this.comboMasterServer.Location = new System.Drawing.Point(117, 8);
      this.comboMasterServer.Name = "comboMasterServer";
      this.comboMasterServer.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.comboMasterServer.Properties.Appearance.Options.UseFont = true;
      this.comboMasterServer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.comboMasterServer.Size = new System.Drawing.Size(268, 22);
      this.comboMasterServer.TabIndex = 1;
      // 
      // txtTagIncludeServer
      // 
      this.txtTagIncludeServer.Location = new System.Drawing.Point(716, 8);
      this.txtTagIncludeServer.MenuManager = this.barManager1;
      this.txtTagIncludeServer.Name = "txtTagIncludeServer";
      this.txtTagIncludeServer.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.txtTagIncludeServer.Properties.Appearance.Options.UseFont = true;
      this.txtTagIncludeServer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
      this.txtTagIncludeServer.Properties.NullValuePrompt = "space/comma=AND";
      this.txtTagIncludeServer.Size = new System.Drawing.Size(133, 22);
      this.txtTagIncludeServer.TabIndex = 9;
      this.txtTagIncludeServer.ToolTip = "Tags are filtered by the Master Server to shorten the list of game servers.";
      this.txtTagIncludeServer.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtTag_ButtonClick);
      // 
      // panelTabs
      // 
      this.panelTabs.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
      this.panelTabs.Controls.Add(this.tabControl);
      this.panelTabs.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelTabs.Location = new System.Drawing.Point(0, 102);
      this.panelTabs.Name = "panelTabs";
      this.panelTabs.Size = new System.Drawing.Size(1650, 37);
      this.panelTabs.TabIndex = 1;
      // 
      // tabControl
      // 
      this.tabControl.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
      this.tabControl.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.tabControl.HeaderButtons = ((DevExpress.XtraTab.TabButtons)((((DevExpress.XtraTab.TabButtons.Prev | DevExpress.XtraTab.TabButtons.Next) 
            | DevExpress.XtraTab.TabButtons.Close) 
            | DevExpress.XtraTab.TabButtons.Default)));
      this.tabControl.Images = this.imageCollection;
      this.tabControl.Location = new System.Drawing.Point(0, 9);
      this.tabControl.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
      this.tabControl.MultiLine = DevExpress.Utils.DefaultBoolean.False;
      this.tabControl.Name = "tabControl";
      this.tabControl.SelectedTabPage = this.tabGame;
      this.tabControl.Size = new System.Drawing.Size(1650, 28);
      this.tabControl.TabIndex = 0;
      this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabGame,
            this.tabFavorites,
            this.tabAdd});
      this.tabControl.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabControl_SelectedPageChanged);
      this.tabControl.SelectedPageChanging += new DevExpress.XtraTab.TabPageChangingEventHandler(this.tabControl_SelectedPageChanging);
      this.tabControl.CloseButtonClick += new System.EventHandler(this.tabControl_CloseButtonClick);
      this.tabControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl_MouseDown);
      this.tabControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tabControl_MouseMove);
      this.tabControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tabControl_MouseUp);
      // 
      // tabGame
      // 
      this.tabGame.ImageOptions.ImageIndex = 0;
      this.tabGame.Name = "tabGame";
      this.tabGame.Size = new System.Drawing.Size(1644, 0);
      this.tabGame.Text = "Master Server Query";
      // 
      // tabFavorites
      // 
      this.tabFavorites.ImageOptions.ImageIndex = 3;
      this.tabFavorites.Name = "tabFavorites";
      this.tabFavorites.Size = new System.Drawing.Size(1644, 0);
      this.tabFavorites.Text = "Favorites";
      // 
      // tabAdd
      // 
      this.tabAdd.ImageOptions.ImageIndex = 14;
      this.tabAdd.Name = "tabAdd";
      this.tabAdd.ShowCloseButton = DevExpress.Utils.DefaultBoolean.False;
      this.tabAdd.Size = new System.Drawing.Size(1644, 0);
      // 
      // panelOptions
      // 
      this.panelOptions.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
      this.panelOptions.Controls.Add(this.cbHideGhosts);
      this.panelOptions.Controls.Add(this.cbConnectOnDoubleClick);
      this.panelOptions.Controls.Add(this.cbUseSteamApi);
      this.panelOptions.Controls.Add(this.cbShowCounts);
      this.panelOptions.Controls.Add(this.cbShowFilterPanelInfo);
      this.panelOptions.Controls.Add(this.labelControl19);
      this.panelOptions.Controls.Add(this.cbNoUpdateWhilePlaying);
      this.panelOptions.Controls.Add(this.cbHideUnresponsiveServers);
      this.panelOptions.Controls.Add(this.rbUpdateStatusOnly);
      this.panelOptions.Controls.Add(this.cbFavServersOnTop);
      this.panelOptions.Controls.Add(this.rbAddressGamePort);
      this.panelOptions.Controls.Add(this.rbAddressQueryPort);
      this.panelOptions.Controls.Add(this.labelControl10);
      this.panelOptions.Controls.Add(this.labelControl9);
      this.panelOptions.Controls.Add(this.btnSkin);
      this.panelOptions.Controls.Add(this.cbRefreshSelectedServer);
      this.panelOptions.Controls.Add(this.rbAddressHidden);
      this.panelOptions.Controls.Add(this.spinRefreshInterval);
      this.panelOptions.Controls.Add(this.rbUpdateListAndStatus);
      this.panelOptions.Controls.Add(this.rbUpdateDisabled);
      this.panelOptions.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelOptions.Location = new System.Drawing.Point(0, 0);
      this.panelOptions.Name = "panelOptions";
      this.panelOptions.Size = new System.Drawing.Size(1650, 102);
      this.panelOptions.TabIndex = 0;
      this.panelOptions.Visible = false;
      // 
      // cbHideGhosts
      // 
      this.cbHideGhosts.EditValue = true;
      this.cbHideGhosts.Location = new System.Drawing.Point(796, 72);
      this.cbHideGhosts.Name = "cbHideGhosts";
      this.cbHideGhosts.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.cbHideGhosts.Properties.Appearance.Options.UseFont = true;
      this.cbHideGhosts.Properties.AutoWidth = true;
      this.cbHideGhosts.Properties.Caption = "Hide ghost players";
      this.cbHideGhosts.Size = new System.Drawing.Size(120, 19);
      this.cbHideGhosts.TabIndex = 19;
      this.cbHideGhosts.ToolTip = "Game server sometimes report players who are not really on the server.\r\nSuch play" +
    "ers can be hidden when there is an alternative way to query information.";
      this.cbHideGhosts.CheckedChanged += new System.EventHandler(this.cbHideGhosts_CheckedChanged);
      // 
      // cbConnectOnDoubleClick
      // 
      this.cbConnectOnDoubleClick.EditValue = true;
      this.cbConnectOnDoubleClick.Location = new System.Drawing.Point(796, 53);
      this.cbConnectOnDoubleClick.Name = "cbConnectOnDoubleClick";
      this.cbConnectOnDoubleClick.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.cbConnectOnDoubleClick.Properties.Appearance.Options.UseFont = true;
      this.cbConnectOnDoubleClick.Properties.AutoWidth = true;
      this.cbConnectOnDoubleClick.Properties.Caption = "Use double-click to connect to a game server";
      this.cbConnectOnDoubleClick.Size = new System.Drawing.Size(260, 19);
      this.cbConnectOnDoubleClick.TabIndex = 18;
      // 
      // cbUseSteamApi
      // 
      this.cbUseSteamApi.Location = new System.Drawing.Point(327, 72);
      this.cbUseSteamApi.Name = "cbUseSteamApi";
      this.cbUseSteamApi.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.cbUseSteamApi.Properties.Appearance.Options.UseFont = true;
      this.cbUseSteamApi.Properties.AutoWidth = true;
      this.cbUseSteamApi.Properties.Caption = "Use Steam API to detect in-game status";
      this.cbUseSteamApi.Size = new System.Drawing.Size(230, 19);
      this.cbUseSteamApi.TabIndex = 10;
      this.cbUseSteamApi.ToolTip = "If you experience extremely low FPS when you start the game, disable this option." +
    "\r\nYou will then have to manually refresh your server list when you return from y" +
    "our game.";
      // 
      // cbShowCounts
      // 
      this.cbShowCounts.EditValue = true;
      this.cbShowCounts.Location = new System.Drawing.Point(796, 32);
      this.cbShowCounts.Name = "cbShowCounts";
      this.cbShowCounts.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.cbShowCounts.Properties.Appearance.Options.UseFont = true;
      this.cbShowCounts.Properties.AutoWidth = true;
      this.cbShowCounts.Properties.Caption = "Show counts in the list footer";
      this.cbShowCounts.Size = new System.Drawing.Size(176, 19);
      this.cbShowCounts.TabIndex = 17;
      this.cbShowCounts.CheckedChanged += new System.EventHandler(this.cbShowCounts_CheckedChanged);
      // 
      // cbShowFilterPanelInfo
      // 
      this.cbShowFilterPanelInfo.EditValue = true;
      this.cbShowFilterPanelInfo.Location = new System.Drawing.Point(796, 13);
      this.cbShowFilterPanelInfo.Name = "cbShowFilterPanelInfo";
      this.cbShowFilterPanelInfo.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.cbShowFilterPanelInfo.Properties.Appearance.Options.UseFont = true;
      this.cbShowFilterPanelInfo.Properties.AutoWidth = true;
      this.cbShowFilterPanelInfo.Properties.Caption = "Show filter panel information text";
      this.cbShowFilterPanelInfo.Size = new System.Drawing.Size(199, 19);
      this.cbShowFilterPanelInfo.TabIndex = 16;
      this.cbShowFilterPanelInfo.CheckedChanged += new System.EventHandler(this.cbShowFilterPanelInfo_CheckedChanged);
      // 
      // labelControl19
      // 
      this.labelControl19.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.labelControl19.Appearance.Options.UseFont = true;
      this.labelControl19.Location = new System.Drawing.Point(182, 15);
      this.labelControl19.Name = "labelControl19";
      this.labelControl19.Size = new System.Drawing.Size(43, 15);
      this.labelControl19.TabIndex = 2;
      this.labelControl19.Text = "minutes";
      this.labelControl19.ToolTip = "If a game is not listed here, you can enter it\'s Steam ApplicationID here";
      // 
      // cbNoUpdateWhilePlaying
      // 
      this.cbNoUpdateWhilePlaying.EditValue = true;
      this.cbNoUpdateWhilePlaying.Location = new System.Drawing.Point(117, 72);
      this.cbNoUpdateWhilePlaying.Name = "cbNoUpdateWhilePlaying";
      this.cbNoUpdateWhilePlaying.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.cbNoUpdateWhilePlaying.Properties.Appearance.Options.UseFont = true;
      this.cbNoUpdateWhilePlaying.Properties.AutoWidth = true;
      this.cbNoUpdateWhilePlaying.Properties.Caption = "off while in-game";
      this.cbNoUpdateWhilePlaying.Size = new System.Drawing.Size(116, 19);
      this.cbNoUpdateWhilePlaying.TabIndex = 6;
      this.cbNoUpdateWhilePlaying.ToolTip = "Disable Auto-Update while you are in a game.\r\n(This looks at your Steam status)";
      // 
      // cbHideUnresponsiveServers
      // 
      this.cbHideUnresponsiveServers.EditValue = true;
      this.cbHideUnresponsiveServers.Location = new System.Drawing.Point(327, 53);
      this.cbHideUnresponsiveServers.Name = "cbHideUnresponsiveServers";
      this.cbHideUnresponsiveServers.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.cbHideUnresponsiveServers.Properties.Appearance.Options.UseFont = true;
      this.cbHideUnresponsiveServers.Properties.AutoWidth = true;
      this.cbHideUnresponsiveServers.Properties.Caption = "Hide timed out servers";
      this.cbHideUnresponsiveServers.Size = new System.Drawing.Size(141, 19);
      this.cbHideUnresponsiveServers.TabIndex = 9;
      this.cbHideUnresponsiveServers.CheckedChanged += new System.EventHandler(this.cbHideUnresponsiveServers_CheckedChanged);
      // 
      // rbUpdateStatusOnly
      // 
      this.rbUpdateStatusOnly.EditValue = true;
      this.rbUpdateStatusOnly.Location = new System.Drawing.Point(13, 53);
      this.rbUpdateStatusOnly.Name = "rbUpdateStatusOnly";
      this.rbUpdateStatusOnly.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.rbUpdateStatusOnly.Properties.Appearance.Options.UseFont = true;
      this.rbUpdateStatusOnly.Properties.AutoWidth = true;
      this.rbUpdateStatusOnly.Properties.Caption = "Update Status only";
      this.rbUpdateStatusOnly.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
      this.rbUpdateStatusOnly.Properties.RadioGroupIndex = 2;
      this.rbUpdateStatusOnly.Size = new System.Drawing.Size(121, 19);
      this.rbUpdateStatusOnly.TabIndex = 4;
      // 
      // cbFavServersOnTop
      // 
      this.cbFavServersOnTop.EditValue = true;
      this.cbFavServersOnTop.Location = new System.Drawing.Point(327, 34);
      this.cbFavServersOnTop.Name = "cbFavServersOnTop";
      this.cbFavServersOnTop.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.cbFavServersOnTop.Properties.Appearance.Options.UseFont = true;
      this.cbFavServersOnTop.Properties.AutoWidth = true;
      this.cbFavServersOnTop.Properties.Caption = "Keep my favorite servers on top";
      this.cbFavServersOnTop.Size = new System.Drawing.Size(188, 19);
      this.cbFavServersOnTop.TabIndex = 8;
      this.cbFavServersOnTop.CheckedChanged += new System.EventHandler(this.cbFavServersOnTop_CheckedChanged);
      // 
      // rbAddressGamePort
      // 
      this.rbAddressGamePort.Location = new System.Drawing.Point(639, 69);
      this.rbAddressGamePort.Name = "rbAddressGamePort";
      this.rbAddressGamePort.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.rbAddressGamePort.Properties.Appearance.Options.UseFont = true;
      this.rbAddressGamePort.Properties.AutoWidth = true;
      this.rbAddressGamePort.Properties.Caption = "Game Port";
      this.rbAddressGamePort.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
      this.rbAddressGamePort.Properties.RadioGroupIndex = 1;
      this.rbAddressGamePort.Size = new System.Drawing.Size(78, 19);
      this.rbAddressGamePort.TabIndex = 14;
      this.rbAddressGamePort.TabStop = false;
      this.rbAddressGamePort.CheckedChanged += new System.EventHandler(this.rbAddress_CheckedChanged);
      // 
      // rbAddressQueryPort
      // 
      this.rbAddressQueryPort.Location = new System.Drawing.Point(639, 50);
      this.rbAddressQueryPort.Name = "rbAddressQueryPort";
      this.rbAddressQueryPort.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.rbAddressQueryPort.Properties.Appearance.Options.UseFont = true;
      this.rbAddressQueryPort.Properties.AutoWidth = true;
      this.rbAddressQueryPort.Properties.Caption = "Query Port";
      this.rbAddressQueryPort.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
      this.rbAddressQueryPort.Properties.RadioGroupIndex = 1;
      this.rbAddressQueryPort.Size = new System.Drawing.Size(79, 19);
      this.rbAddressQueryPort.TabIndex = 13;
      this.rbAddressQueryPort.TabStop = false;
      this.rbAddressQueryPort.CheckedChanged += new System.EventHandler(this.rbAddress_CheckedChanged);
      // 
      // labelControl10
      // 
      this.labelControl10.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.labelControl10.Appearance.Options.UseFont = true;
      this.labelControl10.Location = new System.Drawing.Point(639, 15);
      this.labelControl10.Name = "labelControl10";
      this.labelControl10.Size = new System.Drawing.Size(80, 15);
      this.labelControl10.TabIndex = 11;
      this.labelControl10.Text = "Server Address:";
      // 
      // labelControl9
      // 
      this.labelControl9.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.labelControl9.Appearance.Options.UseFont = true;
      this.labelControl9.Location = new System.Drawing.Point(13, 15);
      this.labelControl9.Name = "labelControl9";
      this.labelControl9.Size = new System.Drawing.Size(72, 15);
      this.labelControl9.TabIndex = 0;
      this.labelControl9.Text = "Auto-Update:";
      // 
      // btnSkin
      // 
      this.btnSkin.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.btnSkin.Appearance.Options.UseFont = true;
      this.btnSkin.Location = new System.Drawing.Point(1160, 10);
      this.btnSkin.Name = "btnSkin";
      this.btnSkin.Size = new System.Drawing.Size(115, 25);
      this.btnSkin.TabIndex = 15;
      this.btnSkin.Text = "Change Skin";
      this.btnSkin.Click += new System.EventHandler(this.btnSkin_Click);
      // 
      // cbRefreshSelectedServer
      // 
      this.cbRefreshSelectedServer.EditValue = true;
      this.cbRefreshSelectedServer.Location = new System.Drawing.Point(327, 13);
      this.cbRefreshSelectedServer.Name = "cbRefreshSelectedServer";
      this.cbRefreshSelectedServer.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.cbRefreshSelectedServer.Properties.Appearance.Options.UseFont = true;
      this.cbRefreshSelectedServer.Properties.AutoWidth = true;
      this.cbRefreshSelectedServer.Properties.Caption = "Update status when selecting a server";
      this.cbRefreshSelectedServer.Size = new System.Drawing.Size(219, 19);
      this.cbRefreshSelectedServer.TabIndex = 7;
      this.cbRefreshSelectedServer.ToolTip = "NOTE: This may cause the row to be re-ordered when data is updated";
      // 
      // rbAddressHidden
      // 
      this.rbAddressHidden.EditValue = true;
      this.rbAddressHidden.Location = new System.Drawing.Point(639, 31);
      this.rbAddressHidden.Name = "rbAddressHidden";
      this.rbAddressHidden.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.rbAddressHidden.Properties.Appearance.Options.UseFont = true;
      this.rbAddressHidden.Properties.AutoWidth = true;
      this.rbAddressHidden.Properties.Caption = "Don\'t show";
      this.rbAddressHidden.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
      this.rbAddressHidden.Properties.RadioGroupIndex = 1;
      this.rbAddressHidden.Size = new System.Drawing.Size(82, 19);
      this.rbAddressHidden.TabIndex = 12;
      this.rbAddressHidden.CheckedChanged += new System.EventHandler(this.rbAddress_CheckedChanged);
      // 
      // spinRefreshInterval
      // 
      this.spinRefreshInterval.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.spinRefreshInterval.Location = new System.Drawing.Point(117, 11);
      this.spinRefreshInterval.MenuManager = this.barManager1;
      this.spinRefreshInterval.Name = "spinRefreshInterval";
      this.spinRefreshInterval.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.spinRefreshInterval.Properties.Appearance.Options.UseFont = true;
      this.spinRefreshInterval.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.spinRefreshInterval.Properties.DisplayFormat.FormatString = "n0";
      this.spinRefreshInterval.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
      this.spinRefreshInterval.Properties.Mask.EditMask = "d";
      this.spinRefreshInterval.Properties.Mask.UseMaskAsDisplayFormat = true;
      this.spinRefreshInterval.Properties.MaxValue = new decimal(new int[] {
            15,
            0,
            0,
            0});
      this.spinRefreshInterval.Size = new System.Drawing.Size(59, 22);
      this.spinRefreshInterval.TabIndex = 1;
      this.spinRefreshInterval.EditValueChanged += new System.EventHandler(this.spinRefreshInterval_EditValueChanged);
      // 
      // rbUpdateListAndStatus
      // 
      this.rbUpdateListAndStatus.Location = new System.Drawing.Point(13, 34);
      this.rbUpdateListAndStatus.Name = "rbUpdateListAndStatus";
      this.rbUpdateListAndStatus.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.rbUpdateListAndStatus.Properties.Appearance.Options.UseFont = true;
      this.rbUpdateListAndStatus.Properties.AutoWidth = true;
      this.rbUpdateListAndStatus.Properties.Caption = "Find Servers + Update Status";
      this.rbUpdateListAndStatus.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
      this.rbUpdateListAndStatus.Properties.RadioGroupIndex = 2;
      this.rbUpdateListAndStatus.Size = new System.Drawing.Size(172, 19);
      this.rbUpdateListAndStatus.TabIndex = 3;
      this.rbUpdateListAndStatus.TabStop = false;
      // 
      // rbUpdateDisabled
      // 
      this.rbUpdateDisabled.Location = new System.Drawing.Point(13, 72);
      this.rbUpdateDisabled.Name = "rbUpdateDisabled";
      this.rbUpdateDisabled.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.rbUpdateDisabled.Properties.Appearance.Options.UseFont = true;
      this.rbUpdateDisabled.Properties.AutoWidth = true;
      this.rbUpdateDisabled.Properties.Caption = "Off";
      this.rbUpdateDisabled.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
      this.rbUpdateDisabled.Properties.RadioGroupIndex = 2;
      this.rbUpdateDisabled.Size = new System.Drawing.Size(39, 19);
      this.rbUpdateDisabled.TabIndex = 5;
      this.rbUpdateDisabled.TabStop = false;
      // 
      // panelContainer1
      // 
      this.panelContainer1.ActiveChild = this.panelRules;
      this.panelContainer1.Controls.Add(this.panelPlayers);
      this.panelContainer1.Controls.Add(this.panelServerDetails);
      this.panelContainer1.Controls.Add(this.panelRules);
      this.panelContainer1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
      this.panelContainer1.ID = new System.Guid("30169f55-f874-4297-811b-db9e1af4c59a");
      this.panelContainer1.Location = new System.Drawing.Point(1296, 336);
      this.panelContainer1.Name = "panelContainer1";
      this.panelContainer1.OriginalSize = new System.Drawing.Size(362, 200);
      this.panelContainer1.Size = new System.Drawing.Size(362, 442);
      this.panelContainer1.Tabbed = true;
      this.panelContainer1.Text = "panelContainer1";
      // 
      // panelRules
      // 
      this.panelRules.Controls.Add(this.controlContainer2);
      this.panelRules.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
      this.panelRules.ID = new System.Guid("7cfd1891-8f2c-4d0a-bd2c-1bb030d15a66");
      this.panelRules.Location = new System.Drawing.Point(5, 25);
      this.panelRules.Name = "panelRules";
      this.panelRules.OriginalSize = new System.Drawing.Size(354, 386);
      this.panelRules.Size = new System.Drawing.Size(353, 386);
      this.panelRules.Text = "Rules";
      // 
      // controlContainer2
      // 
      this.controlContainer2.Controls.Add(this.gcRules);
      this.controlContainer2.Location = new System.Drawing.Point(0, 0);
      this.controlContainer2.Name = "controlContainer2";
      this.controlContainer2.Size = new System.Drawing.Size(353, 386);
      this.controlContainer2.TabIndex = 0;
      // 
      // gcRules
      // 
      this.gcRules.DataSource = this.dsRules;
      this.gcRules.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gcRules.Location = new System.Drawing.Point(0, 0);
      this.gcRules.MainView = this.gvRules;
      this.gcRules.Name = "gcRules";
      this.gcRules.Size = new System.Drawing.Size(353, 386);
      this.gcRules.TabIndex = 31;
      this.gcRules.ToolTipController = this.toolTipController;
      this.gcRules.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRules});
      // 
      // dsRules
      // 
      this.dsRules.DataSource = typeof(QueryMaster.Rule);
      // 
      // gvRules
      // 
      this.gvRules.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRuleName,
            this.colRuleValue});
      this.gvRules.GridControl = this.gcRules;
      this.gvRules.Name = "gvRules";
      this.gvRules.OptionsView.ShowGroupPanel = false;
      this.gvRules.OptionsView.ShowIndicator = false;
      this.gvRules.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gvRules_CustomColumnDisplayText);
      this.gvRules.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvRules_MouseDown);
      // 
      // colRuleName
      // 
      this.colRuleName.Caption = "Setting";
      this.colRuleName.FieldName = "Name";
      this.colRuleName.Name = "colRuleName";
      this.colRuleName.OptionsColumn.ReadOnly = true;
      this.colRuleName.Visible = true;
      this.colRuleName.VisibleIndex = 0;
      this.colRuleName.Width = 100;
      // 
      // colRuleValue
      // 
      this.colRuleValue.Caption = "Value";
      this.colRuleValue.FieldName = "Value";
      this.colRuleValue.Name = "colRuleValue";
      this.colRuleValue.OptionsColumn.ReadOnly = true;
      this.colRuleValue.Visible = true;
      this.colRuleValue.VisibleIndex = 1;
      this.colRuleValue.Width = 150;
      // 
      // panelPlayers
      // 
      this.panelPlayers.Controls.Add(this.dockPanel1_Container);
      this.panelPlayers.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
      this.panelPlayers.ID = new System.Guid("5ff9161d-077a-43fb-9f49-f8a0728b7b57");
      this.panelPlayers.Location = new System.Drawing.Point(5, 25);
      this.panelPlayers.Name = "panelPlayers";
      this.panelPlayers.Options.AllowFloating = false;
      this.panelPlayers.Options.ShowCloseButton = false;
      this.panelPlayers.OriginalSize = new System.Drawing.Size(354, 386);
      this.panelPlayers.Size = new System.Drawing.Size(353, 386);
      this.panelPlayers.Text = "Players";
      // 
      // dockPanel1_Container
      // 
      this.dockPanel1_Container.Controls.Add(this.gcPlayers);
      this.dockPanel1_Container.Location = new System.Drawing.Point(0, 0);
      this.dockPanel1_Container.Name = "dockPanel1_Container";
      this.dockPanel1_Container.Size = new System.Drawing.Size(353, 386);
      this.dockPanel1_Container.TabIndex = 0;
      // 
      // panelServerDetails
      // 
      this.panelServerDetails.Controls.Add(this.dockPanel2_Container);
      this.panelServerDetails.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
      this.panelServerDetails.ID = new System.Guid("adca8b15-d626-4469-97cf-a6cc21c21f6e");
      this.panelServerDetails.Location = new System.Drawing.Point(5, 25);
      this.panelServerDetails.Name = "panelServerDetails";
      this.panelServerDetails.Options.AllowFloating = false;
      this.panelServerDetails.Options.ShowCloseButton = false;
      this.panelServerDetails.OriginalSize = new System.Drawing.Size(354, 386);
      this.panelServerDetails.Size = new System.Drawing.Size(353, 386);
      this.panelServerDetails.Text = "Server Details";
      // 
      // dockPanel2_Container
      // 
      this.dockPanel2_Container.Controls.Add(this.gcDetails);
      this.dockPanel2_Container.Location = new System.Drawing.Point(0, 0);
      this.dockPanel2_Container.Name = "dockPanel2_Container";
      this.dockPanel2_Container.Size = new System.Drawing.Size(353, 386);
      this.dockPanel2_Container.TabIndex = 0;
      // 
      // panelServerList
      // 
      this.panelServerList.Controls.Add(this.controlContainer1);
      this.panelServerList.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
      this.panelServerList.ID = new System.Guid("865607d4-b558-4563-b50b-7827abfe171b");
      this.panelServerList.Location = new System.Drawing.Point(0, 377);
      this.panelServerList.Name = "panelServerList";
      this.panelServerList.Options.AllowDockAsTabbedDocument = false;
      this.panelServerList.Options.AllowDockRight = false;
      this.panelServerList.Options.AllowDockTop = false;
      this.panelServerList.Options.AllowFloating = false;
      this.panelServerList.Options.FloatOnDblClick = false;
      this.panelServerList.Options.ShowAutoHideButton = false;
      this.panelServerList.Options.ShowCloseButton = false;
      this.panelServerList.OriginalSize = new System.Drawing.Size(1102, 401);
      this.panelServerList.Size = new System.Drawing.Size(1296, 401);
      this.panelServerList.Text = "Servers";
      // 
      // controlContainer1
      // 
      this.controlContainer1.Controls.Add(this.gcServers);
      this.controlContainer1.Controls.Add(this.grpQuickFilter);
      this.controlContainer1.Location = new System.Drawing.Point(4, 26);
      this.controlContainer1.Name = "controlContainer1";
      this.controlContainer1.Size = new System.Drawing.Size(1288, 371);
      this.controlContainer1.TabIndex = 0;
      // 
      // grpQuickFilter
      // 
      this.grpQuickFilter.Controls.Add(this.txtFilterInfoClient);
      this.grpQuickFilter.Controls.Add(this.comboMinPlayers);
      this.grpQuickFilter.Controls.Add(this.labelControl17);
      this.grpQuickFilter.Controls.Add(this.btnTagExcludeClient);
      this.grpQuickFilter.Controls.Add(this.labelControl5);
      this.grpQuickFilter.Controls.Add(this.btnTagIncludeClient);
      this.grpQuickFilter.Controls.Add(this.linkFilter1);
      this.grpQuickFilter.Controls.Add(this.cbMinPlayersBots);
      this.grpQuickFilter.Controls.Add(this.btnApplyFilter);
      this.grpQuickFilter.Controls.Add(this.labelControl1);
      this.grpQuickFilter.Controls.Add(this.comboMaxPing);
      this.grpQuickFilter.Controls.Add(this.labelControl3);
      this.grpQuickFilter.Controls.Add(this.cbAlert);
      this.grpQuickFilter.Dock = System.Windows.Forms.DockStyle.Top;
      this.grpQuickFilter.Location = new System.Drawing.Point(0, 0);
      this.grpQuickFilter.Name = "grpQuickFilter";
      this.grpQuickFilter.ShowCaption = false;
      this.grpQuickFilter.Size = new System.Drawing.Size(1288, 66);
      this.grpQuickFilter.TabIndex = 1;
      this.grpQuickFilter.Text = "Quick Filters";
      // 
      // txtFilterInfoClient
      // 
      this.txtFilterInfoClient.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.txtFilterInfoClient.Appearance.Options.UseFont = true;
      this.txtFilterInfoClient.Location = new System.Drawing.Point(980, 8);
      this.txtFilterInfoClient.Name = "txtFilterInfoClient";
      this.txtFilterInfoClient.Size = new System.Drawing.Size(270, 39);
      this.txtFilterInfoClient.TabIndex = 19;
      this.txtFilterInfoClient.Text = "These are client-side filters, applied by your Browser.\r\nThey will further filter" +
    " down the received server list\r\nand are not restricted by any throttling.\r\n";
      // 
      // comboMinPlayers
      // 
      this.comboMinPlayers.Location = new System.Drawing.Point(114, 35);
      this.comboMinPlayers.Name = "comboMinPlayers";
      this.comboMinPlayers.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.comboMinPlayers.Properties.Appearance.Options.UseFont = true;
      this.comboMinPlayers.Properties.Appearance.Options.UseTextOptions = true;
      this.comboMinPlayers.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
      this.comboMinPlayers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.comboMinPlayers.Properties.DropDownRows = 30;
      this.comboMinPlayers.Properties.Items.AddRange(new object[] {
            "",
            "1",
            "2",
            "3",
            "4",
            "6",
            "8",
            "10",
            "12",
            "16",
            "20",
            "24"});
      this.comboMinPlayers.Size = new System.Drawing.Size(59, 22);
      this.comboMinPlayers.TabIndex = 12;
      this.comboMinPlayers.EditValueChanged += new System.EventHandler(this.comboMinPlayers_EditValueChanged);
      // 
      // labelControl17
      // 
      this.labelControl17.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.labelControl17.Appearance.Options.UseFont = true;
      this.labelControl17.Location = new System.Drawing.Point(431, 38);
      this.labelControl17.Name = "labelControl17";
      this.labelControl17.Size = new System.Drawing.Size(71, 15);
      this.labelControl17.TabIndex = 8;
      this.labelControl17.Text = "Exclude Tags:";
      // 
      // btnTagExcludeClient
      // 
      this.btnTagExcludeClient.Location = new System.Drawing.Point(517, 35);
      this.btnTagExcludeClient.MenuManager = this.barManager1;
      this.btnTagExcludeClient.Name = "btnTagExcludeClient";
      this.btnTagExcludeClient.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.btnTagExcludeClient.Properties.Appearance.Options.UseFont = true;
      this.btnTagExcludeClient.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
      this.btnTagExcludeClient.Properties.NullValuePrompt = "Tags separated by space/comma=AND; semicolon=OR";
      this.btnTagExcludeClient.Size = new System.Drawing.Size(328, 22);
      this.btnTagExcludeClient.TabIndex = 9;
      this.btnTagExcludeClient.ToolTip = "These tags are filtered locally on your PC";
      this.btnTagExcludeClient.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtTag_ButtonClick);
      // 
      // labelControl5
      // 
      this.labelControl5.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.labelControl5.Appearance.Options.UseFont = true;
      this.labelControl5.Location = new System.Drawing.Point(432, 10);
      this.labelControl5.Name = "labelControl5";
      this.labelControl5.Size = new System.Drawing.Size(69, 15);
      this.labelControl5.TabIndex = 6;
      this.labelControl5.Text = "Include Tags:";
      // 
      // btnTagIncludeClient
      // 
      this.btnTagIncludeClient.Location = new System.Drawing.Point(517, 7);
      this.btnTagIncludeClient.MenuManager = this.barManager1;
      this.btnTagIncludeClient.Name = "btnTagIncludeClient";
      this.btnTagIncludeClient.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.btnTagIncludeClient.Properties.Appearance.Options.UseFont = true;
      this.btnTagIncludeClient.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
      this.btnTagIncludeClient.Properties.NullValuePrompt = "Tags separated by space/comma=AND; semicolon=OR";
      this.btnTagIncludeClient.Size = new System.Drawing.Size(328, 22);
      this.btnTagIncludeClient.TabIndex = 7;
      this.btnTagIncludeClient.ToolTip = "These tags are filtered locally on your PC";
      this.btnTagIncludeClient.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtTag_ButtonClick);
      // 
      // linkFilter1
      // 
      this.linkFilter1.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.linkFilter1.Appearance.Options.UseFont = true;
      this.linkFilter1.Appearance.Options.UseTextOptions = true;
      this.linkFilter1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
      this.linkFilter1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
      this.linkFilter1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.linkFilter1.Location = new System.Drawing.Point(10, 7);
      this.linkFilter1.Name = "linkFilter1";
      this.linkFilter1.Size = new System.Drawing.Size(384, 19);
      this.linkFilter1.TabIndex = 0;
      this.linkFilter1.Text = "Use the top row for simple filters or the <href>filter editor</href> for advanced" +
    " criteria.";
      this.linkFilter1.HyperlinkClick += new DevExpress.Utils.HyperlinkClickEventHandler(this.linkFilter_HyperlinkClick);
      // 
      // cbMinPlayersBots
      // 
      this.cbMinPlayersBots.EditValue = true;
      this.cbMinPlayersBots.Location = new System.Drawing.Point(179, 36);
      this.cbMinPlayersBots.Name = "cbMinPlayersBots";
      this.cbMinPlayersBots.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.cbMinPlayersBots.Properties.Appearance.Options.UseFont = true;
      this.cbMinPlayersBots.Properties.AutoWidth = true;
      this.cbMinPlayersBots.Properties.Caption = "incl. Bots";
      this.cbMinPlayersBots.Size = new System.Drawing.Size(70, 19);
      this.cbMinPlayersBots.TabIndex = 3;
      this.cbMinPlayersBots.CheckedChanged += new System.EventHandler(this.cbMinPlayersBots_CheckedChanged);
      // 
      // btnApplyFilter
      // 
      this.btnApplyFilter.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.btnApplyFilter.Appearance.Options.UseFont = true;
      this.btnApplyFilter.ImageOptions.ImageIndex = 13;
      this.btnApplyFilter.ImageOptions.ImageList = this.imageCollection;
      this.btnApplyFilter.ImageOptions.ImageToTextIndent = 10;
      this.btnApplyFilter.Location = new System.Drawing.Point(867, 6);
      this.btnApplyFilter.Name = "btnApplyFilter";
      this.btnApplyFilter.Size = new System.Drawing.Size(98, 25);
      this.btnApplyFilter.TabIndex = 10;
      this.btnApplyFilter.Text = "Apply Filter";
      this.btnApplyFilter.ToolTip = "Get new server list from Valve master server";
      this.btnApplyFilter.Click += new System.EventHandler(this.btnApplyFilter_Click);
      // 
      // labelControl1
      // 
      this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.labelControl1.Appearance.Options.UseFont = true;
      this.labelControl1.Appearance.Options.UseTextOptions = true;
      this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
      this.labelControl1.Location = new System.Drawing.Point(10, 38);
      this.labelControl1.Name = "labelControl1";
      this.labelControl1.Size = new System.Drawing.Size(98, 15);
      this.labelControl1.TabIndex = 1;
      this.labelControl1.Text = "Min. Player Count:";
      // 
      // comboMaxPing
      // 
      this.comboMaxPing.Location = new System.Drawing.Point(323, 35);
      this.comboMaxPing.Name = "comboMaxPing";
      this.comboMaxPing.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.comboMaxPing.Properties.Appearance.Options.UseFont = true;
      this.comboMaxPing.Properties.Appearance.Options.UseTextOptions = true;
      this.comboMaxPing.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
      this.comboMaxPing.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.comboMaxPing.Properties.DropDownRows = 30;
      this.comboMaxPing.Properties.Items.AddRange(new object[] {
            "",
            "20",
            "40",
            "60",
            "80",
            "100",
            "150",
            "200"});
      this.comboMaxPing.Size = new System.Drawing.Size(59, 22);
      this.comboMaxPing.TabIndex = 5;
      this.comboMaxPing.EditValueChanged += new System.EventHandler(this.comboMaxPing_EditValueChanged);
      // 
      // labelControl3
      // 
      this.labelControl3.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.labelControl3.Appearance.Options.UseFont = true;
      this.labelControl3.Appearance.Options.UseTextOptions = true;
      this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
      this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
      this.labelControl3.Location = new System.Drawing.Point(267, 38);
      this.labelControl3.Name = "labelControl3";
      this.labelControl3.Size = new System.Drawing.Size(50, 15);
      this.labelControl3.TabIndex = 4;
      this.labelControl3.Text = "Ping <";
      // 
      // cbAlert
      // 
      this.cbAlert.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.cbAlert.Appearance.Options.UseFont = true;
      this.cbAlert.Appearance.Options.UseTextOptions = true;
      this.cbAlert.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
      this.cbAlert.ImageOptions.ImageIndex = 5;
      this.cbAlert.ImageOptions.ImageList = this.imageCollection;
      this.cbAlert.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
      this.cbAlert.ImageOptions.ImageToTextIndent = 10;
      this.cbAlert.Location = new System.Drawing.Point(867, 34);
      this.cbAlert.Name = "cbAlert";
      this.cbAlert.Size = new System.Drawing.Size(98, 25);
      this.cbAlert.TabIndex = 11;
      this.cbAlert.Text = "Notify me";
      this.cbAlert.ToolTip = "... when servers match my filters";
      this.cbAlert.CheckedChanged += new System.EventHandler(this.cbAlert_CheckedChanged);
      // 
      // timerUpdateServerList
      // 
      this.timerUpdateServerList.Enabled = true;
      this.timerUpdateServerList.Interval = 1000;
      this.timerUpdateServerList.Tick += new System.EventHandler(this.timerUpdateServerList_Tick);
      // 
      // panelControl1
      // 
      this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
      this.panelControl1.Controls.Add(this.txtStatus);
      this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panelControl1.Location = new System.Drawing.Point(0, 778);
      this.panelControl1.Name = "panelControl1";
      this.panelControl1.Size = new System.Drawing.Size(1658, 24);
      this.panelControl1.TabIndex = 35;
      // 
      // txtStatus
      // 
      this.txtStatus.Location = new System.Drawing.Point(8, 3);
      this.txtStatus.Name = "txtStatus";
      this.txtStatus.Size = new System.Drawing.Size(0, 13);
      this.txtStatus.TabIndex = 0;
      // 
      // menuServers
      // 
      this.menuServers.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.miUpdateServerInfo),
            new DevExpress.XtraBars.LinkPersistInfo(this.miConnect),
            new DevExpress.XtraBars.LinkPersistInfo(this.miConnectSpectator),
            new DevExpress.XtraBars.LinkPersistInfo(this.miCopyAddress, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.miSteamUrl),
            new DevExpress.XtraBars.LinkPersistInfo(this.miPasteAddress),
            new DevExpress.XtraBars.LinkPersistInfo(this.miFavServer),
            new DevExpress.XtraBars.LinkPersistInfo(this.miUnfavServer),
            new DevExpress.XtraBars.LinkPersistInfo(this.miDelete)});
      this.menuServers.Manager = this.barManager1;
      this.menuServers.Name = "menuServers";
      // 
      // menuPlayers
      // 
      this.menuPlayers.Manager = this.barManager1;
      this.menuPlayers.Name = "menuPlayers";
      // 
      // timerReloadServers
      // 
      this.timerReloadServers.Interval = 60000;
      this.timerReloadServers.Tick += new System.EventHandler(this.timerReloadServers_Tick);
      // 
      // alertControl1
      // 
      this.alertControl1.AutoFormDelay = 30000;
      this.alertControl1.AutoHeight = true;
      this.alertControl1.FormMaxCount = 1;
      this.alertControl1.FormShowingEffect = DevExpress.XtraBars.Alerter.AlertFormShowingEffect.MoveVertical;
      this.alertControl1.AlertClick += new DevExpress.XtraBars.Alerter.AlertClickEventHandler(this.alertControl1_AlertClick);
      // 
      // menuRules
      // 
      this.menuRules.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.miAddRulesColumnText),
            new DevExpress.XtraBars.LinkPersistInfo(this.miAddRulesColumnNumeric)});
      this.menuRules.Manager = this.barManager1;
      this.menuRules.Name = "menuRules";
      // 
      // menuTab
      // 
      this.menuTab.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.miRenameTab),
            new DevExpress.XtraBars.LinkPersistInfo(this.miCloneTab),
            new DevExpress.XtraBars.LinkPersistInfo(this.miCreateSnapshot)});
      this.menuTab.Manager = this.barManager1;
      this.menuTab.Name = "menuTab";
      // 
      // menuAddTab
      // 
      this.menuAddTab.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.miCloneTab),
            new DevExpress.XtraBars.LinkPersistInfo(this.miAddMasterServerTab, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.miAddCustomServerTab),
            new DevExpress.XtraBars.LinkPersistInfo(this.miNewFavoritesTab)});
      this.menuAddTab.Manager = this.barManager1;
      this.menuAddTab.Name = "menuAddTab";
      // 
      // defaultLookAndFeel1
      // 
      this.defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2010 Black";
      // 
      // menuDetails
      // 
      this.menuDetails.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.miAddDetailColumn)});
      this.menuDetails.Manager = this.barManager1;
      this.menuDetails.Name = "menuDetails";
      // 
      // splashScreenManager1
      // 
      this.splashScreenManager1.ClosingDelay = 500;
      // 
      // timerHideWaitForm
      // 
      this.timerHideWaitForm.Interval = 5000;
      this.timerHideWaitForm.Tick += new System.EventHandler(this.timerHideWaitForm_Tick);
      // 
      // ServerBrowserForm
      // 
      this.Appearance.Options.UseFont = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1658, 802);
      this.Controls.Add(this.panelServerList);
      this.Controls.Add(this.panelContainer1);
      this.Controls.Add(this.panelTop);
      this.Controls.Add(this.panelControl1);
      this.Controls.Add(this.barDockControlLeft);
      this.Controls.Add(this.barDockControlRight);
      this.Controls.Add(this.barDockControlBottom);
      this.Controls.Add(this.barDockControlTop);
      this.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("ServerBrowserForm.IconOptions.Icon")));
      this.Name = "ServerBrowserForm";
      this.Text = "Steam Server Browser";
      ((System.ComponentModel.ISupportInitialize)(this.riCheckEdit)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.gcDetails)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.gcPlayers)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dsPlayer)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.gvPlayers)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.gcServers)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dsServers)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.gvServers)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.riCountryFlagEdit)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.imgFlags)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.riFavServer)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.riPrivate)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.riDedicated)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.riJoinStatus)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
      this.panelRcon.ResumeLayout(false);
      this.controlContainer3.ResumeLayout(false);
      this.controlContainer3.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtRconPort.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.riFindPlayer)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtRconConsole.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtRconCommand.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtRconPassword.Properties)).EndInit();
      this.panelTop.ResumeLayout(false);
      this.controlContainer4.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.panelStaticList)).EndInit();
      this.panelStaticList.ResumeLayout(false);
      this.panelStaticList.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtGameServer.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.panelQuery)).EndInit();
      this.panelQuery.ResumeLayout(false);
      this.panelQuery.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtVersion.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboQueryLimit.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbGetFull.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtMod.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboGames.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtMap.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbGetEmpty.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtTagExcludeServer.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboMasterServer.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtTagIncludeServer.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.panelTabs)).EndInit();
      this.panelTabs.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
      this.tabControl.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.panelOptions)).EndInit();
      this.panelOptions.ResumeLayout(false);
      this.panelOptions.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.cbHideGhosts.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbConnectOnDoubleClick.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbUseSteamApi.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbShowCounts.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbShowFilterPanelInfo.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbNoUpdateWhilePlaying.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbHideUnresponsiveServers.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbUpdateStatusOnly.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbFavServersOnTop.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbAddressGamePort.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbAddressQueryPort.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbRefreshSelectedServer.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbAddressHidden.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.spinRefreshInterval.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbUpdateListAndStatus.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbUpdateDisabled.Properties)).EndInit();
      this.panelContainer1.ResumeLayout(false);
      this.panelRules.ResumeLayout(false);
      this.controlContainer2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gcRules)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dsRules)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.gvRules)).EndInit();
      this.panelPlayers.ResumeLayout(false);
      this.dockPanel1_Container.ResumeLayout(false);
      this.panelServerDetails.ResumeLayout(false);
      this.dockPanel2_Container.ResumeLayout(false);
      this.panelServerList.ResumeLayout(false);
      this.controlContainer1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.grpQuickFilter)).EndInit();
      this.grpQuickFilter.ResumeLayout(false);
      this.grpQuickFilter.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.comboMinPlayers.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.btnTagExcludeClient.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.btnTagIncludeClient.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbMinPlayersBots.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboMaxPing.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
      this.panelControl1.ResumeLayout(false);
      this.panelControl1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.menuServers)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.menuPlayers)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.menuRules)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.menuTab)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.menuAddTab)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.menuDetails)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    protected DevExpress.XtraGrid.GridControl gcDetails;
    protected DevExpress.XtraGrid.Views.Grid.GridView gvDetails;
    protected DevExpress.XtraGrid.GridControl gcPlayers;
    protected DevExpress.XtraGrid.Views.Grid.GridView gvPlayers;
    protected DevExpress.XtraGrid.GridControl gcServers;
    protected DevExpress.XtraGrid.Views.Grid.GridView gvServers;
    protected DevExpress.XtraBars.Docking.DockManager dockManager1;
    protected DevExpress.XtraBars.Docking.DockPanel panelServerDetails;
    protected DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
    protected DevExpress.XtraBars.Docking.DockPanel panelPlayers;
    protected DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
    protected System.Windows.Forms.BindingSource dsServers;
    protected DevExpress.XtraGrid.Columns.GridColumn colEndPoint;
    protected DevExpress.XtraGrid.Columns.GridColumn colName;
    protected DevExpress.XtraGrid.Columns.GridColumn colPing;
    protected DevExpress.XtraGrid.Columns.GridColumn colHumanPlayers;
    protected DevExpress.XtraGrid.Columns.GridColumn colMap;
    protected DevExpress.XtraGrid.Columns.GridColumn colDescription;
    protected System.Windows.Forms.BindingSource dsPlayer;
    protected DevExpress.XtraGrid.Columns.GridColumn colPlayerName;
    protected DevExpress.XtraGrid.Columns.GridColumn colScore;
    protected DevExpress.XtraGrid.Columns.GridColumn colTime;
    protected DevExpress.XtraGrid.Columns.GridColumn colStatus;
    protected DevExpress.XtraGrid.Columns.GridColumn colKey;
    protected DevExpress.XtraGrid.Columns.GridColumn colValue;
    protected DevExpress.XtraGrid.Columns.GridColumn colTags;
    protected DevExpress.XtraGrid.Columns.GridColumn colPrivate;
    protected CheckEdit cbRefreshSelectedServer;
    protected SimpleButton btnUpdateList;
    protected SimpleButton btnSkin;
    protected PanelControl panelQuery;
    protected DevExpress.XtraBars.Docking.DockPanel panelServerList;
    protected DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
    protected DevExpress.XtraBars.Docking.DockPanel panelContainer1;
    protected DevExpress.XtraBars.Docking.DockPanel panelRules;
    protected DevExpress.XtraBars.Docking.ControlContainer controlContainer2;
    protected DevExpress.XtraGrid.GridControl gcRules;
    protected DevExpress.XtraGrid.Views.Grid.GridView gvRules;
    protected DevExpress.XtraGrid.Columns.GridColumn colRuleName;
    protected DevExpress.XtraGrid.Columns.GridColumn colRuleValue;
    protected System.Windows.Forms.BindingSource dsRules;
    protected DevExpress.XtraGrid.Columns.GridColumn colDedicated;
    protected ComboBoxEdit comboGames;
    protected LabelControl labelControl4;
    protected System.Windows.Forms.Timer timerUpdateServerList;
    protected PanelControl panelOptions;
    protected PanelControl panelControl1;
    protected LabelControl txtStatus;
    protected DevExpress.XtraBars.BarDockControl barDockControlLeft;
    protected DevExpress.XtraBars.BarDockControl barDockControlRight;
    protected DevExpress.XtraBars.BarDockControl barDockControlBottom;
    protected DevExpress.XtraBars.BarDockControl barDockControlTop;
    protected XBarManager barManager1;
    protected DevExpress.XtraBars.BarButtonItem miConnect;
    protected DevExpress.XtraBars.BarButtonItem miConnectSpectator;
    protected DevExpress.XtraBars.PopupMenu menuServers;
    protected DevExpress.XtraBars.BarButtonItem miCopyAddress;
    protected DevExpress.XtraBars.PopupMenu menuPlayers;
    protected DevExpress.XtraBars.BarButtonItem miUpdateServerInfo;
    protected DevExpress.XtraGrid.Columns.GridColumn colBots;
    protected DevExpress.XtraGrid.Columns.GridColumn colTotalPlayers;
    protected DevExpress.XtraGrid.Columns.GridColumn colMaxPlayers;
    protected DevExpress.XtraGrid.Columns.GridColumn colPlayerCount;
    protected LabelControl labelControl9;
    protected SpinEdit spinRefreshInterval;
    protected System.Windows.Forms.Timer timerReloadServers;
    protected DevExpress.XtraBars.Alerter.AlertControl alertControl1;
    protected HyperlinkLabelControl linkFilter1;
    protected CheckEdit cbGetEmpty;
    protected CheckEdit cbGetFull;
    protected ComboBoxEdit comboMasterServer;
    protected LabelControl labelControl7;
    protected LabelControl labelControl6;
    protected DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riCheckEdit;
    protected LabelControl labelControl10;
    protected CheckButton cbAlert;
    protected CheckEdit rbAddressHidden;
    protected CheckEdit rbAddressGamePort;
    protected CheckEdit rbAddressQueryPort;
    protected LabelControl labelControl8;
    protected LabelControl labelControl2;
    protected DevExpress.Utils.ImageCollection imageCollection;
    protected ButtonEdit txtTagExcludeServer;
    protected ButtonEdit txtTagIncludeServer;
    protected ButtonEdit txtMap;
    protected LabelControl labelControl11;
    protected ButtonEdit txtMod;
    protected LabelControl labelControl12;
    protected DevExpress.XtraBars.Bar barMenu;
    protected DevExpress.XtraBars.BarButtonItem miQuickRefresh;
    private DevExpress.XtraBars.BarButtonItem miAddRulesColumnText;
    private DevExpress.XtraBars.PopupMenu menuRules;
    private DevExpress.XtraBars.BarButtonItem miAddRulesColumnNumeric;
    private DevExpress.XtraGrid.Columns.GridColumn colLocation;
    private DevExpress.Utils.ImageCollection imgFlags;
    private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riCountryFlagEdit;
    private DevExpress.Utils.ToolTipController toolTipController;
    private DevExpress.XtraBars.Docking.DockPanel panelRcon;
    private DevExpress.XtraBars.Docking.ControlContainer controlContainer3;
    protected LabelControl labelControl15;
    private MemoEdit txtRconConsole;
    protected ButtonEdit txtRconCommand;
    protected LabelControl labelControl14;
    protected ButtonEdit txtRconPassword;
    protected LabelControl labelControl13;
    protected ButtonEdit txtRconPort;
    private DevExpress.XtraGrid.Columns.GridColumn colFavServer;
    private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riFavServer;
    private DevExpress.XtraBars.BarButtonItem miFavServer;
    protected SimpleButton btnUpdateStatus;
    protected CheckEdit cbFavServersOnTop;
    private ComboBoxEdit comboQueryLimit;
    protected LabelControl labelControl16;
    protected CheckEdit rbUpdateStatusOnly;
    protected CheckEdit rbUpdateListAndStatus;
    private DevExpress.XtraTab.XtraTabControl tabControl;
    private DevExpress.XtraTab.XtraTabPage tabGame;
    private DevExpress.XtraTab.XtraTabPage tabAdd;
    private DevExpress.XtraBars.BarButtonItem miShowOptions;
    private DevExpress.XtraBars.BarButtonItem miFindServers;
    private DevExpress.XtraBars.BarButtonItem miShowServerQuery;
    private PanelControl panelTabs;
    private DevExpress.XtraBars.BarButtonItem miRenameTab;
    private DevExpress.XtraBars.PopupMenu menuTab;
    protected CheckEdit rbUpdateDisabled;
    protected PanelControl panelStaticList;
    protected ButtonEdit txtGameServer;
    private DevExpress.XtraBars.BarButtonItem miCloneTab;
    private DevExpress.XtraBars.BarButtonItem miCreateSnapshot;
    private DevExpress.XtraTab.XtraTabPage tabFavorites;
    private DevExpress.XtraBars.BarButtonItem miUnfavServer;
    private DevExpress.XtraBars.BarButtonItem miPasteAddress;
    private DevExpress.XtraBars.BarButtonItem miDelete;
    private DevExpress.XtraBars.BarButtonItem miAddMasterServerTab;
    private DevExpress.XtraBars.BarButtonItem miAddCustomServerTab;
    private DevExpress.XtraBars.BarButtonItem miNewFavoritesTab;
    private DevExpress.XtraBars.PopupMenu menuAddTab;
    private DevExpress.XtraBars.BarSubItem mnuView;
    private DevExpress.XtraBars.BarSubItem mnuTabs;
    private DevExpress.XtraBars.BarSubItem mnuServer;
    private DevExpress.XtraBars.BarSubItem mnuUpdate;
    private LabelControl labelControl1;
    protected SimpleButton btnApplyFilter;
    protected ComboBoxEdit comboMaxPing;
    private LabelControl labelControl3;
    protected CheckEdit cbMinPlayersBots;
    private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
    protected CheckEdit cbHideUnresponsiveServers;
    private DevExpress.XtraBars.BarEditItem miFindPlayer;
    private DevExpress.XtraEditors.Repository.RepositoryItemComboBox riFindPlayer;
    private DevExpress.XtraGrid.Columns.GridColumn colBuddyCount;
    private DevExpress.XtraBars.BarButtonItem miAddDetailColumn;
    private DevExpress.XtraBars.PopupMenu menuDetails;
    private DevExpress.XtraGrid.Columns.GridColumn colJoinStatus;
    private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riJoinStatus;
    protected CheckEdit cbNoUpdateWhilePlaying;
    private GroupControl grpQuickFilter;
    private DevExpress.XtraBars.BarButtonItem miShowFilter;
    private DevExpress.XtraBars.BarLinkContainerItem mnuGameOptions;
    private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    private System.Windows.Forms.Timer timerHideWaitForm;
    private DevExpress.XtraBars.BarButtonItem miStopUpdate;
    protected LabelControl labelControl17;
    protected ButtonEdit btnTagExcludeClient;
    protected LabelControl labelControl5;
    protected ButtonEdit btnTagIncludeClient;
    private DevExpress.XtraBars.BarSubItem barSubItem1;
    private DevExpress.XtraBars.BarButtonItem miAboutGithub;
    private DevExpress.XtraBars.BarButtonItem miAboutSteamWorkshop;
    protected SimpleButton btnPasteAddresses;
    private LabelControl labelControl18;
    protected LabelControl labelControl19;
    protected ComboBoxEdit comboMinPlayers;
    private LabelControl txtFilterInfoClient;
    private LabelControl txtFilterInfoMaster;
    protected CheckEdit cbShowFilterPanelInfo;
    protected CheckEdit cbShowCounts;
    protected CheckEdit cbUseSteamApi;
    private DevExpress.XtraBars.BarButtonItem miAboutVersionHistory;
    private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riPrivate;
    private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riDedicated;
    protected CheckEdit cbConnectOnDoubleClick;
    private DevExpress.XtraBars.Docking.DockPanel panelTop;
    private DevExpress.XtraBars.Docking.ControlContainer controlContainer4;
    private DevExpress.XtraBars.BarButtonItem miRestoreStandardLayout;
    protected CheckEdit cbHideGhosts;
    protected ButtonEdit txtVersion;
    protected LabelControl labelControl20;
    private DevExpress.XtraBars.BarButtonItem miSteamUrl;
    private DevExpress.XtraBars.BarSubItem mnuGeoIpImpl;
    private DevExpress.XtraBars.BarButtonItem miGeoLite2;
    private DevExpress.XtraBars.BarButtonItem miIpApiCom;
  }
}
