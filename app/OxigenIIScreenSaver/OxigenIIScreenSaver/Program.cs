using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using OxigenIIAdvertising.ScreenSaver.Properties;
using OxigenIIAdvertising.AppData;
using OxigenIIAdvertising.XMLSerializer;
using OxigenIIAdvertising.LoggerInfo;
using OxigenIIAdvertising.AssetScheduling;
using OxigenIIAdvertising.LogWriter;
using OxigenIIAdvertising.FileRights;
using System.IO;
using System.Threading;
using System.Diagnostics;
using OxigenIIAdvertising.Singletons;
using OxigenIIAdvertising.UserSettings;
using System.Net;
using System.Net.Sockets;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Configuration;
using System.Management;
using System.Globalization;

namespace OxigenIIAdvertising.ScreenSaver
{
  static class Program
  {
    private static string _screenSaverConfigPath = "";
    private static string _tempDecryptPath = "";
    private static string _assetPath = "";
    private static string _debugFilePath = "";
    private static string _userSettingsPath = "";
    private static string _playlistPath = "";
    private static string _settingsDataPath = "";
    private static string _generalDataPath = "";
    private static string _contentExchangerPath = "";
    private static string _password = "";
    private static string _advertClickLogsPath1 = "";
    private static string _advertClickLogsPath2 = "";
    private static string _advertImpressionLogsPath1 = "";
    private static string _advertImpressionLogsPath2 = "";
    private static string _contentClickLogsPath1 = "";
    private static string _contentClickLogsPath2 = "";
    private static string _contentImpressionLogsPath1 = "";
    private static string _contentImpressionLogsPath2 = "";
    private static string _usageCountLogsPath1 = "";
    private static string _usageCountLogsPath2 = "";
    private static string _appDataPath = "";

    private static Playlist _playlist;
    private static Logger _logger = null;
    private static System.Timers.Timer _logTimer; // every interval, save log files to disk so they don't clutter into memory
    private static System.Timers.Timer _autoExecuteProgramTimer; // after this time interval, an external program will execute
    private static System.Timers.Timer _playlistLoadTimer; // after this time interval, re-load playlist in case the CE has run while the SS is still playing
    private static float _advertDisplayThreshold = -1;
    private static float _protectedContentTime = -1F;
    private static float _displayMessageAssetDisplayLength = -1F;
    private static int _requestTimeout = -1;
    private static int _logTimerInterval = -1;
    private static DateTime _startDateTime = DateTime.Now;
    private static bool _bPreviewMode = false;
    private static string _displayMessage = "";
    private static string _appToRun = "";
    private static bool _bErrorOnStartup = false; // errors during setup? process command line but show 'error' screen saver
    private static bool _bAlreadyWritingLogs = false;
    private static IScreenSaver[] _screenSavers = null;
    private static User _user = null;
    private static bool _bInsufficientMemoryForLargeFiles = false;
    private static object _lockPlaylistObj = new object(); // need to lock the playlist object as it will be updated at regular intervals if the CE runs in parallel.

    // Termination events: either user instigated or auto termination instigated
    // Mutually exclude the two events from running at the same time, if auto executes roughly at the same
    // time as user exits the screen saver manually
    private static bool _bTerminationStarted = false;
    private static object _terminationObj = new object();

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
      // make sure no other Screensaver process is running
      Process process = Process.GetCurrentProcess();
      string processName = process.ProcessName;

      Process[] processes = Process.GetProcessesByName(processName);

      if (processes.Length > 1)
        return;

      System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
      System.Threading.Thread.CurrentThread.CurrentCulture = ci;
      System.Threading.Thread.CurrentThread.CurrentUICulture = ci; 

