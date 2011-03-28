using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using InstallCustomSteps.DuplicateLibrary;
using System.Management;
using ServiceErrorReporting;
using System.ServiceModel;

namespace InstallCustomSteps
{
  class Program
  {
    internal static string GetSystemDirectory()
    {
      StringBuilder path = new StringBuilder(260);
      SHGetSpecialFolderPath(IntPtr.Zero, path, 0x0029, false);
      return path.ToString();
    }

    [System.Runtime.InteropServices.DllImport("shell32.dll")]
    private static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner,
       [System.Runtime.InteropServices.Out] StringBuilder lpszPath, int nFolder, bool fCreate);

    private static Logger logger = null;

    static void Main(string[] args)
    {
      if (args.Length != 2)
      {
        Console.WriteLine("Two installation paths needed: Number of given installation paths: " + args.Length);
        Console.WriteLine("Paths:");

        foreach (string arg in args)
          Console.WriteLine(arg);

        Console.ReadLine();
        return;
      }

      string binariesPath = args[0].Replace("{001}", "") + "bin\\";
      string dataPath = args[1].Replace("{001}", "") + "data\\";

      Console.WriteLine("Binaries path:" + binariesPath);
      Console.WriteLine("Data path:" + dataPath);

      logger = new Logger("InstallCustomSteps", dataPath + "SettingsData\\OxigenDebug.txt", LoggingMode.Release);

      string systemFolder = GetSystemDirectory() + "\\";

      try
      {
        UpdateOneConfigFileWithPaths(systemFolder + "Oxigen.scr.config", dataPath, binariesPath, "");
        UpdateOneConfigFileWithPaths(binariesPath + "OxigenLE.exe.config", dataPath, binariesPath, "");
        UpdateOneConfigFileWithPaths(binariesPath + "OxigenCE.exe.config", dataPath, binariesPath, "");
        UpdateOneConfigFileWithPaths(binariesPath + "ScreenSaverConfig.exe.config", dataPath, binariesPath, "");
        UpdateOneConfigFileWithPaths(binariesPath + "OxigenService.exe.config", dataPath, binariesPath, "");
        UpdateOneConfigFileWithPaths(binariesPath + "OxigenTray.exe.config", dataPath, binariesPath, systemFolder);
        UpdateOneConfigFileWithPaths(binariesPath + "OxigenSU.exe.config", dataPath, binariesPath, systemFolder);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
        throw ex;
      }

      if (!File.Exists("Setup.ini"))
        return;

      string[][] subscriptions= GetSubscriptions();

      if (subscriptions == null)
        return;

      // register PC and upload subscriptions
      string macAddress = GetMACAddress();

      GeneralData gd = (GeneralData)Serializer.Deserialize(typeof(GeneralData), dataPath + "SettingsData\\ss_general_data.dat", "password");
      User user = (User)Serializer.Deserialize(typeof(User), dataPath + "SettingsData\\UserSettings.dat", "password");
      int noUMSServers = int.Parse(gd.NoServers["masterConfig"]);
      int serverTimeout = int.Parse(gd.Properties["serverTimeout"]);

      string UMSUri = ResponsiveServerDeterminator.GetResponsiveURI(ServerType.MasterGetConfig, noUMSServers, serverTimeout, user.GetUserGUIDSuffix(),
        gd.Properties["primaryDomainName"], gd.Properties["secondaryDomainName"], "UserManagementServices.svc");

      if (string.IsNullOrEmpty(UMSUri))
        return;

      UserManagementServicesNonStreamerClient client = null;
      StringErrorWrapper sw = null;

      try
      {
        client = new UserManagementServicesNonStreamerClient();
        client.Endpoint.Address = new System.ServiceModel.EndpointAddress(UMSUri);
        sw = client.AddSubscriptionsAndNewPC(user.UserGUID, macAddress, Environment.MachineName, gd.SoftwareMajorVersionNumber, gd.SoftwareMinorVersionNumber,
          subscriptions, "password");
      }
      catch (Exception ex)
      {
        logger.WriteError(ex);
        return;
      }

      if (sw.ErrorStatus != ErrorStatus.Success)
      {
        logger.WriteError(sw.ErrorStatus + " " + sw.Message);
        return;
      }

      user.MachineGUID = sw.ReturnString;

      Serializer.Serialize(user, dataPath + "SettingsData\\UserSettings.dat", "password");
    }

    private static void UpdateOneConfigFileWithPaths(string configFilePath, string selectedDataPath,
      string selectedBinariesPath, string systemFolder)
    {
      string xmlFile = File.ReadAllText(configFilePath);

      int part1Length = xmlFile.LastIndexOf("<appSettings>") + "<appSettings>".Length;

      string part1 = xmlFile.Substring(0, part1Length);

      int part2Length = xmlFile.Length - xmlFile.LastIndexOf("</appSettings>");

      string part2 = xmlFile.Substring(xmlFile.LastIndexOf("</appSettings>"), part2Length);

      StringBuilder newConfig = new StringBuilder(part1);

      newConfig.Append("<add key=\"AppDataPath\" value=\"");
      newConfig.Append(selectedDataPath);
      newConfig.Append("\"/>");
      newConfig.Append("<add key=\"BinariesPath\" value=\"");
      newConfig.Append(selectedBinariesPath);
      newConfig.Append("\"/>");

      if (systemFolder != "")
      {
        newConfig.Append("<add key=\"WindowsSystemPath\" value=\"");
        newConfig.Append(systemFolder);
        newConfig.Append("\"/>");
      }

      newConfig.Append(part2);

      File.WriteAllText(configFilePath, newConfig.ToString(), Encoding.ASCII);
    }

    private static string GetMACAddress()
    {
      ManagementObjectSearcher query = null;
      ManagementObjectCollection queryCollection = null;

      try
      {
        query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled='TRUE'");

        queryCollection = query.Get();

        foreach (ManagementObject mo in queryCollection)
        {
          if (mo["MacAddress"] != null)
            return (mo["MacAddress"]).ToString();
        }
      }
      catch (Exception ex)
      {
        return ex.ToString();
      }

      return String.Empty;
    }

    private static string[][] GetSubscriptions()
    {
      string setupString = File.ReadAllText("Setup.ini");

      if (string.IsNullOrEmpty(setupString))
        return null;

      string[] setupParameters = setupString.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

      int length = setupParameters.Length;

      // no subscriptions for this PC
      if (length == 0)
        return null;

      string[][] subscriptions = new string[length][];

      for (int i = 0; i < length; i++)
      {
        string[] subscriptionProperties = setupParameters[i].Split(new string[] { ",," }, StringSplitOptions.RemoveEmptyEntries);

        subscriptions[i] = subscriptionProperties;
      }

      return subscriptions;
    }   
  }
}
