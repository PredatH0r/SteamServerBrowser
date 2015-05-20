using System;
using DevExpress.XtraEditors;
using ServerBrowser.Properties;

namespace ServerBrowser
{
  public partial class SkinPicker : XtraForm
  {
    string initialSkinName;

    public SkinPicker()
    {
      InitializeComponent();
    }

    private void SkinPicker_Load(object sender, EventArgs e)
    {
      this.initialSkinName = DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName;

      DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(gallery);
      gallery.Gallery.ColumnCount = 8;
      gallery.Gallery.AllowFilter = false;
      gallery.Gallery.FixedImageSize = true;
      gallery.Gallery.ImageSize = new System.Drawing.Size(48, 48);
      gallery.Gallery.FixedHoverImageSize = true;
      gallery.Gallery.HoverImageSize = new System.Drawing.Size(48, 48);
      gallery.Gallery.ShowItemText = true;
      gallery.Gallery.ShowScrollBar = DevExpress.XtraBars.Ribbon.Gallery.ShowScrollBar.Auto;
      foreach (var galItem in gallery.Gallery.GetAllItems())
      {
        galItem.Caption = galItem.Caption.Replace("DevExpress", "DX");
        galItem.Image = galItem.HoverImage;
      }
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
      foreach (var galItem in gallery.Gallery.GetAllItems())
      {
        if (galItem.Caption == DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName)
        {
          galItem.Checked = true;
          break;
        }
      }
      DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = this.initialSkinName;
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      Settings.Default.Skin = DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName;
      Settings.Default.Save();
      this.Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = this.initialSkinName;
      this.Close();
    }
  }
}
