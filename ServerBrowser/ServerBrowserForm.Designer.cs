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
      this.riCheckEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
      this.gcDetails = new DevExpress.XtraGrid.GridControl();
      this.gvDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
      this.colKey = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colValue = new DevExpress.XtraGrid.Columns.GridColumn();
      this.comboRegion = new DevExpress.XtraEditors.ComboBoxEdit();
      this.rbFavGame2 = new DevExpress.XtraEditors.CheckEdit();
      this.rbFavGame1 = new DevExpress.XtraEditors.CheckEdit();
      this.rbFavGame3 = new DevExpress.XtraEditors.CheckEdit();
      this.gcPlayers = new DevExpress.XtraGrid.GridControl();
      this.dsPlayer = new System.Windows.Forms.BindingSource(this.components);
      this.gvPlayers = new DevExpress.XtraGrid.Views.Grid.GridView();
      this.colName1 = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colScore = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colTime = new DevExpress.XtraGrid.Columns.GridColumn();
      this.gcServers = new DevExpress.XtraGrid.GridControl();
      this.dsServers = new System.Windows.Forms.BindingSource(this.components);
      this.gvServers = new DevExpress.XtraGrid.Views.Grid.GridView();
      this.colLocation = new DevExpress.XtraGrid.Columns.GridColumn();
      this.riCountryFlagEdit = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
      this.imgFlags = new DevExpress.Utils.ImageCollection(this.components);
      this.colEndPoint = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colDedicated = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colPrivate = new DevExpress.XtraGrid.Columns.GridColumn();
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
      this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
      this.barMenu = new DevExpress.XtraBars.Bar();
      this.miReloadServers = new DevExpress.XtraBars.BarButtonItem();
      this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
      this.imageCollection = new DevExpress.Utils.ImageCollection(this.components);
      this.miUpdateServerInfo = new DevExpress.XtraBars.BarButtonItem();
      this.miConnect = new DevExpress.XtraBars.BarButtonItem();
      this.miConnectSpectator = new DevExpress.XtraBars.BarButtonItem();
      this.miCopyAddress = new DevExpress.XtraBars.BarButtonItem();
      this.miAddRulesColumnText = new DevExpress.XtraBars.BarButtonItem();
      this.miAddRulesColumnNumeric = new DevExpress.XtraBars.BarButtonItem();
      this.panelContainer1 = new DevExpress.XtraBars.Docking.DockPanel();
      this.panelRules = new DevExpress.XtraBars.Docking.DockPanel();
      this.controlContainer2 = new DevExpress.XtraBars.Docking.ControlContainer();
      this.gcRules = new DevExpress.XtraGrid.GridControl();
      this.dsRules = new System.Windows.Forms.BindingSource(this.components);
      this.gvRules = new DevExpress.XtraGrid.Views.Grid.GridView();
      this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
      this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
      this.panelPlayers = new DevExpress.XtraBars.Docking.DockPanel();
      this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
      this.panelServerDetails = new DevExpress.XtraBars.Docking.DockPanel();
      this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
      this.panelServerList = new DevExpress.XtraBars.Docking.DockPanel();
      this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
      this.btnSkin = new DevExpress.XtraEditors.SimpleButton();
      this.cbRefreshSelectedServer = new DevExpress.XtraEditors.CheckEdit();
      this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
      this.btnQueryMaster = new DevExpress.XtraEditors.SimpleButton();
      this.panelTop = new DevExpress.XtraEditors.PanelControl();
      this.panelControls = new DevExpress.XtraEditors.PanelControl();
      this.panelOptions = new DevExpress.XtraEditors.PanelControl();
      this.rbFavGame4 = new DevExpress.XtraEditors.CheckEdit();
      this.txtMod = new DevExpress.XtraEditors.ButtonEdit();
      this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
      this.txtMap = new DevExpress.XtraEditors.ButtonEdit();
      this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
      this.txtTagExclude = new DevExpress.XtraEditors.ButtonEdit();
      this.txtTagInclude = new DevExpress.XtraEditors.ButtonEdit();
      this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
      this.cbAdvancedOptions = new DevExpress.XtraEditors.CheckButton();
      this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
      this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
      this.comboMasterServer = new DevExpress.XtraEditors.ComboBoxEdit();
      this.cbGetFull = new DevExpress.XtraEditors.CheckEdit();
      this.comboGames = new DevExpress.XtraEditors.ComboBoxEdit();
      this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
      this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
      this.cbGetEmpty = new DevExpress.XtraEditors.CheckEdit();
      this.panelTopFill = new DevExpress.XtraEditors.PanelControl();
      this.linkFilter1 = new DevExpress.XtraEditors.HyperlinkLabelControl();
      this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
      this.timerUpdateServerList = new System.Windows.Forms.Timer(this.components);
      this.panelAdvancedOptions = new DevExpress.XtraEditors.PanelControl();
      this.txtGameServer = new DevExpress.XtraEditors.ButtonEdit();
      this.rbAddressGamePort = new DevExpress.XtraEditors.CheckEdit();
      this.rbAddressQueryPort = new DevExpress.XtraEditors.CheckEdit();
      this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
      this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
      this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
      this.cbAlert = new DevExpress.XtraEditors.CheckButton();
      this.rbAddressHidden = new DevExpress.XtraEditors.CheckEdit();
      this.spinRefreshInterval = new DevExpress.XtraEditors.SpinEdit();
      this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
      this.txtStatus = new DevExpress.XtraEditors.LabelControl();
      this.menuServers = new DevExpress.XtraBars.PopupMenu(this.components);
      this.menuPlayers = new DevExpress.XtraBars.PopupMenu(this.components);
      this.timerReloadServers = new System.Windows.Forms.Timer(this.components);
      this.alertControl1 = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
      this.menuRules = new DevExpress.XtraBars.PopupMenu(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.riCheckEdit)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.gcDetails)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboRegion.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbFavGame2.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbFavGame1.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbFavGame3.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.gcPlayers)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dsPlayer)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.gvPlayers)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.gcServers)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dsServers)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.gvServers)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.riCountryFlagEdit)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.imgFlags)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).BeginInit();
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
      ((System.ComponentModel.ISupportInitialize)(this.cbRefreshSelectedServer.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.panelTop)).BeginInit();
      this.panelTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.panelControls)).BeginInit();
      this.panelControls.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.panelOptions)).BeginInit();
      this.panelOptions.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.rbFavGame4.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtMod.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtMap.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtTagExclude.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtTagInclude.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboMasterServer.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbGetFull.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboGames.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbGetEmpty.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.panelTopFill)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.panelAdvancedOptions)).BeginInit();
      this.panelAdvancedOptions.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtGameServer.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbAddressGamePort.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbAddressQueryPort.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbAddressHidden.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.spinRefreshInterval.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
      this.panelControl1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.menuServers)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.menuPlayers)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.menuRules)).BeginInit();
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
      this.gcDetails.Size = new System.Drawing.Size(354, 516);
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
      this.gvDetails.OptionsBehavior.Editable = false;
      this.gvDetails.OptionsView.ShowGroupPanel = false;
      this.gvDetails.OptionsView.ShowIndicator = false;
      // 
      // colKey
      // 
      this.colKey.Caption = "Setting";
      this.colKey.FieldName = "Item1";
      this.colKey.Name = "colKey";
      this.colKey.Visible = true;
      this.colKey.VisibleIndex = 0;
      this.colKey.Width = 100;
      // 
      // colValue
      // 
      this.colValue.Caption = "Value";
      this.colValue.FieldName = "Item2";
      this.colValue.Name = "colValue";
      this.colValue.Visible = true;
      this.colValue.VisibleIndex = 1;
      this.colValue.Width = 150;
      // 
      // comboRegion
      // 
      this.comboRegion.Location = new System.Drawing.Point(445, 7);
      this.comboRegion.Name = "comboRegion";
      this.comboRegion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.comboRegion.Properties.DropDownRows = 10;
      this.comboRegion.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
      this.comboRegion.Size = new System.Drawing.Size(184, 20);
      this.comboRegion.TabIndex = 3;
      this.comboRegion.SelectedIndexChanged += new System.EventHandler(this.comboRegion_SelectedIndexChanged);
      // 
      // rbFavGame2
      // 
      this.rbFavGame2.Location = new System.Drawing.Point(183, 57);
      this.rbFavGame2.Name = "rbFavGame2";
      this.rbFavGame2.Properties.AutoWidth = true;
      this.rbFavGame2.Properties.Caption = "Toxikk";
      this.rbFavGame2.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
      this.rbFavGame2.Properties.RadioGroupIndex = 1;
      this.rbFavGame2.Size = new System.Drawing.Size(52, 19);
      this.rbFavGame2.TabIndex = 8;
      this.rbFavGame2.TabStop = false;
      this.rbFavGame2.ToolTip = "Use Ctrl+Click to assign the game selected above to this button";
      this.rbFavGame2.CheckedChanged += new System.EventHandler(this.rbFavGame_CheckedChanged);
      // 
      // rbFavGame1
      // 
      this.rbFavGame1.Location = new System.Drawing.Point(99, 57);
      this.rbFavGame1.Name = "rbFavGame1";
      this.rbFavGame1.Properties.AutoWidth = true;
      this.rbFavGame1.Properties.Caption = "Reflex";
      this.rbFavGame1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
      this.rbFavGame1.Properties.RadioGroupIndex = 1;
      this.rbFavGame1.Size = new System.Drawing.Size(53, 19);
      this.rbFavGame1.TabIndex = 7;
      this.rbFavGame1.TabStop = false;
      this.rbFavGame1.ToolTip = "Use Ctrl+Click to assign the game selected above to this button";
      this.rbFavGame1.CheckedChanged += new System.EventHandler(this.rbFavGame_CheckedChanged);
      // 
      // rbFavGame3
      // 
      this.rbFavGame3.Location = new System.Drawing.Point(262, 57);
      this.rbFavGame3.Name = "rbFavGame3";
      this.rbFavGame3.Properties.AutoWidth = true;
      this.rbFavGame3.Properties.Caption = "Quake Live";
      this.rbFavGame3.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
      this.rbFavGame3.Properties.RadioGroupIndex = 1;
      this.rbFavGame3.Size = new System.Drawing.Size(75, 19);
      this.rbFavGame3.TabIndex = 9;
      this.rbFavGame3.TabStop = false;
      this.rbFavGame3.ToolTip = "Use Ctrl+Click to assign the game selected above to this button";
      this.rbFavGame3.CheckedChanged += new System.EventHandler(this.rbFavGame_CheckedChanged);
      // 
      // gcPlayers
      // 
      this.gcPlayers.DataSource = this.dsPlayer;
      this.gcPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gcPlayers.Location = new System.Drawing.Point(0, 0);
      this.gcPlayers.MainView = this.gvPlayers;
      this.gcPlayers.Name = "gcPlayers";
      this.gcPlayers.Size = new System.Drawing.Size(354, 516);
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
            this.colName1,
            this.colScore,
            this.colTime});
      this.gvPlayers.GridControl = this.gcPlayers;
      this.gvPlayers.Name = "gvPlayers";
      this.gvPlayers.OptionsView.ShowGroupPanel = false;
      this.gvPlayers.OptionsView.ShowIndicator = false;
      this.gvPlayers.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.gvPlayers_CustomUnboundColumnData);
      this.gvPlayers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvPlayers_MouseDown);
      this.gvPlayers.DoubleClick += new System.EventHandler(this.gvPlayers_DoubleClick);
      // 
      // colName1
      // 
      this.colName1.FieldName = "Name";
      this.colName1.Name = "colName1";
      this.colName1.OptionsColumn.ReadOnly = true;
      this.colName1.Visible = true;
      this.colName1.VisibleIndex = 0;
      this.colName1.Width = 131;
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
      this.gcServers.Location = new System.Drawing.Point(0, 0);
      this.gcServers.MainView = this.gvServers;
      this.gcServers.Name = "gcServers";
      this.gcServers.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riCountryFlagEdit});
      this.gcServers.Size = new System.Drawing.Size(1095, 497);
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
            this.colEndPoint,
            this.colName,
            this.colDedicated,
            this.colPrivate,
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
      this.gvServers.Name = "gvServers";
      this.gvServers.OptionsDetail.EnableMasterViewMode = false;
      this.gvServers.OptionsFilter.DefaultFilterEditorView = DevExpress.XtraEditors.FilterEditorViewMode.VisualAndText;
      this.gvServers.OptionsView.ColumnAutoWidth = false;
      this.gvServers.OptionsView.ShowAutoFilterRow = true;
      this.gvServers.OptionsView.ShowGroupPanel = false;
      this.gvServers.OptionsView.ShowIndicator = false;
      this.gvServers.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colPlayerCount, DevExpress.Data.ColumnSortOrder.Descending)});
      this.gvServers.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvServers_FocusedRowChanged);
      this.gvServers.ColumnFilterChanged += new System.EventHandler(this.gvServers_ColumnFilterChanged);
      this.gvServers.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.gvServers_CustomUnboundColumnData);
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
      this.colLocation.VisibleIndex = 0;
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
      // colEndPoint
      // 
      this.colEndPoint.Caption = "Address";
      this.colEndPoint.FieldName = "Address";
      this.colEndPoint.Name = "colEndPoint";
      this.colEndPoint.OptionsColumn.ReadOnly = true;
      this.colEndPoint.UnboundType = DevExpress.Data.UnboundColumnType.String;
      this.colEndPoint.Visible = true;
      this.colEndPoint.VisibleIndex = 1;
      this.colEndPoint.Width = 132;
      // 
      // colName
      // 
      this.colName.FieldName = "Name";
      this.colName.Name = "colName";
      this.colName.OptionsColumn.AllowEdit = false;
      this.colName.OptionsColumn.ReadOnly = true;
      this.colName.UnboundType = DevExpress.Data.UnboundColumnType.String;
      this.colName.Visible = true;
      this.colName.VisibleIndex = 2;
      this.colName.Width = 260;
      // 
      // colDedicated
      // 
      this.colDedicated.Caption = "Ded";
      this.colDedicated.FieldName = "Dedicated";
      this.colDedicated.Name = "colDedicated";
      this.colDedicated.ToolTip = "Dedicated";
      this.colDedicated.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
      this.colDedicated.Visible = true;
      this.colDedicated.VisibleIndex = 3;
      this.colDedicated.Width = 35;
      // 
      // colPrivate
      // 
      this.colPrivate.Caption = "Private";
      this.colPrivate.ColumnEdit = this.riCheckEdit;
      this.colPrivate.FieldName = "ServerInfo.IsPrivate";
      this.colPrivate.Name = "colPrivate";
      this.colPrivate.OptionsColumn.AllowEdit = false;
      this.colPrivate.Visible = true;
      this.colPrivate.VisibleIndex = 4;
      this.colPrivate.Width = 45;
      // 
      // colDescription
      // 
      this.colDescription.Caption = "Description";
      this.colDescription.FieldName = "ServerInfo.Description";
      this.colDescription.Name = "colDescription";
      this.colDescription.OptionsColumn.AllowEdit = false;
      this.colDescription.Visible = true;
      this.colDescription.VisibleIndex = 5;
      this.colDescription.Width = 101;
      // 
      // colTags
      // 
      this.colTags.Caption = "Tags";
      this.colTags.FieldName = "ServerInfo.Extra.Keywords";
      this.colTags.Name = "colTags";
      this.colTags.OptionsColumn.AllowEdit = false;
      this.colTags.Visible = true;
      this.colTags.VisibleIndex = 6;
      this.colTags.Width = 127;
      // 
      // colPlayerCount
      // 
      this.colPlayerCount.Caption = "Players";
      this.colPlayerCount.FieldName = "PlayerCount";
      this.colPlayerCount.Name = "colPlayerCount";
      this.colPlayerCount.OptionsColumn.AllowEdit = false;
      this.colPlayerCount.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
      this.colPlayerCount.Visible = true;
      this.colPlayerCount.VisibleIndex = 7;
      // 
      // colHumanPlayers
      // 
      this.colHumanPlayers.Caption = "Humans";
      this.colHumanPlayers.FieldName = "PlayerCount.RealPlayers";
      this.colHumanPlayers.Name = "colHumanPlayers";
      this.colHumanPlayers.OptionsColumn.AllowEdit = false;
      this.colHumanPlayers.ToolTip = "Human Players";
      this.colHumanPlayers.Visible = true;
      this.colHumanPlayers.VisibleIndex = 8;
      this.colHumanPlayers.Width = 30;
      // 
      // colBots
      // 
      this.colBots.Caption = "Bots";
      this.colBots.FieldName = "PlayerCount.Bots";
      this.colBots.Name = "colBots";
      this.colBots.OptionsColumn.AllowEdit = false;
      this.colBots.Visible = true;
      this.colBots.VisibleIndex = 9;
      this.colBots.Width = 30;
      // 
      // colTotalPlayers
      // 
      this.colTotalPlayers.Caption = "Total";
      this.colTotalPlayers.FieldName = "PlayerCount.TotalPlayers";
      this.colTotalPlayers.Name = "colTotalPlayers";
      this.colTotalPlayers.OptionsColumn.AllowEdit = false;
      this.colTotalPlayers.ToolTip = "Total Players (Humans + Bots)";
      this.colTotalPlayers.Visible = true;
      this.colTotalPlayers.VisibleIndex = 10;
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
      this.colMaxPlayers.VisibleIndex = 11;
      this.colMaxPlayers.Width = 30;
      // 
      // colMap
      // 
      this.colMap.Caption = "Map";
      this.colMap.FieldName = "ServerInfo.Map";
      this.colMap.Name = "colMap";
      this.colMap.OptionsColumn.AllowEdit = false;
      this.colMap.Visible = true;
      this.colMap.VisibleIndex = 12;
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
      this.colPing.VisibleIndex = 13;
      this.colPing.Width = 32;
      // 
      // colStatus
      // 
      this.colStatus.Caption = "Status";
      this.colStatus.FieldName = "Status";
      this.colStatus.Name = "colStatus";
      this.colStatus.OptionsColumn.AllowEdit = false;
      this.colStatus.Visible = true;
      this.colStatus.VisibleIndex = 14;
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
      this.dockManager1.MenuManager = this.barManager1;
      this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
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
            "DevExpress.XtraEditors.PanelControl"});
      this.dockManager1.StartDocking += new DevExpress.XtraBars.Docking.DockPanelCancelEventHandler(this.dockManager1_StartDocking);
      // 
      // barManager1
      // 
      this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barMenu});
      this.barManager1.Categories.AddRange(new DevExpress.XtraBars.BarManagerCategory[] {
            new DevExpress.XtraBars.BarManagerCategory("Server", new System.Guid("b1e08833-8d08-415c-9522-c31e9bf3c2de")),
            new DevExpress.XtraBars.BarManagerCategory("Player", new System.Guid("9969d11d-8a54-4afd-8aea-1c952f049e03")),
            new DevExpress.XtraBars.BarManagerCategory("Menu", new System.Guid("dce44941-9e20-4803-823c-6829c76924c5"))});
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
            this.miCopyAddress,
            this.miReloadServers,
            this.miAddRulesColumnText,
            this.miAddRulesColumnNumeric});
      this.barManager1.MaxItemId = 7;
      // 
      // barMenu
      // 
      this.barMenu.BarName = "barMenu";
      this.barMenu.DockCol = 0;
      this.barMenu.DockRow = 0;
      this.barMenu.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
      this.barMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.miReloadServers, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
      this.barMenu.OptionsBar.AllowQuickCustomization = false;
      this.barMenu.OptionsBar.DisableClose = true;
      this.barMenu.OptionsBar.DrawDragBorder = false;
      this.barMenu.OptionsBar.UseWholeRow = true;
      this.barMenu.Text = "barMenu";
      this.barMenu.Visible = false;
      // 
      // miReloadServers
      // 
      this.miReloadServers.Caption = "Refresh";
      this.miReloadServers.CategoryGuid = new System.Guid("dce44941-9e20-4803-823c-6829c76924c5");
      this.miReloadServers.Id = 4;
      this.miReloadServers.ImageIndex = 1;
      this.miReloadServers.Name = "miReloadServers";
      this.miReloadServers.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miReloadServers_ItemClick);
      // 
      // barDockControlTop
      // 
      this.barDockControlTop.CausesValidation = false;
      this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
      this.barDockControlTop.Size = new System.Drawing.Size(1465, 31);
      // 
      // barDockControlBottom
      // 
      this.barDockControlBottom.CausesValidation = false;
      this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.barDockControlBottom.Location = new System.Drawing.Point(0, 808);
      this.barDockControlBottom.Size = new System.Drawing.Size(1465, 0);
      // 
      // barDockControlLeft
      // 
      this.barDockControlLeft.CausesValidation = false;
      this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
      this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
      this.barDockControlLeft.Size = new System.Drawing.Size(0, 777);
      // 
      // barDockControlRight
      // 
      this.barDockControlRight.CausesValidation = false;
      this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
      this.barDockControlRight.Location = new System.Drawing.Point(1465, 31);
      this.barDockControlRight.Size = new System.Drawing.Size(0, 777);
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
      // 
      // miUpdateServerInfo
      // 
      this.miUpdateServerInfo.Caption = "Update Information";
      this.miUpdateServerInfo.CategoryGuid = new System.Guid("b1e08833-8d08-415c-9522-c31e9bf3c2de");
      this.miUpdateServerInfo.Id = 3;
      this.miUpdateServerInfo.ImageIndex = 1;
      this.miUpdateServerInfo.Name = "miUpdateServerInfo";
      this.miUpdateServerInfo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miUpdateServerInfo_ItemClick);
      // 
      // miConnect
      // 
      this.miConnect.Caption = "Connect";
      this.miConnect.CategoryGuid = new System.Guid("b1e08833-8d08-415c-9522-c31e9bf3c2de");
      this.miConnect.Id = 0;
      this.miConnect.ImageIndex = 7;
      this.miConnect.Name = "miConnect";
      this.miConnect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miConnect_ItemClick);
      // 
      // miConnectSpectator
      // 
      this.miConnectSpectator.Caption = "Connect as Spectator";
      this.miConnectSpectator.CategoryGuid = new System.Guid("b1e08833-8d08-415c-9522-c31e9bf3c2de");
      this.miConnectSpectator.Id = 1;
      this.miConnectSpectator.Name = "miConnectSpectator";
      this.miConnectSpectator.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miConnectSpectator_ItemClick);
      // 
      // miCopyAddress
      // 
      this.miCopyAddress.Caption = "Copy Address to Clipboard";
      this.miCopyAddress.CategoryGuid = new System.Guid("b1e08833-8d08-415c-9522-c31e9bf3c2de");
      this.miCopyAddress.Id = 2;
      this.miCopyAddress.ImageIndex = 6;
      this.miCopyAddress.Name = "miCopyAddress";
      this.miCopyAddress.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miCopyAddress_ItemClick);
      // 
      // miAddRulesColumnText
      // 
      this.miAddRulesColumnText.Caption = "Add as text column to Servers table";
      this.miAddRulesColumnText.Id = 5;
      this.miAddRulesColumnText.Name = "miAddRulesColumnText";
      this.miAddRulesColumnText.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miAddRulesColumnString_ItemClick);
      // 
      // miAddRulesColumnNumeric
      // 
      this.miAddRulesColumnNumeric.Caption = "Add as numeric column to Servers table";
      this.miAddRulesColumnNumeric.Id = 6;
      this.miAddRulesColumnNumeric.Name = "miAddRulesColumnNumeric";
      this.miAddRulesColumnNumeric.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.miAddRulesColumnNumeric_ItemClick);
      // 
      // panelContainer1
      // 
      this.panelContainer1.ActiveChild = this.panelPlayers;
      this.panelContainer1.Controls.Add(this.panelPlayers);
      this.panelContainer1.Controls.Add(this.panelServerDetails);
      this.panelContainer1.Controls.Add(this.panelRules);
      this.panelContainer1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
      this.panelContainer1.ID = new System.Guid("30169f55-f874-4297-811b-db9e1af4c59a");
      this.panelContainer1.Location = new System.Drawing.Point(1103, 214);
      this.panelContainer1.Name = "panelContainer1";
      this.panelContainer1.OriginalSize = new System.Drawing.Size(362, 200);
      this.panelContainer1.Size = new System.Drawing.Size(362, 570);
      this.panelContainer1.Tabbed = true;
      this.panelContainer1.Text = "panelContainer1";
      // 
      // panelRules
      // 
      this.panelRules.Controls.Add(this.controlContainer2);
      this.panelRules.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
      this.panelRules.ID = new System.Guid("7cfd1891-8f2c-4d0a-bd2c-1bb030d15a66");
      this.panelRules.Location = new System.Drawing.Point(4, 23);
      this.panelRules.Name = "panelRules";
      this.panelRules.OriginalSize = new System.Drawing.Size(354, 518);
      this.panelRules.Size = new System.Drawing.Size(354, 516);
      this.panelRules.Text = "Rules";
      // 
      // controlContainer2
      // 
      this.controlContainer2.Controls.Add(this.gcRules);
      this.controlContainer2.Location = new System.Drawing.Point(0, 0);
      this.controlContainer2.Name = "controlContainer2";
      this.controlContainer2.Size = new System.Drawing.Size(354, 516);
      this.controlContainer2.TabIndex = 0;
      // 
      // gcRules
      // 
      this.gcRules.DataSource = this.dsRules;
      this.gcRules.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gcRules.Location = new System.Drawing.Point(0, 0);
      this.gcRules.MainView = this.gvRules;
      this.gcRules.Name = "gcRules";
      this.gcRules.Size = new System.Drawing.Size(354, 516);
      this.gcRules.TabIndex = 31;
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
            this.gridColumn1,
            this.gridColumn2});
      this.gvRules.GridControl = this.gcRules;
      this.gvRules.Name = "gvRules";
      this.gvRules.OptionsView.ShowGroupPanel = false;
      this.gvRules.OptionsView.ShowIndicator = false;
      this.gvRules.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvRules_MouseDown);
      // 
      // gridColumn1
      // 
      this.gridColumn1.Caption = "Setting";
      this.gridColumn1.FieldName = "Name";
      this.gridColumn1.Name = "gridColumn1";
      this.gridColumn1.OptionsColumn.ReadOnly = true;
      this.gridColumn1.Visible = true;
      this.gridColumn1.VisibleIndex = 0;
      this.gridColumn1.Width = 100;
      // 
      // gridColumn2
      // 
      this.gridColumn2.Caption = "Value";
      this.gridColumn2.FieldName = "Value";
      this.gridColumn2.Name = "gridColumn2";
      this.gridColumn2.OptionsColumn.ReadOnly = true;
      this.gridColumn2.Visible = true;
      this.gridColumn2.VisibleIndex = 1;
      this.gridColumn2.Width = 150;
      // 
      // panelPlayers
      // 
      this.panelPlayers.Controls.Add(this.dockPanel1_Container);
      this.panelPlayers.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
      this.panelPlayers.ID = new System.Guid("5ff9161d-077a-43fb-9f49-f8a0728b7b57");
      this.panelPlayers.Location = new System.Drawing.Point(4, 23);
      this.panelPlayers.Name = "panelPlayers";
      this.panelPlayers.Options.AllowFloating = false;
      this.panelPlayers.Options.ShowCloseButton = false;
      this.panelPlayers.OriginalSize = new System.Drawing.Size(354, 518);
      this.panelPlayers.Size = new System.Drawing.Size(354, 516);
      this.panelPlayers.Text = "Players";
      // 
      // dockPanel1_Container
      // 
      this.dockPanel1_Container.Controls.Add(this.gcPlayers);
      this.dockPanel1_Container.Location = new System.Drawing.Point(0, 0);
      this.dockPanel1_Container.Name = "dockPanel1_Container";
      this.dockPanel1_Container.Size = new System.Drawing.Size(354, 516);
      this.dockPanel1_Container.TabIndex = 0;
      // 
      // panelServerDetails
      // 
      this.panelServerDetails.Controls.Add(this.dockPanel2_Container);
      this.panelServerDetails.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
      this.panelServerDetails.ID = new System.Guid("adca8b15-d626-4469-97cf-a6cc21c21f6e");
      this.panelServerDetails.Location = new System.Drawing.Point(4, 23);
      this.panelServerDetails.Name = "panelServerDetails";
      this.panelServerDetails.Options.AllowFloating = false;
      this.panelServerDetails.Options.ShowCloseButton = false;
      this.panelServerDetails.OriginalSize = new System.Drawing.Size(354, 518);
      this.panelServerDetails.Size = new System.Drawing.Size(354, 516);
      this.panelServerDetails.Text = "Server Details";
      // 
      // dockPanel2_Container
      // 
      this.dockPanel2_Container.Controls.Add(this.gcDetails);
      this.dockPanel2_Container.Location = new System.Drawing.Point(0, 0);
      this.dockPanel2_Container.Name = "dockPanel2_Container";
      this.dockPanel2_Container.Size = new System.Drawing.Size(354, 516);
      this.dockPanel2_Container.TabIndex = 0;
      // 
      // panelServerList
      // 
      this.panelServerList.Controls.Add(this.controlContainer1);
      this.panelServerList.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
      this.panelServerList.ID = new System.Guid("865607d4-b558-4563-b50b-7827abfe171b");
      this.panelServerList.Location = new System.Drawing.Point(0, 260);
      this.panelServerList.Name = "panelServerList";
      this.panelServerList.Options.AllowDockAsTabbedDocument = false;
      this.panelServerList.Options.AllowDockRight = false;
      this.panelServerList.Options.AllowDockTop = false;
      this.panelServerList.Options.AllowFloating = false;
      this.panelServerList.Options.FloatOnDblClick = false;
      this.panelServerList.Options.ShowAutoHideButton = false;
      this.panelServerList.Options.ShowCloseButton = false;
      this.panelServerList.OriginalSize = new System.Drawing.Size(1102, 524);
      this.panelServerList.Size = new System.Drawing.Size(1103, 524);
      this.panelServerList.Text = "Servers";
      // 
      // controlContainer1
      // 
      this.controlContainer1.Controls.Add(this.gcServers);
      this.controlContainer1.Location = new System.Drawing.Point(4, 23);
      this.controlContainer1.Name = "controlContainer1";
      this.controlContainer1.Size = new System.Drawing.Size(1095, 497);
      this.controlContainer1.TabIndex = 0;
      // 
      // btnSkin
      // 
      this.btnSkin.Location = new System.Drawing.Point(514, 5);
      this.btnSkin.Name = "btnSkin";
      this.btnSkin.Size = new System.Drawing.Size(115, 25);
      this.btnSkin.TabIndex = 12;
      this.btnSkin.Text = "Change Skin";
      this.btnSkin.Click += new System.EventHandler(this.btnSkin_Click);
      // 
      // cbRefreshSelectedServer
      // 
      this.cbRefreshSelectedServer.EditValue = true;
      this.cbRefreshSelectedServer.Location = new System.Drawing.Point(136, 28);
      this.cbRefreshSelectedServer.Name = "cbRefreshSelectedServer";
      this.cbRefreshSelectedServer.Properties.AutoWidth = true;
      this.cbRefreshSelectedServer.Properties.Caption = "Refresh server when selecting a row";
      this.cbRefreshSelectedServer.Size = new System.Drawing.Size(198, 19);
      this.cbRefreshSelectedServer.TabIndex = 6;
      // 
      // labelControl1
      // 
      this.labelControl1.Location = new System.Drawing.Point(397, 9);
      this.labelControl1.Name = "labelControl1";
      this.labelControl1.Size = new System.Drawing.Size(37, 13);
      this.labelControl1.TabIndex = 2;
      this.labelControl1.Text = "Region:";
      // 
      // btnQueryMaster
      // 
      this.btnQueryMaster.ImageIndex = 1;
      this.btnQueryMaster.ImageList = this.imageCollection;
      this.btnQueryMaster.Location = new System.Drawing.Point(939, 54);
      this.btnQueryMaster.Name = "btnQueryMaster";
      this.btnQueryMaster.Size = new System.Drawing.Size(115, 25);
      this.btnQueryMaster.TabIndex = 20;
      this.btnQueryMaster.Text = "Reload Servers";
      this.btnQueryMaster.Click += new System.EventHandler(this.btnQueryMaster_Click);
      // 
      // panelTop
      // 
      this.panelTop.Controls.Add(this.panelControls);
      this.panelTop.Controls.Add(this.panelTopFill);
      this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelTop.Location = new System.Drawing.Point(0, 31);
      this.panelTop.Name = "panelTop";
      this.panelTop.Size = new System.Drawing.Size(1465, 93);
      this.panelTop.TabIndex = 0;
      // 
      // panelControls
      // 
      this.panelControls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.panelControls.Appearance.BackColor = System.Drawing.Color.Transparent;
      this.panelControls.Appearance.Options.UseBackColor = true;
      this.panelControls.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
      this.panelControls.Controls.Add(this.panelOptions);
      this.panelControls.Location = new System.Drawing.Point(0, 5);
      this.panelControls.Name = "panelControls";
      this.panelControls.Size = new System.Drawing.Size(1460, 83);
      this.panelControls.TabIndex = 25;
      // 
      // panelOptions
      // 
      this.panelOptions.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
      this.panelOptions.Controls.Add(this.rbFavGame4);
      this.panelOptions.Controls.Add(this.txtMod);
      this.panelOptions.Controls.Add(this.labelControl12);
      this.panelOptions.Controls.Add(this.txtMap);
      this.panelOptions.Controls.Add(this.labelControl11);
      this.panelOptions.Controls.Add(this.txtTagExclude);
      this.panelOptions.Controls.Add(this.txtTagInclude);
      this.panelOptions.Controls.Add(this.labelControl7);
      this.panelOptions.Controls.Add(this.cbAdvancedOptions);
      this.panelOptions.Controls.Add(this.rbFavGame2);
      this.panelOptions.Controls.Add(this.labelControl8);
      this.panelOptions.Controls.Add(this.rbFavGame3);
      this.panelOptions.Controls.Add(this.rbFavGame1);
      this.panelOptions.Controls.Add(this.labelControl2);
      this.panelOptions.Controls.Add(this.comboMasterServer);
      this.panelOptions.Controls.Add(this.cbGetFull);
      this.panelOptions.Controls.Add(this.comboGames);
      this.panelOptions.Controls.Add(this.labelControl5);
      this.panelOptions.Controls.Add(this.comboRegion);
      this.panelOptions.Controls.Add(this.btnQueryMaster);
      this.panelOptions.Controls.Add(this.labelControl4);
      this.panelOptions.Controls.Add(this.cbGetEmpty);
      this.panelOptions.Controls.Add(this.labelControl1);
      this.panelOptions.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelOptions.Location = new System.Drawing.Point(0, 0);
      this.panelOptions.Name = "panelOptions";
      this.panelOptions.Size = new System.Drawing.Size(1460, 83);
      this.panelOptions.TabIndex = 0;
      // 
      // rbFavGame4
      // 
      this.rbFavGame4.Location = new System.Drawing.Point(343, 57);
      this.rbFavGame4.Name = "rbFavGame4";
      this.rbFavGame4.Properties.AutoWidth = true;
      this.rbFavGame4.Properties.Caption = "CS:GO";
      this.rbFavGame4.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
      this.rbFavGame4.Properties.RadioGroupIndex = 1;
      this.rbFavGame4.Size = new System.Drawing.Size(54, 19);
      this.rbFavGame4.TabIndex = 9;
      this.rbFavGame4.TabStop = false;
      this.rbFavGame4.ToolTip = "Use Ctrl+Click to assign the game selected above to this button";
      this.rbFavGame4.CheckedChanged += new System.EventHandler(this.rbFavGame_CheckedChanged);
      // 
      // txtMod
      // 
      this.txtMod.Location = new System.Drawing.Point(445, 33);
      this.txtMod.MenuManager = this.barManager1;
      this.txtMod.Name = "txtMod";
      this.txtMod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
      this.txtMod.Size = new System.Drawing.Size(184, 20);
      this.txtMod.TabIndex = 11;
      this.txtMod.ToolTip = "Matches the \"directory\" field of the server details";
      this.txtMod.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtTag_ButtonClick);
      // 
      // labelControl12
      // 
      this.labelControl12.Location = new System.Drawing.Point(410, 35);
      this.labelControl12.Name = "labelControl12";
      this.labelControl12.Size = new System.Drawing.Size(24, 13);
      this.labelControl12.TabIndex = 10;
      this.labelControl12.Text = "Mod:";
      // 
      // txtMap
      // 
      this.txtMap.Location = new System.Drawing.Point(725, 6);
      this.txtMap.MenuManager = this.barManager1;
      this.txtMap.Name = "txtMap";
      this.txtMap.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
      this.txtMap.Size = new System.Drawing.Size(184, 20);
      this.txtMap.TabIndex = 13;
      this.txtMap.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtTag_ButtonClick);
      // 
      // labelControl11
      // 
      this.labelControl11.Location = new System.Drawing.Point(695, 9);
      this.labelControl11.Name = "labelControl11";
      this.labelControl11.Size = new System.Drawing.Size(24, 13);
      this.labelControl11.TabIndex = 12;
      this.labelControl11.Text = "Map:";
      // 
      // txtTagExclude
      // 
      this.txtTagExclude.Location = new System.Drawing.Point(725, 59);
      this.txtTagExclude.MenuManager = this.barManager1;
      this.txtTagExclude.Name = "txtTagExclude";
      this.txtTagExclude.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
      this.txtTagExclude.Size = new System.Drawing.Size(184, 20);
      this.txtTagExclude.TabIndex = 17;
      this.txtTagExclude.ToolTip = "Comma separated list of tags which must not occur in the server\'s sv_tags";
      this.txtTagExclude.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtTag_ButtonClick);
      // 
      // txtTagInclude
      // 
      this.txtTagInclude.Location = new System.Drawing.Point(725, 32);
      this.txtTagInclude.MenuManager = this.barManager1;
      this.txtTagInclude.Name = "txtTagInclude";
      this.txtTagInclude.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
      this.txtTagInclude.Size = new System.Drawing.Size(184, 20);
      this.txtTagInclude.TabIndex = 15;
      this.txtTagInclude.ToolTip = "Comma separated list of tags that must all be included in the server\'s sv_tags";
      this.txtTagInclude.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtTag_ButtonClick);
      // 
      // labelControl7
      // 
      this.labelControl7.Location = new System.Drawing.Point(13, 10);
      this.labelControl7.Name = "labelControl7";
      this.labelControl7.Size = new System.Drawing.Size(71, 13);
      this.labelControl7.TabIndex = 0;
      this.labelControl7.Text = "Master server:";
      // 
      // cbAdvancedOptions
      // 
      this.cbAdvancedOptions.ImageIndex = 4;
      this.cbAdvancedOptions.ImageList = this.imageCollection;
      this.cbAdvancedOptions.Location = new System.Drawing.Point(1070, 54);
      this.cbAdvancedOptions.Name = "cbAdvancedOptions";
      this.cbAdvancedOptions.Size = new System.Drawing.Size(115, 25);
      this.cbAdvancedOptions.TabIndex = 21;
      this.cbAdvancedOptions.Text = "Show Options";
      this.cbAdvancedOptions.CheckedChanged += new System.EventHandler(this.cbAdvancedOptions_CheckedChanged);
      // 
      // labelControl8
      // 
      this.labelControl8.Location = new System.Drawing.Point(652, 61);
      this.labelControl8.Name = "labelControl8";
      this.labelControl8.Size = new System.Drawing.Size(67, 13);
      this.labelControl8.TabIndex = 16;
      this.labelControl8.Text = "Exclude Tags:";
      // 
      // labelControl2
      // 
      this.labelControl2.Location = new System.Drawing.Point(652, 35);
      this.labelControl2.Name = "labelControl2";
      this.labelControl2.Size = new System.Drawing.Size(65, 13);
      this.labelControl2.TabIndex = 14;
      this.labelControl2.Text = "Include Tags:";
      // 
      // comboMasterServer
      // 
      this.comboMasterServer.Location = new System.Drawing.Point(99, 7);
      this.comboMasterServer.Name = "comboMasterServer";
      this.comboMasterServer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.comboMasterServer.Size = new System.Drawing.Size(268, 20);
      this.comboMasterServer.TabIndex = 1;
      // 
      // cbGetFull
      // 
      this.cbGetFull.Location = new System.Drawing.Point(939, 26);
      this.cbGetFull.Name = "cbGetFull";
      this.cbGetFull.Properties.AutoWidth = true;
      this.cbGetFull.Properties.Caption = "Get full servers";
      this.cbGetFull.Size = new System.Drawing.Size(95, 19);
      this.cbGetFull.TabIndex = 19;
      // 
      // comboGames
      // 
      this.comboGames.Location = new System.Drawing.Point(99, 33);
      this.comboGames.Name = "comboGames";
      this.comboGames.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.comboGames.Properties.DropDownRows = 30;
      this.comboGames.Size = new System.Drawing.Size(268, 20);
      this.comboGames.TabIndex = 5;
      this.comboGames.SelectedIndexChanged += new System.EventHandler(this.comboGames_SelectedIndexChanged);
      // 
      // labelControl5
      // 
      this.labelControl5.Location = new System.Drawing.Point(35, 60);
      this.labelControl5.Name = "labelControl5";
      this.labelControl5.Size = new System.Drawing.Size(49, 13);
      this.labelControl5.TabIndex = 6;
      this.labelControl5.Text = "Favorites:";
      this.labelControl5.ToolTip = "To change a favorite, select a game and hold the Ctrl key and click on a fav";
      // 
      // labelControl4
      // 
      this.labelControl4.Location = new System.Drawing.Point(53, 36);
      this.labelControl4.Name = "labelControl4";
      this.labelControl4.Size = new System.Drawing.Size(31, 13);
      this.labelControl4.TabIndex = 4;
      this.labelControl4.Text = "Game:";
      this.labelControl4.ToolTip = "If a game is not listed here, you can enter it\'s Steam ApplicationID here";
      // 
      // cbGetEmpty
      // 
      this.cbGetEmpty.Location = new System.Drawing.Point(939, 7);
      this.cbGetEmpty.Name = "cbGetEmpty";
      this.cbGetEmpty.Properties.AutoWidth = true;
      this.cbGetEmpty.Properties.Caption = "Get empty servers";
      this.cbGetEmpty.Size = new System.Drawing.Size(111, 19);
      this.cbGetEmpty.TabIndex = 18;
      // 
      // panelTopFill
      // 
      this.panelTopFill.Appearance.BackColor = System.Drawing.Color.Transparent;
      this.panelTopFill.Appearance.Options.UseBackColor = true;
      this.panelTopFill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
      this.panelTopFill.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelTopFill.Location = new System.Drawing.Point(2, 2);
      this.panelTopFill.Name = "panelTopFill";
      this.panelTopFill.Size = new System.Drawing.Size(1461, 89);
      this.panelTopFill.TabIndex = 25;
      // 
      // linkFilter1
      // 
      this.linkFilter1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
      this.linkFilter1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
      this.linkFilter1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.linkFilter1.Location = new System.Drawing.Point(725, 38);
      this.linkFilter1.Name = "linkFilter1";
      this.linkFilter1.Size = new System.Drawing.Size(329, 34);
      this.linkFilter1.TabIndex = 14;
      this.linkFilter1.Text = "HINT: Use the top row of the table for simple filters or the\r\n<href>filter editor" +
    "</href> for more complex filters.";
      this.linkFilter1.HyperlinkClick += new DevExpress.Utils.HyperlinkClickEventHandler(this.linkFilter_HyperlinkClick);
      // 
      // labelControl3
      // 
      this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
      this.labelControl3.Location = new System.Drawing.Point(136, 53);
      this.labelControl3.Name = "labelControl3";
      this.labelControl3.Size = new System.Drawing.Size(215, 26);
      this.labelControl3.TabIndex = 7;
      this.labelControl3.Text = "NOTE: A Refresh will reorder the selected row, if its sorting value changes";
      // 
      // timerUpdateServerList
      // 
      this.timerUpdateServerList.Enabled = true;
      this.timerUpdateServerList.Interval = 1000;
      this.timerUpdateServerList.Tick += new System.EventHandler(this.timerUpdateServerList_Tick);
      // 
      // panelAdvancedOptions
      // 
      this.panelAdvancedOptions.Controls.Add(this.txtGameServer);
      this.panelAdvancedOptions.Controls.Add(this.rbAddressGamePort);
      this.panelAdvancedOptions.Controls.Add(this.rbAddressQueryPort);
      this.panelAdvancedOptions.Controls.Add(this.labelControl6);
      this.panelAdvancedOptions.Controls.Add(this.labelControl10);
      this.panelAdvancedOptions.Controls.Add(this.linkFilter1);
      this.panelAdvancedOptions.Controls.Add(this.labelControl9);
      this.panelAdvancedOptions.Controls.Add(this.btnSkin);
      this.panelAdvancedOptions.Controls.Add(this.labelControl3);
      this.panelAdvancedOptions.Controls.Add(this.cbAlert);
      this.panelAdvancedOptions.Controls.Add(this.cbRefreshSelectedServer);
      this.panelAdvancedOptions.Controls.Add(this.rbAddressHidden);
      this.panelAdvancedOptions.Controls.Add(this.spinRefreshInterval);
      this.panelAdvancedOptions.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelAdvancedOptions.Location = new System.Drawing.Point(0, 124);
      this.panelAdvancedOptions.Name = "panelAdvancedOptions";
      this.panelAdvancedOptions.Size = new System.Drawing.Size(1465, 90);
      this.panelAdvancedOptions.TabIndex = 1;
      this.panelAdvancedOptions.Visible = false;
      // 
      // txtGameServer
      // 
      this.txtGameServer.Location = new System.Drawing.Point(445, 50);
      this.txtGameServer.MenuManager = this.barManager1;
      this.txtGameServer.Name = "txtGameServer";
      this.txtGameServer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)});
      this.txtGameServer.Size = new System.Drawing.Size(184, 20);
      this.txtGameServer.TabIndex = 15;
      this.txtGameServer.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtGameServer_ButtonClick);
      this.txtGameServer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGameServer_KeyDown);
      // 
      // rbAddressGamePort
      // 
      this.rbAddressGamePort.Location = new System.Drawing.Point(12, 66);
      this.rbAddressGamePort.Name = "rbAddressGamePort";
      this.rbAddressGamePort.Properties.AutoWidth = true;
      this.rbAddressGamePort.Properties.Caption = "Game Port";
      this.rbAddressGamePort.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
      this.rbAddressGamePort.Properties.RadioGroupIndex = 1;
      this.rbAddressGamePort.Size = new System.Drawing.Size(72, 19);
      this.rbAddressGamePort.TabIndex = 3;
      this.rbAddressGamePort.TabStop = false;
      this.rbAddressGamePort.CheckedChanged += new System.EventHandler(this.rbAddress_CheckedChanged);
      // 
      // rbAddressQueryPort
      // 
      this.rbAddressQueryPort.Location = new System.Drawing.Point(12, 47);
      this.rbAddressQueryPort.Name = "rbAddressQueryPort";
      this.rbAddressQueryPort.Properties.AutoWidth = true;
      this.rbAddressQueryPort.Properties.Caption = "Query Port";
      this.rbAddressQueryPort.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
      this.rbAddressQueryPort.Properties.RadioGroupIndex = 1;
      this.rbAddressQueryPort.Size = new System.Drawing.Size(75, 19);
      this.rbAddressQueryPort.TabIndex = 2;
      this.rbAddressQueryPort.TabStop = false;
      this.rbAddressQueryPort.CheckedChanged += new System.EventHandler(this.rbAddress_CheckedChanged);
      // 
      // labelControl6
      // 
      this.labelControl6.Location = new System.Drawing.Point(369, 53);
      this.labelControl6.Name = "labelControl6";
      this.labelControl6.Size = new System.Drawing.Size(65, 13);
      this.labelControl6.TabIndex = 8;
      this.labelControl6.Text = "Game server:";
      // 
      // labelControl10
      // 
      this.labelControl10.Location = new System.Drawing.Point(13, 9);
      this.labelControl10.Name = "labelControl10";
      this.labelControl10.Size = new System.Drawing.Size(78, 13);
      this.labelControl10.TabIndex = 0;
      this.labelControl10.Text = "Server Address:";
      // 
      // labelControl9
      // 
      this.labelControl9.Location = new System.Drawing.Point(136, 9);
      this.labelControl9.Name = "labelControl9";
      this.labelControl9.Size = new System.Drawing.Size(113, 13);
      this.labelControl9.TabIndex = 4;
      this.labelControl9.Text = "Refresh interval (mins):";
      // 
      // cbAlert
      // 
      this.cbAlert.ImageIndex = 5;
      this.cbAlert.ImageList = this.imageCollection;
      this.cbAlert.Location = new System.Drawing.Point(725, 5);
      this.cbAlert.Name = "cbAlert";
      this.cbAlert.Size = new System.Drawing.Size(329, 25);
      this.cbAlert.TabIndex = 13;
      this.cbAlert.Text = "Notify me when servers pass my table filter below";
      this.cbAlert.CheckedChanged += new System.EventHandler(this.cbAlert_CheckedChanged);
      // 
      // rbAddressHidden
      // 
      this.rbAddressHidden.Location = new System.Drawing.Point(12, 28);
      this.rbAddressHidden.Name = "rbAddressHidden";
      this.rbAddressHidden.Properties.AutoWidth = true;
      this.rbAddressHidden.Properties.Caption = "Don\'t show";
      this.rbAddressHidden.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
      this.rbAddressHidden.Properties.RadioGroupIndex = 1;
      this.rbAddressHidden.Size = new System.Drawing.Size(75, 19);
      this.rbAddressHidden.TabIndex = 1;
      this.rbAddressHidden.TabStop = false;
      this.rbAddressHidden.CheckedChanged += new System.EventHandler(this.rbAddress_CheckedChanged);
      // 
      // spinRefreshInterval
      // 
      this.spinRefreshInterval.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.spinRefreshInterval.Location = new System.Drawing.Point(264, 6);
      this.spinRefreshInterval.MenuManager = this.barManager1;
      this.spinRefreshInterval.Name = "spinRefreshInterval";
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
      this.spinRefreshInterval.Size = new System.Drawing.Size(47, 20);
      this.spinRefreshInterval.TabIndex = 5;
      this.spinRefreshInterval.EditValueChanged += new System.EventHandler(this.spinRefreshInterval_EditValueChanged);
      // 
      // panelControl1
      // 
      this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
      this.panelControl1.Controls.Add(this.txtStatus);
      this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panelControl1.Location = new System.Drawing.Point(0, 784);
      this.panelControl1.Name = "panelControl1";
      this.panelControl1.Size = new System.Drawing.Size(1465, 24);
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
            new DevExpress.XtraBars.LinkPersistInfo(this.miCopyAddress, true)});
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
      // ServerBrowserForm
      // 
      this.Appearance.Options.UseFont = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1465, 808);
      this.Controls.Add(this.panelServerList);
      this.Controls.Add(this.panelContainer1);
      this.Controls.Add(this.panelAdvancedOptions);
      this.Controls.Add(this.panelControl1);
      this.Controls.Add(this.panelTop);
      this.Controls.Add(this.barDockControlLeft);
      this.Controls.Add(this.barDockControlRight);
      this.Controls.Add(this.barDockControlBottom);
      this.Controls.Add(this.barDockControlTop);
      this.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.Name = "ServerBrowserForm";
      this.Text = "Steam Server Browser";
      ((System.ComponentModel.ISupportInitialize)(this.riCheckEdit)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.gcDetails)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboRegion.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbFavGame2.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbFavGame1.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbFavGame3.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.gcPlayers)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dsPlayer)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.gvPlayers)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.gcServers)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dsServers)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.gvServers)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.riCountryFlagEdit)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.imgFlags)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).EndInit();
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
      ((System.ComponentModel.ISupportInitialize)(this.cbRefreshSelectedServer.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.panelTop)).EndInit();
      this.panelTop.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.panelControls)).EndInit();
      this.panelControls.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.panelOptions)).EndInit();
      this.panelOptions.ResumeLayout(false);
      this.panelOptions.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.rbFavGame4.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtMod.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtMap.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtTagExclude.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtTagInclude.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboMasterServer.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbGetFull.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboGames.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbGetEmpty.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.panelTopFill)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.panelAdvancedOptions)).EndInit();
      this.panelAdvancedOptions.ResumeLayout(false);
      this.panelAdvancedOptions.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtGameServer.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbAddressGamePort.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbAddressQueryPort.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbAddressHidden.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.spinRefreshInterval.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
      this.panelControl1.ResumeLayout(false);
      this.panelControl1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.menuServers)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.menuPlayers)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.menuRules)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    protected DevExpress.XtraGrid.GridControl gcDetails;
    protected DevExpress.XtraGrid.Views.Grid.GridView gvDetails;
    protected DevExpress.XtraGrid.GridControl gcPlayers;
    protected DevExpress.XtraGrid.Views.Grid.GridView gvPlayers;
    protected DevExpress.XtraGrid.GridControl gcServers;
    protected DevExpress.XtraGrid.Views.Grid.GridView gvServers;
    protected CheckEdit rbFavGame3;
    protected ComboBoxEdit comboRegion;
    protected CheckEdit rbFavGame2;
    protected CheckEdit rbFavGame1;
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
    protected DevExpress.XtraGrid.Columns.GridColumn colName1;
    protected DevExpress.XtraGrid.Columns.GridColumn colScore;
    protected DevExpress.XtraGrid.Columns.GridColumn colTime;
    protected DevExpress.XtraGrid.Columns.GridColumn colStatus;
    protected DevExpress.XtraGrid.Columns.GridColumn colKey;
    protected DevExpress.XtraGrid.Columns.GridColumn colValue;
    protected DevExpress.XtraGrid.Columns.GridColumn colTags;
    protected DevExpress.XtraGrid.Columns.GridColumn colPrivate;
    protected CheckEdit cbRefreshSelectedServer;
    protected LabelControl labelControl1;
    protected SimpleButton btnQueryMaster;
    protected SimpleButton btnSkin;
    protected PanelControl panelTop;
    protected PanelControl panelTopFill;
    protected PanelControl panelControls;
    protected PanelControl panelOptions;
    protected LabelControl labelControl3;
    protected DevExpress.XtraBars.Docking.DockPanel panelServerList;
    protected DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
    protected DevExpress.XtraBars.Docking.DockPanel panelContainer1;
    protected DevExpress.XtraBars.Docking.DockPanel panelRules;
    protected DevExpress.XtraBars.Docking.ControlContainer controlContainer2;
    protected DevExpress.XtraGrid.GridControl gcRules;
    protected DevExpress.XtraGrid.Views.Grid.GridView gvRules;
    protected DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    protected DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
    protected System.Windows.Forms.BindingSource dsRules;
    protected DevExpress.XtraGrid.Columns.GridColumn colDedicated;
    protected ComboBoxEdit comboGames;
    protected LabelControl labelControl4;
    protected LabelControl labelControl5;
    protected System.Windows.Forms.Timer timerUpdateServerList;
    protected PanelControl panelAdvancedOptions;
    protected CheckButton cbAdvancedOptions;
    protected PanelControl panelControl1;
    protected LabelControl txtStatus;
    protected DevExpress.XtraBars.BarDockControl barDockControlLeft;
    protected DevExpress.XtraBars.BarDockControl barDockControlRight;
    protected DevExpress.XtraBars.BarDockControl barDockControlBottom;
    protected DevExpress.XtraBars.BarDockControl barDockControlTop;
    protected DevExpress.XtraBars.BarManager barManager1;
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
    protected ButtonEdit txtTagExclude;
    protected ButtonEdit txtTagInclude;
    protected ButtonEdit txtMap;
    protected LabelControl labelControl11;
    protected ButtonEdit txtMod;
    protected LabelControl labelControl12;
    protected ButtonEdit txtGameServer;
    protected CheckEdit rbFavGame4;
    protected DevExpress.XtraBars.Bar barMenu;
    protected DevExpress.XtraBars.BarButtonItem miReloadServers;
    private DevExpress.XtraBars.BarButtonItem miAddRulesColumnText;
    private DevExpress.XtraBars.PopupMenu menuRules;
    private DevExpress.XtraBars.BarButtonItem miAddRulesColumnNumeric;
    private DevExpress.XtraGrid.Columns.GridColumn colLocation;
    private DevExpress.Utils.ImageCollection imgFlags;
    private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riCountryFlagEdit;
    private DevExpress.Utils.ToolTipController toolTipController;



  }
}
