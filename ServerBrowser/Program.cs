using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using DevExpress.Data.Async.Helpers;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.XtraEditors;

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
      var iniPath = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "ServerBrowser.ini");
      var iniFile = new IniFile(iniPath);

      var baseFontSize = iniFile.GetSection("Options")?.GetDecimal("FontSize", 9) ?? 9m;
      // change font before creating the main form to get correct auto-scaling
      Init("Segoe UI", (float)baseFontSize, "Office 2010 Black");

      var mainForm = new ServerBrowserForm(iniFile);
      Application.Run(mainForm);
    }

    public static void Init(string fontName, float fontSize, string skinName)
    {
      InitExceptionHandling();
      WindowsFormsSettings.SetPerMonitorDpiAware();
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      ThreadPool.SetMinThreads(50 + 1 + 5, 100);
      ThreadPool.SetMaxThreads(50 + 1 + 5, 100);

      AppearanceObject.DefaultFont = new Font(fontName, fontSize); // must not create a Font instance before initializing GDI+
      UserLookAndFeel.Default.SkinName = skinName;
      SkinManager.EnableFormSkins();
    }
    
    #region Server Blacklist
    
    private static List<string> InitServerblacklist()
	{
	  List<string> list = new List<string>();
	  using (StreamReader streamReader = new StreamReader(Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "server_blacklist.txt")))
	  {
		while (!streamReader.EndOfStream)
		{
		  String line = streamReader.ReadLine();
		  IPAddress address;
		  if (IPAddress.TryParse(line, out address))
		  {
		    list.Add(line);
		  }
		}
	  }
	  return list;
	}
	
	public static List<string> serverBlacklist = Program.InitServerblacklist();
	#endregion

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