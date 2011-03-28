namespace LibraryTester
{
  partial class ChecksumCalculator
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
      this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.btnChecksumWrite = new System.Windows.Forms.Button();
      this.textBox = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // folderBrowserDialog
      // 
      this.folderBrowserDialog.Description = "Select folder with the data to keep the checksums of";
      this.folderBrowserDialog.SelectedPath = "C:\\OxigenData";
      this.folderBrowserDialog.ShowNewFolderButton = false;
      // 
      // btnChecksumWrite
      // 
      this.btnChecksumWrite.FlatAppearance.BorderColor = System.Drawing.Color.Green;
      this.btnChecksumWrite.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Aqua;
      this.btnChecksumWrite.Location = new System.Drawing.Point(279, 29);
      this.btnChecksumWrite.Name = "btnChecksumWrite";
      this.btnChecksumWrite.Size = new System.Drawing.Size(101, 23);
      this.btnChecksumWrite.TabIndex = 0;
      this.btnChecksumWrite.Text = "Write Checksums";
      this.btnChecksumWrite.UseVisualStyleBackColor = true;
      this.btnChecksumWrite.Click += new System.EventHandler(this.btnChecksumWrite_Click);
      // 
      // textBox
      // 
      this.textBox.Location = new System.Drawing.Point(13, 29);
      this.textBox.Name = "textBox";
      this.textBox.ReadOnly = true;
      this.textBox.Size = new System.Drawing.Size(260, 20);
      this.textBox.TabIndex = 1;
      // 
      // ChecksumCalculator
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(392, 81);
      this.Controls.Add(this.textBox);
      this.Controls.Add(this.btnChecksumWrite);
      this.Name = "ChecksumCalculator";
      this.Text = "ChechsumCalculator";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    private System.Windows.Forms.Button btnChecksumWrite;
    private System.Windows.Forms.TextBox textBox;
  }
}