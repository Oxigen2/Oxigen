using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace InterCommunicationStructures
{
  [DataContract]
  public class MachineVersionInfo
  {
    private string _machineGUID;
    private DateTime _versionUpdatedAt;
    private int _majorVersionNumber;
    private int _minorVersionNumber;

    public MachineVersionInfo(string machineGuid, DateTime versionUpdatedAt, int majorVersionNumber, int minorVersionNumber)
    {
      _machineGUID = machineGuid;
      _versionUpdatedAt = versionUpdatedAt;
      _majorVersionNumber = majorVersionNumber;
      _minorVersionNumber = minorVersionNumber;
    }

    public MachineVersionInfo()
    {
    }

    [DataMember]
    public string MachineGUID
    {
      get { return _machineGUID; }
      set { _machineGUID = value; }
    }

    [DataMember]
    public DateTime VersionUpdatedAt
    {
      get { return _versionUpdatedAt; }
      set { _versionUpdatedAt = value; }
    }

    [DataMember]
    public int MajorVersionNumber
    {
      get { return _majorVersionNumber; }
      set { _majorVersionNumber = value; }
    }

    [DataMember]
    public int MinorVersionNumber
    {
      get { return _minorVersionNumber; }
      set { _minorVersionNumber = value; }
    }
  }
}
