using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using OxigenIIAdvertising.ContentExchanger.Properties;
using System.Threading;
using OxigenIIAdvertising.UserSettings;

namespace OxigenIIAdvertising.ContentExchanger
{
  public partial class VerboseRunForm : Form
  {
    const int MF_BYPOSITION = 0x400;

    [DllImport("User32")]
    private static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);

    [DllImport("User32")]
    private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

    [DllImport("User32")]
    private static extern int GetMenuItemCount(IntPtr hWnd);

    bool _bFinished = false;
    bool _bCancelled = false;

    System.Globalization.CultureInfo _systemCultureInfo;

    public VerboseRunForm(System.Globalization.CultureInfo systemCultureInfo)
    {
      InitializeComponent();

      _systemCultureInfo = systemCultureInfo;

      IntPtr hMenu = GetSystemMenu(this.Handle, false);
      int menuItemCount = GetMenuItemCount(hMenu);
      RemoveMenu(hMenu, menuItemCount - 1, MF_BYPOSITION);
    }

    private void VerboseRunForm_Shown(object sender, EventArgs e)
    {
      CELog log = new CELog();
      log.LoadLog();

      if (log.HasLog)
      {
        System.Globalization.CultureInfo englishCultureInfo = new System.Globalization.CultureInfo("en-GB");

        Thread.CurrentThread.CurrentCulture = _systemCultureInfo;
        txtLastRunDate.Text = log.LastRun.ToString();
        txtLastRunStatus.Text = log.LastStatus;

        // if content never downloaded or log file deleted, a new DateTime() will laways return a date of 1/1/0001
        if (log.LastDownloadedContent.Year != 1)
          txtContentLastDownloaded.Text = log.LastDownloadedContent.ToString();

        Thread.CurrentThread.CurrentCulture = englishCultureInfo;
      }

      Application.DoEvents();
      
      backgroundWorker.RunWorkerAsync();
    }    

    private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
      System.Threading.Thread.CurrentThread.CurrentCulture = ci;
      System.Threading.Thread.CurrentThread.CurrentUICulture = ci; 

      Exchanger exchanger = new Exchanger((BackgroundWorker)sender);

      e.Result = exchanger.Execute();      
    } 

    private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      var status = (ProcessStatus)e.UserState;

      int overallProgress = (int)status.OverallProgress;
      int taskProgress = (int)status.TaskProgress;

      if (!string.IsNullOrEmpty(status.TaskMessage))
        lblCurrentOperation.Text = status.TaskMessage;

      if (overallProgress != -1)
        progressBarOverall.Value = (int)status.OverallProgress;

      if (taskProgress != -1)
        progressBarCurrent.Value = (int)status.TaskProgress;
    }

    private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      ExchangeStatus status = (ExchangeStatus)e.Result;

      if (status.LowDiskSpace)
        MessageBox.Show(Resources.LowDiskSpaceText, Resources.LowDiskSpaceHeader);

      if (status.LowAssetSpace)
        MessageBox.Show(Resources.LowAssetSpaceText, Resources.LowAssetSpaceHeader);

      DateTime currentRun = DateTime.Now;

      txtLastRunDate.Text = currentRun.ToString();

      if (status.ContentDownloaded)
        txtContentLastDownloaded.Text = currentRun.ToString(); ;

      txtLastRunStatus.Text = status.ExitWithError ? "Failed" : "Succeeded";

      if (_bCancelled)
        lblCurrentOperation.Text = "Cancelled by user.";
      else
        lblCurrentOperation.Text = "Finished!";
      
      _bFinished = true;
      this.Close();
    }

    private void btnStop_Click(object sender, EventArgs e)
    {
      backgroundWorker.CancelAsync();
      _bCancelled = true;
      ((Button)sender).Enabled = false;
      ((Button)sender).Text = "Canceling...";
      Application.DoEvents();
    }

    private void VerboseRunForm_Closing(object sender, FormClosingEventArgs e)
    {
      if (_bFinished)
        Application.Exit();
      else
      {
        if (!backgroundWorker.CancellationPending)
        {
          Application.Exit();
          return;
        }

        e.Cancel = true;
      }
    }
  }
}
