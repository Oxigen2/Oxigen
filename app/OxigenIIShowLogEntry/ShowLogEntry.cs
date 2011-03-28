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
  public abstract class ShowLogEntry : LogEntry
  {
    private DateTime _startDateTime;
    private DateTime _endDateTime;
    private TimeSpan _duration;

    /// <summary>
    /// Time the asset started to play
    /// </summary>
    public DateTime StartDateTime
    {
      get { return _startDateTime; }
      set { _startDateTime = value; }
    }

    /// <summary>
    /// Time the asset finished playing
    /// </summary>
    public DateTime EndDateTime
    {
      get { return _endDateTime; }
      set { _endDateTime = value; }
    }

    /// <summary>
    /// Duration of play
    /// </summary>
    public TimeSpan Duration
    {
      get { return _duration; }
      set { _duration = value; }
    }

    /// <summary>
    /// Parameterless constructor for ShowLogEntry class
    /// </summary>
    protected ShowLogEntry() { }

        /// <summary>
    /// base constructor that takes the pipe-delimited string logLine as a parameter
    /// </summary>
    /// <param name="logLine">the pipe-delimited string from the log file</param>
    protected ShowLogEntry(string logLine)
    {
      InitializeByLogLine(logLine);
    }

    /// <summary>
    /// Initializes a log entry. Factors out object construction, which could occur either at constructor
    /// or when setting the logLine property
    /// </summary>
    /// <param name="logLine">one pipe-delimited log line from log file</param>
    protected abstract override void InitializeByLogLine(string logLine);
  }
}
