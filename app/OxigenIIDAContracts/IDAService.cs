using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterCommunicationStructures;
using OxigenIIAdvertising.SOAStructures;
using System.ServiceModel;
using OxigenIIAdvertising.Demographic;

namespace OxigenIIAdvertising.ServiceContracts.DAServices
{
  [ServiceContract(Namespace = "http://oxigen.net")]
  public interface IDAService
  {
    [OperationContract]
    PageChannelData GetChannelListByCategoryID(int userID, int categoryID, int startChannelNo, int endChannelNo, SortChannelsBy sortBy);

    [OperationContract]
    List<Category> GetCategoryList(int categoryID);

    [OperationContract]
    Channel GetChannelDetailsSimple(int channelID);

    [OperationContract]
    List<PC> GetPcListRegistered(int userID);
    
    [OperationContract]
    List<PC> GetPcListUnregistered(string pcProfileToken);

    [OperationContract]
    List<ChannelListChannel> Search(int userID, string text);

    [OperationContract]
    PageChannelData GetPcStreamsRegistered(int userID, int pcID);

    [OperationContract]
    PageChannelData GetPcStreamsUnregistered(int pcID);

    [OperationContract]
    User Login(string username, string password);

    [OperationContract]
   // [TransactionFlow(TransactionFlowOption.Mandatory)]
    User Signup(string emailAddress, string password, string firstName, string lastName, out string channelGUID);

    [OperationContract]
    PageAssetContentData GetRawContent(int userID, int folderID);

    [OperationContract]
    AssetContentProperties GetRawContentProperties(int userID, int contentID);

    [OperationContract]
    PageSlideData GetStreamSlides(int userID, int folderID);

    [OperationContract]
    PageSlideData GetUserStreams(int userID, int folderID);

    [OperationContract]
    List<List<CreateContentGenericFolder>> GetFolders(int userID);

    [OperationContract]
    void PutRawContentProperties(int userID, AssetContentProperties contentProperties);

    [OperationContract]
    ChannelProperties GetChannelProperties(int userID, int streamID);

    [OperationContract]
//    [TransactionFlow(TransactionFlowOption.Mandatory)]
    void AddChannelContent(int userID, int channelID, List<int> slideIDList, string schedule, string presentationSchedule, string url, float displayDuration, string newThumbnailGUID, out string slideThumbnailWithPartialPath, out string channelGUID);

    [OperationContract]
 //   [TransactionFlow(TransactionFlowOption.Mandatory)]
    void AddAssetContent(int userID, int folderID, List<AssetContent> assetContents);

    [OperationContract]
  //  [TransactionFlow(TransactionFlowOption.Mandatory)]
    List<SlideContent> AddSlideContent(int userID, int folderID, List<int> contentIDList);

    [OperationContract]
    void RemoveChannelContent(int userID, int folderID, List<int> contentIDList);

    [OperationContract]
    [TransactionFlow(TransactionFlowOption.Mandatory)]
    RemovableContent RemoveSlideContent(int userID, int slideFolderID, int slideID);

    [OperationContract]
    void AddPCStream(int userID, int pcID, List<int> contentIDList);

    [OperationContract]
    void AddPCStreamNotRegistered(int pcID, List<int> contentIDList);

    [OperationContract]
    void RemovePCStreamRegistered(int userID, int pcID, List<int> contentIDList);

    [OperationContract]
    void RemovePCStreamUnregistered(int pcID, List<int> contentIDList);

    [OperationContract]
    int AddAssetContentFolder(int userID, string folderName);

    [OperationContract]
    string EditAssetContentFolder(int userID, int folderID, string folderName);

    [OperationContract]
    HashSet<RemovableContent> RemoveAssetContentFolder(int userID, int folderID, out int filesLength);

    [OperationContract]
    List<ChannelSimple> GetChannelMostPopular();

    [OperationContract]
    List<ChannelSimple> GetChannelListByLetter(string activeLetter, SortChannelsBy sort);

    [OperationContract]
    int AddPC(int userID, string pcName);

    [OperationContract]
    void RenamePC(int userID, int pcID, string pcName);

    [OperationContract]
    void EditChannelWeighting(int userID, int pcID, int channelID, int channelWeighting);

    [OperationContract]
    Channel GetChannelDetailsFull(int userID, int channelID);

    [OperationContract]
    List<List<ChannelListChannel>> GetPCStreamsAll(int userID);

    [OperationContract]
    List<Channel> GetTop5MostPopular(int categoryID);

    [OperationContract]
    string GetCategoryName(int categoryID);

    [OperationContract]
    List<List<AssetContentListContent>> GetAssetContentAll(int userID);

    [OperationContract]
    List<List<SlideListSlide>> GetSlideFoldersAll(int userID);

