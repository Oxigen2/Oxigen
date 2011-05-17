using System;
using System.Collections.Generic;
using System.Linq;
using OxigenIIAdvertising.LoggingStructures;
using OxigenIIAdvertising.ServerConnectAttempt;
using OxigenIIAdvertising.AppData;
using OxigenIIAdvertising.XMLSerializer;
using OxigenIIAdvertising.LoggerInfo;
using System.ServiceModel;
using System.IO;
using ServiceErrorReporting;
using OxigenIIAdvertising.UserDataMarshallerServiceClient;
using OxigenIIAdvertising.LogStats;
using OxigenIIAdvertising.Exceptions;
using OxigenIIAdvertising.LogExchanger.Properties;
using OxigenIIAdvertising.FileRights;
using System.Windows.Forms;
using OxigenIIAdvertising.FileLocker;
using OxigenIIAdvertising.TimeSpanObjectWrapper;
using OxigenIIAdvertising.UserSettings;
using System.Configuration;
using InterCommunicationStructures;

namespace OxigenIIAdvertising.LogExchanger
{
  /// <summary>
  /// Performs the following steps on the log files, if they exist:
  /// Work unit 1: To aggregate logs and save.
  /// Lock, decrypt, open and read raw log files
  /// Lock, decrypt, open and read aggregated files
  /// Read and aggregate raw log files
  /// Truncate raw log files
  /// Append data to aggregated files
  /// Unlock raw log and aggregated files
  ///
  /// Work unit 2: To upload contents of aggregated logs to Relay Server
  /// Lock aggregated file
  /// Open it and decrypt it
  /// Send its contents
  /// Truncate it
  /// Unlock it
  /// </summary>
  public class Exchanger
  {
    private string _appDataPath = null;
    private string _usageCountLogsPath1 = null;
    private string _usageCountLogsPath2 = null;
    private string _generalDataPath = null;
    private string _channelSubscriptionPath = null;
    private string _systemPassPhrase = null;
    private string _userGUID = null;
    private string _machineGUID = null;
    private string _password = null;
    private string _settingsDataPath = null;
    private string _userSettingsPath = null;
    private string _debugFilePath = null;
    private string _logPath = null;
    private string _dateClicksPerAssetExisting = null;
    private string _dateImpressionsPerAssetExisting = null;
    private string _advertChannelClickProportionsExisting = null;
    private string _advertChannelImpressionProportionsExisting = null;
    private string _dateClicksPerAssetPath = null;
    private string _dateImpressionsPerAssetPath = null;
    private string _advertChannelClickProportionsPath = null;
    private string _advertChannelImpressionProportionsPath = null;

    private string _dateClicksPerAssetNew = null;
    private string _dateImpressionsPerAssetNew = null;
    private string _advertChannelClickProportionsNew = null;
    private string _advertChannelImpressionProportionsNew = null;
    private string _dateClicksPerAssetAll = null;
    private string _dateImpressionsPerAssetAll = null;
    private string _advertChannelClickProportionsAll = null;
    private string _advertChannelImpressionProportionsAll = null;
    private FileStream _advertClickLogStream = null;
    private FileStream _advertImpressionLogStream = null;
    private FileStream _contentClickLogStream = null;
    private FileStream _contentImpressionLogStream = null;
    private FileStream _dateClicksPerAssetStream = null;
    private FileStream _dateImpressionsPerAssetStream = null;
    private FileStream _advertChannelClickProportionsStream = null;
    private FileStream _advertChannelImpressionProportionsStream = null;
    private FileStream _usageCountStream = null;

    private bool _bDateClicksPerAssetUploaded = false;
    private bool _bDateImpressionsPerAssetUploaded = false;
    private bool _bAdvertChannelClickProportionsUploaded = false;
    private bool _bAdvertChannelImpressionProportionsUploaded = false;

    private List<ImpressionLogEntry> _advertImpressionLog = null;
    private List<ImpressionLogEntry> _contentImpressionLog = null;
    private List<ClickLogEntry> _advertClickLog = null;
    private List<ClickLogEntry> _contentClickLog = null;

    private List<DateOperationsPerAsset> _dateClicksPerAssetSet = null;
    private List<DateOperationsPerAsset> _dateImpressionsPerAssetSet = null;
    private List<AdvertChannelOperationProportions> _advertChannelClickProportionsSet = null;
    private List<AdvertChannelOperationProportions> _advertChannelImpressionProportionsSet = null;
    
    private Logger _logger = null;

    private UserDataMarshallerClient _userDataMarshallerClient = null;
    private UserDataMarshallerStreamerClient _userDataMarshallerStreamerClient = null;

