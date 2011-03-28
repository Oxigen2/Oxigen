using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Diagnostics;
using System.Timers;
using OxigenService.Properties;
using OxigenIIAdvertising.AppData;
using OxigenIIAdvertising.XMLSerializer;
using System.ServiceModel;
using SSGContracts;
using System.Security.Principal;
using OxigenIIAdvertising.UserSettings;

namespace OxigenService
{
  public class ServiceOperations : IDisposable
  {
    private object generalDataObj = new object();

    bool _bDisposed = false;

    OxigenTimer _logExchangerTimer = null;
    OxigenTimer _contentExchangerTimer = null;
    OxigenTimer _softwareUpdaterTimer = null;

    EventLog _eventLog = null;

    string _debugFilePath = "";
    string _generalDataPath = "";
    string _logExchangerPath = "";
    string _contentExchangerPath = "";
    string _softwareUpdaterPath = "";
    string _newSoftwareUpdaterPath = "";
    string _userSettingsPath = "";
    string _decryptionPassword = "password";
    bool _bCanUpdate;

    GeneralData _generalData = null;

    public ServiceOperations()
    {
      _debugFilePath = ConfigurationSettings.AppSettings["AppDataPath"] + "SettingsData\\OxigenDebug.txt";
      _generalDataPath = ConfigurationSettings.AppSettings["AppDataPath"] + "SettingsData\\ss_general_data.dat";
      _userSettingsPath = ConfigurationSettings.AppSettings["AppDataPath"] + "SettingsData\\UserSettings.dat";
      _logExchangerPath = ConfigurationSettings.AppSettings["BinariesPath"] + "OxigenLE.exe";
      _contentExchangerPath = ConfigurationSettings.AppSettings["BinariesPath"] + "OxigenCE.exe";
      _softwareUpdaterPath = ConfigurationSettings.AppSettings["BinariesPath"] + "OxigenSU.exe";
      _newSoftwareUpdaterPath = ConfigurationSettings.AppSettings["BinariesPath"] + "OxigenSU.new";

      _eventLog = new EventLog();
      _eventLog.Log = String.Empty;
      _eventLog.Source = "Oxigen Service";

      _logExchangerTimer = new OxigenTimer("logExchangerProcessingInterval");
      _contentExchangerTimer = new OxigenTimer("contentExchangerProcessingInterval");
      _softwareUpdaterTimer = new OxigenTimer("softwareUpdaterProcessingInterval");
    }

