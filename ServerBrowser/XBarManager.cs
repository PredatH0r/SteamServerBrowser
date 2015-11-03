using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

namespace ServerBrowser
{
  public class XBarManager : BarManager
  {
    public XBarManager() : base()
    {
      RegisterEvents();
    }

    public XBarManager(IContainer cont) : base(cont)
    {
      RegisterEvents();
    }

    private void RegisterEvents()
    {
      this.ShortcutItemClick += OnShortcutItemClick;
    }

    private void OnShortcutItemClick(object sender, ShortcutItemClickEventArgs e)
    {
      // prevents that a BarManager from a different DockPanel reacts to the shortcut
      if (!this.Form.ContainsFocus)
        e.Cancel = true;

      // prevent that the BarManager reacts to e.g. Ctrl+C when the current editor should react to it
      if (e.Shortcut.Key == (Keys.Control | Keys.C) || e.Shortcut.Key == (Keys.Control | Keys.V))
      {
        Control focusedControl = GetFocus();
        if (focusedControl is TextBox || focusedControl is TextEdit)
          e.Cancel = true;
      }
    }


    #region GetFocus()
    [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "GetFocus")]
    private static extern IntPtr GetFocusCore();

    /// <summary>
    /// Returns the control that currently has the input focus
    /// </summary>
    /// <returns></returns>
    public static Control GetFocus()
    {
      IntPtr hwnd = GetFocusCore();
      if (hwnd == IntPtr.Zero)
        return null;
      return Control.FromHandle(hwnd);
    }
    #endregion

  }
}
