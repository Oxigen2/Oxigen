 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.AppData;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.AppData 
{
  /// <summary>
  /// Contains a HashSet of that contain Channel objects
  /// </summary>
  public class ChannelData
  {
    private HashSet<Channel> _channels = null;

    /// <summary>
    /// The HashSet of Channel objects
    /// </summary>
    public HashSet<Channel> Channels
    {
      get { return _channels; }
      set { _channels = value; }
    }

    /// <summary>
    /// Initializing a ChannelData object will initialize the underlying HashSet
    /// </summary>
    public ChannelData()
    {
      _channels = new HashSet<Channel>();
    }
  }
}
