using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterCommunicationStructures;
using OxigenIIAdvertising.ServiceContracts.DAServices;
using System.ServiceModel;
using OxigenIIAdvertising.SOAStructures;
using ProxyClientBaseLib;
using OxigenIIAdvertising.Demographic;

namespace OxigenIIAdvertising.DAClients
{
  public class DAClient : ProxyClientBase<IDAService>, IDAService
  {
    public PageChannelData GetChannelListByCategoryID(int userID, int categoryID, int startChannelNo, int endChannelNo, SortChannelsBy sortBy)
    {
      return Channel.GetChannelListByCategoryID(userID, categoryID, startChannelNo, endChannelNo, sortBy);
    }

    public List<Category> GetCategoryList(int categoryID)
    {
      return Channel.GetCategoryList(categoryID);
    }

    public Channel GetChannelDetailsSimple(int channelID)
    {
      return Channel.GetChannelDetailsSimple(channelID);
    }

    public List<ChannelListChannel> Search(int userID, string text)
    {
      return Channel.Search(userID, text);
    }

    public List<PC> GetPcListRegistered(int userID)
    {
      return Channel.GetPcListRegistered(userID);
    }

    public List<PC> GetPcListUnregistered(string pcProfileToken)
    {
      return Channel.GetPcListUnregistered(pcProfileToken);
    }

    public PageChannelData GetPcStreamsRegistered(int userID, int pcID)
    {
      return Channel.GetPcStreamsRegistered(userID, pcID);
    }

    public PageChannelData GetPcStreamsUnregistered(int pcID)
    {
      return Channel.GetPcStreamsUnregistered(pcID);
    }

    public User Login(string username, string password)
    {
      return Channel.Login(username, password);
    }

    public User Signup(string emailAddress, string password, string firstName, string lastName, out string channelGUID)
    {
      return Channel.Signup(emailAddress, password, firstName, lastName, out channelGUID);
    }

    public PageAssetContentData GetRawContent(int userID, int folderID)
    {
      return Channel.GetRawContent(userID, folderID);
    }

    public AssetContentProperties GetRawContentProperties(int userID, int contentID)
    {
      return Channel.GetRawContentProperties(userID, contentID);
    }

    public PageSlideData GetStreamSlides(int userID, int folderID)
    {
      return Channel.GetStreamSlides(userID, folderID);
    }

    public PageSlideData GetUserStreams(int userID, int folderID)
    {
      return Channel.GetUserStreams(userID, folderID);
    }

    public List<List<CreateContentGenericFolder>> GetFolders(int userID)
    {
      return Channel.GetFolders(userID);
    }

    public void PutRawContentProperties(int userID, AssetContentProperties contentProperties)
    {
      Channel.PutRawContentProperties(userID, contentProperties);
    }

    public ChannelProperties GetChannelProperties(int userID, int streamID)
    {
      return Channel.GetChannelProperties(userID, streamID);
    }

    public void AddChannelContent(int userID, int channelID, List<int> slideIDList, string schedule, 
      string presentationSchedule, string url, float displayDuration, string newThumbnailGUID, out string slideThumbnailWithPartialPath, out string channelGUID)
    {
      Channel.AddChannelContent(userID, channelID, slideIDList, schedule, presentationSchedule, url, displayDuration, newThumbnailGUID, out slideThumbnailWithPartialPath, out channelGUID);
    }

    public void AddAssetContent(int userID, int folderID, List<AssetContent> assetContents)
    {
      Channel.AddAssetContent(userID, folderID, assetContents);
    }

    public List<SlideContent> AddSlideContent(int userID, int folderID, List<int> contentIDList)
    {
      return Channel.AddSlideContent(userID, folderID, contentIDList);
    }

    public void RemoveChannelContent(int userID, int folderID, List<int> contentIDList)
    {
      Channel.RemoveChannelContent(userID, folderID, contentIDList);
    }

    public RemovableContent RemoveSlideContent(int userID, int slideFolderID, int slideID)
    {
      return Channel.RemoveSlideContent(userID, slideFolderID, slideID);
    }

    public void AddPCStream(int userID, int pcID, List<int> contentIDList)
    {
      Channel.AddPCStream(userID, pcID, contentIDList);
    }

    public void AddPCStreamNotRegistered(int pcID, List<int> contentIDList)
    {
      Channel.AddPCStreamNotRegistered(pcID, contentIDList);
    }

    public void RemovePCStreamRegistered(int userID, int pcID, List<int> contentIDList)
    {
      Channel.RemovePCStreamRegistered(userID, pcID, contentIDList);
    }

