using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using InterCommunicationStructures;
using System.IO;
using ServiceErrorReporting;
using System.ServiceProcess;
using System.Threading;

namespace OxigenSU
{
  public partial class ProgressForm : Form
  {
    const int MF_BYPOSITION = 0x400;

    [DllImport("User32")]
    private static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);

    [DllImport("User32")]
    private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

    [DllImport("User32")]
    private static extern int GetMenuItemCount(IntPtr hWnd);

    [System.Runtime.InteropServices.DllImport("shell32.dll")]
    private static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner,
       [System.Runtime.InteropServices.Out] StringBuilder lpszPath, int nFolder, bool fCreate);

    internal static string GetSystemDirectory()
    {
      StringBuilder path = new StringBuilder(260);
      SHGetSpecialFolderPath(IntPtr.Zero, path, 0x0029, false);
      return path.ToString();
    }

    string _appDataPath = System.Configuration.ConfigurationSettings.AppSettings["AppDataPath"];
    string _binariesPath = System.Configuration.ConfigurationSettings.AppSettings["BinariesPath"];
    string _systemDir = GetSystemDirectory() + "\\";
    string _userSettingsPath = System.Configuration.ConfigurationSettings.AppSettings["AppDataPath"] + "\\SettingsData\\UserSettings.dat";

    bool _bError = false;
      
    EventLog _log;

    int _progressBarVal = 0;
    int _step = 0;

    private bool _bCanClose = false;

    public ProgressForm()
    {
      _log = new EventLog();
      _log.Source = "OxigenSU";
      _log.Log = String.Empty;

      InitializeComponent();
    }

    private bool GetSettingsFiles(ref GeneralData generalData, ref User user)
    {
      for (int i = 0; i < 10; i++)
      {
        try
        {
          generalData = (GeneralData)Serializer.DeserializeNoLock(typeof(GeneralData), _appDataPath + "\\SettingsData\\ss_general_data.dat", "password");
        }
        catch (IOException)
        {
          Thread.Sleep(100);
        }
        catch
        {
          _log.WriteEntry("Cannot retrieve general settings.", EventLogEntryType.Error);
          return false;
        }
      }

      if (generalData == null)
      {
        _log.WriteEntry("Cannot retrieve general settings. File was possibly locked", EventLogEntryType.Error);
        return false;
      }

      for (int i = 0; i < 10; i++)
      {
        try
        {
          user = (User)Serializer.DeserializeNoLock(typeof(User), _appDataPath + "\\SettingsData\\UserSettings.dat", "password");
        }
        catch (IOException)
        {
          Thread.Sleep(100);
        }
        catch
        {
          _log.WriteEntry("Cannot retrieve user settings.", EventLogEntryType.Error);
          return false;
        }
      }

      if (user == null)
      {
        _log.WriteEntry("Cannot retrieve user settings. File was possibly locked", EventLogEntryType.Error);
        return false;
      }

      return true;
    }

    private void GetNewVersionNumber(GeneralData generalData, ref int updatedMajorVersionNumber, 
      ref int updatedMinorVersionNumber, ref string updatedVersionNumber)
    {
      updatedMajorVersionNumber = generalData.SoftwareMajorVersionNumber;
      updatedMinorVersionNumber = generalData.SoftwareMinorVersionNumber;

      updatedVersionNumber = String.Format("{0}.{1}", generalData.SoftwareMajorVersionNumber, generalData.SoftwareMinorVersionNumber);
    }

    private void Form_Closing(object sender, FormClosingEventArgs e)
    {
      // don't let this form close if process hasn't finished
      if (!_bCanClose)
        e.Cancel = true;
    }

    private void Form_Load(object sender, EventArgs e)
    {
      IntPtr hMenu = GetSystemMenu(this.Handle, false);
      int menuItemCount = GetMenuItemCount(hMenu);
      RemoveMenu(hMenu, menuItemCount - 1, MF_BYPOSITION);
    }

    private void Form_Shown(object sender, EventArgs e)
    {    
      backgroundWorker.RunWorkerAsync();
    }

    private bool GetValuesFromGeneralData(GeneralData generalData, ref int maxNoServersDownload, ref int maxNoServersRelay,
      ref int timeout, ref string primaryDomainName, ref string secondaryDomainName)
    {
      if (!int.TryParse(generalData.NoServers["download"], out maxNoServersDownload))
      {
        _log.WriteEntry("Cannot get maximum number of component list download servers.", EventLogEntryType.Error);
        return false;
      }

      if (!int.TryParse(generalData.NoServers["relayLog"], out maxNoServersRelay))
      {
        _log.WriteEntry("Cannot get maximum number of component list relay servers.", EventLogEntryType.Error);
        return false;
      }

      if (!int.TryParse(generalData.Properties["serverTimeout"], out timeout))
      {
        _log.WriteEntry("Cannot get timeout download servers.", EventLogEntryType.Error);
        return false;
      }

      if (!generalData.Properties.ContainsKey("primaryDomainName"))
      {
        _log.WriteEntry("Primary domain name not found.", EventLogEntryType.Error);
        return false;
      }
      else
        primaryDomainName = generalData.Properties["primaryDomainName"];

      if (!generalData.Properties.ContainsKey("secondaryDomainName"))
      {
        _log.WriteEntry("Secondary domain name not found.", EventLogEntryType.Error);
        return false;
      }
      else
        secondaryDomainName = generalData.Properties["secondaryDomainName"];

      return true;
    }

    private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
      System.Threading.Thread.CurrentThread.CurrentCulture = ci;
      System.Threading.Thread.CurrentThread.CurrentUICulture = ci; 

      string tempDir = _binariesPath + "Temp\\";
      string binTempDir = _binariesPath + "Temp\\Bin\\";
      string systemTempDir = _binariesPath + "Temp\\System\\";

      // Check if admin has access rights in the binary folder. System folder will always fail as
      // method checks all fiels and some files are inaccessible.
      // We only need enough privileges to overwrite the Oxigen files.
      if (!FileDirectoryRightsChecker.CanCreateDeleteDirectories(_binariesPath) ||
        !FileDirectoryRightsChecker.AreFilesReadableWritable(_binariesPath))
      {
        _log.WriteEntry("Cannot update Oxigen. File and directory access rights are insufficient.", EventLogEntryType.Error);

        _bError = true;
        _bCanClose = true;
        return;
      }

      if (!Directory.Exists(binTempDir))
        Directory.CreateDirectory(binTempDir);
      else
        DeleteAllFiles(binTempDir);

      if (!Directory.Exists(systemTempDir))
        Directory.CreateDirectory(systemTempDir);
      else
        DeleteAllFiles(systemTempDir);

      GeneralData generalData = null;
      User user = null;

      if (!GetSettingsFiles(ref generalData, ref user))
      {
        MessageBox.Show("Cannot retrieve new version info. Updating will be postponed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        _bError = true;
        _bCanClose = true;
        return;
      }

      int maxNoServersDownload = -1;
      int maxNoServersDelay = -1;
      int timeout = -1;
      string primaryDomainName = null;
      string secondaryDomainName = null;
      int updatedMajorVersionNumber = -1;
      int updatedMinorVersionNumber = -1;
      string updatedVersionNumber = null;

      if (!GetValuesFromGeneralData(generalData, ref maxNoServersDownload, ref maxNoServersDelay,
        ref timeout, ref primaryDomainName, ref secondaryDomainName))
      {
        MessageBox.Show("There was an error reading the global Oxigen settings.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

        _bError = true;
        _bCanClose = true;
        return;
      }

      GetNewVersionNumber(generalData, ref updatedMajorVersionNumber, 
        ref updatedMinorVersionNumber, ref updatedVersionNumber);

      string machineGUIDSuffix = user.GetMachineGUIDSuffix();
      HashSet<InterCommunicationStructures.ComponentInfo> changedComponents;

      try
      {
        changedComponents = (HashSet<InterCommunicationStructures.ComponentInfo>)Serializer.DeserializeClearText(typeof(HashSet<InterCommunicationStructures.ComponentInfo>), _appDataPath + "\\SettingsData\\components.dat");
      }
      catch
      {
        _log.WriteEntry("Error retrieving the Oxigen Updated Component List.", EventLogEntryType.Error);
        MessageBox.Show("There was an error retrieving the Oxigen Updated Components List.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

        _bError = true;
        _bCanClose = true;
        return;
      }

      _step = 100 / (changedComponents.Count + 1);

      // Download components
      UserFileMarshallerSUClient client = null;
      StreamErrorWrapper wrapper = null;

      string uri = ResponsiveServerDeterminator.GetResponsiveURI(ServerType.DownloadGetFile, 
        maxNoServersDownload, timeout,  machineGUIDSuffix, primaryDomainName, 
        secondaryDomainName, "UserFileMarshaller.svc");

      if (uri == "")
      {
        backgroundWorker.ReportProgress(100);
        _bCanClose = true;
        Application.Exit();
        return;
      }

      uri += "/file";

      try
      {
        client = new UserFileMarshallerSUClient();

        client.Endpoint.Address = new System.ServiceModel.EndpointAddress(uri);

        foreach (ComponentInfo component in changedComponents)
        {
          ComponentParameterMessage message = new ComponentParameterMessage()
          {
            ComponentFileName = component.File,
            VersionNumber = updatedVersionNumber,
            SystemPassPhrase = "password"
          };

          wrapper = client.GetComponent(message);

          if (wrapper.ErrorStatus == ErrorStatus.Success)
          {
            string path = null;

            if (component.Location == ComponentLocation.BinaryFolder)
              path = binTempDir + component.File;
            else
              path = systemTempDir + component.File;

            SaveStreamAndDispose(wrapper.ReturnStream, path);

            ReportProgress();
          }
          else
          {
            wrapper.ReturnStream.Dispose();

            _log.WriteEntry("Attempting to download file: " + component .File + " returned this error: " + wrapper.ErrorCode + " " + wrapper.Message, EventLogEntryType.Error);

            MessageBox.Show("There was an error getting the update from the server. A new update will be attempted later.");

            if (Directory.Exists(tempDir))
              Directory.Delete(tempDir, true);

            if (File.Exists(_appDataPath + "SettingsData\\components.dat"))
              File.Delete(_appDataPath + "SettingsData\\components.dat");

            // it's an all-or-nothing update so if one file fails, abort
            _bError = true;
            _bCanClose = true;
            return;
          }
        }
      }
      catch
      {
        if (Directory.Exists(tempDir))
        {
          try
          {
            Directory.Delete(tempDir, true);
          }
          catch
          {
            // ignore
          }
        }

        MessageBox.Show("Oxigen could not complete the software update. A new update will be attempted later.");

        _bError = true;
        _bCanClose = true;
        return;
      }
      finally
      {
        if (wrapper != null)
          wrapper.ReturnStream.Dispose();

        client.Dispose();
      }

      // stop the SSG
      KillProcess("OxigenService");

      // kill the Tray that monitors the SSG
      KillProcess("OxigenTray");

      // if CE and LE are running, wait until they're finished
      while (IsProcessRunning("OxigenLE") || IsProcessRunning("OxigenCE")) ;

      // TODO: stop the screensaver from starting
     
      MoveFiles(binTempDir, systemTempDir);

      System.Diagnostics.Process.Start(_binariesPath + "OxigenService.exe");

      if (File.Exists(_appDataPath + "SettingsData\\components.dat"))
        File.Delete(_appDataPath + "SettingsData\\components.dat");

      if (Directory.Exists(_binariesPath + "Temp\\"))
        Directory.Delete(_binariesPath + "Temp\\", true);

      // upload user info to server and  update local UserSettings information with the latest version of Screensaver      
      UploadUpdateCurrentVersionInfo(updatedMajorVersionNumber, updatedMinorVersionNumber, 
        updatedVersionNumber, maxNoServersDelay, timeout, machineGUIDSuffix, primaryDomainName, secondaryDomainName);

      backgroundWorker.ReportProgress(100);

      Thread.Sleep(1000);
    }

    private void UploadUpdateCurrentVersionInfo(int updatedMajorVersionNumber, 
      int updatedMinorVersionNumber, 
      string updatedVersionNumber,
      int maxNoServersRelay,
      int timeout,
      string machineGUIDSuffix,
      string primaryDomainName,
      string secondaryDomainName)
    {
      User user = null;

      // try to deserialize 10 times in case another application is accessing the file
      for (int i = 0; i < 10; i++)
      {
        try
        {
          user = (User)Serializer.DeserializeNoLock(typeof(User), _userSettingsPath, "password");
          break;
        }
        catch (IOException)
        {
          System.Threading.Thread.Sleep(1000);
          // ignore
        }

        if (user == null)
          return;
      }

      UserDataMarshallerSUClient client = null;

      string uri = ResponsiveServerDeterminator.GetResponsiveURI(ServerType.RelayLogs, maxNoServersRelay,
        timeout, machineGUIDSuffix, primaryDomainName, secondaryDomainName, "UserDataMarshaller.svc");

      if (uri == "")
      {
        backgroundWorker.ReportProgress(100);
        _bCanClose = true;

        // if no responsive URI found, abort and re-update later.
        // we need to keep database up to date with end user's machine, so we need 
        // to re-initiate update next time the SU runs.        
        return;
      }

      uri += "/suno";

      try
      {
        client = new UserDataMarshallerSUClient();

        client.Endpoint.Address = new System.ServiceModel.EndpointAddress(uri);

        client.SetCurrentVersionInfo(user.UserGUID, user.MachineGUID, updatedVersionNumber, "password");
      }
      catch
      {
        // ignore
      }
      finally
      {
        client.Dispose();
      }

      user.SoftwareMajorVersionNumber = updatedMajorVersionNumber;
      user.SoftwareMinorVersionNumber = updatedMinorVersionNumber;

      // try to deserialize 10 times in order another application is accessing the file
      for (int i = 0; i < 10; i++)
      {
        try
        {
          Serializer.Serialize(user, _userSettingsPath, "password");
          break;
        }
        catch (IOException)
        {
          System.Threading.Thread.Sleep(1000);
          // ignore
        }
      }
    }

    private void MoveFiles(string binTempDir, string systemTempDir)
    {
      // bin folder
      string[] files = Directory.GetFiles(binTempDir);

      foreach (string file in files)
      {
        // if the file is a newer software updater, then copy and rename the newer file;
        // SU can only be updated by the SSG
        if (!file.EndsWith("OxigenSU.exe"))
          File.Copy(file, _binariesPath + Path.GetFileName(file), true);
        else
          File.Copy(file, _binariesPath + Path.GetFileNameWithoutExtension(file) + ".new", true);

        File.Delete(file);
      }

      files = Directory.GetFiles(systemTempDir);

      foreach (string file in files)
      {
        File.Copy(file, _systemDir + Path.GetFileName(file), true);
        File.Delete(file);
      }
    }

    internal static bool IsProcessRunning(string processName)
    {
      Process[] processes = Process.GetProcessesByName(processName);

      if (processes.Length > 0)
        return true;

      return false;
    }

    private void KillProcess(string processName)
    {
      Process[] processes = Process.GetProcessesByName(processName);

      foreach (Process process in processes)
      {
        process.Kill();
        process.WaitForExit();
      }
    }

    public static void StartService(string serviceName, int timeoutMilliseconds)
    {
      ServiceController service = new ServiceController(serviceName);

      if (service.Status == ServiceControllerStatus.Running)
        return;

      TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

      service.Start();

      try
      {
        service.WaitForStatus(ServiceControllerStatus.Running, timeout);
      }
      catch (System.ServiceProcess.TimeoutException)
      {
        // ignore this type of exception
      }
    }

    public static void StopService(string serviceName, int timeoutMilliseconds)
    {
      ServiceController service = new ServiceController(serviceName);

      if (service.Status == ServiceControllerStatus.Stopped)
        return;

      TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

      service.Stop();

      try
      {
        service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
      }
      catch (System.ServiceProcess.TimeoutException)
      {
        // ignore this type of exception
      }
    }

    private bool SaveStreamAndDispose(Stream stream, string filePath)
    {
      FileStream fileStream = null;
      byte[] downloadedData = null;

      try
      {
        downloadedData = StreamToByteArray(stream);
      }
      catch
      {
        _log.WriteEntry(String.Format("Could not process file: {0}", filePath) + EventLogEntryType.Error);
        return false;
      }
      finally
      {
        if (stream != null)
          stream.Dispose();
      }

      try
      {
        File.WriteAllBytes(filePath, downloadedData);
      }
      catch
      {
        _log.WriteEntry(String.Format("Could not save file: {0}", filePath), EventLogEntryType.Error);
        return false;
      }
      finally
      {
        if (fileStream != null)
          fileStream.Dispose();
      }

      return true;
    }

    private byte[] StreamToByteArray(Stream stream)
    {
      MemoryStream ms = new MemoryStream();

      byte[] buffer = new byte[1000];

      int bytesRead = 0;

      do
      {
        bytesRead = stream.Read(buffer, 0, buffer.Length);

        ms.Write(buffer, 0, bytesRead);
      }
      while (bytesRead > 0);

      byte[] downloadedDataBuffer = ms.ToArray();

      ms.Close();
      ms.Dispose();

      return downloadedDataBuffer;
    }

    // delete all files and subdirectories
    public static void DeleteAllFiles(string path)
    {
      if (!Directory.Exists(path))
        return;

      try
      {
        string[] directories = Directory.GetDirectories(path);

        string[] rootFiles = Directory.GetFiles(path);

        foreach (string file in rootFiles)
        {
          try
          {
            File.Delete(file);
          }
          catch
          {
            // ignore and go to the next file
          }
        }

        foreach (string dir in directories)
        {
          try
          {
            Directory.Delete(dir, true);
          }
          catch
          {
            // ignore and go to the next file
          }
        }
      }
      catch
      {
        // ignore
      }
    }

    private void ReportProgress()
    {
      _progressBarVal += _step;

      backgroundWorker.ReportProgress(_progressBarVal);
    }

    private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      progressBar.Value = e.ProgressPercentage;
    }

    private void BackgroundWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
    {
      if (!_bError)
        MessageBox.Show("Oxigen has been successfully updated", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

      _bCanClose = true;

      Application.Exit();
    }
  }
}
