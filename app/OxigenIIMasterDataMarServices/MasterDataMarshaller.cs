using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using DirectoryEnumerator;
using log4net;
using OxigenIIAdvertising.ServiceContracts.MasterDataMarshaller;
using InterCommunicationStructures;
using OxigenIIAdvertising.FileChecksumCalculator;
using System.IO;
using System.Diagnostics;

namespace OxigenIIAdvertising.RelayServers
{
  [ServiceBehavior(Namespace = "http://oxigen.net")]
  public class MasterDataMarshaller : IMasterDataMarshallerStreamer, IMasterDataMarshaller
  {
    private string _appDataPath = System.Configuration.ConfigurationSettings.AppSettings["appDataPath"];
    private string _assetPath = System.Configuration.ConfigurationSettings.AppSettings["assetPath"];
    private string _channelDataPath = System.Configuration.ConfigurationSettings.AppSettings["channelDataPath"];
    private string _aggregatedLogsPath = System.Configuration.ConfigurationSettings.AppSettings["aggregatedLogsPath"];
    private string _machineSpecificDataPath = System.Configuration.ConfigurationSettings.AppSettings["machineSpecificDataPath"];
    private string _machineSpecificDataPathUDM = System.Configuration.ConfigurationSettings.AppSettings["machineSpecificDataPathUDM"];
    private string _systemPassPhrase = System.Configuration.ConfigurationSettings.AppSettings["systemPassPhrase"];
    private int _maxUninstallRows = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["maxUninstallRows"]);
    private string[] _retrievedSoftwareInfoFiles = null;

    private readonly ILog _logger = LogManager.GetLogger(typeof(MasterDataMarshaller));

    private EventLog _eventLog = null;

    public MasterDataMarshaller()
    {
      _eventLog = new EventLog();
      _eventLog.Log = String.Empty;
      _eventLog.Source = "Oxigen Master Data Marshaller";
      log4net.Config.XmlConfigurator.Configure();
    }

    public void SetAppDataFiles(AppDataFileStreamParameterMessage appDataFileStreamParameterMessage)
    {
      if (appDataFileStreamParameterMessage.SystemPassPhrase != _systemPassPhrase)
        return;

      string appDataFullPath = "";

      switch (appDataFileStreamParameterMessage.DataFileType)
      {
        case DataFileType.AdvertConditions:
          appDataFullPath = _appDataPath + "ss_adcond_data.dat";
          break;
        case DataFileType.GeneralConfiguration:
          appDataFullPath = _appDataPath + "ss_general_data.dat";
          break;
        case DataFileType.ChannelData:
          appDataFullPath = _channelDataPath + appDataFileStreamParameterMessage.ChannelID + "_channel.dat";
          break;
      }

      SaveFileIfDifferent(appDataFileStreamParameterMessage, appDataFullPath);
    }

    public void SetAssetFile(AssetFileStreamParameterMessage assetFileParameterStreamMessage)
    {
      if (assetFileParameterStreamMessage.SystemPassPhrase != _systemPassPhrase)
        return;

      string assetDir = _assetPath + assetFileParameterStreamMessage.Filename.Substring(assetFileParameterStreamMessage.Filename.LastIndexOf("_") + 1, 1).ToUpper();

      if (!Directory.Exists(assetDir))
        Directory.CreateDirectory(assetDir);

      string assetFullPath = assetDir + "\\" + assetFileParameterStreamMessage.Filename;

      SaveFileIfDifferent(assetFileParameterStreamMessage, assetFullPath);
    }

