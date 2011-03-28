using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.IO;
using Setup.Properties;
using System.Diagnostics;
using OxigenIIAdvertising.LoggerInfo;

namespace Setup
{
  internal class Oxigen1Uninstaller
  {
    private bool _bSystemModified = false;
    private System.ComponentModel.BackgroundWorker _backgroundWorker = null;

    [System.Runtime.InteropServices.DllImport("msi.dll", SetLastError = true)]
    static extern Int32 MsiGetProductInfo(string product, string property, [System.Runtime.InteropServices.Out] StringBuilder valueBuf, ref Int32 len);

    Logger _logger = new Logger("Old Oxigen Uninstaller", Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Debug.txt", LoggingMode.Release);

    // Background thread uninstall constructor
    public Oxigen1Uninstaller(System.ComponentModel.BackgroundWorker backgroundWorker)
    {
      _backgroundWorker = backgroundWorker;
    }

    // Uninstall on main thread constructor
    public Oxigen1Uninstaller() { }

    public bool SystemModified
    {
      get { return _bSystemModified; }
      set { _bSystemModified = value; }
    }

    internal OldOxigenUninstallReturnStatus Uninstall()
    {
      string productCode = null;
      string olderOxigenInstallSource = null;

      UninstallOlderSoftwareStatus status = GetProductCode(ref productCode, ref olderOxigenInstallSource);

      _logger.WriteTimestampedMessage("Has product code been retrieved? UninstallOlderSoftwareStatus: " + status.ToString());

      if (status != UninstallOlderSoftwareStatus.Success)
        return new OldOxigenUninstallReturnStatus(status, _bSystemModified);

      _logger.WriteTimestampedMessage("Existing product code found: " + productCode);

      status = RemoveProduct(productCode, olderOxigenInstallSource);

      return new OldOxigenUninstallReturnStatus(status, _bSystemModified);
    }

    private UninstallOlderSoftwareStatus RemoveProduct(string productCode, string olderOxigenInstallSource)
    {
      string installPath = null;
      UninstallOlderSoftwareStatus status = RunSupportFiles();

      _logger.WriteTimestampedMessage("Have support files run? UninstallOlderSoftwareStatus: " + status.ToString());

      if (_backgroundWorker != null)
        _backgroundWorker.ReportProgress(5);

      if (status != UninstallOlderSoftwareStatus.Success)
        return status;

      object installPathObj = GenericRegistryAccess.GetRegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Oxigen", "path");

      if (installPathObj != null)
      {
        installPath = (string)installPathObj;
        _logger.WriteTimestampedMessage(@"HKEY_LOCAL_MACHINE\SOFTWARE\Oxigen\path = " + installPath);
      }
      else
        _logger.WriteTimestampedMessage(@"HKEY_LOCAL_MACHINE\SOFTWARE\Oxigen\path not found");

      status = UninstallMSI(productCode);

      _logger.WriteTimestampedMessage("UninstallMSI return status: " + status);

      if (_backgroundWorker != null)
        _backgroundWorker.ReportProgress(80);

      _bSystemModified = true;

      if (status != UninstallOlderSoftwareStatus.Success)
        return status;

      // delete install directory
      if (installPathObj != null && Directory.Exists(installPath))
      {
        _logger.WriteTimestampedMessage("Deleting directory " + installPath);

        try
        {
          if (Directory.Exists(installPath))
            Directory.Delete(installPath, true);
        }
        catch (Exception ex)
        {
          _logger.WriteTimestampedMessage(ex.ToString());

          return UninstallOlderSoftwareStatus.ErrorDeletingBinaries;
        }
      }

      // delete temp install directory that Older Oxigen normally leaves
      if (olderOxigenInstallSource != null && Directory.Exists(olderOxigenInstallSource))
      {
        try
        {
          if (Directory.Exists(olderOxigenInstallSource))
            Directory.Delete(olderOxigenInstallSource, true);
        }
        catch (Exception ex)
        {
          _logger.WriteTimestampedMessage(ex.ToString());

          return UninstallOlderSoftwareStatus.ErrorDeletingTempInstallFiles;
        }
      }

      GenericRegistryAccess.DeleteRegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Oxigen", "path");
      GenericRegistryAccess.DeleteRegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Oxigen", "path");

      if (_backgroundWorker != null)
        _backgroundWorker.ReportProgress(100);

      return UninstallOlderSoftwareStatus.Success;
    }

    private UninstallOlderSoftwareStatus UninstallMSI(string productCode)
    {
      Process process = null;

      ProcessStartInfo startInfo = new ProcessStartInfo("msiexec.exe", "/x \"" + productCode + "\" /qn REBOOT=ReallySuppress");

      try
      {
        process = Process.Start(startInfo);
      }
      catch (Exception ex)
      {
        _logger.WriteTimestampedMessage(ex.ToString());
        return UninstallOlderSoftwareStatus.ErrorUninstallingMSI;
      }

      process.WaitForExit();

      return UninstallOlderSoftwareStatus.Success;
    }

    private UninstallOlderSoftwareStatus RunSupportFiles()
    {
      try
      {
        string currentDir = Directory.GetCurrentDirectory();

        if (!Directory.Exists(currentDir + "\\Temp"))
          Directory.CreateDirectory(currentDir + "\\Temp");

        File.WriteAllBytes(currentDir + "\\Temp\\KillTimer.exe", Resources.KillTimr);
        File.WriteAllBytes(currentDir + "\\Temp\\KillOption.exe", Resources.KillOption);
        File.WriteAllBytes(currentDir + "\\Temp\\KillPanl.exe", Resources.KillPanl);
        File.WriteAllBytes(currentDir + "\\Temp\\KillTray.exe", Resources.KillTray);

        try
        {
          RunProcessAndWaitForExit(currentDir + "\\Temp\\KillTimer.exe");
          RunProcessAndWaitForExit(currentDir + "\\Temp\\KillOption.exe");
          RunProcessAndWaitForExit(currentDir + "\\Temp\\KillPanl.exe");
          RunProcessAndWaitForExit(currentDir + "\\Temp\\KillTray.exe");
        }
        catch
        {
          return UninstallOlderSoftwareStatus.ErrorExecutingSupport;
        }

        Directory.Delete(currentDir + "\\Temp", true);
      }
      catch (Exception ex)
      {
        _logger.WriteTimestampedMessage(ex.ToString());

        // ignore
      }

      return UninstallOlderSoftwareStatus.Success;
    }

    private bool RunProcessAndWaitForExit(string fileName)
    {
      Process process = null;

      ProcessStartInfo startInfo = new ProcessStartInfo(fileName);

      try
      {
        process = Process.Start(startInfo);
      }
      catch
      {
        return false;
      }

      process.WaitForExit();

      return true;
    }

    private UninstallOlderSoftwareStatus GetProductCode(ref string productCode, ref string installSource)
    {
      RegistryKey uninstallKey = GenericRegistryAccess.GetRegistryKey(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");

      if (uninstallKey == null)
        return UninstallOlderSoftwareStatus.KeyNotFound;

      List<string> productCodes = GetProductCodes();

      string[] subkeyNames = uninstallKey.GetSubKeyNames();

      foreach (string keyName in subkeyNames)
      {
        if (Contains(keyName, productCodes))
        {
          _logger.WriteTimestampedMessage("Product code found: " + keyName);

          productCode = keyName;

          // get the install source as older Oxigen leaves stuff there when it's done uninstalling so
          // we need to clean those too
          RegistryKey productUninstallKey = GenericRegistryAccess.GetRegistryKey(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\" + productCode);

          object installSourceObj = productUninstallKey.GetValue("InstallSource");

          if (installSourceObj != null)
            installSource = (string)installSourceObj;

          return UninstallOlderSoftwareStatus.Success;
        }
      }

      if (productCode == null)
        _logger.WriteTimestampedMessage("No product code found");

      return UninstallOlderSoftwareStatus.Success;
    }

    private bool Contains(string stringToFind, List<string> list)
    {
      foreach (string str in list)
      {
        if (stringToFind == str)
          return true;
      }

      return false;
    }

    private List<string> GetProductCodes()
    {
      List<string> list = new List<string>();

      // v3.x
      list.Add("{320E9058-4526-47A6-B740-8AB03414D1F8}");
      list.Add("{97DE900E-4B0A-45E8-B33A-79EF806246D0}");
      list.Add("{26A70E60-0A3D-494B-9842-8EE5A6A1055F}");
      list.Add("{D93F3284-F92A-4519-A4E4-7D423972368A}");
      list.Add("{C6506C30-B2F6-4E19-B025-C39DFB169C5F}");
      list.Add("{A9B1CD4C-BF60-447D-A41F-65F12FBE04C5}");
      list.Add("{CFA364E8-6446-40B5-B05D-9FA71654A385}");
      list.Add("{CE53AB66-6B67-4BB7-B422-C9A7BC5AB5D0}");
      list.Add("{EB2D9DD7-2416-4B90-859A-6439E29C8D0F}");
      list.Add("{A7A003B0-0A2B-4A39-BBE7-1B381A058AF9}");
      list.Add("{7E502D25-9BC3-4C91-A4B3-A6E3B513990B}");
      list.Add("{4475FC2E-087E-4A94-9EF1-E8AD1F3F67C5}");

      // v4.x
      list.Add("{4DC52B8C-2973-48F1-909A-0984CF7F8BAE}");
      list.Add("{EC9F90B8-6DA4-4F81-8669-690DEC1F3BB5}");
      list.Add("{F2678016-574E-46EE-B64C-0A2B7D413934}");
      list.Add("{4B721A3D-6240-4491-AD91-FA8A5A1A1986}");

      // v5.x
      list.Add("{D6D532B2-22E1-43AA-B4B7-34D772314859}");

      return list;
    }
  }

  /// <summary>
  /// Enumeration of outcomes of the older product uninstall operation
  /// </summary>
  public enum UninstallOlderSoftwareStatus
  {
    /// <summary>
    /// Operation was successful
    /// </summary>
    Success,

    /// <summary>
    /// Error executing the support files that kill the older Oxigen.
    /// </summary>
    ErrorExecutingSupport,

    /// <summary>
    /// A critical key was not found
    /// </summary>
    KeyNotFound,

    /// <summary>
    /// There was an error uninstalling the older MSI
    /// </summary>
    ErrorUninstallingMSI,

    /// <summary>
    /// There was an error deleting the program folder
    /// </summary>
    ErrorDeletingBinaries,

    /// <summary>
    /// There was an error deleting the temp install files
    /// </summary>
    ErrorDeletingTempInstallFiles,
  }

  public struct ProcessStatus
  {
    private float _overallProgress;
    private float _taskProgress;
    private string _taskMessage;

    public float OverallProgress
    {
      get { return _overallProgress; }
      set { _overallProgress = value; }
    }

    public float TaskProgress
    {
      get { return _taskProgress; }
      set { _taskProgress = value; }
    }

    public string TaskMessage
    {
      get { return _taskMessage; }
      set { _taskMessage = value; }
    }

    public ProcessStatus(float taskProgress, float overallProgress, string taskMessage)
      : this()
    {
      _taskProgress = taskProgress;
      _overallProgress = overallProgress;
      _taskMessage = taskMessage;
    }
  }

  public struct OldOxigenUninstallReturnStatus
  {
    private UninstallOlderSoftwareStatus _status;
    private bool _bSystemModified;

    public UninstallOlderSoftwareStatus Status
    {
      get { return _status; }
      set { _status = value; }
    }

    public bool SystemModified
    {
      get { return _bSystemModified; }
      set { _bSystemModified = value; }
    }

    public OldOxigenUninstallReturnStatus(UninstallOlderSoftwareStatus status, bool bSystemModified)
    {
      _status = status;
      _bSystemModified = bSystemModified;
    }
  }
}
