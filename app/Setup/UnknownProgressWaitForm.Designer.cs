namespace Setup
{
  partial class UnknownProgressWaitForm
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
    protected void InitializeComponent()
    {
      this.lblProgress = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.AutoSize = true;
      this.lblProgress.Location = new System.Drawing.Point(156, 44);
      this.lblProgress.MaximumSize = new System.Drawing.Size(400, 0);
      this.lblProgress.Name = "lblProgress";
      this.lblProgress.Size = new System.Drawing.Size(108, 13);
      this.lblProgress.TabIndex = 0;
      this.lblProgress.Text = "Checking your details";
      this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // UnknownProgressWaitForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(443, 113);
      this.ControlBox = false;
      this.Controls.Add(this.lblProgress);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "UnknownProgressWaitForm";
      this.ShowInTaskbar = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Oxigen Setup";
      this.Load += new System.EventHandler(this.UnknownProgressWaitForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    protected System.Windows.Forms.Label lblProgress;

  }
}