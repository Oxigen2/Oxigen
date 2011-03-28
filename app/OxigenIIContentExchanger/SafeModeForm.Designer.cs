namespace OxigenIIAdvertising.ContentExchanger
{
  partial class SafeModeForm
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
      this.progressBar = new System.Windows.Forms.ProgressBar();
      this.label1 = new System.Windows.Forms.Label();
      this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
      this.SuspendLayout();
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(37, 40);
      this.progressBar.Name = "progressBar";
      this.progressBar.Size = new System.Drawing.Size(323, 16);
      this.progressBar.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(37, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(198, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Missing settings are being downloaded...";
      // 
      // backgroundWorker
      // 
      this.backgroundWorker.WorkerReportsProgress = true;
      this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Worker_DoWork);
      this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Worker_Completed);
      this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Worker_ProgressChanged);
      // 
      // SafeModeForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(386, 68);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.progressBar);
      this.Icon = global::OxigenIIAdvertising.ContentExchanger.Properties.Resources.oxigen;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SafeModeForm";
      this.Text = "Downloading";
      this.Load += new System.EventHandler(this.SafeModeForm_Load);
      this.Shown += new System.EventHandler(this.SafeModeForm_Shown);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SafeModeForm_Closing);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ProgressBar progressBar;
    private System.Windows.Forms.Label label1;
    private System.ComponentModel.BackgroundWorker backgroundWorker;
  }
}