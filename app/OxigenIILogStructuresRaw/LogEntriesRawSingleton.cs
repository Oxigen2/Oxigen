using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.LogStats;

namespace OxigenIIAdvertising.Singletons
{
  /// <summary>
  /// Singleton object to hold logs written to by the screensaver
  /// </summary>
  public sealed class LogEntriesRawSingleton
  {
    private static volatile LogEntriesRawSingleton _instance;
    private static object _lockObject = new Object();
    
    private List<string> _advertClickLogEntries = null;
    private List<string> _advertImpressionLogEntries = null;
    private List<string> _contentClickLogEntries = null;
    private List<string> _contentImpressionLogEntries = null;
  
    /// <summary>
    /// Gets Advert click log entries. Thread-safe
    /// </summary>
    public List<string> AdvertClickLogEntries
    {
      get 
      {
        lock (_lockObject)
          return _advertClickLogEntries; 
      }
    }

    /// <summary>
    /// Gets Advert Impression logo entries. Thread-safe
    /// </summary>
    public List<string> AdvertImpressionLogEntries
    {
      get 
      { 
        lock (_lockObject)
          return _advertImpressionLogEntries; 
      }
    }

    /// <summary>
    /// Gets Content Click log entries. Thread-safe
    /// </summary>
    public List<string> ContentClickLogEntries
    {
      get 
      {
        lock (_lockObject)
          return _contentClickLogEntries; 
      }
    }

    /// <summary>
    /// Gets Content Impression log entries. Thread-safe
    /// </summary>
    public List<string> ContentImpressionLogEntries
    {
      get
      {
        lock (_lockObject)
          return _contentImpressionLogEntries; 
      }
    }

    private LogEntriesRawSingleton()
    {
      _advertClickLogEntries = new List<string>();
      _advertImpressionLogEntries = new List<string>();
      _contentClickLogEntries = new List<string>();
      _contentImpressionLogEntries = new List<string>();
    }

    /// <summary>
    /// Creates a Singleton object to store log entries for the screensaver
    /// </summary>
    public static LogEntriesRawSingleton Instance 
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
              _instance = new LogEntriesRawSingleton();
          }
        }

        return _instance;
      }
    }

    /// <summary>
    /// Moves the content of one collection of the Singleton to an array.
    /// </summary>
    /// <param name="logs">List of the logs to copy and clear</param>
    /// <returns>an array of strings with the elements previously contained in the array</returns>
    public static string[] MoveElementsToArray(List<string> logs)
    {
      if (logs == null)
        return null;

      string[] array = null;

      lock (_lockObject)
      {
        array = new string[logs.Count];

        logs.CopyTo(array);

        logs.Clear();
      }

      return array;
    }

    /// <summary>
    /// Dereferences the singleton object to make it ready for the Garbage Collector
    /// </summary>
    public static void Reset()
    {
      _instance = null;
    }
  }
}