    private int _dateTimeDiffTolerance = -1;
    private int _maxNoRelayLogServers = -1;
    private int _serverTimeout = -1;
    private string _primaryDomainName = null;
    private string _secondaryDomainName = null;
    private string _machineGUIDSuffix = null;

    private bool _bGlobalsSet = false;

    public Exchanger()
    { 
      // set global variables
      _bGlobalsSet = SetGlobals();
    }

    /// <summary>
    /// Executes LogExchanger steps.
    /// </summary>
    public void Execute()
    {
      if (!_bGlobalsSet)
        return;

      if (!FileAccessRights())
        return;

      if (!TryRecreateDirectoryStructureIfNecessary())
        return;

      ChannelSubscriptions channelSubscriptions = GetChannelSubscriptions();

      _logger.WriteMessage(DateTime.Now.ToString() + " GetChannelSubscriptions returned.");

      if (channelSubscriptions == null)
        return; // log exchanger cannot run without channel subscriptions

      // lock and load raw logs.
      bool bRawLogsExist = LoadRawAll();

      TimeSpanWrapperClass dateTimeDiff = GetDateTimeDiff();

      foreach (ChannelSubscription channelSubscription in channelSubscriptions.SubscriptionSet)
      {
        if (!AggregateAndUploadNonAdvertSpecificLogs(channelSubscription.ChannelID, channelSubscription.GetGUIDSuffix(), dateTimeDiff, bRawLogsExist))
          return;
      }

      // adverts: channel 0
      if (!AggregateAndUploadNonAdvertSpecificLogs(0, "", dateTimeDiff, bRawLogsExist))
        return;

      // advert-specific logs (adverts - channel weighting calculations)
      AggregateAndUploadAdvertSpecificLogs(channelSubscriptions, dateTimeDiff, bRawLogsExist);

      // if raw files can't be truncated they will be reprocessed.
      if (!TruncateRawFiles())
        ReleaseRawFiles();

      TryUploadTruncateReleaseUsageCountFile(bRawLogsExist);

      _userDataMarshallerStreamerClient.Dispose();
    }

    private bool FileAccessRights()
    {
      // check permissions on all applicable directories for write access
      // if no write access return
      bool bReadableWritableSettingsPath = FileDirectoryRightsChecker.AreFilesReadableWritable(_settingsDataPath);
      bool bReadableWritableLogPath = FileDirectoryRightsChecker.AreFilesReadableWritable(_logPath);
      bool bCanCreateDirectoriesLogPath = FileDirectoryRightsChecker.CanCreateDeleteDirectories(_logPath);

      if (!bReadableWritableSettingsPath || !bReadableWritableLogPath || !bCanCreateDirectoriesLogPath)
        return false;

      return true;
    }

    private bool TryRecreateDirectoryStructureIfNecessary()
    {
      if (!Directory.Exists(_logPath))
      {
        try
        {
          Directory.CreateDirectory(_logPath);
        }
        catch (Exception ex)
        {
          _logger.WriteError(ex);
          return false;
        }
      }

      return true;
    }

    // returns false if there's a critical error or application can otherwise not continue (e.g. no data)
    private bool AggregateAndUploadNonAdvertSpecificLogs(long channelID, string channelGUIDSuffix, TimeSpanWrapperClass dateTimeDiff, bool bRawLogsExist)
    {
      //
      // Work unit 1: aggregate to flat aggregated files
      //

      string channelAggregatedLogsPath = _logPath + channelID;

      if (!Directory.Exists(channelAggregatedLogsPath))
        Directory.CreateDirectory(channelAggregatedLogsPath);

      // load and lock aggregated Logs for a specific channel or generic advert logs.
      bool bAggregatedNonAdvertSpecificLogsExist = LoadAggregatedNonAdvertSpecific(channelAggregatedLogsPath);

      // if no logs exist, return .
      if (!bRawLogsExist && !bAggregatedNonAdvertSpecificLogsExist)
      {
        ReleaseAggregatedFiles();
        return true;
      }

      // if we have the time difference, time-correct
      if (dateTimeDiff != null)
        TimeCorrect(dateTimeDiff.TimeSpan);

      AggregateRawDataToMemoryNonAdvertSpecific(channelID);

      // append aggregated data from memory to files.
      AppendAggregatedDataNonAdvertSpecific();

      ReleaseAggregatedFiles();

      //
      // Work unit 2: upload data from aggregated files and truncate if successful
      // 

      // search again for responsive server
      string sendLogURI = null;

      // condition channelGUIDSuffix == "" only if logs pertain to advert logs, as adverts do not have a channel
      if (channelGUIDSuffix == "")
        sendLogURI = GetResponsiveServer(ServerType.RelayLogAdv, _maxNoRelayLogServers, _machineGUIDSuffix, "UserDataMarshaller.svc");
      else
        sendLogURI = GetResponsiveServer(ServerType.RelayLogCont, _maxNoRelayLogServers, channelGUIDSuffix, "UserDataMarshaller.svc");

      // if no responsive servers found, return. at this point data have been aggregated, try next channel
      if (sendLogURI == "")
      {
        _logger.WriteTimestampedMessage("AggregateAndUploadNonAdvertSpecificLogs: URL not found");
        return true;
      }

      _logger.WriteTimestampedMessage("AggregateAndUploadNonAdvertSpecificLogs: URL: " + sendLogURI + "/file");

      _userDataMarshallerStreamerClient.Endpoint.Address = new EndpointAddress(sendLogURI + "/file");

      UploadNonAdvertSpecificAggregatedFiles(channelID);

      // truncate aggregated files. no need to check if there was an error as this is near the end of the LogExchanger
      TruncateNonAdvertSpecificAggregatedFilesIfUploaded();

      ReleaseAggregatedFiles();
     
      return true;
    }

