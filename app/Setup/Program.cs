using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Setup
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
      Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
      System.Threading.Thread.CurrentThread.CurrentCulture = ci;
      System.Threading.Thread.CurrentThread.CurrentUICulture = ci; 

      // is another Setup instance running; if so, exit this one
      Process process = Process.GetCurrentProcess();
      string processName = process.ProcessName;

      Process[] setupProcesses = Process.GetProcessesByName(processName);

      if (setupProcesses.Length > 1)
        return;

      if (!string.IsNullOrEmpty(System.Configuration.ConfigurationSettings.AppSettings["debugMode"]))
        AppDataSingleton.Instance.DebugMode = true;

      SetupForm form;

      if (File.Exists("Setup.ini"))
        ParseINI();

      if (args.Length > 0)
      {
        if (args[0] == "/x")
        {
          SilentInstaller.Uninstall();
          return;
        }

        if (args[0] == "/m")
        {
          SilentMerger.Merge(args);
          return;
        }
      }

      if (File.Exists("UserSettings.ini"))
      {
        if (!ParseUserSettings())
          return;
      }

      if (!AppDataSingleton.Instance.SilentMode)
      {
        if (args.Length == 0)
          form = new WelcomeForm();
        else
        {
          switch (args[0])
          {
            case "/u":
              form = new UninstallPromptForm();
              break;

            default:
              form = new WelcomeForm();
              break;
          }
        }

        form.Show();
        Application.Run();
      }
      else
        SilentInstaller.Install();
    }

    static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
    {
      AppDataSingleton.Instance.ExitPromptSuppressed = true;
      ErrorForm errorForm = new ErrorForm(e.Exception.ToString());
      errorForm.Show();
    }
    
    private static void ParseINI()
    {     
      string setupString = File.ReadAllText("Setup.ini");

      if (string.IsNullOrEmpty(setupString))
        return;

      string[] setupParameters = setupString.Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);

      int length = setupParameters.Length;

      // no subscriptions for this PC
      if (length == 0)
        return;

      List<Setup.DuplicateLibrary.ChannelSubscription> channelSubscriptions = new List<Setup.DuplicateLibrary.ChannelSubscription>();

      for(int i = 0; i < length; i++)
      {
        int channelID;
        int channelWeightingUnnormalised;

        string[] subscriptionProperties = setupParameters[i].Split(new string[] { ",," }, StringSplitOptions.RemoveEmptyEntries);

        Setup.DuplicateLibrary.ChannelSubscription cs = new Setup.DuplicateLibrary.ChannelSubscription();

        if (int.TryParse(subscriptionProperties[0], out channelID))
        {
          cs.ChannelID = channelID;
          cs.ChannelGUID = subscriptionProperties[1];
          cs.ChannelName = subscriptionProperties[2];

          if (int.TryParse(subscriptionProperties[3], out channelWeightingUnnormalised))
          {
            cs.ChannelWeightingUnnormalised = channelWeightingUnnormalised;

            channelSubscriptions.Add(cs);
          }
        }
      }

      if (channelSubscriptions.Count > 0)
      {
        Setup.DuplicateLibrary.ChannelSubscription[] channelSubscriptionsLocal = channelSubscriptions.ToArray();

        AppDataSingleton.Instance.FileDetectedChannelSubscriptionsNet.SubscriptionSet = SetupHelper.GetChannelSubscriptionsNetFromLocal(channelSubscriptionsLocal);
        AppDataSingleton.Instance.FileDetectedChannelSubscriptionsLocal.SubscriptionSet = channelSubscriptionsLocal;
      }
    }

    private static bool ParseUserSettings()
    {
      string userSettings = File.ReadAllText("UserSettings.ini");

      if (string.IsNullOrEmpty(userSettings))
        return false;

      string[] parameters = userSettings.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

      Dictionary<string, object> dict = GetParams(parameters);

      if (dict == null)
        return false;

      AppDataSingleton.Instance.SilentMode = true;
      AppDataSingleton.Instance.Username = (string)dict["User"];
      AppDataSingleton.Instance.User.CanUpdate = (bool)dict["CanUpdate"];
      AppDataSingleton.Instance.User.DefaultDisplayDuration = (int)dict["DefaultDisplayDuration"];
      AppDataSingleton.Instance.User.AssetFolderSize = (long)dict["MaxSizeDataFolder"];
      AppDataSingleton.Instance.BinariesPath = (string)dict["BinariesInstallationPath"];
      AppDataSingleton.Instance.DataPath = (string)dict["DataInstallationPath"];
      AppDataSingleton.Instance.User.TimeUntilProgramToRunMinutes = (int)dict["AutoLogoffTimeout"];
      AppDataSingleton.Instance.User.ProgramToRun = (string)dict["AutoLogoffPath"];

      return true;
    }

    private static Dictionary<string, object> GetParams(string[] parameters)
    {
      Dictionary<string, object> dict = new Dictionary<string, object>();

      foreach (string param in parameters)
      {
        string[] pair = param.Split(new char[] { '=' }, 2);
        
        string pair0 = pair[0].Trim();
        string pair1 = pair[1].Trim();

        if (!string.IsNullOrEmpty(pair0))
        {
          switch (pair0)
          {
            case "User":
              if (string.IsNullOrEmpty(pair1))
                return null;
              dict.Add(pair0, pair1);
              break;
            case "MaxSizeDataFolder":
              long size;
              if (!long.TryParse(pair1, out size))
                size = -1L;
              dict.Add(pair0, size);
              break;
            case "CanUpdate":
              bool bCanUpdate = false;
              if (pair1.ToLower() == "true")
                bCanUpdate = true;
              dict.Add(pair0, bCanUpdate);
              break;
            case "DefaultDisplayDuration":
              int defaultDisplayDuration;
              if (!int.TryParse(pair1, out defaultDisplayDuration))
                defaultDisplayDuration = 5;
              dict.Add(pair0, defaultDisplayDuration);
              break;
            case "AutoLogoffTimeout":
              int timeout;
              if (!int.TryParse(pair1, out timeout))
                timeout = -1;
              dict.Add(pair0, timeout);
              break;
            case "AutoLogoffPath":
              string path = null;
              if (!string.IsNullOrEmpty(pair1))
                path = pair1;
              dict.Add(pair0, path);
              break;
            case "BinariesInstallationPath":
              string binariesPath = null;
              if (!string.IsNullOrEmpty(pair1))
              {
                if (!pair1.EndsWith("\\"))
                  pair1 += "\\";

                binariesPath = pair1;
              }
              dict.Add(pair0, binariesPath);
              break;
            case "DataInstallationPath":
              string datePath = null;
              if (!string.IsNullOrEmpty(pair1))
              {
                if (!pair1.EndsWith("\\"))
                  pair1 += "\\";

                datePath = pair1;
              }
              dict.Add(pair0, datePath);
              break;
          }
        }    
      }

      if (dict.Count < 8)
        return null;

      return dict;
    }
  }
}
