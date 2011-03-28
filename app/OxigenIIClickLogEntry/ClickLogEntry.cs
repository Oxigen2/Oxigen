using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxigenIIAdvertising.LoggingStructures
{
  /// <summary>
  /// Abstract class to represent click log entries
  /// </summary>
  public abstract class ClickLogEntry : LogEntry
  {
    private DateTime _clickDateTime;

    /// <summary>
    /// Date and time an asset was clicked
    /// </summary>
    public DateTime ClickDateTime
    {
      get { return _clickDateTime; }
      set { _clickDateTime = value; }
    }

    /// <summary>
    /// Parameterless constructor for ClickLogentry
    /// </summary>
    protected ClickLogEntry() { }

    /// <summary>
    /// base constructor that takes the pipe-delimited string logLine as a parameter
    /// </summary>
    /// <param name="logLine">the pipe-delimited string from the log file</param>
    protected ClickLogEntry(string logLine)
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
