using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
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

    #region Steam AppIds
    public static class AppIds
    {
      public const int Reflex = 328070;
      public const int Toxikk = 324810;
      public const int QlTesting = 344320;
    }
    #endregion

    private string brandingUrl;
    private readonly List<ServerRow> servers = new List<ServerRow>();
    private ServerRow lastSelectedServer;
    private volatile bool shutdown;
    private volatile int steamAppId;
    private readonly Dictionary<int, GameExtension> extenders = new Dictionary<int, GameExtension>();
    private GameExtension currentExtension;
    private int ignoreUiEvents;

    #region ctor()
    public ServerBrowserForm()
    {
      // change font before InitializeComponent() to get correct auto-scaling
      if (Properties.Settings.Default.FontSizeDelta != 0)
      {
        AppearanceObject.DefaultFont = new Font(
          Properties.Settings.Default.FontName,
          AppearanceObject.DefaultFont.Size + Properties.Settings.Default.FontSizeDelta);
      }

      InitializeComponent();

      // ReSharper disable AssignNullToNotNullAttribute
      this.Icon = new Icon(this.GetType().Assembly.GetManifestResourceStream("ServerBrowser.rocket.ico"));
      // ReSharper restore AssignNullToNotNullAttribute

      this.gcServers.DataSource = servers;
      this.panelServerList.Parent.Controls.Remove(this.panelServerList);
      this.panelServerList.Dock = DockingStyle.Fill;
      this.Controls.Add(this.panelServerList);
    }
    #endregion

    #region OnLoad()
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      ++this.ignoreUiEvents;

      this.InitGameInfoExtenders();
      this.InitBranding();

      if (this.btnSkin.Visible)
        this.LoadBonusSkins();

      FillSteamServerRegions();

      --this.ignoreUiEvents;
      this.UpdateServerList();
    }
    #endregion

    #region OnClosed()
    protected override void OnClosed(EventArgs e)
    {
      this.shutdown = true;
      base.OnClosed(e);
    }
    #endregion

    #region InitGameInfoExtenders()

    private void InitGameInfoExtenders()
    {
      extenders.Add(AppIds.Toxikk, new Toxikk());
      extenders.Add(AppIds.Reflex, new Reflex());
      extenders.Add(AppIds.QlTesting, new QuakeLive());
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
        this.picLogo.Width = img.Width*topHeight/img.Height;
        this.picLogo.Visible = true;
        
        this.panelGame.Visible = false;
        this.rbReflex.Checked = true;

        UserLookAndFeel.Default.SkinName = "Visual Studio 2013 Dark";
        this.btnSkin.Visible = false;
        return;
      }

      UserLookAndFeel.Default.SkinName = Properties.Settings.Default.Skin;
      this.rbToxikk.Checked = true;
    }
    #endregion
    
    #region LoadBonusSkins()

    /// <summary>
    /// Load the BonusSkin DLL dynamically so that the application can be executed without it being present
    /// </summary>
    private void LoadBonusSkins()
    {
      try
      {
        var dir = Path.GetDirectoryName(Application.ExecutablePath) ?? ".\\";
        var files = Directory.GetFiles(dir, "DevExpress.*BonusSkin*.dll");
        if (files.Length == 0)
          return;
        var ass = Assembly.LoadFrom(files[0]);
        var type = ass.GetType("DevExpress.UserSkins.BonusSkins");
        if (type == null)
          return;
        var method = type.GetMethod("Register", BindingFlags.Static | BindingFlags.Public);
        if (method != null)
          method.Invoke(null, null);
      }
      catch
      {
        // it's a pity, but life goes on
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

    #region SteamAppId
    private int SteamAppID
    {
      get { return this.steamAppId; }
      set
      {
        if (value == this.steamAppId)
          return;
        this.steamAppId = value;
        this.CreatedColumnsForGameExtender();
        this.panelRules.Visibility = this.currentExtension.SupportsRules ? DockVisibility.Visible : DockVisibility.Hidden;
      }
    }
    #endregion

    #region CreatedColumnsForGameExtender()
    private void CreatedColumnsForGameExtender()
    {
      this.gvServers.BeginUpdate();
      var cols = new List<GridColumn>(this.gvServers.Columns);
      foreach (var col in cols)
      {
        if (col.Tag != null)
          this.gvServers.Columns.Remove(col);
        else
          col.Visible = true;
      }

      if (!extenders.TryGetValue(this.SteamAppID, out this.currentExtension))
        this.currentExtension = new GameExtension();
      this.currentExtension.CustomizeGridColumns(gvServers);
      this.gvServers.EndUpdate();
    }
    #endregion

    #region UpdateServerList()
    private void UpdateServerList()
    {
      if (this.ignoreUiEvents > 0)
        return;

      this.txtStatus.Caption = "Requesting server list from master server...";
      this.gvServers.BeginDataUpdate();
      servers.Clear();
      this.gvServers.EndDataUpdate();

      MasterServer master = MasterQuery.GetMasterServerInstance(EngineType.Source);
      IpFilter filter = new IpFilter();
      int appId;
      int.TryParse(this.txtAppId.Text, out appId);
      this.SteamAppID = appId;
      filter.App = (Game) appId;
      var region = (QueryMaster.Region)steamRegions[this.comboRegion.SelectedIndex * 2 + 1];
      master.GetAddresses(region, endpoints => OnMasterServerReceive(endpoints, appId), filter);
    }
    #endregion

    #region OnMasterServerReceive()
    private void OnMasterServerReceive(ReadOnlyCollection<IPEndPoint> endPoints, int queryAppId)
    {
      // ignore results from older queries with a different appId
      if (queryAppId != this.SteamAppID)
        return;

      string statusText;
      if (endPoints == null)
        statusText = "Master server request timed out";
      else
      {
        statusText = "Requesting next batch of server list...";
        foreach (var ep in endPoints)
        {
          if (this.shutdown)
            return;
          if (ep.Address.Equals(IPAddress.Any))
          {
            statusText = "Master server returned " + this.servers.Count + " servers";
            AllServersReceived();
          }
          else if (servers.Count >= 1000)
          {
            statusText = "Server list limited to 1000 entries";
            AllServersReceived();
            break;
          }
          else
            servers.Add(new ServerRow(ep));
        }
      }

      this.BeginInvoke((Action) (() =>
      {
        if (statusText != null)
          this.txtStatus.Caption = statusText;
        this.gvServers.BeginDataUpdate();
        this.gvServers.EndDataUpdate();
      }));
    }
    #endregion

    #region AllServersReceived()
    private void AllServersReceived()
    {
      foreach (var row in this.servers)
      {
        var safeRow = row;
        ThreadPool.QueueUserWorkItem(context => UpdateServerDetails(safeRow));
      }
    }
    #endregion

    #region UpdateServerDetails()
    private void UpdateServerDetails(ServerRow row, Action callback = null)
    {     
      Server server = ServerQuery.GetServerInstance(EngineType.Source, row.EndPoint, false, 500, 500);
      string status = UpdateServerInfo(row, server) && UpdatePlayers(row, server) && UpdateRules(row, server) ? "ok" : "timeout";
      if (row.Retries > 0)
        status += " (" + row.Retries + ")";
      row.Status = status;
      row.Update();

      if (this.shutdown)
        return;
      this.BeginInvoke((Action)(() =>
      {
        this.gvServers.BeginDataUpdate();
        this.gvServers.EndDataUpdate();
        if (callback != null)
          callback();
      }));
    }
    #endregion

    #region UpdateServerInfo()
    private bool UpdateServerInfo(ServerRow row, Server server)
    {
      for (row.Retries = 0; row.Retries < 3; row.Retries++)
      {
        try
        {
          row.Status = "try " + (row.Retries + 1);
          row.ServerInfo = server.GetInfo();
          return true;
        }
        catch { }
      }
      return false;
    }
    #endregion

    #region UpdatePlayers()
    private bool UpdatePlayers(ServerRow row, Server server)
    {
      for (int attempt=0; attempt < 3; attempt++, row.Retries++)
      {
        try
        {
          row.Status = "try " + (row.Retries + 1);
          row.Players = new List<Player>(server.GetPlayers());
          return true;
        }
        catch { }
      }
      return false;
    }
    #endregion

    #region UpdateRules()
    private bool UpdateRules(ServerRow row, Server server)
    {
      if (!this.currentExtension.SupportsRules)
        return true;

      for (int attempt = 0; attempt < 3; attempt++, row.Retries++)
      {
        try
        {
          row.Status = "try " + (row.Retries + 1);
          row.Rules = new List<Rule>(server.GetRules());
          return true;
        }
        catch { }
      }
      return false;
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
          if (prop.Name != "Extra" && prop.Name != "Item")
            result.Add(new Tuple<string, object>(prop.Name.ToLower(), prop.GetValue(obj, null)));
        }
      }
      return result;
    }
    #endregion

    #region UpdateGridDataSources()
    private void UpdateGridDataSources(ServerRow row)
    {
      this.gcDetails.DataSource = EnumerateProps(row.ServerInfo, row.ServerInfo == null ? null : row.ServerInfo.Extra);
      this.gcPlayers.DataSource = row.Players;
      this.gcRules.DataSource = row.Rules;
    }
    #endregion


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

    #region rbGame_CheckedChanged
    private void rbGame_CheckedChanged(object sender, EventArgs e)
    {
      this.txtAppId.Text = 
        this.rbQuakeLive.Checked ? AppIds.QlTesting.ToString() : 
        this.rbReflex.Checked ? AppIds.Reflex.ToString() : 
        this.rbToxikk.Checked ? AppIds.Toxikk.ToString() : 
        "";
      this.UpdateServerList();
    }
    #endregion

    #region comboRegion_SelectedIndexChanged
    private void comboRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.UpdateServerList();
    }
    #endregion

    #region btnQueryMaster_Click
    private void btnQueryMaster_Click(object sender, EventArgs e)
    {
      UpdateServerList();
    }
    #endregion

    #region gvServers_CustomUnboundColumnData
    private void gvServers_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
    {
      if (e.IsGetData)
        e.Value = ((ServerRow)e.Row).GetExtenderCellValue(this.currentExtension, e.Column.FieldName);
    }
    #endregion

    #region gvServers_FocusedRowChanged
    private void gvServers_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
    {
      try
      {
        var row = (ServerRow)this.gvServers.GetRow(this.gvServers.FocusedRowHandle);
        if (row == null || row == this.lastSelectedServer) // prevent consecutive updates due to row-reordering
          return;

        this.lastSelectedServer = row;
        this.UpdateGridDataSources(row);

        if (!this.cbAutoUpdateSelectedServer.Checked)
          return;

        Application.DoEvents();
        row.Status = "updating...";
        ThreadPool.QueueUserWorkItem(dummy =>
          this.UpdateServerDetails(row, () =>
          {
            if (this.gvServers.GetRow(this.gvServers.FocusedRowHandle) == row)
              this.UpdateGridDataSources(row);
          }));
      }
      catch (Exception ex)
      {
        this.txtStatus.Caption = ex.Message;
      }
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
        Process.Start("steam://connect/" + row.EndPoint);

      }
      catch (Exception ex)
      {
        this.txtStatus.Caption = ex.Message;
      }
    }
    #endregion

    #region gvServers_CustomColumnSort
    private void gvServers_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
    {
      //if (e.Column == this.colPlayerCount)
      //{
      //  var a = this.servers[e.ListSourceRowIndex1].PlayerCount.Replace(" ", "");
      //  var b = this.servers[e.ListSourceRowIndex1].PlayerCount.Replace(" ", "");
      //  var aa = a.Split('+', '/');
      //  var bb = b.Split('+', '/');
      //  for (int i = 0; i < aa.Length; i++)
      //  {
      //    e.Result = StringComparer.InvariantCulture.Compare(aa[i], bb[i]);
      //    if (e.Result != 0)
      //      return;
      //  }
      //}
    }
    #endregion

    #region btnSkin_Click
    private void btnSkin_Click(object sender, EventArgs e)
    {
      using (var dlg = new SkinPicker())
        dlg.ShowDialog(this);
    }
    #endregion

    #region dockManager1_StartDocking
    private void dockManager1_StartDocking(object sender, DockPanelCancelEventArgs e)
    {
      if (e.Panel == this.panelServerList)
        e.Cancel = true;
    }
    #endregion

  }
}