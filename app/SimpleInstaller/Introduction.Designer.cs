namespace SimpleInstaller
{
  partial class Introduction
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
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.btnNext = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(23, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(289, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "This will install the Oxigen Screen Saver on your computer.";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(23, 40);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(329, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Please make sure that your Screen Saver preview window is closed.";
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(23, 64);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(372, 30);
      this.label3.TabIndex = 2;
      this.label3.Text = "IMPORTANT: You will need the .NET Framework 3.5, Adobe Flash 10 and Apple QuickTi" +
          "me 7 to run this software.";
      // 
      // btnNext
      // 
      this.btnNext.Location = new System.Drawing.Point(370, 117);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(75, 23);
      this.btnNext.TabIndex = 3;
      this.btnNext.Text = "Next";
      this.btnNext.UseVisualStyleBackColor = true;
      this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
      // 
      // Introduction
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(457, 152);
      this.Controls.Add(this.btnNext);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.Name = "Introduction";
      this.Text = "Welcome to Oxigen Screen Saver setup";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Closing);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button btnNext;
  }
}