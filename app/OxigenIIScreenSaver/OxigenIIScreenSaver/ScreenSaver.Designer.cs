using System;

namespace OxigenIIAdvertising.ScreenSaver
{
  partial class ScreenSaver
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Indicates whether the object has been disposed
    /// </summary>
    private bool _bDisposed = false;

    /// <summary>
    /// Cleans up resources used by OxigenIIAdvertising.ScreenSaver.ScreenSaver
    /// </summary>
    public void DisposeForms()
    {
      Dispose(true);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (!_bDisposed)
      {
        if (disposing)
        {
          _faderForm.Dispose();

          if (_pictureBoxA.Image != null)
            _pictureBoxA.Image.Dispose();

          if (_pictureBoxB.Image != null)
            _pictureBoxB.Image.Dispose();

          _pictureBoxA.Dispose();
          _pictureBoxB.Dispose();
          _webBrowserA.Dispose();
          _webBrowserB.Dispose();
          _flashPlayerA.Dispose();
          _flashPlayerB.Dispose();
          _videoPlayerA.Dispose();
          _videoPlayerB.Dispose();
          _quickTimePlayerA.Dispose();
          _quickTimePlayerB.Dispose();

          _noAssetsAnimatorPlayer.Dispose();

          if (components != null)
            components.Dispose();
        }

        _noAssetsAnimatorPlayer = null;
        _webBrowserA = null;
        _webBrowserB = null;
        _pictureBoxA = null;
        _pictureBoxB = null;
        _videoPlayerA = null;
        _videoPlayerB = null;
        _quickTimePlayerA = null;
        _quickTimePlayerB = null;

        base.Dispose(disposing);

        _bDisposed = true;
      }
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.SuspendLayout();
      // 
      // ScreenSaver
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Black;
      this.ClientSize = new System.Drawing.Size(860, 777);
      this.ForeColor = System.Drawing.Color.White;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "ScreenSaver";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ScreenSaver_Closed);
      this.HandleCreated += new EventHandler(ScreenSaver_HandleCreated);
      this.Load +=new EventHandler(ScreenSaver_Load);
      this.ResumeLayout(false);
    }

    #endregion

  }
}

