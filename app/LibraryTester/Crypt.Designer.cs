namespace LibraryTester
{
  partial class Crypt
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
      this.btnEncrypt = new System.Windows.Forms.Button();
      this.btnDecrypt = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // btnEncrypt
      // 
      this.btnEncrypt.Location = new System.Drawing.Point(21, 12);
      this.btnEncrypt.Name = "btnEncrypt";
      this.btnEncrypt.Size = new System.Drawing.Size(75, 23);
      this.btnEncrypt.TabIndex = 1;
      this.btnEncrypt.Text = "Encrypt";
      this.btnEncrypt.UseVisualStyleBackColor = true;
      this.btnEncrypt.Click += new System.EventHandler(this.button1_Click);
      // 
      // btnDecrypt
      // 
      this.btnDecrypt.Location = new System.Drawing.Point(122, 13);
      this.btnDecrypt.Name = "btnDecrypt";
      this.btnDecrypt.Size = new System.Drawing.Size(75, 23);
      this.btnDecrypt.TabIndex = 2;
      this.btnDecrypt.Text = "Decrypt";
      this.btnDecrypt.UseVisualStyleBackColor = true;
      this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
      // 
      // Crypt
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(226, 48);
      this.Controls.Add(this.btnDecrypt);
      this.Controls.Add(this.btnEncrypt);
      this.Name = "Crypt";
      this.Text = "Crypt";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnEncrypt;
    private System.Windows.Forms.Button btnDecrypt;
  }
}