using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.SOAStructures
{
  /// <summary>
  /// Represnets a slide folder
  /// </summary>
  [DataContract]
  public class SlideFolder
  {
    private int _slideFolderID;
    private string _slideFolderName;
    private List<SlideListSlide> _slides;

    /// <summary>
    /// The unique row ID for the slide folder
    /// </summary>
    [DataMember]
    public int SlideFolderID
    {
      get { return _slideFolderID; }
      set { _slideFolderID = value; }
    }

    /// <summary>
    /// The name of the slide folder
    /// </summary>
    [DataMember]
    public string SlideFolderName
    {
      get { return _slideFolderName; }
      set { _slideFolderName = value; }
    }

    /// <summary>
    /// List of the slides contained in the slide folder
    /// </summary>
    [DataMember]
    public List<SlideListSlide> Slides
    {
      get { return _slides; }
      set { _slides = value; }
    }
  }
}
