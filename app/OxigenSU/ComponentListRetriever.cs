using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using ServiceErrorReporting;
using System.IO;
using InterCommunicationStructures;
using System.ComponentModel;
using System.Management;
using OxigenIIAdvertising.UserManagementServicesServiceClient;
using System.ServiceModel;

namespace OxigenSU
{
  public class ComponentListRetriever
  {
    EventLog _log;
    string _appDataPath = null;
    string _binariesPath = null;
    GeneralData _generalData = null;
    User _user = null;
    HashSet<ComponentInfo> _changedComponents = null;

    public ComponentListRetriever()
    {
      _log = new EventLog();
      _log.Source = "OxigenSU";
      _log.Log = String.Empty;

      _appDataPath = System.Configuration.ConfigurationSettings.AppSettings["AppDataPath"];
      _binariesPath = System.Configuration.ConfigurationSettings.AppSettings["BinariesPath"];
    }

    public HashSet<ComponentInfo> ChangedComponents
    {
      get { return _changedComponents; }
      set { _changedComponents = value; }
    }

    public void Retrieve()
    {
      if (string.IsNullOrEmpty(_binariesPath))
      {
        _log.WriteEntry("Could not find BinariesPath value in config file.", EventLogEntryType.Error);
        return;
      }

      if (string.IsNullOrEmpty(_appDataPath))
      {
        _log.WriteEntry("Could not find AppDataPath value in config file.", EventLogEntryType.Error);
        return;
      }

      if (!GetSettingsFiles(ref _generalData, ref _user))
        return;

      // in closed networks, software updating may not be allowed.
      if (!_user.CanUpdate)
        return;

      if (!PackageOutdated())
        return;

      int maxNoUFMServers = -1;
      int maxNoUMSServers = -1;
      int timeout = -1;

      if (!GetNumericals(ref maxNoUFMServers, ref maxNoUMSServers, ref timeout))
        return;

      ComponentInfo[] downloadedComponentList = null;

      // Check if MAC Address has a match on server
      if (!GetMachineGUIDIfNotExists(maxNoUMSServers, timeout))
        return;

      if (!DownloadComponentList(maxNoUFMServers, timeout, ref downloadedComponentList))
        return;

      // compare local file versions with downloaded list
      _changedComponents = GetChangedComponents(downloadedComponentList);
    }

