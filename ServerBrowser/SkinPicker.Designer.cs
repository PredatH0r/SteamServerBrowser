namespace ServerBrowser
{
  partial class SkinPicker
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.gallery = new DevExpress.XtraBars.Ribbon.GalleryControl();
      this.galleryControlClient1 = new DevExpress.XtraBars.Ribbon.GalleryControlClient();
      this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
      this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
      this.btnOk = new DevExpress.XtraEditors.SimpleButton();
      this.btnReset = new DevExpress.XtraEditors.SimpleButton();
      ((System.ComponentModel.ISupportInitialize)(this.gallery)).BeginInit();
      this.gallery.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
      this.panelControl1.SuspendLayout();
      this.SuspendLayout();
      // 
      // gallery
      // 
      this.gallery.Controls.Add(this.galleryControlClient1);
      this.gallery.DesignGalleryGroupIndex = 0;
      this.gallery.DesignGalleryItemIndex = 0;
      this.gallery.Dock = System.Windows.Forms.DockStyle.Fill;
      // 
      // galleryControlGallery1
      // 
      this.gallery.Gallery.AllowFilter = false;
      this.gallery.Gallery.ColumnCount = 8;
      this.gallery.Gallery.ImageSize = new System.Drawing.Size(48, 48);
      this.gallery.Gallery.ShowItemText = true;
      this.gallery.Gallery.ShowScrollBar = DevExpress.XtraBars.Ribbon.Gallery.ShowScrollBar.Auto;
      this.gallery.Location = new System.Drawing.Point(0, 0);
      this.gallery.Name = "gallery";
      this.gallery.Size = new System.Drawing.Size(984, 527);
      this.gallery.TabIndex = 0;
      this.gallery.Text = "galleryControl1";
      // 
      // galleryControlClient1
      // 
      this.galleryControlClient1.GalleryControl = this.gallery;
      this.galleryControlClient1.Location = new System.Drawing.Point(2, 2);
      this.galleryControlClient1.Size = new System.Drawing.Size(980, 523);
      // 
      // panelControl1
      // 
      this.panelControl1.Controls.Add(this.btnCancel);
      this.panelControl1.Controls.Add(this.btnOk);
      this.panelControl1.Controls.Add(this.btnReset);
      this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panelControl1.Location = new System.Drawing.Point(0, 527);
      this.panelControl1.Name = "panelControl1";
      this.panelControl1.Size = new System.Drawing.Size(984, 35);
      this.panelControl1.TabIndex = 0;
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(286, 7);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(84, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "Abbrechen";
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // btnOk
      // 
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(186, 7);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(84, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "Ok";
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // btnReset
      // 
      this.btnReset.Location = new System.Drawing.Point(12, 7);
      this.btnReset.Name = "btnReset";
      this.btnReset.Size = new System.Drawing.Size(158, 23);
      this.btnReset.TabIndex = 0;
      this.btnReset.Text = "Standarddesign verwenden";
      this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
      // 
      // SkinPicker
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(984, 562);
      this.Controls.Add(this.gallery);
      this.Controls.Add(this.panelControl1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Name = "SkinPicker";
      this.Text = "Design und Farbschema";
      this.Load += new System.EventHandler(this.SkinPicker_Load);
      ((System.ComponentModel.ISupportInitialize)(this.gallery)).EndInit();
      this.gallery.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
      this.panelControl1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private DevExpress.XtraBars.Ribbon.GalleryControl gallery;
    private DevExpress.XtraBars.Ribbon.GalleryControlClient galleryControlClient1;
    private DevExpress.XtraEditors.PanelControl panelControl1;
    private DevExpress.XtraEditors.SimpleButton btnCancel;
    private DevExpress.XtraEditors.SimpleButton btnOk;
    private DevExpress.XtraEditors.SimpleButton btnReset;
  }
}