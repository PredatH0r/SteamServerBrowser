using DevExpress.XtraEditors;

namespace ServerBrowser.Games
{
  public partial class QuakeLiveOptionsDialog : XtraForm
  {
    public QuakeLiveOptionsDialog()
    {
      InitializeComponent();
      this.linkExtraQL.Appearance.LinkColor = ServerBrowserForm.LinkControlColor;
    }

    public bool UseKeystrokes
    {
      get { return this.cbSendKeystrokes.Checked; }
      set { this.cbSendKeystrokes.Checked = value; }
    }

    public bool StartExtraQL
    {
      get { return this.cbStartExtraQL.Checked; }
      set { this.cbStartExtraQL.Checked = value; }
    }

    private void linkExtraQL_Click(object sender, System.EventArgs e)
    {
      System.Diagnostics.Process.Start("steam://url/CommunityFilePage/539252269");
    }
  }
}