    private bool GetMachineGUIDIfNotExists(int maxNoUMSServers, int timeout)
    {
      if (!string.IsNullOrEmpty(_user.MachineGUID))
        return true;

      string macAddress = GetMACAddress();

      UserManagementServicesNonStreamerClient client = null;
      StringErrorWrapper wrapper = null;

      try
      {
        string uri = ResponsiveServerDeterminator.GetResponsiveURI(ServerType.MasterGetConfig, maxNoUMSServers, timeout,
        _user.GetUserGUIDSuffix(), _generalData.Properties["primaryDomainName"],
        _generalData.Properties["secondaryDomainName"], "UserManagementServices.svc");

        if (string.IsNullOrEmpty(uri))
          return false;

        client = new UserManagementServicesNonStreamerClient();
        client.Endpoint.Address = new EndpointAddress(uri);

        wrapper = client.CreatePCIfNotExists(_user.UserGUID, macAddress, Environment.MachineName, _user.SoftwareMajorVersionNumber,
          _user.SoftwareMinorVersionNumber, "password");

        if (wrapper.ErrorStatus == ErrorStatus.Failure)
          return false;

        if (wrapper.ErrorStatus == ErrorStatus.Success)
        {
          _user.MachineGUID = wrapper.ReturnString;
          Serializer.Serialize(_user, _appDataPath + "\\SettingsData\\UserSettings.dat", "password");
        }
      }
      catch
      {
        return false;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return true;
    }

    private string GetMACAddress()
    {
      ManagementObjectSearcher query = null;
      ManagementObjectCollection queryCollection = null;

      try
      {
        query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled='TRUE'");

        queryCollection = query.Get();

        foreach (ManagementObject mo in queryCollection)
        {
          if (mo["MacAddress"] != null)
            return (mo["MacAddress"]).ToString();
        }
      }
      catch (Exception ex)
      {
        return ex.ToString();
      }

      return String.Empty;
    }

    private HashSet<ComponentInfo> GetChangedComponents(ComponentInfo[] downloadedComponentList)
    {
      ComponentInfo[] localComponentListBinaryFolder = GetComponents(_binariesPath);
      ComponentInfo[] localComponentListSystemFolder = GetSystemComponents(downloadedComponentList);

      HashSet<ComponentInfo> changedComponents = new HashSet<ComponentInfo>();

      foreach (ComponentInfo ci in downloadedComponentList)
      {
        if (ci.Location == ComponentLocation.BinaryFolder && MustDownloadOrUpdate(ci, localComponentListBinaryFolder))
        {
          ComponentInfo c = new ComponentInfo()
          {
            File = ci.File,
            Location = ComponentLocation.BinaryFolder
          };

          changedComponents.Add(c);
        }

        if (ci.Location == ComponentLocation.SystemFolder && MustDownloadOrUpdate(ci, localComponentListSystemFolder))
        {
          ComponentInfo c = new ComponentInfo()
          {
            File = ci.File,
            Location = ComponentLocation.SystemFolder
          };

          changedComponents.Add(c);
        }
      }

      return changedComponents;
    }

    private bool DownloadComponentList(int maxNoServers, int timeout, ref ComponentInfo[] componentList)
    {
      string uri = ResponsiveServerDeterminator.GetResponsiveURI(ServerType.RelayLogs, maxNoServers, timeout,
        _user.GetMachineGUIDSuffix(), _generalData.Properties["primaryDomainName"],
        _generalData.Properties["secondaryDomainName"], "UserDataMarshaller.svc");
    
      if (uri == "")
        return false;

      uri += "/su";

      UserDataMarshallerSUStreamerClient client = null;
      StreamErrorWrapper wrapper = null;
      VersionParameterMessage message = new VersionParameterMessage()
      {
        Version = String.Format("{0}.{1}", _generalData.SoftwareMajorVersionNumber, _generalData.SoftwareMinorVersionNumber),
        SystemPassPhrase = "password"
      };

      try
      {
        client = new UserDataMarshallerSUStreamerClient();
        client.Endpoint.Address = new System.ServiceModel.EndpointAddress(uri);
        wrapper = client.GetComponentList(message);
      }
      catch (Exception ex)
      {
        client.Dispose();
        _log.WriteEntry("Could not get a list of updated components.", EventLogEntryType.Error);
        return false;
      }

      if (wrapper.ErrorStatus == ErrorStatus.NoData)
      {
        _log.WriteEntry(wrapper.ErrorCode + " " + wrapper.Message, EventLogEntryType.Warning);
        return false;
      }

      if (wrapper.ErrorStatus == ErrorStatus.Failure)
      {
        _log.WriteEntry(wrapper.ErrorCode + " " + wrapper.Message, EventLogEntryType.Error);
        return false;
      }

      byte[] buffer = StreamToByteArray(wrapper.ReturnStream);

      if (wrapper.ReturnStream != null)
        wrapper.ReturnStream.Dispose();

      string stringXmlObj = ByteArrayToString(buffer);

      componentList = (ComponentInfo[])Serializer.DeserializeFromString(typeof(ComponentInfo[]), stringXmlObj);

      return true;
    }

    public static ComponentInfo[] GetComponents(string path)
    {
      string[] files = Directory.GetFiles(path);

      int length = files.Length;

      ComponentInfo[] components = new ComponentInfo[length];

      FileVersionInfo fileVersionInfo;

      for (int counter = 0; counter < length; counter++)
      {
        ComponentInfo ci = new ComponentInfo();

        fileVersionInfo = FileVersionInfo.GetVersionInfo(files[counter]);

        ci.File = Path.GetFileName(files[counter]);
        ci.MajorVersionNumber = fileVersionInfo.FileMajorPart;
        ci.MinorVersionNumber = fileVersionInfo.FileMinorPart;
        ci.Location = ComponentLocation.BinaryFolder;

        components[counter] = ci;
      }

      return components;
    }

    private ComponentInfo[] GetSystemComponents(ComponentInfo[] downloadedComponentList)
    {
      string systemFolder = Environment.GetFolderPath(Environment.SpecialFolder.System);

      List<ComponentInfo> systemComponents = new List<ComponentInfo>();

      foreach (ComponentInfo downloadedCI in downloadedComponentList)
      {
        if (downloadedCI.Location == ComponentLocation.SystemFolder)
        {
          string path = systemFolder + "\\" + downloadedCI.File;

          if (File.Exists(path))
          {
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(systemFolder + "\\" + downloadedCI.File);

            ComponentInfo ci = new ComponentInfo()
            {
              File = downloadedCI.File,
              MajorVersionNumber = fileVersionInfo.FileMajorPart,
              MinorVersionNumber = fileVersionInfo.FileMinorPart,
              Location = ComponentLocation.SystemFolder
            };

            systemComponents.Add(ci);
          }
        }
      }

      return systemComponents.ToArray();
    }

    private bool SaveStreamAndDispose(Stream stream, string filePath)
    {
      FileStream fileStream = null;
      byte[] downloadedData = null;

      try
      {
        downloadedData = StreamToByteArray(stream);
      }
      catch
      {
        _log.WriteEntry(String.Format("Could not process file: {0}", filePath) + EventLogEntryType.Error);
        return false;
      }
      finally
      {
        if (stream != null)
          stream.Dispose();
      }

      try
      {
        File.WriteAllBytes(filePath, downloadedData);
      }
      catch 
      {
        _log.WriteEntry(String.Format("Could not save file: {0}", filePath), EventLogEntryType.Error);
        return false;
      }
      finally
      {
        if (fileStream != null)
          fileStream.Dispose();
      }

      return true;
    }

    private byte[] StreamToByteArray(Stream stream)
    {
      MemoryStream ms = new MemoryStream();

      byte[] buffer = new byte[1000];

      int bytesRead = 0;

      do
      {
        bytesRead = stream.Read(buffer, 0, buffer.Length);

        ms.Write(buffer, 0, bytesRead);
      }
      while (bytesRead > 0);

      byte[] downloadedDataBuffer = ms.ToArray();

      ms.Close();
      ms.Dispose();

      return downloadedDataBuffer;
    }

    private bool GetNumericals(ref int maxNoUFMServers, ref int maxNoUMSServers, ref int timeout)
    {
      if (!int.TryParse(_generalData.NoServers["relayLog"], out maxNoUFMServers))
      {
        _log.WriteEntry("Cannot get maximum number of component list download servers.", EventLogEntryType.Error);
        return false;
      }

      if (!int.TryParse(_generalData.Properties["serverTimeout"], out timeout))
      {
        _log.WriteEntry("Cannot get timeout download servers.", EventLogEntryType.Error);
        return false;
      }

      if (!int.TryParse(_generalData.NoServers["masterConfig"], out maxNoUMSServers))
      {
        _log.WriteEntry("Cannot get maximum number of component list User Managemet Services servers.", EventLogEntryType.Error);
        return false;
      }

      return true;
    }

    private bool PackageOutdated()
    {
      return _user.SoftwareMajorVersionNumber < _generalData.SoftwareMajorVersionNumber ||
        _user.SoftwareMinorVersionNumber < _generalData.SoftwareMinorVersionNumber;
    }

    private bool MustDownloadOrUpdate(ComponentInfo ciDownloaded, ComponentInfo[] localList)
    {
      foreach (ComponentInfo ciLocal in localList)
      {
        // if there is a local component with the same filename as the downloaded component, check versions
        if (ciDownloaded.File == ciLocal.File)
        {
          return ciLocal.MajorVersionNumber < ciDownloaded.MajorVersionNumber ||
            ciLocal.MinorVersionNumber < ciDownloaded.MinorVersionNumber;
        }
      }

      // if no local component filename that matches the downloaded component's filename,
      // it must be a new component, so mark for download
      return true;
    }

    private bool GetSettingsFiles(ref GeneralData generalData, ref User user)
    {
      try
      {
        generalData = (GeneralData)Serializer.DeserializeNoLock(typeof(GeneralData), _appDataPath + "\\SettingsData\\ss_general_data.dat", "password");
      }
      catch
      {
        _log.WriteEntry("Cannot retrieve general settings. Updating will be postponed.", EventLogEntryType.Error);
        return false;
      }

      try
      {
        user = (User)Serializer.DeserializeNoLock(typeof(User), _appDataPath + "\\SettingsData\\UserSettings.dat", "password");
      }
      catch
      {
        _log.WriteEntry("Cannot retrieve user settings. Updating will be postponed.", EventLogEntryType.Error);
        return false;
      }

      return true;
    }

    private string ByteArrayToString(byte[] arrBytes)
    {
      ASCIIEncoding enc = new ASCIIEncoding();
      return enc.GetString(arrBytes);
    }
  }
}
