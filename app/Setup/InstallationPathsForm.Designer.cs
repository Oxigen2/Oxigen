namespace Setup
{
  partial class InstallationPathsForm
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.label5 = new System.Windows.Forms.Label();
      this.btnBrowseBinaries = new System.Windows.Forms.Button();
      this.txtBinariesPath = new System.Windows.Forms.TextBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.label4 = new System.Windows.Forms.Label();
      this.btnBrowseData = new System.Windows.Forms.Button();
      this.txtDataPath = new System.Windows.Forms.TextBox();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnNext = new System.Windows.Forms.Button();
      this.folderBrowserBinaries = new System.Windows.Forms.FolderBrowserDialog();
      this.folderBrowserData = new System.Windows.Forms.FolderBrowserDialog();
      this.lblValidation = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(13, 82);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(129, 13);
      this.label1.TabIndex = 5;
      this.label1.Text = "Oxigen File Locations";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 118);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(238, 13);
      this.label2.TabIndex = 6;
      this.label2.Tag = "";
      this.label2.Text = "Setup will install Oxigen in the following locations.";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 142);
      this.label3.MaximumSize = new System.Drawing.Size(510, 0);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(468, 13);
      this.label3.TabIndex = 7;
      this.label3.Tag = "";
      this.label3.Text = "To install to different folders, either edit the paths below or clock Browse and " +
          "select another folder.";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.label5);
      this.groupBox1.Controls.Add(this.btnBrowseBinaries);
      this.groupBox1.Controls.Add(this.txtBinariesPath);
      this.groupBox1.Location = new System.Drawing.Point(15, 186);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(483, 85);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Oxigen Program";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(7, 25);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(82, 13);
      this.label5.TabIndex = 2;
      this.label5.Text = "15 MB required.";
      // 
      // btnBrowseBinaries
      // 
      this.btnBrowseBinaries.Location = new System.Drawing.Point(394, 51);
      this.btnBrowseBinaries.Name = "btnBrowseBinaries";
      this.btnBrowseBinaries.Size = new System.Drawing.Size(75, 23);
      this.btnBrowseBinaries.TabIndex = 1;
      this.btnBrowseBinaries.Text = "Browse...";
      this.btnBrowseBinaries.UseVisualStyleBackColor = true;
      this.btnBrowseBinaries.Click += new System.EventHandler(this.btnBrowseBinaries_Click);
      // 
      // txtBinariesPath
      // 
      this.txtBinariesPath.Location = new System.Drawing.Point(7, 51);
      this.txtBinariesPath.Name = "txtBinariesPath";
      this.txtBinariesPath.Size = new System.Drawing.Size(366, 20);
      this.txtBinariesPath.TabIndex = 0;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.label4);
      this.groupBox2.Controls.Add(this.btnBrowseData);
      this.groupBox2.Controls.Add(this.txtDataPath);
      this.groupBox2.Location = new System.Drawing.Point(16, 291);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(483, 86);
      this.groupBox2.TabIndex = 1;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Oxigen Data Files";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(6, 27);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(368, 13);
      this.label4.TabIndex = 2;
      this.label4.Text = "Location of Oxigen\'s user settings and content files. 100MB or more required.";
      // 
      // btnBrowseData
      // 
      this.btnBrowseData.Location = new System.Drawing.Point(393, 52);
      this.btnBrowseData.Name = "btnBrowseData";
      this.btnBrowseData.Size = new System.Drawing.Size(75, 23);
      this.btnBrowseData.TabIndex = 1;
      this.btnBrowseData.Text = "Browse...";
      this.btnBrowseData.UseVisualStyleBackColor = true;
      this.btnBrowseData.Click += new System.EventHandler(this.btnBrowseData_Click);
      // 
      // txtDataPath
      // 
      this.txtDataPath.Location = new System.Drawing.Point(6, 52);
      this.txtDataPath.Name = "txtDataPath";
      this.txtDataPath.Size = new System.Drawing.Size(366, 20);
      this.txtDataPath.TabIndex = 0;
      this.txtDataPath.TextChanged += new System.EventHandler(this.txtDataPath_TextChanged);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(341, 392);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // btnNext
      // 
      this.btnNext.Location = new System.Drawing.Point(422, 392);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(75, 23);
      this.btnNext.TabIndex = 2;
      this.btnNext.Text = "Next >";
      this.btnNext.UseVisualStyleBackColor = true;
      this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
      // 
      // lblValidation
      // 
      this.lblValidation.AutoSize = true;
      this.lblValidation.ForeColor = System.Drawing.Color.Red;
      this.lblValidation.Location = new System.Drawing.Point(13, 392);
      this.lblValidation.Name = "lblValidation";
      this.lblValidation.Size = new System.Drawing.Size(0, 13);
      this.lblValidation.TabIndex = 8;
      // 
      // InstallationPathsForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(524, 427);
      this.Controls.Add(this.lblValidation);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnNext);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.label3);
      this.Name = "InstallationPathsForm";
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.groupBox2, 0);
      this.Controls.SetChildIndex(this.groupBox1, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.pictureBox1, 0);
      this.Controls.SetChildIndex(this.btnNext, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.lblValidation, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button btnBrowseBinaries;
    private System.Windows.Forms.TextBox txtBinariesPath;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Button btnBrowseData;
    private System.Windows.Forms.TextBox txtDataPath;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnNext;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserBinaries;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserData;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label lblValidation;
  }
}
