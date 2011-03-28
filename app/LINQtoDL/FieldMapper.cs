using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Reflection;
using System.ComponentModel;

namespace LINQtoDL
{
  /// <summary>
  /// Summary description for FieldMapper
  /// </summary>
  internal class FieldMapper<T>
  {
    protected class TypeFieldInfo : IComparable<TypeFieldInfo>
    {
      public int _index = DLColumnAttribute._defaultFieldIndex;
      public string _name = null;
      public bool _canBeNull = true;
      public NumberStyles _inputNumberStyle = NumberStyles.Any;
      public string _outputFormat = null;
      public bool _bHasColumnAttribute = false;

      public MemberInfo _memberInfo = null;
      public Type _fieldType = null;

      // parseNumberMethod will remain null if the property is not a numeric type.
      // This would be the case for DateTime, Boolean, String and custom types.
      // In those cases, just use a TypeConverter.
      //
      // DateTime and Boolean also have Parse methods, but they don't provide
      // functionality that TypeConverter doesn't give you.

      public TypeConverter typeConverter = null;
      public MethodInfo parseNumberMethod = null;

      // ----

      public int CompareTo(TypeFieldInfo other)
      {
        return _index.CompareTo(other._index);
      }
    }

    // -----------------------------

    // IndexToInfo is used to quickly translate the index of a field
    // to its TypeFieldInfo.
    protected TypeFieldInfo[] m_IndexToInfo = null;

    // Used to build IndexToInfo
    protected Dictionary<string, TypeFieldInfo> m_NameToInfo = null;

    protected DLFileDescription m_fileDescription;

    // Only used when throwing an exception
    protected string m_fileName;

    // -----------------------------
    // AnalyzeTypeField
    //
    private TypeFieldInfo AnalyzeTypeField(
                            MemberInfo mi,
                            bool allRequiredFieldsMustHaveFieldIndex,
                            bool allDLColumnFieldsMustHaveFieldIndex)
    {
      TypeFieldInfo tfi = new TypeFieldInfo();

      tfi._memberInfo = mi;

      if (mi is PropertyInfo)
      {
        tfi._fieldType = ((PropertyInfo)mi).PropertyType;
      }
      else
      {
        tfi._fieldType = ((FieldInfo)mi).FieldType;
      }

      // parseNumberMethod will remain null if the property is not a numeric type.
      // This would be the case for DateTime, Boolean, String and custom types.
      // In those cases, just use a TypeConverter.
      //
      // DateTime and Boolean also have Parse methods, but they don't provide
      // functionality that TypeConverter doesn't give you.

      tfi.parseNumberMethod =
          tfi._fieldType.GetMethod("Parse",
              new Type[] { typeof(String), typeof(NumberStyles), typeof(IFormatProvider) });

      tfi.typeConverter = null;
      if (tfi.parseNumberMethod == null)
      {
        tfi.typeConverter =
            TypeDescriptor.GetConverter(tfi._fieldType);
      }

      // -----
      // Process the attributes

      tfi._index = DLColumnAttribute._defaultFieldIndex;
      tfi._name = mi.Name;
      tfi._inputNumberStyle = NumberStyles.Any;
      tfi._outputFormat = "";
      tfi._bHasColumnAttribute = false;

      foreach (Object attribute in mi.GetCustomAttributes(typeof(DLColumnAttribute), true))
      {
        DLColumnAttribute cca = (DLColumnAttribute)attribute;

        if (!string.IsNullOrEmpty(cca.Name))
        {
          tfi._name = cca.Name;
        }

        tfi._index = cca.FieldIndex;
        tfi._bHasColumnAttribute = true;
        tfi._canBeNull = cca.CanBeNull;
        tfi._outputFormat = cca.OutputFormat;
        tfi._inputNumberStyle = cca.NumberStyle;
      }

      // -----

      if (allDLColumnFieldsMustHaveFieldIndex &&
          tfi._bHasColumnAttribute &&
          tfi._index == DLColumnAttribute._defaultFieldIndex)
      {
        throw new ToBeWrittenButMissingFieldIndexException(
                        typeof(T).ToString(),
                        tfi._name);
      }

      if (allRequiredFieldsMustHaveFieldIndex &&
          (!tfi._canBeNull) &&
          (tfi._index == DLColumnAttribute._defaultFieldIndex))
      {
        throw new RequiredButMissingFieldIndexException(
                        typeof(T).ToString(),
                        tfi._name);
      }

      // -----

      return tfi;
    }

