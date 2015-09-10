using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ServerBrowser
{
  public partial class RenameTabForm : XtraForm
  {
    public RenameTabForm()
    {
      InitializeComponent();
      this.ActiveControl = this.txtCaption;
    }

    private void txtCaption_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
        this.DialogResult = DialogResult.Cancel;
      else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
        this.DialogResult = DialogResult.OK;
    }

    public string Caption
    {
      get { return this.txtCaption.Text; }
      set { this.txtCaption.Text = value; }
    }
  }
}
