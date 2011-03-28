using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.LoggingStructures;
using System.IO;
using OxigenIIAdvertising.XMLSerializer;
using OxigenIIAdvertising.Exceptions;
using OxigenIIAdvertising.FileLocker;
using System.Security.Cryptography;
using OxigenIIAdvertising.LoggerInfo;

namespace OxigenIIAdvertising.LogExchanger
{
  /// <summary>
  /// Provides static methods to read from log file and place a lock to prevent the 
  /// screen saver from accessing the file while log exchanging operations take place
  /// </summary>
  public static class LogFileReader
  {
    /// <summary>
    /// Attempts to read log entries from a file and creates advert log entries with the log entries. If a log file doesn't exist, create it.
    /// If file is locked, try a second path.
    /// </summary>
    /// <param name="fileStream">the FileStream to handle the log file</param>
    /// <param name="decryptionPassword">password to decrypt the log file</param>
    /// <param name="inputPath1">path of the temporary log file 1</param>
    /// <param name="inputPath2">path of the temporary log file 2, in the case log file 1 is locked by the screen saver</param>
    /// <returns>a List with advert click log entries ready for processing, null if no data exist</returns>
    /// <exception cref="System.OutOfMemoryException"></exception>
    /// <exception cref="System.IO.IOException"></exception>
    /// <exception cref="System.IO.PathTooLongException"></exception>
    /// <exception cref="System.IO.IOException"></exception>
    /// <exception cref="System.IO.DirectoryNotFoundException"></exception>
    /// <exception cref="System.UnauthorizedAccessException"></exception>
    /// <exception cref="System.IO.FileNotFoundException"></exception>
    /// <exception cref="System.NotSupportedException"></exception>
    /// <exception cref="System.Security.SecurityException"></exception>
    /// <exception cref="System.ArgumentOutOfRangeException">thrown when index is out of range</exception>
    /// <exception cref="OxigenIIAdvertising.Exceptions.LogDateTimeException">thrown when date parameters are not in the correct format.</exception>
    /// <exception cref="System.ArgumentException">thrown when the asset level input is not of those expected</exception>
    /// <exception cref="System.FormatException">timespan part of the logLine has an invalid format</exception>
    /// <exception cref="System.OverflowException">timespan part of the logLine represents a number less than System.TimeSpan.MinValue or greater than System.TimeSpan.MaxValue.  -or- At least one of the days, hours, minutes, or seconds components is outside its valid range.</exception>
    public static List<T> TryGetEntries<T>(ref FileStream fileStream, string inputPath1, string inputPath2, 
      string decryptionPassword, Logger logger) where T : LogEntry, new()
    {
      List<T> hashT = null;

      try
      {
        hashT = GetEntries<T>(ref fileStream, inputPath1, decryptionPassword);

        logger.WriteMessage(DateTime.Now.ToString() + " successfully read and decrypted " + inputPath1);
      }
      catch (IOException ex)
      {
        logger.WriteError(ex);

        hashT = GetEntries<T>(ref fileStream, inputPath1, decryptionPassword);

        logger.WriteMessage(DateTime.Now.ToString() + " successfully read and decrypted " + inputPath2);
      }

      return hashT;
    }

    /// <summary>
    /// Reads an encrypted log file and creates advert log entries with the log entries. If a log file doesn't exist, create it.
    /// </summary>
    /// <param name="fileStream">the FileStream to handle the log file</param>
    /// <param name="decryptionPassword">password to decrypt the log file</param>
    /// <param name="inputPath">path of the temporary log file</param>
    /// <returns>a List with advert click log entries ready for processing, null if no data exist</returns>
    /// <exception cref="System.OutOfMemoryException"></exception>
    /// <exception cref="System.IO.IOException"></exception>
    /// <exception cref="System.IO.PathTooLongException"></exception>
    /// <exception cref="System.IO.IOException"></exception>
    /// <exception cref="System.IO.DirectoryNotFoundException"></exception>
    /// <exception cref="System.UnauthorizedAccessException"></exception>
    /// <exception cref="System.IO.FileNotFoundException"></exception>
    /// <exception cref="System.NotSupportedException"></exception>
    /// <exception cref="System.Security.SecurityException"></exception>
    /// <exception cref="System.ArgumentOutOfRangeException">thrown when index is out of range</exception>
    /// <exception cref="OxigenIIAdvertising.Exceptions.LogDateTimeException">thrown when date parameters are not in the correct format.</exception>
    /// <exception cref="System.ArgumentException">thrown when the asset level input is not of those expected</exception>
    /// <exception cref="System.FormatException">timespan part of the logLine has an invalid format</exception>
    /// <exception cref="System.OverflowException">timespan part of the logLine represents a number less than System.TimeSpan.MinValue or greater than System.TimeSpan.MaxValue.  -or- At least one of the days, hours, minutes, or seconds components is outside its valid range.</exception>
    private static List<T> GetEntries<T>(ref FileStream fileStream, string inputPath, string decryptionPassword) where T : LogEntry, new()
    {
      List<T> logEntries = new List<T>();

      MemoryStream ms = null;

      try
      {
        ms = Locker.ReadDecryptFile(ref fileStream, inputPath, decryptionPassword, false);
      }
      catch (CryptographicException)
      {
        return logEntries;
      }
      catch (DirectoryNotFoundException)
      {
        return logEntries;
      }

      // no data, exit
      if (ms == null)
        return logEntries;

      StreamReader sr = new StreamReader(ms);

      string logLine = "";

      while (sr.Peek() >= 0)
      {
        try
        {
          logLine = sr.ReadLine();
        }
        catch (Exception ex)
        {        
          sr.Close();
          sr.Dispose();
          ms.Close();
          ms.Dispose();

          throw ex;
        }

        T t = new T();

        t.LogLine = logLine;

        logEntries.Add(t);
      }

      sr.Close();
      sr.Dispose();
      ms.Close();
      ms.Dispose();

      return logEntries;
    }

    /// <summary>
    /// Locks and loads Aggregated file if it exists.
    /// </summary>
    /// <param name="path">Path to the aggregate file</param>
    /// <param name="fs">FileStream reference</param>
    /// <param name="decryptionPassword">password to use to decrypt the file</param>
    /// <returns>a string with the flat aggragated logs if there are any or an empty string if there aren't</returns>
    public static string LoadAggregatedLogFile(string path, ref FileStream fs, string decryptionPassword)
    {
      string existingLogs = "";

      MemoryStream memoryStream = Locker.ReadDecryptFile(ref fs, path, decryptionPassword, false, true);

      if (memoryStream != null)
      {
        try
        {
          existingLogs = GetExistingString(memoryStream);
        }
        catch (Exception ex)
        {
          Locker.ClearFileStream(ref fs);
          throw ex;
        }
        finally
        {
          memoryStream.Close();
          memoryStream.Dispose();
        }
      }

      return existingLogs;
    }

    private static string GetExistingString(MemoryStream memoryStream)
    {
      string existingString = "";

      StreamReader sr = new StreamReader(memoryStream);

      while (sr.Peek() >= 0)
      {
        try
        {
          existingString = sr.ReadToEnd();
        }
        catch (Exception ex)
        {
          sr.Close();
          sr.Dispose();

          throw ex;
        }
      }

      sr.Close();
      sr.Dispose();

      return existingString;
    }
  }
}
