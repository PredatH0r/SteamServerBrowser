namespace ServerBrowser
{
  partial class KeyBindForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
      this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
      this.SuspendLayout();
      // 
      // labelControl1
      // 
      this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
      this.labelControl1.Location = new System.Drawing.Point(12, 28);
      this.labelControl1.Name = "labelControl1";
      this.labelControl1.Size = new System.Drawing.Size(266, 16);
      this.labelControl1.TabIndex = 0;
      this.labelControl1.Text = "message";
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(203, 67);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "Cancel";
      // 
      // KeyBindForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(290, 102);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.labelControl1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "KeyBindForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Key Binding";
      this.ResumeLayout(false);

    }

    #endregion

    private DevExpress.XtraEditors.LabelControl labelControl1;
    private DevExpress.XtraEditors.SimpleButton btnCancel;
  }
}