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
    public int ScanCode { get; private set; }

    bool IMessageFilter.PreFilterMessage(ref Message msg)
    {
      if (msg.Msg == Win32.WM_KEYDOWN)
      {
        this.Key = (Keys)msg.WParam;
        this.ScanCode = (int)msg.LParam & 0x00FF0000;
        this.DialogResult = DialogResult.OK;
        return true;
      }
      return false;
    }
  }
}
