namespace ServerBrowser.Games
{
  partial class QuakeLiveOptionsDialog
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
      this.cbSendKeystrokes = new DevExpress.XtraEditors.CheckEdit();
      this.btnOk = new DevExpress.XtraEditors.SimpleButton();
      this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
      this.cbStartExtraQL = new DevExpress.XtraEditors.CheckEdit();
      this.linkExtraQL = new DevExpress.XtraEditors.HyperlinkLabelControl();
      ((System.ComponentModel.ISupportInitialize)(this.cbSendKeystrokes.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbStartExtraQL.Properties)).BeginInit();
      this.SuspendLayout();
      // 
      // cbSendKeystrokes
      // 
      this.cbSendKeystrokes.Location = new System.Drawing.Point(13, 13);
      this.cbSendKeystrokes.Name = "cbSendKeystrokes";
      this.cbSendKeystrokes.Properties.Caption = "Use key strokes to connect";
      this.cbSendKeystrokes.Size = new System.Drawing.Size(168, 19);
      this.cbSendKeystrokes.TabIndex = 0;
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(319, 11);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 23);
      this.btnOk.TabIndex = 3;
      this.btnOk.Text = "Ok";
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(319, 40);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Text = "Cancel";
      // 
      // cbStartExtraQL
      // 
      this.cbStartExtraQL.Location = new System.Drawing.Point(13, 37);
      this.cbStartExtraQL.Name = "cbStartExtraQL";
      this.cbStartExtraQL.Properties.Caption = "Start extraQL when connecting to Quake Live";
      this.cbStartExtraQL.Size = new System.Drawing.Size(256, 19);
      this.cbStartExtraQL.TabIndex = 5;
      // 
      // linkExtraQL
      // 
      this.linkExtraQL.Cursor = System.Windows.Forms.Cursors.Hand;
      this.linkExtraQL.Location = new System.Drawing.Point(31, 56);
      this.linkExtraQL.Name = "linkExtraQL";
      this.linkExtraQL.Size = new System.Drawing.Size(179, 13);
      this.linkExtraQL.TabIndex = 6;
      this.linkExtraQL.Text = "Open extraQL Steam Workshop Page";
      this.linkExtraQL.Click += new System.EventHandler(this.linkExtraQL_Click);
      // 
      // QuakeLiveOptionsDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(406, 85);
      this.Controls.Add(this.linkExtraQL);
      this.Controls.Add(this.cbStartExtraQL);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.cbSendKeystrokes);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "QuakeLiveOptionsDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Options for Quake Live";
      ((System.ComponentModel.ISupportInitialize)(this.cbSendKeystrokes.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.cbStartExtraQL.Properties)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private DevExpress.XtraEditors.CheckEdit cbSendKeystrokes;
    private DevExpress.XtraEditors.SimpleButton btnOk;
    private DevExpress.XtraEditors.SimpleButton btnCancel;
    private DevExpress.XtraEditors.CheckEdit cbStartExtraQL;
    private DevExpress.XtraEditors.HyperlinkLabelControl linkExtraQL;
  }
}