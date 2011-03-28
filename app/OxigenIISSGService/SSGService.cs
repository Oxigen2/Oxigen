using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using OxigenIIAdvertising.AppData;
using OxigenIIAdvertising.XMLSerializer;
using OxigenIIAdvertising.LoggerInfo;
using OxigenIIAdvertising.SSGService.Properties;
using System.Configuration;
using System.ServiceModel;

namespace OxigenIIAdvertising.SSGService
{
  public partial class SSGService : ServiceBase
  {
    private object generalDataObj = new object();

    ServiceHost _selfHost = null;
    Timer _logExchangerTimer = null;
    Timer _contentExchangerTimer = null;
    Timer _softwareUpdaterTimer = null;

    // Default intervals for System.Timers.Timer is 500ms.
    // Too fast if there is an error reading the intervals from the General Data.
    // Use this variable as contigency
    const double _logExchangerSafetyInterval = 60 * 60 * 1000; // one hour
    const double _contentExchangerSafetyInterval = 60 * 60 * 1000; // one hour
    const double _softwareUpdaterSafetyInterval = 7 * 24 * 60 * 60 * 1000; // one week
    
    double _logExchangerProcessingInterval = _logExchangerSafetyInterval;
    double _contentExchangerProcessingInterval = _contentExchangerSafetyInterval;
    double _softwareUpdaterProcessingInterval = _softwareUpdaterSafetyInterval;

    bool _bIntervalsReadAtLeastOnce = false;

    string _debugFilePath = "";
    string _generalDataPath = "";
    string _logExchangerPath = "";
    string _contentExchangerPath = "";
    string _softwareUpdaterPath = "";

    GeneralData _generalData = null;

    string _decryptionPassword = Settings.Default.DecryptionPassword;

    /// <summary>
    /// Will attempt to read intervals from general data file.
    /// If it fails it will read the data from the last successful read.
    /// </summary>
    public SSGService()
    {
      InitializeComponent();

      SetGlobals();

      eventLog.Source = "Oxigen Service";
      eventLog.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 7);
      eventLog.MaximumKilobytes = 1024;
    }

    private void SetGlobals()
    {
      _debugFilePath = ConfigurationSettings.AppSettings["AppDataPath"] + "SettingsData\\OxigenDebug.txt";
      _generalDataPath = ConfigurationSettings.AppSettings["AppDataPath"] + "SettingsData\\ss_general_data.dat";
      _logExchangerPath = ConfigurationSettings.AppSettings["BinariesPath"] + "OxigenLE.exe";
      _contentExchangerPath = ConfigurationSettings.AppSettings["BinariesPath"] + "OxigenCE.exe";
      _softwareUpdaterPath = ConfigurationSettings.AppSettings["BinariesPath"] + "OxigenSU.exe";
    }

    private void SetTimerIntervals()
    {
      _logExchangerTimer.Interval = _logExchangerProcessingInterval;
      _softwareUpdaterTimer.Interval = _softwareUpdaterProcessingInterval;
      _contentExchangerTimer.Interval = _contentExchangerProcessingInterval;
    }

    void logExchangerTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      ReadIntervalsSetTimerIntervals();

      eventLog.WriteEntry("OxigenLE", EventLogEntryType.Information);