      try
      {
        // set globals
        SetGlobalsFromConfFile();

        long ram = GetPhysicalRam();

        if (ram == -1)
          _bErrorOnStartup = true;

        _bInsufficientMemoryForLargeFiles = ram < 1095216660;

        if (args.Length > 0)
        {
          string firstArgument = args[0].ToLower().Trim();
          string firstArgumentConfSwitch = firstArgument.Substring(0, 2);

          switch (firstArgumentConfSwitch)
          {
            case "/s":
              _logger.WriteTimestampedMessage("Screensaver running in /s mode.");
              Application.EnableVisualStyles();
              Application.SetCompatibleTextRenderingDefault(false);
              ShowScreenSaver();
              Application.Run();
              break;

            case "/p":
              _logger.WriteTimestampedMessage("Screensaver running in /p mode.");
              // preview the screen saver
              Application.EnableVisualStyles();
              Application.SetCompatibleTextRenderingDefault(false);
              //args[1] is the handle to the preview window
              SetScreenSaverPreviewMode(args[1]);
              Application.Run((PreviewScreenSaver)_screenSavers[0]);
              break;

            case "/c":
              _logger.WriteTimestampedMessage("Screensaver running in /c mode.");
              //configure the screen saver
              Application.EnableVisualStyles();
              try
              {
                System.Diagnostics.Process.Start(_screenSaverConfigPath);
              }
              catch (Exception ex)
              {
                _logger.WriteError(ex);
                MessageBox.Show("Error launching Screensaver Configuration program.", "Error.");
                return;
              }
              break;

            default:
              _logger.WriteTimestampedMessage("Screensaver running in default mode.");
              //no known parameter: show the screen saver anyway
              Application.EnableVisualStyles();
              Application.SetCompatibleTextRenderingDefault(false);
              ShowScreenSaver();
              Application.Run();
              break;
          }
        }
        else
        {
          //no arguments were passed - show the screen saver
          _logger.WriteTimestampedMessage("Screensaver running with no arguments passed.");
          Application.EnableVisualStyles();
          Application.SetCompatibleTextRenderingDefault(false);
          ShowScreenSaver();
          Application.Run();
        }
      }
      catch
      {
        Application.Exit();
      }
    }

    private static void TryCreateTempAssetPath()
    {
      try
      {
        // create directory. if directory already exist, the method will not throw an exception
        Directory.CreateDirectory(_tempDecryptPath);
        while (!Directory.Exists(_tempDecryptPath)) ;

        _logger.WriteTimestampedMessage("Temp directory in existence.");
      }
      catch (Exception ex)
      {
        _bErrorOnStartup = true;
        _displayMessage = Resources.FatalError;
        _logger.WriteError(ex);
      }
    }

    private static void SetGlobalsFromConfFile()
    {
      try
      {
        _appDataPath = ConfigurationSettings.AppSettings["AppDataPath"];

        _debugFilePath = _appDataPath + "SettingsData\\OxigenDebug.txt";
        _userSettingsPath = _appDataPath + "SettingsData\\UserSettings.dat";
        _screenSaverConfigPath = ConfigurationSettings.AppSettings["BinariesPath"] + "ScreenSaverConfig.exe";
        _tempDecryptPath = _appDataPath + "Temp\\";
        _userSettingsPath = _appDataPath + "SettingsData\\UserSettings.dat";
        _playlistPath = _appDataPath + "SettingsData\\ss_play_list.dat";
        _settingsDataPath = _appDataPath + "SettingsData\\";
        _generalDataPath = _appDataPath + "SettingsData\\ss_general_data.dat";
        _contentExchangerPath = ConfigurationSettings.AppSettings["BinariesPath"] + "OxigenCE.exe";
        _assetPath = _appDataPath + "Assets\\";

        _advertImpressionLogsPath1 = _appDataPath + "Other\\ss_ad_i_1.dat";
        _advertClickLogsPath1 = _appDataPath + "Other\\ss_ad_c_1.dat";
        _contentImpressionLogsPath1 = _appDataPath + "Other\\ss_co_i_1.dat";
        _contentClickLogsPath1 = _appDataPath + "Other\\ss_co_c_1.dat";
        _advertImpressionLogsPath2 = _appDataPath + "Other\\ss_ad_i_2.dat";
        _advertClickLogsPath2 = _appDataPath + "Other\\ss_ad_c_2.dat";
        _contentImpressionLogsPath2 = _appDataPath + "Other\\ss_co_i_2.dat";
        _contentClickLogsPath2 = _appDataPath + "Other\\ss_co_c_2.dat";
        _usageCountLogsPath1 = _appDataPath + "Other\\ss_usg_1.dat";
        _usageCountLogsPath2 = _appDataPath + "Other\\ss_usg_2.dat";

        _password = "password";
        _logger = new Logger("ScreenSaver Main", _debugFilePath, LoggingMode.Debug);

        _logger.WriteTimestampedMessage("Startup. Global variables read from config file successfully.");
      }
      catch (Exception ex)
      {
        _bErrorOnStartup = true;
        _displayMessage = Resources.DataMissingFromConfFile;
        
        if (_logger != null)
          _logger.WriteError(ex);

        return;
      }
    }

