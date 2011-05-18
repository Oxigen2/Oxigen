using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using OxigenIIAdvertising.ContentExchanger.Properties;
using OxigenIIAdvertising.AppData;
using System.IO;
using OxigenIIAdvertising.UserSettings;

namespace OxigenIIAdvertising.ContentExchanger
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
      // make sure no other content exchanger is running
      Process process = Process.GetCurrentProcess();
      string processName = process.ProcessName;

      Process[] contentExchangerProcesses = Process.GetProcessesByName(processName);

      if (contentExchangerProcesses.Length > 1)
        return;

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      AddCdnSubdomain();

      System.Globalization.CultureInfo systemCultureInfo = System.Globalization.CultureInfo.CurrentCulture;

      System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
      System.Threading.Thread.CurrentThread.CurrentCulture = ci;
      System.Threading.Thread.CurrentThread.CurrentUICulture = ci; 

      if (args.Length > 0)
      {
        if (args[0] == "/v")
        {
          Application.Run(new VerboseRunForm(systemCultureInfo));
          return;
        }

        if (args[0] == "/s")
        {
          Application.Run(new SafeModeForm());
          return;
        }
      }
      else
      {
        Exchanger exchanger = new Exchanger();

        ExchangeStatus status = exchanger.Execute();

        CELog log = new CELog(DateTime.Now, status.ContentDownloaded, status.ExitWithError);
        log.SaveLog();

        if (status.LowDiskSpace)
        {
          MessageBox.Show(Resources.LowDiskSpaceText, Resources.LowDiskSpaceHeader);

          return;
        }

        if (status.LowAssetSpace)
        {
          MessageBox.Show(Resources.LowAssetSpaceText, Resources.LowAssetSpaceHeader);

          return;
        }
      }
    }

    private static void AddCdnSubdomain() {
      Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
      config.AppSettings.Settings.Add("cdnSubdomain", "http://the-cdnsubdomain.com");

      // Save the changes in App.config file.
      config.Save(ConfigurationSaveMode.Modified);

      // Force a reload of a changed section.
      ConfigurationManager.RefreshSection("appSettings");
    }
  }
}
