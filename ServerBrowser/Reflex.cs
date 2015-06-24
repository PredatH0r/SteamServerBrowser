using DevExpress.XtraGrid.Views.Grid;

namespace ServerBrowser
{
  public class Reflex : GameExtension
  {
    public Reflex()
    {
      // Reflex doesn't reply to A2S_GETRULES queries and would thus show "timeout" for all servers.
      this.SupportsRulesQuery = false;
    }

    public override void CustomizeServerGridColumns(GridView view)
    {
      var colDescription = view.Columns["ServerInfo.Description"];
      var idx = colDescription.VisibleIndex;
      colDescription.Visible = false;
      AddColumn(view, "_gametype", "GT", "Gametype", 40, idx);
      AddColumn(view, "_location", "Loc", "Location", 40, ++idx);
    }

    public override object GetServerCellValue(ServerRow row, string fieldName)
    {
      if (fieldName == "_gametype")
      {
        if (row.ServerInfo == null || row.ServerInfo.Extra == null || row.ServerInfo.Extra.Keywords == null)
          return null;
        var parts = row.ServerInfo.Extra.Keywords.Split('|');
        return parts[0];
      }
      if (fieldName == "_location")
      {
        if (row.ServerInfo == null || row.ServerInfo.Extra == null || row.ServerInfo.Extra.Keywords == null)
          return null;
        var parts = row.ServerInfo.Extra.Keywords.Split('|');
        return parts.Length > 1 ? parts[1] : null;
      }

      return base.GetServerCellValue(row, fieldName);
    }
  }
}