    private bool AggregateAndUploadAdvertSpecificLogs(ChannelSubscriptions channelSubscriptions, TimeSpanWrapperClass dateTimeDiff, bool bRawLogsExist)
    {
      // work unit 1: aggregate to flat aggregated files

      bool bAggregatedAdvertSpecificLogsExist = LoadAggregatedAdvertSpecific();

      // if no logs exist, return.
      if (!bRawLogsExist && !bAggregatedAdvertSpecificLogsExist)
      {
        ReleaseAggregatedFiles();
        return true;
      }

      // if we have the time difference, time-correct
      if (dateTimeDiff != null)
        TimeCorrect(dateTimeDiff.TimeSpan);

      AggregateRawDataToMemoryAdvertSpecific(channelSubscriptions);

      // append aggregated data from memory to files.
      AppendAggregatedDataAdvertSpecific();

      ReleaseAggregatedFiles();

      //
      // Work unit 2: upload data from aggregated files and truncate if successful
      // 

      // search again for responsive server
      string sendLogURI = GetResponsiveServer(ServerType.RelayLogAdv, _maxNoRelayLogServers, _machineGUIDSuffix, "UserDataMarshaller.svc");

      // if no responsive servers found, return
      if (sendLogURI == "")
      {
        _logger.WriteTimestampedMessage("AggregateAndUploadNonAdvertSpecificLogs: URL not found");
        return true;
      }

      _logger.WriteTimestampedMessage("AggregateAndUploadNonAdvertSpecificLogs: URL: " + sendLogURI + "/file");

      _userDataMarshallerStreamerClient.Endpoint.Address = new EndpointAddress(sendLogURI + "/file");

      UploadAdvertSpecificAggregatedFiles();

      // truncate aggregated files. no need to check if there was an error as this is near the end of the LogExchanger
      TruncateAdvertSpecificAggregatedFilesIfUploaded();

      ReleaseAggregatedFiles();

      return true;
    }

    private void TryUploadTruncateReleaseUsageCountFile(bool bRawLogsExist)
    {
      if (!bRawLogsExist)
        return;

      bool bUsageCountUploaded = TryLockAndUploadStringFromFile(ref _usageCountStream, _usageCountLogsPath1, _usageCountLogsPath2, -2L);

      if (bUsageCountUploaded)
        TryTruncate(_usageCountStream);
       
      Locker.ClearFileStream(ref _usageCountStream);
    }

    private TimeSpanWrapperClass GetDateTimeDiff()
    {
      TimeSpanWrapperClass dateTimeDiff = null;

      string timeServerURI = GetResponsiveServer(ServerType.RelayLogs, _maxNoRelayLogServers, _machineGUIDSuffix, "UserDataMarshaller.svc");

      // if responsive server found
      if (timeServerURI != "")
      {
        _userDataMarshallerClient.Endpoint.Address = new EndpointAddress(timeServerURI);

        dateTimeDiff = GetTimeDiff(_dateTimeDiffTolerance);

        // if not able to write time difference to file, don't time-correct 
        TryWriteTimeDiffToFile(dateTimeDiff);
      }
      else // if not able to connect, read existing time difference from file, if it exists
      {
        // if not able to read an existing time difference, don't time-correct
        TryReadTimeDiffFromFile(ref dateTimeDiff);
      }

      return dateTimeDiff;
    }

