using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using OxigenIIAdvertising.ServerConnectAttempt;

namespace Setup
{
  public partial class UninstallProgressForm : OxigenForm
  {
    const int MF_BYPOSITION = 0x400;

    [DllImport("User32")]
    private static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);

    [DllImport("User32")]
    private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

    [DllImport("User32")]
    private static extern int GetMenuItemCount(IntPtr hWnd);

    public UninstallProgressForm()
    {
      InitializeComponent();
      progressBar.Maximum = 100;
    }

    private void UninstallProgressForm_Load(object sender, EventArgs e)
    {
      IntPtr hMenu = GetSystemMenu(this.Handle, false);
      int menuItemCount = GetMenuItemCount(hMenu);
      RemoveMenu(hMenu, menuItemCount - 1, MF_BYPOSITION);      
    }

    private void UninstallProgressForm_Shown(object sender, EventArgs e)
    {
      string userGUID = null;
      string machineGUID = null;
      string binaryPath = null;
      string dataSettingsPath = null;

      Application.DoEvents();

      if (!SetupHelper.GetValuesFromRegistry(ref userGUID, ref machineGUID, ref dataSettingsPath, ref binaryPath))
      {
        MessageBox.Show("Cannot proceed with installation. Registry entries are corrupted.\r\nPlease run the installer again in repair mode.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Application.Exit();
        return;
      }

      progressBar.Value = 3;

      Setup.UserManagementServicesLive.SimpleErrorWrapper wrapper = SetupHelper.SendUninstallInfo(userGUID, machineGUID);

      if (wrapper.ErrorStatus != Setup.UserManagementServicesLive.ErrorStatus1.Success)
      {
        if (wrapper.ErrorCode == "CONN")
          MessageBox.Show("Oxigen needs an active internet connection to uninstall. Please ensure that you are connected to the Internet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        else
          MessageBox.Show(wrapper.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        Application.Exit();
        return;
      }

      progressBar.Value = 10;

      ScreenSaver.SetScreenSaverActive(0);
      SetupHelper.KillApplications();

      progressBar.Value = 70;

      SetupHelper.ShowMessage(lblProgress, "Removing binaries...");

      SetupHelper.UninstallMSI(this);

      progressBar.Value = 90;

      SetupHelper.ShowMessage(lblProgress, "Cleaning up...");

      SetupHelper.RemoveAllFiles(binaryPath + "bin", SetupHelper.GetSystemDirectory(), dataSettingsPath);

      progressBar.Value = 100;

      SetupHelper.OpenForm<UninstallComplete>(this);
    }
  }
}
