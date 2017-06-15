using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using ServerBrowser.Games;

namespace ServerBrowser
{
  public class Reflex : GameExtension
  {
    private const int SecondsToWaitForMainWindowAfterLaunch = 45;
    private const Keys ConsoleKey = Keys.Space; // dummy value. Real value would be OEM5 on German keybaords, OEM3 on US keyboards
    private const int ConsoleKeyScanCode = 0x29 << 16; // upper left key on keyboard
    private bool useKeystrokesToConnect;


    public Reflex()
    {
      // Reflex doesn't reply to A2S_GETRULES queries and would thus show "timeout" for all servers.
      //this.supportsRulesQuery = false;
      this.OptionMenuCaption = "Reflex...";
    }

    #region LoadConfig()
    public override void LoadConfig(IniFile ini)
    {
      var sec = ini.GetSection("Reflex", true);
      this.useKeystrokesToConnect = sec.GetBool("useKeystrokesToConnect", true);
    }
    #endregion

    #region SaveConfig()
    public override void SaveConfig(StringBuilder ini)
    {
      ini.AppendLine();
      ini.AppendLine("[Reflex]");
      ini.AppendLine($"useKeystrokesToConnect={this.useKeystrokesToConnect}");
    }
    #endregion

    #region OnOptionMenuClick()
    public override void OnOptionMenuClick()
    {
      using (var dlg = new QuakeLiveOptionsDialog())
      {
        dlg.Text = "Options for Reflex";
        dlg.UseKeystrokes = this.useKeystrokesToConnect;
        if (dlg.ShowDialog(Form.ActiveForm) != DialogResult.OK)
          return;
        this.useKeystrokesToConnect = dlg.UseKeystrokes;
      }
    }
    #endregion

    #region CustomizeServerGridColumns()
    public override void CustomizeServerGridColumns(GridView view)
    {
      var colDescription = view.Columns["ServerInfo.Description"];
      var idx = colDescription.VisibleIndex;
      colDescription.Visible = false;
      AddColumn(view, "_gametype", "GT", "Gametype", 40, idx)
        .OptionsFilter.AutoFilterCondition = AutoFilterCondition.Default;
      AddColumn(view, "_location", "Loc", "Location", 40, ++idx);
    }
    #endregion

    #region GetServerCellValue()
    public override object GetServerCellValue(ServerRow row, string fieldName)
    {
      if (fieldName == "_gametype")
      {
        var parts = row.ServerInfo?.Extra.Keywords?.Split(',', '|');
        return parts?[0];
      }
      if (fieldName == "_location")
      {
        var parts = row.ServerInfo?.Extra.Keywords?.Split(',', '|');
        return parts?.Length > 1 ? parts[1] : null;
      }

      return base.GetServerCellValue(row, fieldName);
    }
    #endregion

    #region Connect()

    public override bool Connect(ServerRow server, string password, bool spectate)
    {
      if (useKeystrokesToConnect)
      {
        // don't use the ThreadPool b/c it might be full with waiting server update requests
        new Thread(ctx => { ConnectInBackground(server, password, spectate); }).Start();
        return true;
      }
      return base.Connect(server, password, spectate);
    }
    #endregion

    #region ConnectInBackground()
    private void ConnectInBackground(ServerRow server, string password, bool spectate)
    {
      var hWnd = FindGameWindow();
      if (hWnd == IntPtr.Zero)
      {
        hWnd = StartReflex();
        if (hWnd == IntPtr.Zero)
          return;
        SkipIntro(hWnd);
      }

      Win32.PostMessage(hWnd, Win32.WM_KEYDOWN, (int)ConsoleKey, ConsoleKeyScanCode);
      Win32.PostMessage(hWnd, Win32.WM_KEYUP, (int)ConsoleKey, ConsoleKeyScanCode);
      Thread.Sleep(500);

      var msg = "connect " + server.EndPoint.Address + ":" + server.ServerInfo.Extra.Port;
      if (!string.IsNullOrEmpty(password)) // no idea if this is correct
        msg += " " + password;
      foreach (var c in msg)
        Win32.PostMessage(hWnd, Win32.WM_CHAR, c, 0);

      Win32.PostMessage(hWnd, Win32.WM_KEYDOWN, (int)Keys.Return, 0);
      Win32.PostMessage(hWnd, Win32.WM_KEYUP, (int)Keys.Return, 0);

      Win32.PostMessage(hWnd, Win32.WM_KEYDOWN, (int)ConsoleKey, ConsoleKeyScanCode);
      Win32.PostMessage(hWnd, Win32.WM_KEYUP, (int)ConsoleKey, ConsoleKeyScanCode);

      this.ActivateGameWindow(hWnd);
    }
    #endregion

    #region FindGameWindow()
    protected override IntPtr FindGameWindow()
    {
      foreach (Process proc in Process.GetProcessesByName("reflex"))
      {
        // ignore zombie processes
        if (proc.Threads.Count == 0)
          continue;

        var hWnd = proc.MainWindowHandle;
        Win32.RECT rect;

        // wait for the main window and ignore the smaller splash screen
        Win32.GetWindowRect(hWnd, out rect);
        if (rect.Height >= 600)
          return hWnd;

        // when the window is minimized, it can't be the splash screen
        var placement = Win32.GetWindowPlacement(hWnd);
        if (placement.showCmd == Win32.ShowWindowCommands.Minimized)
          return hWnd;
      }
      return IntPtr.Zero;
    }
    #endregion

    #region StartReflex()
    private IntPtr StartReflex()
    {
      Process.Start("steam://rungameid/328070");
      for (int i = 0; i < SecondsToWaitForMainWindowAfterLaunch; i++)
      {
        Thread.Sleep(1000);
        var hWnd = FindGameWindow();
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

  }
}
