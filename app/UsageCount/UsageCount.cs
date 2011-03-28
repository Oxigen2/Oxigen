using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxigenIIAdvertising
{
  /// <summary>
  /// Structure to hold the total counter of impressions and clicks by the screensaver
  /// </summary>
  public class UsageCount
  {
    private string _machineGUID;
    private string _userGUID;
    private int _noClicks;
    private int _noScreenSaverSessions;
    private int _totalPlayTime;

    /// <summary>
    /// The machine's unique identifier, needed to identify the machine that produced the logs
    /// </summary>
    public string MachineGUID
    {
      get { return _machineGUID; }
      set { _machineGUID = value; }
    }

    /// <summary>
    /// The user's unique identifier, used to identify the user under which the screen saver has run
    /// </summary>
    public string UserGUID
    {
      get { return _userGUID; }
      set { _userGUID = value; }
    }

    /// <summary>
    /// Gets or sets the number of clicks in the screen saver
    /// </summary>
    public int NoClicks
    {
      get { return _noClicks; }
      set { _noClicks = value; }
    }

    /// <summary>
    /// Gets or sets the total number of times that the screen saver has run
    /// </summary>
    public int NoScreenSaverSessions
    {
      get { return _noScreenSaverSessions; }
      set { _noScreenSaverSessions = value; }
    }

    /// <summary>
    /// Gets or sets the total time that the screen saver has run
    /// </summary>
    public int TotalPlayTime
    {
      get { return _totalPlayTime; }
      set { _totalPlayTime = value; }
    }

    /// <summary>
    /// Default constructor
    /// </summary>
    public UsageCount() { }
  }
}