    public StreamMessage GetLogData(LogAggregatedDataParameterMessage logAggregatedDataParameterMessage)
    {
      string logFileFullPath = "";

      FileStream fileStream = null;

      bool bSuccess = false;

      // close returned stream and delete sent file on completion
      OperationContext clientContext = OperationContext.Current;
      clientContext.OperationCompleted += new EventHandler(delegate(object sender, EventArgs args)
      {
        if (fileStream != null)
        {
          // truncate file. there is a probability that the Log Aggregator Windows service will lock that file
          // just before deleting it, so make sure the file is empty.
          fileStream.SetLength(0);

          fileStream.Dispose();
        }

        if (logFileFullPath != "" && bSuccess)
        {
          try
          {
            File.Delete(logFileFullPath);
          }
          catch(Exception ex)
          {
            _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
          }
        }
      });

      if (logAggregatedDataParameterMessage.SystemPassPhrase != _systemPassPhrase)
        return null;

      // aggregate logs' path: <path from root>\machineGUID\
      // and then search for an available aggregated log file
      string logPartialPath = _aggregatedLogsPath;

      switch (logAggregatedDataParameterMessage.LogTypeAggregated)
      {
        case LogTypeAggregated.AdvertWeightedClicksPerChannel:
          logPartialPath += "a_c_c_p_";
          break;
        case LogTypeAggregated.AdvertWeightedImpressionsPerChannel:
          logPartialPath += "a_c_i_p_";
          break;
        case LogTypeAggregated.ClicksPerDatePerAsset:
          logPartialPath += "c_d_a_";
          break;
        case LogTypeAggregated.ImpressionsPerDatePerAsset:
          logPartialPath += "s_d_a_";
          break;
      }

      // if file exists, just read it. No need to check for checksum as file will be deleted
      // when transfer is complete
      bSuccess = TryGetAndLockFile(ref fileStream, logPartialPath, ref logFileFullPath);

      return new StreamMessage(fileStream);
    }

    public StreamMessage GetCurrentScreenSaverProducts(MasterParameterMessage authentication)
    {
      FileStream fileStream = null;

      bool bSuccess = false;

      string aggregatedCurrentScreenSaverFullPath = "";

      // close returned stream and delete sent file on completion
      OperationContext clientContext = OperationContext.Current;

      clientContext.OperationCompleted += new EventHandler(delegate(object sender, EventArgs args)
      {
        if (fileStream != null)
        {
          // truncate file. there is a probability that the Screensaver Product Aggregator Windows service will lock that file
          // just before deleting it, so make sure the file is empty.
          fileStream.SetLength(0);

          fileStream.Dispose();
        }

        if (aggregatedCurrentScreenSaverFullPath != "" && bSuccess)
        {
          try
          {
            File.Delete(aggregatedCurrentScreenSaverFullPath);
          }
          catch (Exception ex)
          {
            _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
          }
        }
      });

      if (authentication.SystemPassPhrase != _systemPassPhrase)
        return null;

      string aggregatedCurrentScreenSaverPartialPath = _machineSpecificDataPath + "current_screensaver_info";

      // if file exists, just read it. No need to check for checksum as file will be deleted
      // when transfer is complete
      bSuccess = TryGetAndLockFile(ref fileStream, aggregatedCurrentScreenSaverPartialPath, ref aggregatedCurrentScreenSaverFullPath);

      return new StreamMessage(fileStream);
    }

    public Stream GetSoftwareUninstalls(string systemPassPhrase)
    {
      MemoryStream ms = null;

      if (systemPassPhrase != _systemPassPhrase)
      {
        ms = new MemoryStream(new byte[0]);

        return ms;
      }

      List<string> readFiles = new List<string>();

      // close returned stream and delete sent file on completion
      OperationContext clientContext = OperationContext.Current;

      clientContext.OperationCompleted += new EventHandler(delegate(object sender, EventArgs args)
      {
        if (ms != null)
          ms.Dispose();

        foreach (string file in readFiles)
          File.Delete(file);
      });

      string[] files = Directory.GetFiles(_machineSpecificDataPath, "software_uninstall*.*");

      // do we have any files?
      if (files.Length == 0)
      {
        ms = new MemoryStream(new byte[0]);

        return ms;
      }

      StringBuilder guidsToUninstall = new StringBuilder();

      int counter = 0;
     
      foreach (string file in files)
      {
        if (counter >= _maxUninstallRows)
         break;

        try
        {
          string uninstallInfo = File.ReadAllText(file);
          string[] uninstallLines = uninstallInfo.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

          foreach (string line in uninstallLines)
          {
            string machineGUID = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries)[0];
            guidsToUninstall.Append(machineGUID);
            guidsToUninstall.Append("|");

            counter++;
          }

          readFiles.Add(file);
        }
        catch (Exception ex)
        {
          // log error and move to the next file in the loop
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
      }
   
      ms = new MemoryStream(ASCIIEncoding.Default.GetBytes(guidsToUninstall.ToString()));

      return ms;
    }

