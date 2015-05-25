using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
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

    private string brandingUrl;
    private List<ServerRow> servers;
    private ServerRow lastSelectedServer;
    private volatile bool shutdown;
    private volatile Game steamAppId;
    private readonly Dictionary<Game, GameExtension> extenders = new Dictionary<Game, GameExtension>();
    private GameExtension currentExtension;
    private int ignoreUiEvents;
    private readonly CheckEdit[] favGameRadioButtons;
    private readonly List<Game> gameIdForComboBoxIndex = new List<Game>();
    private volatile bool serverListUpdateNeeded;
    private const int maxResults = 500;
    private volatile int currentRequestId; // logical clock to drop obsolete replies, e.g. when the user selected a different game in the meantime

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
      if (this.btnSkin.Visible)
        this.LoadBonusSkins();
      this.InitAppSettings();

      --this.ignoreUiEvents;
      this.UpdateServerList();
    }
    #endregion

    #region OnClosed()
    protected override void OnClosed(EventArgs e)
    {
      Properties.Settings.Default.InitialGameID = (int) this.SteamAppID;
      Properties.Settings.Default.Save();

      this.shutdown = true;
      base.OnClosed(e);
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

    #region InitGameInfoExtenders()

    private void InitGameInfoExtenders()
    {
      extenders.Add(Game.Toxikk, new Toxikk());
      extenders.Add(Game.Reflex, new Reflex());
      extenders.Add(Game.QuakeLiveTesting, new QuakeLive());
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

        this.panelGame.Visible = false;
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

    #region InitAppSettings()
    private void InitAppSettings()
    {
      InitFavGameRadioButtons();
      this.SteamAppID = (Game)Properties.Settings.Default.InitialGameID;
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
      if (prevRadio != null && this.panelGame.Width < prevRadio.Right)
        this.panelGame.Width = prevRadio.Right;
    }

    #endregion

    #region GetGameCaption()
    private string GetGameCaption(Game game)
    {
      string rawName = game.ToString();
      var sb = new StringBuilder();
      bool prevWasUpper = true;
      foreach (char c in rawName)
      {
        if (c == '_')
        {
          sb.Append(" ");
          prevWasUpper = true;
          continue;
        }

        if (Char.IsUpper(c))
        {
          if (!prevWasUpper)
            sb.Append(" ");
          prevWasUpper = true;
        }
        else
          prevWasUpper = false;

        sb.Append(c);
      }
      return sb.ToString();
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
        this.steamAppId = value;
        this.CreatedColumnsForGameExtender();
        
        // show/hide the Rules panel but don't bring it to the front
        var parentPanel = this.panelRules.SavedParent;
        var curTopmost = parentPanel == null ? -1 : parentPanel.ActiveChildIndex;
        this.panelRules.Visibility = this.currentExtension.SupportsRules ? DockVisibility.Visible : DockVisibility.Hidden;
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
      if (this.SteamAppID == 0) // this would result in a truncated list of all games
        return;

      this.txtStatus.Caption = "Requesting server list from master server...";
      this.gvServers.BeginDataUpdate();
      var rows = new List<ServerRow>(); // local reference to guarantee thread safety
      this.servers = rows;
      this.gcServers.DataSource = this.servers;
      this.gvServers.EndDataUpdate();

      MasterServer master = MasterQuery.GetMasterServerInstance(EngineType.Source);
      master.GetAddressesLimit = maxResults;
      IpFilter filter = new IpFilter();
      filter.App = this.SteamAppID;
      var region = (QueryMaster.Region)steamRegions[this.comboRegion.SelectedIndex * 2 + 1];

      var requestId = ++this.currentRequestId;
      master.GetAddresses(region, endpoints => OnMasterServerReceive(endpoints, requestId, rows), filter);
    }
    #endregion

    #region OnMasterServerReceive()
    private void OnMasterServerReceive(ReadOnlyCollection<IPEndPoint> endPoints, int requestId, List<ServerRow> rows)
    {
      // ignore results from older queries
      if (requestId != this.currentRequestId)
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
            this.AllServersReceived(requestId, rows);
          }
          else if (servers.Count >= maxResults)
          {
            statusText = "Server list limited to " + maxResults + " entries";
            this.AllServersReceived(requestId, rows);
            break;
          }
          else
            rows.Add(new ServerRow(ep));
        }
      }

      this.serverListUpdateNeeded = true;
      this.BeginInvoke((Action) (() =>
      {
        this.txtStatus.Caption = statusText;
      }));
    }
    #endregion

    #region AllServersReceived()
    private void AllServersReceived(int requestId, List<ServerRow> rows)
    {
      // use a background thread so that the caller doesn't have to wait for the accumulated Therad.Sleep()
      ThreadPool.QueueUserWorkItem(dummy =>
      {
        foreach (var row in rows)
        {
          if (requestId != this.currentRequestId)
            return;
          var safeRow = row;
          ThreadPool.QueueUserWorkItem(context => UpdateServerDetails(safeRow, requestId));
          Thread.Sleep(5); // launching all threads at once results in totally wrong ping values
        }
      });
    }
    #endregion

    #region UpdateServerDetails()
    private void UpdateServerDetails(ServerRow row, int requestId, Action callback = null)
    {
      if (requestId != this.currentRequestId) // drop obsolete requests
        return;

      string status;
      using (Server server = ServerQuery.GetServerInstance(EngineType.Source, row.EndPoint, false, 500, 500))
      {
        server.Retries = 3;
        status =
          UpdateServerInfo(row, server, requestId) &&
          UpdatePlayers(row, server, requestId) &&
          UpdateRules(row, server, requestId)
            ? "ok"
            : "timeout";
      }

      if (requestId != this.currentRequestId) // status might have changed
        return;

      if (row.Retries > 0)
        status += " (" + row.Retries + ")";
      row.Status = status;
      row.Update();

      if (this.shutdown)
        return;
      this.serverListUpdateNeeded = true;
      if (callback != null)
        this.BeginInvoke(callback);
    }
    #endregion

    #region UpdateServerInfo()
    private bool UpdateServerInfo(ServerRow row, Server server, int requestId)
    {
      return UpdateDetail(row, server, requestId, retryHandler =>
      {
        row.ServerInfo = server.GetInfo(retryHandler);
      });
    }
    #endregion

    #region UpdatePlayers()
    private bool UpdatePlayers(ServerRow row, Server server, int requestId)
    {
      return UpdateDetail(row, server, requestId, retryHandler =>
      {
        var players = server.GetPlayers(retryHandler);
        row.Players = players == null ? null : new List<Player>(players);
      });
    }
    #endregion

    #region UpdateRules()
    private bool UpdateRules(ServerRow row, Server server, int requestId)
    {
      if (currentExtension != null && !currentExtension.SupportsRules)
        return true;
      return UpdateDetail(row, server, requestId, retryHandler =>
      {
        row.Rules = new List<Rule>(server.GetRules(retryHandler));
      });
    }
    #endregion

    #region UpdateDetail()
    private bool UpdateDetail(ServerRow row, Server server, int requestId, Action<Action<int>> updater)
    {
      if (requestId != this.currentRequestId)
        return false;

      try
      {
        row.Status = "try " + (row.Retries + 1);
        updater(retry =>
        {
          if (requestId != currentRequestId)
            throw new OperationCanceledException();
          row.Status = "try " + (++row.Retries + 1);
        });
        return true;
      }
      catch
      {
        return false;
      }
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

    #region comboGames_SelectedIndexChanged
    private void comboGames_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.ignoreUiEvents > 0) return;
      var idx = this.comboGames.SelectedIndex;
      if (idx < 0)
        return;
      this.SteamAppID = this.gameIdForComboBoxIndex[idx];
      this.UpdateServerList();
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
        Properties.Settings.Default.Save();
        this.InitFavGameRadioButtons();
      }
      else
      {
        // update server list for selected favorite
        this.SteamAppID = (Game)radio.Tag;
        this.UpdateServerList();
      }
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
      if (this.comboGames.SelectedIndex < 0)
      {
        int id;
        if (!int.TryParse(this.comboGames.Text, out id))
          return;
        this.SteamAppID = (Game) id;
      }

      UpdateServerList();
    }
    #endregion

    #region btnSkin_Click
    private void btnSkin_Click(object sender, EventArgs e)
    {
      using (var dlg = new SkinPicker())
        dlg.ShowDialog(this);
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
          this.UpdateServerDetails(row, this.currentRequestId, () =>
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

    #region dockManager1_StartDocking
    private void dockManager1_StartDocking(object sender, DockPanelCancelEventArgs e)
    {
      if (e.Panel == this.panelServerList)
        e.Cancel = true;
    }
    #endregion

    #region timerUpdateServerList_Tick
    private void timerUpdateServerList_Tick(object sender, EventArgs e)
    {
      if (!this.serverListUpdateNeeded)
        return;
      this.serverListUpdateNeeded = false;
      this.gvServers.BeginDataUpdate();
      this.gvServers.EndDataUpdate();
    }
    #endregion
  }
}