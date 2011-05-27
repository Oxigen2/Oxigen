using System;
using System.Collections.Generic;
using System.Text;
using OxigenIIAdvertising.LoggerInfo;

namespace Setup
{
  public sealed class AppDataSingleton
  {
    private static volatile AppDataSingleton _instance;
    private static object _lockObject = new Object();

    private bool _bSilentMode = false;
    private bool _bOneFormClosed = false;
    private bool _bExistingUserDetailsRetrieved = false;
    private bool _bExistingUser = false;
    private bool _bUserInfoExists = false;
    private string _emailAddress;
    private string _password;
    private string _firstName;
    private string _lastName;
    private string _gender;
    private int _dobDay;
    private int _dobMonth; 
    private int _dobYear;
    private DateTime _dob;
    private string _country;
    private string _state;
    private string _townCity;
    private string _occupationSector;
    private string _employmentLevel;
    private string _annualHouseholdIncome;
    private string _binariesPath;
    private string _dataPath;
    private int _defaultDisplayDuration = -1;
    private bool _bSoundOn = true;
    private User _user;
    private GeneralData _generalData;
    private bool _bRepair = false;
    private string _newPCName;
    private bool _bExitPromptSuppressed = false;
    private int _pcID = -1;
    private bool _bNewPC = false;
    private bool _mergeStreamsInstallation = false;
    private Setup.UserManagementServicesLive.ChannelSubscriptions _fileDetectedChannelSubscriptionsNet;
    private Setup.DuplicateLibrary.ChannelSubscriptions _downloadedChannelSubscriptionsLocal;
    private Setup.DuplicateLibrary.ChannelSubscriptions _fileDetectedChannelSubscriptionsLocal;
    private Setup.UserManagementServicesLive.ChannelSubscriptions _channelSubscriptionsToUpload;
    private bool _bOldOxigenSystemModified = false;
    private bool _bIs64BitSystem = false;
    private string _username;
    private bool _bDebugMode = false;
    private Logger _setupLogger = new Logger("Oxigen Installer", Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\OxigenInstallLog.txt", LoggingMode.Release);

    public string Username
    {
      get { return _username; }
      set { _username = value; }
    }

    public bool Is64BitSystem
    {
      get { return _bIs64BitSystem; }
      set { _bIs64BitSystem = value; }
    }

    public bool OldOxigenSystemModified
    {
      get
      {
        lock (_lockObject)
          return _bOldOxigenSystemModified;
      }
      set
      {
        lock (_lockObject)
          _bOldOxigenSystemModified = value;
      }
    }

    public Setup.UserManagementServicesLive.ChannelSubscriptions ChannelSubscriptionsToUpload
    {
      get
      {
        lock (_lockObject)
          return _channelSubscriptionsToUpload;
      }
      set
      {
        lock (_lockObject)
          _channelSubscriptionsToUpload = value;
      }
    }

    public int PcID
    {
      get
      {
        lock (_lockObject)
          return _pcID;
      }
      set
      {
        lock (_lockObject)
          _pcID = value;
      }
    }

    public bool NewPC
    {
      get
      {
        lock (_lockObject)
          return _bNewPC;
      }
      set
      {
        lock (_lockObject)
          _bNewPC = value;
      }
    }
    
    public Setup.DuplicateLibrary.ChannelSubscriptions DownloadedChannelSubscriptionsLocal
    {
      get
      {
        lock (_lockObject)
          return _downloadedChannelSubscriptionsLocal;
      }
      set
      {
        lock (_lockObject)
          _downloadedChannelSubscriptionsLocal = value;
      }
    }

    public bool FileDetectedSubscriptionsFound
    {
      get
      {
        lock (_lockObject)
          return _fileDetectedChannelSubscriptionsLocal.SubscriptionSet != null;
      }
    }

    public bool DownloadedChannelSubscriptionsExist
    {
      get
      {
        lock (_lockObject)
          return _downloadedChannelSubscriptionsLocal.SubscriptionSet != null;
      }
    }

    public Setup.UserManagementServicesLive.ChannelSubscriptions FileDetectedChannelSubscriptionsNet
    {
      get
      {
        lock (_lockObject)
          return _fileDetectedChannelSubscriptionsNet;
      }
      set
      {
        lock (_lockObject)
          _fileDetectedChannelSubscriptionsNet = value;
      }
    }

    public Setup.DuplicateLibrary.ChannelSubscriptions FileDetectedChannelSubscriptionsLocal
    {
      get
      {
        lock (_lockObject)
          return _fileDetectedChannelSubscriptionsLocal;
      }
      set
      {
        lock (_lockObject)
          _fileDetectedChannelSubscriptionsLocal = value;
      }
    }

    public bool ExitPromptSuppressed
    {
      get
      {
        lock (_lockObject)
          return _bExitPromptSuppressed;
      }
      set
      {
        lock (_lockObject)
          _bExitPromptSuppressed = value;
      }
    }

    public string NewPCName
    {
      get
      {
        lock (_lockObject)
          return _newPCName;
      }
      set
      {
        lock (_lockObject)
          _newPCName = value;
      }
    }

    public bool MergeStreamsInstallation
    {
      get
      {
        lock (_lockObject)
          return _mergeStreamsInstallation;
      }
      set
      {
        lock (_lockObject)
          _mergeStreamsInstallation = value;
      }
    }

    public string EmailAddress
    {
      get 
      {
        lock (_lockObject)
          return _emailAddress; 
      }
      set
      {
        lock (_lockObject)
          _emailAddress = value; 
      }
    }

    public string Password
    {
      get 
      {
        lock (_lockObject)
          return _password; 
      }
      set 
      {
        lock (_lockObject)
          _password = value; 
      }
    }

    public bool OneFormClosed
    {
      get
      {
        lock (_lockObject)
          return _bOneFormClosed;
      }

      set
      {
        lock (_lockObject)
          _bOneFormClosed = value;
      }
    }

    public string FirstName
    {
      get { return _firstName; }
      set { _firstName = value; }
    }

    public string LastName
    {
      get
      {
        lock (_lockObject)
          return _lastName;
      }
      set
      {
        lock (_lockObject)
          _lastName = value;
      }
    }

    public bool ExistingUserDetailsDataRetrieved
    {
      get
      {
        lock (_lockObject)
          return _bExistingUserDetailsRetrieved;
      }
      set
      {
        lock (_lockObject)
          _bExistingUserDetailsRetrieved = value;
      }
    }

    public bool ExistingUser
    {
      get
      {
        lock (_lockObject)
          return _bExistingUser;
      }

      set
      {
        lock (_lockObject)
          _bExistingUser = value;
      }
    }

    public bool UserInfoExists
    {
      get
      {
        lock (_lockObject)
          return _bUserInfoExists;
      }

      set
      {
        lock (_lockObject)
          _bUserInfoExists = value;
      }
    }


    public string Gender
    {
      get
      {
        lock (_lockObject)
          return _gender;
      }
      set
      {
        lock (_lockObject)
          _gender = value;
      }
    }

    public int DOBDay
    {
      get
      {
        lock (_lockObject)
          return _dobDay;
      }
      set
      {
        lock (_lockObject)
          _dobDay = value;
      }
    }

    public int DOBMonth
    {
      get
      {
        lock (_lockObject)
          return _dobMonth;
      }
      set
      {
        lock (_lockObject)
          _dobMonth = value;
      }
    }

    public int DOBYear
    {
      get
      {
        lock (_lockObject)
          return _dobYear;
      }
      set
      {
        lock (_lockObject)
          _dobYear = value;
      }
    }

    public DateTime DOB
    {
      get
      {
        lock (_lockObject)
          return _dob;
      }
      set
      {
        lock (_lockObject)
          _dob = value;
      }
    }

    public string Country
    {
      get
      {
        lock (_lockObject)
          return _country;
      }
      set
      {
        lock (_lockObject)
          _country = value;
      }
    }

    public string State
    {
      get
      {
        lock (_lockObject)
          return _state;
      }
      set
      {
        lock (_lockObject)
          _state = value;
      }
    }

    public string TownCity
    {
      get
      {
        lock (_lockObject)
          return _townCity;
      }
      set
      {
        lock (_lockObject)
          _townCity = value;
      }
    }

    public string OccupationSector
    {
      get
      {
        lock (_lockObject)
          return _occupationSector;
      }
      set
      {
        lock (_lockObject)
          _occupationSector = value;
      }
    }

    public string EmploymentLevel
    {
      get
      {
        lock (_lockObject)
          return _employmentLevel;
      }
      set
      {
        lock (_lockObject)
          _employmentLevel = value;
      }
    }

    public string AnnualHouseholdIncome
    {
      get
      {
        lock (_lockObject)
          return _annualHouseholdIncome;
      }
      set
      {
        lock (_lockObject)
          _annualHouseholdIncome = value;
      }
    }

    public string BinariesPath
    {
      get
      {
        lock (_lockObject)
          return _binariesPath;
      }
      set
      {
        lock (_lockObject)
          _binariesPath = value;
      }
    }

    public string DataPath
    {
      get
      {
        lock (_lockObject)
          return _dataPath;
      }
      set
      {
        lock (_lockObject)
          _dataPath = value;
      }
    }

    public int DefaultDisplayDuration
    {
      get
      {
        lock (_lockObject)
          return _defaultDisplayDuration;
      }
      set
      {
        lock (_lockObject)
          _defaultDisplayDuration = value;
      }
    }

    public bool SoundOn
    {
      get
      {
        lock (_lockObject)
          return _bSoundOn;
      }
      set
      {
        lock (_lockObject) _bSoundOn = value;
      }
    }

    public User User
    {
      get
      {
        lock (_lockObject)
          return _user;
      }
      set
      {
        lock (_lockObject)
          _user = value;
      }
    }

    public GeneralData GeneralData
    {
      get
      {
        lock (_lockObject)
          return _generalData;
      }
      set
      {
        lock (_lockObject)
          _generalData = value;
      }
    }

    public bool Repair
    {
      get
      {
        lock (_lockObject)
          return _bRepair;
      }
      set
      {
        lock (_lockObject)
          _bRepair = value;
      }
    }

    public Logger SetupLogger
    {
        get { return _setupLogger; }
        set { _setupLogger = value; }
    }

    public bool SilentMode
    {
      get { return _bSilentMode; }
      set { _bSilentMode = value; }
    }

    public bool DebugMode
    {
      get { return _bDebugMode; }
      set { _bDebugMode = value; }
    }

    private AppDataSingleton()
    {
      _user = new User();

      _user.SoftwareMajorVersionNumber = 1;
      _user.SoftwareMinorVersionNumber = 8;

      _generalData = new GeneralData();

      _generalData.SoftwareMajorVersionNumber = 1;
      _generalData.SoftwareMinorVersionNumber = 8;

      _fileDetectedChannelSubscriptionsNet = new Setup.UserManagementServicesLive.ChannelSubscriptions();
      _fileDetectedChannelSubscriptionsLocal = new Setup.DuplicateLibrary.ChannelSubscriptions();
      _downloadedChannelSubscriptionsLocal = new Setup.DuplicateLibrary.ChannelSubscriptions();
      _channelSubscriptionsToUpload = new Setup.UserManagementServicesLive.ChannelSubscriptions();

      _generalData.Properties.Add("logExchangerProcessingInterval", "1800");
      _generalData.Properties.Add("contentExchangerProcessingInterval", "1800");
      _generalData.Properties.Add("softwareUpdaterProcessingInterval", "604800000");
      _generalData.Properties.Add("serverTimeout", "5000"); // milliseconds
      _generalData.Properties.Add("primaryDomainName", ".oxigen.net");
      _generalData.Properties.Add("secondaryDomainName", ".oxigen.net");
      _generalData.Properties.Add("advertDisplayThreshold", "0.1");
      _generalData.Properties.Add("logTimerInterval", "20");
      _generalData.Properties.Add("protectedContentTime", "25");
      _generalData.Properties.Add("maxLines", "10");
      _generalData.Properties.Add("noAssetDisplayLength", "15");
      _generalData.Properties.Add("requestTimeout", "4");
      _generalData.Properties.Add("dateTimeDiffTolerance", "5"); // minutes
      _generalData.Properties.Add("daysToKeepAssetFiles", "8");

      _generalData.NoServers.Add("relayLog", "1");
      _generalData.NoServers.Add("relayConfig", "1");
      _generalData.NoServers.Add("relayChannelAssets", "1");
      _generalData.NoServers.Add("relayChannelData", "1");
      _generalData.NoServers.Add("masterConfig", "4");
      _generalData.NoServers.Add("download", "1");

      _bIs64BitSystem = Wow.Is64BitOperatingSystem;
    }

    public static AppDataSingleton Instance
    {
      get
      {
        // first check if instance is null: if it isn't null, do not execute the lock block
        if (_instance == null)
        {
          lock (_lockObject)
          {
            // double check if instance exists as it may have been created between the first if and the lock
            if (_instance == null)
              _instance = new AppDataSingleton();
          }
        }

        return _instance;
      }
    }
  } 
}
