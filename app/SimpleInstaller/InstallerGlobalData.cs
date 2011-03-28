using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleInstaller
{
  /// <summary>
  /// Singleton object to hold logs written to by the screensaver
  /// </summary>
  public sealed class InstallerGlobalData
  {
    private static volatile InstallerGlobalData _instance;
    private static object _lockObject = new Object();

    private string _dataPath = "";
    private string _contentExchangerPath = "";
    private string _settingsDataPath = "";

    /// <summary>
    /// Gets or sets the path where Application's data will be stored
    /// </summary>
    public string DataPath
    {
      get 
      { 
        lock (_lockObject)
          return _dataPath;
      }

      set 
      {
        lock (_lockObject)
          _dataPath = value; 
      }
    }

    /// <summary>
    /// Gets or sets the Content Exchanger's path
    /// </summary>
    public string ContentExchangerPath
    {
      get 
      { 
        lock (_lockObject)
          return _contentExchangerPath;
      }

      set 
      { 
        lock (_lockObject)
          _contentExchangerPath = value; 
      }
    }

    /// <summary>
    /// Gets or sets the settings data path
    /// </summary>
    public string SettingsDataPath
    {
      get
      {
        lock (_lockObject)
          return _settingsDataPath;
      }

      set
      {
        lock (_lockObject)
          _settingsDataPath = value;
      }
    }

    /// <summary>
    /// Creates a Singleton object to store log entries for the screensaver
    /// </summary>
    public static InstallerGlobalData Instance
    {
      get
      {
        // first check if instance is null: if it isn't null, do not execute the lock block
        if (_instance == null)
        {
          lock (_lockObject)
          {
            // double check if instance exists as it may have been created between the first if and the lock
            if (_instance == null)
              _instance = new InstallerGlobalData();
          }
        }

        return _instance;
      }
    }
  }
}
