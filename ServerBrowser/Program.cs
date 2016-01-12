using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils;

namespace ServerBrowser
{
  public static class Program
  {
    [STAThread]
    public static void Main()
    {
#if false
      var culture = new System.Globalization.CultureInfo("en");
      Application.CurrentCulture = culture;
      System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
      System.Threading.Thread.CurrentThread.CurrentCulture = culture;
#endif
      // change font before creating the main form to get correct auto-scaling
      Init(new Font("Segoe UI", AppearanceObject.DefaultFont.Size + 0.75f), "Office 2010 Black");

      var mainForm = new ServerBrowserForm();
      Application.Run(mainForm);
    }

    public static void Init(Font uiFont, string skinName)
    {
      InitExceptionHandling();
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      ThreadPool.SetMinThreads(50 + GeoIpClient.ThreadCount + 5, 100);
      ThreadPool.SetMaxThreads(50 + GeoIpClient.ThreadCount + 5, 100);

      AppearanceObject.DefaultFont = uiFont;
      UserLookAndFeel.Default.SkinName = skinName;
      SkinManager.EnableFormSkins();
    }

    #region Exception handling

    private static void InitExceptionHandling()
    {
      AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
      Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
      Application.ThreadException += Application_ThreadException;
    }

    private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
    {
      HandleException(e.Exception);
    }

    private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      HandleException(e.ExceptionObject as Exception);
    }

    private static void HandleException(Exception ex)
    {
      MessageBox.Show(ex.ToString(), "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    #endregion
  }
}