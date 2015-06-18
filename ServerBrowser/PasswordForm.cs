using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ServerBrowser
{
  public partial class PasswordForm : XtraForm
  {
    public PasswordForm()
    {
      InitializeComponent();
      this.ActiveControl = this.txtPassword;
    }

    private void txtPassword_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
        this.DialogResult = DialogResult.Cancel;
      else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
        this.DialogResult = DialogResult.OK;
    }

    public string Password
    {
      get { return this.txtPassword.Text; }
      set { this.txtPassword.Text = value; }
    }
  }
}
