using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.Singletons;
using System.IO;
using OxigenIIAdvertising.XMLSerializer;
using OxigenIIAdvertising.FileLocker;
using System.Security.Cryptography;
using OxigenIIAdvertising.LoggerInfo;

namespace OxigenIIAdvertising.LogWriter
{
  /// <summary>
  /// Provides static methods for writing log files
  /// </summary>
  public static class LogFileWriter
  {
    /// <summary>
    /// Encrypts and writes the accummulated log entries in the LogEntriesRawSingleton to file
    /// </summary>
    /// <param name="adClickLogsPath1">Path of the first advert click log file to try and write to</param>
    /// <param name="adClickLogsPath2">Path of the second advert click log file to try and write to</param>
    /// <param name="adImpressionLogsPath1">Path of the first advert show log file to try and write to</param>
    /// <param name="adImpressionLogsPath2">Path of the second advert show log file to try and write to</param>
    /// <param name="contentClickLogsPath1">Path of the first content click log file to try and write to</param>
    /// <param name="contentClickLogsPath2">Path of the second content click log file to try and write to</param>
    /// <param name="contentImpressionLogsPath1">Path of the first content show log file to try and write to</param>
    /// <param name="contentImpressionLogsPath2">Path of the second content show log file to try and write to</param>
    /// <param name="usagePath1">Path of the first usage file to try and write to</param>
    /// <param name="usagePath2">Path of the second usage file to try and write to</param>
    /// <param name="playTime">play time (in seconds) in this screensaver session</param>
    /// <param name="bEncrypt">true to encrypt files, false if not. Must default to true. Use false only for debuggign purposes.</param>
    /// <param name="machineGUID">the machine's GUID</param>
    /// <param name="userGUID">the user's GUID</param>
    /// <param name="cryptPassword">encryption/decryption password</param>
    /// <param name="maxLines">maximum size of lines in log files. If numbers of lines in the log file exceed this value, trim it.</param>
    /// <param name="bFinalWrite">Is this the log dump that takes place when screen saver exits. If so, write the log file as well.</param>
    /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters.</exception>
    /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive).</exception>
    /// <exception cref="System.UnauthorizedAccessException">path specified a file that is read-only and access is not Read. -or- path specified a directory. -or- The caller does not have the required permission.</exception>
    /// <exception cref="System.IO.IOException">If no file has write rights (for example when a user deliberately locks the files, an InvalidOperationException will be thrown.</exception>
    /// <exception cref="InvalidOperationException">thrown by XmlSerializer.Deserialize</exception>
    /// <exception cref="IOException">thrown when there is an error during read</exception>
    /// <exception cref="DirectoryNotFoundException">thrown when the directory is not found</exception>
    /// <exception cref="CryptographicException">thrown when encrypted file is corrupted</exception>
    /// <param name="logger">the Logger object to use when debugging exceptions</param>
    public static void WriteLogs(string adClickLogsPath1, string adClickLogsPath2,
      string adImpressionLogsPath1, string adImpressionLogsPath2, 
      string contentClickLogsPath1, string contentClickLogsPath2, 
      string contentImpressionLogsPath1, string contentImpressionLogsPath2, 
      string usagePath1, string usagePath2, 
      string machineGUID, string userGUID, 
      int playTime, string cryptPassword, bool bFinalWrite, bool bPreviewMode, Logger logger)
    {
      // logging accumulation must continue when disk operations are conducted. Copy existing logs.
      // moving of list to string arrays is thread safe
      string[] advertClickLogEntries = LogEntriesRawSingleton.MoveElementsToArray(LogEntriesRawSingleton.Instance.AdvertClickLogEntries);
      string[] advertImpressionLogEntries = LogEntriesRawSingleton.MoveElementsToArray(LogEntriesRawSingleton.Instance.AdvertImpressionLogEntries);
      string[] contentClickLogEntries = LogEntriesRawSingleton.MoveElementsToArray(LogEntriesRawSingleton.Instance.ContentClickLogEntries);
      string[] contentImpressionLogEntries = LogEntriesRawSingleton.MoveElementsToArray(LogEntriesRawSingleton.Instance.ContentImpressionLogEntries);

      logger.WriteMessage(DateTime.Now.ToString() + " successfully moved log structures to array.");

      if (bPreviewMode)
      {
        logger.WriteMessage(DateTime.Now.ToString() + " this is preview mode. Will not write logs to disk.");
        return;
      }

      int noNewClicks = advertClickLogEntries.Length + contentClickLogEntries.Length;

      logger.WriteMessage(DateTime.Now.ToString() + " new clicks: " + noNewClicks);

      WriteRawLogs(adClickLogsPath1, adClickLogsPath2, cryptPassword, advertClickLogEntries, logger);

      logger.WriteMessage(DateTime.Now.ToString() + " successfully written ad click logs.");

      WriteRawLogs(adImpressionLogsPath1, adImpressionLogsPath2, cryptPassword, advertImpressionLogEntries, logger);

      logger.WriteMessage(DateTime.Now.ToString() + " successfully written ad impression logs.");

      WriteRawLogs(contentClickLogsPath1, contentClickLogsPath2, cryptPassword, contentClickLogEntries, logger);

      logger.WriteMessage(DateTime.Now.ToString() + " successfully written content click logs.");

      WriteRawLogs(contentImpressionLogsPath1, contentImpressionLogsPath2, cryptPassword, contentImpressionLogEntries, logger);

      logger.WriteMessage(DateTime.Now.ToString() + " successfully written content impression logs.");

      if (bFinalWrite)
      {
        UpdateUsageCounts(usagePath1, usagePath2, cryptPassword, playTime, noNewClicks, machineGUID, userGUID, logger);
        logger.WriteMessage(DateTime.Now.ToString() + " successfully written usage counts.");
      }
    }

