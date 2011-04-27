using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.Diagnostics;
using ServiceErrorReporting;
using System.IO;
using InterCommunicationStructures;
using OxigenIIAdvertising.LoggerInfo;

namespace OxigenIIDownloadServers
{
  [ServiceBehavior(Namespace = "http://oxigen.net")]
  public class UserFileMarshaller : IUserFileMarshaller
  {
    private EventLog _eventLog = null;

    private string _systemPassPhrase = System.Configuration.ConfigurationSettings.AppSettings["systemPassPhrase"];
    private string _changesetPath = System.Configuration.ConfigurationSettings.AppSettings["changeSetPath"];
    private string _debugFilePath = System.Configuration.ConfigurationSettings.AppSettings["debugFilePath"];
    private Logger _logger = null;

    public UserFileMarshaller()
    {
      _eventLog = new EventLog();
      _eventLog.Log = String.Empty;
      _eventLog.Source = "Oxigen User File Marshaller";
    }

    public StreamErrorWrapper GetComponent(ComponentParameterMessage message)
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

      string changesetFullPath = _changesetPath + message.VersionNumber + "\\" + message.ComponentFileName;

      if (!File.Exists(changesetFullPath))
      {
        _logger.WriteTimestampedMessage("File does not exist: " + changesetFullPath);

        streamErrorWrapper.ErrorCode = "ERR:002";
        streamErrorWrapper.Message = "File not found";
        streamErrorWrapper.ErrorSeverity = ErrorSeverity.Retriable; // search for asset file later
        streamErrorWrapper.ErrorStatus = ErrorStatus.Failure;
        streamErrorWrapper.ReturnStream = new MemoryStream(new byte[0]);
        return streamErrorWrapper;
      }

      TryReadFileWithReadLock(streamErrorWrapper, changesetFullPath);

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
        _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);

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
  }
}