    public void RemovePCStreamUnregistered(int pcID, List<int> contentIDList)
    {
      Channel.RemovePCStreamUnregistered(pcID, contentIDList);
    }

    public int AddAssetContentFolder(int userID, string folderName)
    {
      return Channel.AddAssetContentFolder(userID, folderName);
    }

    public string EditAssetContentFolder(int userID, int folderID, string folderName)
    {
      return Channel.EditAssetContentFolder(userID, folderID, folderName);
    }

    public HashSet<RemovableContent> RemoveAssetContentFolder(int userID, int folderID, out int filesLength)
    {
      return Channel.RemoveAssetContentFolder(userID, folderID, out filesLength);
    }

    public List<ChannelSimple> GetChannelMostPopular()
    {
      return Channel.GetChannelMostPopular();
    }

    public List<ChannelSimple> GetChannelListByLetter(string activeLetter, SortChannelsBy sort)
    {
      return Channel.GetChannelListByLetter(activeLetter, sort);
    }

    public int AddPC(int userID, string pcName)
    {
      return Channel.AddPC(userID, pcName);
    }

    public void RenamePC(int userID, int pcID, string pcName)
    {
      Channel.RenamePC(userID, pcID, pcName);
    }

    public void EditChannelWeighting(int userID, int pcID, int channelID, int channelWeighting)
    {
      Channel.EditChannelWeighting(userID, pcID, channelID, channelWeighting);
    }

    public Channel GetChannelDetailsFull(int userID, int channelID)
    {
      return Channel.GetChannelDetailsFull(userID, channelID);
    }

    public List<List<ChannelListChannel>> GetPCStreamsAll(int userID)
    {
      return Channel.GetPCStreamsAll(userID);
    }

    public List<Channel> GetTop5MostPopular(int categoryID)
    {
      return Channel.GetTop5MostPopular(categoryID);
    }

    public string GetCategoryName(int categoryID)
    {
      return Channel.GetCategoryName(categoryID);
    }

    public List<List<AssetContentListContent>> GetAssetContentAll(int userID)
    {
      return Channel.GetAssetContentAll(userID);
    }

    public List<List<SlideListSlide>> GetSlideFoldersAll(int userID)
    {
      return Channel.GetSlideFoldersAll(userID);
    }

    public bool UnlockStream(int userID, int channelID, string channelPassword)
    {
      return Channel.UnlockStream(userID, channelID, channelPassword);
    }

    public RemovableContent RemoveAssetContent(int userID, int assetContentFolderID, int assetContentID)
    {
      return Channel.RemoveAssetContent(userID, assetContentFolderID, assetContentID);
    }

    public int AddSlideFolder(int userID, string folderName)
    {
      return Channel.AddSlideFolder(userID, folderName);
    }

    public string EditSlideFolder(int userID, int folderID, string folderName)
    {
      return Channel.EditSlideFolder(userID, folderID, folderName);
    }

    public HashSet<RemovableContent> RemoveSlideFolder(int userID, int folderID)
    {
      return Channel.RemoveSlideFolder(userID, folderID);
    }

    public string AddChannel(int userID, int? categoryID, string channelName, string description,
      string longDescription, string keywords, bool bLocked, string password, bool bAcceptPasswordRequests)
    {
      return Channel.AddChannel(userID, categoryID, channelName, description, longDescription, keywords,
        bLocked, password, bAcceptPasswordRequests);
    }

    public void EditChannel(int userID, int channelID, int? categoryID, string channelName, string description,
      string longDescription, string keywords, bool bLocked, string password, bool bAcceptPasswordRequests, ChannelPrivacyOptions options)
    {
      Channel.EditChannel(userID, channelID, categoryID, channelName, description, longDescription, keywords,
        bLocked, password, bAcceptPasswordRequests, options);
    }

    public string RemoveChannel(int userID, int channelID)
    {
      return Channel.RemoveChannel(userID, channelID);
    }

    public DemographicData GetUserDemographicData(string userGUID)
    {
      return Channel.GetUserDemographicData(userGUID);
    }

    public OxigenIIAdvertising.AppData.ChannelSubscriptions GetUserChannelSubscriptions(string userGUID, string machineGUID)
    {
      return Channel.GetUserChannelSubscriptions(userGUID, machineGUID);
    }

    public List<OxigenIIAdvertising.AppData.Channel> GetChannelsDirty()
    {
      return Channel.GetChannelsDirty();
    }