      try
      {
        System.Diagnostics.Process.Start(_logExchangerPath);
      }
      catch
      {
        eventLog.WriteEntry(Resources.ErrorOpeningLogExchanger);
      }
    }

    void softwareUpdaterTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      ReadIntervalsSetTimerIntervals();

      eventLog.WriteEntry("Starting OxigenSU", EventLogEntryType.Information);

      // check if a new software updater has been downloaded adn replace the old one if so.
      string newSoftwareUpdater = System.IO.Path.GetDirectoryName(_softwareUpdaterPath) + "\\" + System.IO.Path.GetFileNameWithoutExtension(_softwareUpdaterPath) + ".new";

      if (System.IO.File.Exists(newSoftwareUpdater))
      {
        System.IO.File.Copy(newSoftwareUpdater, _softwareUpdaterPath, true);
        System.IO.File.Delete(newSoftwareUpdater);
      }

      // set the SU run flag to true so the Tray icon can pick it up if it's open.
      // if it's closed it won't make a difference as flag will remain to true until
      // it comes on and use it immediately or the service is shut down.
      AppDataSingleton.Instance.CanRunSU = true;
    }

    void contentExchangerTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      ReadIntervalsSetTimerIntervals();

      eventLog.WriteEntry("Starting OxigenCE", EventLogEntryType.Information);

      try
      {
        System.Diagnostics.Process.Start(_contentExchangerPath);
      }
      catch
      {
        eventLog.WriteEntry(Resources.ErrorOpeningContentExchanger);
      }
    }

    protected override void OnStart(string[] args)
    {
      // set up listening channel
      Uri baseAddress = new Uri("net.pipe://localhost/OxigenService");

      _selfHost = new ServiceHost(typeof(OxigenServiceCommService), baseAddress);

      NetNamedPipeBinding binding = new NetNamedPipeBinding();
      binding.ReceiveTimeout = TimeSpan.FromSeconds(30);
      binding.SendTimeout = TimeSpan.FromSeconds(30);

      try
      {
        _selfHost.AddServiceEndpoint(typeof(IOxigenService), binding, baseAddress);

        _selfHost.Open();
      }
      catch
      {
        _selfHost.Abort();
        eventLog.WriteEntry(Resources.ErrorStartingCommunicationChannel, EventLogEntryType.Error);

        // without a communication channel, stop the service
        this.Stop();
        return;
      }

      _logExchangerTimer = new Timer();
      _logExchangerTimer.Elapsed += new ElapsedEventHandler(logExchangerTimer_Elapsed);

      _contentExchangerTimer = new Timer();
      _contentExchangerTimer.Elapsed += new ElapsedEventHandler(contentExchangerTimer_Elapsed);

      _softwareUpdaterTimer = new Timer();
      _softwareUpdaterTimer.Elapsed += new ElapsedEventHandler(softwareUpdaterTimer_Elapsed);

      ReadIntervalsSetTimerIntervals();

      // Start raising the System.Timers.Timer.Elapsed for the timers
      _logExchangerTimer.Start();
      _contentExchangerTimer.Start();
      _softwareUpdaterTimer.Start();

      eventLog.WriteEntry(Resources.ServiceStarted);
    }

    // this method exits with timer intervals been set
    private void ReadIntervalsSetTimerIntervals()
    {
      bool bErrorReadingGeneralData = false;

      // as a safety measure, before reading the file always set the processing intervals to safety values
      _logExchangerProcessingInterval = _logExchangerSafetyInterval;
      _contentExchangerProcessingInterval = _contentExchangerSafetyInterval;
      _softwareUpdaterProcessingInterval = _softwareUpdaterSafetyInterval;

      // read general data
      try
      {
        lock (generalDataObj)
          _generalData = (GeneralData)Serializer.Deserialize(typeof(GeneralData), _generalDataPath, _decryptionPassword);
      }
      catch
      {
        bErrorReadingGeneralData = true;
        eventLog.WriteEntry(Resources.ErrorOpeningGeneralData, EventLogEntryType.Error);
      }

      // if general data's intervals haven't been acquired during the life of the service
      // and there was an error reading them now, set to the default safety interval
      if (!_bIntervalsReadAtLeastOnce && bErrorReadingGeneralData)
      {
        _logExchangerProcessingInterval = _logExchangerSafetyInterval;
        _contentExchangerProcessingInterval = _contentExchangerSafetyInterval;
        _softwareUpdaterProcessingInterval = _softwareUpdaterSafetyInterval;
      }
      
      // if general data's intervals have been read during the life of the service 
      // but there was an error reading General Data now
      if (_bIntervalsReadAtLeastOnce && bErrorReadingGeneralData)
          return;

      // if general data have been read, process new values
      if (!bErrorReadingGeneralData)
      {
        double readLogExchangerProcessingInterval = -1;
        double readContentExchangerProcessingInterval = -1;
        double softwareUpdaterProcessingInterval = -1;

        lock (generalDataObj)
        {
          readLogExchangerProcessingInterval = double.Parse(_generalData.Properties["logExchangerProcessingInterval"]) * 1000; // convert to miliseconds
          readContentExchangerProcessingInterval = double.Parse(_generalData.Properties["contentExchangerProcessingInterval"]) * 1000;
          softwareUpdaterProcessingInterval = double.Parse(_generalData.Properties["softwareUpdaterProcessingInterval"]) * 1000;
        }

        try
        {
          // if read intervals are less that the safety interval, then select the safety interval
          _logExchangerProcessingInterval = readLogExchangerProcessingInterval;
          _contentExchangerProcessingInterval = readContentExchangerProcessingInterval;
          _softwareUpdaterProcessingInterval = softwareUpdaterProcessingInterval;
        }
        catch (FormatException)
        {
          eventLog.WriteEntry(Resources.ErrorRetrievingTimerValues, EventLogEntryType.Error);
        }
        catch (OverflowException)
        {
          eventLog.WriteEntry(Resources.ErrorOverflowTimerValues, EventLogEntryType.Error);
        }

        // now clear references to General Data until next time they are needed
        lock (generalDataObj)
          _generalData = null;

        _bIntervalsReadAtLeastOnce = true;
      }

      // Set Timer intervals
      SetTimerIntervals();
    }

    protected override void OnStop()
    {
      try
      {
        if (_selfHost.State == CommunicationState.Opened || _selfHost.State == CommunicationState.Opening)
          _selfHost.Close();
      }
      catch
      {
        _selfHost.Abort();
        eventLog.WriteEntry(Resources.ErrorStoppingCommunicationChannel, EventLogEntryType.Error);
      }

      _logExchangerTimer.Stop();
      _contentExchangerTimer.Stop();
      _softwareUpdaterTimer.Stop();

      _logExchangerTimer.Dispose();
      _contentExchangerTimer.Dispose();
      _softwareUpdaterTimer.Dispose();

      eventLog.WriteEntry(Resources.ServiceStopped);
    }

    protected override void OnContinue()
    {
      eventLog.WriteEntry(Resources.ServiceContinue);
    }

    protected override void OnPause()
    {
      eventLog.WriteEntry(Resources.ServicePaused);
    }
  }
}
