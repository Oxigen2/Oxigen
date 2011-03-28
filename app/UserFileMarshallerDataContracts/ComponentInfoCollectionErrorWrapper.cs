using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceErrorReporting;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.DataContracts.UserFileMarshaller
{
  /// <summary>
  /// Object to hold user's registration data along with error information, if error is applicable
  /// </summary>
  [DataContract]
  public class ComponentInfoCollectionErrorWrapper : SimpleErrorWrapper
  {
    private HashSet<ComponentInfo> _returnComponentInfoCollection;

    /// <summary>
    /// UserMain object with user registration data
    /// </summary>
    [DataMember]
    public HashSet<ComponentInfo> ReturnComponentInfoCollection
    {
      get { return _returnComponentInfoCollection; }
      set { _returnComponentInfoCollection = value; }
    }
  }  
}
