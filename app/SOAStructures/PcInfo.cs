using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.SOAStructures
{
  [DataContract]
  public class PcInfo
  {
    private int _pcID;
    private string _pcName;

    [DataMember]
    public int PcID
    {
      get { return _pcID; }
      set { _pcID = value; }
    }

    [DataMember]
    public string PcName
    {
      get { return _pcName; }
      set { _pcName = value; }
    }
  }
}
