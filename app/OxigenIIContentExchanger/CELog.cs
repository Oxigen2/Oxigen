using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OxigenIIAdvertising.ContentExchanger
{
  /// <summary>
  /// Class to save/load content exchanger log to display in Verbose Mode
  /// </summary>
  public class CELog
  {
    private DateTime _currentRun;
    private DateTime _lastRun;
    private DateTime _lastDownloadedContent;
    private string _lastStatus;
    private string _logPath = System.Configuration.ConfigurationManager.AppSettings["AppDataPath"] + "SettingsData\\CE.dat";
    private bool _bHasLog = false;
    private bool _bContentDownloaded = false;
    private bool _bExitedWithErrors = false;

    /// <summary>
    /// Date/time the application last run
    /// </summary>
    public DateTime LastRun
    {
      get { return _lastRun; }
    }

    /// <summary>
    /// Date/time the application last downloaded content
    /// </summary>
    public DateTime LastDownloadedContent
    {
      get { return _lastDownloadedContent; }
    }

    /// <summary>
    /// Status of the last execution of the application
    /// </summary>
    public string LastStatus
    {
      get { return _lastStatus; }
    }

    /// <summary>
    /// Returns true if log file has been found and is of the correct format.
    /// </summary>
    public bool HasLog
    {
      get { return _bHasLog; }
      set { _bHasLog = value; }
    }

    public CELog(DateTime currentRun, bool bContentDownloaded, bool bExitedWithErrors)
    {
      _currentRun = currentRun;
      _bContentDownloaded = bContentDownloaded;
      _bExitedWithErrors = bExitedWithErrors;
    }

    public CELog() { }

    public void SaveLog()
    {
      DateTime currentRun = DateTime.Now;
      DateTime currentDownloadedContent;

      string currentStatus = _bExitedWithErrors ? "Failed" : "Succeeded";

      if (_bContentDownloaded)
        currentDownloadedContent = currentRun;
      else
      {
        LoadLog();

        if (_bHasLog)
          currentDownloadedContent = _lastDownloadedContent;
        else
         currentDownloadedContent = new DateTime();
      }

      try
      {
        File.WriteAllText(_logPath, _currentRun.ToString() + "," + currentDownloadedContent.ToString() + "," + currentStatus);
      }
      catch
      {
        // ignore;
      }
    }
    
    public void LoadLog()
    {
      if (!File.Exists(_logPath))
      {
        _bHasLog = false;
        return;
      }

      string log;

      try
      {
        log = File.ReadAllText(_logPath);
      }
      catch
      {
        // can't read file, abort
        return;
      }

      string[] logElements = log.Split(new char[] { ',' });

      if (logElements.Length < 3)
        return;

      if (!DateTime.TryParse(logElements[0], out _lastRun))
        return;

      if (!DateTime.TryParse(logElements[1], out _lastDownloadedContent))
        return;

      _lastStatus = logElements[2];
      
      _bHasLog = true;      
    }
  }
}
