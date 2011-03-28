using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ServiceErrorReporting
{
  /// <summary>
  /// Object to hold a string value along with error information, if error is applicable
  /// </summary>
  [DataContract]
  public class StringErrorWrapper : SimpleErrorWrapper
  {
    private string _returnString;

    /// <summary>
    /// String value wrapped by the object
    /// </summary>
    [DataMember]
    public string ReturnString
    {
      get { return _returnString; }
      set { _returnString = value; }
    }
  }
}