    // -----------------------------
    // AnalyzeType
    //
    protected void AnalyzeType(
                    Type type,
                    bool allRequiredFieldsMustHaveFieldIndex,
                    bool allDLColumnFieldsMustHaveFieldIndex)
    {
      m_NameToInfo.Clear();

      // ------
      // Initialize NameToInfo

      foreach (MemberInfo mi in type.GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
      {
        // Only process field and property members.
        if ((mi.MemberType == MemberTypes.Field) ||
            (mi.MemberType == MemberTypes.Property))
        {
          // Note that the compiler does not allow fields and/or properties
          // with the same name as some other field or property.
          TypeFieldInfo tfi =
              AnalyzeTypeField(
                      mi,
                      allRequiredFieldsMustHaveFieldIndex,
                      allDLColumnFieldsMustHaveFieldIndex);

          m_NameToInfo[tfi._name] = tfi;
        }
      }

      // -------
      // Initialize IndexToInfo

      int nbrTypeFields = m_NameToInfo.Keys.Count;
      m_IndexToInfo = new TypeFieldInfo[nbrTypeFields];

      int i = 0;
      foreach (KeyValuePair<string, TypeFieldInfo> kvp in m_NameToInfo)
      {
        m_IndexToInfo[i++] = kvp.Value;
      }

      // Sort by FieldIndex. Fields without FieldIndex will 
      // be sorted towards the back, because their FieldIndex
      // is Int32.MaxValue.
      //
      // The sort order is important when reading a file that 
      // doesn't have the field names in the first line, and when
      // writing a file. 
      //
      // Note that for reading from a file with field names in the 
      // first line, method ReadNames reworks IndexToInfo.

      Array.Sort(m_IndexToInfo);

      // ----------
      // Make sure there are no duplicate FieldIndices.
      // However, allow gaps in the FieldIndex range, to make it easier to later insert
      // fields in the range.

      int lastFieldIndex = Int32.MinValue;
      string lastName = "";
      foreach (TypeFieldInfo tfi in m_IndexToInfo)
      {
        if ((tfi._index == lastFieldIndex) &&
            (tfi._index != DLColumnAttribute._defaultFieldIndex))
        {
          throw new DuplicateFieldIndexException(
                      typeof(T).ToString(),
                      tfi._name,
                      lastName,
                      tfi._index);
        }

        lastFieldIndex = tfi._index;
        lastName = tfi._name;
      }
    }

    /// ///////////////////////////////////////////////////////////////////////
    /// FieldMapper
    /// 
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="fileDescription"></param>
    public FieldMapper(DLFileDescription fileDescription, string fileName, bool writingFile)
    {
      if ((!fileDescription.FirstLineHasColumnNames) &&
          (!fileDescription.EnforceDLColumnAttribute))
      {
        throw new DLColumnAttributeRequiredException();
      }

      // ---------

      m_fileDescription = fileDescription;
      m_fileName = fileName;

      m_NameToInfo = new Dictionary<string, TypeFieldInfo>();

      AnalyzeType(
          typeof(T),
          !fileDescription.FirstLineHasColumnNames,
          writingFile && !fileDescription.FirstLineHasColumnNames);
    }

    /// ///////////////////////////////////////////////////////////////////////
    /// WriteNames
    /// 
    /// <summary>
    /// Writes the field names given in T to row.
    /// </summary>
    /// 
    public void WriteNames(ref List<string> row)
    {
      row.Clear();

      for (int i = 0; i < m_IndexToInfo.Length; i++)
      {
        TypeFieldInfo tfi = m_IndexToInfo[i];

        if (m_fileDescription.EnforceDLColumnAttribute &&
                (!tfi._bHasColumnAttribute))
        {
          continue;
        }

        // ----

        row.Add(tfi._name);
      }
    }


    /// ///////////////////////////////////////////////////////////////////////
    /// WriteObject
    /// 
    public void WriteObject(T obj, ref List<string> row)
    {
      row.Clear();

      for (int i = 0; i < m_IndexToInfo.Length; i++)
      {
        TypeFieldInfo tfi = m_IndexToInfo[i];

        if (m_fileDescription.EnforceDLColumnAttribute &&
                (!tfi._bHasColumnAttribute))
        {
          continue;
        }

        // ----

        Object objValue = null;

        if (tfi._memberInfo is PropertyInfo)
        {
          objValue =
              ((PropertyInfo)tfi._memberInfo).GetValue(obj, null);
        }
        else
        {
          objValue =
              ((FieldInfo)tfi._memberInfo).GetValue(obj);
        }

        // ------

        string resultString = null;
        if (objValue != null)
        {
          if ((objValue is IFormattable))
          {
            resultString =
                ((IFormattable)objValue).ToString(
                    tfi._outputFormat,
                    m_fileDescription.FileCultureInfo);
          }
          else
          {
            resultString = objValue.ToString();
          }
        }

        // -----

        row.Add(resultString);
      }
    }
  }