    /// <summary>
    /// Gets the user settings
    /// </summary>
    private static void GetUserSettings()
    {
      try
      {
        _user = (User)Serializer.Deserialize(typeof(User), _userSettingsPath, "password");

        _logger.WriteTimestampedMessage("UserSettings.dat variables read and decrypted successfully.");
      }
      catch (Exception ex)
      {
        _logger.WriteError(ex);

        _displayMessage = Resources.DataMissingFromConfFile;
        // TODO: when installer supports repair mode, add this: _appToRun = "app://SoftwareInstaller";
        _bErrorOnStartup = true;
      }
    }
    
    /// <summary>
    /// Gets the playlist
    /// </summary>
    /// <exception cref="SerializationException">thrown when an error occurs during the object's deserializaton</exception>
    /// <exception cref="FileNotFoundException">thrown if XML serialized file not found</exception>
    /// <exception cref="InvalidOperationException">thrown by XmlSerializer.Deserialize</exception>
    /// <exception cref="IOException">thrown when there is an error during read</exception>
    /// <exception cref="DirectoryNotFoundException">thrown when the directory is not found</exception>
    /// <exception cref="CryptographicException">thrown when encrypted file is corrupted</exception>
    private static void GetPlaylist()
    {
      try
      {
        lock(_lockPlaylistObj)
          _playlist = (Playlist)Serializer.Deserialize(typeof(Playlist), _playlistPath, "password");

        _logger.WriteTimestampedMessage("ss_play_list.dat read and decrypted successfully.");
      }
      catch // no need to treat the exception as an error as it isn't really an error if the playlist doesn't exist
      {
        _displayMessage = Resources.NoPlaylist;
        _appToRun = "app://ContentExchanger";
        _bErrorOnStartup = true;
      }
    }

    private static Playlist GetSubsequentPlaylist()
    {
      Playlist playlist; 

      try
      {
        playlist = (Playlist)Serializer.Deserialize(typeof(Playlist), _playlistPath, "password");
        _logger.WriteTimestampedMessage("Subsequent ss_play_list.dat read and decrypted successfully.");

        return playlist;
      }
      catch (Exception ex)
      {
        // ignore
        _logger.WriteError("Error retrieving the playlist: " + ex.ToString());
        return null;
      }
    }

    private static void FileAccessRights()
    {
      if (!FileDirectoryRightsChecker.AreFilesReadableWritable(_appDataPath))
      {
        _displayMessage = Resources.NoLogFileRights;
        _appToRun = "";
        _bErrorOnStartup = true;
      }

      _logger.WriteTimestampedMessage("file access rights tested successfully.");
    }
        
    /// <summary>
    /// Retrieves data from general configuration file
    /// </summary>
    /// <returns>true if data extraction successful, false otherwise</returns>
    private static void GetGeneralDataValues()
    {
      GeneralData generalData = null;

      // get general data
      try
      {
        generalData = (GeneralData)Serializer.Deserialize(typeof(GeneralData), _generalDataPath, "password");

        _logger.WriteTimestampedMessage("ss_general_data.dat successfully read and unencrypted.");
      }
      catch (Exception ex)
      {
        _logger.WriteError(ex);
        _displayMessage = Resources.NoGeneralData;
        _appToRun = "app://ContentExchanger-s";
        _bErrorOnStartup = true;
        return;
      }

      if (!ParseNumericalGeneralProperties(generalData))
      {
        // TODO: when installer supports repair mode, add this: _appToRun = "app://SoftwareInstaller";
        _bErrorOnStartup = true;
      }
    }

