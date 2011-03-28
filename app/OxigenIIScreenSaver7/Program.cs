using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using OxigenIIAdvertising.ScreenSaver7.Properties;
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

namespace OxigenIIAdvertising.ScreenSaver7
{
  static class Program
  {
    private static Playlist _playlist;
    private static Logger _logger = null;
    private static System.Timers.Timer _logTimer; // every interval, save log files to disk so they don't clutter into memory
    private static float _advertDisplayThreshold = -1;
    private static int _protectedContentTime = -1;
    private static int _noAssetDisplayLength = -1;
    private static int _requestTimeout = -1;
    private static int _logTimerInterval = -1;
    private static DateTime _startDateTime = DateTime.Now;
    private static bool _bPreviewMode = false;
    private static string _errorMessage = "";
    private static bool _bErrorOnSetup = false; // errors during setup? process command line but show 'error' screen saver
    private static bool _bAlreadyWritingLogs = false;
    private static ScreenSaver[] _screenSavers = null;
    private static User _user = null;

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
      GetUserSettings();

      if (!_bErrorOnSetup)
      {
        // no error occurred on setup, now can find debug file
        _logger = new Logger("ScreenSaver Main", _user.FilePaths["DebugFilePath"], LoggingMode.Debug);

        // is there read/ write access to log and conf files 
        // TODO: in windows 7, file access is different
        //FileAccessRights();

        // retrieve admin-set values needed for the operation of the screensaver
        GetGeneralDataValues();

        // retrieve the playlist
        GetPlaylist();
      }

      if (!_bErrorOnSetup)
        SetLogTimer();

      if (args.Length > 0)
      {
        string firstArgument = args[0].ToLower().Trim();
        string firstArgumentConfSwitch = firstArgument.Substring(0, 2);

        // TODO: check culture info for arguments
        switch (firstArgumentConfSwitch)
        {
          case "/s":
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ShowScreenSaver();
            Application.Run();
            break;

          case "/p":
            // preview the screen saver
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //args[1] is the handle to the preview window
            SetScreenSaverPreviewMode(args[1]);
            Application.Run(_screenSavers[0]);
            break;

          case "/c":
            //configure the screen saver
            Application.EnableVisualStyles();
            System.Diagnostics.Process.Start(_user.FilePaths["ScreenSaverConfigApp"]);
            break;

          default:
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
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        ShowScreenSaver();
        Application.Run();
      }
    }

    /// <summary>
    /// Gets the user settings
    /// </summary>
    private static void GetUserSettings()
    {
      try
      {
        _user = (User)Serializer.Deserialize(typeof(User), Settings.Default.UserSettingsPath, true, Settings.Default.Password);
      }
      catch
      {
        // have not acquired Logger data at the moment, exception cannot be written into the debug file
        _errorMessage = Resources.FatalError;
        _bErrorOnSetup = true;
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
        _playlist = (Playlist)Serializer.Deserialize(typeof(Playlist), _user.FilePaths["PlaylistPath"], true, Settings.Default.Password);
      }
      catch (Exception ex)
      {
        _logger.WriteError(ex);
        _errorMessage = Resources.NoPlaylist;
        _bErrorOnSetup = true;
      }
    }

    private static void FileAccessRights()
    {
      if (!FileRightsChecker.AreFilesReadableWritable(_user.FilePaths["AppDataPath"]))
      {
        _errorMessage = Resources.NoLogFileRights;
        _bErrorOnSetup = true;
      }
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
        generalData = (GeneralData)Serializer.Deserialize(typeof(GeneralData), _user.FilePaths["GeneralDataPath"], true, Settings.Default.Password);
      }
      catch (Exception ex)
      {
        _logger.WriteError(ex);
        _errorMessage = Resources.NoGeneralData;
        _bErrorOnSetup = true;
        return;
      }

      if (!ParseNumericalGeneralProperties(generalData))
        _bErrorOnSetup = true;
    }

    private static bool ParseNumericalGeneralProperties(GeneralData generalData)
    {
      if (!TryParseFloat(generalData, "advertDisplayThreshold", out _advertDisplayThreshold))
        return false;

      if (!TryParseInteger(generalData, "logTimerInterval", out _logTimerInterval))
        return false;

      if (!TryParseInteger(generalData, "protectedContentTime", out _protectedContentTime))
        return false;

      if (!TryParseInteger(generalData, "noAssetDisplayLength", out _noAssetDisplayLength))
        return false;

      if (!TryParseInteger(generalData, "requestTimeout", out _requestTimeout))
        return false;

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
        _errorMessage = Resources.ParameterNorParseable;
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
        _errorMessage = Resources.ParameterNorParseable;
        return false;
      }