    private bool TryWriteTimeDiffToFile(TimeSpanWrapperClass dateTimeDiff)
    {
      if (dateTimeDiff == null)
        return true;

      try
      {
        User user = (User)Serializer.Deserialize(typeof(User), _userSettingsPath, _password);

        user.LastTimeDiff = dateTimeDiff.TimeSpan;

        Serializer.Serialize(user, _userSettingsPath, _password);
      }
      catch (Exception ex)
      {
        _logger.WriteError(ex);
        return false;
      }

      return true;
    }

    private bool TryReadTimeDiffFromFile(ref TimeSpanWrapperClass timeSpanWrapperClass)
    {
      User user = null;

      try
      {
        user = (User)Serializer.Deserialize(typeof(User), _userSettingsPath, _password);
      }
      catch (Exception ex)
      {
        _logger.WriteError(ex);
        return false;
      }

      timeSpanWrapperClass = new TimeSpanWrapperClass(user.LastTimeDiff);

      return true;
    }

    private void AggregateRawDataToMemoryNonAdvertSpecific(long channelID)
    {
      // adverts are notioned as channel ID 0.
      if (channelID == 0)
      {
        _dateClicksPerAssetSet = LogStatsCalculator.AggregateClicksPerDay(_advertClickLog, channelID);
        _dateImpressionsPerAssetSet = LogStatsCalculator.AggregateImpressionsPerDay(_advertImpressionLog, channelID);
      }
      else
      {
        _dateClicksPerAssetSet = LogStatsCalculator.AggregateClicksPerDay(_contentClickLog, channelID);
        _dateImpressionsPerAssetSet = LogStatsCalculator.AggregateImpressionsPerDay(_contentImpressionLog, channelID);
      }
    }

    private void AggregateRawDataToMemoryAdvertSpecific(ChannelSubscriptions channelSubscriptions)
    {
      _advertChannelClickProportionsSet = LogStatsCalculator.GetAdvertChannelClickProportions(_advertClickLog, channelSubscriptions);
      _advertChannelImpressionProportionsSet = LogStatsCalculator.GetAdvertChannelImpressionProportions(_advertImpressionLog, channelSubscriptions);
    }

    private bool TruncateRawFiles()
    {
      bool contentClick = TryTruncate(_contentClickLogStream);
      bool advertClick = TryTruncate(_advertClickLogStream);
      bool contentImpression = TryTruncate(_contentImpressionLogStream);
      bool advertImpression = TryTruncate(_advertImpressionLogStream);

      if (!contentClick || !advertClick || !contentImpression || !advertImpression)
        return false;

      return true;
    }

    private void TruncateNonAdvertSpecificAggregatedFilesIfUploaded()
    {
      if (_bDateImpressionsPerAssetUploaded)
        TryTruncate(_dateImpressionsPerAssetStream);
      
      if (_bDateClicksPerAssetUploaded)
        TryTruncate(_dateClicksPerAssetStream);
    }

    private void TruncateAdvertSpecificAggregatedFilesIfUploaded()
    {
      if (_bAdvertChannelImpressionProportionsUploaded)
        TryTruncate(_advertChannelImpressionProportionsStream);

      if (_bAdvertChannelClickProportionsUploaded)
        TryTruncate(_advertChannelClickProportionsStream);
    }

    private bool TryTruncate(FileStream fileStream)
    {
      if (fileStream == null)
        return true;

      try
      {
        fileStream.SetLength(0);
      }
      catch (Exception ex)
      {
        _logger.WriteError(ex);
        return false;
      }

      return true;
    }

    private void ReleaseAllLogFiles()
    {
      ReleaseRawFiles();
      ReleaseAggregatedFiles();
    }

    private void ReleaseRawFiles()
    {
      Locker.ClearFileStream(ref _contentClickLogStream);
      Locker.ClearFileStream(ref _advertClickLogStream);
      Locker.ClearFileStream(ref _contentImpressionLogStream);
      Locker.ClearFileStream(ref _advertImpressionLogStream);
    }

    private void ReleaseAggregatedFiles()
    {
      Locker.ClearFileStream(ref _dateImpressionsPerAssetStream);
      Locker.ClearFileStream(ref _dateClicksPerAssetStream);
      Locker.ClearFileStream(ref _advertChannelImpressionProportionsStream);
      Locker.ClearFileStream(ref _advertChannelClickProportionsStream);
    }

