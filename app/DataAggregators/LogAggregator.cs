using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using InterCommunicationStructures;
using OxigenIIAdvertising.FlatAggregatedLogStructures;

namespace OxigenIIAdvertising.DataAggregators
{
  /// <summary>
  /// Aggregates client side aggregated log information and makes ready for upload to Master Servers
  /// </summary>
  public class LogAggregator
  {
    private string _clientSideAggregatedLogsPath;
    private string _serverSideAggregatedLogsPath;
    private int _maxNoClientSideFiles;
    private List<string> _filesToDelete;

    private EventLog _eventLog;

    /// <summary>
    /// Constructor for LogAggregator
    /// </summary>
    /// <param name="clientSideAggregatedLogsPath">File Path where the client side aggregated log files are stored</param>
    /// <param name="serverSideAggregatedLogsPath">File Path where the server side aggregated log files will go</param>
    /// <param name="maxNoClientSideFiles">Maximum number of client side files to process when creating a new server side file</param>
    /// <param name="eventLog">EventLog object to provide interaction with Windows event logs.</param>
    public LogAggregator(string clientSideAggregatedLogsPath, string serverSideAggregatedLogsPath,
      int maxNoClientSideFiles, EventLog eventLog)
    {
      _clientSideAggregatedLogsPath = clientSideAggregatedLogsPath;
      _serverSideAggregatedLogsPath = serverSideAggregatedLogsPath;
      _maxNoClientSideFiles = maxNoClientSideFiles;
      _eventLog = eventLog;

      _filesToDelete = new List<string>();
    }

    /// <summary>
    /// Aggregates non-aggregated files that have been uploaded by clients to this Relay Server.
    /// Only as many files per type as specified by _maxNoClientSideFiles will be aggregated per execution of this method.
    /// </summary>
    public void Execute()
    {
      AggregateSaveDateOperationsPerAsset("c_d_a.dat");
      AggregateSaveDateOperationsPerAsset("i_d_a.dat");
      AggregateSaveAdvertChannelOperationProportions("a_c_c_p.dat");
      AggregateSaveAdvertChannelOperationProportions("a_c_i_p.dat");
    }

    private void AggregateSaveDateOperationsPerAsset(string filePattern)
    {
      // date per clicks per asset. from the given root search for files ending in c_d_a.dat
      List<string> logFilePaths = GetContentSpecificLogs(filePattern);

      int noLogFiles = logFilePaths.Count;

      if (noLogFiles == 0)
        return;

      // determine how many files to process: if there are less than the maximum allowed, do as many as there are on disk
      // otherwise do as many as the maximum allowed per server side aggregated file
      int noFilesToProcess = noLogFiles < _maxNoClientSideFiles ? noLogFiles : _maxNoClientSideFiles;

      string[] filesToDelete = new string[noFilesToProcess];

      int counter = 0;

      List<DateOperationsPerAsset> logsToProcess = new List<DateOperationsPerAsset>();

      foreach (string logFilePath in logFilePaths)
      {
        string channelID = GetFilesCurrentDirectory(logFilePath);

        // inflate file into class and add to logsToProcess
        Merge(logsToProcess, LogStatsInflator.InflateDateOperationsPerAsset(logFilePath, channelID));

        // keep the path of the file to delete
        filesToDelete[counter] = logFilePath;

        // increase the counter and check if it's time to break the loop
        counter++;

        if (counter == noFilesToProcess)
          break;
      }

      string aggregatedLogs = AggregateDateOperationsPerAsset(logsToProcess);

      string destinationPath = GetNewFileNameWithSuffix(filePattern);

      try
      {
        File.WriteAllText(GetNewFileNameWithSuffix(filePattern), aggregatedLogs);

        // if write was successful, delete processed files
        foreach (string file in filesToDelete)
        {
          try
          {
            File.Delete(file);
          }
          catch (Exception ex)
          {
            _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
          }
        }
      }
      catch (Exception ex)
      {
        _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
      }
    }

    // adds the elements of ie2 to ie1
    private void Merge<T>(ICollection<T> ie1, ICollection<T> ie2)
    {
      foreach (T t in ie2)
        ie1.Add(t);
    }

    private void AggregateSaveAdvertChannelOperationProportions(string filePattern)
    {
      // date per clicks per asset. from the given root search for files ending in c_d_a.dat
      List<string> logFilePaths = GetAdvertSpecificLogs(filePattern);

      int noLogFiles = logFilePaths.Count;

      if (noLogFiles == 0)
        return;

      // determine how many files to process: if there are less than the maximum allowed, do as many as there are on disk
      // otherwise do as many as the maximum allowed per server side aggregated file
      int noFilesToProcess = noLogFiles < _maxNoClientSideFiles ? noLogFiles : _maxNoClientSideFiles;

      string[] filesToDelete = new string[noFilesToProcess];

      int counter = 0;

      List<AdvertChannelOperationProportions> logsToProcess = new List<AdvertChannelOperationProportions>();

      foreach (string logFilePath in logFilePaths)
      {
        // inflate file into class and add to logsToProcess
        Merge(logsToProcess, LogStatsInflator.InflateAdvertChannelOperationProportions(logFilePath));

        // keep the path of the file to delete
        filesToDelete[counter] = logFilePath;

        // increase the counter and check if it's time to break the loop
        counter++;

        if (counter == noFilesToProcess)
          break;
      }

      string aggregatedLogs = AggregateAdvertChannelOperationProportions(logsToProcess);

      try
      {
        File.WriteAllText(GetNewFileNameWithSuffix(filePattern), aggregatedLogs);

        // if write was successful, delete processed files
        foreach (string file in filesToDelete)
        {
          try
          {
            File.Delete(file);
          }
          catch (Exception ex)
          {
           _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
          }
        }
      }
      catch (Exception ex)
      {
        _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
      }
    }

