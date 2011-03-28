using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxigenIIAdvertising.LoggingStructures
{
  /// <summary>
  /// Abstract class to represent click log entries
  /// </summary>
  public class ClickLogEntry : LogEntry
  {
    /// <summary>
    /// Date and time an asset was clicked
    /// </summary>
    protected DateTime _clickDateTime;

    /// <summary>
    /// Sets the log line in the log file. 
    /// Subclasses implement InitializeByLogLine(), which fills the properties of the log entry
    /// from the elements of the linear pipe delimited list which is the LogLine
    /// </summary>
    public override string LogLine
    {
      set
      {
        base.LogLine = value;

        InitializeByLogLine(ref value);
      }
    }

    /// <summary>
    /// Date and time an asset was clicked
    /// </summary>
    public DateTime ClickDateTime
    {
      get { return _clickDateTime; }
      set { _clickDateTime = value; }
    }

    /// <summary>
    /// Initializes a log entry. Factors out object construction, which could occur either at constructor
    /// or when setting the logLine property
    /// </summary>
    /// <param name="logLine">one pipe-delimited log line from log file</param>
    /// <exception cref="OxigenIIAdvertising.Exceptions.LogDateTimeException">thrown when date parameters are not in the correct format.</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">thrown when index is out of range</exception>
    /// <exception cref="System.ArgumentException">thrown when the asset level input is not of those expected</exception>
    /// <exception cref="System.FormatException">thrown when the AssetID is in incorrect format</exception>
    protected override void InitializeByLogLine(ref string logLine)
    {
      string[] logElements = logLine.Split('|');

      ChannelID = long.Parse(logElements[0]);
      AssetID = long.Parse(logElements[1]);
      ClickDateTime = StringToDateTime(logElements[2], logElements[3]);
    }

    /// <summary>
    /// Shifts the log entry's date time to the relay server's time zone.
    /// </summary>
    /// <param name="dateTimeDiff">Time difference between the client and the relay server. Positive if relay server's time zone is ahead, negative if it is behind</param>
    public override void TimeCorrect(TimeSpan dateTimeDiff)
    {
      _clickDateTime = _clickDateTime.Add(dateTimeDiff);
    }
  }
}