    /// <summary>
    /// Locks and loads aggregated data if they exist, into four strings.
    /// For each log file not found, its lock is released
    /// </summary>
    private bool LoadAggregatedNonAdvertSpecific(string channelAggregatedLogsPath)
    {
      _dateClicksPerAssetPath = channelAggregatedLogsPath + "\\c_d_a.dat";
      _dateImpressionsPerAssetPath = channelAggregatedLogsPath + "\\i_d_a.dat";

      _dateClicksPerAssetExisting = LogFileReader.LoadAggregatedLogFile(_dateClicksPerAssetPath, ref _dateClicksPerAssetStream, _password);
      _dateImpressionsPerAssetExisting = LogFileReader.LoadAggregatedLogFile(_dateImpressionsPerAssetPath, ref _dateImpressionsPerAssetStream, _password);
     
      if (_dateClicksPerAssetExisting == "" && _dateImpressionsPerAssetExisting == "")
        return false;

      return true;
    }

    private bool LoadAggregatedAdvertSpecific()
    {
      _advertChannelClickProportionsPath = _logPath + "\\a_c_c_p.dat";
      _advertChannelImpressionProportionsPath = _logPath + "\\a_c_i_p.dat";

      _advertChannelClickProportionsExisting = LogFileReader.LoadAggregatedLogFile(_advertChannelClickProportionsPath, ref _advertChannelClickProportionsStream, _password);
      _advertChannelImpressionProportionsExisting = LogFileReader.LoadAggregatedLogFile(_advertChannelImpressionProportionsPath, ref _advertChannelImpressionProportionsStream, _password);

      if (_advertChannelClickProportionsExisting == "" && _advertChannelImpressionProportionsExisting == "")
        return false;

      return true;
    }
    
    // Flatten aggregated structures that are in memory and appends them to existing ones on files
    private void AppendAggregatedDataNonAdvertSpecific()
    {
      _dateClicksPerAssetNew = LogFlattener.FlattenOperationsPerDatePerAsset(_dateClicksPerAssetSet);
      _dateImpressionsPerAssetNew = LogFlattener.FlattenOperationsPerDatePerAsset(_dateImpressionsPerAssetSet);

      _dateClicksPerAssetAll = _dateClicksPerAssetExisting + _dateClicksPerAssetNew;
      _dateImpressionsPerAssetAll = _dateImpressionsPerAssetExisting + _dateImpressionsPerAssetNew;
            
      try
      {
        Locker.WriteEncryptString(ref _dateClicksPerAssetStream, _dateClicksPerAssetAll, _password);
      }
      catch (Exception ex)
      {
        _logger.WriteError(ex);
      }

      try
      {
        Locker.WriteEncryptString(ref _dateImpressionsPerAssetStream, _dateImpressionsPerAssetAll, _password);
      }
      catch (Exception ex)
      {
        _logger.WriteError(ex);
      }

      _dateClicksPerAssetAll = "";
      _dateClicksPerAssetExisting = "";
      _dateClicksPerAssetNew = "";
      _dateImpressionsPerAssetAll = "";
      _dateImpressionsPerAssetExisting = "";
      _dateImpressionsPerAssetNew = "";
    }

    private void AppendAggregatedDataAdvertSpecific()
    {
      _advertChannelClickProportionsNew = LogFlattener.FlattenAdvertChannelOperationProportions(_advertChannelClickProportionsSet);
      _advertChannelImpressionProportionsNew = LogFlattener.FlattenAdvertChannelOperationProportions(_advertChannelImpressionProportionsSet);
      _advertChannelImpressionProportionsAll = _advertChannelImpressionProportionsExisting + _advertChannelImpressionProportionsNew;
      _advertChannelClickProportionsAll = _advertChannelClickProportionsExisting + _advertChannelClickProportionsNew;

      try
      {
        Locker.WriteEncryptString(ref _advertChannelImpressionProportionsStream, _advertChannelImpressionProportionsAll, _password);
      }
      catch (Exception ex)
      {
        _logger.WriteError(ex);
      }

      try
      {
        Locker.WriteEncryptString(ref _advertChannelClickProportionsStream, _advertChannelClickProportionsAll, _password);
      }
      catch (Exception ex)
      {
        _logger.WriteError(ex);
      }

      _advertChannelImpressionProportionsAll = "";
      _advertChannelImpressionProportionsExisting = "";
      _advertChannelImpressionProportionsNew = "";
      _advertChannelClickProportionsAll = "";
      _advertChannelClickProportionsExisting = "";
      _advertChannelClickProportionsNew = "";
    }
   
