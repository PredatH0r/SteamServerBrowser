using System;
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
    private const string Version = "1.17";

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
      ServerBrowser.Program.Init(new Font("Segoe UI", AppearanceObject.DefaultFont.Size + 0.75f), ServerBrowser.Properties.Settings.Default.Skin);

      var mainForm = new ServerBrowserForm();
      var icon = typeof(Program).Assembly.GetManifestResourceStream("Main.App.ico");
      if (icon != null)
        mainForm.Icon = new Icon(icon);

      mainForm.Text += " " + Version;

      Application.Run(mainForm);
    }

    public static void Init(Font uiFont, string skinName)
    {
      InitExceptionHandling();
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      ThreadPool.SetMinThreads(100, 90);
      ThreadPool.SetMaxThreads(100, 90);

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