    private static void UpdateUsageCounts(string usagePath1, string usagePath2, string cryptPassword, int playTime, int noNewClicks, string machineGUID, string userGUID, Logger logger)
    {
      FileStream fileStream = null;
      UsageCount usageCount = null;

      try
      {
        usageCount = (UsageCount)Serializer.Deserialize(typeof(UsageCount), usagePath1, cryptPassword, ref fileStream, false, true);

        logger.WriteMessage(DateTime.Now.ToString() + " successfully read existing usage count from path 1.");
      }
      catch (IOException ex)
      {
        logger.WriteError(ex);

        usageCount = (UsageCount)Serializer.Deserialize(typeof(UsageCount), usagePath2, cryptPassword, ref fileStream, false, true);

        logger.WriteMessage(DateTime.Now.ToString() + " successfully read existing usage count from path 2.");
      }

      // if a new UsageCount was created, its numerical values will be set to 0.
      usageCount.MachineGUID = machineGUID;
      usageCount.UserGUID = userGUID;
      usageCount.NoClicks += noNewClicks;
      usageCount.NoScreenSaverSessions++;
      usageCount.TotalPlayTime += playTime;

      logger.WriteMessage(DateTime.Now.ToString() + " successfully updated usage count in memory.");

      Serializer.Serialize(usageCount, cryptPassword, ref fileStream);

      logger.WriteMessage(DateTime.Now.ToString() + " successfully written usage count on disk.");
    }

