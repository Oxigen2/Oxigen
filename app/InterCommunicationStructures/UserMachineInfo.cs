using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterCommunicationStructures
{
  /// <summary>
  /// Stores the basic units of information for a user and a machine they operate
  /// </summary>
  public class UserMachineInfo
  {
    protected string _userGUID;
    protected string _machineGUID;

    /// <summary>
    /// User's unique identifier
    /// </summary>
    public string UserGUID
    {
      get { return _userGUID; }
      set { _userGUID = value; }
    }

    /// <summary>
    /// User's machine's unique identifier
    /// </summary>
    public string MachineGUID
    {
      get { return _machineGUID; }
      set { _machineGUID = value; }
    }
  }
}
