namespace LibraryTester
{
  partial class TryWebService
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
      this.btnClick = new System.Windows.Forms.Button();
      this.serverTime = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnClick
      // 
      this.btnClick.Location = new System.Drawing.Point(113, 100);
      this.btnClick.Name = "btnClick";
      this.btnClick.Size = new System.Drawing.Size(76, 23);
      this.btnClick.TabIndex = 0;
      this.btnClick.Text = "Click";
      this.btnClick.UseVisualStyleBackColor = true;
      this.btnClick.Click += new System.EventHandler(this.btnClick_Click);
      // 
      // serverTime
      // 
      this.serverTime.AutoSize = true;
      this.serverTime.Location = new System.Drawing.Point(13, 13);
      this.serverTime.Name = "serverTime";
      this.serverTime.Size = new System.Drawing.Size(0, 13);
      this.serverTime.TabIndex = 1;
      // 
      // TryWebService
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(304, 135);
      this.Controls.Add(this.serverTime);
      this.Controls.Add(this.btnClick);
      this.Name = "TryWebService";
      this.Text = "TryWebService";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnClick;
    private System.Windows.Forms.Label serverTime;
  }
}