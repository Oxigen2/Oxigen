using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using OxigenIIAdvertising.InclusionExclusionRules;
using XmlSerializableSortableGenericList;

namespace OxigenIIAdvertising.AppData
{
  [Serializable]
  public class Asset
  {
    protected long _assetID;
    protected PlayerType _playerType;
    protected string _assetFilename;
    protected float _displayDuration;
    protected string _assetWebSite;
    protected string _clickDestination;
    protected string[] _scheduleInfo;
    protected DateTime _startDateTime;
    protected DateTime _endDateTime;
    private string[] _demoRequirements;

    /// <summary>
    /// The Unique ID of the Asset
    /// </summary>
    public long AssetID
    {
      get { return _assetID; }
      set { _assetID = value; }
    }

    /// <summary>
    /// Which player plays this Asset
    /// </summary>
    public PlayerType PlayerType
    {
      get { return _playerType; }
      set { _playerType = value; }
    }

    /// <summary>
    /// The file name of the asset without the path
    /// </summary>
    public string AssetFilename
    {
      get { return _assetFilename; }
      set { _assetFilename = value; }
    }

    /// <summary>
    /// Display duration
    /// </summary>
    public float DisplayDuration
    {
      get { return _displayDuration; }
      set { _displayDuration = value; }
    }

    /// <summary>
    /// Click-through URL to take the user to while this asset is being played
    /// </summary>
    public string ClickDestination
    {
      get { return _clickDestination; }
      set { _clickDestination = value; }
    }

    /// <summary>
    /// Temporal scheduling
    /// </summary>
    public string[] ScheduleInfo
    {
      get { return _scheduleInfo; }
      set { _scheduleInfo = value; }
    }

    /// <summary>
    /// Gets or sets the lower date and time the asset can be shown
    /// </summary>
    public DateTime StartDateTime
    {
      get { return _startDateTime; }
      set { _startDateTime = value; }
    }

    /// <summary>
    /// Gets or sets the upper date and time the asset must be shown
    /// </summary>
    public DateTime EndDateTime
    {
      get { return _endDateTime; }
      set { _endDateTime = value; }
    }

    /// <summary>
    /// Gets or sets the web site to show on the screen saver for web assets
    /// </summary>
    public string AssetWebSite
    {
      get { return _assetWebSite; }
      set { _assetWebSite = value; }
    }

    /// <summary>
    /// Demographic Data conditions that must be met for the asset to be played
    /// </summary>
    public string[] DemoRequirements
    {
      get { return _demoRequirements; }
      set { _demoRequirements = value; }
    }
  }
}