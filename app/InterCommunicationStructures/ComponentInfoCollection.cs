using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterCommunicationStructures
{
  /// <summary>
  /// Retains a user's software version information in a machine
  /// </summary>
  [Serializable]
  public class ComponentInfoCollection : UserMachineInfo
  {
    private HashSet<ComponentInfo> _components;

    /// <summary>
    /// The components this ComponentInfoCollection holds
    /// </summary>
    public HashSet<ComponentInfo> Components
    {
      get { return _components; }
      set { _components = value; }
    }

    /// <summary>
    /// Returns the number of components that this ComponentInfo holds.
    /// </summary>
    public int NoRowsIndex
    {
      get { return _components.Count; }
    } 

    //public ComponentInfoCollection(string userGUID, string machineGUID)
    //{
    //  _userGUID = userGUID;
    //  _machineGUID = machineGUID;

    //  _components = new HashSet<ComponentInfo>();
    //}

    //public ComponentInfoCollection()
    //{
    //  _components = new HashSet<ComponentInfo>();
    //}

    /// <summary>
    /// Flattens a ComponentInfoCollection into a string separated by pipes
    /// </summary>
    /// <returns>a pipe-delimited string that contains the data of a ComponentInfoCollection</returns>
    //public string Flatten()
    //{
    //  StringBuilder sb = new StringBuilder();

    //  foreach (ComponentInfo componentInfo in _components)
    //  {
    //    sb.Append(_userGUID);
    //    sb.Append("|");
    //    sb.Append(_machineGUID);
    //    sb.Append("|");
    //    sb.Append(componentInfo.ComponentFile);
    //    sb.Append("|");
    //    sb.Append(componentInfo.ComponentVersion);
    //    sb.AppendLine();          
    //  }

    //  return sb.ToString();
    //}
  }
}