    public StreamMessage GetUserHeartbeats(MasterParameterMessage authentication)
    {
      FileStream fileStream = null;

      bool bSuccess = false;

      string aggregatedHeartbeatsFullPath = "";

      // close returned stream and delete sent file on completion
      OperationContext clientContext = OperationContext.Current;

      clientContext.OperationCompleted += new EventHandler(delegate(object sender, EventArgs args)
      {
        if (fileStream != null)
        {
          // truncate file. there is a probability that the Heartbeat Aggreagator Windows service will lock that file
          // just before deleting it, so make sure the file is empty.
          fileStream.SetLength(0);

          fileStream.Dispose();
        }

        if (aggregatedHeartbeatsFullPath != "" && bSuccess)
        {
          try
          {
            File.Delete(aggregatedHeartbeatsFullPath);
          }
          catch (Exception ex)
          {
            _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
          }
        }
      });

      if (authentication.SystemPassPhrase != _systemPassPhrase)
        return null;

      string aggregatedHeartbeatsPartialPath = _machineSpecificDataPath + "heartbeat";

      // if file exists, just read it. No need to check for checksum as file will be deleted
      // when transfer is complete
      bSuccess = TryGetAndLockFile(ref fileStream, aggregatedHeartbeatsPartialPath, ref aggregatedHeartbeatsFullPath);

      return new StreamMessage(fileStream);
    }

    private bool TryGetAndLockFile(ref FileStream fileStream, string partialPath, ref string fullPath)
    {
      fullPath = GetAvailableFullPath(partialPath);

      if (fullPath == "")
        return false ;

      return TryReadFileWithLock(ref fileStream, fullPath);    
    }

    // gets an available full file given a folder path and a partial file name.
    // it should return the full path back suffixed by an underscore and a number
    // as aggregated files are saved in the form of <general filename>_n.dat where n > 1.
    private string GetAvailableFullPath(string partialPath)
    {
      string path = Path.GetDirectoryName(partialPath);

      // get the partial file name from the partialPath string
      string searchPattern = Path.GetFileName(partialPath);

      // search for files beginning with search pattern
      string[] files = Directory.GetFiles(path, searchPattern + "*");

      if (files.Length == 0)
        return "";

      return files[0];
    }

    // checks if file exists and is different than the one coming to this server.
    // if file does not exist or is different, then save file and calculate a checksum and save it too, else return error.
    private void SaveFileIfDifferent(StreamMasterParameterMessage streamMasterParameterMessage, string destinationFullPath)
    {
      string checksumPath = Path.GetDirectoryName(destinationFullPath) + "\\" + Path.GetFileNameWithoutExtension(destinationFullPath) + ".chk";

      string relaySideChecksum = ReadStringFromFile(checksumPath);

      if (streamMasterParameterMessage.Checksum == relaySideChecksum)
        return;

      // save received file and checksum. if saving fails, exit with an error
      SaveStreamChecksumDispose(streamMasterParameterMessage.Stream, checksumPath, destinationFullPath);
    }

