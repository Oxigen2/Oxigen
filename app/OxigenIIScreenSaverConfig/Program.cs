using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace OxigenIIScreenSaverConfig
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
      // make sure no other log exchanger is running
      Process process = Process.GetCurrentProcess();
      string processName = process.ProcessName;

      Process[] logExchangerProcesses = Process.GetProcessesByName(processName);

      if (logExchangerProcesses.Length > 1)
        return;

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      System.Globalization.CultureInfo systemCultureInfo = System.Globalization.CultureInfo.CurrentCulture;

      System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
      System.Threading.Thread.CurrentThread.CurrentCulture = ci;
      System.Threading.Thread.CurrentThread.CurrentUICulture = ci;

      Application.Run(new ScreenSaverConfigForm(systemCultureInfo));
    }
  }
}