    public List<SimpleFileInfo> GetAssetsDirty()
    {
      return Channel.GetAssetsDirty();
    }

    public void EditChannelsMakeClean(HashSet<int> channelIDs)
    {
      Channel.EditChannelsMakeClean(channelIDs);
    }

    public void EditSlidesMakeClean(HashSet<int> slideIDs)
    {
      Channel.EditSlidesMakeClean(slideIDs);
    }

    public void EditThumbnailGUID(int userID, int channelID, string thumbnailGUID, out string channelGUIDSuffix, out string oldChannelThumbnailPath)
    {
      Channel.EditThumbnailGUID(userID, channelID, thumbnailGUID, out channelGUIDSuffix, out oldChannelThumbnailPath);
    }

    public void EditSlideContentProperties(int userID, SlideProperties slide)
    {
      Channel.EditSlideContentProperties(userID, slide);
    }

    public SlideProperties GetSlideProperties(int userID, int slideID)
    {
      return Channel.GetSlideProperties(userID, slideID);
    }

    public void MoveRawContent(int userID, int oldFolderID, int newFolderID, List<int> contentIDList)
    {
      Channel.MoveRawContent(userID, oldFolderID, newFolderID, contentIDList);
    }

    public void MoveSlideContent(int userID, int oldFolderID, int newFolderID, List<int> slideIDList)
    {
      Channel.MoveSlideContent(userID, oldFolderID, newFolderID, slideIDList);
    }

    public void MovePCChannels(int userID, int oldPCID, int newPCID, List<int> channelIDList)
    {
      Channel.MovePCChannels(userID, oldPCID, newPCID, channelIDList);
    }

    public void EditChannelSlideProperties(int userID, int channelSlideID, string url, float displayDuration, string scheduleString, string presentationString)
    {
      Channel.EditChannelSlideProperties(userID, channelSlideID, url, displayDuration, scheduleString, presentationString);
    }

    public ChannelSlideProperties GetChannelSlideProperties(int userID, int channelsSlideID)
    {
      return Channel.GetChannelSlideProperties(userID, channelsSlideID);
    }

    public void SetCategory(int userID, int channelID, int categoryID)
    {
      Channel.SetCategory(userID, channelID, categoryID);
    }

    public void GetDataForPreviewFrames(int userID, string mediaType, int mediaID, out string subDir, out string mediaGUID, out string fileNameWithSubdir, out PreviewType previewType)
    {
      Channel.GetDataForPreviewFrames(userID, mediaType, mediaID, out subDir, out mediaGUID, out fileNameWithSubdir, out previewType);
    }

    public int GetUserExistsByUserCredentials(string emailAddress, string password, out string userGUID)
    {
      return Channel.GetUserExistsByUserCredentials(emailAddress, password, out userGUID);
    }   

    public void GetExistingUserDetails(string userGUID, string password, out string firstName, out string lastName, out string gender, 
      out DateTime dob, out string country, out string state, out string townCity, 
      out string occupationSector, out string employmentLevel, out string annualHouseholdIncome)
    {
      Channel.GetExistingUserDetails(userGUID, password, out firstName, out lastName, out gender, out dob, out country, out state,
        out townCity, out occupationSector, out employmentLevel, out annualHouseholdIncome);
    }

    public void UpdateUserAccount(string emailAddress, string password,
      string firstName, string lastName, string gender, DateTime dob, string townCity, string state, string country, 
      string occupationSector, string employmentLevel, string annualHouseholdIncome, 
      AppData.ChannelSubscriptions channelSubscriptions,
      int softwareMajorVersionNumber, 
      int softwareMinorVersionNumber,      
      string machineGUID,
      string newPcName,
      string macAddress)
    {
      Channel.UpdateUserAccount(emailAddress, password, firstName, lastName, gender, dob,
        townCity, state, country,
        occupationSector, employmentLevel, annualHouseholdIncome,
            channelSubscriptions,
            softwareMajorVersionNumber,
            softwareMinorVersionNumber,
            machineGUID,
            newPcName,
            macAddress);
    }

    public List<PcInfo> GetPcListForInstallerEmail(string emailAddress)
    {
      return Channel.GetPcListForInstallerEmail(emailAddress);
    }

    public List<PcInfo> GetPcListForInstallerGUID(string userGUID)
    {
      return Channel.GetPcListForInstallerGUID(userGUID);
    }

