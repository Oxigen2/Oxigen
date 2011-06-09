using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using OxigenIIAdvertising.LoggerInfo;
using OxigenIIAdvertising.UserSettings;
using OxigenIIAdvertising.AppData;
using ProxyClientBaseLib;

namespace OxigenIIAdvertising.LogExchanger
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

      SSLValidator.OverrideValidation();

      System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
      System.Threading.Thread.CurrentThread.CurrentCulture = ci;
      System.Threading.Thread.CurrentThread.CurrentUICulture = ci; 

      if (args.Length > 0 && args[0] == "/n")
      {
        GeneralData generalData = GetGeneralData();
        User user = GetUser();

        Logger logger = new Logger("LogExchanger", ConfigurationSettings.AppSettings["AppDataPath"] + "SettingsData\\OxigenDebugLE.txt");

        if (generalData != null && user != null)
        {
          // simply try to access the network to provoke any firewall the target machine has, then exit application
          // connect to relay
          ServerConnectAttempt.ResponsiveServerDeterminator.GetResponsiveURI
            (ServerConnectAttempt.ServerType.RelayLogs,
            int.Parse(generalData.NoServers["relayLog"]),
            int.Parse(generalData.Properties["serverTimeout"]),
            user.GetMachineGUIDSuffix(),
            generalData.Properties["primaryDomainName"],
            generalData.Properties["secondaryDomainName"],
            "UserDataMarshaller.svc", logger);
        }

        Application.Exit();
        return;
      }
      else
      {
        Exchanger exchanger = new Exchanger();
        exchanger.Execute();
      }
    }

    private static GeneralData GetGeneralData()
    {
      GeneralData generalData = null;

      try
      {
        generalData = (GeneralData)XMLSerializer.Serializer.DeserializeNoLock(typeof(GeneralData), System.Configuration.ConfigurationSettings.AppSettings["AppDataPath"] + "SettingsData\\ss_general_data.dat", "password");
      }
      catch
      {
        // can't display interface, so ignore.
      }

      return generalData;
    }

    private static User GetUser()
    {
      User user = null;

      try
      {
        user = (User)XMLSerializer.Serializer.DeserializeNoLock(typeof(User), System.Configuration.ConfigurationSettings.AppSettings["AppDataPath"] + "SettingsData\\UserSettings.dat", "password");
      }
      catch
      {
        // can't display interface, so ignore.
      }

      return user;
    }
  }
}
