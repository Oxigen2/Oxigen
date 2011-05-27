using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using log4net;
using OxigenIIUserDataMarshallrService;
using ServiceErrorReporting;
using System.IO;
using System.ServiceModel;
using OxigenIIAdvertising.ServiceContracts.UserDataMarshaller;
using InterCommunicationStructures;
using System.Diagnostics;

namespace OxigenIIAdvertising.RelayServers
{
  /// <summary>
  /// User Data Marshaller
  /// </summary>
  [ServiceBehavior(Namespace = "http://oxigen.net")]
  public class UserDataMarshaller : IUserDataMarshaller, IUserDataMarshallerStreamer, 
    IUserDataMarshallerSU, IUserDataMarshallerSUStreamer
  {
    private string _appDataPath = ConfigurationManager.AppSettings["appDataPath"];
    private string _assetPath = ConfigurationManager.AppSettings["assetPath"];
    private string _logPath = ConfigurationManager.AppSettings["logPath"];
    private string _debugFilePath = ConfigurationManager.AppSettings["debugFilePath"];
    private string _machineSpecificDataPath = ConfigurationManager.AppSettings["machineSpecificDataPath"];
    private string _channelDataPath = ConfigurationManager.AppSettings["channelDataPath"];
    private string _systemPassPhrase = ConfigurationManager.AppSettings["systemPassPhrase"];
    private string _changesetPath = ConfigurationManager.AppSettings["changesetPath"];

    private static readonly ILog _applicationLog = LogManager.GetLogger("ApplicationLog");
    private static readonly ILog _impressionsLogger = LogManager.GetLogger("ImpressionsLogger");
    private static readonly ILog _clicksLogger = LogManager.GetLogger("ClicksLogger");
    private static readonly ILog _generalUsageLogger = LogManager.GetLogger("GeneralUsageLogger");
    private static readonly ILog _adClicksChannelProportionsLogger = LogManager.GetLogger("AdClicksChannelProportionsLogger");
    private static readonly ILog _adImpressionsChannelProportionsLogger = LogManager.GetLogger("AdImpressionsChannelProportionsLogger");
    private static readonly ILog _softwareVersionInfoLogger = LogManager.GetLogger("SoftwareVersionInfoLogger");

    /// <summary>
    /// Constructor for UserDataMarshaller that initializes a logger object
    /// </summary>
    public UserDataMarshaller()
    {
      log4net.Config.XmlConfigurator.Configure();
    }

    /// <summary>
    /// Gets the Relay server's time
    /// </summary>
    /// <param name="systemPassPhrase">Pass phrase to authenticate across services</param>
    /// <returns>A LongErrorWrapper object with the Relay server's time in ticks or with error information</returns>
    public DateTimeErrorWrapper GetCurrentServerTime(string systemPassPhrase)
    {
      DateTimeErrorWrapper dateTimeErrorWrapper = new DateTimeErrorWrapper();

      if (systemPassPhrase != _systemPassPhrase)
      {
        dateTimeErrorWrapper.ErrorCode = "ERR:001";
        dateTimeErrorWrapper.Message = "Authentication failure";
        dateTimeErrorWrapper.ErrorSeverity = ErrorSeverity.Retriable;
        dateTimeErrorWrapper.ErrorStatus = ErrorStatus.Failure;

        return dateTimeErrorWrapper;
      }

      dateTimeErrorWrapper.ReturnDateTime = DateTime.Now;
      dateTimeErrorWrapper.ErrorStatus = ErrorStatus.Success;

      return dateTimeErrorWrapper;
    }

    /// <summary>
    /// Receives log data from the client
    /// </summary>
    /// <param name="logDataParameterMessage">Message Wrapper that holds the system pass phrase, the user's GUID, the asset type, the log type and the stream with the log file</param>
    /// <returns>A SimpleErrorWrapperMessage object with the operation result</returns>
    public SimpleErrorWrapperMessage ProcessLogData(LogDataParameterMessage logDataParameterMessage)
    {
      SimpleErrorWrapperMessage simpleErrorWrapper = new SimpleErrorWrapperMessage();

      if (logDataParameterMessage.SystemPassPhrase != _systemPassPhrase)
      {
        simpleErrorWrapper.ErrorCode = "ERR:001";
        simpleErrorWrapper.Message = "Authentication failure";
        simpleErrorWrapper.ErrorSeverity = ErrorSeverity.Retriable;
        simpleErrorWrapper.ErrorStatus = ErrorStatus.Failure;

        return simpleErrorWrapper;
      }
      try
      {
        LogDataFile(logDataParameterMessage);
      }
      catch (Exception ex)
      {
        _applicationLog.Error(ex.ToString());
      }

      return simpleErrorWrapper;
    }

    private void LogDataFile(LogDataParameterMessage logDataParameterMessage)
    {
      var clientDataLogger = new ClientDataLogger();
      switch (logDataParameterMessage.ChannelID)
      {
        case "-1":
          clientDataLogger.LogAdImpressionOrClickChannelProportion(
            logDataParameterMessage.FileName == "i_d_a.dat" ? _adImpressionsChannelProportionsLogger : _adClicksChannelProportionsLogger,
            logDataParameterMessage.MachineGUID, logDataParameterMessage.LogFileStream);
          break;
        case "-2":
          clientDataLogger.LogGeneralUsage(_generalUsageLogger, logDataParameterMessage.LogFileStream);
          TryLogLastImpressionToDB(logDataParameterMessage.MachineGUID);
          break;
        default:
          ILog log;
          if (logDataParameterMessage.FileName == "i_d_a.dat")
          {
            //We do not always receive these so log LastImpression when we recieve General Usage file instead
            //TryLogLastImpressionToDB(logDataParameterMessage.MachineGUID);
            log = _impressionsLogger;
          }
          else
          {
            TryLogLastClickToDB(logDataParameterMessage.MachineGUID);
            log = _clicksLogger;
          }
          clientDataLogger.LogImpressionsOrClicks(log, logDataParameterMessage.MachineGUID, logDataParameterMessage.ChannelID, logDataParameterMessage.LogFileStream);
          break;
      }
    }

    private void TryLogLastClickToDB(string machineGuid)
    {
      TryRunSQL(string.Format("update PCs Set LastImpressionDate = GetDate() where PCGUID = '{0}'", machineGuid));
    }

    private void TryLogLastImpressionToDB(string machineGuid)
    {
      TryRunSQL(string.Format("update PCs Set LastClickDate = GetDate() where PCGUID = '{0}'", machineGuid));
    }

    private void TryRunSQL(string sql)
    {
      try
      {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Oxigen"].ConnectionString))
        {
          SqlCommand command = new SqlCommand(sql, connection);
          connection.Open();
          command.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        _applicationLog.Error(ex.ToString());
      }
    }

    private void SaveLogStreamAndDispose(SimpleErrorWrapperMessage simpleErrorWrapper, Stream readStream, string path, string fileName)
    {
      FileStream fileStream = null;

      try
      {
        if (!Directory.Exists(path))
          Directory.CreateDirectory(path);

        fileStream = new FileStream(path + "\\" + DateTime.Now.Ticks + "_" + fileName, FileMode.CreateNew);

        SaveStream(readStream, fileStream);
      }
      catch (Exception ex)
      {
   //     _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);

        simpleErrorWrapper.ErrorCode = "ERR:003";
        simpleErrorWrapper.Message = "Could not save file";
        simpleErrorWrapper.ErrorSeverity = ErrorSeverity.Retriable;
        simpleErrorWrapper.ErrorStatus = ErrorStatus.Failure;

        return;
      }
      finally
      {
        OperationContext clientContext = OperationContext.Current;

        clientContext.OperationCompleted += new EventHandler(delegate(object sender, EventArgs e)
          {
            if (readStream != null)
              readStream.Dispose();

            if (fileStream != null)
              fileStream.Dispose();
          });
      }

      simpleErrorWrapper.ErrorStatus = ErrorStatus.Success;
    }

    /// <summary>
    /// Gets an asset file in the form of an array of bytes
    /// </summary>
    /// <param name="assetFileParameterMessage">Message wrapper for GetAssetFile parameters</param>
    /// <returns>a StreamErrorWrapper with the relevant stream or with a code that specifies that the file is the same as the one on the client side or with error information</returns>
    public StreamErrorWrapper GetAssetFile(AssetFileParameterMessage assetFileParameterMessage)
    {
      StreamErrorWrapper streamErrorWrapper = new StreamErrorWrapper();

      // close returned stream on completion
      OperationContext clientContext = OperationContext.Current;

      clientContext.OperationCompleted += new EventHandler(delegate(object sender, EventArgs args)
      {
        if (streamErrorWrapper.ReturnStream != null)
          streamErrorWrapper.ReturnStream.Dispose();
      });

      if (assetFileParameterMessage.SystemPassPhrase != _systemPassPhrase)
      {
        streamErrorWrapper.ErrorCode = "ERR:001";
        streamErrorWrapper.Message = "Authentication failure";
        streamErrorWrapper.ErrorSeverity = ErrorSeverity.Retriable;
        streamErrorWrapper.ErrorStatus = ErrorStatus.Failure;
        streamErrorWrapper.ReturnStream = new MemoryStream(new byte[0]);

        return streamErrorWrapper;
      }

      // _assetPath/<asset's GUID suffix>/asset filename
      string assetFullPath = _assetPath +
        assetFileParameterMessage.AssetFileName.Substring(assetFileParameterMessage.AssetFileName.LastIndexOf("_") + 1, 1)
        + "\\" + assetFileParameterMessage.AssetFileName;

      if (!File.Exists(assetFullPath))
      {
        streamErrorWrapper.ErrorCode = "ERR:002";
        streamErrorWrapper.Message = "File not found";
        streamErrorWrapper.ErrorSeverity = ErrorSeverity.Retriable; // search for asset file later
        streamErrorWrapper.ErrorStatus = ErrorStatus.Failure;
        streamErrorWrapper.ReturnStream = new MemoryStream(new byte[0]);
        return streamErrorWrapper;
      }

      string serverSideChecksum = ReadStringFromFile(Path.GetDirectoryName(assetFullPath) + "\\" + Path.GetFileNameWithoutExtension(assetFullPath) + ".chk");

      if (assetFileParameterMessage.Checksum == serverSideChecksum)
      {
        streamErrorWrapper.ErrorStatus = ErrorStatus.ChecksumEqual;
        streamErrorWrapper.ReturnStream = new MemoryStream(new byte[0]);
        return streamErrorWrapper;
      }

      TryReadFileWithReadLock(streamErrorWrapper, assetFullPath);

      return streamErrorWrapper;
    }

    /// <summary>
    /// Gets the XML files (in the form of an array of bytes) that hold general or advert condition data, if different from the client's
    /// </summary>
    /// <param name="appDataFileParameterMessage">Message contract to pass as a parameter</param>
    /// <returns>a StreamErrorWrapper with the relevant stream or with a code that specifies that the file is the same as the one on the client side or with error information</returns>
    public StreamErrorWrapper GetAppDataFiles(AppDataFileParameterMessage appDataFileParameterMessage)
    {
      StreamErrorWrapper streamErrorWrapper = new StreamErrorWrapper();

      // close returned stream on completion
      OperationContext clientContext = OperationContext.Current;

      clientContext.OperationCompleted += new EventHandler(delegate(object sender, EventArgs args)
      {
        if (streamErrorWrapper.ReturnStream != null)
          streamErrorWrapper.ReturnStream.Dispose();
      });

      if (appDataFileParameterMessage.SystemPassPhrase != _systemPassPhrase)
      {
        streamErrorWrapper.ErrorCode = "ERR:001";
        streamErrorWrapper.Message = "Authentication failure";
        streamErrorWrapper.ErrorSeverity = ErrorSeverity.Retriable;
        streamErrorWrapper.ErrorStatus = ErrorStatus.Failure;
        streamErrorWrapper.ReturnStream = new MemoryStream(new byte[0]);

        return streamErrorWrapper;
      }

      string appDataFullPath = "";

      switch (appDataFileParameterMessage.DataFileType)
      {
        case DataFileType.AdvertConditions:
          appDataFullPath = _appDataPath + "ss_adcond_data.dat";
          break;
        case DataFileType.GeneralConfiguration:
          appDataFullPath = _appDataPath + "ss_general_data.dat";
          break;
        case DataFileType.ChannelData:
          appDataFullPath = _channelDataPath + appDataFileParameterMessage.ChannelID + "_channel.dat";
          break;
      }

      if (!File.Exists(appDataFullPath))
      {
        streamErrorWrapper.ErrorStatus = ErrorStatus.Failure;
        streamErrorWrapper.ErrorSeverity = ErrorSeverity.Retriable;
        streamErrorWrapper.ErrorCode = "ERR:002";
        streamErrorWrapper.Message = "File does not exist";
        streamErrorWrapper.ReturnStream = new MemoryStream(new byte[0]); // stream must not be null as it is the message body
        return streamErrorWrapper;
      }

      string serverSideChecksum = ReadStringFromFile(Path.GetDirectoryName(appDataFullPath) + "\\" + Path.GetFileNameWithoutExtension(appDataFullPath) + ".chk");

      if (appDataFileParameterMessage.Checksum == serverSideChecksum)
      {
        streamErrorWrapper.ErrorStatus = ErrorStatus.ChecksumEqual;
        streamErrorWrapper.ReturnStream = new MemoryStream(new byte[0]);
        return streamErrorWrapper;
      }

      TryReadFileWithReadLock(streamErrorWrapper, appDataFullPath);

      return streamErrorWrapper;
    }

    private void TryReadFileWithReadLock(StreamErrorWrapper streamErrorWrapper, string fileFullPath)
    {
      FileStream fs = null;

      try
      {
        fs = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
      }
      catch (Exception ex)
      {
//        _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);

        if (fs != null)
          fs.Dispose();

        streamErrorWrapper.ErrorCode = "ERR:006";
        streamErrorWrapper.ErrorSeverity = ErrorSeverity.Retriable;
        streamErrorWrapper.ErrorStatus = ErrorStatus.Failure;
        streamErrorWrapper.Message = "Could not read file.";
        streamErrorWrapper.ReturnStream = new MemoryStream(new byte[0]);

        return;
      }

      streamErrorWrapper.ErrorStatus = ErrorStatus.Success;
      streamErrorWrapper.ReturnStream = fs;
    }
    
    /// <summary>
    /// Adds Heartbeat information to an XML file ready to be passed on to the Master Server database
    /// </summary>
    /// <param name="systemPassPhrase">Pass phrase to authenticate across services</param>
    /// <param name="machineGUID">The user's machine's unique identifier across the system</param>
    /// <param name="userGUID">The user's unique identifier</param>
    /// <returns>A SimpleErrorWrapper object with the operation result</returns>
    public SimpleErrorWrapper RegisterHeartBeat(string systemPassPhrase, string userGUID, string machineGUID)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Adds Uninstall information to an XML file ready to be passed on to the Master Server database
    /// </summary>
    /// <param name="systemPassPhrase">Pass phrase to authenticate across services</param>
    /// <param name="userGUID">The user's unique identifier across the system</param>
    /// <returns>A SimpleErrorWrapper object with the operation result</returns>
    public SimpleErrorWrapper RegisterSoftwareUninstall(string systemPassPhrase, string userGUID, string machineGUID)
    {
      SimpleErrorWrapper simpleErrorWrapper = new SimpleErrorWrapper();

      if (systemPassPhrase != "password")
      {
        simpleErrorWrapper.ErrorCode = "ERR:001";
        simpleErrorWrapper.Message = "Authentication failure";
        simpleErrorWrapper.ErrorSeverity = ErrorSeverity.Retriable;
        simpleErrorWrapper.ErrorStatus = ErrorStatus.Failure;

        return simpleErrorWrapper;
      }

      string uninstallInfoPath = _machineSpecificDataPath + machineGUID + "\\software_uninstall.dat";

      if (!WriteTextToNewFile(DateTime.Now.ToString(), uninstallInfoPath))
      {
        simpleErrorWrapper.ErrorStatus = ErrorStatus.Failure;
        simpleErrorWrapper.ErrorSeverity = ErrorSeverity.Retriable;
        simpleErrorWrapper.ErrorCode = "ERR:005";
        simpleErrorWrapper.Message = "Uninstall info could not be registered.";

        return simpleErrorWrapper;
      }

      simpleErrorWrapper.ErrorStatus = ErrorStatus.Success;

      return simpleErrorWrapper;
    }

    /// <summary>
    /// Sets the user's current screen saver product from the computer the information has come from
    /// </summary>
    /// <param name="systemPassPhrase">Pass phrase to authenticate across services</param>
    /// <param name="userGUID">The user's unique identifier across the system</param>
    /// <param name="machineGUID">The user's machine's unique identifier across the system</param>
    /// <param name="screenSaverName"></param>
    /// <returns>A SimpleErrorWrapper object with the operation result</returns>
    public SimpleErrorWrapper SetCurrentScreenSaverProduct(string systemPassPhrase, string userGUID, string machineGUID, string screenSaverName)
    {
      throw new NotImplementedException();
    }

    private bool WriteTextToNewFile(string data, string fullPath)
    {
      string pathWithoutFilename = Path.GetDirectoryName(fullPath);

      try
      {
        if (!Directory.Exists(pathWithoutFilename))
          Directory.CreateDirectory(pathWithoutFilename);

        File.WriteAllText(fullPath, data);
      }
      catch (Exception ex)
      {
//        _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);

        return false;
      }

      return true;
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

    private string ReadStringFromFile(string fullPath)
    {
      try
      {
        return File.ReadAllText(fullPath);
      }
      catch (Exception ex)
      {
//        _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);

        // if an error occurs, then return a string that can never be a checksum. This
        // will trigger the download to the client.
        return "--";
      }
    }

    public StreamErrorWrapper GetComponentList(VersionParameterMessage message)
    {
      StreamErrorWrapper streamErrorWrapper = new StreamErrorWrapper();

      // close returned stream on completion
      OperationContext clientContext = OperationContext.Current;

      clientContext.OperationCompleted += new EventHandler(delegate(object sender, EventArgs args)
      {
        if (streamErrorWrapper.ReturnStream != null)
          streamErrorWrapper.ReturnStream.Dispose();
      });

      if (message.SystemPassPhrase != _systemPassPhrase)
      {
        streamErrorWrapper.ErrorCode = "ERR:001";
        streamErrorWrapper.Message = "Authentication failure";
        streamErrorWrapper.ErrorSeverity = ErrorSeverity.Retriable;
        streamErrorWrapper.ErrorStatus = ErrorStatus.Failure;
        streamErrorWrapper.ReturnStream = new MemoryStream(new byte[0]);

        return streamErrorWrapper;
      }

      string appDataFullPath = _changesetPath + message.Version + "\\ss_component_list.dat";

      if (!File.Exists(appDataFullPath))
      {
        _applicationLog.Warn("Attempted access to " + appDataFullPath + ". File does not exist");

        streamErrorWrapper.ErrorStatus = ErrorStatus.NoData;
        streamErrorWrapper.ErrorSeverity = ErrorSeverity.Retriable;
        streamErrorWrapper.ErrorCode = "WARN:001";
        streamErrorWrapper.Message = "File does not exist";
        streamErrorWrapper.ReturnStream = new MemoryStream(new byte[0]); // stream must not be null as it is the message body
        return streamErrorWrapper;
      }

      TryReadFileWithReadLock(streamErrorWrapper, appDataFullPath);

      return streamErrorWrapper;
    }

    public SimpleErrorWrapper SetCurrentVersionInfo(string userGUID, 
                                                    string machineGUID, 
                                                    string version, 
                                                    string systemPassPhrase)
    {
      SimpleErrorWrapper simpleErrorWrapper = new SimpleErrorWrapper();

      if (systemPassPhrase != _systemPassPhrase)
      {
        simpleErrorWrapper.ErrorCode = "ERR:001";
        simpleErrorWrapper.Message = "Authentication failure";
        simpleErrorWrapper.ErrorSeverity = ErrorSeverity.Retriable;
        simpleErrorWrapper.ErrorStatus = ErrorStatus.Failure;

        return simpleErrorWrapper;
      }

      var clientDataLogger = new ClientDataLogger();

      try
      {
        clientDataLogger.LogLatestSoftwareVersionInfo(_softwareVersionInfoLogger, machineGUID, version);
      }
      catch (Exception ex)
      {
        _applicationLog.Error("Software version info could not be registered: " + ex);

        simpleErrorWrapper.ErrorStatus = ErrorStatus.Failure;
        simpleErrorWrapper.ErrorSeverity = ErrorSeverity.Retriable;
        simpleErrorWrapper.ErrorCode = "ERR:005";
        simpleErrorWrapper.Message = "Software version info could not be registered.";

        return simpleErrorWrapper;
      }

      simpleErrorWrapper.ErrorStatus = ErrorStatus.Success;

      return simpleErrorWrapper;
    }
  }
}
