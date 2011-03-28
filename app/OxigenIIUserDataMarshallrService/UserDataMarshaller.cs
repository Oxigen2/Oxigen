using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceErrorReporting;
using System.IO;
using OxigenIIAdvertising.FileChecksumCalculator;
using System.ServiceModel;
using OxigenIIAdvertising.ServiceContracts.UserDataMarshaller;
using System.Web;
using OxigenIIAdvertising.XMLSerializer;
using InterCommunicationStructures;
using System.Diagnostics;
using OxigenIIAdvertising.LoggerInfo;

namespace OxigenIIAdvertising.RelayServers
{
  /// <summary>
  /// User Data Marshaller
  /// </summary>
  [ServiceBehavior(Namespace = "http://oxigen.net")]
  public class UserDataMarshaller : IUserDataMarshaller, IUserDataMarshallerStreamer, 
    IUserDataMarshallerSU, IUserDataMarshallerSUStreamer
  {
    private string _appDataPath = System.Configuration.ConfigurationSettings.AppSettings["appDataPath"];
    private string _assetPath = System.Configuration.ConfigurationSettings.AppSettings["assetPath"];
    private string _logPath = System.Configuration.ConfigurationSettings.AppSettings["logPath"];
    private string _debugFilePath = System.Configuration.ConfigurationSettings.AppSettings["debugFilePath"];
    private string _machineSpecificDataPath = System.Configuration.ConfigurationSettings.AppSettings["machineSpecificDataPath"];
    private string _channelDataPath = System.Configuration.ConfigurationSettings.AppSettings["channelDataPath"];
    private string _systemPassPhrase = System.Configuration.ConfigurationSettings.AppSettings["systemPassPhrase"];
    private string _changesetPath = System.Configuration.ConfigurationSettings.AppSettings["changesetPath"];

    private EventLog _eventLog = null;
    Logger _logger = null;

    /// <summary>
    /// Constructor for UserDataMarshaller that initializes a logger object
    /// </summary>
    public UserDataMarshaller()
    {
      _logger = new Logger("UDM", _debugFilePath + "Debug.txt", LoggingMode.Debug);

      _eventLog = new EventLog();
      _eventLog.Log = "Oxigen Services";
      _eventLog.Source = "Oxigen User Data Marshaller";
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

      string folderPath = null;

      switch (logDataParameterMessage.ChannelID)
      {
        case "-1":
          folderPath = _logPath + logDataParameterMessage.MachineGUID + "\\AdvertSpecificLogs";
          break;
        case "-2":
          folderPath = _logPath + logDataParameterMessage.MachineGUID + "\\UsageLogs";
          break;
        default:
          folderPath = _logPath + logDataParameterMessage.MachineGUID + "\\" + logDataParameterMessage.ChannelID;
          break;
      }

      SaveLogStreamAndDispose(simpleErrorWrapper, logDataParameterMessage.LogFileStream, folderPath, logDataParameterMessage.FileName);

      return simpleErrorWrapper;
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
      SimpleErrorWrapper simpleErrorWrapper = new SimpleErrorWrapper();

      if (systemPassPhrase != "password")
      {
        simpleErrorWrapper.ErrorCode = "ERR:001";
        simpleErrorWrapper.Message = "Authentication failure";
        simpleErrorWrapper.ErrorSeverity = ErrorSeverity.Retriable;
        simpleErrorWrapper.ErrorStatus = ErrorStatus.Failure;

        return simpleErrorWrapper;
      }

      string heartbeatPath = _machineSpecificDataPath + machineGUID + "\\heartbeat.dat";

      if (!WriteTextToNewFile(DateTime.Now.ToString(), heartbeatPath))
      {
        simpleErrorWrapper.ErrorStatus = ErrorStatus.Failure;
        simpleErrorWrapper.ErrorSeverity = ErrorSeverity.Retriable;
        simpleErrorWrapper.ErrorCode = "ERR:004";
        simpleErrorWrapper.Message = "Heartbeat could not be registered.";

        return simpleErrorWrapper;
      }

      simpleErrorWrapper.ErrorStatus = ErrorStatus.Success;

      return simpleErrorWrapper;
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
      SimpleErrorWrapper simpleErrorWrapper = new SimpleErrorWrapper();

      if (systemPassPhrase != _systemPassPhrase)
      {
        simpleErrorWrapper.ErrorCode = "ERR:001";
        simpleErrorWrapper.Message = "Authentication failure";
        simpleErrorWrapper.ErrorSeverity = ErrorSeverity.Retriable;
        simpleErrorWrapper.ErrorStatus = ErrorStatus.Failure;

        return simpleErrorWrapper;
      }

      string currentScreenSaverInfoPath = _machineSpecificDataPath + machineGUID + "\\current_screensaver_info.dat";

      if (!WriteTextToNewFile(DateTime.Now.ToString() + "||" + screenSaverName, currentScreenSaverInfoPath))
      {
        simpleErrorWrapper.ErrorStatus = ErrorStatus.Failure;
        simpleErrorWrapper.ErrorSeverity = ErrorSeverity.Retriable;
        simpleErrorWrapper.ErrorCode = "ERR:004";
        simpleErrorWrapper.Message = "Screensaver info could not be registered.";

        return simpleErrorWrapper;
      }

      simpleErrorWrapper.ErrorStatus = ErrorStatus.Success;

      return simpleErrorWrapper;
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
//        _eventLog.WriteEntry("Attempted access to " + appDataFullPath + ". File does not exist", EventLogEntryType.Warning);

        _logger.WriteTimestampedMessage("Attempted access to " + appDataFullPath + ". File does not exist.");
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

      string path = _machineSpecificDataPath + machineGUID + "\\current_software.dat";

      if (!WriteTextToNewFile(DateTime.Now.ToString() + "||" + version, path))
      {
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
