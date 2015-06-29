using System;
using System.Collections.Generic;
using System.Diagnostics;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using QueryMaster;

namespace ServerBrowser
{
  /// <summary>
  /// Base class for game specific extensions which add information to the Server grid
  /// </summary>
  public class GameExtension
  {
    public GameExtension()
    {
      this.SupportsRulesQuery = true;
      this.SupportsConnectAsSpectator = false;
    }

    /// <summary>
    /// Returns information whether the game servers of this particular game are expected to reply to AS2_GETRULES queries.
    /// For games which don't, this should be overridden to return false, otherwise all servers will have a "timeout" status.
    /// </summary>
    public bool SupportsRulesQuery { get; protected set; }
    public bool SupportsConnectAsSpectator { get; protected set; }

    /// <summary>
    /// Allows server list columns to be customized for specific games.
    /// Derived classes can use the <see cref="AddColumn"/> method to add columns with typical settings.
    /// </summary>
    public virtual void CustomizeServerGridColumns(GridView view)
    {      
    }

    /// <summary>
    /// Gets a value for a game specific column.
    /// Derived classes can put game specific logic here for more complex value calculations.
    /// The default implementation just looks up the given fieldName in the row's Rules collection.
    /// </summary>
    /// <param name="row">ServerRow object with all the data known about the server</param>
    /// <param name="fieldName">FieldName of the GridColumn that was added with CustomizeGridColumns</param>
    public virtual object GetServerCellValue(ServerRow row, string fieldName)
    {
      return row.GetRule(fieldName);
    }

    public virtual bool Connect(ServerRow server, string password, bool spectate)
    {
      var proc = Process.Start("steam://connect/" + server.EndPoint + (string.IsNullOrEmpty(password) ? "" : "/" + password));
      return proc != null;
    }

    /// <summary>
    /// Utility method for derived classes to add game specific GridColumns to the server list
    /// </summary>
    protected GridColumn AddColumn(GridView view, string fieldName, string caption, string toolTip, int width=70, int visibleIndex=-1, UnboundColumnType type=UnboundColumnType.String)
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
      view.Columns.Add(col);
      col.VisibleIndex = visibleIndex >= 0 ? visibleIndex : view.Columns.Count;
      return col;
    }


    /// <summary>
    /// Allows player list columns to be customized for specific games.
    /// Derived classes can use the <see cref="AddColumn"/> method to add columns with typical settings.
    /// </summary>
    public virtual void CustomizePlayerGridColumns(GridView view)
    {
    }

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

    public virtual void CustomizePlayerContextMenu(ServerRow server, Player player, List<PlayerContextMenuItem> menu)
    {
    }
  }

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
}
