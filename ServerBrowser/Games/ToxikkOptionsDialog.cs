using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ServerBrowser.Games
{
  public partial class ToxikkOptionsDialog : XtraForm
  {
    private Keys consoleKey;

    public ToxikkOptionsDialog()
    {
      InitializeComponent();
    }

    public bool UseKeystrokes
    {
      get { return this.cbSendKeystrokes.Checked; }
      set { this.cbSendKeystrokes.Checked = value; }
    }

    public Keys ConsoleKey
    {
      get { return this.consoleKey; }
      set
      {
        this.consoleKey = value;
        this.edConsoleKey.Text = value == 0 ? "" : value.ToString();
      }
    }

    public bool UseSteamId
    {
      get { return this.rbSteamId.Checked; }
      set { (value ? this.rbSteamId : this.rbIpPort).Checked = true; }
    }

    private void edConsoleKey_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
    {
      using (var dlg = new KeyBindForm("Please press your TOXIKK console key ..."))
      {
        if (dlg.ShowDialog(this) == DialogResult.OK)
          this.ConsoleKey = dlg.Key;
      }
    }
  }
}
