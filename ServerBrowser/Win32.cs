using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ServerBrowser
{
  public static class Win32
  {
    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

    [DllImport("user32.dll")]
    public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, string lParam);

    [DllImport("user32.dll")]
    public static extern int PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

    [DllImport("user32.dll")]
    public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

    [DllImport("user32.dll")]
    public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

    [DllImport("user32")]
    public static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr i);

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

    [DllImport("user32.dll")]
    public static extern bool AdjustWindowRect(ref RECT rect, uint dwStyle, bool bMenu);

    [DllImport("user32.dll")]
    public static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImportAttribute("user32.dll")]
    public static extern bool ReleaseCapture();

    [DllImportAttribute("user32.dll")]
    public static extern bool IsWindowUnicode(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

    public delegate bool EnumWindowProc(IntPtr hWnd, IntPtr lParam);

    public const int WM_KEYDOWN = 0x100;
    public const int WM_KEYUP = 0x101;
    public const int WM_CHAR = 0x0102;
    public const int WM_UNICHAR = 0x0109;
    public const int WM_IME_CHAR = 0x0286;
    public const int WM_MOUSEMOVE = 0x0200;
    public const int WM_LBUTTONDOWN = 0x0201;
    public const int WM_LBUTTONUP = 0x0202;
    public const int WM_SETTEXT = 0x000C;
    public const int WM_NCLBUTTONDOWN = 0x00A1;
    public const int WM_EXITSIZEMOVE = 0x0232;
    public const int WM_SYSKEYDOWN = 0x0104;
    public const int WM_SYSKEYUP = 0x0105;

    public const int SWP_NOACTIVATE = 0x0010;
    public const int SWP_NOOWNERZORDER = 0x0200;
    public const int SWP_NOSENDCHANGING = 0x0400;
    public const int SWP_NOZORDER = 0x0004;

    public const int GWL_STYLE = -16;

    public const int WS_DISABLED = 0x08000000;

    public const int HT_CAPTION = 2;

    #region struct RECT

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
      public int Left, Top, Right, Bottom;

      public int Height
      {
        get { return Bottom - Top; }
        set { Bottom = value + Top; }
      }

      public int Width
      {
        get { return Right - Left; }
        set { Right = value + Left; }
      }
    }

    #endregion

    #region struct WINDOWPLACEMENT
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWPLACEMENT
    {
      public int length;
      public int flags;
      public ShowWindowCommands showCmd;
      public System.Drawing.Point ptMinPosition;
      public System.Drawing.Point ptMaxPosition;
      public System.Drawing.Rectangle rcNormalPosition;
    }
    #endregion

    #region enum ShowWindowCommands
    public enum ShowWindowCommands
    {
      Hide = 0,
      Normal = 1,
      Minimized = 2,
      Maximized = 3,
    }
    #endregion

    #region GetChildWindows()

    public static List<IntPtr> GetChildWindows(IntPtr parent)
    {
      var result = new List<IntPtr>();
      GCHandle listHandle = GCHandle.Alloc(result);
      try
      {
        EnumWindowProc childProc = EnumWindow;
        EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
      }
      finally
      {
        if (listHandle.IsAllocated)
          listHandle.Free();
      }
      return result;
    }

    /// <summary>
    ///   Callback method to be used when enumerating windows.
    /// </summary>
    /// <param name="handle">Handle of the next window</param>
    /// <param name="pointer">Pointer to a GCHandle that holds a reference to the list to fill</param>
    /// <returns>True to continue the enumeration, false to bail</returns>
    private static bool EnumWindow(IntPtr handle, IntPtr pointer)
    {
      GCHandle gch = GCHandle.FromIntPtr(pointer);
      var list = gch.Target as List<IntPtr>;
      if (list == null)
      {
        throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
      }
      list.Add(handle);
      //  You can modify this to check to see if you want to cancel the operation, then return a null here
      return true;
    }

    #endregion

    #region GetWindowPlacement()
    public static WINDOWPLACEMENT GetWindowPlacement(IntPtr hwnd)
    {
      WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
      placement.length = Marshal.SizeOf(placement);
      GetWindowPlacement(hwnd, ref placement);
      return placement;
    }
    #endregion
  }
}