  /// ///////////////////////////////////////////////////////////////////////
  // To do reading, the object needs to create an object of type T
  // to read the data into. This requires the restriction T : new()
  // However, for writing, you don't want to impose that restriction.
  //
  // So, use FieldMapper (without the restriction) for writing,
  // and derive a FieldMapper_Reading (with restrictions) for reading.
  //
  internal class FieldMapper_Reading<T> : FieldMapper<T> where T : new()
  {
    /// ///////////////////////////////////////////////////////////////////////
    /// FieldMapper
    /// 
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="fileDescription"></param>
    public FieldMapper_Reading(
                DLFileDescription fileDescription,
                string fileName,
                bool writingFile)
      : base(fileDescription, fileName, writingFile)
    {
    }


    /// ///////////////////////////////////////////////////////////////////////
    /// ReadNames
    /// 
    /// <summary>
    /// Assumes that the fields in parameter row are field names.
    /// Reads the names into the objects internal structure.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="firstRow"></param>
    /// <returns></returns>
    ///
    public void ReadNames(List<DataRowItem> row)
    {
      // It is now the order of the field names that determines
      // the order of the elements in m_IndexToInfo, instead of
      // the FieldIndex fields.

      // If there are more names in the file then fields in the type,
      // one of the names will not be found, causing an exception.

      for (int i = 0; i < row.Count; i++)
      {
        if (!m_NameToInfo.ContainsKey(row[i].Value))
        {
          // name not found
          throw new NameNotInTypeException(typeof(T).ToString(), row[i].Value, m_fileName);
        }

        // ----

        m_IndexToInfo[i] = m_NameToInfo[row[i].Value];

        if (m_fileDescription.EnforceDLColumnAttribute &&
            (!m_IndexToInfo[i]._bHasColumnAttribute))
        {
          // enforcing column attr, but this field/prop has no column attr.
          throw new MissingDLColumnAttributeException(typeof(T).ToString(), row[i].Value, m_fileName);
        }
      }
    }

