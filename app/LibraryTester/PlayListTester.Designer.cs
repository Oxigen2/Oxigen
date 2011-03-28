namespace LibraryTester
{
  partial class PlayListTester
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
      this.button1 = new System.Windows.Forms.Button();
      this.txt = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(146, 421);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(144, 23);
      this.button1.TabIndex = 1;
      this.button1.Text = "Make Playlist";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // txt
      // 
      this.txt.Location = new System.Drawing.Point(33, 25);
      this.txt.Multiline = true;
      this.txt.Name = "txt";
      this.txt.ReadOnly = true;
      this.txt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txt.Size = new System.Drawing.Size(382, 376);
      this.txt.TabIndex = 0;
      this.txt.UseSystemPasswordChar = true;
      // 
      // PlayListTester
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(447, 466);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.txt);
      this.Name = "PlayListTester";
      this.Text = "PlayListTester";
      this.Load += new System.EventHandler(this.PlayListTester_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.TextBox txt;
  }
}