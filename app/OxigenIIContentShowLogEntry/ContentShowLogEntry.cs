using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.OxigenIIAsset;

namespace OxigenIIAdvertising.LoggingStructures
{
  /// <summary>
  /// Show log entry that represents content impressions
  /// </summary>
  public sealed class ContentShowLogEntry : ShowLogEntry
  {
    private AssetLevel _assetLevel;

    /// <summary>
    /// Normal or Premium
    /// </summary>
    public AssetLevel AssetLevel
    {
      get { return _assetLevel; }
      set { _assetLevel = value; }
    }

    /// <summary>
    /// Constructor for ContentShowLogEntry
    /// </summary>
    /// <param name="logLine">a pipe-delimited line from the log file</param>
    /// <exception cref="OxigenIIAdvertising.Exceptions.LogDateTimeException">thrown when date parameters are not in the correct format.</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">thrown when index is out of range</exception>
    /// <exception cref="System.ArgumentException">thrown when the asset level input is not of those expected</exception>
    /// <exception cref="System.FormatException">timespan part of the logLine has an invalid format</exception>
    /// <exception cref="System.OverflowException">timespan part of the logLine represents a number less than System.TimeSpan.MinValue or greater than System.TimeSpan.MaxValue.  -or- At least one of the days, hours, minutes, or seconds components is outside its valid range.</exception>
    public ContentShowLogEntry(string logLine)
    {
      InitializeByLogLine(logLine);
    }

    /// <summary>
    /// Parameterless constructor for ContentShowLogEntry
    /// </summary>
    public ContentShowLogEntry() { }

    protected override void InitializeByLogLine(string logLine)
    {
      string[] logElements = logLine.Split('|');

      AssetID = logElements[0];

      _assetLevel = (AssetLevel)Enum.Parse(typeof(AssetLevel), logElements[1]);

      StartDateTime = StringToDateTime(logElements[2], logElements[3]);
      EndDateTime = StringToDateTime(logElements[4], logElements[5]);

      Duration = TimeSpan.Parse(logElements[6]);
    }
  }
}
