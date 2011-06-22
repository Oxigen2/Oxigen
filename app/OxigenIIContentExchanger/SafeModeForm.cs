using System;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using InterCommunicationStructures;
using ServiceErrorReporting;
using System.IO;

namespace OxigenIIAdvertising.ContentExchanger
{
  public partial class SafeModeForm : Form
  {
    private bool _bError = false;
    private bool _bFinished = false;

    const int MF_BYPOSITION = 0x400;

    [DllImport("User32")]
    private static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);

    [DllImport("User32")]
    private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

    [DllImport("User32")]
    private static extern int GetMenuItemCount(IntPtr hWnd);

    public SafeModeForm()
    {
      InitializeComponent();
    }

    private void SafeModeForm_Load(object sender, EventArgs e)
    {
      IntPtr hMenu = GetSystemMenu(this.Handle, false);
      int menuItemCount = GetMenuItemCount(hMenu);
      RemoveMenu(hMenu, menuItemCount - 1, MF_BYPOSITION);      
    }

    private void SafeModeForm_Shown(object sender, EventArgs e)
    {
      backgroundWorker.RunWorkerAsync();
    }

    private void Worker_DoWork(object sender, DoWorkEventArgs e)
    {
      System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
      System.Threading.Thread.CurrentThread.CurrentCulture = ci;
      System.Threading.Thread.CurrentThread.CurrentUICulture = ci; 

      try
      {
        backgroundWorker.ReportProgress(10);

        using (var webClient = new WebClient())
        {
            webClient.DownloadFile("http://assets.oxigen.net/data/ss_general_data.dat", System.Configuration.ConfigurationSettings.AppSettings["AppDataPath"] + "\\SettingsData\\ss_general_data.dat");
        }
      }
      catch
      {
        MessageBox.Show(OxigenIIAdvertising.ContentExchanger.Properties.Resources.SMGeneralError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        _bError = true;
        return;
      }
    }
  
    private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      progressBar.Value = e.ProgressPercentage;
    }

    private void Worker_Completed(object sender, RunWorkerCompletedEventArgs e)
    {
      _bFinished = true;

      if (!_bError)
        MessageBox.Show(OxigenIIAdvertising.ContentExchanger.Properties.Resources.SMCompleted, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

      Application.Exit();
    }

    private void SafeModeForm_Closing(object sender, FormClosingEventArgs e)
    {
      if (_bFinished)
        Application.Exit();
      else
        e.Cancel = true;
    }
  }
}