    private static bool ParseNumericalGeneralProperties(GeneralData generalData)
    {
      if (!TryParseFloat(generalData, "advertDisplayThreshold", out _advertDisplayThreshold))
      {
        _displayMessage = Resources.ParameterNorParseable;
        return false;
      }

      if (!TryParseInteger(generalData, "logTimerInterval", out _logTimerInterval))
      {
        _displayMessage = Resources.ParameterNorParseable;
        return false;
      }

      if (!TryParseFloat(generalData, "protectedContentTime", out _protectedContentTime))
      {
        _displayMessage = Resources.ParameterNorParseable;
        return false;
      }

      if (!TryParseFloat(generalData, "noAssetDisplayLength", out _displayMessageAssetDisplayLength))
      {
        _displayMessage = Resources.ParameterNorParseable;
        return false;
      }

      if (!TryParseInteger(generalData, "requestTimeout", out _requestTimeout))
      {
        _displayMessage = Resources.ParameterNorParseable;
        return false;
      }

      _logger.WriteTimestampedMessage("numerical values from ss_general_data.dat parsed successfully.");

      return true;
    }

    public static bool TryParseInteger(GeneralData generalData, string generalDataProperty, out int integerValue)
    {
      integerValue = -1;

      if (!GeneralDataContainsKey(generalData, generalDataProperty))
        return false;

      if (!int.TryParse(generalData.Properties[generalDataProperty], out integerValue))
      {
        _logger.WriteError("Unable to parse parameter " + generalDataProperty + " from general configuration file.");
        return false;
      }

      return true;
    }

    public static bool TryParseFloat(GeneralData generalData, string generalDataProperty, out float floatValue)
    {
      floatValue = -1;

      if (!GeneralDataContainsKey(generalData, generalDataProperty))
        return false;

      if (!float.TryParse(generalData.Properties[generalDataProperty], out floatValue))
      {
        _logger.WriteError("Unable to parse parameter " + generalDataProperty + " from general configuration file.");
        return false;
      }

      return true;
    }

    private static bool GeneralDataContainsKey(GeneralData generalData, string key)
    {
      if (!generalData.Properties.ContainsKey(key))
      {
        _logger.WriteError(key + " not found");
        _displayMessage = Resources.GeneralDataParameterNotFound;
        // TODO: when installer supports repair mode, add this: _appToRun = "app://SoftwareInstaller";
        return false;
      }

      return true;
    }

    private static void SetLogTimer()
    {
      _logTimer = new System.Timers.Timer();

      _logTimer.Interval = 1000 * 60 * _logTimerInterval;

      _logTimer.Elapsed += new System.Timers.ElapsedEventHandler(logTimer_Elapsed);
      _logTimer.Start();

      _logger.WriteTimestampedMessage("Log Timer set up and started successfully.");
    }

    private static void SetAutoExecuteProgramTimer()
    {
      _logger.WriteTimestampedMessage("Is there a program that we can run after x time.");

      if (string.IsNullOrEmpty(_user.ProgramToRun))
      {
        _logger.WriteTimestampedMessage("No; program will not run");

        return;
      }

      _autoExecuteProgramTimer = new System.Timers.Timer();

      _logger.WriteTimestampedMessage("Setting _autoExecuteProgramTimer's interval.");

      _autoExecuteProgramTimer.Interval = 1000 * 60 * _user.TimeUntilProgramToRunMinutes;
      
      _logger.WriteTimestampedMessage("_autoExecuteProgramTimer's interval. set.");

      _autoExecuteProgramTimer.Elapsed += new System.Timers.ElapsedEventHandler(autoExecuteProgramTimer_Elapsed);
      _autoExecuteProgramTimer.Start();

      _logger.WriteTimestampedMessage("Auto execute Timer set up and started successfully.");
    }

