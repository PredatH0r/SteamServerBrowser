using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraGrid.Views.Grid;
using QueryMaster;

namespace ServerBrowser
{
  public class Toxikk : GameExtension
  {
    private const int SecondsToWaitForMainWindowAfterLaunch = 45;
    private Keys consoleKey;

    public Toxikk()
    {
      consoleKey = (Keys)Properties.Settings.Default.ToxikkConsoleKey;
      this.SupportsRulesQuery = true;
      this.SupportsConnectAsSpectator = true;
    }

    #region CustomizeGridColumns()
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
    #endregion

    #region GetCellValue()
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
          return gt == null ? null : gt.Contains("BloodLust") ? "BL" : gt.Contains("TeamGame") ? "SA" : gt.Contains("Cell") ? "CC" : gt;
      }
      return base.GetCellValue(row, fieldName);
    }
    #endregion

    #region Connect()

    public override bool Connect(ServerRow server, string password, bool spectate)
    {
      if (consoleKey == Keys.None)
      {
        using (var dlg = new KeyBindForm("Please press your Toxikk console key..."))
        {
          if (dlg.ShowDialog(Application.OpenForms[0]) == DialogResult.Cancel)
            return false;
          consoleKey = dlg.Key;
          Properties.Settings.Default.ToxikkConsoleKey = (int) consoleKey;
          Properties.Settings.Default.Save();
        }
      }

      ThreadPool.QueueUserWorkItem(context => ConnectInBackground(server, password, spectate), null);
      return true;
    }
    #endregion

    #region ConnectInBackground()
    private bool ConnectInBackground(ServerRow server, string password, bool spectate)
    {
      var win = FindToxikkWindow();
      if (win == IntPtr.Zero)
      {
        win = StartToxikk();
        if (win == IntPtr.Zero)
          return false;
        SkipIntro(win);
      }

      Win32.PostMessage(win, Win32.WM_KEYDOWN, (int)consoleKey, 0);
      Win32.PostMessage(win, Win32.WM_KEYUP, (int)consoleKey, 0);

      // hack: prevent WM_DEADCHAR quirks that might be a side-effect of the console key
      Win32.PostMessage(win, Win32.WM_CHAR, ' ', 0);
      for (int i = 0; i < 3; i++)
      {
        Win32.PostMessage(win, Win32.WM_KEYDOWN, (int)Keys.Back, 0);
        Win32.PostMessage(win, Win32.WM_CHAR, 8, 0);
        Win32.PostMessage(win, Win32.WM_KEYUP, (int)Keys.Back, 0);
      }

      var msg = "open " + server.EndPoint.Address + ":" + server.ServerInfo.Extra.Port;
      if (!string.IsNullOrEmpty(password))
        msg += "?password=" + password;
      if (spectate)
        msg += "?spectatoronly=1";
      foreach (var c in msg)
        Win32.PostMessage(win, Win32.WM_CHAR, c, 0);

      Win32.PostMessage(win, Win32.WM_KEYDOWN, (int)Keys.Return, 0);
      Win32.PostMessage(win, Win32.WM_KEYUP, (int)Keys.Return, 0);

      return true;
    }
    #endregion

    #region FindToxikkWindow()
    private static IntPtr FindToxikkWindow()
    {
      foreach (Process proc in Process.GetProcessesByName("toxikk"))
      {
        var hWnd = proc.MainWindowHandle;
        Win32.RECT rect;
        Win32.GetWindowRect(hWnd, out rect);
        if (rect.Height >= 600)
          return hWnd;
      }
      return IntPtr.Zero;
    }
    #endregion

    #region StartToxikk()
    private static IntPtr StartToxikk()
    {
      Process.Start("steam://rungameid/324810");
      for (int i = 0; i < SecondsToWaitForMainWindowAfterLaunch; i++)
      {
        Thread.Sleep(1000);
        var hWnd = FindToxikkWindow();
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
        Win32.PostMessage(win, Win32.WM_LBUTTONDOWN, 1, 0);
        Win32.PostMessage(win, Win32.WM_LBUTTONUP, 0, 0);
        Thread.Sleep(500);
      }
    }
    #endregion

    
    #region GetPlayerContextMenu()
    public override List<PlayerContextMenuItem> GetPlayerContextMenu(ServerRow server, Player player)
    {
      var strSteamIds = server.GetRule("p1073741829");
      var strNames = server.GetRule("p1073741832");
      if (string.IsNullOrEmpty(strSteamIds) || string.IsNullOrEmpty(strNames))
        return null;
      var names = strNames.Trim(';').Split(',');
      var idx = Array.IndexOf(names, player.Name);
      var steamIds = strSteamIds.Trim(';').Split(',');
      if (idx < 0 || idx >= steamIds.Length)
        return null;
      var menuItem = new PlayerContextMenuItem("Add to Steam Friends", () =>
      {
        Process.Start("steam://friends/add/" + steamIds[idx]);
      });
      return new List<PlayerContextMenuItem> { menuItem };
    }
    #endregion
  }
}