    /// <summary>
    /// Time shifts all log entries to relay server's time zone
    /// </summary>
    /// <param name="dateTimeDiff">Time difference between the client machine and the relay server. If positive, server is ahead, if negative, server is behind</param>
    private void TimeCorrect(TimeSpan dateTimeDiff)
    {
      foreach (ClickLogEntry clickLogEntry in _advertClickLog)
        clickLogEntry.TimeCorrect(dateTimeDiff);

      foreach (ClickLogEntry clickLogEntry in _contentClickLog)
        clickLogEntry.TimeCorrect(dateTimeDiff);

      foreach (ImpressionLogEntry impressionLogEntry in _advertImpressionLog)
        impressionLogEntry.TimeCorrect(dateTimeDiff);

      foreach (ImpressionLogEntry impressionLogEntry in _contentImpressionLog)
        impressionLogEntry.TimeCorrect(dateTimeDiff);
    }

    /// <summary>
    /// Reads raw log files if they exist and converts their entries into LogEntry objects.
    /// For each log file with no data, the lock is released.
    /// If there are errors on a file, it is released.
    /// </summary>
    private bool LoadRawAll()
    {
      string contentClickLogsPath1 = _logPath + "\\ss_co_c_1.dat";
      string contentClickLogsPath2 = _logPath + "\\ss_co_c_2.dat";
      string contentImpressionLogsPath1 = _logPath + "\\ss_co_i_1.dat";
      string contentImpressionLogsPath2 = _logPath + "\\ss_co_i_2.dat";
      string advertClickLogsPath1 = _logPath + "\\ss_ad_c_1.dat";
      string advertClickLogsPath2 = _logPath + "\\ss_ad_c_2.dat";
      string advertImpressionLogsPath1 = _logPath + "\\ss_ad_i_1.dat";
      string advertImpressionLogsPath2 = _logPath + "\\ss_ad_i_2.dat";
      
      try
      {
        _contentClickLog = LogFileReader.TryGetEntries<ClickLogEntry>(ref _contentClickLogStream, contentClickLogsPath1, contentClickLogsPath2, _password, _logger);
      }
      catch (Exception ex)
      {
        // log and continue with the next file
        _logger.WriteError(ex);
      }

      try
      {
        _contentImpressionLog = LogFileReader.TryGetEntries<ImpressionLogEntry>(ref _contentImpressionLogStream, contentImpressionLogsPath1, contentImpressionLogsPath2, _password, _logger);
      }
      catch (Exception ex)
      {
        // log and continue with the next file
        _logger.WriteError(ex);
      }

      try
      {
        _advertClickLog = LogFileReader.TryGetEntries<ClickLogEntry>(ref _advertClickLogStream, advertClickLogsPath1, advertClickLogsPath2, _password, _logger);
      }
      catch (Exception ex)
      {
        // log and continue with the next file
        _logger.WriteError(ex);
      }

      try
      {
        _advertImpressionLog = LogFileReader.TryGetEntries<ImpressionLogEntry>(ref _advertImpressionLogStream, advertImpressionLogsPath1, advertImpressionLogsPath2, _password, _logger);
      }
      catch (Exception ex)
      {
        // log and continue with the next file
        _logger.WriteError(ex);
      }

      if (_advertImpressionLog.Count == 0 &&
        _contentImpressionLog.Count == 0 &&
        _advertClickLog.Count == 0 &&
        _contentClickLog.Count == 0)
        return false;

      return true;
    }

    /// <summary>
    /// Gets the channel subscriptions from disk and normalizes their channel weightings
    /// </summary>
    /// <returns>a ChannelSubscriptions object with channel subscription information</returns>
    private ChannelSubscriptions GetChannelSubscriptions()
    {
      if (!File.Exists(_channelSubscriptionPath))
        return null;

      ChannelSubscriptions channelSubscriptions = null;

      try
      {
        channelSubscriptions = (ChannelSubscriptions)Serializer.Deserialize(typeof(ChannelSubscriptions), _channelSubscriptionPath, _password);
      }
      catch (Exception ex)
      {
        _logger.WriteError(ex);
        return null;
      }

      channelSubscriptions.NormalizeChannelWeightings();

      return channelSubscriptions;
    }

    private GeneralData GetGeneralData()
    {
      GeneralData generalData = null;

      try
      {
        generalData = (GeneralData)Serializer.Deserialize(typeof(GeneralData), _generalDataPath, _password);
      }
      catch (Exception ex)
      {
        _logger.WriteError(ex);

        return null;
      }

      return generalData;
    }
    
