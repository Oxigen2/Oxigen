using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OxigenIIAdvertising.Exceptions;

namespace OxigenIIAdvertising.LoggingStructures
{
  /// <summary>
  /// Abstract class that represents an entry in the log file.
  /// </summary>
  public abstract class LogEntry
  {
    /// <summary>
    /// channel ID the asset was selected from
    /// </summary>
    private long _channelID;

    /// <summary>
    /// advert or content ID to log.
    /// </summary>
    protected long _assetID;

    /// <summary>
    /// Sets the log line in the log file. 
    /// Subclasses implement InitializeByLogLine(), which fills the properties of the log entry
    /// from the elements of the linear pipe delimited list which is the LogLine
    /// </summary>
    /// <exception cref="InitializationSequenceException"></exception>
    /// <exception cref="AlreadyDefinedException"></exception>
    public virtual string LogLine
    {
      set 
      {
        // if the log entry is set, the _assetID will have a value other than 0.
        if (_assetID != 0L)
          throw new AlreadyDefinedException("LogEntry object has already been initialized and changing its LogLine property would change its other properties");
      }
    }

    /// <summary>
    /// channel ID the asset was selected from
    /// </summary>
    public long ChannelID
    {
      get { return _channelID; }
      set { _channelID = value; }
    }

    /// <summary>
    /// advert or content ID to log.
    /// </summary>
    public long AssetID
    {
      get { return _assetID; }
      set { _assetID = value; }
    }

    /// <summary>
    /// Creates a datetime object from a string of the format YYYYMMDD and a string of the format HH:MM:SS
    /// </summary>
    /// <param name="stringRepresentedDate">the YYYYMMDD string to convert</param>
    /// <param name="stringRepresentedTime">the time string to convert to time. it must be of the format HH:MM:SS</param>
    /// <returns>a DateTime object with the date part filled in</returns>
    /// <exception cref="LogDateTimeException">thrown when date parameters are not in the correct format.</exception>
    protected DateTime StringToDateTime(string stringRepresentedDate, string stringRepresentedTime)
    {
      int year = GetDateTimePartValueFromString(stringRepresentedDate, DatePart.Year);
      int month = GetDateTimePartValueFromString(stringRepresentedDate, DatePart.Month);
      int day = GetDateTimePartValueFromString(stringRepresentedDate, DatePart.Day);
      int hour = GetDateTimePartValueFromString(stringRepresentedTime, DatePart.Hour);
      int minute = GetDateTimePartValueFromString(stringRepresentedTime, DatePart.Minute);
      int second = GetDateTimePartValueFromString(stringRepresentedTime, DatePart.Second);
           
      DateTime datetime = new DateTime();

      try
      {
        datetime = new DateTime(year, month, day, hour, minute, second);
      }
      catch (Exception ex)
      {
        throw new LogDateTimeException("Error converting date from log file", ex);
      }

      return datetime;
    }

    private int GetDateTimePartValueFromString(string stringRepresentedDate, DatePart datepart)
    {
      int i = -1;
      int startIndex = -1;
      int length = -1;

      switch (datepart)
      {
        case DatePart.Second:
          startIndex = 6;
          length = 2;
          break;
        case DatePart.Minute:
          startIndex = 3;
          length = 2;
          break;
        case DatePart.Hour:
          startIndex = 0;
          length = 2;
          break;
        case DatePart.Day:
          startIndex = 6;
          length = 2;
          break;
        case DatePart.Month:
          startIndex = 4;
          length = 2;
          break;
        case DatePart.Year:
          startIndex = 0;
          length = 4;
          break;
      }

      try
      {
        i = int.Parse(stringRepresentedDate.Substring(startIndex, length));
      }
      catch (Exception ex)
      {
        throw new LogDateTimeException(datepart.ToString() + " was not found in correct format", ex);
      }

      return i;
    }

    /// <summary>
    /// Initializes a log entry. Factors out object construction, which could occur either at constructor
    /// or when setting the logLine property. Determines if a log line is marked as 'dirty' (new antry after last run of the Log Exchanger).
    /// 
    /// </summary>
    /// <param name="logLine">one pipe-delimited log line from log file</param>
    protected abstract void InitializeByLogLine(ref string logLine);

    /// <summary>
    /// Shifts the log entry's date time to the relay server's time zone.
    /// </summary>
    /// <param name="dateTimeDiff">Time difference between the client and the relay server. Positive if relay server's time zone is ahead, negative if it is behind</param>
    public abstract void TimeCorrect(TimeSpan dateTimeDiff);

    private enum DatePart
    {
      Second,
      Minute,
      Hour,
      Day,
      Month,
      Year
    }
  }
}