    [OperationContract]
    bool UnlockStream(int userID, int channelID, string channelPassword);

    [OperationContract]
    [TransactionFlow(TransactionFlowOption.Mandatory)]
    RemovableContent RemoveAssetContent(int userID, int assetContentFolderID, int assetContentID);

    [OperationContract]
    int AddSlideFolder(int userID, string folderName);

    [OperationContract]
    string EditSlideFolder(int userID, int folderID, string folderName);

    [OperationContract]
    HashSet<RemovableContent> RemoveSlideFolder(int userID, int folderID);

    [OperationContract]
    string AddChannel(int userID, int? categoryID, string channelName, string description,
      string longDescription, string keywords, bool bLocked, string password, bool bAcceptPasswordRequests);

    [OperationContract]
    void EditChannel(int userID, int channelID, int? categoryID, string channelName, string description,
      string longDescription, string keywords, bool bLocked, string password, bool bAcceptPasswordRequests,
      ChannelPrivacyOptions options);

    [OperationContract]
 //   [TransactionFlow(TransactionFlowOption.Mandatory)]
    string RemoveChannel(int userID, int channelID);

    [OperationContract]
    List<AppData.Channel> GetChannelsDirty();

    [OperationContract]
    List<SimpleFileInfo> GetAssetsDirty();

    [OperationContract]
    void EditChannelsMakeClean(HashSet<int> channelIDs);

    [OperationContract]
    void EditSlidesMakeClean(HashSet<int> slideIDs);

    [OperationContract]
    DemographicData GetUserDemographicData(string userGUID);

    [OperationContract]
    AppData.ChannelSubscriptions GetUserChannelSubscriptions(string userGUID, string machineGUID);

    [OperationContract]
    [TransactionFlow(TransactionFlowOption.Mandatory)]
    void EditThumbnailGUID(int userID, int channelID, string thumbnailGUID, out string channelGUIDSuffix, out string oldChannelThumbnailPath);

    [OperationContract]
    void EditSlideContentProperties(int userID, SlideProperties slide);

    [OperationContract]
    SlideProperties GetSlideProperties(int userID, int slideID);

    [OperationContract]
    void MoveRawContent(int userID, int oldFolderID, int newFolderID, List<int> contentIDList);

    [OperationContract]
    void MoveSlideContent(int userID, int oldFolderID, int newFolderID, List<int> slideIDList);

    [OperationContract]
    void MovePCChannels(int userID, int oldPCID, int newPCID, List<int> channelIDList);

    [OperationContract]
    void EditChannelSlideProperties(int userID, int channelSlideID, string url, float displayDuration, string scheduleString, string presentationString);

    [OperationContract]
    ChannelSlideProperties GetChannelSlideProperties(int userID, int channelsSlideID);

    [OperationContract]
    void SetCategory(int userID, int channelID, int categoryID);

    [OperationContract]
    void GetDataForPreviewFrames(int userID, string mediaType, int mediaID, out string subDir, out string mediaGUID, out string fileNameWithSubdir, out PreviewType previewType);

    [OperationContract]
    int GetUserExistsByUserCredentials(string emailAddress, string password, out string userGUID);
        
    [OperationContract]
    void GetExistingUserDetails(string userGUID, string password, out string firstName, out string lastName, out string gender, 
      out DateTime dob, out string country, out string state, out string townCity, 
      out string occupationSector, out string employmentLevel, out string annualHouseholdIncome);

    [OperationContract]
    void UpdateUserAccount(string emailAddress, string password, string firstName, string lastName,
      string gender, DateTime dob, 
      string townCity, string state, string country,
      string occupationSector, string employmentLevel, string annualHouseholdIncome,
      AppData.ChannelSubscriptions channelSubscriptions,
      int softwareMajorVersionNumber,
      int softwareMinorVersionNumber,
      string machineGUID,
      string newPcName,
      string macAddress);

    [OperationContract]
    List<PcInfo> GetPcListForInstallerEmail(string emailAddress);

    [OperationContract]
    List<PcInfo> GetPcListForInstallerGUID(string userGUID);

    [OperationContract]
    void LinkPCAndSubscriptionsExistingPC(string userGUID, 
      int pcID, 
      string machineGUID,
      string macAddress,
      int softwareMajorVersionNumber, 
      int softwareMinorVersionNumber, 
      AppData.ChannelSubscriptions channelSubscriptions);

    [OperationContract]
    [TransactionFlow(TransactionFlowOption.Mandatory)]
    void RegisterNewUser(string emailAddress, string password, string firstName, string lastName, string gender, 
      DateTime dob, string townCity, string state, string country, string occupationSector,
      string employmentLevel, string annualHouseholdIncome, string userGUID, string machineGUID, 
      int softwareMajorVersionNumber, int softwareMinorVersionNumber, string macAddress, 
      string machineName, AppData.ChannelSubscriptions channelSubscriptions, out string channelGUID);

