using System;
using System.Threading;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Skins;

namespace ServerBrowser
{
  static class Program
  {
    [STAThread]
    static void Main()
    {
      InitExceptionHandling();
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      
      ThreadPool.SetMinThreads(100, 90);
      ThreadPool.SetMaxThreads(100, 90);

      UserLookAndFeel.Default.SkinName = "Office 2010 Black";
      SkinManager.EnableFormSkins();
      Application.Run(new ServerBrowserForm());
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