namespace SimpleInstaller
{
  partial class InstallationPathPrompt
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
      this.label1 = new System.Windows.Forms.Label();
      this.binariesPathTextBox = new System.Windows.Forms.TextBox();
      this.dataPathTextBox = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.btnInstall = new System.Windows.Forms.Button();
      this.binariesPathButton = new System.Windows.Forms.Button();
      this.dataPathButton = new System.Windows.Forms.Button();
      this.binariesBrowseDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.dataBrowseDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.statusStrip = new System.Windows.Forms.StatusStrip();
      this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
      this.statusStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(25, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(232, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Oxigen II will be installed in the following folders:";
      // 
      // binariesPathTextBox
      // 
      this.binariesPathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.binariesPathTextBox.Location = new System.Drawing.Point(30, 39);
      this.binariesPathTextBox.Name = "binariesPathTextBox";
      this.binariesPathTextBox.ReadOnly = true;
      this.binariesPathTextBox.Size = new System.Drawing.Size(335, 20);
      this.binariesPathTextBox.TabIndex = 1;
      // 
      // dataPathTextBox
      // 
      this.dataPathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.dataPathTextBox.Location = new System.Drawing.Point(30, 78);
      this.dataPathTextBox.Name = "dataPathTextBox";
      this.dataPathTextBox.ReadOnly = true;
      this.dataPathTextBox.Size = new System.Drawing.Size(335, 20);
      this.dataPathTextBox.TabIndex = 1;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(27, 119);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(293, 29);
      this.label2.TabIndex = 2;
      this.label2.Text = "IMPORTANT: You will need the .NET Framework 3.5, Adobe Flash 10 and Apple QuickTi" +
          "me 7 to run this software.";
      // 
      // btnInstall
      // 
      this.btnInstall.Location = new System.Drawing.Point(372, 125);
      this.btnInstall.Name = "btnInstall";
      this.btnInstall.Size = new System.Drawing.Size(75, 23);
      this.btnInstall.TabIndex = 3;
      this.btnInstall.Text = "Install";
      this.btnInstall.UseVisualStyleBackColor = true;
      this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
      // 
      // binariesPathButton
      // 
      this.binariesPathButton.Location = new System.Drawing.Point(372, 39);
      this.binariesPathButton.Name = "binariesPathButton";
      this.binariesPathButton.Size = new System.Drawing.Size(75, 23);
      this.binariesPathButton.TabIndex = 4;
      this.binariesPathButton.Text = "Browse...";
      this.binariesPathButton.UseVisualStyleBackColor = true;
      this.binariesPathButton.Click += new System.EventHandler(this.binariesPathButton_Click);
      // 
      // dataPathButton
      // 
      this.dataPathButton.Location = new System.Drawing.Point(372, 78);
      this.dataPathButton.Name = "dataPathButton";
      this.dataPathButton.Size = new System.Drawing.Size(75, 23);
      this.dataPathButton.TabIndex = 5;
      this.dataPathButton.Text = "Browse...";
      this.dataPathButton.UseVisualStyleBackColor = true;
      this.dataPathButton.Click += new System.EventHandler(this.dataPathButton_Click);
      // 
      // statusStrip
      // 
      this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
      this.statusStrip.Location = new System.Drawing.Point(0, 154);
      this.statusStrip.Name = "statusStrip";
      this.statusStrip.Size = new System.Drawing.Size(459, 22);
      this.statusStrip.TabIndex = 7;
      this.statusStrip.Text = "statusStrip";
      // 
      // toolStripProgressBar1
      // 
      this.toolStripProgressBar1.Name = "toolStripProgressBar1";
      this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
      // 
      // InstallationPathPrompt
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(459, 176);
      this.Controls.Add(this.statusStrip);
      this.Controls.Add(this.dataPathButton);
      this.Controls.Add(this.binariesPathButton);
      this.Controls.Add(this.btnInstall);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.dataPathTextBox);
      this.Controls.Add(this.binariesPathTextBox);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "InstallationPathPrompt";
      this.Text = "Setup";
      this.statusStrip.ResumeLayout(false);
      this.statusStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox binariesPathTextBox;
    private System.Windows.Forms.TextBox dataPathTextBox;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnInstall;
    private System.Windows.Forms.Button binariesPathButton;
    private System.Windows.Forms.Button dataPathButton;
    private System.Windows.Forms.FolderBrowserDialog binariesBrowseDialog;
    private System.Windows.Forms.FolderBrowserDialog dataBrowseDialog;
    private System.Windows.Forms.StatusStrip statusStrip;
    private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
  }
}

