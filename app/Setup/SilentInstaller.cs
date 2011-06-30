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
      AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 1");

      if (!SetupHelper.HasAdminRights())
        return;

      AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 2");

      if (SetupHelper.OlderOxigenExists())
      {
        Oxigen1Uninstaller uninstaller = new Oxigen1Uninstaller();
        OldOxigenUninstallReturnStatus status = uninstaller.Uninstall();

        if (status.Status != UninstallOlderSoftwareStatus.Success)
          return;
      }

      AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 3");

      if (SetupHelper.OxigenExists())
        return;

      AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 4");

      if (!SetupHelper.PrerequisitesMet())
        return;

      AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 5");

      if (!File.Exists(Directory.GetCurrentDirectory() + "\\Oxigen.msi"))
        return;

      AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 6");
        
      string userGUID = GetUserGUIDByUsername();

      AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 7");

      if (string.IsNullOrEmpty(userGUID))
        return;

      AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 8");

      string machineGUID = System.Guid.NewGuid().ToString().ToUpper() + "_" + SetupHelper.GetRandomLetter();

      AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 9");

      AppDataSingleton.Instance.User.UserGUID = userGUID;
      AppDataSingleton.Instance.User.MachineGUID = machineGUID;

      // get installation paths
      if (!GetBinaryPath())
        return;

      AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 10");

      if (!GetDataPath())
        return;

      AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 11");

      if (!SetupHelper.IsSufficientSpace(AppDataSingleton.Instance.BinariesPath, _binaryRequiredSpace))
        return;

      AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 12");

      if (!SetupHelper.IsSufficientSpace(AppDataSingleton.Instance.DataPath, _dataRequiredSpace))
        return;

      AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 13");

      CalculateMaxDiskSpaceForAssets();

      AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 14");

      if (AppDataSingleton.Instance.FileDetectedChannelSubscriptionsLocal.SubscriptionSet != null) {
        AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 14 - 1");
        AppDataSingleton.Instance.ChannelSubscriptionsToUpload.SubscriptionSet = SetupHelper.GetChannelSubscriptionsNetFromLocal(AppDataSingleton.Instance.FileDetectedChannelSubscriptionsLocal.SubscriptionSet);
        AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 14 - 2");
      }

      AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 15");

      try
      {
        AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 16");

        if (!InstallMSI())
          return;

        AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 17");

        SetupHelper.DoPostMSIInstallSteps();

        AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 18");

        SetupHelper.CopySetup();

        AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 19");

        if (!SendDetails())
        {
          AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 20");

          UninstallMSI();

          AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 21");

          SetupHelper.RemoveAllFiles(AppDataSingleton.Instance.BinariesPath + "bin", SetupHelper.GetSystemDirectory(), AppDataSingleton.Instance.DataPath);

          AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 22");

          return;
        }

        AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 23");

        SetupHelper.SetRegistryForModifyUninstall();

        AppDataSingleton.Instance.SetupLogger.WriteMessage("Silent Install 24");
      }
      catch (Exception ex)
      {
        AppDataSingleton.Instance.SetupLogger.WriteMessage(ex.ToString());

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
        AppDataSingleton.Instance.DataPath = SetupHelper.GetDefaultDataFolder() + "\\Oxigen\\";
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

    private static string GetUserGUIDByUsername()
    {

      
      StringErrorWrapper wrapper = null;

      try
      {
          using (var client = new UserDataManagementClient())
          {
              wrapper = client.GetUserGUIDByUsername(AppDataSingleton.Instance.Username, "password");
          }
      }
      catch (System.Net.WebException)
      {
        AppDataSingleton.Instance.SetupLogger.WriteMessage("GetUserGUIDByUsername 8");

        return null;
      }
      
      if (wrapper.ErrorStatus != ErrorStatus1.Success) {
        AppDataSingleton.Instance.SetupLogger.WriteMessage(wrapper.Message);
        return null;
      }
        
      return wrapper.ReturnString;
    }

    private static bool SendDetails()
    {
      SimpleErrorWrapper wrapper = null;
        
      try
      {
          using (var client = new UserDataManagementClient())
          {
              wrapper = client.SyncWithServerNoPersonalDetails(AppDataSingleton.Instance.User.UserGUID,
                                                               AppDataSingleton.Instance.User.MachineGUID,
                                                               AppDataSingleton.Instance.ChannelSubscriptionsToUpload,
                                                               AppDataSingleton.Instance.User.SoftwareMajorVersionNumber,
                                                               true,
                                                               AppDataSingleton.Instance.User.SoftwareMinorVersionNumber,
                                                               true,
                                                               Environment.MachineName,
                                                               null,
                                                               "password");
          }
      }
      catch (System.Net.WebException ex)
      {
         AppDataSingleton.Instance.SetupLogger.WriteError(ex);

        return false;
      }

      if (wrapper.ErrorStatus != ErrorStatus1.Success) 
      {
          AppDataSingleton.Instance.SetupLogger.WriteTimestampedMessage("Send Details failed: message: " + wrapper.Message);
          return false;
      }

      AppDataSingleton.Instance.SetupLogger.WriteTimestampedMessage("Send Details: success");

      return  true;
    }

    private static bool InstallMSI()
    {
      Process process = null;

      ProcessStartInfo startInfo = new ProcessStartInfo("msiexec.exe", "/i \"Oxigen.msi\" /qn ALLUSERS=1 TARGETDIR=\"" + AppDataSingleton.Instance.BinariesPath + "\" DATAANDSETTINGS=\"" + AppDataSingleton.Instance.DataPath + "\" PCGUID=" + AppDataSingleton.Instance.User.MachineGUID + " USERGUID=" + AppDataSingleton.Instance.User.UserGUID + " REBOOT=ReallySuppress");

      try
      {
        process = Process.Start(startInfo);
      }
      catch
      {
        return false;
      }

      process.WaitForExit();

      if (process.ExitCode != 0)
          return false;

      return true;
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
