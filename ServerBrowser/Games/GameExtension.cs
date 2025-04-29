﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using QueryMaster;

namespace ServerBrowser
{
  #region class GameExtension

  /// <summary>
  /// Base class for game specific extensions which optimize server querying and add information to the UI
  /// </summary>
  public class GameExtension
  {
    public static string SteamWorkshopFolder = null;

    protected bool supportsRulesQuery = true;
    protected bool supportsPlayersQuery = true;
    protected bool supportsConnectAsSpectator = false;

    public Steamworks Steamworks { get; set; }

    // menu

    public string OptionMenuCaption { get; protected set; } = null;

    public virtual void OnOptionMenuClick() { }

    public virtual void LoadConfig(IniFile ini) { }
    public virtual void SaveConfig(StringBuilder ini) { }


    // server queries

    #region SupportsPlayersQuery()
    /// <summary>
    /// Returns information whether the game servers of this particular game are expected to reply to A2S_PLAYER queries.
    /// For games which never do, you can set supportsPlayersQuery=false, or override this method for more complex conditions.
    /// This helps to prevent unnecessary queries and delays when it's known that a server ignores this request.
    /// </summary>
    public virtual bool SupportsPlayersQuery(ServerRow server)
    {
      return this.supportsPlayersQuery;
    }
    #endregion

    #region SupportsRulesQuery()
    /// <summary>
    /// Returns information whether the game servers of this particular game are expected to reply to A2S_RULES queries.
    /// For games which never do, you can set supportsRulesQuery=false, or override this method for more complex conditions.
    /// This helps to prevent unnecessary queries and delays when it's known that a server ignores this request.
    /// </summary>
    public virtual bool SupportsRulesQuery(ServerRow server)
    {
      return this.supportsRulesQuery;
    }
    #endregion

    #region CustomizeServerFilter()
    /// <summary>
    /// This method is called before a server source (e.g. master server) is queried and allows to add game specific filters.
    /// In particular servers which are only accessible through lobbies should be filtered out by adding a "Nor" filter.
    /// </summary>
    public virtual void CustomizeServerFilter(IpFilter filter)
    {      
    }
    #endregion

    #region AcceptGameServer()
    /// <summary>
    /// Allows client side filtering of game servers based on their IP address. 
    /// The methods in the Ip4Utils class can be used to filter for specific subnets.
    /// </summary>
    public virtual bool AcceptGameServer(IPEndPoint server)
    {
      return !Program.serverBlacklist.Contains(server.Address.ToString());
    }
    #endregion

    public virtual void Refresh(ServerRow row = null, Action callback = null) { }

    // server list UI

    public virtual bool FilterServerRow(ServerRow row) => false;

    #region CustomizeServerGridColumns()
    /// <summary>
    /// Allows server list columns to be customized for specific games.
    /// Derived classes should use the <see cref="AddColumn"/> method to add columns for important settings.
    /// </summary>
    public virtual void CustomizeServerGridColumns(GridView view)
    {      
    }
    #endregion

    #region GetServerCellValue()
    /// <summary>
    /// Gets a value for a game specific column.
    /// Derived classes can put game specific logic here for more complex value calculations.
    /// The default implementation just looks up the given fieldName in the server's Rules collection.
    /// </summary>
    /// <param name="row">ServerRow object with all the data known about the server</param>
    /// <param name="fieldName">FieldName of the GridColumn that was added in <see cref="CustomizeServerGridColumns"/></param>
    public virtual object GetServerCellValue(ServerRow row, string fieldName)
    {
      return row.GetRule(fieldName);
    }
    #endregion

    #region GetRealPlayerCount()
    public virtual int? GetRealPlayerCount(ServerRow row)
    {
      return null;
    }
    #endregion

    #region GetSpectatorCount()
    /// <summary>
    /// returns number of spectators among the RealPlayerCount
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    public virtual int GetSpectatorCount(ServerRow row)
    {
      return 0;
    }
    #endregion

    #region GetBotCount()
    public virtual int? GetBotCount(ServerRow row)
    {
      return row.ServerInfo?.Bots;
    }
    #endregion

    // player list UI

    #region CustomizePlayerGridColumns()
    /// <summary>
    /// Allows player list columns to be customized for specific games.
    /// Derived classes can use the <see cref="AddColumn"/> method to add columns with typical settings.
    /// </summary>
    public virtual void CustomizePlayerGridColumns(GridView view)
    {
    }
    #endregion

    #region GetCleanPlayerName()
    /// <summary>
    /// Remove color codes or other game specific parts of the player name which may disturb display or search
    /// </summary>
    public virtual string GetCleanPlayerName(Player player)
    {
      return player?.Name;
    }
    #endregion

    #region GetPlayerCellValue()
    /// <summary>
    /// Gets a value for a game specific column.
    /// Derived classes can put game specific logic here for more complex value calculations.
    /// The default implementation just looks up the given fieldName in the row's Rules collection.
    /// </summary>
    /// <param name="server">ServerRow object with all the data known about the server</param>
    /// <param name="player">Player object with all the data known about the player</param>
    /// <param name="fieldName">FieldName of the GridColumn that was added with CustomizePlayerGridColumns</param>
    public virtual object GetPlayerCellValue(ServerRow server, Player player, string fieldName)
    {
      return null;
    }
    #endregion