      return true;
    }

    private static bool GeneralDataContainsKey(GeneralData generalData, string key)
    {
      if (!generalData.Properties.ContainsKey(key))
      {
        _logger.WriteError(key + " not found");
        _errorMessage = Resources.GeneralDataParameterNotFound;
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
    }

    static void logTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      WriteLogs(false);
    }

    public static void TerminateApplication(bool bClickThroughPressed)
    {
      // hide screensavers and log last impressed asset (as it won't have run its full circle)
      foreach (ScreenSaver screensaver in _screenSavers)
        screensaver.Hide();

      // Stop logging/background work here lest displaying and, more importantly, logging
      // continue running when the screen savers are hidden.
      foreach (ScreenSaver screensaver in _screenSavers)
        screensaver.StopWork();

      if (!_bPreviewMode)
      {
        // if spacebar was hit, pop up browser windows and write click logs
        if (bClickThroughPressed)
        {
          foreach (ScreenSaver screenSaver in _screenSavers)
          {
            string clickThroughURL = screenSaver.GetClickThroughURL();

            if (clickThroughURL.StartsWith("app://"))
            {
              switch (clickThroughURL)
              {
                case "app://ContentExchanger":
                  // TODO: find content exchanger path and run it
                  break;
                case "app://SoftwareInstaller":
                  // TODO: run software isntaller in repair mode
                  break;
              }
            }

            if (clickThroughURL.StartsWith("http://") || clickThroughURL.StartsWith("htpps://"))
            {
              // click log to memory
              screenSaver.AddClickLog();

              // pop up browser with click through URL
              System.Diagnostics.Process.Start(clickThroughURL);
            }
          }
        }

        WriteLogs(true);
      }

      // dispose of all forms, for each monitor
      foreach (ScreenSaver screensaver in _screenSavers)
      {
        // in preview mode, the form is closed when user presses OK, Cancel or Preview,
        // and raising the OnClose event again here would result in an infinite loop with TerminateApplication.
        if (!_bPreviewMode)
          screensaver.Close();

        screensaver.DisposeForm();
      }

      // local cleanup
      _playlist = null;

      _logTimer.Stop();
      _logTimer.Dispose();
      _logTimer = null;

      // exit application
      Application.Exit();
    }

    private static void WriteLogs(bool bFinalWrite)
    {
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
        LogFileWriter.WriteLogs(_user.FilePaths["AdvertClickLogsPath1"],
                                _user.FilePaths["AdvertClickLogsPath2"],
                                _user.FilePaths["AdvertImpressionLogsPath1"],
                                _user.FilePaths["AdvertImpressionLogsPath2"],
                                _user.FilePaths["ContentClickLogsPath1"],
                                _user.FilePaths["ContentClickLogsPath2"],
                                _user.FilePaths["ContentImpressionLogsPath1"],
                                _user.FilePaths["ContentImpressionLogsPath2"],
                                _user.FilePaths["UsagePath1"],
                                _user.FilePaths["UsagePath2"],
                                _user.MachineGUID,
                                _user.UserGUID,
                                playTime,
                                Settings.Default.Password,
                                bFinalWrite,
                                _bPreviewMode);
      }
      catch (Exception ex)
      {
        _logger.WriteError(ex);
      }

      if (bFinalWrite)
        LogEntriesRawSingleton.Reset();

      _bAlreadyWritingLogs = false;
    }

    /// <summary>
    /// Starts up one screen saver per monitor
    /// </summary>
    private static void ShowScreenSaver()
    {
      // how many monitors has the user got?
      int screenCount = Screen.AllScreens.Length;

      _screenSavers = new ScreenSaver[screenCount];

      for (int i = 0; i < screenCount; i++)
      {
        ScreenSaver screenSaver = new ScreenSaver(i, _playlist, _advertDisplayThreshold, _protectedContentTime, _noAssetDisplayLength, _requestTimeout, _user);
        _screenSavers[i] = screenSaver;

        screenSaver.Show();
      }

      // capture events
      Application.AddMessageFilter(new UserInputFilter());
    }

    private static void SetScreenSaverPreviewMode(string previewHandle)
    {
      _bPreviewMode = true;
      _screenSavers = new ScreenSaver[1];
      _screenSavers[0] = new ScreenSaver(new IntPtr(long.Parse(previewHandle)), _playlist, _advertDisplayThreshold, _protectedContentTime, _noAssetDisplayLength, _requestTimeout, _user);
    }
  }
}