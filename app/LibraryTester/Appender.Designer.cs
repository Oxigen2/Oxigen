namespace LibraryTester
{
  partial class Appender
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
      this.btnAppend = new System.Windows.Forms.Button();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.btnEncrypt = new System.Windows.Forms.Button();
      this.btnLoad = new System.Windows.Forms.Button();
      this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.btnEncryptUsage = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // btnAppend
      // 
      this.btnAppend.Location = new System.Drawing.Point(354, 470);
      this.btnAppend.Name = "btnAppend";
      this.btnAppend.Size = new System.Drawing.Size(75, 23);
      this.btnAppend.TabIndex = 0;
      this.btnAppend.Text = "Append";
      this.btnAppend.UseVisualStyleBackColor = true;
      this.btnAppend.Click += new System.EventHandler(this.btnAppend_Click);
      // 
      // textBox1
      // 
      this.textBox1.AllowDrop = true;
      this.textBox1.Location = new System.Drawing.Point(24, 13);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new System.Drawing.Size(405, 431);
      this.textBox1.TabIndex = 1;
      this.textBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextBox_DragDrop);
      this.textBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.TextBox_DragEnter);
      // 
      // btnEncrypt
      // 
      this.btnEncrypt.Location = new System.Drawing.Point(24, 470);
      this.btnEncrypt.Name = "btnEncrypt";
      this.btnEncrypt.Size = new System.Drawing.Size(75, 23);
      this.btnEncrypt.TabIndex = 2;
      this.btnEncrypt.Text = "Encrypt";
      this.btnEncrypt.UseVisualStyleBackColor = true;
      this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
      // 
      // btnLoad
      // 
      this.btnLoad.Location = new System.Drawing.Point(131, 470);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(75, 23);
      this.btnLoad.TabIndex = 3;
      this.btnLoad.Text = "Load";
      this.btnLoad.UseVisualStyleBackColor = true;
      this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
      // 
      // openFileDialog
      // 
      this.openFileDialog.Filter = "OxigenII Log Files (*.dat)|*.dat";
      this.openFileDialog.InitialDirectory = "C:\\Documents and Settings\\All Users\\OxigenData\\SettingsData";
      this.openFileDialog.Multiselect = true;
      // 
      // btnEncryptUsage
      // 
      this.btnEncryptUsage.Location = new System.Drawing.Point(245, 469);
      this.btnEncryptUsage.Name = "btnEncryptUsage";
      this.btnEncryptUsage.Size = new System.Drawing.Size(75, 23);
      this.btnEncryptUsage.TabIndex = 4;
      this.btnEncryptUsage.Text = "EncryptUsage";
      this.btnEncryptUsage.UseVisualStyleBackColor = true;
      this.btnEncryptUsage.Click += new System.EventHandler(this.btnEncryptUsage_Click);
      // 
      // Appender
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(459, 518);
      this.Controls.Add(this.btnEncryptUsage);
      this.Controls.Add(this.btnLoad);
      this.Controls.Add(this.btnEncrypt);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.btnAppend);
      this.Name = "Appender";
      this.Text = "Appender";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnAppend;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Button btnEncrypt;
    private System.Windows.Forms.Button btnLoad;
    private System.Windows.Forms.OpenFileDialog openFileDialog;
    private System.Windows.Forms.Button btnEncryptUsage;
  }
}