using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ServiceErrorReporting
{
  /// <summary>
  /// Object to hold a DateTime value along with error information, if error is applicable
  /// </summary>
  [DataContract]
  public class DateTimeErrorWrapper : SimpleErrorWrapper
  {
    private DateTime _returnDateTime;

    /// <summary>
    /// DateTime value wrapped by the object
    /// </summary>
    [DataMember]
    public DateTime ReturnDateTime
    {
      get { return _returnDateTime; }
      set { _returnDateTime = value; }
    }
  }
}
