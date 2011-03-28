using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using OxigenIIAdvertising.SOAStructures;
using OxigenIIAdvertising.ServiceContracts.BLServices;
using ProxyClientBaseLib;

namespace OxigenIIAdvertising.BLClients
{
  public class BLClient : ProxyClientBase<IBLService>, IBLService
  {
    public PageChannelData GetChannelListByCategoryID(int userID, int categoryID, int startPageNo, int endPageNo, SortChannelsBy sortBy)
    {
      return Channel.GetChannelListByCategoryID(userID, categoryID, startPageNo, endPageNo, sortBy);
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

    public List<PC> GetPcList(int userID, string pcProfileToken)
    {
      return Channel.GetPcList(userID, pcProfileToken);
    }

    public PageChannelData GetPcStreams(int userID, int pcID)
    {
      return Channel.GetPcStreams(userID, pcID);
    }

    public User Login(string username, string password)
    {
      return Channel.Login(username, password);
    }

    public User Signup(string emailAddress, string password, string firstName, string lastName)
    {
      return Channel.Signup(emailAddress, password, firstName, lastName);
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

    public PageSlideData GetUserStreams(int userID, int channelID)
    {
      return Channel.GetUserStreams(userID, channelID);
    }

    public List<List<CreateContentGenericFolder>> GetFolders(int userID)
    {
      return Channel.GetFolders(userID);
    }

    public void PutRawContentProperties(int userID, int contentID, string title, string creator,
      string caption, DateTime? date, string url, float displayDuration)
    {
      Channel.PutRawContentProperties(userID, contentID, title, creator, caption, date, url, displayDuration);
    }

    public ChannelProperties GetChannelProperties(int userID, int streamID)
    {
      return Channel.GetChannelProperties(userID, streamID);
    }

    public void AddChannelContent(int userID, int channelID, string url, float duration, string startDate, string endDate, 
      string startTime, string endTime, string daysOfWeek, List<int> slideIDList)
    {
      Channel.AddChannelContent(userID, channelID, url, duration, startDate, endDate, startTime, endTime, daysOfWeek, slideIDList);
    }

    public bool AddAssetContent(int userID, int folderID, List<AssetContent> assetContents)
    {
      return Channel.AddAssetContent(userID, folderID, assetContents);
    }

    public void AddSlideContent(int userID, int folderID, List<int> contentIDList)
    {
      Channel.AddSlideContent(userID, folderID, contentIDList);
    }

    public void RemoveChannelContent(int userID, int folderID, List<int> contentIDList)
    {
      Channel.RemoveChannelContent(userID, folderID, contentIDList);
    }

    public bool RemoveSlideContent(int userID, int slideFolderID, int slideID)
    {
      return Channel.RemoveSlideContent(userID, slideFolderID, slideID);
    }

    public void AddPCStream(int userID, int pcID, List<int> contentIDList)
    {
      Channel.AddPCStream(userID, pcID, contentIDList);
    }

    public void RemovePCStream(int userID, int pcID, List<int> contentIDList)
    {
       Channel.RemovePCStream(userID, pcID, contentIDList);
    }

    public int AddAssetContentFolder(int userID, string folderName)
    {
      return Channel.AddAssetContentFolder(userID, folderName);
    }

    public string EditAssetContentFolder(int userID, int folderID, string folderName)
    {
      return Channel.EditAssetContentFolder(userID, folderID, folderName);
    }

    public int RemoveAssetContentFolder(int userID, int folderID)
    {
      return Channel.RemoveAssetContentFolder(userID, folderID);
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

    public int RemoveAssetContent(int userID, int assetContentFolderID, int assetContentID)
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

    public bool RemoveSlideFolder(int userID, int folderID)
    {
      return Channel.RemoveSlideFolder(userID, folderID);
    }

    public void AddChannel(int userID, int? categoryID, string channelName, string description,
      string longDescription, string keywords, bool bLocked, string password, bool bAcceptPasswordRequests)
    {
      Channel.AddChannel(userID, categoryID, channelName, description, longDescription, keywords,
        bLocked, password, bAcceptPasswordRequests);
    }

    public void EditChannel(int userID, int channelID, int? categoryID, string channelName, string description,
      string longDescription, string keywords, bool bLocked, string password, bool bAcceptPasswordRequests, ChannelPrivacyOptions options)
    {
      Channel.EditChannel(userID, channelID, categoryID, channelName, description, longDescription, keywords,
        bLocked, password, bAcceptPasswordRequests, options);
    }

    public void RemoveChannel(int userID, int channelID)
    {
      Channel.RemoveChannel(userID, channelID);
    }

    public List<ChannelSimple> GetChannelsAll(int userID)
    {
      //TODO: implement
      throw new NotImplementedException();
    }

    public void EditChannelThumbnail(int userID, int channelID, string imagePath)
    {
      Channel.EditChannelThumbnail(userID, channelID, imagePath);
    }

    public void EditSlideContentProperties(int userID, int slideID, string title, string creator, string caption, DateTime? date, string URL, float displayDuration)
    {
      Channel.EditSlideContentProperties(userID, slideID, title, creator, caption, date, URL, displayDuration);
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

    public void EditChannelSlideProperties(int userID, int channelSlideID, string url, float displayDuration, string dayOfWeekCode, string[] startEndDateTimes)
    {
      Channel.EditChannelSlideProperties(userID, channelSlideID, url, displayDuration, dayOfWeekCode, startEndDateTimes);
    }

    public ChannelSlideProperties GetChannelSlideProperties(int userID, int channelsSlideID)
    {
      return Channel.GetChannelSlideProperties(userID, channelsSlideID);
    }

    public void SetCategory(int userID, int channelID, int categoryID)
    {
      Channel.SetCategory(userID, channelID, categoryID);
    }

    public void GetPreviewFrames(int userID, string mediaType, int mediaID, out string subDir, out string[] files, out PreviewType previewType)
    {
      Channel.GetPreviewFrames(userID, mediaType, mediaID, out subDir, out files, out previewType);
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

    public void SendPasswordReminder(string email)
    {
      Channel.SendPasswordReminder(email);
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

    public void SendPasswordRequest(int channelID, string name, string emailAddress, string message)
    {
      Channel.SendPasswordRequest(channelID, name, emailAddress, message);
    }

    public bool EditUserDetails(int userID, string emailAddress, string password, string firstName, string lastName, 
      string gender, DateTime dob, int townCityID, int occupationSectorID, int employmentLevelID, int annualHouseholdIncomeID)
    {
      return Channel.EditUserDetails(userID, emailAddress, password, firstName, lastName, gender, dob, townCityID, occupationSectorID, employmentLevelID, annualHouseholdIncomeID);
    }

    public bool RemoveUserAccount(int userID)
    {
      return Channel.RemoveUserAccount(userID);
    }

    public void SendContactEmail(string name, string emailAddress, string subject, string message)
    {
      Channel.SendContactEmail(name, emailAddress, subject, message);
    }
  }
}
