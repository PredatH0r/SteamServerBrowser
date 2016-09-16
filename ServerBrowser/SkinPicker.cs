using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;

namespace ServerBrowser
{
  public partial class SkinPicker : XtraForm
  {
    string initialSkinName;
    float initialFontSize;
    private readonly string bonusSkinDllPath;

    public SkinPicker(string bonusSkinDllPath)
    {
      InitializeComponent();
      this.bonusSkinDllPath = bonusSkinDllPath;
      this.SetBonusSkinLinkState();
    }

    #region SetBonusSkinLinkState()
    private void SetBonusSkinLinkState()
    {
      if (File.Exists(bonusSkinDllPath) && new FileInfo(bonusSkinDllPath).Length > 0) 
        this.btnDownloadBonusSkins.Visible = false;
      else
      {
        this.btnDownloadBonusSkins.Text = "Download Bonus Skins";
        this.btnDownloadBonusSkins.Visible = true;
        this.btnDownloadBonusSkins.Enabled = true;
      }
    }

    #endregion

    #region SkinPicker_Load()
    private void SkinPicker_Load(object sender, EventArgs e)
    {
      this.initialSkinName = DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName;
      this.cbFontSize.Text = AppearanceObject.DefaultFont.SizeInPoints.ToString("n2");
      this.InitGallery();
    }
    #endregion

    #region InitGallery()
    private void InitGallery()
    {
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
    #endregion

    #region btnReset_Click
    private void btnReset_Click(object sender, EventArgs e)
    {
      const string DefaultSkin = "Office 2010 Black";
      foreach (var galItem in gallery.Gallery.GetAllItems())
      {
        if (galItem.Caption == DefaultSkin)
        {
          galItem.Checked = true;
          break;
        }
      }
      DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = DefaultSkin;
    }
    #endregion

    #region btnDownloadBonusSkins_Click
    private void btnDownloadBonusSkins_Click(object sender, EventArgs e)
    {
      this.btnDownloadBonusSkins.Text = "Downloading...";
      this.btnDownloadBonusSkins.Enabled = false;

      var dllPath = this.bonusSkinDllPath;
      var client = new WebClient();
      client.Proxy = null;
      client.DownloadFileCompleted += delegate(object o, AsyncCompletedEventArgs args)
      {
        ((WebClient)o).Dispose();
        if (args.Error != null)
        {
          XtraMessageBox.Show(this, "Failed to download bonus skin pack:\n" + args.Error, "Skin Pack", MessageBoxButtons.OK, MessageBoxIcon.Error);
          File.Delete(dllPath);
          this.SetBonusSkinLinkState();
          return;
        }
        ServerBrowserForm.LoadBonusSkins(this.bonusSkinDllPath);
        this.SetBonusSkinLinkState();
        this.InitGallery();
      };

      client.DownloadFileAsync(new Uri("https://github.com/PredatH0r/SteamServerBrowser/blob/master/ServerBrowser/DLL/" + Path.GetFileName(dllPath) + "?raw=true"), dllPath);
    }
    #endregion

    #region cbFontSize_SelectedIndexChanged
    private void cbFontSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      initialFontSize = int.Parse(this.cbFontSize.Text);
      AppearanceObject.DefaultFont = new Font(AppearanceObject.DefaultFont.FontFamily, initialFontSize);
    }
    #endregion

    #region btnOk_Click
    private void btnOk_Click(object sender, EventArgs e)
    {
      this.Close();
    }
    #endregion

    #region btnCancel_Click
    private void btnCancel_Click(object sender, EventArgs e)
    {
      DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = this.initialSkinName;
      this.Close();
    }
    #endregion
  }
}
