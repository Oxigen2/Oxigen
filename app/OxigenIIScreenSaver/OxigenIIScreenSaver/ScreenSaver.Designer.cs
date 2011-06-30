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
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        if (disposing && (_faderForm != null)) {
            _faderForm.Dispose();
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
            this.components = new System.ComponentModel.Container();
            this.workerTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // workerTimer
            // 
            this.workerTimer.Tick += new System.EventHandler(this.WorkerTimerTick);
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
            this.Load += new System.EventHandler(this.ScreenSaver_Load);
            this.HandleCreated += new System.EventHandler(this.ScreenSaver_HandleCreated);
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Timer workerTimer;

  }
}

