namespace OxigenIIAdvertising.ContentExchanger
{
  partial class VerboseRunForm
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
      this.progressBarOverall = new System.Windows.Forms.ProgressBar();
      this.progressBarCurrent = new System.Windows.Forms.ProgressBar();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.lblCurrentOperation = new System.Windows.Forms.Label();
      this.txtLastRunDate = new System.Windows.Forms.TextBox();
      this.txtLastRunStatus = new System.Windows.Forms.TextBox();
      this.txtContentLastDownloaded = new System.Windows.Forms.TextBox();
      this.txtOverallPC = new System.Windows.Forms.Label();
      this.btnStop = new System.Windows.Forms.Button();
      this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
      this.SuspendLayout();
      // 
      // progressBarOverall
      // 
      this.progressBarOverall.Location = new System.Drawing.Point(32, 36);
      this.progressBarOverall.Name = "progressBarOverall";
      this.progressBarOverall.Size = new System.Drawing.Size(246, 23);
      this.progressBarOverall.TabIndex = 0;
      // 
      // progressBarCurrent
      // 
      this.progressBarCurrent.Location = new System.Drawing.Point(32, 91);
      this.progressBarCurrent.Name = "progressBarCurrent";
      this.progressBarCurrent.Size = new System.Drawing.Size(246, 23);
      this.progressBarCurrent.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(68, 154);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(79, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Last Run Date:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(61, 187);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(86, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Last Run Status:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(14, 219);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(133, 13);
      this.label3.TabIndex = 2;
      this.label3.Text = "Content Last Downloaded:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(32, 20);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(84, 13);
      this.label4.TabIndex = 1;
      this.label4.Text = "Overall Progress";
      // 
      // lblCurrentOperation
      // 
      this.lblCurrentOperation.AutoSize = true;
      this.lblCurrentOperation.Location = new System.Drawing.Point(32, 75);
      this.lblCurrentOperation.Name = "lblCurrentOperation";
      this.lblCurrentOperation.Size = new System.Drawing.Size(70, 13);
      this.lblCurrentOperation.TabIndex = 1;
      this.lblCurrentOperation.Text = "Connecting...";
      // 
      // txtLastRunDate
      // 
      this.txtLastRunDate.Enabled = false;
      this.txtLastRunDate.Location = new System.Drawing.Point(160, 151);
      this.txtLastRunDate.Name = "txtLastRunDate";
      this.txtLastRunDate.ReadOnly = true;
      this.txtLastRunDate.Size = new System.Drawing.Size(118, 20);
      this.txtLastRunDate.TabIndex = 3;
      // 
      // txtLastRunStatus
      // 
      this.txtLastRunStatus.Enabled = false;
      this.txtLastRunStatus.Location = new System.Drawing.Point(160, 184);
      this.txtLastRunStatus.Name = "txtLastRunStatus";
      this.txtLastRunStatus.ReadOnly = true;
      this.txtLastRunStatus.Size = new System.Drawing.Size(118, 20);
      this.txtLastRunStatus.TabIndex = 3;
      // 
      // txtContentLastDownloaded
      // 
      this.txtContentLastDownloaded.Enabled = false;
      this.txtContentLastDownloaded.Location = new System.Drawing.Point(160, 216);
      this.txtContentLastDownloaded.Name = "txtContentLastDownloaded";
      this.txtContentLastDownloaded.ReadOnly = true;
      this.txtContentLastDownloaded.Size = new System.Drawing.Size(118, 20);
      this.txtContentLastDownloaded.TabIndex = 3;
      // 
      // txtOverallPC
      // 
      this.txtOverallPC.AutoSize = true;
      this.txtOverallPC.Location = new System.Drawing.Point(194, 62);
      this.txtOverallPC.Name = "txtOverallPC";
      this.txtOverallPC.Size = new System.Drawing.Size(0, 13);
      this.txtOverallPC.TabIndex = 1;
      // 
      // btnStop
      // 
      this.btnStop.Location = new System.Drawing.Point(203, 253);
      this.btnStop.Name = "btnStop";
      this.btnStop.Size = new System.Drawing.Size(75, 23);
      this.btnStop.TabIndex = 4;
      this.btnStop.Text = "Cancel";
      this.btnStop.UseVisualStyleBackColor = true;
      this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
      // 
      // backgroundWorker
      // 
      this.backgroundWorker.WorkerReportsProgress = true;
      this.backgroundWorker.WorkerSupportsCancellation = true;
      this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
      this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
      this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
      // 
      // VerboseRunForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(313, 288);
      this.Controls.Add(this.btnStop);
      this.Controls.Add(this.txtContentLastDownloaded);
      this.Controls.Add(this.txtLastRunStatus);
      this.Controls.Add(this.txtLastRunDate);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.lblCurrentOperation);
      this.Controls.Add(this.txtOverallPC);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.progressBarCurrent);
      this.Controls.Add(this.progressBarOverall);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = global::OxigenIIAdvertising.ContentExchanger.Properties.Resources.oxigen;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "VerboseRunForm";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Oxigen Content Exchanger";
      this.Shown += new System.EventHandler(this.VerboseRunForm_Shown);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VerboseRunForm_Closing);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ProgressBar progressBarOverall;
    private System.Windows.Forms.ProgressBar progressBarCurrent;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label lblCurrentOperation;
    private System.Windows.Forms.TextBox txtLastRunDate;
    private System.Windows.Forms.TextBox txtLastRunStatus;
    private System.Windows.Forms.TextBox txtContentLastDownloaded;
    private System.Windows.Forms.Label txtOverallPC;
    private System.Windows.Forms.Button btnStop;
    private System.ComponentModel.BackgroundWorker backgroundWorker;
  }
}