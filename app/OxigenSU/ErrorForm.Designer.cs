namespace OxigenSU
{
  partial class ErrorForm
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
      this.btnNoSend = new System.Windows.Forms.Button();
      this.btnSend = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(22, 27);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(170, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "An error has occurred with Oxigen.";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(22, 55);
      this.label2.MaximumSize = new System.Drawing.Size(380, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(374, 26);
      this.label2.TabIndex = 0;
      this.label2.Text = "It would be helpful if you could forward the details of the error to us so that w" +
          "e can try to fix it.";
      // 
      // btnNoSend
      // 
      this.btnNoSend.Location = new System.Drawing.Point(96, 118);
      this.btnNoSend.Name = "btnNoSend";
      this.btnNoSend.Size = new System.Drawing.Size(75, 23);
      this.btnNoSend.TabIndex = 1;
      this.btnNoSend.Text = "Don\'t Send";
      this.btnNoSend.UseVisualStyleBackColor = true;
      this.btnNoSend.Click += new System.EventHandler(this.btnNoSend_Click);
      // 
      // btnSend
      // 
      this.btnSend.Location = new System.Drawing.Point(253, 118);
      this.btnSend.Name = "btnSend";
      this.btnSend.Size = new System.Drawing.Size(75, 23);
      this.btnSend.TabIndex = 1;
      this.btnSend.Text = "Send";
      this.btnSend.UseVisualStyleBackColor = true;
      this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
      // 
      // ErrorForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(424, 159);
      this.Controls.Add(this.btnSend);
      this.Controls.Add(this.btnNoSend);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Icon = OxigenSU.Properties.Resources.oxigen;
      this.Name = "ErrorForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Oxigen Error";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnNoSend;
    private System.Windows.Forms.Button btnSend;
  }
}