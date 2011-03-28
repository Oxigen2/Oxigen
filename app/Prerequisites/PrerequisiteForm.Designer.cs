namespace Prerequisites
{
  partial class PrerequisiteForm
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
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.qtReq = new System.Windows.Forms.Label();
      this.wmpReq = new System.Windows.Forms.Label();
      this.flashReq = new System.Windows.Forms.Label();
      this.ramReq = new System.Windows.Forms.Label();
      this.btnBack = new System.Windows.Forms.Button();
      this.btnNext = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 55);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(388, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Below are the minimum Requirements needed for Oxigen to run on your computer";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(46, 97);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(144, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Apple Quicktime (v.7 or over)";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(46, 128);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(181, 13);
      this.label3.TabIndex = 2;
      this.label3.Text = "Windows Media Player (v.10 or over)";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(46, 156);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(98, 13);
      this.label4.TabIndex = 3;
      this.label4.Text = "Adobe Flash Player";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(46, 187);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(134, 13);
      this.label5.TabIndex = 4;
      this.label5.Text = "System RAM (1GB or over)";
      // 
      // qtReq
      // 
      this.qtReq.AutoSize = true;
      this.qtReq.Location = new System.Drawing.Point(266, 97);
      this.qtReq.Name = "qtReq";
      this.qtReq.Size = new System.Drawing.Size(69, 13);
      this.qtReq.TabIndex = 5;
      this.qtReq.Text = "Tick or Cross";
      // 
      // wmpReq
      // 
      this.wmpReq.AllowDrop = true;
      this.wmpReq.AutoSize = true;
      this.wmpReq.Location = new System.Drawing.Point(266, 128);
      this.wmpReq.Name = "wmpReq";
      this.wmpReq.Size = new System.Drawing.Size(69, 13);
      this.wmpReq.TabIndex = 5;
      this.wmpReq.Text = "Tick or Cross";
      // 
      // flashReq
      // 
      this.flashReq.AutoSize = true;
      this.flashReq.Location = new System.Drawing.Point(266, 156);
      this.flashReq.Name = "flashReq";
      this.flashReq.Size = new System.Drawing.Size(69, 13);
      this.flashReq.TabIndex = 5;
      this.flashReq.Text = "Tick or Cross";
      // 
      // ramReq
      // 
      this.ramReq.AutoSize = true;
      this.ramReq.Location = new System.Drawing.Point(266, 187);
      this.ramReq.Name = "ramReq";
      this.ramReq.Size = new System.Drawing.Size(69, 13);
      this.ramReq.TabIndex = 5;
      this.ramReq.Text = "Tick or Cross";
      // 
      // btnBack
      // 
      this.btnBack.Location = new System.Drawing.Point(329, 261);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(75, 23);
      this.btnBack.TabIndex = 6;
      this.btnBack.Text = "< Back";
      this.btnBack.UseVisualStyleBackColor = true;
      this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
      // 
      // btnNext
      // 
      this.btnNext.Location = new System.Drawing.Point(410, 261);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(75, 23);
      this.btnNext.TabIndex = 6;
      this.btnNext.Text = "Next >";
      this.btnNext.UseVisualStyleBackColor = true;
      this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(248, 261);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 6;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // PrerequisiteForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(497, 296);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnNext);
      this.Controls.Add(this.btnBack);
      this.Controls.Add(this.ramReq);
      this.Controls.Add(this.flashReq);
      this.Controls.Add(this.wmpReq);
      this.Controls.Add(this.qtReq);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Name = "PrerequisiteForm";
      this.Text = "Required Components";
      this.Load += new System.EventHandler(this.PrerequisiteForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label qtReq;
    private System.Windows.Forms.Label wmpReq;
    private System.Windows.Forms.Label flashReq;
    private System.Windows.Forms.Label ramReq;
    private System.Windows.Forms.Button btnBack;
    private System.Windows.Forms.Button btnNext;
    private System.Windows.Forms.Button btnCancel;
  }
}