using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.Exceptions;

namespace OxigenIIAdvertising.LoggingStructures
{
  /// <summary>
  /// Abstract class that represents an show entry in the log file.
  /// </summary>
  public class ImpressionLogEntry : LogEntry
  {
    /// <summary>
    /// Time the asset started to play
    /// </summary>
    protected DateTime _startDateTime;

    /// <summary>
    /// Duration of play
    /// </summary>
    protected int _duration;

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
    /// Time the asset started to play
    /// </summary>
    public DateTime StartDateTime
    {
      get { return _startDateTime; }
      set { _startDateTime = value; }
    }

    /// <summary>
    /// Duration of play
    /// </summary>
    public int Duration
    {
      get { return _duration; }
      set { _duration = value; }
    }

    /// <summary>
    /// Initializes a log entry. Factors out object construction, which could occur either at constructor
    /// or when setting the logLine property
    /// </summary>
    /// <param name="logLine">one pipe-delimited log line from log file</param>
    /// <exception cref="OxigenIIAdvertising.Exceptions.LogDateTimeException">thrown when date parameters are not in the correct format.</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">thrown when index is out of range</exception>
    /// <exception cref="System.ArgumentException">thrown when the asset level input is not of those expected</exception>
    /// <exception cref="System.FormatException">integer part of the logLine has an invalid format or the AssetID is in incorrect format</exception>
    /// <exception cref="System.OverflowException">integer part of the logLine represents a number less than System.Int32.MinValue or greater than System.Int32.MaxValue.  -or- At least one of the days, hours, minutes, or seconds components is outside its valid range.</exception>
    protected override void InitializeByLogLine(ref string logLine)
    {
      string[] logElements = logLine.Split('|');

      ChannelID = long.Parse(logElements[0]);
      AssetID = long.Parse(logElements[1]);
      StartDateTime = StringToDateTime(logElements[2], logElements[3]);

      Duration = int.Parse(logElements[4]);
    }

    /// <summary>
    /// Shifts the log entry's date time to the relay server's time zone.
    /// </summary>
    /// <param name="dateTimeDiff">Time difference between the client and the relay server. Positive if relay server's time zone is ahead, negative if it is behind</param>
    public override void TimeCorrect(TimeSpan dateTimeDiff)
    {
      _startDateTime = _startDateTime.Add(dateTimeDiff);
    }
  }
}
