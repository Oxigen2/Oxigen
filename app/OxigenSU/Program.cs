using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Principal;
using System.IO;
using System.Configuration;
using OxigenSU.DuplicateLibraries;

namespace OxigenSU
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
      // Put main thread on hold for a second. This is for when the application restarts itself with elevated privileges
      // to perform the software update.
      // immediately after this line there is a check to see if there is a software updater already running and exit if
      // there is. As the software updater starts a new instance of itself if elevated privileges are needed and THEN
      // the first instance exits, it is possible that the second instance will start the check before the first instance
      // exits and that will exit too.
      // Sleeping the thread for 1 sec doesn't guarantee that the first instance won't exit in time but
      // the chances of both instances exiting are minuscule as the first instance starts the second just right before
      // it exits.
      System.Threading.Thread.Sleep(1000);

      // make sure no other software updater is running
      Process process = Process.GetCurrentProcess();
      string processName = process.ProcessName;

      Process[] processes = Process.GetProcessesByName(processName);

      if (processes.Length > 1)
        return;

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      SSLValidator.OverrideValidation();

      System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
      System.Threading.Thread.CurrentThread.CurrentCulture = ci;
      System.Threading.Thread.CurrentThread.CurrentUICulture = ci;

      string appDataPath = System.Configuration.ConfigurationSettings.AppSettings["AppDataPath"];

      if (args.Length > 0)
      {
        // network access mode to provoke the client's firewall
        if (args[0] == "/n")
        {
          GeneralData generalData = GetGeneralData();
          User user = GetUser();

          if (generalData != null && user != null)
          {
            ResponsiveServerDeterminator.GetResponsiveURI
                  (ServerType.RelayLogs,
                  int.Parse(generalData.NoServers["relayChannelAssets"]),
                  int.Parse(generalData.Properties["serverTimeout"]),
                  user.GetMachineGUIDSuffix(),
                  generalData.Properties["primaryDomainName"],
                  generalData.Properties["secondaryDomainName"],
                  "UserDataMarshaller.svc");
          }

          Application.Exit();
          return;
        }

        // verbose mode: pop up form.
        if (args[0] == "/v")
        {
          AppDataSingleton.Instance.IsVerboseMode = true;
          VerboseModeForm form = new VerboseModeForm(appDataPath);
          Application.Run(form);               
        }
      }

      // This if will be true if user has restarted the application to gain Admin privileges.
      // Application does not need Admin privileges if it's only checking for new updates but only
      // if it has determined that they exist and needs to download them.
      if (HasAdminRights() && System.IO.Directory.Exists(appDataPath)
        && System.IO.File.Exists(appDataPath + "\\SettingsData\\components.dat"))
      {
        // update app.config with any new parameters
        UpdateConfig();

        if (CanDeserialize(appDataPath + "\\SettingsData\\components.dat"))
        {
          Application.Run(new ProgressForm());
          return;
        }
        else
          File.Delete(appDataPath + "\\SettingsData\\components.dat");
      }

      if (args.Length == 0)
      {
        // Code that will execute if user run app as non-admin.
        // it will check for updates and ask for elevated privileges.
        ComponentListRetriever clr = new ComponentListRetriever();
        clr.Retrieve();

        UpdatePrompter prompter = new UpdatePrompter(clr, appDataPath);
        prompter.PromptForUpdateIfExists();
      }
    }

    // update maximum received message size programmatically for older versions.
    private static void UpdateConfig()
    {
      Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

      ServiceModelSectionGroup sm = config.GetSectionGroup("system.serviceModel") as ServiceModelSectionGroup;

      sm.Bindings.BasicHttpBinding.Bindings["StreamedBindingUFM"].MaxReceivedMessageSize = 209715200;
      config.Save(ConfigurationSaveMode.Modified);

      ConfigurationManager.RefreshSection("system.serviceModel");
    }

    private static bool HasAdminRights()
    {
      WindowsPrincipal pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
      return pricipal.IsInRole(WindowsBuiltInRole.Administrator);
    }

    private static bool CanDeserialize(string path)
    {
      try
      {
        Serializer.DeserializeClearText(typeof(HashSet<InterCommunicationStructures.ComponentInfo>), path);
      }
      catch
      {
        return false;
      }

      return true;
    }

    private static GeneralData GetGeneralData()
    {
      GeneralData generalData = null;

      try
      {
        generalData = (GeneralData)Serializer.DeserializeNoLock(typeof(GeneralData), System.Configuration.ConfigurationSettings.AppSettings["AppDataPath"] + "SettingsData\\ss_general_data.dat", "password");
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
        user = (User)Serializer.DeserializeNoLock(typeof(User), System.Configuration.ConfigurationSettings.AppSettings["AppDataPath"] + "SettingsData\\UserSettings.dat", "password");
      }
      catch
      {
        // can't display interface, so ignore.
      }

      return user;
    }
  }
}
