using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxigenIIAdvertising.OxigenIIStopwatch
{
  /// <summary>
  /// Thread-safe stopwatch that uses the system's clock
  /// </summary>
  public class Stopwatch
  {
    private long _startValue = 0;
    private long _endValue = 0;
    private object _lockObject = new object();
    private bool _bIsRunning;
    
    /// <summary>
    /// Gets the total elapsed time in milliseconds
    /// </summary>
    public double ElapsedTotalMilliseconds
    {
      get 
      {
        lock (_lockObject)
        {
          if (!_bIsRunning)
            return _endValue - _startValue;

          _endValue = DateTime.Now.Ticks;

          return (_endValue - _startValue) / 10000L;
        }
      }
    }

    /// <summary>
    /// Gets a value indicating whether the stopwatch is running
    /// </summary>
    public bool IsRunning
    {
      get 
      { 
        lock (_lockObject)
          return _bIsRunning; 
      }
    }

    /// <summary>
    /// Starts the stopwatch
    /// </summary>
    public void Start()
    {
      lock (_lockObject)
      {
        if (!_bIsRunning)
        {
          _startValue = DateTime.Now.Ticks;

          _bIsRunning = true;
        }
      }
    }

    /// <summary>
    /// Stop the stopwatch
    /// </summary>
    public void Stop()
    {
      lock (_lockObject)
      {
        if (!_bIsRunning)
        {
          _endValue = DateTime.Now.Ticks;

          _bIsRunning = false;
        }
      }
    }

    /// <summary>
    /// Resets the stopwatch without restarting it
    /// </summary>
    public void Reset()
    {
      lock (_lockObject)
      {
        _startValue = 0;
        _endValue = 0;

        _bIsRunning = false;
      }
    }
  }
}
