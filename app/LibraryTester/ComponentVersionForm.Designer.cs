namespace LibraryTester
{
  partial class ComponentVersionForm
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
      this.versionList = new System.Windows.Forms.DataGridView();
      this.filename = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.version = new System.Windows.Forms.DataGridViewTextBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.versionList)).BeginInit();
      this.SuspendLayout();
      // 
      // versionList
      // 
      this.versionList.AllowUserToDeleteRows = false;
      this.versionList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.versionList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.filename,
            this.version});
      this.versionList.Location = new System.Drawing.Point(12, 12);
      this.versionList.Name = "versionList";
      this.versionList.ReadOnly = true;
      this.versionList.Size = new System.Drawing.Size(467, 541);
      this.versionList.TabIndex = 0;
      // 
      // filename
      // 
      this.filename.HeaderText = "File";
      this.filename.MaxInputLength = 200;
      this.filename.Name = "filename";
      this.filename.ReadOnly = true;
      this.filename.Width = 300;
      // 
      // version
      // 
      this.version.HeaderText = "Version";
      this.version.MaxInputLength = 50;
      this.version.Name = "version";
      this.version.ReadOnly = true;
      this.version.Width = 130;
      // 
      // ComponentVersionForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(491, 577);
      this.Controls.Add(this.versionList);
      this.Name = "ComponentVersionForm";
      this.Text = "ComponentVersionForm";
      this.Load += new System.EventHandler(this.ComponentVersionForm_Load);
      ((System.ComponentModel.ISupportInitialize)(this.versionList)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView versionList;
    private System.Windows.Forms.DataGridViewTextBoxColumn filename;
    private System.Windows.Forms.DataGridViewTextBoxColumn version;
  }
}