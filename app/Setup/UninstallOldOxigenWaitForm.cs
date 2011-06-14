using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Setup.ClientLoggers;

namespace Setup
{
  public partial class UninstallOldOxigenWaitForm : SetupForm
  {
    const int MF_BYPOSITION = 0x400;

    [DllImport("User32")]
    private static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);

    [DllImport("User32")]
    private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

    [DllImport("User32")]
    private static extern int GetMenuItemCount(IntPtr hWnd);

    private void UninstallOldOxigenWaitForm_Load(object sender, EventArgs e)
    {
      IntPtr hMenu = GetSystemMenu(this.Handle, false);
      int menuItemCount = GetMenuItemCount(hMenu);
      RemoveMenu(hMenu, menuItemCount - 1, MF_BYPOSITION);
    }
    
    public UninstallOldOxigenWaitForm()
    {
      InitializeComponent();

      backgroundWorker.WorkerReportsProgress = true;
    }

    private void Form_Shown(object sender, EventArgs e)
    {
      ClientLogger logger = new PersistentClientLogger();
      logger.Log("2.2-UninstallOldOxigenProgress");

      Application.DoEvents();

      backgroundWorker.RunWorkerAsync();
    }

    private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
      System.Threading.Thread.CurrentThread.CurrentCulture = ci;
      System.Threading.Thread.CurrentThread.CurrentUICulture = ci; 

      Oxigen1Uninstaller uninstaller = new Oxigen1Uninstaller((BackgroundWorker)sender);

      e.Result = uninstaller.Uninstall();
    }

    private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      progressBar.Value = (int)e.ProgressPercentage;
    }

    private void backgroundWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
    {
      OldOxigenUninstallReturnStatus status = (OldOxigenUninstallReturnStatus)e.Result;

      AppDataSingleton.Instance.OldOxigenSystemModified = status.SystemModified;

      if (status.Status != UninstallOlderSoftwareStatus.Success)
      {
        progressBar.Value = 100;
        
        switch(status.Status)
        {
          case UninstallOlderSoftwareStatus.ErrorDeletingBinaries:
            MessageBox.Show("There was an error uninstalling Oxigen. Please state the following error code to helpdesk: 001", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            break;
          case UninstallOlderSoftwareStatus.ErrorExecutingSupport:
            MessageBox.Show("Could not terminate existing running Oxigen product. Please close all running Oxigen programs and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            break;
          case UninstallOlderSoftwareStatus.ErrorUninstallingMSI:
            MessageBox.Show("Could not uninstall existing Oxigen product. Existing Oxigen must be uninstalled before Setup can install Oxigen. Please locate the Oxigen MSI that you have used to install Oxigen and double-click it to uninstall.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            break;
        }

        Application.Exit();
        return;
      }

      MessageBox.Show("Existing Oxigen product has been uninstalled from your computer.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

      SetupHelper.OpenForm<PrerequisitesForm>(this);
    }
  }
}
