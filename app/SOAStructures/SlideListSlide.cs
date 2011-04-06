using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using System.Data.SqlClient;

namespace OxigenIIAdvertising.SOAStructures
{
  /// <summary>
  /// Represents a content slide
  /// </summary>
  [Serializable]
  [DataContract]
  public class SlideListSlide
  {
    private int _slideID;
    private string _slideName;
    private string _imagePath;
    private bool _bLocked;

    /// <summary>
    /// The slide's unique database identifier
    /// </summary>
    [DataMember]
    public int SlideID
    {
      get { return _slideID; }
      set { _slideID = value; }
    }

    /// <summary>
    /// Slide's name
    /// </summary>
    [DataMember]
    public string SlideName
    {
      get { return _slideName; }
      set { _slideName = value; }
    }

    /// <summary>
    /// The file path of the thumbnail image to show on the website
    /// </summary>
    [DataMember]
    public string ImagePath
    {
      get { return _imagePath; }
      set { _imagePath = value; }
    }

    /// <summary>
    /// True if the slide is locked
    /// </summary>
    [DataMember]
    public bool Locked
    {
      get { return _bLocked; }
      set { _bLocked = value; }
    }

    public SlideListSlide(int slideID, string slideName, string imagePath, bool bLocked)
    {
      _slideID = slideID;
      _slideName = slideName;
      _imagePath = imagePath;
      _bLocked = bLocked;
    }

    public SlideListSlide() { }
  }
}
