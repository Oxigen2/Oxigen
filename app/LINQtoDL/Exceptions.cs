﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQtoDL
{
  /// <summary>
  /// All exceptions have a human readable message in the Message property,
  /// and machine readable data in the Data property.
  /// </summary>
  public class LINQtoDLException : Exception
  {
    public LINQtoDLException(
                string message,
                Exception innerException)
      : base(message, innerException)
    {
    }

    public LINQtoDLException(
                string message)
      : base(message)
    {
    }

    // ----

    public static string FileNameMessage(string fileName)
    {
      return ((fileName == null) ? "" : " Reading file \"" + fileName + "\".");
    }
  }

  /// ///////////////////////////////////////////////////////////////////////
  /// Exceptions not related to the input file.

  /// <summary>
  /// Thrown when the stream passed to Read is either null, or does not support Seek.
  /// It has to support Seek, because that way it can rewind when the stream is accessed again.
  /// </summary>
  public class BadStreamException : LINQtoDLException
  {
    public BadStreamException() :
      base(
            "Stream provided to Read is either null, or does not support Seek.")
    {
    }
  }

  /// <summary>
  /// DLFileDescription.FirstLineHasColumnNames is false, then the only way
  /// to reliably identify data fields is by their order in the data file.
  /// To get that order, the Read and Write methods look at the FieldIndex property
  /// of the DLColumn attribute of the fields/properties of the data class.
  /// 
  /// However, if DLFileDescription.EnforceDLColumnAttribute is false,
  /// then that implies that fields/properties that don't have the DLColumn attribute
  /// (and therefore no FieldIndex), participate in reading and writing.
  /// 
  /// When this inconsistency within the DLFileDescription object is detected,
  /// this exception is thrown.
  /// </summary>
  public class DLColumnAttributeRequiredException : LINQtoDLException
  {
    public DLColumnAttributeRequiredException() :
      base(
            "DLFileDescription.EnforceDLColumnAttribute is false, but needs to be true because " +
            "DLFileDescription.FirstLineHasColumnNames is false. See the description for DLColumnAttributeRequiredException.")
    {
    }
  }

  /// <summary>
  /// Thrown when 2 or more fields or properties have the same FieldIndex in the DLColumn attribute.
  /// </summary>
  public class DuplicateFieldIndexException : LINQtoDLException
  {
    public DuplicateFieldIndexException(
                string typeName,
                string fieldName,
                string fieldName2,
                int duplicateIndex) :
      base(string.Format(
            "Fields or properties \"{0}\" and \"{1}\" of type \"{2}\" have duplicate FieldIndex {3}.",
            fieldName,
            fieldName2,
            typeName,
            duplicateIndex))
    {
      Data["TypeName"] = typeName;
      Data["FieldName"] = fieldName;
      Data["FieldName2"] = fieldName2;
      Data["Index"] = duplicateIndex;
    }
  }

  /// <summary>
  /// Thrown when there are no names in the first line, so each field assigned to must have a FieldIndex,
  /// but there is a field that is both required (CanBeNull is false) and that doesn't have a FieldIndex.
  /// </summary>
  public class RequiredButMissingFieldIndexException : LINQtoDLException
  {
    public RequiredButMissingFieldIndexException(
                string typeName,
                string fieldName) :
      base(string.Format(
            "Field or property \"{0}\" of type \"{1}\" is required, but does not have a FieldIndex. " +
            "This exception only happens for files without column names in the first record.",
            fieldName,
            typeName))
    {
      Data["TypeName"] = typeName;
      Data["FieldName"] = fieldName;
    }
  }

  /// <summary>
  /// Thrown when a field will be written to a file that has no names in the first line,
  /// but that field has no FieldIndex.
  /// </summary>
  public class ToBeWrittenButMissingFieldIndexException : LINQtoDLException
  {
    public ToBeWrittenButMissingFieldIndexException(
                string typeName,
                string fieldName) :
      base(string.Format(
            "Field or property \"{0}\" of type \"{1}\" will be written to a file, but does not have a FieldIndex. " +
            "This exception only happens for input files without column names in the first record.",
            fieldName,
            typeName))
    {
      Data["TypeName"] = typeName;
      Data["FieldName"] = fieldName;
    }
  }

  /// ///////////////////////////////////////////////////////////////////////
  /// Exceptions related to reading column names from file

  /// <summary>
  /// Thrown when the file has a column name without a counterpart in the data class definition
  /// </summary>
  public class NameNotInTypeException : LINQtoDLException
  {
    public NameNotInTypeException(string typeName, string fieldName, string fileName) :
      base(string.Format(
                "The input file has column name \"{0}\" in the first record, but there is no field or property with that name in type \"{1}\"." +
                FileNameMessage(fileName),
                fieldName,
                typeName))
    {
      Data["TypeName"] = typeName;
      Data["FieldName"] = fieldName;
      Data["FileName"] = fileName;
    }
  }

  /// <summary>
  /// Thrown when the file has a field name that corresponds to a field in the type,
  /// but that field does not have the DLColumn attribute while only fields with that
  /// attribute can be used according to the DLFileDescription.
  /// </summary>
  public class MissingDLColumnAttributeException : LINQtoDLException
  {
    public MissingDLColumnAttributeException(string typeName, string fieldName, string fileName) :
      base(string.Format(
                 "Field \"{0}\" in type \"{1}\" does not have the DLColumn attribute." +
                 FileNameMessage(fileName),
                 fieldName,
                 typeName))
    {
      Data["TypeName"] = typeName;
      Data["FieldName"] = fieldName;
      Data["FileName"] = fileName;
    }
  }

  /// ///////////////////////////////////////////////////////////////////////
  /// Exceptions related to reading data from file

  /// <summary>
  /// Thrown when a data record has too many fields.
  /// 
  /// All TooManyDataFieldsExceptions get aggregated into
  /// an AggregatedException.
  /// </summary>
  public class TooManyDataFieldsException : LINQtoDLException
  {
    public TooManyDataFieldsException(string typeName, int lineNbr, string fileName) :
      base(string.Format(
                 "Line {0} has more fields then are available in type \"{1}\"." +
                 FileNameMessage(fileName),
                 lineNbr,
                 typeName))
    {
      Data["TypeName"] = typeName;
      Data["LineNbr"] = lineNbr;
      Data["FileName"] = fileName;
    }
  }

  /// <summary>
  /// Thrown when a data record has more fields then there are fields in the type with
  /// the DLColumn attribute.
  /// 
  /// All  these Exceptions get aggregated into
  /// an AggregatedException.
  /// </summary>
  public class TooManyNonDLColumnDataFieldsException : LINQtoDLException
  {
    public TooManyNonDLColumnDataFieldsException(string typeName, int lineNbr, string fileName) :
      base(string.Format(
                 "Line {0} has more fields then there are fields or properties in type \"{1}\" with the DLColumn attribute set." +
                 FileNameMessage(fileName),
                 lineNbr,
                 typeName))
    {
      Data["TypeName"] = typeName;
      Data["LineNbr"] = lineNbr;
      Data["FileName"] = fileName;
    }
  }

  /// <summary>
  /// Thrown when a data field has no corresponding field in the type with a FieldIndex.
  /// This means there is no guarantee that the data field will be assigned to the right
  /// field in the type.
  /// 
  /// All MissingFieldIndexExceptions get aggregated into
  /// an AggregatedException.
  /// </summary>
  public class MissingFieldIndexException : LINQtoDLException
  {
    public MissingFieldIndexException(string typeName, int lineNbr, string fileName) :
      base(string.Format(
             "Line {0} has more fields then there are fields or properties in type \"{1}\" with a FieldIndex." +
             FileNameMessage(fileName),
             lineNbr,
             typeName))
    {
      Data["TypeName"] = typeName;
      Data["LineNbr"] = lineNbr;
      Data["FileName"] = fileName;
    }
  }

  /// <summary>
  /// Thrown when a data field is empty, while its DLColumn attribute has CanBeNull=false
  /// 
  /// All WrongDataFormatExceptions get aggregated into
  /// an AggregatedException.
  /// </summary>
  public class MissingRequiredFieldException : LINQtoDLException
  {
    public MissingRequiredFieldException(
                    string typeName,
                    string fieldName,
                    int lineNbr,
                    string fileName) :
      base(
             string.Format(
                 "In line {0}, no value provided for required field or property \"{1}\" in type \"{2}\"." +
                 FileNameMessage(fileName),
                 lineNbr,
                 fieldName,
                 typeName))
    {
      Data["TypeName"] = typeName;
      Data["LineNbr"] = lineNbr;
      Data["FileName"] = fileName;
      Data["FieldName"] = fieldName;
    }
  }

  /// <summary>
  /// Thrown when a data field has the wrong format (for example a number field with letters).
  /// 
  /// All WrongDataFormatExceptions get aggregated into
  /// an AggregatedException.
  /// </summary>
  public class WrongDataFormatException : LINQtoDLException
  {
    public WrongDataFormatException(
                    string typeName,
                    string fieldName,
                    string fieldValue,
                    int lineNbr,
                    string fileName,
                    Exception innerExc) :
      base(
             string.Format(
                 "Value \"{0}\" in line {1} has the wrong format for field or property \"{2}\" in type \"{3}\"." +
                 FileNameMessage(fileName),
                 fieldValue,
                 lineNbr,
                 fieldName,
                 typeName),
             innerExc)
    {
      Data["TypeName"] = typeName;
      Data["LineNbr"] = lineNbr;
      Data["FileName"] = fileName;
      Data["FieldValue"] = fieldValue;
      Data["FieldName"] = fieldName;
    }
  }

  /// <summary>
  /// Thrown when one or more Exceptions were raised while
  /// reading data record from a file.
  /// 
  /// Contains a List with all the Exceptions.
  /// </summary>
  public class AggregatedException : LINQtoDLException
  {
    public List<Exception> m_InnerExceptionsList;
    private int m_MaximumNbrExceptions = 100;

    // -----

    public AggregatedException(string typeName, string fileName, int maximumNbrExceptions) :
      base(string.Format(
             "There were 1 or more exceptions while reading data using type \"{0}\"." +
             FileNameMessage(fileName),
             typeName))
    {
      m_MaximumNbrExceptions = maximumNbrExceptions;
      m_InnerExceptionsList = new List<Exception>();

      Data["TypeName"] = typeName;
      Data["FileName"] = fileName;
      Data["InnerExceptionsList"] = m_InnerExceptionsList;
    }

    // -----

    public void AddException(Exception e)
    {
      m_InnerExceptionsList.Add(e);
      if ((m_MaximumNbrExceptions != -1) &&
          (m_InnerExceptionsList.Count >= m_MaximumNbrExceptions))
      {
        throw this;
      }
    }

    // -----

    public void ThrowIfExceptionsStored()
    {
      if (m_InnerExceptionsList.Count > 0)
      {
        throw this;
      }
    }
  }
}
