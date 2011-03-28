namespace LibraryTester
{
  partial class LINQAggregator
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
      this.lbl = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // lbl
      // 
      this.lbl.AutoSize = true;
      this.lbl.Location = new System.Drawing.Point(13, 24);
      this.lbl.Name = "lbl";
      this.lbl.Size = new System.Drawing.Size(13, 13);
      this.lbl.TabIndex = 0;
      this.lbl.Text = "1";
      // 
      // LINQAggregator
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(262, 250);
      this.Controls.Add(this.lbl);
      this.Name = "LINQAggregator";
      this.Text = "LINQAggregator";
      this.Load += new System.EventHandler(this.LINQAggregator_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lbl;
  }
}