using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterCommunicationStructures
{
  /// <summary>
  /// Component Info
  /// </summary>
  [Serializable]
  public class ComponentInfo
  {
    private string _file;
    private int _majorVersionNumber;
    private int _minorVersionNumber;
    private ComponentLocation _location;

    /// <summary>
    /// File name
    /// </summary>
    public string File
    {
      get { return _file; }
      set { _file = value; }
    }

    /// <summary>
    /// Component's Major Version Number
    /// </summary>
    public int MajorVersionNumber
    {
      get { return _majorVersionNumber; }
      set { _majorVersionNumber = value; }
    }

    /// <summary>
    /// Component's Minor Version Number
    /// </summary>
    public int MinorVersionNumber
    {
      get { return _minorVersionNumber; }
      set { _minorVersionNumber = value; }
    }

    /// <summary>
    /// Destination location of the component in the target machine
    /// </summary>
    public ComponentLocation Location
    {
      get { return _location; }
      set { _location = value; }
    }
  }

  [Serializable]
  public enum ComponentLocation { BinaryFolder, SystemFolder }
}
