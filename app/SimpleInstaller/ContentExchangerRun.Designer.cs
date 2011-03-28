namespace SimpleInstaller
{
  partial class ContentExchangerRun
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
      this.info = new System.Windows.Forms.Label();
      this.progressInfo = new System.Windows.Forms.Label();
      this.pictureBox = new System.Windows.Forms.PictureBox();
      this.btnClose = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
      this.SuspendLayout();
      // 
      // info
      // 
      this.info.AutoSize = true;
      this.info.Location = new System.Drawing.Point(52, 25);
      this.info.Name = "info";
      this.info.Size = new System.Drawing.Size(291, 13);
      this.info.TabIndex = 0;
      this.info.Text = "Downloading new content. Please do not close this window.";
      // 
      // progressInfo
      // 
      this.progressInfo.AutoSize = true;
      this.progressInfo.Location = new System.Drawing.Point(102, 93);
      this.progressInfo.Name = "progressInfo";
      this.progressInfo.Size = new System.Drawing.Size(0, 13);
      this.progressInfo.TabIndex = 1;
      // 
      // pictureBox
      // 
      this.pictureBox.Location = new System.Drawing.Point(175, 41);
      this.pictureBox.Name = "pictureBox";
      this.pictureBox.Size = new System.Drawing.Size(36, 37);
      this.pictureBox.TabIndex = 2;
      this.pictureBox.TabStop = false;
      // 
      // btnClose
      // 
      this.btnClose.Enabled = false;
      this.btnClose.Location = new System.Drawing.Point(159, 115);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 23);
      this.btnClose.TabIndex = 3;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
      // 
      // ContentExchangerRun
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(397, 150);
      this.Controls.Add(this.btnClose);
      this.Controls.Add(this.pictureBox);
      this.Controls.Add(this.progressInfo);
      this.Controls.Add(this.info);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "ContentExchangerRun";
      this.Text = "Downloading Content";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Closing);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label info;
    private System.Windows.Forms.Label progressInfo;
    private System.Windows.Forms.PictureBox pictureBox;
    private System.Windows.Forms.Button btnClose;
  }
}