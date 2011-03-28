namespace LibraryTester
{
  partial class ServerCycleConnect
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
      this.btnAttempt = new System.Windows.Forms.Button();
      this.lblFailedOnes = new System.Windows.Forms.Label();
      this.lblSuccessfulLink = new System.Windows.Forms.Label();
      this.btnAttemptExisting = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // btnAttempt
      // 
      this.btnAttempt.Location = new System.Drawing.Point(70, 83);
      this.btnAttempt.Name = "btnAttempt";
      this.btnAttempt.Size = new System.Drawing.Size(98, 23);
      this.btnAttempt.TabIndex = 0;
      this.btnAttempt.Text = "Attempt Now";
      this.btnAttempt.UseVisualStyleBackColor = true;
      this.btnAttempt.Click += new System.EventHandler(this.btnAttempt_Click);
      // 
      // lblFailedOnes
      // 
      this.lblFailedOnes.AutoSize = true;
      this.lblFailedOnes.Location = new System.Drawing.Point(25, 21);
      this.lblFailedOnes.Name = "lblFailedOnes";
      this.lblFailedOnes.Size = new System.Drawing.Size(63, 13);
      this.lblFailedOnes.TabIndex = 1;
      this.lblFailedOnes.Text = "Failed Ones";
      // 
      // lblSuccessfulLink
      // 
      this.lblSuccessfulLink.AutoSize = true;
      this.lblSuccessfulLink.Location = new System.Drawing.Point(25, 45);
      this.lblSuccessfulLink.Name = "lblSuccessfulLink";
      this.lblSuccessfulLink.Size = new System.Drawing.Size(82, 13);
      this.lblSuccessfulLink.TabIndex = 2;
      this.lblSuccessfulLink.Text = "Successful Link";
      // 
      // btnAttemptExisting
      // 
      this.btnAttemptExisting.Cursor = System.Windows.Forms.Cursors.Default;
      this.btnAttemptExisting.Location = new System.Drawing.Point(62, 115);
      this.btnAttemptExisting.Name = "btnAttemptExisting";
      this.btnAttemptExisting.Size = new System.Drawing.Size(113, 23);
      this.btnAttemptExisting.TabIndex = 3;
      this.btnAttemptExisting.Text = "Attempt an Existing one";
      this.btnAttemptExisting.UseVisualStyleBackColor = true;
      this.btnAttemptExisting.Click += new System.EventHandler(this.btnAttemptExisting_Click);
      // 
      // ServerCycleConnect
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(235, 150);
      this.Controls.Add(this.btnAttemptExisting);
      this.Controls.Add(this.lblSuccessfulLink);
      this.Controls.Add(this.lblFailedOnes);
      this.Controls.Add(this.btnAttempt);
      this.Name = "ServerCycleConnect";
      this.Text = "Server Cycle Connect";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnAttempt;
    private System.Windows.Forms.Label lblFailedOnes;
    private System.Windows.Forms.Label lblSuccessfulLink;
    private System.Windows.Forms.Button btnAttemptExisting;
  }
}