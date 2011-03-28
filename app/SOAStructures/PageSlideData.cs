using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.SOAStructures
{
  /// <summary>
  /// Represents a structure of slides under a folder and number of pages under the same folder
  /// </summary>
  [DataContract]
  public class PageSlideData
  {
    private int _noPages;
    private List<SlideListSlide> _slides;
    private string _channelThumbnailPath;

    [DataMember]
    public int NoPages
    {
      get { return _noPages; }
      set { _noPages = value; }
    }

    [DataMember]
    public List<SlideListSlide> Slides
    {
      get { return _slides; }
      set { _slides = value; }
    }

    [DataMember]
    public string ChannelThumbnailPath
    {
      get { return _channelThumbnailPath; }
      set { _channelThumbnailPath = value; }
    }
  }
}
