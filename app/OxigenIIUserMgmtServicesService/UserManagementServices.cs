using System;
using System.Collections.Generic;
using System.Text;
using ServiceErrorReporting;
using OxigenIIAdvertising.ServiceContracts.UserManagementServices;
using InterCommunicationStructures;
using System.ServiceModel;
using System.IO;
using OxigenIIAdvertising.XMLSerializer;
using OxigenIIAdvertising.AppData;
using OxigenIIAdvertising.Demographic;
using System.Diagnostics;
using OxigenIIAdvertising.DAClients;
using System.Transactions;
using OxigenIIUserMgmtServicesService;
using OxigenIIAdvertising.SOAStructures;
using log4net;

namespace OxigenIIAdvertising.MasterServers
{
  /// <summary>
  /// Implements User management communication operations
  /// </summary>
  public class UserManagementServices : IUserManagementServicesStreamer, IUserManagementServicesNonStreamer
  {
    private string _demoDataPath = System.Configuration.ConfigurationSettings.AppSettings["demoDataPath"];
    private string _userChannelSubscriptionsPath = System.Configuration.ConfigurationSettings.AppSettings["userChannelSubscriptionsPath"];
    private string _debugFilePath = System.Configuration.ConfigurationSettings.AppSettings["debugFilePath"];
    private string _systemPassPhrase = System.Configuration.ConfigurationSettings.AppSettings["systemPassPhrase"];
    private string _smtpServer = System.Configuration.ConfigurationSettings.AppSettings["SMTPserver"];

    private EventLog _eventLog = null;

    private readonly ILog _logger = LogManager.GetLogger(typeof(UserManagementServices));

    public UserManagementServices()
    {
      _eventLog = new EventLog();
      _eventLog.Log = String.Empty;
      _eventLog.Source = "Oxigen User Management Services";
      log4net.Config.XmlConfigurator.Configure();
    }

    /// <summary>
    /// Used by the Content Exchanger to receive uses-specific data
    /// </summary>
    /// <param name="appDataFileParameterMessage">Stream wrapper for the user specific data</param>
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

      switch (appDataFileParameterMessage.DataFileType)
      {
        case DataFileType.DemographicData:
          streamErrorWrapper.ReturnStream = GetDemographicDataStream(streamErrorWrapper, appDataFileParameterMessage.UserGUID);
          break;
        case DataFileType.ChannelSubscriptions:
          streamErrorWrapper.ReturnStream = GetChannelSubscriptionStream(streamErrorWrapper, appDataFileParameterMessage.UserGUID, appDataFileParameterMessage.MachineGUID);
          break;
      }
      
      return streamErrorWrapper;
    }
    
    private MemoryStream GetDemographicDataStream(StreamErrorWrapper streamErrorWrapper, string userGUID)
    {
      MemoryStream ms = null;
      DemographicData dd = null;
      DAClient client = null;

      try
      {
        client = new DAClient();

        dd = client.GetUserDemographicData(userGUID);

        if (dd != null)
          ms = Serializer.SerializeClear(dd);
      }
      catch (Exception ex)
      {
        _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);

        streamErrorWrapper.ErrorCode = "ERR:003";
        streamErrorWrapper.Message = "Could not get demographic data";
        streamErrorWrapper.ErrorSeverity = ErrorSeverity.Retriable;
        streamErrorWrapper.ErrorStatus = ErrorStatus.Failure;
        streamErrorWrapper.ReturnStream = new MemoryStream(new byte[0]);

        return null;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      streamErrorWrapper.ErrorStatus = ErrorStatus.Success;
      
      if (ms == null)
        return new MemoryStream(new byte[0]);

      return ms;
    }

