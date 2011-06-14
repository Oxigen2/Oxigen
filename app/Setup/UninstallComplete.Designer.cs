namespace Setup
{
  partial class UninstallComplete
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
      this.btnExit = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // btnExit
      // 
      this.btnExit.Location = new System.Drawing.Point(422, 315);
      this.btnExit.Name = "btnExit";
      this.btnExit.Size = new System.Drawing.Size(75, 23);
      this.btnExit.TabIndex = 48;
      this.btnExit.Text = "Exit";
      this.btnExit.UseVisualStyleBackColor = true;
      this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 118);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(279, 13);
      this.label2.TabIndex = 47;
      this.label2.Text = "Oxigen has been successfully removed on your computer.";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(13, 82);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(111, 13);
      this.label1.TabIndex = 46;
      this.label1.Text = "Uninstall complete";
      // 
      // UninstallComplete
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(524, 350);
      this.Controls.Add(this.btnExit);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Name = "UninstallComplete";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UninstallComplete_Closed);
      this.Shown += new System.EventHandler(this.Form_Shown);
      this.Controls.SetChildIndex(this.pictureBox1, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.btnExit, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnExit;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
  }
}