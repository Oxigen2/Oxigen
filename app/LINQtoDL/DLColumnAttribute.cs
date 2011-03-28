using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace LINQtoDL
{
  /// <summary>
  /// Summary description for DLColumnAttribute
  /// </summary>
  [System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Property, AllowMultiple = false)]
  public class DLColumnAttribute : System.Attribute
  {
    internal const int _defaultFieldIndex = Int32.MaxValue;

    public string Name { get; set; }
    public bool CanBeNull { get; set; }
    public int FieldIndex { get; set; }
    public NumberStyles NumberStyle { get; set; }
    public string OutputFormat { get; set; }

    public DLColumnAttribute()
    {
      Name = "";
      FieldIndex = _defaultFieldIndex;
      CanBeNull = true;
      NumberStyle = NumberStyles.Any;
      OutputFormat = "G";
    }

    public DLColumnAttribute(
                string name,
                int fieldIndex,
                bool canBeNull,
                string outputFormat,
                NumberStyles numberStyle)
    {
      Name = name;
      FieldIndex = fieldIndex;
      CanBeNull = canBeNull;
      NumberStyle = numberStyle;
      OutputFormat = outputFormat;
    }
  }
}
