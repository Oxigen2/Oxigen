using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Setup.UserManagementServicesLive;

namespace Setup
{
  internal static class SilentInstaller
  {
    private const long _binaryRequiredSpace = 15728640;
    private const long _dataRequiredSpace = 104857600;

    /// <summary>
    /// Silently installs the application.
    /// </summary>
    internal static void Install()
    {
      if (!SetupHelper.HasAdminRights())
        return;

      if (SetupHelper.OlderOxigenExists())
      {
        Oxigen1Uninstaller uninstaller = new Oxigen1Uninstaller();
        OldOxigenUninstallReturnStatus status = uninstaller.Uninstall();

        if (status.Status != UninstallOlderSoftwareStatus.Success)
          return;
      }

      if (SetupHelper.OxigenExists())
        return;
  
      if (!SetupHelper.PrerequisitesMet())
        return;

      if (!File.Exists(Directory.GetCurrentDirectory() + "\\Oxigen.msi"))
        return;

      string userGUID = null;
      string machineGUID = null;

      CheckServerAndGetGUIDs(ref userGUID, ref machineGUID);

      if (string.IsNullOrEmpty(userGUID) || string.IsNullOrEmpty(machineGUID))
        return;

      AppDataSingleton.Instance.User.UserGUID = userGUID;
      AppDataSingleton.Instance.User.MachineGUID = machineGUID;

      // get installation paths
      if (!GetBinaryPath())
        return;

      if (!GetDataPath())
        return;

      if (!SetupHelper.IsSufficientSpace(AppDataSingleton.Instance.BinariesPath, _binaryRequiredSpace))
        return;

      if (!SetupHelper.IsSufficientSpace(AppDataSingleton.Instance.DataPath, _dataRequiredSpace))
        return;

      CalculateMaxDiskSpaceForAssets();

      if (AppDataSingleton.Instance.FileDetectedChannelSubscriptionsLocal.SubscriptionSet != null)
        AppDataSingleton.Instance.ChannelSubscriptionsToUpload.SubscriptionSet = SetupHelper.GetChannelSubscriptionsNetFromLocal(AppDataSingleton.Instance.FileDetectedChannelSubscriptionsLocal.SubscriptionSet);

      try
      {
        InstallMSI();

        if (!SetupHelper.OxigenExists())
          return;

        SetupHelper.DoPostMSIInstallSteps();
        SetupHelper.CopySetup();

        if (!SendDetails())
        {
          UninstallMSI();
          SetupHelper.RemoveAllFiles(AppDataSingleton.Instance.BinariesPath + "bin", SetupHelper.GetSystemDirectory(), AppDataSingleton.Instance.DataPath);
          return;
        }

        SetupHelper.SetRegistryForModifyUninstall();
      }
      catch
      {
        UninstallMSI();
        SetupHelper.RemoveAllFiles(AppDataSingleton.Instance.BinariesPath + "bin", SetupHelper.GetSystemDirectory(), AppDataSingleton.Instance.DataPath);
        return;
      }

      StartApps();
      SetScreensaver();
    }

    private static bool GetBinaryPath()
    {
      if (string.IsNullOrEmpty(AppDataSingleton.Instance.BinariesPath))
      {
        AppDataSingleton.Instance.BinariesPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Oxigen\\";
        return true;
      }

      if (!AppDataSingleton.Instance.BinariesPath.EndsWith("\\"))
        AppDataSingleton.Instance.BinariesPath += "\\";

      return SetupHelper.CheckDriveExists(AppDataSingleton.Instance.BinariesPath);
    }

    private static bool GetDataPath()
    {
      if (string.IsNullOrEmpty(AppDataSingleton.Instance.DataPath))
      {
        AppDataSingleton.Instance.DataPath = SetupHelper.GetProgramDataFolder() + "\\Oxigen\\";
        return true;
      }

      if (!AppDataSingleton.Instance.DataPath.EndsWith("\\"))
        AppDataSingleton.Instance.DataPath += "\\";

      return SetupHelper.CheckDriveExists(AppDataSingleton.Instance.DataPath);
    }

