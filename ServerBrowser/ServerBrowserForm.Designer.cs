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
      this.gcDetails = new DevExpress.XtraGrid.GridControl();
      this.gvDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
      this.colKey = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colValue = new DevExpress.XtraGrid.Columns.GridColumn();
      this.comboRegion = new DevExpress.XtraEditors.ComboBoxEdit();
      this.txtAppId = new DevExpress.XtraEditors.TextEdit();
      this.rbCustom = new DevExpress.XtraEditors.CheckEdit();
      this.rbToxikk = new DevExpress.XtraEditors.CheckEdit();
      this.rbReflex = new DevExpress.XtraEditors.CheckEdit();
      this.rbQuakeLive = new DevExpress.XtraEditors.CheckEdit();
      this.gcPlayers = new DevExpress.XtraGrid.GridControl();
      this.dsPlayer = new System.Windows.Forms.BindingSource();
      this.gvPlayers = new DevExpress.XtraGrid.Views.Grid.GridView();
      this.colName1 = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colScore = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colTime = new DevExpress.XtraGrid.Columns.GridColumn();
      this.gcServers = new DevExpress.XtraGrid.GridControl();
      this.dsServers = new System.Windows.Forms.BindingSource();
      this.gvServers = new DevExpress.XtraGrid.Views.Grid.GridView();
      this.colEndPoint = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colPrivate = new DevExpress.XtraGrid.Columns.GridColumn();
      this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
      this.gridGameType = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colKeywords = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colPlayerCount = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colMap = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colPing = new DevExpress.XtraGrid.Columns.GridColumn();
      this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
      this.barManager1 = new DevExpress.XtraBars.BarManager();
      this.barStatus = new DevExpress.XtraBars.Bar();
      this.txtStatus = new DevExpress.XtraBars.BarStaticItem();
      this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
      this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager();
      this.panelServerDetails = new DevExpress.XtraBars.Docking.DockPanel();
      this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
      this.panelPlayers = new DevExpress.XtraBars.Docking.DockPanel();
      this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
      this.panelServerList = new DevExpress.XtraBars.Docking.DockPanel();
      this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
      this.btnSkin = new DevExpress.XtraEditors.SimpleButton();
      this.cbAutoUpdateSelectedServer = new DevExpress.XtraEditors.CheckEdit();
      this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
      this.btnQueryMaster = new DevExpress.XtraEditors.SimpleButton();
      this.panelTop = new DevExpress.XtraEditors.PanelControl();
      this.panelTopFill = new DevExpress.XtraEditors.PanelControl();
      this.panelControls = new DevExpress.XtraEditors.PanelControl();
      this.panelOptions = new DevExpress.XtraEditors.PanelControl();
      this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
      this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
      this.panelGame = new DevExpress.XtraEditors.PanelControl();
      this.picLogo = new DevExpress.XtraEditors.PictureEdit();
      ((System.ComponentModel.ISupportInitialize)(this.gcDetails)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboRegion.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtAppId.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbCustom.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbToxikk.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbReflex.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbQuakeLive.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.gcPlayers)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dsPlayer)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.gvPlayers)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.gcServers)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dsServers)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.gvServers)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
      this.panelServerDetails.SuspendLayout();
      this.dockPanel2_Container.SuspendLayout();
      this.panelPlayers.SuspendLayout();
      this.dockPanel1_Container.SuspendLayout();
      this.panelServerList.SuspendLayout();
      this.controlContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.cbAutoUpdateSelectedServer.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.panelTop)).BeginInit();
      this.panelTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.panelTopFill)).BeginInit();
      this.panelTopFill.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.panelControls)).BeginInit();
      this.panelControls.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.panelOptions)).BeginInit();
      this.panelOptions.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.panelGame)).BeginInit();
      this.panelGame.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picLogo.Properties)).BeginInit();
      this.SuspendLayout();
      // 
      // gcDetails
      // 
      this.gcDetails.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gcDetails.Location = new System.Drawing.Point(0, 0);
      this.gcDetails.MainView = this.gvDetails;
      this.gcDetails.Name = "gcDetails";
      this.gcDetails.Size = new System.Drawing.Size(252, 698);
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
      this.comboRegion.Location = new System.Drawing.Point(112, 3);
      this.comboRegion.Name = "comboRegion";
      this.comboRegion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.comboRegion.Properties.DropDownRows = 10;
      this.comboRegion.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
      this.comboRegion.Size = new System.Drawing.Size(184, 20);
      this.comboRegion.TabIndex = 18;
      this.comboRegion.SelectedIndexChanged += new System.EventHandler(this.comboRegion_SelectedIndexChanged);
      // 
      // txtAppId
      // 
      this.txtAppId.Location = new System.Drawing.Point(145, 29);
      this.txtAppId.Name = "txtAppId";
      this.txtAppId.Size = new System.Drawing.Size(117, 20);
      this.txtAppId.TabIndex = 17;
      // 
      // rbCustom
      // 
      this.rbCustom.Location = new System.Drawing.Point(30, 29);
      this.rbCustom.Name = "rbCustom";
      this.rbCustom.Properties.Caption = "Custom AppID";
      this.rbCustom.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
      this.rbCustom.Properties.RadioGroupIndex = 1;
      this.rbCustom.Size = new System.Drawing.Size(107, 19);
      this.rbCustom.TabIndex = 16;
      this.rbCustom.TabStop = false;
      // 
      // rbToxikk
      // 
      this.rbToxikk.Location = new System.Drawing.Point(108, 3);
      this.rbToxikk.Name = "rbToxikk";
      this.rbToxikk.Properties.Caption = "Toxikk";
      this.rbToxikk.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
      this.rbToxikk.Properties.RadioGroupIndex = 1;
      this.rbToxikk.Size = new System.Drawing.Size(71, 19);
      this.rbToxikk.TabIndex = 15;
      this.rbToxikk.TabStop = false;
      this.rbToxikk.CheckedChanged += new System.EventHandler(this.rbGame_CheckedChanged);
      // 
      // rbReflex
      // 
      this.rbReflex.Location = new System.Drawing.Point(30, 3);
      this.rbReflex.Name = "rbReflex";
      this.rbReflex.Properties.Caption = "Reflex";
      this.rbReflex.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
      this.rbReflex.Properties.RadioGroupIndex = 1;
      this.rbReflex.Size = new System.Drawing.Size(71, 19);
      this.rbReflex.TabIndex = 14;
      this.rbReflex.TabStop = false;
      this.rbReflex.CheckedChanged += new System.EventHandler(this.rbGame_CheckedChanged);
      // 
      // rbQuakeLive
      // 
      this.rbQuakeLive.Location = new System.Drawing.Point(187, 3);
      this.rbQuakeLive.Name = "rbQuakeLive";
      this.rbQuakeLive.Properties.Caption = "Quake Live";
      this.rbQuakeLive.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
      this.rbQuakeLive.Properties.RadioGroupIndex = 1;
      this.rbQuakeLive.Size = new System.Drawing.Size(87, 19);
      this.rbQuakeLive.TabIndex = 13;
      this.rbQuakeLive.TabStop = false;
      this.rbQuakeLive.CheckedChanged += new System.EventHandler(this.rbGame_CheckedChanged);
      // 
      // gcPlayers
      // 
      this.gcPlayers.DataSource = this.dsPlayer;
      this.gcPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gcPlayers.Location = new System.Drawing.Point(0, 0);
      this.gcPlayers.MainView = this.gvPlayers;
      this.gcPlayers.Name = "gcPlayers";
      this.gcPlayers.Size = new System.Drawing.Size(292, 698);
      this.gcPlayers.TabIndex = 15;
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
      this.gvPlayers.OptionsBehavior.Editable = false;
      this.gvPlayers.OptionsView.ShowGroupPanel = false;
      this.gvPlayers.OptionsView.ShowIndicator = false;
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
            this.repositoryItemCheckEdit1});
      this.gcServers.Size = new System.Drawing.Size(972, 698);
      this.gcServers.TabIndex = 16;
      this.gcServers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvServers});
      // 
      // dsServers
      // 
      this.dsServers.DataSource = typeof(ServerBrowser.ServerBrowserForm.ServerRow);
      // 
      // gvServers
      // 
      this.gvServers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colEndPoint,
            this.colName,
            this.colPrivate,
            this.gridGameType,
            this.colKeywords,
            this.colPlayerCount,
            this.colMap,
            this.colPing,
            this.colStatus});
      this.gvServers.GridControl = this.gcServers;
      this.gvServers.Name = "gvServers";
      this.gvServers.OptionsDetail.EnableMasterViewMode = false;
      this.gvServers.OptionsView.ColumnAutoWidth = false;
      this.gvServers.OptionsView.ShowAutoFilterRow = true;
      this.gvServers.OptionsView.ShowGroupPanel = false;
      this.gvServers.OptionsView.ShowIndicator = false;
      this.gvServers.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colPlayerCount, DevExpress.Data.ColumnSortOrder.Descending)});
      this.gvServers.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvServers_FocusedRowChanged);
      this.gvServers.CustomColumnSort += new DevExpress.XtraGrid.Views.Base.CustomColumnSortEventHandler(this.gvServers_CustomColumnSort);
      this.gvServers.DoubleClick += new System.EventHandler(this.gvServers_DoubleClick);
      // 
      // colEndPoint
      // 
      this.colEndPoint.Caption = "Address";
      this.colEndPoint.FieldName = "EndPoint";
      this.colEndPoint.Name = "colEndPoint";
      this.colEndPoint.OptionsColumn.ReadOnly = true;
      this.colEndPoint.Visible = true;
      this.colEndPoint.VisibleIndex = 0;
      this.colEndPoint.Width = 132;
      // 
      // colName
      // 
      this.colName.FieldName = "ServerInfo.Name";
      this.colName.Name = "colName";
      this.colName.OptionsColumn.AllowEdit = false;
      this.colName.OptionsColumn.ReadOnly = true;
      this.colName.Visible = true;
      this.colName.VisibleIndex = 1;
      this.colName.Width = 274;
      // 
      // colPrivate
      // 
      this.colPrivate.Caption = "Private";
      this.colPrivate.ColumnEdit = this.repositoryItemCheckEdit1;
      this.colPrivate.FieldName = "ServerInfo.IsPrivate";
      this.colPrivate.Name = "colPrivate";
      this.colPrivate.OptionsColumn.AllowEdit = false;
      this.colPrivate.Visible = true;
      this.colPrivate.VisibleIndex = 2;
      this.colPrivate.Width = 45;
      // 
      // repositoryItemCheckEdit1
      // 
      this.repositoryItemCheckEdit1.AutoHeight = false;
      this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
      // 
      // gridGameType
      // 
      this.gridGameType.Caption = "Gametype";
      this.gridGameType.FieldName = "ServerInfo.Description";
      this.gridGameType.Name = "gridGameType";
      this.gridGameType.OptionsColumn.AllowEdit = false;
      this.gridGameType.Visible = true;
      this.gridGameType.VisibleIndex = 3;
      this.gridGameType.Width = 101;
      // 
      // colKeywords
      // 
      this.colKeywords.Caption = "Keywords";
      this.colKeywords.FieldName = "ServerInfo.Extra.Keywords";
      this.colKeywords.Name = "colKeywords";
      this.colKeywords.OptionsColumn.AllowEdit = false;
      this.colKeywords.Visible = true;
      this.colKeywords.VisibleIndex = 4;
      this.colKeywords.Width = 127;
      // 
      // colPlayerCount
      // 
      this.colPlayerCount.Caption = "Players";
      this.colPlayerCount.FieldName = "PlayerCount";
      this.colPlayerCount.Name = "colPlayerCount";
      this.colPlayerCount.OptionsColumn.AllowEdit = false;
      this.colPlayerCount.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
      this.colPlayerCount.Visible = true;
      this.colPlayerCount.VisibleIndex = 5;
      this.colPlayerCount.Width = 65;
      // 
      // colMap
      // 
      this.colMap.Caption = "Map";
      this.colMap.FieldName = "ServerInfo.Map";
      this.colMap.Name = "colMap";
      this.colMap.OptionsColumn.AllowEdit = false;
      this.colMap.Visible = true;
      this.colMap.VisibleIndex = 6;
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
      this.colPing.VisibleIndex = 7;
      this.colPing.Width = 32;
      // 
      // colStatus
      // 
      this.colStatus.Caption = "Status";
      this.colStatus.FieldName = "Status";
      this.colStatus.Name = "colStatus";
      this.colStatus.OptionsColumn.AllowEdit = false;
      this.colStatus.Visible = true;
      this.colStatus.VisibleIndex = 8;
      this.colStatus.Width = 61;
      // 
      // barManager1
      // 
      this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barStatus});
      this.barManager1.DockControls.Add(this.barDockControlTop);
      this.barManager1.DockControls.Add(this.barDockControlBottom);
      this.barManager1.DockControls.Add(this.barDockControlLeft);
      this.barManager1.DockControls.Add(this.barDockControlRight);
      this.barManager1.DockManager = this.dockManager1;
      this.barManager1.Form = this;
      this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.txtStatus});
      this.barManager1.MaxItemId = 1;
      this.barManager1.StatusBar = this.barStatus;
      // 
      // barStatus
      // 
      this.barStatus.BarName = "Status bar";
      this.barStatus.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
      this.barStatus.DockCol = 0;
      this.barStatus.DockRow = 0;
      this.barStatus.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
      this.barStatus.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.txtStatus)});
      this.barStatus.OptionsBar.AllowQuickCustomization = false;
      this.barStatus.OptionsBar.DrawDragBorder = false;
      this.barStatus.OptionsBar.UseWholeRow = true;
      this.barStatus.Text = "Status bar";
      // 
      // txtStatus
      // 
      this.txtStatus.Id = 0;
      this.txtStatus.Name = "txtStatus";
      this.txtStatus.TextAlignment = System.Drawing.StringAlignment.Near;
      // 
      // barDockControlTop
      // 
      this.barDockControlTop.CausesValidation = false;
      this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
      this.barDockControlTop.Size = new System.Drawing.Size(1540, 0);
      // 
      // barDockControlBottom
      // 
      this.barDockControlBottom.CausesValidation = false;
      this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.barDockControlBottom.Location = new System.Drawing.Point(0, 783);
      this.barDockControlBottom.Size = new System.Drawing.Size(1540, 25);
      // 
      // barDockControlLeft
      // 
      this.barDockControlLeft.CausesValidation = false;
      this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
      this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
      this.barDockControlLeft.Size = new System.Drawing.Size(0, 783);
      // 
      // barDockControlRight
      // 
      this.barDockControlRight.CausesValidation = false;
      this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
      this.barDockControlRight.Location = new System.Drawing.Point(1540, 0);
      this.barDockControlRight.Size = new System.Drawing.Size(0, 783);
      // 
      // dockManager1
      // 
      this.dockManager1.Form = this;
      this.dockManager1.MenuManager = this.barManager1;
      this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.panelServerDetails,
            this.panelPlayers,
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
      // panelServerDetails
      // 
      this.panelServerDetails.Controls.Add(this.dockPanel2_Container);
      this.panelServerDetails.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
      this.panelServerDetails.ID = new System.Guid("adca8b15-d626-4469-97cf-a6cc21c21f6e");
      this.panelServerDetails.Location = new System.Drawing.Point(1280, 58);
      this.panelServerDetails.Name = "panelServerDetails";
      this.panelServerDetails.Options.AllowFloating = false;
      this.panelServerDetails.Options.ShowCloseButton = false;
      this.panelServerDetails.OriginalSize = new System.Drawing.Size(260, 200);
      this.panelServerDetails.Size = new System.Drawing.Size(260, 725);
      this.panelServerDetails.Text = "Server Details";
      // 
      // dockPanel2_Container
      // 
      this.dockPanel2_Container.Controls.Add(this.gcDetails);
      this.dockPanel2_Container.Location = new System.Drawing.Point(4, 23);
      this.dockPanel2_Container.Name = "dockPanel2_Container";
      this.dockPanel2_Container.Size = new System.Drawing.Size(252, 698);
      this.dockPanel2_Container.TabIndex = 0;
      // 
      // panelPlayers
      // 
      this.panelPlayers.Controls.Add(this.dockPanel1_Container);
      this.panelPlayers.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
      this.panelPlayers.ID = new System.Guid("5ff9161d-077a-43fb-9f49-f8a0728b7b57");
      this.panelPlayers.Location = new System.Drawing.Point(980, 58);
      this.panelPlayers.Name = "panelPlayers";
      this.panelPlayers.Options.AllowFloating = false;
      this.panelPlayers.Options.ShowCloseButton = false;
      this.panelPlayers.OriginalSize = new System.Drawing.Size(300, 200);
      this.panelPlayers.Size = new System.Drawing.Size(300, 725);
      this.panelPlayers.Text = "Players";
      // 
      // dockPanel1_Container
      // 
      this.dockPanel1_Container.Controls.Add(this.gcPlayers);
      this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
      this.dockPanel1_Container.Name = "dockPanel1_Container";
      this.dockPanel1_Container.Size = new System.Drawing.Size(292, 698);
      this.dockPanel1_Container.TabIndex = 0;
      // 
      // panelServerList
      // 
      this.panelServerList.Controls.Add(this.controlContainer1);
      this.panelServerList.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
      this.panelServerList.ID = new System.Guid("865607d4-b558-4563-b50b-7827abfe171b");
      this.panelServerList.Location = new System.Drawing.Point(0, 58);
      this.panelServerList.Name = "panelServerList";
      this.panelServerList.Options.AllowDockAsTabbedDocument = false;
      this.panelServerList.Options.AllowDockBottom = false;
      this.panelServerList.Options.AllowDockRight = false;
      this.panelServerList.Options.AllowDockTop = false;
      this.panelServerList.Options.AllowFloating = false;
      this.panelServerList.Options.FloatOnDblClick = false;
      this.panelServerList.Options.ShowAutoHideButton = false;
      this.panelServerList.Options.ShowCloseButton = false;
      this.panelServerList.OriginalSize = new System.Drawing.Size(980, 200);
      this.panelServerList.Size = new System.Drawing.Size(980, 725);
      this.panelServerList.Text = "Servers";
      // 
      // controlContainer1
      // 
      this.controlContainer1.Controls.Add(this.gcServers);
      this.controlContainer1.Location = new System.Drawing.Point(4, 23);
      this.controlContainer1.Name = "controlContainer1";
      this.controlContainer1.Size = new System.Drawing.Size(972, 698);
      this.controlContainer1.TabIndex = 0;
      // 
      // btnSkin
      // 
      this.btnSkin.Location = new System.Drawing.Point(677, 2);
      this.btnSkin.Name = "btnSkin";
      this.btnSkin.Size = new System.Drawing.Size(87, 27);
      this.btnSkin.TabIndex = 22;
      this.btnSkin.Text = "Change Skin";
      this.btnSkin.Click += new System.EventHandler(this.btnSkin_Click);
      // 
      // cbAutoUpdateSelectedServer
      // 
      this.cbAutoUpdateSelectedServer.EditValue = true;
      this.cbAutoUpdateSelectedServer.Location = new System.Drawing.Point(456, 5);
      this.cbAutoUpdateSelectedServer.MenuManager = this.barManager1;
      this.cbAutoUpdateSelectedServer.Name = "cbAutoUpdateSelectedServer";
      this.cbAutoUpdateSelectedServer.Properties.Caption = "Auto-update selected server";
      this.cbAutoUpdateSelectedServer.Size = new System.Drawing.Size(194, 19);
      this.cbAutoUpdateSelectedServer.TabIndex = 21;
      // 
      // labelControl1
      // 
      this.labelControl1.Location = new System.Drawing.Point(62, 7);
      this.labelControl1.Name = "labelControl1";
      this.labelControl1.Size = new System.Drawing.Size(37, 13);
      this.labelControl1.TabIndex = 20;
      this.labelControl1.Text = "Region:";
      // 
      // btnQueryMaster
      // 
      this.btnQueryMaster.Location = new System.Drawing.Point(331, 2);
      this.btnQueryMaster.Name = "btnQueryMaster";
      this.btnQueryMaster.Size = new System.Drawing.Size(87, 27);
      this.btnQueryMaster.TabIndex = 19;
      this.btnQueryMaster.Text = "Update";
      this.btnQueryMaster.Click += new System.EventHandler(this.btnQueryMaster_Click);
      // 
      // panelTop
      // 
      this.panelTop.Controls.Add(this.panelTopFill);
      this.panelTop.Controls.Add(this.picLogo);
      this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelTop.Location = new System.Drawing.Point(0, 0);
      this.panelTop.Name = "panelTop";
      this.panelTop.Size = new System.Drawing.Size(1540, 58);
      this.panelTop.TabIndex = 25;
      // 
      // panelTopFill
      // 
      this.panelTopFill.Appearance.BackColor = System.Drawing.Color.Transparent;
      this.panelTopFill.Appearance.Options.UseBackColor = true;
      this.panelTopFill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
      this.panelTopFill.Controls.Add(this.panelControls);
      this.panelTopFill.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelTopFill.Location = new System.Drawing.Point(119, 2);
      this.panelTopFill.Name = "panelTopFill";
      this.panelTopFill.Size = new System.Drawing.Size(1419, 54);
      this.panelTopFill.TabIndex = 25;
      // 
      // panelControls
      // 
      this.panelControls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.panelControls.Appearance.BackColor = System.Drawing.Color.Transparent;
      this.panelControls.Appearance.Options.UseBackColor = true;
      this.panelControls.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
      this.panelControls.Controls.Add(this.panelOptions);
      this.panelControls.Controls.Add(this.panelGame);
      this.panelControls.Location = new System.Drawing.Point(0, 1);
      this.panelControls.Name = "panelControls";
      this.panelControls.Size = new System.Drawing.Size(1419, 58);
      this.panelControls.TabIndex = 25;
      // 
      // panelOptions
      // 
      this.panelOptions.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
      this.panelOptions.Controls.Add(this.labelControl3);
      this.panelOptions.Controls.Add(this.labelControl2);
      this.panelOptions.Controls.Add(this.labelControl1);
      this.panelOptions.Controls.Add(this.btnQueryMaster);
      this.panelOptions.Controls.Add(this.comboRegion);
      this.panelOptions.Controls.Add(this.btnSkin);
      this.panelOptions.Controls.Add(this.cbAutoUpdateSelectedServer);
      this.panelOptions.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelOptions.Location = new System.Drawing.Point(289, 0);
      this.panelOptions.Name = "panelOptions";
      this.panelOptions.Size = new System.Drawing.Size(1130, 58);
      this.panelOptions.TabIndex = 26;
      // 
      // labelControl3
      // 
      this.labelControl3.Location = new System.Drawing.Point(456, 32);
      this.labelControl3.Name = "labelControl3";
      this.labelControl3.Size = new System.Drawing.Size(375, 13);
      this.labelControl3.TabIndex = 24;
      this.labelControl3.Text = "NOTE: When you sort by a column, auto-update will reorder the selected row.";
      // 
      // labelControl2
      // 
      this.labelControl2.Location = new System.Drawing.Point(62, 32);
      this.labelControl2.Name = "labelControl2";
      this.labelControl2.Size = new System.Drawing.Size(282, 13);
      this.labelControl2.TabIndex = 23;
      this.labelControl2.Text = "HINT: Use the top row of the table to specify filter criteria.";
      // 
      // panelGame
      // 
      this.panelGame.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
      this.panelGame.Controls.Add(this.rbReflex);
      this.panelGame.Controls.Add(this.txtAppId);
      this.panelGame.Controls.Add(this.rbCustom);
      this.panelGame.Controls.Add(this.rbQuakeLive);
      this.panelGame.Controls.Add(this.rbToxikk);
      this.panelGame.Dock = System.Windows.Forms.DockStyle.Left;
      this.panelGame.Location = new System.Drawing.Point(0, 0);
      this.panelGame.Name = "panelGame";
      this.panelGame.Size = new System.Drawing.Size(289, 58);
      this.panelGame.TabIndex = 24;
      // 
      // picLogo
      // 
      this.picLogo.Cursor = System.Windows.Forms.Cursors.Hand;
      this.picLogo.Dock = System.Windows.Forms.DockStyle.Left;
      this.picLogo.Location = new System.Drawing.Point(2, 2);
      this.picLogo.MenuManager = this.barManager1;
      this.picLogo.Name = "picLogo";
      this.picLogo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
      this.picLogo.Properties.Appearance.Options.UseBackColor = true;
      this.picLogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
      this.picLogo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
      this.picLogo.Size = new System.Drawing.Size(117, 54);
      this.picLogo.TabIndex = 23;
      this.picLogo.Visible = false;
      this.picLogo.Click += new System.EventHandler(this.picLogo_Click);
      // 
      // ServerBrowserForm
      // 
      this.Appearance.Options.UseFont = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1540, 808);
      this.Controls.Add(this.panelServerList);
      this.Controls.Add(this.panelPlayers);
      this.Controls.Add(this.panelServerDetails);
      this.Controls.Add(this.panelTop);
      this.Controls.Add(this.barDockControlLeft);
      this.Controls.Add(this.barDockControlRight);
      this.Controls.Add(this.barDockControlBottom);
      this.Controls.Add(this.barDockControlTop);
      this.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.Name = "ServerBrowserForm";
      this.Text = "Steam Server Browser";
      ((System.ComponentModel.ISupportInitialize)(this.gcDetails)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboRegion.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtAppId.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbCustom.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbToxikk.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbReflex.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbQuakeLive.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.gcPlayers)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dsPlayer)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.gvPlayers)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.gcServers)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dsServers)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.gvServers)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
      this.panelServerDetails.ResumeLayout(false);
      this.dockPanel2_Container.ResumeLayout(false);
      this.panelPlayers.ResumeLayout(false);
      this.dockPanel1_Container.ResumeLayout(false);
      this.panelServerList.ResumeLayout(false);
      this.controlContainer1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.cbAutoUpdateSelectedServer.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.panelTop)).EndInit();
      this.panelTop.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.panelTopFill)).EndInit();
      this.panelTopFill.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.panelControls)).EndInit();
      this.panelControls.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.panelOptions)).EndInit();
      this.panelOptions.ResumeLayout(false);
      this.panelOptions.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.panelGame)).EndInit();
      this.panelGame.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.picLogo.Properties)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private DevExpress.XtraGrid.GridControl gcDetails;
    private DevExpress.XtraGrid.Views.Grid.GridView gvDetails;
    private DevExpress.XtraGrid.GridControl gcPlayers;
    private DevExpress.XtraGrid.Views.Grid.GridView gvPlayers;
    private DevExpress.XtraGrid.GridControl gcServers;
    private DevExpress.XtraGrid.Views.Grid.GridView gvServers;
    private CheckEdit rbQuakeLive;
    private ComboBoxEdit comboRegion;
    private TextEdit txtAppId;
    private CheckEdit rbCustom;
    private CheckEdit rbToxikk;
    private CheckEdit rbReflex;
    private DevExpress.XtraBars.BarManager barManager1;
    private DevExpress.XtraBars.Bar barStatus;
    private DevExpress.XtraBars.BarStaticItem txtStatus;
    private DevExpress.XtraBars.BarDockControl barDockControlTop;
    private DevExpress.XtraBars.BarDockControl barDockControlBottom;
    private DevExpress.XtraBars.BarDockControl barDockControlLeft;
    private DevExpress.XtraBars.BarDockControl barDockControlRight;
    private DevExpress.XtraBars.Docking.DockManager dockManager1;
    private DevExpress.XtraBars.Docking.DockPanel panelServerDetails;
    private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
    private DevExpress.XtraBars.Docking.DockPanel panelPlayers;
    private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
    private System.Windows.Forms.BindingSource dsServers;
    private DevExpress.XtraGrid.Columns.GridColumn colEndPoint;
    private DevExpress.XtraGrid.Columns.GridColumn colName;
    private DevExpress.XtraGrid.Columns.GridColumn colPing;
    private DevExpress.XtraGrid.Columns.GridColumn colPlayerCount;
    private DevExpress.XtraGrid.Columns.GridColumn colMap;
    private DevExpress.XtraGrid.Columns.GridColumn gridGameType;
    private System.Windows.Forms.BindingSource dsPlayer;
    private DevExpress.XtraGrid.Columns.GridColumn colName1;
    private DevExpress.XtraGrid.Columns.GridColumn colScore;
    private DevExpress.XtraGrid.Columns.GridColumn colTime;
    private DevExpress.XtraGrid.Columns.GridColumn colStatus;
    private DevExpress.XtraGrid.Columns.GridColumn colKey;
    private DevExpress.XtraGrid.Columns.GridColumn colValue;
    private DevExpress.XtraGrid.Columns.GridColumn colKeywords;
    private DevExpress.XtraGrid.Columns.GridColumn colPrivate;
    private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
    private CheckEdit cbAutoUpdateSelectedServer;
    private LabelControl labelControl1;
    private SimpleButton btnQueryMaster;
    private SimpleButton btnSkin;
    private PanelControl panelTop;
    private PictureEdit picLogo;
    private PanelControl panelGame;
    private PanelControl panelTopFill;
    private PanelControl panelControls;
    private PanelControl panelOptions;
    private LabelControl labelControl3;
    private LabelControl labelControl2;
    private DevExpress.XtraBars.Docking.DockPanel panelServerList;
    private DevExpress.XtraBars.Docking.ControlContainer controlContainer1;



  }
}
