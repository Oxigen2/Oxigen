using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Setup.ClientLoggers;

namespace Setup
{
  public partial class InstallComplete : OxigenForm
  {
    const int MF_BYPOSITION = 0x400;

    [DllImport("User32")]
    private static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);

    [DllImport("User32")]
    private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

    [DllImport("User32")]
    private static extern int GetMenuItemCount(IntPtr hWnd);

    public InstallComplete()
    {
      InitializeComponent();
    }

    private void InstallComplete_Load(object sender, EventArgs e)
    {
      IntPtr hMenu = GetSystemMenu(this.Handle, false);
      int menuItemCount = GetMenuItemCount(hMenu);
      RemoveMenu(hMenu, menuItemCount - 1, MF_BYPOSITION);
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void InstallationComplete_Closed(object sender, FormClosedEventArgs e)
    {
      Process processCE = null;

      ProcessStartInfo startInfoCE = new ProcessStartInfo(AppDataSingleton.Instance.BinariesPath + "\\bin\\OxigenCE.exe", "/v");

      try
      {
        processCE = Process.Start(startInfoCE);
      }
      catch
      {
        // ignore
      }

      Application.Exit();
    }

    private void InstallationComplete_Shown(object sender, EventArgs e)
    {
      ClientLogger logger = new PersistentClientLogger();
      logger.Log("9-InstallationComplete");

      ScreenSaver.SetScreenSaverActive(0);

      if (!AppDataSingleton.Instance.Repair)
      {
        // provoke LE, SU
        Process processLE = null;

        ProcessStartInfo startInfoLE = new ProcessStartInfo(AppDataSingleton.Instance.BinariesPath + "\\bin\\OxigenLE.exe", "/n");

        try
        {
          processLE = Process.Start(startInfoLE);
        }
        catch
        {
          // ignore
        }
               
        Process processSU = null;

        ProcessStartInfo startInfoSU = new ProcessStartInfo(AppDataSingleton.Instance.BinariesPath + "\\bin\\OxigenSU.exe", "/n");

        try
        {
          processSU = Process.Start(startInfoSU);
        }
        catch
        {
          // suppress all errors
        }

        processLE.WaitForExit();
        processSU.WaitForExit();
      }

      try
      {
        GenericRegistryAccess.DeleteRegistryKey(RegistryBranch.HKLM_LOCAL_MACHINE__SOFTWARE_OxigenRef);
      }
      catch
      {
        // suppress all errors
      }

      ScreenSaver.SetScreenSaver("Oxigen");
      ScreenSaver.SetScreenSaverActive(1);
      ScreenSaver.SetScreenSaverTimeout(180);

      btnExit.Enabled = true;
    }
  }
}
