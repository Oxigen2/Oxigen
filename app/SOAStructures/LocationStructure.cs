using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.SOAStructures
{  
  [DataContract]
  public struct LocationNameValue
  {
    private string _name;
    private int _value;
    private bool _bHasChildren;

    [DataMember]
    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }

    [DataMember]
    public int Value
    {
      get { return _value; }
      set { _value = value; }
    }

    [DataMember]
    public bool HasChildren
    {
      get { return _bHasChildren; }
      set { _bHasChildren = value; }
    }

    public LocationNameValue(string name, int value, bool bHasChildren)
    {
      _name = name;
      _value = value;
      _bHasChildren = bHasChildren;
    }
  }
}
