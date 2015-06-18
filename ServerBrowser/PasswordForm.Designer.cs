namespace ServerBrowser
{
  partial class PasswordForm
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
      this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
      this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
      this.txtPassword = new DevExpress.XtraEditors.TextEdit();
      this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
      this.btnOk = new DevExpress.XtraEditors.SimpleButton();
      this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
      ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
      this.SuspendLayout();
      // 
      // labelControl1
      // 
      this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelControl1.Location = new System.Drawing.Point(13, 13);
      this.labelControl1.Name = "labelControl1";
      this.labelControl1.Size = new System.Drawing.Size(222, 16);
      this.labelControl1.TabIndex = 0;
      this.labelControl1.Text = "You are connecting to a private server.";
      // 
      // labelControl2
      // 
      this.labelControl2.Location = new System.Drawing.Point(13, 52);
      this.labelControl2.Name = "labelControl2";
      this.labelControl2.Size = new System.Drawing.Size(50, 13);
      this.labelControl2.TabIndex = 1;
      this.labelControl2.Text = "Password:";
      // 
      // txtPassword
      // 
      this.txtPassword.Location = new System.Drawing.Point(69, 51);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.Size = new System.Drawing.Size(171, 20);
      this.txtPassword.TabIndex = 2;
      this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
      // 
      // labelControl3
      // 
      this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
      this.labelControl3.Location = new System.Drawing.Point(13, 87);
      this.labelControl3.Name = "labelControl3";
      this.labelControl3.Size = new System.Drawing.Size(227, 26);
      this.labelControl3.TabIndex = 3;
      this.labelControl3.Text = "(If the game has an internal password prompt, you can leave the password field em" +
    "pty)";
      // 
      // btnOk
      // 
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(260, 49);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 23);
      this.btnOk.TabIndex = 4;
      this.btnOk.Text = "Ok";
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(260, 90);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 5;
      this.btnCancel.Text = "Cancel";
      // 
      // PasswordForm
      // 
      this.AcceptButton = this.btnOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(346, 127);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.labelControl3);
      this.Controls.Add(this.txtPassword);
      this.Controls.Add(this.labelControl2);
      this.Controls.Add(this.labelControl1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "PasswordForm";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Server Password";
      ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private DevExpress.XtraEditors.LabelControl labelControl1;
    private DevExpress.XtraEditors.LabelControl labelControl2;
    private DevExpress.XtraEditors.TextEdit txtPassword;
    private DevExpress.XtraEditors.LabelControl labelControl3;
    private DevExpress.XtraEditors.SimpleButton btnOk;
    private DevExpress.XtraEditors.SimpleButton btnCancel;
  }
}