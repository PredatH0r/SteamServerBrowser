using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ServerBrowser
{
  public partial class KeyBindForm : XtraForm, IMessageFilter
  {
    public KeyBindForm(string message)
    {
      InitializeComponent();
      this.labelControl1.Text = message;
      Application.AddMessageFilter(this);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      Application.RemoveMessageFilter(this);
      base.Dispose(disposing);
    }


    public Keys Key { get; private set; }

    bool IMessageFilter.PreFilterMessage(ref Message msg)
    {
      if (msg.Msg == Win32.WM_KEYDOWN)
      {
        Key = (Keys)msg.WParam;
        this.DialogResult = DialogResult.OK;
        return true;
      }
      return false;
    }
  }
}