    public void LinkPCAndSubscriptionsExistingPC(string userGUID, 
      int pcID, 
      string machineGUID, 
      string macAddress,
      int softwareMajorVersionNumber, 
      int softwareMinorVersionNumber, 
      OxigenIIAdvertising.AppData.ChannelSubscriptions channelSubscriptions)
    {
      Channel.LinkPCAndSubscriptionsExistingPC(userGUID, 
        pcID, 
        machineGUID, 
        macAddress,
        softwareMajorVersionNumber, 
        softwareMinorVersionNumber,
        channelSubscriptions);
    }

    public void RegisterNewUser(string emailAddress, string password, string firstName, string lastName, string gender, 
      DateTime dob, string townCity, string state, string country, string occupationSector, 
      string employmentLevel, string annualHouseholdIncome, string userGUID, string machineGUID, 
      int softwareMajorVersionNumber, int softwareMinorVersionNumber, string macAddress,
      string machineName, OxigenIIAdvertising.AppData.ChannelSubscriptions channelSubscriptions, out string channelGUID)
    {
      Channel.RegisterNewUser(emailAddress, password, firstName, lastName, gender, dob,
          townCity, state, country, occupationSector,
          employmentLevel, annualHouseholdIncome,
          userGUID, machineGUID, softwareMajorVersionNumber, softwareMinorVersionNumber,
          macAddress, machineName, channelSubscriptions, out channelGUID);
    }

    public bool EmailAddressExists(string emailAddress)
    {
      return Channel.EmailAddressExists(emailAddress);
    }

    public void EditSubscriptionsByGUID(string userGUID, string machineGUID, OxigenIIAdvertising.AppData.ChannelSubscriptions channelSubscriptions)
    {
      Channel.EditSubscriptionsByGUID(userGUID, machineGUID, channelSubscriptions);
    }

    public OxigenIIAdvertising.AppData.ChannelSubscriptions GetPCSubscriptionsByPCID(string userGUID, int pcID)
    {
      return Channel.GetPCSubscriptionsByPCID(userGUID, pcID);
    }

    public OxigenIIAdvertising.AppData.ChannelSubscriptions GetPCSubscriptionsByMachineGUID(string userGUID, string machineGUID)
    {
      return Channel.GetPCSubscriptionsByMachineGUID(userGUID, machineGUID);
    }

    public bool GetMatchedUserGUID(string userGUID)
    {
      return Channel.GetMatchedUserGUID(userGUID);
    }

    public bool GetMatchedMachineGUID(string userGUID, string machineGUID)
    {
      return Channel.GetMatchedMachineGUID(userGUID, machineGUID);
    }

    public void RemovePCFromUninstall(string userGUID, string machineGUID)
    {
      Channel.RemovePCFromUninstall(userGUID, machineGUID);
    }

    public List<PC> GetSubscriptionsNotRegistered(string PcProfileToken)
    {
      return Channel.GetSubscriptionsNotRegistered(PcProfileToken);
    }

    public List<PC> GetSubscriptions(int userID)
    {
      return Channel.GetSubscriptions(userID);
    }

    public List<Channel> GetChannelsByUserID(int userID)
    {
      return Channel.GetChannelsByUserID(userID);
    }

    public Channel GetChannelToDownload(int userID, int channelID)
    {
      return Channel.GetChannelToDownload(userID, channelID);
    }

    public void RemoveTemporaryPCProfilesNotRegistered(string pcProfileToken)
    {
      Channel.RemoveTemporaryPCProfilesNotRegistered(pcProfileToken);
    }

    public void RemoveTemporaryPCProfiles(int userID)
    {
      Channel.RemoveTemporaryPCProfiles(userID);
    }

    public void SetPassword(string email, string newPassword)
    {
      Channel.SetPassword(email, newPassword);
    }

    public List<LocationNameValue> GetCountries()
    {
      return Channel.GetCountries();
    }

    public UserDetails GetUserDetails(int userID)
    {
      return Channel.GetUserDetails(userID);
    }

    public List<LocationNameValue> GetChildGeoTTNodes(int parentLocationID)
    {
      return Channel.GetChildGeoTTNodes(parentLocationID);
    }

    public List<KeyValuePair<int, string>> GetSocioEconomicStatuses(string seType)
    {
      return Channel.GetSocioEconomicStatuses(seType);
    }

    public void GetChannelCreatorDetailsForPasswordRequest(int channelID, out string channelCreatorEmailAddress, out string channelCreatorName, out string channelName)
    {
      Channel.GetChannelCreatorDetailsForPasswordRequest(channelID, out channelCreatorEmailAddress, out channelCreatorName, out channelName);
    }