    #region CustomizePlayerContextMenu()
    /// <summary>
    /// Allows to modify the context menu of the Players list.
    /// </summary>
    public virtual void CustomizePlayerContextMenu(ServerRow server, Player player, List<PlayerContextMenuItem> menu)
    {
    }
    #endregion

    // connecting

    #region SupportsConnectAsSpectator()
    /// <summary>
    /// Controls whether the UI offers an option to connect as a spectator.
    /// For games which never do, you can set supportsConnectAsSpectator=false, or override this method for more complex conditions.
    /// </summary>
    public virtual bool SupportsConnectAsSpectator(ServerRow server)
    {
      return this.supportsConnectAsSpectator;
    }
    #endregion

    #region Connect()
    /// <summary>
    /// Start the game if necessary and then connect to the given game server.
    /// </summary>
    public virtual bool Connect(ServerRow server, string password, bool spectate)
    {
      this.ActivateGameWindow(this.FindGameWindow());
      var proc = Process.Start("steam://connect/" + server.EndPoint + (string.IsNullOrEmpty(password) ? "" : "/" + password));
      return proc != null;
    }
    #endregion

    protected virtual IntPtr FindGameWindow() => IntPtr.Zero;

    #region ActivateGameWindow()
    protected void ActivateGameWindow(IntPtr hWnd)
    {
      if (hWnd != IntPtr.Zero)
      {
        Win32.ShowWindow(hWnd, Win32.SW_RESTORE);
        Win32.SetForegroundWindow(hWnd);
      }
    }
    #endregion

    // helper methods

    #region AddColumn()
    /// <summary>
    /// Utility method for derived classes to add game specific columns to the server or player list
    /// </summary>
    public GridColumn AddColumn(GridView view, string fieldName, string caption, string toolTip, int width = 70, int visibleIndex = -1, UnboundColumnType type = UnboundColumnType.String)
    {
      var col = new GridColumn();
      col.FieldName = fieldName;
      col.Caption = caption;
      col.Tag = fieldName;
      col.Width = width;
      col.ToolTip = toolTip;
      col.UnboundType = type;
      col.OptionsColumn.ReadOnly = true;
      col.OptionsColumn.AllowEdit = false;
      if (type == UnboundColumnType.Decimal || type == UnboundColumnType.Integer)
      {
        col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
        col.AppearanceCell.Options.UseTextOptions = true;
      }
      else if (type == UnboundColumnType.String)
        col.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;

      view.Columns.Add(col);
      col.VisibleIndex = visibleIndex >= 0 ? visibleIndex : view.Columns.Count;
      return col;
    }
    #endregion

    public virtual void Rcon(ServerRow row, int port, string password, string command)
    {      
    }

    public bool BotsIncludedInPlayerCount { get; set; }

    public bool BotsIncludedInPlayerList { get; set; }

    public virtual int? GetMaxPlayers(ServerRow row) => row?.ServerInfo?.MaxPlayers;

    public virtual int? GetMaxClients(ServerRow row) => row?.ServerInfo?.MaxPlayers;

    public virtual int? GetPrivateClients(ServerRow row) => 0;

    public virtual bool IsValidPlayer(ServerRow row, Player player) => true;

    public virtual string GetServerCellToolTip(ServerRow row, string fieldName) => null;

    public virtual string GetPrettyNameForRule(ServerRow row, string ruleName) => ruleName;
  }
  #endregion

  #region class PlayerContextMenuItem
  public class PlayerContextMenuItem
  {
    public readonly string Text;
    public readonly Action Handler;
    public readonly bool IsDefaultAction;

    public PlayerContextMenuItem(string text, Action handler, bool isDefaultAction = false)
    {
      this.Text = text;
      this.Handler = handler;
      this.IsDefaultAction = isDefaultAction;
    }
  }
  #endregion

  #region class GameExtensionPool
  public class GameExtensionPool : IEnumerable<KeyValuePair<Game, GameExtension>>
  {
    private readonly Dictionary<Game, GameExtension> extensions = new Dictionary<Game, GameExtension>();
    public Steamworks Steamworks { get; set; }

    public void Add(Game game, GameExtension extension)
    {
      extensions[game] = extension;
      extension.Steamworks = this.Steamworks;
    }

    public GameExtension Get(Game game)
    {
      GameExtension extension;
      if (!this.extensions.TryGetValue(game, out extension))
      {
        extension = new GameExtension();
        this.Add(game, extension);
      }
      return extension;
    }

    IEnumerator<KeyValuePair<Game, GameExtension>> IEnumerable<KeyValuePair<Game, GameExtension>>.GetEnumerator()
    {
      return extensions.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return ((IEnumerable) extensions).GetEnumerator();
    }
  }
  #endregion
}