    private static void CalculateMaxDiskSpaceForAssets()
    {
      // available space for the Content Exchanger will be the free disk space 
      // of the disk the data will be stored minus 40MB.
      if (AppDataSingleton.Instance.User.AssetFolderSize != -1)
        return;

      DriveInfo di = new DriveInfo(AppDataSingleton.Instance.DataPath);
      long freeSpace = di.AvailableFreeSpace;
      AppDataSingleton.Instance.User.AssetFolderSize = freeSpace - 41943040L;
    }

    internal static void Uninstall()
    {
      string userGUID = null;
      string machineGUID = null;
      string dataSettingsPath = null;
      string binaryPath = null;

      if (!SetupHelper.GetValuesFromRegistry(ref userGUID, ref machineGUID, ref dataSettingsPath, ref binaryPath))
        return;

      SimpleErrorWrapper wrapper = SetupHelper.SendUninstallInfo(userGUID, machineGUID);

      if (wrapper.ErrorStatus != Setup.UserManagementServicesLive.ErrorStatus1.Success)
        return;

      ScreenSaver.SetScreenSaverActive(0);
      SetupHelper.KillApplications();
      UninstallMSI();
      SetupHelper.RemoveAllFiles(binaryPath + "bin", SetupHelper.GetSystemDirectory(), dataSettingsPath);
    }

    private static void CheckServerAndGetGUIDs(ref string userGUID, ref string machineGUID)
    {
      // Check MAC Address and assign a machine GUID to AppDataSingleton
      string macAddress = SetupHelper.GetMACAddress();

      string UMSUri = SetupHelper.GetResponsiveServer(ServerType.MasterGetConfig,
        int.Parse(AppDataSingleton.Instance.GeneralData.NoServers["masterConfig"]),
        SetupHelper.GetRandomLetter().ToString(),
        "UserManagementServices.svc");

      if (string.IsNullOrEmpty(UMSUri))
        return;

      UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer client = null;
      
      StringErrorWrapper wrapper = null;

      try
      {
        client = new UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer();
        client.Url = UMSUri;
        wrapper = client.CheckIfPCExistsReturnGUID(AppDataSingleton.Instance.Username, macAddress, "password");
      }
      catch (System.Net.WebException)
      {
        return;
      }
      finally
      {
        if (client != null)
        {
          try
          {
            client.Dispose();
          }
          catch
          {
            client.Abort();
          }
        }
      }

      if (wrapper.ErrorStatus != ErrorStatus1.Success)
        return;

      string[] parameters = wrapper.ReturnString.Split(new char[]{'|'});

      userGUID = parameters[0];
      machineGUID = parameters[1];
    }

    private static bool SendDetails()
    {
      // Check MAC Address and assign a machine GUID to AppDataSingleton
      string macAddress = SetupHelper.GetMACAddress();
      AppDataSingleton.Instance.SetupLogger.WriteTimestampedMessage("MAC Address: " + macAddress);
        string UMSUri = SetupHelper.GetResponsiveServer(ServerType.MasterGetConfig,
        int.Parse(AppDataSingleton.Instance.GeneralData.NoServers["masterConfig"]),
        AppDataSingleton.Instance.User.GetUserGUIDSuffix(),
        "UserManagementServices.svc");

      if (string.IsNullOrEmpty(UMSUri))
      {
          AppDataSingleton.Instance.SetupLogger.WriteTimestampedMessage("URI to connect to send details not found");
        return false;
      }
      else
          AppDataSingleton.Instance.SetupLogger.WriteTimestampedMessage("URI to connect to send details: " + UMSUri);

      UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer client = null;
      
      SimpleErrorWrapper wrapper = null;

      AppDataSingleton.Instance.SetupLogger.WriteTimestampedMessage("UserGUID: " + AppDataSingleton.Instance.User.UserGUID);
      AppDataSingleton.Instance.SetupLogger.WriteTimestampedMessage("MachineGUID: " + AppDataSingleton.Instance.User.MachineGUID);
        
      try
      {
        client = new UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer();
        client.Url = UMSUri;

        wrapper = client.SyncWithServerNoPersonalDetails(AppDataSingleton.Instance.User.UserGUID,
          AppDataSingleton.Instance.User.MachineGUID,
          AppDataSingleton.Instance.ChannelSubscriptionsToUpload,
          AppDataSingleton.Instance.User.SoftwareMajorVersionNumber,
          true,
          AppDataSingleton.Instance.User.SoftwareMinorVersionNumber,
          true,
          Environment.MachineName,
          macAddress,          
          "password");
      }
      catch (System.Net.WebException ex)
      {
         AppDataSingleton.Instance.SetupLogger.WriteError(ex);

        return false;
      }
      finally
      {
        if (client != null)
        {
          try
          {
            client.Dispose();
          }
          catch
          {
            client.Abort();
          }
        }
      }

      if (wrapper.ErrorStatus != ErrorStatus1.Success) 
      {
          AppDataSingleton.Instance.SetupLogger.WriteTimestampedMessage("Send Details failed: message: " + wrapper.Message);
          return false;
      }
      AppDataSingleton.Instance.SetupLogger.WriteTimestampedMessage("Send Details: success");

      return  true;
    }

