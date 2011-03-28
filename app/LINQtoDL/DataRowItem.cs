using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQtoDL
{
  internal class DataRowItem
  {
    private string _value;
    private int _lineNbr;

    public DataRowItem(string value, int lineNbr)
    {
      _value = value;
      _lineNbr = lineNbr;
    }

    public int LineNbr
    {
      get { return _lineNbr; }
    }

    public string Value
    {
      get { return _value; }
    }
  }
}
