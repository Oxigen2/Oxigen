using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.SOAStructures
{
  [DataContract]
  public class ChannelSlideProperties
  {
    private string _url;
    private float _displayDuration;
    private string _presentationSchedule;

    [DataMember]
    public string URL
    {
      get { return _url; }
      set { _url = value; }
    }

    [DataMember]
    public float DisplayDuration
    {
      get { return _displayDuration; }
      set { _displayDuration = value; }
    }

    [DataMember]
    public string PresentationSchedule
    {
      get { return _presentationSchedule; }
      set { _presentationSchedule = value; }
    }
  }
}
