namespace Setup
{
    partial class InstallationProgressForm
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
          this.lblProgress = new System.Windows.Forms.Label();
          this.progressBar = new System.Windows.Forms.ProgressBar();
          ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
          this.SuspendLayout();
          // 
          // label1
          // 
          this.label1.AutoSize = true;
          this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.label1.Location = new System.Drawing.Point(13, 82);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(122, 13);
          this.label1.TabIndex = 1;
          this.label1.Text = "Installation Progress";
          // 
          // label2
          // 
          this.label2.AutoSize = true;
          this.label2.Location = new System.Drawing.Point(12, 118);
          this.label2.Name = "label2";
          this.label2.Size = new System.Drawing.Size(116, 13);
          this.label2.TabIndex = 7;
          this.label2.Tag = "";
          this.label2.Text = "Oxigen is now installing";
          // 
          // lblProgress
          // 
          this.lblProgress.AutoSize = true;
          this.lblProgress.Location = new System.Drawing.Point(12, 169);
          this.lblProgress.MaximumSize = new System.Drawing.Size(500, 0);
          this.lblProgress.MinimumSize = new System.Drawing.Size(500, 0);
          this.lblProgress.Name = "lblProgress";
          this.lblProgress.Size = new System.Drawing.Size(500, 13);
          this.lblProgress.TabIndex = 7;
          this.lblProgress.Tag = "";
          this.lblProgress.Text = "Preparing installation...";
          this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // progressBar
          // 
          this.progressBar.Location = new System.Drawing.Point(40, 206);
          this.progressBar.Name = "progressBar";
          this.progressBar.Size = new System.Drawing.Size(433, 23);
          this.progressBar.TabIndex = 8;
          // 
          // InstallationProgressForm
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(524, 350);
          this.Controls.Add(this.progressBar);
          this.Controls.Add(this.lblProgress);
          this.Controls.Add(this.label2);
          this.Controls.Add(this.label1);
          this.MinimizeBox = false;
          this.Name = "InstallationProgressForm";
          this.ShowInTaskbar = false;
          this.Load += new System.EventHandler(this.InstallationProgressForm_Load);
          this.Shown += new System.EventHandler(this.InstallationProgress_Shown);
          this.Controls.SetChildIndex(this.label1, 0);
          this.Controls.SetChildIndex(this.pictureBox1, 0);
          this.Controls.SetChildIndex(this.label2, 0);
          this.Controls.SetChildIndex(this.lblProgress, 0);
          this.Controls.SetChildIndex(this.progressBar, 0);
          ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}