    private static void SetPlaylistLoadTimer()
    {
      _playlistLoadTimer = new System.Timers.Timer();
      _playlistLoadTimer.Interval = 1000 * 60 * 10; // 10 minutes

      _playlistLoadTimer.Elapsed += new System.Timers.ElapsedEventHandler(playlistLoadTimer_Elapsed);
      _playlistLoadTimer.Start();

      _logger.WriteTimestampedMessage("Auto Load Playlist Timer set up and started successfully.");
    }

    static void playlistLoadTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      _logger.WriteTimestampedMessage("playlistLoadTimer_Elapsed: tick");

      // in preview mode, don't load a playlist
      if (_bPreviewMode)
        return;

      _logger.WriteTimestampedMessage("playlistLoadTimer_Elapsed: continuing to playlist");

      // a lock is placed when getting the playlist from disk
      Playlist playlist = GetSubsequentPlaylist();

      _logger.WriteTimestampedMessage("playlistLoadTimer_Elapsed: tick");

      // iterate through the target machine's monitors and set the updated playlist to
      // each screensaver.
      // Instead of placing a big lock statement around the for loop, there is a lock statement
      // in the Playlist property when getting/setting the playlist so one or more playlist operations
      // may occur (e.g. selecting an asset to display) between setting the playlist on each monitor, since the
      // lock is released at the end of each foreach loop below.
      // As a result it is possible that a monitor may still play an asset (or theoretically more) from the
      // old playlist before it gets the lock and updates its playlist.
      // A playlist is a reference object and the old playlist will remain alive until all screensavers will have
      // given up their reference variables to the new playlist.

