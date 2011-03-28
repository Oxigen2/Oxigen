using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;
using System.Diagnostics;

namespace OxigenIIAdvertising.SSG
{
  public static class UserAccountControl
  {
    [DllImport("user32")]
    public static extern UInt32 SendMessage(IntPtr hWnd, UInt32 msg, UInt32 wParam, UInt32 lParam);

    internal const int BCM_FIRST = 0x1600;
    internal const int BCM_SETSHIELD = (BCM_FIRST + 0x000C);

    static internal bool IsVistaOrHigher()
    {
      return Environment.OSVersion.Version.Major < 6;
    }

    /// <summary>
    /// Checks if the process is elevated
    /// </summary>
    /// <returns>If is elevated</returns>
    static internal bool IsAdmin()
    {
      WindowsIdentity id = WindowsIdentity.GetCurrent();
      WindowsPrincipal p = new WindowsPrincipal(id);
      return p.IsInRole(WindowsBuiltInRole.Administrator);
    }

    /// <summary>
    /// Add a shield icon to a menu item
    /// </summary>
    /// <param name="b">The button</param>
    static internal void AddShieldToMenuItem(MenuItem mi)
    {
      SendMessage(mi.Handle, BCM_SETSHIELD, 0, 0xFFFFFFFF);
    }

    /// <summary>
    /// Restart the current process with administrator credentials
    /// </summary>
    internal static void RestartElevated()
    {
      ProcessStartInfo startInfo = new ProcessStartInfo();
      startInfo.UseShellExecute = true;
      startInfo.WorkingDirectory = Environment.CurrentDirectory;
      startInfo.FileName = Application.ExecutablePath;
      startInfo.Verb = "runas";
      try
      {
        Process p = Process.Start(startInfo);
      }
      catch (System.ComponentModel.Win32Exception ex)
      {
        return; //If cancelled, do nothing
      }

      Application.Exit();
    }
  }
}