    public bool EditUserDetails(int userID, string emailAddress, string password, string firstName, 
      string lastName, string gender, DateTime dob, int townCityID, int occupationSectorID, 
      int employmentLevelID, int annualHouseholdIncomeID, bool bUpdatePassword)
    {
      return Channel.EditUserDetails(userID, emailAddress, password, firstName, 
        lastName, gender, dob, townCityID, occupationSectorID,
        employmentLevelID, annualHouseholdIncomeID, bUpdatePassword);
    }

    public void GetUserAccountAssets(int userID, out bool bHasLinkedPCs, out HashSet<AssetContent> assetContentFiles, out HashSet<SlideContent> slideFiles, out HashSet<string> channelThumbnails)
    {
      Channel.GetUserAccountAssets(userID, out bHasLinkedPCs, out assetContentFiles, out slideFiles, out channelThumbnails);
    }

    public void RemoveUserAccount(int userID)
    {
      Channel.RemoveUserAccount(userID);
    }

    public string SyncWithServerNoPersonalDetails(string userGUID, string machineGUID, string macAddress, string machineName, int softwareMajorVersionNumber, int softwareMinorVersionNumber, OxigenIIAdvertising.AppData.ChannelSubscriptions channelSubscriptions)
    {
      return Channel.SyncWithServerNoPersonalDetails(userGUID, machineGUID, macAddress, machineName, softwareMajorVersionNumber, softwareMinorVersionNumber, channelSubscriptions);
    }

    public string GetUserGUIDByUsername(string username)
    {
      return Channel.GetUserGUIDByUsername(username);
    }

    public void RemoveStreamsFromSilentMergeByMachineGUID(string machineGUID, AppData.ChannelSubscriptions channelSubscriptions)
    {
      Channel.RemoveStreamsFromSilentMerge(machineGUID, channelSubscriptions);
    }

    public void ReplaceStreamsFromSilentMergeByMachineGUID(string machineGUID, AppData.ChannelSubscriptions channelSubscriptions)
    {
      Channel.ReplaceStreamsFromSilentMerge(machineGUID, channelSubscriptions);
    }

    public void AddStreamsFromSilentMergeByMachineGUID(string machineGUID, AppData.ChannelSubscriptions channelSubscriptions)
    {
      Channel.AddStreamsFromSilentMerge(machineGUID, channelSubscriptions);
    }

    public HashSet<string> UpdateSoftwareVersionInfo(MachineVersionInfo[] mi)
    {
      return Channel.UpdateSoftwareVersionInfo(mi);
    }

    public void CompareMACAddresses(string macAddressClient, string userGUID, int softwareMajorVersionNumber,
  int softwareMinorVersioNumber, out string newMachineGUID, out bool bMatch)
    {
      Channel.CompareMACAddresses(macAddressClient, userGUID, softwareMajorVersionNumber, softwareMinorVersioNumber, out newMachineGUID, out bMatch);
    }

    public string CreatePCIfNotExists(string userGUID, string macAddress, string machineName, int majorVersionNumber, int minorVersionNumber)
    {
      return Channel.CreatePCIfNotExists(userGUID, macAddress, machineName, majorVersionNumber, minorVersionNumber);
    }

    public string AddSubscriptionsAndNewPC(string userGUID, string macAddress, string machineName, int majorVersionNumber, int minorVersionNumber, string[][] subscriptions)
    {
      return Channel.AddSubscriptionsAndNewPC(userGUID, macAddress, machineName, majorVersionNumber, minorVersionNumber, subscriptions);
    }

    public string CheckIfPCExistsReturnGUID(string username, string macAddress)
    {
      return Channel.CheckIfPCExistsReturnGUID(username, macAddress);
    }

    public void RemoveStreamsFromSilentMerge(string macAddress, OxigenIIAdvertising.AppData.ChannelSubscriptions channelSubscriptions)
    {
      Channel.RemoveStreamsFromSilentMerge(macAddress, channelSubscriptions);
    }

    public void ReplaceStreamsFromSilentMerge(string macAddress, OxigenIIAdvertising.AppData.ChannelSubscriptions channelSubscriptions)
    {
      Channel.ReplaceStreamsFromSilentMerge(macAddress, channelSubscriptions);
    }

    public void AddStreamsFromSilentMerge(string macAddress, OxigenIIAdvertising.AppData.ChannelSubscriptions channelSubscriptions)
    {
      Channel.AddStreamsFromSilentMerge(macAddress, channelSubscriptions);
    }
  }
}
