using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.SOAStructures
{
  /// <summary>
  /// Structure that holds information for a list of channels and the number of pages for all channels
  /// under a category. that number does not always correspond to the number of channels in the Channel list.
  /// </summary>
  [DataContract]
  public class PageChannelData
  {
    private int _noPages;
    private List<ChannelListChannel> _channels;

    /// <summary>
    /// Number of pages for all channels under a category under which the channels in Channels are.
    /// </summary>
    [DataMember]
    public int NoPages
    {
      get { return _noPages; }
      set { _noPages = value; }
    }

    /// <summary>
    /// List of Channels udner a certain category
    /// </summary>
    [DataMember]
    public List<ChannelListChannel> Channels
    {
      get { return _channels; }
      set { _channels = value; }
    }

    public PageChannelData() { }

    public PageChannelData(int noPages, List<ChannelListChannel> channels)
    {
      _noPages = noPages;
      _channels = channels;
    }
  }
}
