using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxigenIIAdvertising.AppData
{
  /// <summary>
  /// Enumeration of supported asset types
  /// </summary>
  [Serializable]
  public enum PlayerType 
  { 
    /// <summary>
    /// Asset is an image
    /// </summary>
    Image, 

    /// <summary>
    /// Asset is a video clip, non Quicktime Format
    /// </summary>
    VideoNonQT,

    /// <summary>
    /// Asset is a video clip, Quicktime Format
    /// </summary>
    VideoQT, 

    /// <summary>
    /// Asset is a flash object
    /// </summary>
    Flash, 

    /// <summary>
    /// Asset is a website
    /// </summary>
    WebSite,

    /// <summary>
    /// Asset is a "no assets" assets and must be animated
    /// </summary>
    NoAssetsAnimator
  } 
}