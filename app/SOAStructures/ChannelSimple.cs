using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using OxigenIIAdvertising.DataAccess;
using System.Data.SqlClient;

namespace OxigenIIAdvertising.SOAStructures
{
  [DataContract]
  public class ChannelSimple
  {
    private int _channelID;
    private string _channelName;
    private List<SlideListSlide> _slides;

    [DataMember]
    public int ChannelID
    {
      get { return _channelID; }
      set { _channelID = value; }
    }

    [DataMember]
    public string ChannelName
    {
      get { return _channelName; }
      set { _channelName = value; }
    }

    [DataMember]
    public List<SlideListSlide> Slides
    {
      get { return _slides; }
      set { _slides = value; }
    }
  }
}