    private string AggregateDateOperationsPerAsset(List<DateOperationsPerAsset> logsToProcess)
    {
      var aggregatedLogs = from DateOperationsPerAsset flatLog in logsToProcess
                           group flatLog by new { flatLog.OperationDate, flatLog.AssetID, flatLog.AssetType } into g
                           select new DateOperationsPerAsset
                           {
                             OperationDate = g.Key.OperationDate,
                             AssetID = g.Key.AssetID,
                             AssetType = g.Key.AssetType,
                             NoOperations = g.Sum(flatLog => flatLog.NoOperations)
                           };

      StringBuilder logs = new StringBuilder();

      foreach (DateOperationsPerAsset dop in aggregatedLogs)
      {
        logs.Append(dop.OperationDate);
        logs.Append("|");
        logs.Append(dop.AssetID);
        logs.Append("|");
        logs.Append(dop.AssetType);
        logs.Append("|");
        logs.Append(dop.NoOperations);
        logs.AppendLine();
      }

      return logs.ToString();
    }

    private string AggregateAdvertChannelOperationProportions(List<AdvertChannelOperationProportions> logsToProcess)
    {
      var aggregatedLogs = from AdvertChannelOperationProportions advertOps in logsToProcess
                           group advertOps by new { advertOps.AdvertAssetID, advertOps.ChannelID } into g
                           select new AdvertChannelOperationProportions
                           {
                             AdvertAssetID = g.Key.AdvertAssetID,
                             ChannelID = g.Key.ChannelID,
                             AdvertOperationProportion = g.Sum(advertOps => advertOps.AdvertOperationProportion)
                           };

      StringBuilder logs = new StringBuilder();

      foreach (AdvertChannelOperationProportions advertOp in aggregatedLogs)
      {
        logs.Append(advertOp.AdvertAssetID);
        logs.Append("|");
        logs.Append(advertOp.ChannelID);
        logs.Append("|");
        logs.Append(advertOp.AdvertOperationProportion);
        logs.AppendLine();
      }

      return logs.ToString();
    }

    // gets all files one level down the given directory with the given pattern
    private List<string> GetContentSpecificLogs(string filePattern)
    {
      List<string> h = new List<string>();

      // get an array of machineguid directories
      string[] subDirectories = Directory.GetDirectories(_clientSideAggregatedLogsPath, "*", SearchOption.TopDirectoryOnly);

      // search all the machineguid directories
      foreach (string subDirectory in subDirectories)
      {
        string[] files = Directory.GetFiles(subDirectory, "*" + filePattern, SearchOption.AllDirectories);

        foreach (string file in files)
          h.Add(file);
      }

      return h;
    }

    private List<string> GetAdvertSpecificLogs(string filePattern)
    {
      List<string> h = new List<string>();

      // get an array of machineguid directories
      string[] subDirectories = Directory.GetDirectories(_clientSideAggregatedLogsPath, "*", SearchOption.TopDirectoryOnly);

      // search all the machineguid directories
      foreach (string subDirectory in subDirectories)
      {
        string[] files = Directory.GetFiles(subDirectory + "\\AdvertSpecificLogs", "*" + filePattern);

        foreach (string file in files)
          h.Add(file);
      }

      return h;
    }

    // search for an appropriate file name to create with a numerical suffix that doesn't exist
    private string GetNewFileNameWithSuffix(string filePattern)
    {
      string filePatternPrefix = filePattern.Substring(0, filePattern.LastIndexOf(".dat"));

      string partialPathWithPattern = _serverSideAggregatedLogsPath + filePatternPrefix;

      int incr = 1;

      while (true)
      {
        string newFileName = partialPathWithPattern + incr + ".dat";

        if (!File.Exists(newFileName))
          return newFileName;

        incr++;
      }
    }

    // gets the sub directory the file is in
    private string GetFilesCurrentDirectory(string fullPath)
    {
      string pathWithoutFilename = Path.GetDirectoryName(fullPath);

      int length = pathWithoutFilename.Length;

      int offset = pathWithoutFilename.LastIndexOf("\\") + 1;

      string fileParentDirectory = pathWithoutFilename.Substring(offset, length - offset);

      return fileParentDirectory;
    }
  }
}
