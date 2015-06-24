using System.Collections.Generic;
using DevExpress.Data;
using DevExpress.XtraGrid.Views.Grid;

namespace ServerBrowser
{
  public class QuakeLive : GameExtension
  {
    private static readonly Dictionary<int,string> gameTypeName = new Dictionary<int, string>
    {
      { 0, "FFA" },{ 1, "Duel" },{ 2, "Race" },{ 3, "TDM" },{ 4, "CA" },{ 5, "CTF" },{ 6, "1Flag" },{ 8, "Harv" },{ 9, "FT" },{ 10, "Dom" },{ 11, "A&D" },{ 12, "RR" }
    };

    public override void CustomizeServerGridColumns(GridView view)
    {
      var colDescription = view.Columns["ServerInfo.Description"];
      var idx = colDescription.VisibleIndex;
      //colDescription.Visible = false;
      AddColumn(view, "_gametype", "GT", "Gametype", 40, idx);

      idx = view.Columns["ServerInfo.Ping"].VisibleIndex;
      AddColumn(view, "_goalscore", "SL", "Score Limit", 30, idx, UnboundColumnType.Integer);
      AddColumn(view, "timelimit", "TL", "Time Limit", 30, ++idx, UnboundColumnType.Integer);
      AddColumn(view, "g_instagib", "Insta", "Instagib", 35, ++idx, UnboundColumnType.Boolean);
    }

    public override object GetServerCellValue(ServerRow row, string fieldName)
    {
      switch (fieldName)
      {
        case "_goalscore":
        {
          var gt = row.GetRule("g_gametype");
          if (gt == "1" || gt == "2") //  DUEL, RACE
            return null;
          if (gt == "4" || gt == "9" || gt == "12") // CA, FT, RR
            return row.GetRule("roundlimit");
          if (gt == "5" || gt == "6") // CTF, 1FLAG
            return row.GetRule("capturelimit");
          if (gt == "8" || gt == "10" || gt == "11") // HAR, DOM, A&D
            return row.GetRule("scorelimit");
          return row.GetRule("fraglimit"); // 0=FFA, 3=TDM
        }
        case "_gametype":
        {
          var gt = row.GetRule("g_gametype");
          string instaPrefix = row.GetRule("g_instagib") == "1" ? "i" : "";
          int num;
          string name;
          if (int.TryParse(gt, out num) && gameTypeName.TryGetValue(num, out name))
            return instaPrefix + name;
          return instaPrefix + gt;
        }
        case "g_instagib": 
          return row.GetRule(fieldName) == "1";
      }
      return base.GetServerCellValue(row, fieldName);
    }
  }
}