    private static void InstallMSI()
    {
      Process process = null;

      AppDataSingleton.Instance.SetupLogger.WriteTimestampedMessage("msiexec.exe /i \"Oxigen.msi\" /qn ALLUSERS=1 TARGETDIR=\"" + AppDataSingleton.Instance.BinariesPath + "\" DATAANDSETTINGS=\"" + AppDataSingleton.Instance.DataPath + "\" PCGUID=" + AppDataSingleton.Instance.User.MachineGUID + " USERGUID=" + AppDataSingleton.Instance.User.UserGUID + " REBOOT=ReallySuppress");

      ProcessStartInfo startInfo = new ProcessStartInfo("msiexec.exe", "/i \"Oxigen.msi\" /qn ALLUSERS=1 TARGETDIR=\"" + AppDataSingleton.Instance.BinariesPath + "\" DATAANDSETTINGS=\"" + AppDataSingleton.Instance.DataPath + "\" PCGUID=" + AppDataSingleton.Instance.User.MachineGUID + " USERGUID=" + AppDataSingleton.Instance.User.UserGUID + " REBOOT=ReallySuppress");

      try
      {
        process = Process.Start(startInfo);
      }
      catch
      {
        return;
      }

      process.WaitForExit();
    }    

    private static void UninstallMSI()
    {
      Process process = null;

      ProcessStartInfo startInfo = new ProcessStartInfo("msiexec.exe", "/x \"{05AB04BD-B62E-4A98-9DA0-9650699CAF8E}\" /qn REBOOT=ReallySuppress");

      try
      {
        process = Process.Start(startInfo);
      }
      catch
      {
        return;
      }

      process.WaitForExit();
    }

    private static void StartApps()
    {
      // Start Screensaver Guardian
      try
      {
        System.Diagnostics.Process.Start(AppDataSingleton.Instance.BinariesPath + "bin\\OxigenService.exe");
      }
      catch
      {
        // ignore starting of service.
      }

      // Start Screensaver Guardian
      try
      {
        System.Diagnostics.Process.Start(AppDataSingleton.Instance.BinariesPath + "bin\\OxigenSU.exe");
      }
      catch
      {
        // ignore starting of service.
      }

      // Start Screensaver Guardian
      try
      {
        System.Diagnostics.Process.Start(AppDataSingleton.Instance.BinariesPath + "bin\\OxigenLE.exe");
      }
      catch
      {
        // ignore starting of service.
      }

      // Start Screensaver Guardian
      try
      {
        System.Diagnostics.Process.Start(AppDataSingleton.Instance.BinariesPath + "bin\\OxigenCE.exe");
      }
      catch
      {
        // ignore starting of service.
      }
    }

    private static void SetScreensaver()
    {
      ScreenSaver.SetScreenSaver("Oxigen");
      ScreenSaver.SetScreenSaverActive(1);
      ScreenSaver.SetScreenSaverTimeout(180);
    }
  }
}
