﻿namespace Setup
{
  partial class WelcomeForm
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
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnReject = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.TCs = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Welcome To Oxigen Setup";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(388, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Please ensure you are connected to the internet as this is required for installat" +
    "ion.";
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(422, 315);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 0;
            this.btnAccept.Text = "Accept >";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnReject
            // 
            this.btnReject.Location = new System.Drawing.Point(341, 315);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(75, 23);
            this.btnReject.TabIndex = 1;
            this.btnReject.Text = "Reject";
            this.btnReject.UseVisualStyleBackColor = true;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 151);
            this.label4.MaximumSize = new System.Drawing.Size(500, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(334, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "By installing Oxigen on your computer, you agree to abide by Oxigen\'s";
            // 
            // TCs
            // 
            this.TCs.AutoSize = true;
            this.TCs.BackColor = System.Drawing.SystemColors.Control;
            this.TCs.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TCs.LinkColor = System.Drawing.SystemColors.ControlText;
            this.TCs.Location = new System.Drawing.Point(345, 151);
            this.TCs.Name = "TCs";
            this.TCs.Size = new System.Drawing.Size(109, 13);
            this.TCs.TabIndex = 4;
            this.TCs.TabStop = true;
            this.TCs.Text = "Terms and Conditions";
            this.TCs.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.TCs_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(450, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = ".";
            // 
            // WelcomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(524, 350);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TCs);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnReject);
            this.Name = "WelcomeForm";
            this.Shown += new System.EventHandler(this.Form_Shown);
            this.Controls.SetChildIndex(this.btnReject, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.TCs, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnAccept, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnAccept;
    private System.Windows.Forms.Button btnReject;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.LinkLabel TCs;
    private System.Windows.Forms.Label label3;
  }
}