    private MemoryStream GetChannelSubscriptionStream(StreamErrorWrapper streamErrorWrapper, string userGUID, 
      string machineGUID)
    {
      MemoryStream ms = null;
      ChannelSubscriptions channelSubscriptions = null;

      DAClient client = null;

      try
      {
        client = new DAClient();

        channelSubscriptions = client.GetUserChannelSubscriptions(userGUID, machineGUID);

        if (channelSubscriptions != null)
          ms = Serializer.SerializeClear(channelSubscriptions);
      }
      catch (Exception ex)
      {
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        streamErrorWrapper.ErrorCode = "ERR:003";
        streamErrorWrapper.Message = "Could not get channel subscription data";
        streamErrorWrapper.ErrorSeverity = ErrorSeverity.Retriable;
        streamErrorWrapper.ErrorStatus = ErrorStatus.Failure;
        streamErrorWrapper.ReturnStream = new MemoryStream(new byte[0]);

        return null;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      streamErrorWrapper.ErrorStatus = ErrorStatus.Success;

      if (ms == null)
        return new MemoryStream(new byte[0]);

      return ms;
    }

    private SimpleErrorWrapper GetUnauthorizedAccessErrorWrapper(string systemPassPhrase)
    {
      SimpleErrorWrapper wrapper = new SimpleErrorWrapper();
      _eventLog.WriteEntry("An attempt was made to access the service with an incorrect pass phrase: " + systemPassPhrase, EventLogEntryType.Warning);
      wrapper.ErrorSeverity = ErrorSeverity.Severe;
      wrapper.ErrorStatus = ErrorStatus.Failure;
      wrapper.Message = "An error has occurred.";
      return wrapper;
    }

    private SimpleErrorWrapper GetGenericRetriableErrorWrapper()
    {
      SimpleErrorWrapper wrapper = new SimpleErrorWrapper();

      wrapper.ErrorSeverity = ErrorSeverity.Retriable;
      wrapper.ErrorStatus = ErrorStatus.Failure;
      wrapper.Message = "An error has occurred. Please try later.";
      return wrapper;
    }

    private SimpleErrorWrapper GetGenericSuccessWrapper()
    {
      SimpleErrorWrapper wrapper = new SimpleErrorWrapper();

      wrapper.ErrorStatus = ErrorStatus.Success;
      return wrapper;
    }

    public SimpleErrorWrapper GetUserExistsByUserCredentials(string emailAddress, string password, 
      out string userGUID, string systemPassPhrase)
    {
      userGUID = null;

      if (systemPassPhrase != _systemPassPhrase)
        return GetUnauthorizedAccessErrorWrapper(systemPassPhrase);

      int result;

      DAClient client = null;

      try
      {
        client = new DAClient();

        result = client.GetUserExistsByUserCredentials(emailAddress, password, out userGUID);
      }
      catch (Exception ex)
      {
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        return GetGenericRetriableErrorWrapper();
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      SimpleErrorWrapper wrapper = new SimpleErrorWrapper();
      wrapper.ErrorStatus = ErrorStatus.Success;

      switch (result)
      {
        case -1:
          wrapper.ErrorCode = "EEPN"; // e-mail exists, password not
          break;
        case -2:
          wrapper.ErrorCode = "ENPN"; // e-mail and password do not exist
          break;
        default:
          wrapper.ErrorCode = "OK";
          break;
      }      

      return wrapper;
    }    

    private void CreateDefaultThumbnailForChannel(string channelGUID)
    {
      string thumbnailChannelPath = (string)System.Configuration.ConfigurationSettings.AppSettings["thumbnailChannelPath"];
      string channelSubDir = channelGUID.Substring(channelGUID.LastIndexOf('_') + 1, 1);
      string thumbnailChannelPathWithoutFilename = thumbnailChannelPath + channelSubDir;

      if (!Directory.Exists(thumbnailChannelPathWithoutFilename))
        Directory.CreateDirectory(thumbnailChannelPathWithoutFilename);

      File.Copy(thumbnailChannelPath + "\\channel-personal.jpg", thumbnailChannelPathWithoutFilename + "\\" + channelGUID + ".jpg");
    }

    public SimpleErrorWrapper GetExistingUserDetails(string userGUID, string password, 
      out UserInfo userInfo, string systemPassPhrase)
    {
      userInfo = null;

      if (systemPassPhrase != _systemPassPhrase)
        return GetUnauthorizedAccessErrorWrapper(systemPassPhrase);

      string firstName;
      string lastName;
      string gender;
      DateTime dob;
      string country;
      string state;
      string townCity;
      string occupationSector;
      string employmentLevel;
      string annualHouseholdIncome;

      DAClient client = null;
      
      try
      {
        client = new DAClient();

        client.GetExistingUserDetails(userGUID, password, out firstName, out lastName, out gender,
          out dob, out country, out state, out townCity, out occupationSector,
          out employmentLevel, out annualHouseholdIncome);
      }
      catch (Exception ex)
      {
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        return GetGenericRetriableErrorWrapper();
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      userInfo = new UserInfo
      {
        FirstName = firstName,
        LastName = lastName,
        Gender = gender,
        DOB = dob,
        Country = country,
        State = state,
        TownCity = townCity,
        OccupationSector = occupationSector,
        EmploymentLevel = employmentLevel,
        AnnualHouseholdIncome = annualHouseholdIncome
      };

      return GetGenericSuccessWrapper();
    }

    public SimpleErrorWrapper UpdateUserAccount(string emailAddress, string password, 
      string firstName, string lastName,
      string gender, DateTime dob, string townCity, string state, string country, 
      string occupationSector, string employmentLevel,
      string annualHouseholdIncome,
      AppData.ChannelSubscriptions channelSubscriptions,
      int softwareMajorVersionNumber,
      int softwareMinorVersionNumber,
      string machineGUID,
      string newPcName,
      string macAddress,
      string systemPassPhrase)
    {
      if (systemPassPhrase != _systemPassPhrase)
        GetUnauthorizedAccessErrorWrapper(systemPassPhrase);

      DAClient client = null;

      using (TransactionScope ts = new TransactionScope())
      {
        try
        {
          client = new DAClient();

          client.UpdateUserAccount(emailAddress, password, firstName, lastName, gender, dob,
            townCity, state, country, occupationSector,
            employmentLevel, annualHouseholdIncome, 
            channelSubscriptions, 
            softwareMajorVersionNumber,
            softwareMinorVersionNumber, 
            machineGUID, 
            newPcName, 
            macAddress);
        }
        catch (Exception ex)
        {
          _logger.Error(ex.ToString());
          
          try
          {
            _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
          }
          catch
          {
            // ignore exception
          }

          return GetGenericRetriableErrorWrapper();
        }
        finally
        {
          if (client != null)
            client.Dispose();
        }
      }

      return GetGenericSuccessWrapper();
    }

    public SimpleErrorWrapper GetPcListForInstallerEmail(string emailAddress, out List<PcInfo> pcs, string systemPassPhrase)
    {
      pcs = null;

      if (systemPassPhrase != _systemPassPhrase)
        return GetUnauthorizedAccessErrorWrapper(systemPassPhrase);

      DAClient client = null;

      try
      {
        client = new DAClient();

        pcs = client.GetPcListForInstallerEmail(emailAddress);
      }
      catch (Exception ex)
      {
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        return GetGenericRetriableErrorWrapper();
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return GetGenericSuccessWrapper();
    }

    public SimpleErrorWrapper GetPcListForInstallerGUID(string userGUID, out List<PcInfo> pcs, string systemPassPhrase)
    {
      pcs = null;

      if (systemPassPhrase != _systemPassPhrase)
        GetUnauthorizedAccessErrorWrapper(systemPassPhrase);

      DAClient client = null;

      try
      {
        client = new DAClient();

        pcs = client.GetPcListForInstallerGUID(userGUID);
      }
      catch (Exception ex)
      {
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        return GetGenericRetriableErrorWrapper();
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return GetGenericSuccessWrapper();
    }

    public SimpleErrorWrapper LinkPCAndSubscriptionsExistingPC(string userGUID, int pcID, string machineGUID, string macAddress,
      int softwareMajorVersionNumber, int softwareMinorVersionNumber, ChannelSubscriptions channelSubscriptions, string systemPassPhrase)
    {
      if (systemPassPhrase != _systemPassPhrase)
        return GetUnauthorizedAccessErrorWrapper(systemPassPhrase);

      DAClient client = null;

      try
      {
        client = new DAClient();

        client.LinkPCAndSubscriptionsExistingPC(userGUID, pcID, machineGUID, macAddress,
          softwareMajorVersionNumber, softwareMinorVersionNumber, channelSubscriptions);
      }
      catch (Exception ex)
      {
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        return GetGenericRetriableErrorWrapper();
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return GetGenericSuccessWrapper();
    }

    public SimpleErrorWrapper RegisterNewUser(string emailAddress, string password, 
      string firstName, string lastName,
      string gender, DateTime dob, string townCity, string state, string country, 
      string occupationSector, string employmentLevel, string annualHouseholdIncome, 
      string userGUID, string machineGUID, int softwareMajorVersionNumber, int softwareMinorVersionNumber,
      string macAddress, string machineName, AppData.ChannelSubscriptions channelSubscriptions, string systemPassPhrase)
    {
      if (systemPassPhrase != _systemPassPhrase)
        return GetUnauthorizedAccessErrorWrapper(systemPassPhrase);

      string channelGUID;
      DAClient client = null;

      using (TransactionScope ts = new TransactionScope())
      {
        try
        {
          client = new DAClient();

          client.RegisterNewUser(emailAddress, password, firstName, lastName, gender, dob,
                                townCity, state, country, occupationSector,
                                employmentLevel, annualHouseholdIncome, userGUID, machineGUID,
                                softwareMajorVersionNumber, softwareMinorVersionNumber, macAddress,
                                machineName, channelSubscriptions, out channelGUID);
        }
        catch (Exception ex)
        {
          _logger.Error(ex.ToString());

          try
          {
            _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
          }
          catch
          {
            // ignore exception
          }

          return GetGenericRetriableErrorWrapper();
        }
        finally
        {
          if (client != null)
            client.Dispose();
        }

        try
        {
          CreateDefaultThumbnailForChannel(channelGUID);
        }
        catch (Exception ex)
        {
          _logger.Error(ex.ToString());

          try
          {
            _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
          }
          catch
          {
            // ignore exception
          }

          return GetGenericRetriableErrorWrapper();
        }

        ts.Complete();
      }

      return GetGenericSuccessWrapper();
    }

    public SimpleErrorWrapper CheckEmailAddressExists(string emailAddress, string systemPassPhrase)
    {
      if (systemPassPhrase != _systemPassPhrase)
        return GetUnauthorizedAccessErrorWrapper(systemPassPhrase);
      
      bool bEmailAddressExists = false;

      DAClient client = null;

      try
      {
        client = new DAClient();

        bEmailAddressExists = client.EmailAddressExists(emailAddress);
      }
      catch (Exception ex)
      {
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        return GetGenericRetriableErrorWrapper();
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      SimpleErrorWrapper wrapper = new SimpleErrorWrapper();
      wrapper.ErrorStatus = ErrorStatus.Success;

      if (bEmailAddressExists)
        wrapper.ErrorCode = "E";
      else
        wrapper.ErrorCode = "N";

      return wrapper;
    }

    public SimpleErrorWrapper EditSubscriptions(string userGUID, string machineGUID, ChannelSubscriptions channelSubscriptions,
      string systemPassPhrase)
    {
      if (systemPassPhrase != _systemPassPhrase)
        return GetUnauthorizedAccessErrorWrapper(systemPassPhrase);

      DAClient client = null;

      try
      {
        client = new DAClient();

        client.EditSubscriptionsByGUID(userGUID, machineGUID, channelSubscriptions);
      }
      catch (Exception ex)
      {
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        return GetGenericRetriableErrorWrapper();
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return GetGenericSuccessWrapper();
    }

    public SimpleErrorWrapper GetPCSubscriptionsByPCID(string userGUID, int pcID, out ChannelSubscriptions channelSubscriptions, string systemPassPhrase)
    {    
      channelSubscriptions = null;

      if (systemPassPhrase != _systemPassPhrase)
        return GetUnauthorizedAccessErrorWrapper(systemPassPhrase);

      DAClient client = null;

      try
      {
        client = new DAClient();

        channelSubscriptions = client.GetPCSubscriptionsByPCID(userGUID, pcID);
      }
      catch (Exception ex)
      {
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        return GetGenericRetriableErrorWrapper();
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return GetGenericSuccessWrapper();
    }

    public SimpleErrorWrapper GetPCSubscriptionsByMachineGUID(string userGUID, string machineGUID, out ChannelSubscriptions channelSubscriptions,
      string systemPassPhrase)
    {
      channelSubscriptions = null;

      if (systemPassPhrase != _systemPassPhrase)
        return GetUnauthorizedAccessErrorWrapper(systemPassPhrase);

      DAClient client = null;

      try
      {
        client = new DAClient();

        channelSubscriptions = client.GetPCSubscriptionsByMachineGUID(userGUID, machineGUID);
      }
      catch (Exception ex)
      {
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        return GetGenericRetriableErrorWrapper();
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return GetGenericSuccessWrapper();
    }

    public SimpleErrorWrapper GetMatchedUserGUID(string userGUID, string systemPassPhrase)
    {
      if (systemPassPhrase != _systemPassPhrase)
        return GetUnauthorizedAccessErrorWrapper(systemPassPhrase);
      
      bool bMatch = false;
      DAClient client = null;

      try
      {
        client = new DAClient();

        bMatch = client.GetMatchedUserGUID(userGUID);
      }
      catch (Exception ex)
      {
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        return GetGenericRetriableErrorWrapper();
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      SimpleErrorWrapper wrapper = new SimpleErrorWrapper();
      wrapper.ErrorStatus = ErrorStatus.Success;

      if (bMatch)
        wrapper.ErrorCode = "M";
      else
        wrapper.ErrorCode = "N";

      return wrapper;
    }

    public SimpleErrorWrapper GetMatchedMachineGUID(string userGUID, string machineGUID, string systemPassPhrase)
    {
      if (systemPassPhrase != _systemPassPhrase)
        return GetUnauthorizedAccessErrorWrapper(systemPassPhrase);

      bool bMatch = false;
      DAClient client = null;

      try
      {
        client = new DAClient();

        bMatch = client.GetMatchedMachineGUID(userGUID, machineGUID);
      }
      catch (Exception ex)
      {
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        return GetGenericRetriableErrorWrapper();
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      SimpleErrorWrapper wrapper = new SimpleErrorWrapper();
      wrapper.ErrorStatus = ErrorStatus.Success;

      if (bMatch)
        wrapper.ErrorCode = "M";
      else
        wrapper.ErrorCode = "N";

      return wrapper;
    }

    public static string GenerateRandomString(int length)
    {
      System.Text.StringBuilder passWord = new System.Text.StringBuilder(length);

      System.Random randomObj = new Random();

      string[] strPasswordChars = { "2", "b", "c", "d", "9", "f", "g", "h", "4", "j", "k", "6", "m", "n", "x", "7", "q", "r", "s", "t", "3", "w", "8", "9", "z" };
      int intPasswordCharCount = strPasswordChars.Length;

      for (int intCounter = 0; intCounter < length; intCounter++)
        passWord.Append(strPasswordChars[randomObj.Next(intPasswordCharCount - 1)]);

      return passWord.ToString();
    }

    public SimpleErrorWrapper SendEmailReminder(string emailAddress, string systemPassPhrase)
    {
      if (systemPassPhrase != _systemPassPhrase)
        return GetUnauthorizedAccessErrorWrapper(systemPassPhrase);

      string newPassword = Path.GetRandomFileName().Replace(".", "");

      DAClient client = null;

      // set new password
      try
      {
        client = new DAClient();

        client.SetPassword(emailAddress, newPassword);
      }
      catch (Exception ex)
      {
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        return GetGenericRetriableErrorWrapper();
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      // send reminder e-mail
      PasswordEmailSender emailSender = new PasswordEmailSender(emailAddress, newPassword);

      try
      {
        emailSender.SendPasswordReminderEmail();
      }
      catch (Exception ex)
      {
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        return GetGenericRetriableErrorWrapper();
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return GetGenericSuccessWrapper();
    }

    public SimpleErrorWrapper RegisterSoftwareUninstall(string userGUID, string machineGUID, string systemPassPhrase)
    {
      if (systemPassPhrase != _systemPassPhrase)
        return GetUnauthorizedAccessErrorWrapper(systemPassPhrase);

      DAClient client = null;

      try
      {
        client = new DAClient();

        client.RemovePCFromUninstall(userGUID, machineGUID);
      }
      catch (Exception ex)
      {
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        return GetGenericRetriableErrorWrapper();
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return GetGenericSuccessWrapper();
    }

    public StringErrorWrapper CreatePCIfNotExists(string userGUID, string macAddress, string machineName,
      int majorVersionNumber, int minorVersionNumber, string systemPassPhrase)
    {
      if (systemPassPhrase != _systemPassPhrase)
      {
        return new StringErrorWrapper()
        {
          ErrorCode = "ERR:004",
          ErrorSeverity = ErrorSeverity.Retriable,
          ErrorStatus = ErrorStatus.Failure,
          Message = "Invalid authentication",
          ReturnString = String.Empty
        };
      }

      string machineGUID = null;

      DAClient client = null;

      try
      {
        client = new DAClient();

        machineGUID = client.CreatePCIfNotExists(userGUID, macAddress, machineName, majorVersionNumber, minorVersionNumber);
      }
      catch (Exception ex)
      {
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        return new StringErrorWrapper()
        {
          ErrorCode = "ERR:001",
          ErrorSeverity = ErrorSeverity.Retriable,
          ErrorStatus = ErrorStatus.Failure,
          Message = "Error getting PC details",
          ReturnString = String.Empty
        };
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return new StringErrorWrapper()
      {
        ErrorStatus = ErrorStatus.Success,
        ReturnString = machineGUID
      };
    }

    public void SendErrorReport(string macAddress, string exceptionDetails)
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("A user has reported the following exception:");
      sb.AppendLine();
      sb.AppendLine();
      sb.Append(exceptionDetails);
      sb.AppendLine();

      try
      {
        File.WriteAllText(System.Configuration.ConfigurationSettings.AppSettings["clientErrorReportsPath"] + System.Guid.NewGuid().ToString() + ".txt", sb.ToString());
      }
      catch (Exception ex)
      {
        _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
      }
    }

    public SimpleErrorWrapper SyncWithServerNoPersonalDetails(string userGUID,
      string machineGUID,
      AppData.ChannelSubscriptions channelSubscriptions,
      int softwareMajorVersionNumber,
      int softwareMinorVersionNumber,
      string machineName,
      string macAddress,
      string systemPassPhrase)
    {
      if (systemPassPhrase != _systemPassPhrase)
      {
        return new SimpleErrorWrapper()
        {
          ErrorCode = "ERR:004",
          ErrorSeverity = ErrorSeverity.Retriable,
          ErrorStatus = ErrorStatus.Failure,
          Message = "Invalid authentication",
        };
      }

      DAClient client = null;

      try
      {
        client = new DAClient();

        client.SyncWithServerNoPersonalDetails(userGUID,
          machineGUID,
          macAddress,
          machineName,
          softwareMajorVersionNumber,
          softwareMinorVersionNumber,
          channelSubscriptions);
      }
      catch (Exception ex)
      {
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        return new SimpleErrorWrapper()
        {
          ErrorCode = "ERR:001",
          ErrorSeverity = ErrorSeverity.Retriable,
          ErrorStatus = ErrorStatus.Failure,
          Message = "Error syncing with server",
        };
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return new SimpleErrorWrapper()
      {
        ErrorStatus = ErrorStatus.Success
      };
    }

    public StringErrorWrapper GetUserGUIDByUsername(string username,  string systemPassPhrase)
    {
        if (systemPassPhrase != _systemPassPhrase)
        {
            return new StringErrorWrapper()
            {
                ErrorCode = "ERR:004",
                ErrorSeverity = ErrorSeverity.Retriable,
                ErrorStatus = ErrorStatus.Failure,
                Message = "Invalid authentication",
                ReturnString = String.Empty
            };
        }

        string userGUID = null;
        DAClient client = null;

        try
        {
            client = new DAClient();

            userGUID = client.GetUserGUIDByUsername(username);
        }
        catch (Exception ex)
        {
          _logger.Error(ex.ToString());
            try
            {
                _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
            }
            catch
            {
                // ignore exception
            }

            return new StringErrorWrapper()
            {
                ErrorCode = "ERR:001",
                ErrorSeverity = ErrorSeverity.Retriable,
                ErrorStatus = ErrorStatus.Failure,
                Message = "Error getting user GUID",
                ReturnString = String.Empty
            };
        }
        finally
        {
            if (client != null)
                client.Dispose();
        }

        return new StringErrorWrapper()
        {
            ErrorStatus = ErrorStatus.Success,
            ReturnString = userGUID
        };
    }

    public SimpleErrorWrapper RemoveStreamsFromSilentMergeByMachineGUID(string machineGUID,
      AppData.ChannelSubscriptions channelSubscriptions,
      string systemPassPhrase)
    {
      if (systemPassPhrase != _systemPassPhrase)
      {
        return new SimpleErrorWrapper()
        {
          ErrorCode = "ERR:004",
          ErrorSeverity = ErrorSeverity.Retriable,
          ErrorStatus = ErrorStatus.Failure,
          Message = "Invalid authentication",
        };
      }

      DAClient client = null;

      try
      {
        client = new DAClient();

        client.RemoveStreamsFromSilentMergeByMachineGUID(machineGUID, channelSubscriptions);
      }
      catch (Exception ex)
      {
        _logger.Error(ex.ToString());
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        return new SimpleErrorWrapper()
        {
          ErrorCode = "ERR:001",
          ErrorSeverity = ErrorSeverity.Retriable,
          ErrorStatus = ErrorStatus.Failure,
          Message = "Error syncing with server",
        };
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return new SimpleErrorWrapper()
      {
        ErrorStatus = ErrorStatus.Success
      };
    }

    public SimpleErrorWrapper ReplaceStreamsFromSilentMergeByMachineGUID(string machineGUID,
      AppData.ChannelSubscriptions channelSubscriptions,
      string systemPassPhrase)
    {
      if (systemPassPhrase != _systemPassPhrase)
      {
        return new SimpleErrorWrapper()
        {
          ErrorCode = "ERR:004",
          ErrorSeverity = ErrorSeverity.Retriable,
          ErrorStatus = ErrorStatus.Failure,
          Message = "Invalid authentication",
        };
      }

      DAClient client = null;

      try
      {
        client = new DAClient();

        client.ReplaceStreamsFromSilentMergeByMachineGUID(machineGUID, channelSubscriptions);
      }
      catch (Exception ex)
      {
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        return new SimpleErrorWrapper()
        {
          ErrorCode = "ERR:001",
          ErrorSeverity = ErrorSeverity.Retriable,
          ErrorStatus = ErrorStatus.Failure,
          Message = "Error syncing with server",
        };
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return new SimpleErrorWrapper()
      {
        ErrorStatus = ErrorStatus.Success
      };
    }

    public SimpleErrorWrapper AddStreamsFromSilentMergeByMachineGUID(string machineGUID,
      AppData.ChannelSubscriptions channelSubscriptions,
      string systemPassPhrase)
    {
      if (systemPassPhrase != _systemPassPhrase)
      {
        return new SimpleErrorWrapper()
        {
          ErrorCode = "ERR:004",
          ErrorSeverity = ErrorSeverity.Retriable,
          ErrorStatus = ErrorStatus.Failure,
          Message = "Invalid authentication",
        };
      }

      DAClient client = null;

      try
      {
        client = new DAClient();

        client.AddStreamsFromSilentMergeByMachineGUID(machineGUID, channelSubscriptions);
      }
      catch (Exception ex)
      {
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        return new SimpleErrorWrapper()
        {
          ErrorCode = "ERR:001",
          ErrorSeverity = ErrorSeverity.Retriable,
          ErrorStatus = ErrorStatus.Failure,
          Message = "Error syncing with server",
        };
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return new SimpleErrorWrapper()
      {
        ErrorStatus = ErrorStatus.Success
      };
    }

    public SimpleErrorWrapper CompareMACAddresses(string macAddressClient, string userGUID, int softwareMajorVersionNumber,
      int softwareMinorVersionNumber, out string newMachineGUID, out bool bMatch, string systemPassPhrase)
    {
      newMachineGUID = null;
      bMatch = false;

      if (systemPassPhrase != _systemPassPhrase)
        return GetUnauthorizedAccessErrorWrapper(systemPassPhrase);

      DAClient client = null;

      try
      {
        client = new DAClient();

        client.CompareMACAddresses(macAddressClient, userGUID, softwareMajorVersionNumber,
        softwareMinorVersionNumber, out newMachineGUID, out bMatch);
      }
      catch (Exception ex)
      {
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        return GetGenericRetriableErrorWrapper();
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return GetGenericSuccessWrapper();
    }

    public StringErrorWrapper AddSubscriptionsAndNewPC(string userGUID,
      string macAddress,
      string machineName,
      int majorVersionNumber,
      int minorVersionNumber,
      string[][] subscriptions,
      string systemPassPhrase)
    {
      if (systemPassPhrase != _systemPassPhrase)
      {
        return new StringErrorWrapper()
        {
          ErrorCode = "ERR:004",
          ErrorSeverity = ErrorSeverity.Retriable,
          ErrorStatus = ErrorStatus.Failure,
          Message = "Invalid authentication",
          ReturnString = String.Empty
        };
      }

      string machineGUID = null;
      DAClient client = null;

      try
      {
        client = new DAClient();

        machineGUID = client.AddSubscriptionsAndNewPC(userGUID,
          macAddress,
          machineName,
          majorVersionNumber,
          minorVersionNumber,
          subscriptions);
      }
      catch (Exception ex)
      {
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        return new StringErrorWrapper()
        {
          ErrorCode = "ERR:001",
          ErrorSeverity = ErrorSeverity.Retriable,
          ErrorStatus = ErrorStatus.Failure,
          Message = "Error getting PC details",
          ReturnString = String.Empty
        };
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return new StringErrorWrapper()
      {
        ErrorStatus = ErrorStatus.Success,
        ReturnString = machineGUID
      };
    }

    public StringErrorWrapper CheckIfPCExistsReturnGUID(string username, string macAddress, string systemPassPhrase)
    {
      if (systemPassPhrase != _systemPassPhrase)
      {
        return new StringErrorWrapper()
        {
          ErrorCode = "ERR:004",
          ErrorSeverity = ErrorSeverity.Retriable,
          ErrorStatus = ErrorStatus.Failure,
          Message = "Invalid authentication",
          ReturnString = String.Empty
        };
      }

      string guids = null;
      DAClient client = null;

      try
      {
        client = new DAClient();

        guids = client.CheckIfPCExistsReturnGUID(username, macAddress);
      }
      catch (Exception ex)
      {
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        return new StringErrorWrapper()
        {
          ErrorCode = "ERR:001",
          ErrorSeverity = ErrorSeverity.Retriable,
          ErrorStatus = ErrorStatus.Failure,
          Message = "Error comparing MAC Addresses",
          ReturnString = String.Empty
        };
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return new StringErrorWrapper()
      {
        ErrorStatus = ErrorStatus.Success,
        ReturnString = guids
      };
    }

    public SimpleErrorWrapper RemoveStreamsFromSilentMerge(string macAddress,
      AppData.ChannelSubscriptions channelSubscriptions,
      string systemPassPhrase)
    {
      if (systemPassPhrase != _systemPassPhrase)
      {
        return new SimpleErrorWrapper()
        {
          ErrorCode = "ERR:004",
          ErrorSeverity = ErrorSeverity.Retriable,
          ErrorStatus = ErrorStatus.Failure,
          Message = "Invalid authentication",
        };
      }

      DAClient client = null;

      try
      {
        client = new DAClient();

        client.RemoveStreamsFromSilentMerge(macAddress, channelSubscriptions);
      }
      catch (Exception ex)
      {
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        return new SimpleErrorWrapper()
        {
          ErrorCode = "ERR:001",
          ErrorSeverity = ErrorSeverity.Retriable,
          ErrorStatus = ErrorStatus.Failure,
          Message = "Error syncing with server",
        };
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return new SimpleErrorWrapper()
      {
        ErrorStatus = ErrorStatus.Success
      };
    }

    public SimpleErrorWrapper ReplaceStreamsFromSilentMerge(string macAddress,
      AppData.ChannelSubscriptions channelSubscriptions,
      string systemPassPhrase)
    {
      if (systemPassPhrase != _systemPassPhrase)
      {
        return new SimpleErrorWrapper()
        {
          ErrorCode = "ERR:004",
          ErrorSeverity = ErrorSeverity.Retriable,
          ErrorStatus = ErrorStatus.Failure,
          Message = "Invalid authentication",
        };
      }

      DAClient client = null;

      try
      {
        client = new DAClient();

        client.ReplaceStreamsFromSilentMerge(macAddress, channelSubscriptions);
      }
      catch (Exception ex)
      {
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        return new SimpleErrorWrapper()
        {
          ErrorCode = "ERR:001",
          ErrorSeverity = ErrorSeverity.Retriable,
          ErrorStatus = ErrorStatus.Failure,
          Message = "Error syncing with server",
        };
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return new SimpleErrorWrapper()
      {
        ErrorStatus = ErrorStatus.Success
      };
    }

    public SimpleErrorWrapper AddStreamsFromSilentMerge(string macAddress,
      AppData.ChannelSubscriptions channelSubscriptions,
      string systemPassPhrase)
    {
      if (systemPassPhrase != _systemPassPhrase)
      {
        return new SimpleErrorWrapper()
        {
          ErrorCode = "ERR:004",
          ErrorSeverity = ErrorSeverity.Retriable,
          ErrorStatus = ErrorStatus.Failure,
          Message = "Invalid authentication",
        };
      }

      DAClient client = null;

      try
      {
        client = new DAClient();

        client.AddStreamsFromSilentMerge(macAddress, channelSubscriptions);
      }
      catch (Exception ex)
      {
        try
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        }
        catch
        {
          // ignore exception
        }

        return new SimpleErrorWrapper()
        {
          ErrorCode = "ERR:001",
          ErrorSeverity = ErrorSeverity.Retriable,
          ErrorStatus = ErrorStatus.Failure,
          Message = "Error syncing with server",
        };
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return new SimpleErrorWrapper()
      {
        ErrorStatus = ErrorStatus.Success
      };
    }
  }
}