    /// ///////////////////////////////////////////////////////////////////////
    /// ReadObject
    /// 
    /// <summary>
    /// Creates an object of type T from the data in row and returns that object.
    /// 
    /// </summary>
    /// <param name="row"></param>
    /// <param name="firstRow"></param>
    /// <returns></returns>
    public T ReadObject(List<DataRowItem> row, AggregatedException ae)
    {
      if (row.Count > m_IndexToInfo.Length)
      {
        // Too many fields
        throw new TooManyDataFieldsException(typeof(T).ToString(), row[0].LineNbr, m_fileName);
      }

      // -----

      T obj = new T();

      for (int i = 0; i < row.Count; i++)
      {
        TypeFieldInfo tfi = m_IndexToInfo[i];

        if (m_fileDescription.EnforceDLColumnAttribute &&
                (!tfi._bHasColumnAttribute))
        {
          // enforcing column attr, but this field/prop has no column attr.
          // So there are too many fields in this record.
          throw new TooManyNonDLColumnDataFieldsException(typeof(T).ToString(), row[i].LineNbr, m_fileName);
        }

        // -----

        if ((!m_fileDescription.FirstLineHasColumnNames) &&
                (tfi._index == DLColumnAttribute._defaultFieldIndex))
        {
          // First line in the file does not have field names, so we're 
          // depending on the FieldIndex of each field in the type
          // to ensure each value is placed in the correct field.
          // However, now hit a field where there is no FieldIndex.
          throw new MissingFieldIndexException(typeof(T).ToString(), row[i].LineNbr, m_fileName);
        }

        // -----

        // value to put in the object
        string value = row[i].Value;

        if (value == null)
        {
          if (!tfi._canBeNull)
          {
            ae.AddException(
                new MissingRequiredFieldException(
                        typeof(T).ToString(),
                        tfi._name,
                        row[i].LineNbr,
                        m_fileName));
          }
        }
        else
        {
          try
          {
            Object objValue = null;

            // Normally, either tfi.typeConverter is not null,
            // or tfi.parseNumberMethod is not null. 
            // 
            if (tfi.typeConverter != null)
            {
              objValue = tfi.typeConverter.ConvertFromString(
                              null,
                              m_fileDescription.FileCultureInfo,
                              value);
            }
            else if (tfi.parseNumberMethod != null)
            {
              objValue =
                  tfi.parseNumberMethod.Invoke(
                      tfi._fieldType,
                      new Object[] { 
                                    value, 
                                    tfi._inputNumberStyle, 
                                    m_fileDescription.FileCultureInfo });
            }
            else
            {
              // No TypeConverter and no Parse method available.
              // Try direct approach.
              objValue = value;
            }

            if (tfi._memberInfo is PropertyInfo)
            {
              ((PropertyInfo)tfi._memberInfo).SetValue(obj, objValue, null);
            }
            else
            {
              ((FieldInfo)tfi._memberInfo).SetValue(obj, objValue);
            }
          }
          catch (Exception e)
          {
            if (e is TargetInvocationException)
            {
              e = e.InnerException;
            }

            if (e is FormatException)
            {
              e = new WrongDataFormatException(
                      typeof(T).ToString(),
                      tfi._name,
                      value,
                      row[i].LineNbr,
                      m_fileName,
                      e);
            }

            ae.AddException(e);
          }
        }
      }

      // Visit any remaining fields in the type for which no value was given
      // in the data row, to see whether any of those was required.
      // If only looking at fields with DLColumn attribute, do ignore
      // fields that don't have that attribute.

      for (int i = row.Count; i < m_IndexToInfo.Length; i++)
      {
        TypeFieldInfo tfi = m_IndexToInfo[i];

        if (((!m_fileDescription.EnforceDLColumnAttribute) ||
             tfi._bHasColumnAttribute) &&
            (!tfi._canBeNull))
        {
          ae.AddException(
              new MissingRequiredFieldException(
                      typeof(T).ToString(),
                      tfi._name,
                      row[row.Count - 1].LineNbr,
                      m_fileName));
        }
      }

      return obj;
    }
  }
}
