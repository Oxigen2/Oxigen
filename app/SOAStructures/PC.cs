using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using OxigenIIAdvertising.DataAccess;
using System.Data.SqlClient;

namespace OxigenIIAdvertising.SOAStructures
{
  /// <summary>
  /// Represents an user's machine
  /// </summary>
  [Serializable]
  [DataContract]
  public class PC
  {
    private int _pcID;
    private string _name;
    private bool _bLinkedToClient;
    private List<ChannelListChannel> _channels;

    /// <summary>
    /// The unique database ID of the user's machine
    /// </summary>
    [DataMember]
    public int PCID
    {
      get { return _pcID; }
      set { _pcID = value; }
    }

    /// <summary>
    /// The machine's name
    /// </summary>
    [DataMember]
    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }

    /// <summary>
    /// Sets or sets whether this PC has a real-world counterpart or it is phantom
    /// </summary>
    [DataMember]
    public bool LinkedToClient
    {
      get { return _bLinkedToClient; }
      set { _bLinkedToClient = value; }
    }

    /// <summary>
    /// List of channels assigned to this stream
    /// </summary>
    [DataMember]
    public List<ChannelListChannel> Channels
    {
      get { return _channels; }
      set { _channels = value; }
    }

    public PC() { }

    public PC(int machineID, string name)
    {
      _pcID = machineID;
      _name = name;
    }
  }
}
