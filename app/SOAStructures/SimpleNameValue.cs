using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.SOAStructures
{
  [DataContract]
  public class SimpleNameValue
  {
    private int _value;
    private string _name;

    [DataMember]
    public int Value
    {
      get { return _value; }
      set { _value = value; }
    }

    [DataMember]
    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }

    public SimpleNameValue(string name, int value)
    {
      _name = name;
      _value = value;
    }
  }
}
