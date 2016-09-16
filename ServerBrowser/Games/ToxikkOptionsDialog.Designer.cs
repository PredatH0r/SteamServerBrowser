namespace ServerBrowser.Games
{
  partial class ToxikkOptionsDialog
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
      this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
      this.edConsoleKey = new DevExpress.XtraEditors.ButtonEdit();
      this.btnOk = new DevExpress.XtraEditors.SimpleButton();
      this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
      this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
      this.rbIpPort = new DevExpress.XtraEditors.CheckEdit();
      this.rbSteamId = new DevExpress.XtraEditors.CheckEdit();
      this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
      this.comboSkillClass = new DevExpress.XtraEditors.ComboBoxEdit();
      this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
      ((System.ComponentModel.ISupportInitialize)(this.cbSendKeystrokes.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.edConsoleKey.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbIpPort.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbSteamId.Properties)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboSkillClass.Properties)).BeginInit();
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
      // labelControl1
      // 
      this.labelControl1.Location = new System.Drawing.Point(34, 45);
      this.labelControl1.Name = "labelControl1";
      this.labelControl1.Size = new System.Drawing.Size(63, 13);
      this.labelControl1.TabIndex = 1;
      this.labelControl1.Text = "Console Key:";
      // 
      // edConsoleKey
      // 
      this.edConsoleKey.Location = new System.Drawing.Point(116, 42);
      this.edConsoleKey.Name = "edConsoleKey";
      this.edConsoleKey.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
      this.edConsoleKey.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
      this.edConsoleKey.Size = new System.Drawing.Size(106, 20);
      this.edConsoleKey.TabIndex = 2;
      this.edConsoleKey.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.edConsoleKey_ButtonClick);
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(348, 11);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 23);
      this.btnOk.TabIndex = 3;
      this.btnOk.Text = "Ok";
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(348, 40);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Text = "Cancel";
      // 
      // labelControl2
      // 
      this.labelControl2.Location = new System.Drawing.Point(13, 69);
      this.labelControl2.Name = "labelControl2";
      this.labelControl2.Size = new System.Drawing.Size(85, 13);
      this.labelControl2.TabIndex = 5;
      this.labelControl2.Text = "Connect through:";
      // 
      // rbIpPort
      // 
      this.rbIpPort.Location = new System.Drawing.Point(116, 66);
      this.rbIpPort.Name = "rbIpPort";
      this.rbIpPort.Properties.Caption = "IP:port";
      this.rbIpPort.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
      this.rbIpPort.Properties.RadioGroupIndex = 1;
      this.rbIpPort.Size = new System.Drawing.Size(75, 19);
      this.rbIpPort.TabIndex = 6;
      this.rbIpPort.TabStop = false;
      // 
      // rbSteamId
      // 
      this.rbSteamId.EditValue = true;
      this.rbSteamId.Location = new System.Drawing.Point(184, 66);
      this.rbSteamId.Name = "rbSteamId";
      this.rbSteamId.Properties.Caption = "Steam-ID";
      this.rbSteamId.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
      this.rbSteamId.Properties.RadioGroupIndex = 1;
      this.rbSteamId.Size = new System.Drawing.Size(75, 19);
      this.rbSteamId.TabIndex = 7;
      // 
      // labelControl3
      // 
      this.labelControl3.Location = new System.Drawing.Point(13, 97);
      this.labelControl3.Name = "labelControl3";
      this.labelControl3.Size = new System.Drawing.Size(76, 13);
      this.labelControl3.TabIndex = 8;
      this.labelControl3.Text = "Skill Class Filter:";
      // 
      // comboSkillClass
      // 
      this.comboSkillClass.EditValue = "ALL";
      this.comboSkillClass.Location = new System.Drawing.Point(116, 94);
      this.comboSkillClass.Name = "comboSkillClass";
      this.comboSkillClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
      this.comboSkillClass.Properties.Items.AddRange(new object[] {
            "ALL",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
      this.comboSkillClass.Size = new System.Drawing.Size(57, 20);
      this.comboSkillClass.TabIndex = 9;
      // 
      // labelControl4
      // 
      this.labelControl4.Location = new System.Drawing.Point(184, 97);
      this.labelControl4.Name = "labelControl4";
      this.labelControl4.Size = new System.Drawing.Size(230, 13);
      this.labelControl4.TabIndex = 10;
      this.labelControl4.Text = "set this to your SC to only see matching servers";
      // 
      // ToxikkOptionsDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(435, 137);
      this.Controls.Add(this.labelControl4);
      this.Controls.Add(this.comboSkillClass);
      this.Controls.Add(this.labelControl3);
      this.Controls.Add(this.rbSteamId);
      this.Controls.Add(this.rbIpPort);
      this.Controls.Add(this.labelControl2);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.edConsoleKey);
      this.Controls.Add(this.labelControl1);
      this.Controls.Add(this.cbSendKeystrokes);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "ToxikkOptionsDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Options for TOXIKK";
      ((System.ComponentModel.ISupportInitialize)(this.cbSendKeystrokes.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.edConsoleKey.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbIpPort.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.rbSteamId.Properties)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.comboSkillClass.Properties)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private DevExpress.XtraEditors.CheckEdit cbSendKeystrokes;
    private DevExpress.XtraEditors.LabelControl labelControl1;
    private DevExpress.XtraEditors.ButtonEdit edConsoleKey;
    private DevExpress.XtraEditors.SimpleButton btnOk;
    private DevExpress.XtraEditors.SimpleButton btnCancel;
    private DevExpress.XtraEditors.LabelControl labelControl2;
    private DevExpress.XtraEditors.CheckEdit rbIpPort;
    private DevExpress.XtraEditors.CheckEdit rbSteamId;
    private DevExpress.XtraEditors.LabelControl labelControl3;
    private DevExpress.XtraEditors.ComboBoxEdit comboSkillClass;
    private DevExpress.XtraEditors.LabelControl labelControl4;
  }
}