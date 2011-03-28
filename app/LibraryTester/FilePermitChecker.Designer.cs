namespace LibraryTester
{
  partial class FilePermitChecker
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
      this.btnOpenDir = new System.Windows.Forms.Button();
      this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
      this.lblVerdict = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnOpenDir
      // 
      this.btnOpenDir.Location = new System.Drawing.Point(117, 50);
      this.btnOpenDir.Name = "btnOpenDir";
      this.btnOpenDir.Size = new System.Drawing.Size(93, 23);
      this.btnOpenDir.TabIndex = 0;
      this.btnOpenDir.Text = "Open Directory";
      this.btnOpenDir.UseVisualStyleBackColor = true;
      this.btnOpenDir.Click += new System.EventHandler(this.btnOpenDir_Click);
      // 
      // lblVerdict
      // 
      this.lblVerdict.AutoSize = true;
      this.lblVerdict.Location = new System.Drawing.Point(22, 13);
      this.lblVerdict.Name = "lblVerdict";
      this.lblVerdict.Size = new System.Drawing.Size(0, 13);
      this.lblVerdict.TabIndex = 1;
      // 
      // FilePermitChecker
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(338, 85);
      this.Controls.Add(this.lblVerdict);
      this.Controls.Add(this.btnOpenDir);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.Name = "FilePermitChecker";
      this.Text = "FilePermitChecker";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnOpenDir;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.Label lblVerdict;
  }
}