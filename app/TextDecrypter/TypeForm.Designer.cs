namespace TextDecrypter
{
  partial class TypeForm
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
      this.btnEncypt = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(33, 39);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(224, 20);
      this.textBox1.TabIndex = 0;
      // 
      // btnEncypt
      // 
      this.btnEncypt.Location = new System.Drawing.Point(263, 36);
      this.btnEncypt.Name = "btnEncypt";
      this.btnEncypt.Size = new System.Drawing.Size(126, 23);
      this.btnEncypt.TabIndex = 1;
      this.btnEncypt.Text = "Serialize and Encrypt";
      this.btnEncypt.UseVisualStyleBackColor = true;
      // 
      // TypeForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(401, 101);
      this.Controls.Add(this.btnEncypt);
      this.Controls.Add(this.textBox1);
      this.Name = "TypeForm";
      this.Text = "Type of:";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Button btnEncypt;
  }
}