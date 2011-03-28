using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.AppData;
using OxigenIIAdvertising.DAClients;
using System.Diagnostics;
using OxigenIIMasterDataMarshallerClient;
using System.IO;
using InterCommunicationStructures;
using OxigenIIAdvertising.ServerConnectAttempt;
using System.ServiceModel;
using OxigenIIAdvertising.XMLSerializer;
using OxigenIIAdvertising.FileChecksumCalculator;
using OxigenIIAdvertising.SOAStructures;
using OxigenIIAdvertising.EncryptionDecryption;

namespace ChannelDatabaseToRelayUploaderLib
{
  public class ChannelDatabaseToRelayUploader
  {
    private int _serverTimeout = -1;
    private string _systemPassPhrase = null;
    private string _primaryDomainName = null;
    private string _secondaryDomainName = null;
    private int _maxNoServers = -1;
    private string _cryptPassword = null;
    private string _slidePath = null;

    private EventLog _eventLog;

    public ChannelDatabaseToRelayUploader(int serverTimeout, string systemPassPhrase, string primaryDomainName,
        string secondaryDomainName, int maxNoServers, string cryptPassword, string slidePath, EventLog eventLog)
    {
      _serverTimeout = serverTimeout;
      _systemPassPhrase = systemPassPhrase;
      _primaryDomainName = primaryDomainName;
      _secondaryDomainName = secondaryDomainName;
      _maxNoServers = maxNoServers;
      _cryptPassword = cryptPassword;
      _slidePath = slidePath;
      _eventLog = eventLog;
    }

    public void Execute()
    {
      ProcessChannelAssetData();
      ProcessAssets();
    }

    private void ProcessChannelAssetData()
    {
      HashSet<int> channelIDs = null;
      List<OxigenIIAdvertising.AppData.Channel> channels = GetDirtyChannelData();

      if (channels.Count > 0)
      {
        channelIDs = UploadChannelData(channels);

        MakeChannelsClean(channelIDs);
      }
      else
        Console.WriteLine("No dirty channels");
    }
    
    private void ProcessAssets()
    {
      HashSet<int> assetIDs = null;

      List<SimpleFileInfo> assets = GetDirtyAssetFilenames();

      if (assets.Count > 0)
      {
        assetIDs = UploadAssets(assets);

        MakeAssetsClean(assetIDs);
      }
      else
        Console.WriteLine("No dirty assets");
    }

    private List<OxigenIIAdvertising.AppData.Channel> GetDirtyChannelData()
    {
      List<OxigenIIAdvertising.AppData.Channel> channels = null;
      DAClient client = null;

      // get "dirty channels"
      try
      {
        client = new DAClient();

        channels = client.GetChannelsDirty();
      }
      finally
      {
        client.Dispose();
      }

      return channels;
    }

    private List<SimpleFileInfo> GetDirtyAssetFilenames()
    {
      List<SimpleFileInfo> assets = null;
      DAClient client = null;

      // get new assets
      try
      {
        client = new DAClient();

        assets = client.GetAssetsDirty();
      }
      finally
      {
        client.Dispose();
      }

      return assets;
    }

    private HashSet<int> UploadChannelData(List<OxigenIIAdvertising.AppData.Channel> channels)
    {
      MasterDataMarshallerStreamerClient streamerClient = null;
      MemoryStream ms = null;
      AppDataFileStreamParameterMessage message = null;
      HashSet<int> channelIDs = new HashSet<int>();

      try
      {
        streamerClient = new MasterDataMarshallerStreamerClient();

        foreach (OxigenIIAdvertising.AppData.Channel channel in channels)
        {
          try
          {
            ms = Serializer.SerializeWithEncryption(channel, _cryptPassword);

            message = new AppDataFileStreamParameterMessage();
            message.Stream = ms;
            message.SystemPassPhrase = _systemPassPhrase;
            message.Checksum = ChecksumCalculator.GetChecksum(ms);
            message.ChannelID = channel.ChannelID;
            message.DataFileType = DataFileType.ChannelData;

            streamerClient.SetAppDataFiles(message);

            channelIDs.Add(channel.ChannelID);
          }
          catch (Exception ex)
          {
            LogException(_eventLog, ex.ToString());
          }
          finally
          {
            if (ms != null)
              ms.Dispose();
          }
        }
      }
      catch (Exception ex)
      {
        LogException(_eventLog, ex.ToString());
      }
      finally
      {
        if (streamerClient != null)
          streamerClient.Dispose();
      }

      return channelIDs;
    }

