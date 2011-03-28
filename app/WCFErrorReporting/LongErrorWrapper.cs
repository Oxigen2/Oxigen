using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ServiceErrorReporting
{
  /// <summary>
  /// Object to hold a long value along with error information, if error is applicable
  /// </summary>
  [DataContract]
  public class LongErrorWrapper : SimpleErrorWrapper
  {
    private long _returnLong;

    /// <summary>
    /// Long value wrapped by the object
    /// </summary>
    [DataMember]
    public long ReturnLong
    {
      get { return _returnLong; }
      set { _returnLong = value; }
    }
  }
}
