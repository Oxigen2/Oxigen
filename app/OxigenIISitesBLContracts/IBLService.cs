using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using OxigenIIAdvertising.SOAStructures;

namespace OxigenIIAdvertising.ServiceContracts.BLServices
{
  [ServiceContract(Namespace = "http://oxigen.net")]
  public interface IBLService
  {
    [OperationContract]
    PageChannelData GetChannelListByCategoryID(int userID, int categoryID, int startPageNo, int endPageNo, SortChannelsBy sortBy);
    
    [OperationContract]
    List<Category> GetCategoryList(int categoryID);

    [OperationContract]
    Channel GetChannelDetailsSimple(int channelID);

    [OperationContract]
    List<PC> GetPcList(int userID, string pcProfileToken);

    [OperationContract]
    List<ChannelListChannel> Search(int userID, string text);

    [OperationContract]
    PageChannelData GetPcStreams(int userID, int pcID);

    [OperationContract]
    User Login(string username, string password);

    [OperationContract]
    User Signup(string emailAddress, string password, string firstName, string lastName);

    [OperationContract]
    PageAssetContentData GetRawContent(int userID, int folderID);

    [OperationContract]
    AssetContentProperties GetRawContentProperties(int userID, int contentID);

    [OperationContract]
    PageSlideData GetStreamSlides(int userID, int folderID);

    [OperationContract]
    PageSlideData GetUserStreams(int userID, int channelID);

    [OperationContract]
    List<List<CreateContentGenericFolder>> GetFolders(int userID);

    [OperationContract]
    void PutRawContentProperties(int userID, int contentID, string title, string creator,
      string caption, DateTime? date, string url, float displayDuration);

    [OperationContract]
    ChannelProperties GetChannelProperties(int userID, int streamID);

    [OperationContract]
    void AddChannelContent(int userID, int channelID, string url, float duration, string startDate, string endDate,
      string startTime, string endTime, string daysOfWeek, List<int> slideIDList);

    [OperationContract]
    bool AddAssetContent(int userID, int folderID, List<AssetContent> assetContents);


    [OperationContract]
    void AddSlideContent(int userID, int folderID, List<int> contentIDList);



    [OperationContract]
    void RemoveChannelContent(int userID, int folderID, List<int> contentIDList);

    [OperationContract]
    bool RemoveSlideContent(int userID, int slideFolderID, int slideID);

    [OperationContract]
    void AddPCStream(int userID, int pcID, List<int> contentIDList);

    [OperationContract]
    void RemovePCStream(int userID, int pcID, List<int> contentIDList);

    [OperationContract]
    int AddAssetContentFolder(int userID, string folderName);

    [OperationContract]
    string EditAssetContentFolder(int userID, int folderID, string folderName);

    [OperationContract]
    int RemoveAssetContentFolder(int userID, int folderID);

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
    int RemoveAssetContent(int userID, int assetContentFolderID, int assetContentID);

    [OperationContract]
    int AddSlideFolder(int userID, string folderName);

    [OperationContract]
    string EditSlideFolder(int userID, int folderID, string folderName);

    [OperationContract]
    bool RemoveSlideFolder(int userID, int folderID);

    [OperationContract]
    void AddChannel(int userID, int? categoryID, string channelName, string description, 
      string longDescription, string keywords, bool bLocked, string password, bool bAcceptPasswordRequests);
    
    [OperationContract]
    void EditChannel(int userID, int channelID, int? categoryID, string channelName, string description,
      string longDescription, string keywords, bool bLocked, string password, bool bAcceptPasswordRequests,
      ChannelPrivacyOptions options);

    [OperationContract]
    void RemoveChannel(int userID, int channelID);

    [OperationContract]
    void EditChannelThumbnail(int userID, int channelID, string imagePath);

    [OperationContract]
    void EditSlideContentProperties(int userID, int slideID, string title, string creator, string caption, DateTime? date, string URL, float displayDuration);

    [OperationContract]
    SlideProperties GetSlideProperties(int userID, int slideID);

    [OperationContract]
    void MoveRawContent(int userID, int oldFolderID, int newFolderID, List<int> contentIDList);

    [OperationContract]
    void MoveSlideContent(int userID, int oldFolderID, int newFolderID, List<int> slideIDList);

    [OperationContract]
    void MovePCChannels(int userID, int oldPCID, int newPCID, List<int> channelIDList);
 
    [OperationContract]
    void EditChannelSlideProperties(int userID, int channelSlideID, string url, float displayDuration, string dayOfWeekCode, string[] startEndDateTimes);

    [OperationContract]
    ChannelSlideProperties GetChannelSlideProperties(int userID, int channelsSlideID);
    
    [OperationContract]
    void SetCategory(int userID, int channelID, int categoryID);

    [OperationContract]
    void GetPreviewFrames(int userID, string mediaType, int mediaID, out string subDir, out string[] files, out PreviewType previewType);

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
    void SendPasswordReminder(string email);

    [OperationContract]
    List<LocationNameValue> GetCountries();

    [OperationContract]
    UserDetails GetUserDetails(int userID);

    [OperationContract]
    List<LocationNameValue> GetChildGeoTTNodes(int parentLocationID);

    [OperationContract]
    List<KeyValuePair<int, string>> GetSocioEconomicStatuses(string seType);

    [OperationContract]
    void SendPasswordRequest(int channelID, string name, string emailAddress, string message);

    [OperationContract]
    bool EditUserDetails(int userID, string emailAddress, string password, string firstName, string lastName, string gender, DateTime dob, int townCityID, int occupationSectorID, int employmentLevelID, int annualHouseholdIncomeID);

    [OperationContract]
    bool RemoveUserAccount(int userID);

    [OperationContract]
    void SendContactEmail(string name, string emailAddress, string subject, string message);
  }
}