    // Uploads non advert specific aggregated files to relay server. If an upload fails, the lock on the read file is released
    private void UploadNonAdvertSpecificAggregatedFiles(long channelID)
    {
      _bDateClicksPerAssetUploaded = TryLockAndUploadStringFromFile(ref _dateClicksPerAssetStream, _dateClicksPerAssetPath, "", channelID);

      if (!_bDateClicksPerAssetUploaded)
        Locker.ClearFileStream(ref _dateClicksPerAssetStream);

      _bDateImpressionsPerAssetUploaded = TryLockAndUploadStringFromFile(ref _dateImpressionsPerAssetStream, _dateImpressionsPerAssetPath, "", channelID);

      if (!_bDateImpressionsPerAssetUploaded)
        Locker.ClearFileStream(ref _dateImpressionsPerAssetStream);
    }

    // Uploads advert specific aggregated files to relay server. If an upload fails, the lock on the read file is released
    private void UploadAdvertSpecificAggregatedFiles()
    {
      _bAdvertChannelClickProportionsUploaded = TryLockAndUploadStringFromFile(ref _advertChannelClickProportionsStream, _advertChannelClickProportionsPath, "", -1L);

      if (!_bAdvertChannelClickProportionsUploaded)
        Locker.ClearFileStream(ref _advertChannelClickProportionsStream);

      _bAdvertChannelImpressionProportionsUploaded = TryLockAndUploadStringFromFile(ref _advertChannelImpressionProportionsStream, _advertChannelImpressionProportionsPath, "", -1L);

      if (!_bAdvertChannelImpressionProportionsUploaded)
        Locker.ClearFileStream(ref _advertChannelImpressionProportionsStream);
    }

    /// <summary>
    /// Locks a file and uploads a string to the relay server.
    /// </summary>
    /// <param name="fileStream">The file stream to use when accessing a log file</param>
    /// <param name="path1">First path to try. In the case of aggregated files, the only path to try</param>
    /// <param name="path2">Second path to try. Only the usage count file uses a path2 as the Screen Saver may be writing to either</param>
    private bool TryLockAndUploadStringFromFile(ref FileStream fileStream, string path1, string path2, long channelID)
    {
      MemoryStream ms = null;
      string fileNameWithoutPath = "";

      try
      {
        try
        {
          ms = Locker.ReadDecryptFile(ref fileStream, path1, _password, false);

          fileNameWithoutPath = Path.GetFileName(path1);
        }
        catch (IOException ioex) // if trying to read a usage count file, still two efforts need to be made. if aggregated files are read, a lock is already on them
        {
          _logger.WriteError(ioex);

          // if trying to read aggregated files variable path2 does not apply, an IOException is an error other than a file lock.
          if (path2 == "")
            return false;

          // if an exception is thrown trying path2, that will be caught outside
          ms = Locker.ReadDecryptFile(ref fileStream, path2, _password, false);

          fileNameWithoutPath = Path.GetFileName(path2);
        }

        if (ms != null)
          UploadMemoryStream(fileNameWithoutPath, ms, channelID); // if operation successful, stream will be closed on the server side
      }
      catch (Exception ex)
      {
        if (ms != null)
        {
          ms.Close();
          ms.Dispose();
        }

        _logger.WriteError(ex);

        return false;
      }

      return true;
    }

    private void UploadMemoryStream(string fileNameWithoutPath, Stream stream, long channelID)
    {
      LogDataParameterMessage lp = new LogDataParameterMessage();
      lp.FileName = fileNameWithoutPath;
      lp.SystemPassPhrase = _systemPassPhrase;
      lp.UserGUID = _userGUID;
      lp.MachineGUID = _machineGUID;
      lp.ChannelID = channelID.ToString();
      lp.LogFileStream = stream;

      _logger.WriteTimestampedMessage("Filename without path: " + fileNameWithoutPath);
     
      SimpleErrorWrapperMessage sw = null;

      try
      {
        sw = _userDataMarshallerStreamerClient.ProcessLogData(lp);
      }
      catch (CommunicationException ex)
      {
        throw ex;
      }
      
      if (sw.ErrorStatus == ErrorStatus.Failure)
      {
        throw new RemoteServerException(sw.ErrorCode, sw.ErrorSeverity.ToString(), sw.Message);
      }
    }

