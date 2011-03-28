namespace OxigenIIAdvertising.ScreenSaver
{
  partial class PreviewScreenSaver
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
      this.pictureBox = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
      this.SuspendLayout();
      // 
      // pictureBox
      // 
      this.pictureBox.Image = global::OxigenIIAdvertising.ScreenSaver.Properties.Resources.Preview;
      this.pictureBox.Location = new System.Drawing.Point(13, 12);
      this.pictureBox.Name = "pictureBox";
      this.pictureBox.Size = new System.Drawing.Size(164, 124);
      this.pictureBox.TabIndex = 0;
      this.pictureBox.TabStop = false;
      // 
      // PreviewScreenSaver
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(189, 158);
      this.Controls.Add(this.pictureBox);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "PreviewScreenSaver";
      this.Load += new System.EventHandler(this.ScreenSaver_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox;
  }
}