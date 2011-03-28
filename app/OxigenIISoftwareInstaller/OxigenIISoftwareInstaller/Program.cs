using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace OxigenIIAdvertising.SoftwareInstaller
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      // make sure no other software updater is running
      Process process = Process.GetCurrentProcess();
      string processName = process.ProcessName;

      Process[] softwareUpdaterProcesses = Process.GetProcessesByName(processName);

      if (softwareUpdaterProcesses.Length > 1)
        return;

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new Form1());
    }
  }
}