    private bool SetGlobals()
    {
      _advertImpressionLog = new List<ImpressionLogEntry>();
      _contentImpressionLog = new List<ImpressionLogEntry>();
      _advertClickLog = new List<ClickLogEntry>();
      _contentClickLog = new List<ClickLogEntry>();

      _dateClicksPerAssetSet = new List<DateOperationsPerAsset>();
      _dateImpressionsPerAssetSet = new List<DateOperationsPerAsset>();
      _advertChannelClickProportionsSet = new List<AdvertChannelOperationProportions>();
      _advertChannelImpressionProportionsSet = new List<AdvertChannelOperationProportions>();

      try
      {
        _debugFilePath = ConfigurationSettings.AppSettings["AppDataPath"] + "SettingsData\\OxigenDebugLE.txt";
        _userSettingsPath = ConfigurationSettings.AppSettings["AppDataPath"] + "SettingsData\\UserSettings.dat";
        _password = "password";

        _logger = new Logger("Log Exchanger", _debugFilePath, LoggingMode.Debug);

        _systemPassPhrase = "password";
        _appDataPath = ConfigurationSettings.AppSettings["AppDataPath"];
        _logPath = _appDataPath + "Other\\";
        _generalDataPath = _appDataPath + "SettingsData\\ss_general_data.dat";
        _usageCountLogsPath1 = _logPath + "ss_usg_1.dat";
        _usageCountLogsPath2 = _logPath + "ss_usg_2.dat";
        _channelSubscriptionPath = _appDataPath + "SettingsData\\ss_channel_subscription_data.dat";
        _settingsDataPath = _appDataPath + "SettingsData\\";

        GeneralData generalData = (GeneralData)Serializer.Deserialize(typeof(GeneralData), _generalDataPath, _password);

        _maxNoRelayLogServers = int.Parse(generalData.NoServers["relayLog"]);
        _serverTimeout = int.Parse(generalData.Properties["serverTimeout"]);
        _primaryDomainName = generalData.Properties["primaryDomainName"];
        _secondaryDomainName = generalData.Properties["secondaryDomainName"];
        _dateTimeDiffTolerance = int.Parse(generalData.Properties["dateTimeDiffTolerance"]);
      }
      catch (Exception ex)
      {
        if (_logger != null)
          _logger.WriteError(ex);

        return false;
      }

      _userDataMarshallerClient = new UserDataMarshallerClient();
      _userDataMarshallerStreamerClient = new UserDataMarshallerStreamerClient();

      User user = GetUserSettings();

      if (user == null)
        return false;

      _userGUID = user.UserGUID;
      _machineGUID = user.MachineGUID;
      _machineGUIDSuffix = user.GetMachineGUIDSuffix();

      return true;
    }

    private User GetUserSettings()
    {
      User user = null;

      try
      {
        user = (User)Serializer.Deserialize(typeof(User), _userSettingsPath, _password);
      }
      catch (Exception ex)
      {
        _logger.WriteError(ex);

        return null;
      }

      return user;
    }

    private string GetResponsiveServer(ServerType serverType, int maxNoServers, string endpointSuffix)
    {
      return GetResponsiveServer(serverType, maxNoServers, "", endpointSuffix);
    }

    private string GetResponsiveServer(ServerType serverType, int maxNoServers, string letter, string endpointSuffix)
    {
      try
      {
        return ResponsiveServerDeterminator.GetResponsiveURI(serverType, maxNoServers, _serverTimeout, 
          letter, _primaryDomainName, _secondaryDomainName, endpointSuffix);
      }
      catch (Exception ex)
      {
        _logger.WriteError(ex);

        return "";
      }
    }

    private TimeSpanWrapperClass GetTimeDiff(int dateTimeDiffTolerance)
    {
      DateTimeErrorWrapper dateTimeErrorWrapper = null;

      try
      {
        dateTimeErrorWrapper = _userDataMarshallerClient.GetCurrentServerTime(_systemPassPhrase);
      }
      catch (CommunicationException ex)
      {
        _logger.WriteError(ex);

        return null;
      }

      if (dateTimeErrorWrapper.ErrorStatus == ErrorStatus.Failure)
      {
          var errorReporting = (IErrorReporting) dateTimeErrorWrapper;

          _logger.WriteError(errorReporting.ErrorCode, errorReporting.Message);
          return null;
      }

      TimeSpan timeDiff = (dateTimeErrorWrapper.ReturnDateTime).Subtract(DateTime.Now);

      // if the time difference between the client and the server is less than
      // the acceptable date/time variance, leave unchanged.
      if (timeDiff < TimeSpan.FromMinutes(dateTimeDiffTolerance))
        return new TimeSpanWrapperClass(new TimeSpan(0));

      return new TimeSpanWrapperClass((dateTimeErrorWrapper.ReturnDateTime).Subtract(DateTime.Now));
    }
  }
}