    /// <summary>
    /// Decrypts a raw log file and appends raw logs at the end of it, then encrypts it and saves it.
    /// </summary>
    /// <param name="path1">Path of first file to try</param>
    /// <param name="path2">Path of second file to try if first file is locked</param>
    /// <param name="cryptPassword">password to use for encryption/decryption</param>
    /// <param name="rawLogEntries">string array with the raw entries to add to the encrypted file</param> 
    /// <param name="maxLines">maximum size of log file. If file size is larger than this value, trim it at a line</param>
    /// <param name="logger">the Logger object to use when debugging exceptions</param>
    private static void WriteRawLogs(string path1, string path2, string cryptPassword, string[] rawLogEntries, Logger logger)
    {
      FileStream fileStream = null;

      // read existing logs and initialize string builder with them
      Queue<string> rawLogs = ReadExistingLogs(ref fileStream, path1, path2, cryptPassword, logger);

      logger.WriteMessage(DateTime.Now.ToString() + " successfully read existing logs.");

      foreach (string rawLogEntry in rawLogEntries)
        rawLogs.Enqueue(rawLogEntry);

      logger.WriteMessage(DateTime.Now.ToString() + " successfully appended new logs to memory.");

     Locker.WriteEncryptString(ref fileStream, LogsToString(rawLogs), cryptPassword);

     logger.WriteMessage(DateTime.Now.ToString() + " successfully written appended logs on disk.");
    }

    private static string LogsToString(Queue<string> rawLogs)
    {
      int noLogs = rawLogs.Count;

      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < noLogs; i++)
        sb.AppendLine(rawLogs.Dequeue());

      return sb.ToString();
    }

    /// <summary>
    /// Opens and locks a log file and returns a string with the decrypted logs if they exist.
    /// If they dont exist, an empty string is returned.
    /// </summary>
    /// <param name="fileStream">FileStream to read and lock a log file</param>
    /// <param name="path1">Path of first file to try</param>
    /// <param name="path2">Path of second file to try if first file is locked</param>
    /// <param name="cryptPassword">password to use for encryption/decryption</param>
    /// <returns>a string with the decrypted logs or an empty string if logs don't exist</returns>
    /// <param name="logger">the Logger object to use when debugging exceptions</param>
    private static Queue<string> ReadExistingLogs(ref FileStream fileStream,
      string path1, string path2, string cryptPassword, Logger logger)
    {
      MemoryStream memoryStream = null;

      try
      {
        try
        {
          memoryStream = Locker.ReadDecryptFile(ref fileStream, path1, cryptPassword, false, true);

          logger.WriteMessage(DateTime.Now.ToString() + " successfully read existing log from path 1.");
        }
        catch (IOException ex)
        {
          logger.WriteError(ex);

          memoryStream = Locker.ReadDecryptFile(ref fileStream, path1, cryptPassword, false, true);

          logger.WriteMessage(DateTime.Now.ToString() + " successfully read existing log from path 2.");
        }
      }
      catch (CryptographicException ex) // file will be truncate in the case of a CryptographicException
      {
        logger.WriteError(ex);
      }
      catch (Exception ex)
      {
        Locker.ClearFileStream(ref fileStream);

        logger.WriteMessage(DateTime.Now.ToString() + " successfully unlocked file. Please see below for error.");

        throw ex;
      }

      Queue<string> existingRawLogs = new Queue<string>();

      // if memory stream is not null (i.e. log files already exist)
      if (memoryStream != null)
      {
        StreamReader sr = new StreamReader(memoryStream);

        logger.WriteMessage(DateTime.Now.ToString() + " successfully created stream reader.");

        while (sr.Peek() >= 0)
        {
          try
          {
            existingRawLogs.Enqueue(sr.ReadLine());
          }
          catch (Exception ex)
          {
            Locker.ClearFileStream(ref fileStream);

            logger.WriteMessage(DateTime.Now.ToString() + " successfully unlocked file. Please see below for error.");

            sr.Close();
            sr.Dispose();

            logger.WriteMessage(DateTime.Now.ToString() + " successfully disposed of StreamReader.");

            memoryStream.Close();
            memoryStream.Dispose();

            logger.WriteMessage(DateTime.Now.ToString() + " successfully disposed of MemoryStream.");

            throw ex;
          }
        }

        sr.Close();
        sr.Dispose();

        logger.WriteMessage(DateTime.Now.ToString() + " successfully disposed of StreamReader.");
        
        memoryStream.Close();
        memoryStream.Dispose();

        logger.WriteMessage(DateTime.Now.ToString() + " successfully disposed of MemoryStream.");
      }

      return existingRawLogs;
    }
  }
}