    private void SaveStreamChecksumDispose(Stream readStream, string checksumPath, string fullPath)
    {
      try
      {
        SaveStreamAndDispose(readStream, fullPath);

        string checksum = ChecksumCalculator.GetChecksum(fullPath);

        try
        {
          File.WriteAllText(checksumPath, checksum);
        }
        catch (Exception ex)
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);

          File.Delete(fullPath);
        }
      }
      catch (Exception ex)
      {
        _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
      }
    }

    private void SaveStreamAndDispose(Stream readStream, string fullPath)
    {
      FileStream fileStream = null;

      string pathWithoutFilename = Path.GetDirectoryName(fullPath);

      try
      {
        if (!Directory.Exists(pathWithoutFilename))
          Directory.CreateDirectory(pathWithoutFilename);

        fileStream = new FileStream(fullPath, FileMode.Create);

        SaveStream(readStream, fileStream);
      }
      finally
      {
        if (fileStream != null)
          fileStream.Dispose();
      }
    }

    private void SaveStream(Stream readStream, Stream writeStream)
    {
      int length = 256;
      byte[] buffer = new byte[length];
      int bytesRead = readStream.Read(buffer, 0, length);

      while (bytesRead > 0)
      {
        writeStream.Write(buffer, 0, bytesRead);
        bytesRead = readStream.Read(buffer, 0, length);
      }
    }

    private bool TryReadFileWithLock(ref FileStream fileStream, string logFullPath)
    {
      try
      {
        fileStream = new FileStream(logFullPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
      }
      catch (Exception ex)
      {
        _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);

        if (fileStream != null)
          fileStream.Dispose();

        return false;
      }

      return true;
    }

    private string ReadStringFromFile(string fullPath)
    {
      try
      {
        if (File.Exists(fullPath))
          return File.ReadAllText(fullPath);
      }
      catch (Exception ex)
      {
        _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
      }

      return "";
    }

    public MachineVersionInfo[] GetMachineVersionInfo(int fileLimit)
    {
      _eventLog.WriteEntry("GetMachineVersionInfo()..", EventLogEntryType.Information);
      _logger.Debug("GetMachineVersionInfo() 1");
      MachineVersionInfo[] machineVersionInfos = null;
      _logger.Debug("GetMachineVersionInfo() 2");
      try
      {
        _logger.Debug("GetMachineVersionInfo() 3");
        _retrievedSoftwareInfoFiles = FastDirectoryEnumerator.GetFiles(_machineSpecificDataPathUDM, "current_software*", SearchOption.AllDirectories, fileLimit);
        _logger.Debug("GetMachineVersionInfo() 4");
        machineVersionInfos = new MachineVersionInfo[_retrievedSoftwareInfoFiles.Length];
        _logger.Debug("GetMachineVersionInfo() 5");
      }
      catch (Exception ex)
      {
        _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        _logger.Error(ex.ToString());
        throw;
      }

      _logger.Debug(_retrievedSoftwareInfoFiles.Length + " files will be processed.");
      _eventLog.WriteEntry(_retrievedSoftwareInfoFiles.Length + " files will be processed.", EventLogEntryType.Information);

      for (int i = 0; i < _retrievedSoftwareInfoFiles.Length; i++)
      {
        try
        {
          DateTime lastUpdatedAt;
          int majorVersionNumber;
          int minorVersionNumber;

          string contents = File.ReadAllText(_retrievedSoftwareInfoFiles[i]);
          string machineGUID = GetMachineGUIDFromPath(_retrievedSoftwareInfoFiles[i]);
          string[] contentsArray = contents.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
          string[] versionComponents = contentsArray[1].Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

          if (!DateTime.TryParse(contentsArray[0], out lastUpdatedAt))
            continue;

          if (!int.TryParse(versionComponents[0], out majorVersionNumber))
            continue;

          if (!int.TryParse(versionComponents[1], out minorVersionNumber))
            continue;

          machineVersionInfos[i] = new MachineVersionInfo(machineGUID, lastUpdatedAt, majorVersionNumber, minorVersionNumber);
        }
        catch (Exception ex)
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
      }

      _logger.Debug("Retrieved " + machineVersionInfos.Length + " version info data.");

      return machineVersionInfos;
    }

    public void DeleteSoftwareInfoFiles(HashSet<string> machineGUIDSFailedToUpdate)
    {
      _eventLog.WriteEntry("DeleteSoftwareInfoFiles()..", EventLogEntryType.Information);
      if (_retrievedSoftwareInfoFiles == null) 
      {
        _logger.Debug("_retrievedSoftwareInfoFiles is null");
        return;
      }

      int count = 0;

      foreach (string file in _retrievedSoftwareInfoFiles) 
      {
        if (!machineGUIDSFailedToUpdate.Contains(GetMachineGUIDFromPath(file)))
        {
          //try {
          //  File.Delete(file);
          count++;
          //}
          //catch (Exception ex) {
          //  _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
          //}
          
          _logger.Debug(file + " deleted.");
        }
      }

      _logger.Info(count + " files deleted.");
    }

    private static string GetMachineGUIDFromPath(string path)
    {
      string pathNoFile = Path.GetDirectoryName(path);
      return pathNoFile.Substring(pathNoFile.LastIndexOf("\\") + 1,
                                         pathNoFile.Length - pathNoFile.LastIndexOf("\\") - 1);
    }
  }
}
