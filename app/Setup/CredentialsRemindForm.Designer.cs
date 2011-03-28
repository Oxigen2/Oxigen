namespace Setup
{
  partial class CredentialsRemindForm
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
      this.btnRemind = new System.Windows.Forms.Button();
      this.btnOK = new System.Windows.Forms.Button();
      this.lblMessage = new System.Windows.Forms.Label();
      this.lblProgress = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnRemind
      // 
      this.btnRemind.Location = new System.Drawing.Point(237, 97);
      this.btnRemind.Name = "btnRemind";
      this.btnRemind.Size = new System.Drawing.Size(83, 23);
      this.btnRemind.TabIndex = 3;
      this.btnRemind.Text = "Reset";
      this.btnRemind.UseVisualStyleBackColor = true;
      this.btnRemind.Click += new System.EventHandler(this.btnReset_Click);
      // 
      // btnOK
      // 
      this.btnOK.Location = new System.Drawing.Point(121, 97);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(83, 23);
      this.btnOK.TabIndex = 2;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // lblMessage
      // 
      this.lblMessage.AutoSize = true;
      this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMessage.Location = new System.Drawing.Point(90, 22);
      this.lblMessage.MaximumSize = new System.Drawing.Size(430, 0);
      this.lblMessage.Name = "lblMessage";
      this.lblMessage.Size = new System.Drawing.Size(260, 13);
      this.lblMessage.TabIndex = 4;
      this.lblMessage.Text = "The password you have entered is incorrect.";
      // 
      // lblProgress
      // 
      this.lblProgress.AutoSize = true;
      this.lblProgress.Location = new System.Drawing.Point(24, 73);
      this.lblProgress.Name = "lblProgress";
      this.lblProgress.Size = new System.Drawing.Size(0, 13);
      this.lblProgress.TabIndex = 5;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(44, 50);
      this.label1.MaximumSize = new System.Drawing.Size(430, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(353, 13);
      this.label1.TabIndex = 4;
      this.label1.Text = "Please click OK to retry or Reset to have a new password emailed to you.";
      // 
      // CredentialsRemindForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(443, 132);
      this.Controls.Add(this.lblProgress);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.lblMessage);
      this.Controls.Add(this.btnRemind);
      this.Controls.Add(this.btnOK);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "CredentialsRemindForm";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Message";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnRemind;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Label lblMessage;
    private System.Windows.Forms.Label lblProgress;
    private System.Windows.Forms.Label label1;
  }
}