using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxigenIIAdvertising.TimeSpanObjectWrapper
{
  /// <summary>
  /// Provides a wrapper class for the non-nullable TimeSpan struct so it can checked for nulls.
  /// </summary>
  [Serializable]
  public class TimeSpanWrapperClass
  {
    private TimeSpan _timeSpan;

    /// <summary>
    /// The TimeSpan property of the TimeSpanWrapperClass.
    /// </summary>
    public TimeSpan TimeSpan
    {
      get { return _timeSpan; }
      set { _timeSpan = value; }
    }

    public TimeSpanWrapperClass(TimeSpan timeSpan)
    {
      _timeSpan = timeSpan;
    }
  }
}
