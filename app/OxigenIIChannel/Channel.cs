using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace OxigenIIChannel
{
  public class Channel
  {
    private int _channelID;
    private Collection<Channel> _channels;

    public Collection<Channel> Channels
    {
      get { return _channels; }
      set { _channels = value; }
    }

    public int ChannelID
    {
      get { return _channelID; }
      set { _channelID = value; }
    }

    public Channel(int channelID)
    {
      _channelID = channelID;
      _channels = new Collection<Channel>();
    }    
  }
}
