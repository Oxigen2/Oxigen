using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace OxigenService
{
  static class Program
  {
    static ServiceOperations _operations = null;

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      // Put main thread on hold for a second. This is for when the application restarts itself with elevated privileges
      // to perform a software update.
      // Immediately after this line there is a check to see if there is an Oxigen Service already running and exit if
      // there is. As the Oxigen Service starts a new instance of itself if elevated privileges are needed and THEN
      // the first instance exits, it is possible that the second instance will start the check before the first instance
      // exits and that will exit too.
      // Sleeping the thread for 1 sec doesn't guarantee that the first instance won't exit in time but
      // the chances of both instances exiting are minuscule as the first instance starts the second just right before
      // it exits.
      System.Threading.Thread.Sleep(1000);
      
      // make sure no other OxigenService processes are running
      Process process = Process.GetCurrentProcess();
      string processName = process.ProcessName;

      Process[] processes = Process.GetProcessesByName(processName);

      if (processes.Length > 1)
        return;

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

      System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
      System.Threading.Thread.CurrentThread.CurrentCulture = ci;
      System.Threading.Thread.CurrentThread.CurrentUICulture = ci; 

      _operations = new ServiceOperations();

      _operations.Start();

      Application.Run();
    }

    static void Application_ApplicationExit(object sender, EventArgs e)
    {
      if (_operations != null)
        _operations.Dispose();
    }
  }
}
