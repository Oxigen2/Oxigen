using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxigenService
{
  /// <summary>
  /// Encapsulates the Screensaver guardian timer logic
  /// </summary>
  public class OxigenTimer : System.Timers.Timer
  {
    private double _protectedInterval = -1D;
    private double _safetyInterval = -1D;
    private bool _intervalReadOnce;
    private string _intervalPropertyOnGeneralSettings;

    /// <summary>
    /// The first interval the timer needs to conform to when the guardian starts
    /// </summary>
    public double ProtectedInterval
    {
      get { return _protectedInterval; }
      set { _protectedInterval = value; }
    }

    /// <summary>
    /// Interval that timer will run at if ss_general_data.dat is unavailable
    /// </summary>
    public double SafetyInterval
    {
      get { return _safetyInterval; }
      set { _safetyInterval = value; }
    }

    /// <summary>
    /// Gets or sets whether the interval for this timer has been read once.
    /// </summary>
    public bool IntervalsReadOnce
    {
      get { return _intervalReadOnce; }
      set { _intervalReadOnce = value; }
    }

    /// <summary>
    /// The name of the interval property for this timer on ss_general_data.dat
    /// </summary>
    public string IntervalPropertyOnGeneralSettings
    {
      get { return _intervalPropertyOnGeneralSettings; }
      set { _intervalPropertyOnGeneralSettings = value; }
    }

    public OxigenTimer(string intervalPropertyOnGeneralSettings) : base() 
    {
      _intervalPropertyOnGeneralSettings = intervalPropertyOnGeneralSettings;
    }
  }
}