    internal void Start()
    {
      // is this an elevated privilege restart? this means that there is a new software updater in a temp folder
      // in the Oxigen bin directory
      if (HasAdminRights() && System.IO.File.Exists(_newSoftwareUpdaterPath))
      {
        try
        {
          // Wait until the Software Updater has exited. This is important
          // in order to update the software updater if an update exists.
          // 
          // If OxigenService has started from an elevated Software Updater,
          // it too will be elevated and it will update the software updater if an update for it exists.
          WairForExit("OxigenSU");

          System.IO.File.Copy(_newSoftwareUpdaterPath, _softwareUpdaterPath, true);
          System.IO.File.Delete(_newSoftwareUpdaterPath);

          // set the SU run flag to true so the Tray icon can pick it up if it's open.
          // if it's closed it won't make a difference as flag will remain to true until
          // it comes on and use it immediately or the service is shut down.
          System.Diagnostics.Process.Start(_softwareUpdaterPath);
        }
        catch(Exception ex)
        {
          _eventLog.WriteEntry(ex.ToString());
          _eventLog.WriteEntry(Resources.ErrorOpeningSoftwareUpdater);
        }
      }

      // start tray icon
      if (!IsProcessRunning("OxigenTray"))
      {
        try
        {
          System.Diagnostics.Process.Start(ConfigurationSettings.AppSettings["BinariesPath"] + "OxigenTray.exe");
        }
        catch
        {
          _eventLog.WriteEntry(Resources.ErrorStartingTrayIconExit, EventLogEntryType.Error);
          System.Windows.Forms.Application.Exit();
          return;
        }
      }

      _bCanUpdate = GetCanUpdate();

      // TODO: move to general data.
      _logExchangerTimer.ProtectedInterval = 60 * 60 * 1000; // 3 mins
      _contentExchangerTimer.ProtectedInterval = 4 * 60 * 1000; // 4 mins
      _softwareUpdaterTimer.ProtectedInterval = 7 * 60 * 1000; // 7 mins

      _logExchangerTimer.SafetyInterval = 60 * 60 * 1000; // one hour
      _contentExchangerTimer.SafetyInterval = 60 * 60 * 1000; // one hour
      _softwareUpdaterTimer.SafetyInterval = 12 * 60 * 60 * 1000; // 12 hours

      _logExchangerTimer.Interval = _logExchangerTimer.ProtectedInterval;
      _contentExchangerTimer.Interval = _contentExchangerTimer.ProtectedInterval;

      if (_bCanUpdate)
        _softwareUpdaterTimer.Interval = _softwareUpdaterTimer.ProtectedInterval;    

      _logExchangerTimer.Elapsed += new ElapsedEventHandler(logExchangerTimer_Elapsed);
      _contentExchangerTimer.Elapsed += new ElapsedEventHandler(contentExchangerTimer_Elapsed);

      if (_bCanUpdate)
        _softwareUpdaterTimer.Elapsed += new ElapsedEventHandler(softwareUpdaterTimer_Elapsed);

      // Set the intervals of the timers to something hard coded for their first execution, then
      // read from general data all subsequent times.
      // Reason: protected period for short logins (if people are logged on for 20 mins each, and one
      // of the applications runs every hour, the timer will always be reset and that application will never run.
      // Each Application needs a chance of running at least once per login.

      // Start raising the System.Timers.Timer.Elapsed for the timers
      _logExchangerTimer.Start();
      _contentExchangerTimer.Start();

      if (_bCanUpdate)
        _softwareUpdaterTimer.Start();

      // start host
      Uri baseAddress = new Uri("net.pipe://localhost/OxigenService");

      ServiceHost selfHost = new ServiceHost(typeof(ScreensaverGuardianComm), baseAddress);

      NetNamedPipeBinding binding = new NetNamedPipeBinding();

      try
      {
        selfHost.AddServiceEndpoint(typeof(IScreensaverGuardian), binding, baseAddress);

        selfHost.Open();
      }
      catch (Exception ex)
      {
        selfHost.Abort();
        _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);

        this.Dispose();
        System.Windows.Forms.Application.Exit();
      }

      _eventLog.WriteEntry(Resources.ServiceStarted);
    }

    private bool GetCanUpdate()
    {
      User user = null;

      // try 5 times to deserialize user settings.
      for (int i = 0; i < 5; i++)
      {
        try
        {
          user = (User)Serializer.Deserialize(typeof(User), _userSettingsPath, _decryptionPassword);
          return user.CanUpdate;
        }
        catch
        {
          // ignore and wait before trying again
          System.Threading.Thread.Sleep(300);
        }
      }

      // don't allow updates, as protection.
      return false;
    }
    
    void logExchangerTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
      System.Threading.Thread.CurrentThread.CurrentCulture = ci;
      System.Threading.Thread.CurrentThread.CurrentUICulture = ci; 

      ReadIntervalsSetTimerIntervals((OxigenTimer)sender);

      _eventLog.WriteEntry("Starting OxigenLE", EventLogEntryType.Information);