      if (playlist != null)
      {
        foreach (ScreenSaver screensaver in _screenSavers)
          screensaver.SetPlaylistAssetPickerPlaylist(new Playlist(playlist));
      }
    }

    static void autoExecuteProgramTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      // If screen saver runs on preview mode, don't execute the external application
      if (_bPreviewMode)
        return;

      lock (_terminationObj)
      {
        // as long as _bTerminationStarted is only being accessed within a thread
        // by a locked _terminationObj, it doesn't matter if we check it's value first and set its value
        // second or sets its value first and check its value second
        if (_bTerminationStarted)
          return;

        _bTerminationStarted = true;

        // hide monitor forms
        foreach (ScreenSaver screensaver in _screenSavers)
          screensaver.HideForms();

        // are all forms hidden on all monitors
        bool bFormsShowing = true;

        while (bFormsShowing)
        {
          foreach (ScreenSaver screensaver in _screenSavers)
          {
            bFormsShowing = screensaver.FormsShowing();

            if (bFormsShowing)
              break;
          }
        }

        _logger.WriteTimestampedMessage("successfully hid forms.");

        // Stop logging/background work here lest displaying and, more importantly, logging
        // continue running when the screen savers are hidden.
        foreach (ScreenSaver screensaver in _screenSavers)
          screensaver.StopWork();

        bool bScreensaverThreadsAlive = true;

        while (bScreensaverThreadsAlive)
        {
          foreach (ScreenSaver screensaver in _screenSavers)
          {
            bScreensaverThreadsAlive = screensaver.AreScreensaverThreadsAlive();

            if (bScreensaverThreadsAlive)
              break;
          }
        }

        _logger.WriteTimestampedMessage("successfully stopped threads.");

        WriteLogs(true);

        // dispose of all forms, for each monitor
        foreach (ScreenSaver screensaver in _screenSavers)
        {
          // in preview mode, the form is closed when user presses OK, Cancel or Preview,
          // and raising the OnClose event again here would result in an infinite loop with TerminateApplication.
          if (!_bPreviewMode)
            screensaver.Close();

          _logger.WriteTimestampedMessage("successfully closed forms.");

          screensaver.DisposeForms();

          _logger.WriteTimestampedMessage("successfully disposed all screen saver objects.");
        }

        // local cleanup
        lock (_lockPlaylistObj)
          _playlist = null;

        if (_logTimer != null)
        {
          _logTimer.Stop();
          _logTimer.Dispose();
          _logTimer = null;

          _logger.WriteTimestampedMessage("successfully disposed of log timer.");
        }

        if (_autoExecuteProgramTimer != null)
        {
          _autoExecuteProgramTimer.Stop();
          _autoExecuteProgramTimer.Dispose();
          _autoExecuteProgramTimer = null;

          _logger.WriteTimestampedMessage("successfully disposed of auto-execute program timer.");
        }

        if (_playlistLoadTimer != null)
        {
          _playlistLoadTimer.Stop();
          _playlistLoadTimer.Dispose();
          _playlistLoadTimer = null;

          _logger.WriteTimestampedMessage("successfully disposed of new playlist check timer.");
        }

        _logger.WriteTimestampedMessage("Exiting application.");

        // start external process
        try
        {
          System.Diagnostics.Process.Start(_user.ProgramToRun);
        }
        catch
        {
          _logger.WriteWarning("Could not start external process");
        }

        // exit application
        Application.Exit();
      }
    }

    static void logTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      WriteLogs(false);
    }   

    public static void TerminateApplication(bool bClickThroughPressed)
    {
      lock (_terminationObj)
      {
        // as long as _bTerminationStarted is only being accessed within a thread
        // by a locked _terminationObj, it doesn't matter if we check it's value first and set its value
        // second or sets its value first and check its value second
        if (_bTerminationStarted)
          return;

        _bTerminationStarted = true;

        if (_bPreviewMode)
        {
          PreviewScreenSaver pss = (PreviewScreenSaver)_screenSavers[0];
          pss.Dispose();

          Application.Exit();
          return;
        }

        // hide monitor forms
        foreach (ScreenSaver screensaver in _screenSavers)
          screensaver.HideForms();

        // are all forms hidden on all monitors
        bool bFormsShowing = true;

        while (bFormsShowing)
        {
          foreach (ScreenSaver screensaver in _screenSavers)
          {
            bFormsShowing = screensaver.FormsShowing();

            if (bFormsShowing)
              break;
          }
        }

        _logger.WriteTimestampedMessage("successfully hid forms.");

        // Stop logging/background work here lest displaying and, more importantly, logging
        // continue running when the screen savers are hidden.
        foreach (ScreenSaver screensaver in _screenSavers)
          screensaver.StopWork();

        bool bScreensaverThreadsAlive = true;

        while (bScreensaverThreadsAlive)
        {
          foreach (ScreenSaver screensaver in _screenSavers)
          {
            bScreensaverThreadsAlive = screensaver.AreScreensaverThreadsAlive();

            if (bScreensaverThreadsAlive)
              break;
          }
        }

        _logger.WriteTimestampedMessage("successfully stopped threads.");

        // if spacebar was hit, pop up browser windows and write click logs
        if (bClickThroughPressed)
        {
          foreach (ScreenSaver screenSaver in _screenSavers)
          {
            string clickThroughURL = screenSaver.GetClickThroughURL();

            _logger.WriteTimestampedMessage("clickthroughURL: " + clickThroughURL);

            if (clickThroughURL.StartsWith("app://"))
            {
              switch (clickThroughURL)
              {
                case "app://ContentExchanger":
                  System.Diagnostics.Process.Start(_contentExchangerPath);
                  _logger.WriteTimestampedMessage("successfully started app in " + _contentExchangerPath);
                  break;
                case "app://ContentExchanger-s":
                  System.Diagnostics.Process.Start(_contentExchangerPath, "/s");
                  _logger.WriteTimestampedMessage("successfully started app in " + _contentExchangerPath + " in safe mode");
                  break;
                // TODO: when installer supports repair mode, add this case statement
             /*   case "app://SoftwareInstaller":
                  // TODO: execute software installer here
                  break;*/
              }
            }

            if (clickThroughURL.StartsWith("http://") || clickThroughURL.StartsWith("https://"))
            {
              // click log to memory
              screenSaver.AddClickLog();

              _logger.WriteTimestampedMessage("successfully added click log.");

              // cross-desktop communication can't be made; communicate to the SSG to open browser
              ScreensaverGuardianClient client = null;

              try
              {
                System.ServiceModel.NetNamedPipeBinding binding = new System.ServiceModel.NetNamedPipeBinding();
                binding.Name = "SSGBinding";
                binding.TransferMode = System.ServiceModel.TransferMode.Buffered;
                binding.ReceiveTimeout = TimeSpan.FromMinutes(1);
                binding.SendTimeout = TimeSpan.FromMinutes(1);
                client = new ScreensaverGuardianClient(binding, new System.ServiceModel.EndpointAddress("net.pipe://localhost/OxigenService"));

                client.OpenBrowser(clickThroughURL);
              }
              catch (Exception ex)
              {
                _logger.WriteError(ex);
              }
              finally
              {
                if (client != null)
                  client.Dispose();
              }

              _logger.WriteTimestampedMessage("web browser opening attempted.");
            }
          }
        }

        WriteLogs(true);

        // dispose of all forms, for each monitor
        foreach (ScreenSaver screensaver in _screenSavers)
        {
          // in preview mode, the form is closed when user presses OK, Cancel or Preview,
          // and raising the OnClose event again here would result in an infinite loop with TerminateApplication.
          if (!_bPreviewMode)
            screensaver.Close();

          _logger.WriteTimestampedMessage("successfully closed forms.");

          screensaver.DisposeForms();

          _logger.WriteTimestampedMessage("successfully disposed all screen saver objects.");
        }

        // local cleanup
        _playlist = null;

        if (_logTimer != null)
        {
          _logTimer.Stop();
          _logTimer.Dispose();
          _logTimer = null;

          _logger.WriteTimestampedMessage("successfully disposed of log timer.");
        }

        if (_autoExecuteProgramTimer != null)
        {
          _autoExecuteProgramTimer.Stop();
          _autoExecuteProgramTimer.Dispose();
          _autoExecuteProgramTimer = null;

          _logger.WriteTimestampedMessage("successfully disposed of auto-execute program timer.");
        }

        _logger.WriteTimestampedMessage("Exiting application.");
        // exit application
        Application.Exit();
      }
    }

    private static void WriteLogs(bool bFinalWrite)
    {
      if (_bErrorOnStartup)
      {
        _logger.WriteTimestampedMessage("there was an error on startup. Cannot write logs. Exiting.");
        return;
      }

      // method may have been fired by the _logTimer. In this case, if method is also fired
      // as application exists and the timer call hasn't yet finished, wait.
      // the probability of two timer calls come as close together as for the first call not to have
      // finished is practically zero as the _logTimer interval is long enough.
      while (_bAlreadyWritingLogs) ;

      _bAlreadyWritingLogs = true;

      // play time in this session
      int playTime = (int)Math.Round((DateTime.Now.Subtract(_startDateTime)).TotalSeconds, 0);

      // write log files
      try
      {
        LogFileWriter.WriteLogs(_advertClickLogsPath1,
                                _advertClickLogsPath2,
                                _advertImpressionLogsPath1,
                                _advertImpressionLogsPath2,
                                _contentClickLogsPath1,
                                _contentClickLogsPath2,
                                _contentImpressionLogsPath1,
                                _contentImpressionLogsPath2,
                                _usageCountLogsPath1,
                                _usageCountLogsPath2,
                                _user.MachineGUID, // closed networks: this may not be populated until CE or LE run. That's fine as no logs will be written if CE has not run to download content first.
                                _user.UserGUID,
                                playTime,
                                _password,
                                bFinalWrite,
                                _bPreviewMode, 
                                _logger);

        _logger.WriteTimestampedMessage("successfully wrote logs.");
      }
      catch (Exception ex)
      {
        _logger.WriteError(ex);
      }

      if (bFinalWrite)
      {
        LogEntriesRawSingleton.Reset();

        _logger.WriteTimestampedMessage("successfully cleared the lgo object.");
      }

      _bAlreadyWritingLogs = false;
    }

    /// <summary>
    /// Starts up one screen saver per monitor
    /// </summary>
    private static void ShowScreenSaver()
    {
      // check if Screensaver can start
      if (!_bErrorOnStartup)
      {
        // retrieve admin-set values needed for the operation of the screensaver
        GetGeneralDataValues();

        if (!_bErrorOnStartup)
        {
          // get user's settings
          GetUserSettings();

          if (!_bErrorOnStartup)
          {
            // is there read/write access to log and conf files 
            FileAccessRights();

            // Create Temp Directory
            TryCreateTempAssetPath();

            if (!_bErrorOnStartup)
              // retrieve the playlist
              GetPlaylist();
          }
        }
      }

      if (!_bErrorOnStartup)
      {
        SetLogTimer();
        SetAutoExecuteProgramTimer();
        SetPlaylistLoadTimer();
      }

      // how many monitors has the user got?
      int screenCount = Screen.AllScreens.Length;

      _screenSavers = new ScreenSaver[screenCount];

      bool bMuteFlash = false;
      bool bMuteVideo = false;
      int flashVolume = 1;
      int videoVolume = 50;
      float defaultDisplayDuration = 10F;

      GetUserSettingsForScreensaver(_user, ref bMuteFlash, ref bMuteVideo, ref flashVolume, 
        ref videoVolume, ref defaultDisplayDuration);

      for (int i = 0; i < screenCount; i++)
      {
        _logger.WriteTimestampedMessage("Setting up screen number " + i);

        if (_user == null)
          _logger.WriteError("user is null");

        ScreenSaver screenSaver = new ScreenSaver(i, new Playlist(_playlist), _advertDisplayThreshold, 
          _protectedContentTime, _displayMessageAssetDisplayLength, _requestTimeout, bMuteFlash, bMuteVideo,
          flashVolume, videoVolume, _bErrorOnStartup, _displayMessage, _appToRun, 
          _debugFilePath, _tempDecryptPath, _assetPath, _bInsufficientMemoryForLargeFiles, _lockPlaylistObj, 
          defaultDisplayDuration);
        _logger.WriteTimestampedMessage("Successfully created Screensaver in monitor " + i);
        
        _screenSavers[i] = screenSaver;

        screenSaver.Show();

        _logger.WriteTimestampedMessage("Successfully started Screensaver in monitor " + i);
      }

      // capture events
      Application.AddMessageFilter(new ScreenSaverMessageFilter());
    }

    private static void GetUserSettingsForScreensaver(User user, ref bool bMuteFlash, 
      ref bool bMuteVideo, ref int flashVolume, ref int videoVolume, ref float defaultDisplayDuration)
    {
      if (user == null)
        return;

      bMuteFlash = user.MuteFlash;
      bMuteVideo = user.MuteVideo;
      flashVolume = user.FlashVolume;
      videoVolume = user.VideoVolume;
      defaultDisplayDuration = user.DefaultDisplayDuration;
    }

    private static void SetScreenSaverPreviewMode(string previewHandle)
    {
      _bPreviewMode = true;
      _screenSavers = new PreviewScreenSaver[1];
      _screenSavers[0] = new PreviewScreenSaver(new IntPtr(long.Parse(previewHandle)), _debugFilePath);

      _logger.WriteTimestampedMessage("successfully started a preview screensaver.");
    }

    private static bool TryGrantAccessToTemp()
    {
      string dir = _tempDecryptPath;

      try
      {
        FileSecurity fss = File.GetAccessControl(dir);
        fss.RemoveAccessRule(new FileSystemAccessRule(WindowsIdentity.GetCurrent().Name, FileSystemRights.FullControl, AccessControlType.Deny));
        File.SetAccessControl(dir, fss);
      }
      catch (Exception ex)
      {
        _logger.WriteError(ex);
        return false;
      }

      return true;
    }

    private static long GetPhysicalRam()
    {
      try
      {
        ObjectQuery winQuery = new ObjectQuery("SELECT * FROM Win32_ComputerSystem");
        ManagementObjectSearcher searcher = new ManagementObjectSearcher(winQuery);
        foreach (ManagementObject item in searcher.Get())
          return long.Parse(item["TotalPhysicalMemory"].ToString());
      }
      catch (Exception ex)
      {
        _logger.WriteError("Cannot get physical memory");
      }

      return -1;
    }
  }
}
