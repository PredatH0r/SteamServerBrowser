using DevExpress.Data;
using DevExpress.XtraGrid.Views.Grid;

namespace ServerBrowser
{
  public class Toxikk : GameExtension
  {
    public override void CustomizeGridColumns(GridView view)
    {
      var colDescription = view.Columns["ServerInfo.Description"];
      var idx = colDescription.VisibleIndex;
      colDescription.Visible = false;
      AddColumn(view, "_gametype", "GT", "Gametype", 40, idx);

      view.Columns["ServerInfo.Extra.Keywords"].Visible = false;

      idx = view.Columns["ServerInfo.Ping"].VisibleIndex;
      AddColumn(view, "p268435708", "Skill", "Skill Limit", 35, idx, UnboundColumnType.Integer);
      AddColumn(view, "p268435704", "SL", "Score Limit", 30, ++idx, UnboundColumnType.Integer);
      AddColumn(view, "p268435705", "TL", "Time Limit", 30, ++idx, UnboundColumnType.Integer);
      AddColumn(view, "s15", "Ofcl", "Official", 35, ++idx, UnboundColumnType.Boolean);
    }

    public override object GetCellValue(ServerRow row, string fieldName)
    {
      switch (fieldName)
      {
        case "p268435708":
          return row.Rules == null ? null : row.GetRule("p268435708") + "-" + row.GetRule("p268435709");
        case "s15":
          return row.GetRule(fieldName) == "1";
        case "_gametype":
          var gt = row.ServerInfo.Description;
          return gt.Contains("BloodLust") ? "BL" : gt.Contains("TeamGame") ? "SA" : gt.Contains("Cell") ? "CC" : gt;
      }
      return base.GetCellValue(row, fieldName);
    }
  }
}