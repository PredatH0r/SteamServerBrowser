using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using DevExpress.Utils;
using ServerBrowser;

namespace Main
{
  public static class Program
  {
    private const string Version = "1.10";

    [STAThread]
    static void Main()
    {
#if false
      var culture = new CultureInfo("en");
      Application.CurrentCulture = culture;
      Thread.CurrentThread.CurrentUICulture = culture;
      Thread.CurrentThread.CurrentCulture = culture;
#endif
      // change font before creating the main form to get correct auto-scaling
      ServerBrowser.Program.Init(new Font("Segoe UI", AppearanceObject.DefaultFont.Size + 0.75f), Properties.Settings.Default.Skin);

      var mainForm = new ServerBrowserForm();
      var icon = typeof (Program).Assembly.GetManifestResourceStream("Main.App.ico");
      if (icon != null)
        mainForm.Icon = new Icon(icon);

      mainForm.Text += " " + Version;

      Application.Run(mainForm);
    }
  }
}