using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.OxigenIIAsset;

namespace OxigenIIAdvertising.LoggingStructures
{
  /// <summary>
  /// Click log entry that represent content clicks 
  /// </summary>
  public sealed class ContentClickLogEntry : ClickLogEntry
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
    /// Constructs a click log entry for content assets
    /// </summary>
    /// <param name="logLine">a pipe-delimited line from the log file</param>
    /// <exception cref="OxigenIIAdvertising.Exceptions.LogDateTimeException">thrown when date parameters are not in the correct format.</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">thrown when index is out of range</exception>
    /// <exception cref="System.ArgumentException">thrown when the asset level input is not of those expected</exception>
    public ContentClickLogEntry(string logLine)
    {
      InitializeByLogLine(logLine);
    }

    /// <summary>
    /// Parameterless constructor for content click log entry
    /// </summary>
    public ContentClickLogEntry() { }

    protected override void InitializeByLogLine(string logLine)
    {
      string[] logElements = logLine.Split('|');

      AssetID = logElements[0];

      _assetLevel = (AssetLevel)Enum.Parse(typeof(AssetLevel), logElements[1]);

      ClickDateTime = StringToDateTime(logElements[2], logElements[3]);
    }
  }
}
