using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace LINQtoDL
{
  /// <summary>
  /// Summary description for DLFileDescription
  /// </summary>
  public class DLFileDescription
  {
    // Culture used to process the DL values, specifically numbers and dates.
    private CultureInfo _cultureInfo = CultureInfo.CurrentCulture;

    private int _maximumNbrExceptions = 100;

    // --------------

    // Character used to separate fields in the file.
    // By default, this is comma (,).
    // For a tab delimited file, you would set this to
    // the tab character ('\t').
    public char SeparatorChar { get; set; }

    // Only used when writing a file
    //
    // If true, all fields are quoted whatever their content.
    // If false, only fields containing a FieldSeparator character,
    // a quote or a newline are quoted.
    //
    public bool QuoteAllFields { get; set; }

    // If true, then:
    // When writing a file, the column names are written in the
    // first line of the new file.
    // When reading a file, the column names are read from the first
    // line of the file.
    //
    public bool FirstLineHasColumnNames { get; set; }

    // If true, only public fields and properties with the
    // [DLColumn] attribute are recognized.
    // If false, all public fields and properties are used.
    //
    public bool EnforceDLColumnAttribute { get; set; }

    // FileCultureName and FileCultureInfo both get/set
    // the CultureInfo used for the file.
    // For example, if the file uses Dutch date and number formats
    // while the current culture is US English, set
    // FileCultureName to "nl-NL".
    //
    // To simply use the current culture, leave the culture as is.
    //
    public string FileCultureName
    {
      get { return _cultureInfo.Name; }
      set { _cultureInfo = new CultureInfo(value); }
    }

    public CultureInfo FileCultureInfo
    {
      get { return _cultureInfo; }
      set { _cultureInfo = value; }
    }

    // When reading a file, exceptions thrown while the file is being read
    // are captured in an aggregate exception. That aggregate exception is then
    // thrown at the end - to make it easier to solve multiple problems with the
    // input file in one. 
    //
    // However, after MaximumNbrExceptions, the aggregate exception is thrown
    // immediately.
    //
    // To not have a maximum at all, set this to -1.
    public int MaximumNbrExceptions
    {
      get { return _maximumNbrExceptions; }
      set { _maximumNbrExceptions = value; }
    }

    // Character encoding. Defaults should work in most cases.
    // However, when reading or writing non-English files, you may want to use
    // Unicode encoding.
    public Encoding TextEncoding { get; set; }
    public bool DetectEncodingFromByteOrderMarks { get; set; }


    // ---------------

    public DLFileDescription()
    {
      _cultureInfo = CultureInfo.CurrentCulture;
      FirstLineHasColumnNames = true;
      EnforceDLColumnAttribute = false;
      QuoteAllFields = false;
      SeparatorChar = ',';
      TextEncoding = Encoding.UTF8;
      DetectEncodingFromByteOrderMarks = true;
    }
  }
}
