namespace TextDecrypter
{
  partial class EncryptedTextViewer
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
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btnEncrypt = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // textBox1
      // 
      this.textBox1.AllowDrop = true;
      this.textBox1.Location = new System.Drawing.Point(29, 47);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.textBox1.Size = new System.Drawing.Size(738, 652);
      this.textBox1.TabIndex = 0;
      this.textBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextBox_DragDrop);
      this.textBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.TextBox_DragEnter);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(26, 18);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(244, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Drag and drop an encrypted file to the area below.";
      // 
      // btnEncrypt
      // 
      this.btnEncrypt.Location = new System.Drawing.Point(328, 714);
      this.btnEncrypt.Name = "btnEncrypt";
      this.btnEncrypt.Size = new System.Drawing.Size(116, 23);
      this.btnEncrypt.TabIndex = 2;
      this.btnEncrypt.Text = "Serialize and Encrypt again";
      this.btnEncrypt.UseVisualStyleBackColor = true;
      this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
      // 
      // EncryptedTextViewer
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(796, 749);
      this.Controls.Add(this.btnEncrypt);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textBox1);
      this.Name = "EncryptedTextViewer";
      this.Text = "EncryptedTextViewer";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Closing);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnEncrypt;
  }
}