using DevExpress.XtraEditors;

namespace ServerBrowser.Games
{
  public partial class QuakeLiveOptionsDialog : XtraForm
  {
    public QuakeLiveOptionsDialog()
    {
      InitializeComponent();
    }

    public bool UseKeystrokes
    {
      get { return this.cbSendKeystrokes.Checked; }
      set { this.cbSendKeystrokes.Checked = value; }
    }
  }
}