    [OperationContract]
    bool EmailAddressExists(string emailAddress);

    [OperationContract]
    void EditSubscriptionsByGUID(string userGUID, string machineGUID, AppData.ChannelSubscriptions channelSubscriptions);

    [OperationContract]
    AppData.ChannelSubscriptions GetPCSubscriptionsByPCID(string userGUID, int pcID);

    [OperationContract]
    AppData.ChannelSubscriptions GetPCSubscriptionsByMachineGUID(string userGUID, string machineGUID);

    [OperationContract]
    void CompareMACAddresses(string macAddressClient, string userGUID, int softwareMajorVersionNumber,
      int softwareMinorVersioNumber, out string newMachineGUID, out bool bMatch);

    [OperationContract]
    string CreatePCIfNotExists(string userGUID, string macAddress, string machineName, int majorVersionNumber, int minorVersionNumber);

    [OperationContract]
    string AddSubscriptionsAndNewPC(string userGUID, string macAddress, string machineName, int majorVersionNumber, int minorVersionNumber, string[][] subscriptions);

    [OperationContract]
    string CheckIfPCExistsReturnGUID(string username, string macAddress);

    [OperationContract]
    void RemoveStreamsFromSilentMerge(string macAddress, AppData.ChannelSubscriptions channelSubscriptions);

    [OperationContract]
    void ReplaceStreamsFromSilentMerge(string macAddress, AppData.ChannelSubscriptions channelSubscriptions);

    [OperationContract]
    void AddStreamsFromSilentMerge(string macAddress, AppData.ChannelSubscriptions channelSubscriptions);

    [OperationContract]
    bool GetMatchedUserGUID(string userGUID);

    [OperationContract]
    bool GetMatchedMachineGUID(string userGUID, string machineGUID);

    [OperationContract]
    void RemovePCFromUninstall(string userGUID, string emailGUID);

    [OperationContract]
    List<PC> GetSubscriptionsNotRegistered(string PcProfileToken);

    [OperationContract]
    List<PC> GetSubscriptions(int userID);

    [OperationContract]
    List<Channel> GetChannelsByUserID(int userID);

    [OperationContract]
    Channel GetChannelToDownload(int userID, int channelID);

    [OperationContract]
    void RemoveTemporaryPCProfilesNotRegistered(string pcProfileToken);

    [OperationContract]
    void RemoveTemporaryPCProfiles(int userID);

    [OperationContract]
    void SetPassword(string email, string newPassword);

    [OperationContract]
    List<LocationNameValue> GetCountries();

    [OperationContract]
    UserDetails GetUserDetails(int userID);

    [OperationContract]
    List<LocationNameValue> GetChildGeoTTNodes(int parentLocationID);

    [OperationContract]
    List<KeyValuePair<int, string>> GetSocioEconomicStatuses(string seType);

    [OperationContract]
    void GetChannelCreatorDetailsForPasswordRequest(int channelID, out string channelCreatorEmailAddress, out string channelCreatorName, out string channelName);

    [OperationContract]
    bool EditUserDetails(int userID, string emailAddress, string password,
      string firstName, string lastName, string gender, DateTime dob, int townCityID, 
      int occupationSectorID, int employmentLevelID, int annualHouseholdIncomeID, 
      bool bUpdatePassword);

    [OperationContract]
    [TransactionFlow(TransactionFlowOption.Mandatory)]
    void GetUserAccountAssets(int userID, out bool bHasLinkedPCs, out HashSet<AssetContent> assetContentFiles,
      out HashSet<SlideContent> slideFiles, out HashSet<string> channelThumbnails);

    [OperationContract]
    [TransactionFlow(TransactionFlowOption.Mandatory)]
    void RemoveUserAccount(int userID);

    [OperationContract]
    string SyncWithServerNoPersonalDetails(string userGUID, string machineGUID, string macAddress, string machineName, int softwareMajorVersionNumber, int softwareMinorVersionNumber, AppData.ChannelSubscriptions channelSubscriptions);

    [OperationContract]
    string GetUserGUIDByUsername(string username);

    [OperationContract]
    void RemoveStreamsFromSilentMergeByMachineGUID(string machineGUID, AppData.ChannelSubscriptions channelSubscriptions);

    [OperationContract]
    void ReplaceStreamsFromSilentMergeByMachineGUID(string machineGUID, AppData.ChannelSubscriptions channelSubscriptions);

    [OperationContract]
    void AddStreamsFromSilentMergeByMachineGUID(string machineGUID, AppData.ChannelSubscriptions channelSubscriptions);

    [OperationContract]
    HashSet<string> UpdateSoftwareVersionInfo(MachineVersionInfo[] mi);
  }
}