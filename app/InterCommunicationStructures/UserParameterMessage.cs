using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace InterCommunicationStructures
{
  /// <summary>
  /// The minimum amount of information to be used as a web method parameter for methods that use message contracts between the relay and user clients.
  /// </summary>
  [MessageContract]
  public class UserParameterMessage
  {
    protected string _systemPassPhrase;
    protected string _userGUID;
    protected string _machineGUID;

    /// <summary>
    /// Pass phrase that identifies the consumer to the web service
    /// </summary>
    [MessageHeader]
    public string SystemPassPhrase
    {
      get { return _systemPassPhrase; }
      set { _systemPassPhrase = value; }
    }

    /// <summary>
    /// User's unique identifier
    /// </summary>
    [MessageHeader]
    public string UserGUID
    {
      get { return _userGUID; }
      set { _userGUID = value; }
    }

    /// <summary>
    /// User's machine's unique identifier
    /// </summary>
    [MessageHeader]
    public string MachineGUID
    {
      get { return _machineGUID; }
      set { _machineGUID = value; }
    }
  }
}
