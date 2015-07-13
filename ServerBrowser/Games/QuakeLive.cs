using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraGrid.Views.Grid;

namespace ServerBrowser
{
  public class QuakeLive : GameExtension
  {
    private const int SecondsToWaitForMainWindowAfterLaunch = 20;

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

    #region GetServerCellValue()
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
    #endregion

    #region Connect()

    public override bool Connect(ServerRow server, string password, bool spectate)
    {
      ThreadPool.QueueUserWorkItem(context => ConnectInBackground(server, password, spectate), null);
      return true;
    }
    #endregion

    #region ConnectInBackground()
    private bool ConnectInBackground(ServerRow server, string password, bool spectate)
    {
      var win = FindQuakeWindow();
      if (win == IntPtr.Zero)
      {
        win = StartQuakeLive();
        if (win == IntPtr.Zero)
          return false;
        SkipIntro(win);
      }

      // console key
      Win32.PostMessage(win, Win32.WM_KEYDOWN, 0, 0x29 << 16);
      Win32.PostMessage(win, Win32.WM_KEYUP, 0, 0x29 << 16);

      Thread.Sleep(100);

      var msg = "/";
      if (!string.IsNullOrEmpty(password))
        msg += "set password \"" + password + "\";";
      msg += "connect " + server.EndPoint.Address + ":" + server.ServerInfo.Extra.Port;
      foreach (var c in msg)
      {
        Win32.PostMessage(win, Win32.WM_CHAR, c, 0);
        Thread.Sleep(10);
      }

      Win32.PostMessage(win, Win32.WM_KEYDOWN, (int)Keys.Return, 0x1C << 16);
      Win32.PostMessage(win, Win32.WM_KEYUP, (int)Keys.Return, 0x1C << 16);

      return true;
    }
    #endregion

    #region FindQuakeWindow()
    private static IntPtr FindQuakeWindow()
    {
      foreach (Process proc in Process.GetProcessesByName("quakelive_steam"))
      {
        var hWnd = proc.MainWindowHandle;
        return hWnd;
      }
      return IntPtr.Zero;
    }
    #endregion

    #region StartQuakeLive()
    private static IntPtr StartQuakeLive()
    {
      Process.Start("steam://rungameid/344320");
      for (int i = 0; i < SecondsToWaitForMainWindowAfterLaunch; i++)
      {
        Thread.Sleep(1000);
        var hWnd = FindQuakeWindow();
        if (hWnd != IntPtr.Zero)
          return hWnd;
      }
      return IntPtr.Zero;
    }


    #endregion

    #region SkipIntro()
    private void SkipIntro(IntPtr win)
    {
      for (int i = 0; i < 3; i++)
      {
        Thread.Sleep(500);
        Win32.PostMessage(win, Win32.WM_LBUTTONDOWN, 1, 0);
        Win32.PostMessage(win, Win32.WM_LBUTTONUP, 0, 0);
      }
    }
    #endregion

  }
}