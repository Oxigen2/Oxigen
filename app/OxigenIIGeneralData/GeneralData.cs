using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmlSerializableGenericDictionary;

namespace OxigenIIAdvertising.AppData
{
  public class GeneralData
  {
    private int _softwareMajorVersionNumber;
    private int _softwareMinorVersionNumber;
    private SerializableDictionary<string, string> _properties;
    private SerializableDictionary<string, string> _noServers;

    public GeneralData()
    {
      _properties = new SerializableDictionary<string, string>();
      _noServers = new SerializableDictionary<string, string>();
    }

    /// <summary>
    /// Retrieves the value of a given property in general data
    /// </summary>
    /// <exception cref="KeyNotFoundException">Thrown when given property is not in the Properties collection</exception>
    public SerializableDictionary<string, string> Properties
    {
      get { return _properties; }
      set { _properties = value; }
    }

    /// <summary>
    /// Retrieves the value of a given relay server type, as a string
    /// </summary>
    /// <exception cref="KeyNotFoundException">Thrown when given relay server type does not exist in the NoRelayServers collection</exception>
    public SerializableDictionary<string, string> NoServers
    {
      get { return _noServers; }
      set { _noServers = value; }
    }

    /// <summary>
    /// Major Version Number of the installed software
    /// </summary>
    public int SoftwareMajorVersionNumber
    {
      get { return _softwareMajorVersionNumber; }
      set { _softwareMajorVersionNumber = value; }
    }

    /// <summary>
    /// Minor Version Number of the installed software
    /// </summary>
    public int SoftwareMinorVersionNumber
    {
      get { return _softwareMinorVersionNumber; }
      set { _softwareMinorVersionNumber = value; }
    }

    /// <summary>
    /// Gets the Major.Minor version of the installed software
    /// </summary>
    public string SoftwareVersion
    {
      get
      {
        return String.Format("{0}.{1}", _softwareMajorVersionNumber, _softwareMinorVersionNumber);
      }
    }
  }
}
