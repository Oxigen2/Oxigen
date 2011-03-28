namespace OxigenIIScreenSaverConfig
{
  partial class ScreenSaverConfigForm
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
      this.txtContentLastDownloaded = new System.Windows.Forms.TextBox();
      this.txtLastRunStatus = new System.Windows.Forms.TextBox();
      this.txtLastRunDate = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.btnApply = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOK = new System.Windows.Forms.Button();
      this.VolumeControl = new System.Windows.Forms.GroupBox();
      this.soundEnabled = new System.Windows.Forms.CheckBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.txtDefaultDisplayDuration = new System.Windows.Forms.MaskedTextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.txtSpace = new System.Windows.Forms.MaskedTextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.maxSpaceMessage = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.VolumeControl.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.SuspendLayout();
      // 
      // txtContentLastDownloaded
      // 
      this.txtContentLastDownloaded.Enabled = false;
      this.txtContentLastDownloaded.Location = new System.Drawing.Point(206, 97);
      this.txtContentLastDownloaded.Name = "txtContentLastDownloaded";
      this.txtContentLastDownloaded.ReadOnly = true;
      this.txtContentLastDownloaded.Size = new System.Drawing.Size(118, 20);
      this.txtContentLastDownloaded.TabIndex = 5;
      // 
      // txtLastRunStatus
      // 
      this.txtLastRunStatus.Enabled = false;
      this.txtLastRunStatus.Location = new System.Drawing.Point(206, 65);
      this.txtLastRunStatus.Name = "txtLastRunStatus";
      this.txtLastRunStatus.ReadOnly = true;
      this.txtLastRunStatus.Size = new System.Drawing.Size(118, 20);
      this.txtLastRunStatus.TabIndex = 4;
      // 
      // txtLastRunDate
      // 
      this.txtLastRunDate.Enabled = false;
      this.txtLastRunDate.Location = new System.Drawing.Point(206, 32);
      this.txtLastRunDate.Name = "txtLastRunDate";
      this.txtLastRunDate.ReadOnly = true;
      this.txtLastRunDate.Size = new System.Drawing.Size(118, 20);
      this.txtLastRunDate.TabIndex = 3;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(60, 100);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(133, 13);
      this.label3.TabIndex = 2;
      this.label3.Text = "Content Last Downloaded:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(107, 68);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(86, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Last Run Status:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(7, 61);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(196, 13);
      this.label4.TabIndex = 1;
      this.label4.Text = "Please enter a value between 5 and 20:";
      // 
      // btnApply
      // 
      this.btnApply.Enabled = false;
      this.btnApply.Location = new System.Drawing.Point(310, 416);
      this.btnApply.Name = "btnApply";
      this.btnApply.Size = new System.Drawing.Size(75, 23);
      this.btnApply.TabIndex = 6;
      this.btnApply.Text = "Apply";
      this.btnApply.UseVisualStyleBackColor = true;
      this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(229, 416);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 5;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // btnOK
      // 
      this.btnOK.Location = new System.Drawing.Point(148, 416);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 4;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // VolumeControl
      // 
      this.VolumeControl.Controls.Add(this.soundEnabled);
      this.VolumeControl.Location = new System.Drawing.Point(12, 12);
      this.VolumeControl.Name = "VolumeControl";
      this.VolumeControl.Size = new System.Drawing.Size(381, 55);
      this.VolumeControl.TabIndex = 0;
      this.VolumeControl.TabStop = false;
      this.VolumeControl.Text = "Sound Control";
      // 
      // soundEnabled
      // 
      this.soundEnabled.AutoSize = true;
      this.soundEnabled.Location = new System.Drawing.Point(10, 28);
      this.soundEnabled.Name = "soundEnabled";
      this.soundEnabled.Size = new System.Drawing.Size(99, 17);
      this.soundEnabled.TabIndex = 0;
      this.soundEnabled.Text = "Sound Enabled";
      this.soundEnabled.UseVisualStyleBackColor = true;
      this.soundEnabled.CheckedChanged += new System.EventHandler(this.Sound_CheckedChanged);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.txtDefaultDisplayDuration);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.label5);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Location = new System.Drawing.Point(12, 73);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(381, 88);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Default Display Duration";
      // 
      // txtDefaultDisplayDuration
      // 
      this.txtDefaultDisplayDuration.Location = new System.Drawing.Point(282, 58);
      this.txtDefaultDisplayDuration.Mask = "00";
      this.txtDefaultDisplayDuration.Name = "txtDefaultDisplayDuration";
      this.txtDefaultDisplayDuration.PromptChar = ' ';
      this.txtDefaultDisplayDuration.Size = new System.Drawing.Size(38, 20);
      this.txtDefaultDisplayDuration.TabIndex = 2;
      this.txtDefaultDisplayDuration.TextChanged += new System.EventHandler(this.DisplayDuration_TextChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(7, 25);
      this.label1.MaximumSize = new System.Drawing.Size(390, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(368, 26);
      this.label1.TabIndex = 0;
      this.label1.Text = "If a display duration has not been specified for a slide, you can set its default" +
          " exposure here.";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(326, 61);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(47, 13);
      this.label5.TabIndex = 3;
      this.label5.Text = "seconds";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(113, 35);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(79, 13);
      this.label6.TabIndex = 0;
      this.label6.Text = "Last Run Date:";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.txtSpace);
      this.groupBox2.Controls.Add(this.label7);
      this.groupBox2.Controls.Add(this.maxSpaceMessage);
      this.groupBox2.Controls.Add(this.label8);
      this.groupBox2.Location = new System.Drawing.Point(12, 167);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(381, 98);
      this.groupBox2.TabIndex = 2;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Disk Space Allocation";
      // 
      // txtSpace
      // 
      this.txtSpace.Location = new System.Drawing.Point(264, 69);
      this.txtSpace.Mask = "0000000";
      this.txtSpace.Name = "txtSpace";
      this.txtSpace.PromptChar = ' ';
      this.txtSpace.Size = new System.Drawing.Size(56, 20);
      this.txtSpace.TabIndex = 2;
      this.txtSpace.TextChanged += new System.EventHandler(this.Space_TextChanged);
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(7, 26);
      this.label7.MaximumSize = new System.Drawing.Size(372, 0);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(359, 26);
      this.label7.TabIndex = 0;
      this.label7.Text = "Please enter the maximum amount of disk space, in megabytes, you would like Oxige" +
          "n to use for storing its content (minimum 100MB):";
      // 
      // maxSpaceMessage
      // 
      this.maxSpaceMessage.AutoSize = true;
      this.maxSpaceMessage.Location = new System.Drawing.Point(7, 72);
      this.maxSpaceMessage.Name = "maxSpaceMessage";
      this.maxSpaceMessage.Size = new System.Drawing.Size(100, 13);
      this.maxSpaceMessage.TabIndex = 1;
      this.maxSpaceMessage.Text = "maxSpaceMessage";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(326, 72);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(23, 13);
      this.label8.TabIndex = 3;
      this.label8.Text = "MB";
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.txtContentLastDownloaded);
      this.groupBox3.Controls.Add(this.label6);
      this.groupBox3.Controls.Add(this.txtLastRunStatus);
      this.groupBox3.Controls.Add(this.label2);
      this.groupBox3.Controls.Add(this.label3);
      this.groupBox3.Controls.Add(this.txtLastRunDate);
      this.groupBox3.Location = new System.Drawing.Point(12, 271);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(381, 139);
      this.groupBox3.TabIndex = 3;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Content Report";
      // 
      // ScreenSaverConfigForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(397, 451);
      this.Controls.Add(this.groupBox3);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.VolumeControl);
      this.Controls.Add(this.btnApply);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.btnCancel);
      this.Icon = global::OxigenIIScreenSaverConfig.Properties.Resources.oxigen;
      this.Name = "ScreenSaverConfigForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Oxigen Settings";
      this.VolumeControl.ResumeLayout(false);
      this.VolumeControl.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnApply;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.TextBox txtContentLastDownloaded;
    private System.Windows.Forms.TextBox txtLastRunStatus;
    private System.Windows.Forms.TextBox txtLastRunDate;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.GroupBox VolumeControl;
    private System.Windows.Forms.CheckBox soundEnabled;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.MaskedTextBox txtDefaultDisplayDuration;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.MaskedTextBox txtSpace;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label maxSpaceMessage;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.GroupBox groupBox3;
  }
}