    // doesn't necessarily have to be a GUID, but can be a string of a filename format.
    // will get one character after the last underscore of the string parameter
    private string GetGUIDSuffix(string GUID)
    {
      return GUID.Substring(GUID.LastIndexOf("_") + 1, 1);
    }

    private string GetResponsiveServer(string letter, string endpointSuffix)
    {
      try
      {
        return ResponsiveServerDeterminator.GetResponsiveURI(ServerType.RelayChannelAssets, _maxNoServers, _serverTimeout,
          letter, _primaryDomainName, _secondaryDomainName, endpointSuffix);
      }
      catch (Exception ex)
      {
        LogException(_eventLog, ex.ToString());

        return "";
      }
    }

    private HashSet<int> UploadAssets(List<SimpleFileInfo> assets)
    {
      string assetContentPath = System.Configuration.ConfigurationSettings.AppSettings["assetContentPath"];

      MasterDataMarshallerStreamerClient streamerClient = null;
      MemoryStream ms = null;
      AssetFileStreamParameterMessage message = null;
      HashSet<int> assetIDs = new HashSet<int>();

      try
      {
        streamerClient = new MasterDataMarshallerStreamerClient();

        foreach (SimpleFileInfo asset in assets)
        {
          try
          {
            ms = EncryptAssetFile(_slidePath + asset.FileNameWithPath, _cryptPassword);

            message = new AssetFileStreamParameterMessage();
            message.Stream = ms;
            message.SystemPassPhrase = _systemPassPhrase;
            message.Checksum = ChecksumCalculator.GetChecksum(ms);
            message.Filename = asset.FilenameNoPath;

            streamerClient.SetAssetFile(message);

            assetIDs.Add(asset.FileID);
          }
          catch (Exception ex)
          {
            LogException(_eventLog, ex.ToString());
          }
          finally
          {
            if (ms != null)
              ms.Dispose();
          }
        }        
      }
      catch (Exception ex)
      {
        LogException(_eventLog, ex.ToString());
      }
      finally
      {
        if (streamerClient != null)
          streamerClient.Dispose();
      }

      return assetIDs;
    }

    private MemoryStream EncryptAssetFile(string assetPath, string encryptionPassword)
    {
      byte[] decryptedBuffer = File.ReadAllBytes(assetPath);

      byte[] encryptedBuffer = Cryptography.Encrypt(decryptedBuffer, encryptionPassword);

      return new MemoryStream(encryptedBuffer);
    }

    private void MakeAssetsClean(HashSet<int> assetIDs)
    {
      DAClient client = null;

      try
      {
        client = new DAClient();

        client.EditSlidesMakeClean(assetIDs);
      }
      catch (Exception ex)
      {
        LogException(_eventLog, ex.ToString());
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    private void MakeChannelsClean(HashSet<int> channelIDs)
    {
      DAClient client = null;

      try
      {
        client = new DAClient();

        client.EditChannelsMakeClean(channelIDs);
      }
      catch (Exception ex)
      {
        LogException(_eventLog, ex.ToString());
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    private void LogException(EventLog eventLog, string exception)
    {
      if (eventLog == null)
        Console.WriteLine(exception);
      else
        eventLog.WriteEntry(exception, EventLogEntryType.Error);
    }
  }
}