      try
      {
        System.Diagnostics.Process.Start(_logExchangerPath);
      }
      catch
      {
        _eventLog.WriteEntry(Resources.ErrorOpeningLogExchanger);
      }
    }

    void softwareUpdaterTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
      System.Threading.Thread.CurrentThread.CurrentCulture = ci;
      System.Threading.Thread.CurrentThread.CurrentUICulture = ci; 

      ReadIntervalsSetTimerIntervals((OxigenTimer)sender);

      // check if a new software updater has been downloaded adn replace the old one if so.
      if (System.IO.File.Exists(_newSoftwareUpdaterPath))
      {
        if (!HasAdminRights())
        {
          System.Windows.Forms.MessageBox.Show("There are new updates available. Click ok to install these updates.",
            "Oxigen", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);

          // application needs to restart with elevated privileges. Cannot elevate privileges
          // in an already running process.
          if (RunElevated(System.Windows.Forms.Application.ExecutablePath))
          {
            System.Windows.Forms.Application.Exit();
            return;
          }
        }
        else  // OxigenService already runs under admin privileges.
        {
          try
          {
            System.IO.File.Copy(_newSoftwareUpdaterPath, _softwareUpdaterPath, true);
            System.IO.File.Delete(_newSoftwareUpdaterPath);
          }
          catch
          {
            _eventLog.WriteEntry(Resources.ErrorUpdatingUpdater, EventLogEntryType.Error);
          }
        }
      }

      _eventLog.WriteEntry("Starting OxigenSU", EventLogEntryType.Information);

      try
      {
        System.Diagnostics.Process.Start(_softwareUpdaterPath);
      }
      catch
      {
        _eventLog.WriteEntry(Resources.ErrorOpeningSoftwareUpdater, EventLogEntryType.Error);
      }
    }

    private bool HasAdminRights()
    {
      WindowsPrincipal pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
      return pricipal.IsInRole(WindowsBuiltInRole.Administrator);
    }

    private static bool RunElevated(string fileName)
    {
      ProcessStartInfo processInfo = new ProcessStartInfo();
      processInfo.Verb = "runas";
      processInfo.FileName = fileName;

      try
      {
        Process.Start(processInfo);
        return true;
      }
      catch (System.ComponentModel.Win32Exception)
      {
        // Do nothing. Probably the user canceled the UAC window, so communicate that fact.
        return false;
      }
    }

    internal static bool IsProcessRunning(string processName)
    {
      Process[] processes = Process.GetProcessesByName(processName);

      if (processes.Length > 0)
        return true;

      return false;
    }

    internal static void WairForExit(string processName)
    {
      Process[] processes = Process.GetProcessesByName(processName);

      if (processes.Length > 0)
      {
        foreach (Process p in processes)
          p.WaitForExit();
      }
    }

    void contentExchangerTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
      System.Threading.Thread.CurrentThread.CurrentCulture = ci;
      System.Threading.Thread.CurrentThread.CurrentUICulture = ci; 

      ReadIntervalsSetTimerIntervals((OxigenTimer)sender);

      _eventLog.WriteEntry("Starting OxigenCE", EventLogEntryType.Information);

      try
      {
        System.Diagnostics.Process.Start(_contentExchangerPath);
      }
      catch
      {
        _eventLog.WriteEntry(Resources.ErrorOpeningContentExchanger);
      }
    }

    private void ReadIntervalsSetTimerIntervals(OxigenTimer timer)
    {
      bool bErrorReadingGeneralData = false;

      // as a safety measure, before reading the file always set the processing intervals to safety values
      timer.Interval = timer.SafetyInterval;

      // read general data
      try
      {
        lock (generalDataObj)
          _generalData = (GeneralData)Serializer.Deserialize(typeof(GeneralData), _generalDataPath, _decryptionPassword);
      }
      catch
      {
        bErrorReadingGeneralData = true;
        _eventLog.WriteEntry(Resources.ErrorOpeningGeneralData, EventLogEntryType.Error);
      }

      // if general data's intervals have been read during the life of the service 
      // but there was an error reading General Data now,
      // keep old values
      if (timer.IntervalsReadOnce && bErrorReadingGeneralData)
        return;

      // if general data have been read, process new values
      if (!bErrorReadingGeneralData)
      {
        double interval = -1;

        lock (generalDataObj)
        {
          interval = double.Parse(_generalData.Properties[timer.IntervalPropertyOnGeneralSettings]) * 1000; // convert to miliseconds

          // now clear references to General Data until next time they are needed
          _generalData = null;
        }

        // Note: there was going to be a logic implemented here that selects the safety interval
        // if interval on ss_general_data.dat is smaller than the safety interval
        // Now we always save the interval from the general data file.
        timer.Interval = interval;
        timer.IntervalsReadOnce = true;
      }
      else
        timer.Interval = timer.SafetyInterval;
    }

    ~ServiceOperations()
    {
      Dispose(false);
    }

    /// <summary>
    /// Releases all resources used in OxigenIIAdvertising.DataAccess.SqlDatabase
    /// </summary>
    public void Dispose()
    {
      Dispose(true);

      // Use SupressFinalize in case a subclass
      // of this type implements a finalizer.
      GC.SuppressFinalize(this);
    }

    public void Dispose(bool bDisposing)
    {
      if (!_bDisposed)
      {
        if (bDisposing)
        {
          if (_logExchangerTimer != null)
          {
            _logExchangerTimer.Dispose();
            _logExchangerTimer = null;
          }

          if (_contentExchangerTimer != null)
          {
            _contentExchangerTimer.Dispose();
            _contentExchangerTimer = null;
          }

          if (_softwareUpdaterTimer != null)
          {
            _softwareUpdaterTimer.Dispose();
            _softwareUpdaterTimer = null;
          }

          if (_eventLog != null)
          {
            _eventLog.Dispose();
            _eventLog = null;
          }
        }

        _bDisposed = true;
      }
    }
  }
}
