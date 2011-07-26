using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.ServiceContracts.BLServices;
using OxigenIIAdvertising.SOAStructures;
using OxigenIIAdvertising.DAClients;
using System.IO;
using System.Transactions;
using System.ServiceModel;
using System.Diagnostics;
using System.Threading;
using OxigenIISitesBLServices;

namespace OxigenIIAdvertising.Services
{
  public class BLService : IBLService
  {
    EventLog _eventLog = null;

    public BLService()
    {
      _eventLog = new EventLog();
      _eventLog.Log = String.Empty;
      _eventLog.Source = "Oxigen Business Logic Host";
    }

    public PageChannelData GetChannelListByCategoryID(int userID, int categoryID, int startPageNo, int endPageNo, SortChannelsBy sortBy)
    {
      int startChannelNo;
      int endChannelNo;

      startChannelNo = (12 * (startPageNo - 1)) + 1;

      if (endPageNo == startPageNo)
        endChannelNo = startChannelNo + 11;
      else
        endChannelNo = startChannelNo - 1 + ((endPageNo - startPageNo) * 12);

      PageChannelData pageChannelData = null;

      DAClient client = null;

      try
      {
        client = new DAClient();

        pageChannelData = client.GetChannelListByCategoryID(userID, categoryID, startChannelNo, endChannelNo, sortBy);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return pageChannelData;
    }

    public List<Category> GetCategoryList(int categoryID)
    {
      List<Category> categoryList = null;

      DAClient client = null;

      try
      {
        client = new DAClient();

        categoryList = client.GetCategoryList(categoryID);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return categoryList;
    }

    public Channel GetChannelDetailsSimple(int channelID)
    {
      Channel channel = null;

      DAClient client = null;

      try
      {
        client = new DAClient();

        channel = client.GetChannelDetailsSimple(channelID);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return channel;
    }

    public List<PC> GetPcList(int userID, string pcProfileToken)
    {
      List<PC> pcList = null;

      DAClient client = null;

      try
      {
        client = new DAClient();

        if (userID != -1)
          pcList = client.GetPcListRegistered(userID);
        else
          pcList = client.GetPcListUnregistered(pcProfileToken);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return pcList;
    }

    public List<ChannelListChannel> Search(int userID, string text)
    {
      List<ChannelListChannel> channelList = null;

      DAClient client = null;

      try
      {
        client = new DAClient();

        channelList = client.Search(userID, text);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return channelList;
    }

    public PageChannelData GetPcStreams(int userID, int pcID)
    {
      PageChannelData pageChannelData = null;

      DAClient client = null;

      try
      {
        client = new DAClient();

        if (userID != -1)
          pageChannelData = client.GetPcStreamsRegistered(userID, pcID);
        else
          pageChannelData = client.GetPcStreamsUnregistered(pcID);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return pageChannelData;
    }

    public User Login(string username, string password)
    {
      User user = null;

      DAClient client = null;
     
      try
      {
        client = new DAClient();

        user = client.Login(username, password);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return user;
    }
    
    public User Signup(string emailAddress, string password, string firstName, string lastName)
    {
      DAClient client = null;
      User user = null;
      string channelGUID;

      //using (TransactionScope ts = new TransactionScope())
      //{
        try
        {
          client = new DAClient();

          user = client.Signup(emailAddress, password, firstName, lastName, out channelGUID);
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.ToString());

          throw ex;
        }
        finally
        {
          if (client != null)
            client.Dispose();
        }

        if (user != null)
        {
          try
          {
            CreateDefaultThumbnailForChannel(channelGUID);
          }
          catch (Exception ex)
          {
            Console.WriteLine(ex.ToString());

            throw ex;
          }
        //}

        //ts.Complete();
      }

      return user;
    }

    private void CreateDefaultThumbnailForChannel(string channelGUID)
    {
      string thumbnailChannelPath = (string)System.Configuration.ConfigurationSettings.AppSettings["thumbnailChannelPath"];
      string channelSubDir = FilenameMakerLib.FilenameFromGUID.GetGUIDSuffix(channelGUID);
      string thumbnailChannelPathWithoutFilename = thumbnailChannelPath + channelSubDir;

      if (!Directory.Exists(thumbnailChannelPathWithoutFilename))
        Directory.CreateDirectory(thumbnailChannelPathWithoutFilename);

      File.Copy(thumbnailChannelPath + "\\channel-personal.jpg", thumbnailChannelPathWithoutFilename + "\\" + channelGUID + ".jpg");
    }

    public PageAssetContentData GetRawContent(int userID, int folderID)
    {
      PageAssetContentData contentList = null;

      DAClient client = null;

      try
      {
        client = new DAClient();

        contentList = client.GetRawContent(userID, folderID);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return contentList;
    }

    public AssetContentProperties GetRawContentProperties(int userID, int contentID)
    {
      AssetContentProperties contentProperties = null;

      DAClient client = null;

      try
      {
        client = new DAClient();

        contentProperties = client.GetRawContentProperties(userID, contentID);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return contentProperties;
    }

    public PageSlideData GetStreamSlides(int userID, int folderID)
    {
      PageSlideData pageSlideData = null;

      DAClient client = null;

      try
      {
        client = new DAClient();

        pageSlideData = client.GetStreamSlides(userID, folderID);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return pageSlideData;
    }

    public PageSlideData GetUserStreams(int userID, int folderID)
    {
      PageSlideData slideList = null;

      DAClient client = null;

      try
      {
        client = new DAClient();

        slideList = client.GetUserStreams(userID, folderID);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return slideList;
    }

    public List<List<CreateContentGenericFolder>> GetFolders(int userID)
    {
      List<List<CreateContentGenericFolder>> folderList = null;

      DAClient client = null;

      try
      {
        client = new DAClient();

        folderList = client.GetFolders(userID);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return folderList;
    }

    public void PutRawContentProperties(int userID, int contentID, string title, string creator,
      string caption, DateTime? date, string url, float displayDuration)
    {
      AssetContentProperties content = new AssetContentProperties();

      content.AssetContentID = contentID;
      content.Name = title;
      content.Creator = creator;
      content.Caption = caption;
      content.UserGivenDate = date;
      content.URL = url;
      content.DisplayDuration = displayDuration;

      DAClient client = null;

      try
      {
        client = new DAClient();

        client.PutRawContentProperties(userID, content);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    public ChannelProperties GetChannelProperties(int userID, int streamID)
    {
      ChannelProperties contentStream;
      
      DAClient client = null;
     
      try
      {
        client = new DAClient();

        contentStream = client.GetChannelProperties(userID, streamID);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return contentStream;
    }

    public void AddChannelContent(int userID, int channelID, string url, float duration, string startDate, string endDate,
      string startTime, string endTime, string dayOfWeekCode, List<int> slideIDList)
    {
      string slideThumbnailWithPartialPath;
      string channelGUID;
      string newThumbnailGUID = System.Guid.NewGuid().ToString();

      DAClient client = null;

      //using (TransactionScope ts = new TransactionScope())
      //{
        try
        {
          client = new DAClient();

          string schedule;
          string presentationSchedule;

          GetSimpleSchedule(startDate, endDate, startTime, endTime, dayOfWeekCode, out schedule, out presentationSchedule);

          client.AddChannelContent(userID, channelID, slideIDList, schedule, presentationSchedule, url, duration, newThumbnailGUID, out slideThumbnailWithPartialPath, out channelGUID);

          if (slideThumbnailWithPartialPath != null) // non-default thumbnail already exists
          {
            string thumbnailSlidePath = (string)System.Configuration.ConfigurationSettings.AppSettings["thumbnailSlidePath"];
            string thumbnailChannelPath = (string)System.Configuration.ConfigurationSettings.AppSettings["thumbnailChannelPath"];
            string channelSubDir = FilenameMakerLib.FilenameFromGUID.GetGUIDSuffix(channelGUID);
            string thumbnailChannelPathWithoutFilename = thumbnailChannelPath + channelSubDir;

            // don't check and create channel thumbnail directory; assume it exists as channel already has default thumbnail
            File.Copy(thumbnailSlidePath + slideThumbnailWithPartialPath, thumbnailChannelPathWithoutFilename + "\\" + newThumbnailGUID + ".jpg", true);

            // Delete default thumbnail
            File.Delete(thumbnailChannelPathWithoutFilename + "\\" + channelGUID + ".jpg");
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.ToString());
          throw ex;
        }
        finally
        {
          if (client != null)
            client.Dispose();
        }

      //  ts.Complete();
      //}
    }

    private void GetSimpleSchedule(string startDate, string endDate, string startTime, string endTime, string dayOfWeekCode, 
      out string schedule, out string presentationSchedule)
    {
      if (string.IsNullOrEmpty(startDate) &&
                  string.IsNullOrEmpty(endDate) &&
                  string.IsNullOrEmpty(startTime) &&
                  string.IsNullOrEmpty(endTime) &&
                  dayOfWeekCode == "0000000")
      {
        schedule = GetDefaultSchedule();
        presentationSchedule = GetDefaultPresentationSchedule();

        return;
      }

      schedule = GetSimpleDBString(startDate, endDate, startTime, endTime, dayOfWeekCode);

      presentationSchedule = GetSimplePresentationString(startDate, endDate, startTime, endTime, dayOfWeekCode);
    }

    private string GetSimpleDBString(string startDate, string endDate, string startTime, string endTime, string dayOfWeekCode)
    {
      HashSet<string> daysOfWeek = ConvertDaysOfWeekToScheduleSyntax(dayOfWeekCode);

      if (daysOfWeek.Count == 0)
        return "";

      StringBuilder sb = new StringBuilder();

      foreach (string dayOfWeek in daysOfWeek)
      {
        if (!string.IsNullOrEmpty(startDate))
        {
          sb.Append("date >= ");
          sb.Append(startDate);
        }

        if (!string.IsNullOrEmpty(endDate))
        {
          sb.Append(" and date <= ");
          sb.Append(endDate);
        }

        if (!string.IsNullOrEmpty(startTime))
        {
          sb.Append(" and time >= ");
          sb.Append(startTime.Replace(":", ""));
        }

        if (!string.IsNullOrEmpty(endTime))
        {
          sb.Append(" and time <= ");
          sb.Append(endTime.Replace(":", ""));
        }

        sb.Append(dayOfWeek);
        sb.Append(" || ");
      }

      sb.Remove(sb.Length - 4, 4);

      return sb.ToString();
    }


    private string GetSimplePresentationString(string startDate, string endDate, string startTime, string endTime, string dayOfWeekCode)
    {
      StringBuilder sb = new StringBuilder();

      sb.Append("[\"");

      foreach (char c in dayOfWeekCode)
      {
        sb.Append(c);
        sb.Append("\",\"");
      }

      sb.Remove(sb.Length - 2, 2);

      sb.Append("],[\"");

      if (!string.IsNullOrEmpty(startDate))
        sb.Append(startDate);

      sb.Append("\",\"");

      if (!string.IsNullOrEmpty(endDate))
        sb.Append(endDate);

      sb.Append("\",\"");

      if (!string.IsNullOrEmpty(startTime))
        sb.Append(startTime);

      sb.Append("\",\"");

      if (!string.IsNullOrEmpty(endTime))
        sb.Append(endTime);

      sb.Append("\"]");

      return sb.ToString();
    }

    private string GetDefaultSchedule()
    {
      return "time >= 0000";
    }

    private string GetDefaultPresentationSchedule()
    {
      StringBuilder sb = new StringBuilder();
      DateTime now = DateTime.Now;

      sb.Append("[\"1\",\"1\",\"1\",\"1\",\"1\",\"1\",\"1\"],[\"");
      sb.Append(now.ToShortDateString());
      sb.Append("\",\"");
      sb.Append(now.AddYears(2).ToShortDateString());
      sb.Append("\",\"00:00\",\"23:59\"]");

      return sb.ToString();
    }

    public bool AddAssetContent(int userID, int folderID, List<AssetContent> assetContents)
    {
      DAClient client = null;

      // check if display durations of assets are within acceptable limits.
      bool bDurationsAmended = AmendDisplayDurationsIfNecessary(assetContents);

      //using (TransactionScope ts = new TransactionScope())
      //{
        try
        {
          client = new DAClient();

          client.AddAssetContent(userID, folderID, assetContents);          
        }
        finally
        {
          if (client != null)
            client.Dispose();
        }

        ExtractThumbnails(assetContents);

        //ts.Complete();
      //}

        return bDurationsAmended;
    }

    private bool AmendDisplayDurationsIfNecessary(List<AssetContent> assetContents)
    {
      bool bDurationsAmended = false;

      float displayDurationFlashMin = float.Parse(System.Configuration.ConfigurationSettings.AppSettings["displayDurationFlashMin"]);
      float displayDurationFlashMax = float.Parse(System.Configuration.ConfigurationSettings.AppSettings["displayDurationFlashMax"]);
      float displayDurationVideoMin = float.Parse(System.Configuration.ConfigurationSettings.AppSettings["displayDurationVideoMin"]);
      float displayDurationVideoMax = float.Parse(System.Configuration.ConfigurationSettings.AppSettings["displayDurationVideoMax"]);
      float displayDurationImageMin = float.Parse(System.Configuration.ConfigurationSettings.AppSettings["displayDurationImageMin"]);
      float displayDurationImageMax = float.Parse(System.Configuration.ConfigurationSettings.AppSettings["displayDurationImageMax"]);
     
      foreach (AssetContent content in assetContents)
      {
        switch (content.Extension)
        {
          case ".swf":
            CheckDurationBoundaries(ref bDurationsAmended, content, displayDurationFlashMin, displayDurationFlashMax);
            break;
          case ".jpeg":
            goto case ".jpg";
          case ".jpe":
            goto case ".jpg";
          case ".png":
             goto case ".jpg";
          case ".bmp":
             goto case ".jpg";
          case ".tiff":
            goto case ".jpg";
          case ".tif":
            goto case ".jpg";
          case ".jpg":
            CheckDurationBoundaries(ref bDurationsAmended, content, displayDurationImageMin, displayDurationImageMax);
            break;
          case ".mov":
            goto case ".wmv";
          case ".avi":
            goto case ".wmv";
          case ".mp4":
            goto case ".wmv";
          case ".wmv":
            CheckDurationBoundaries(ref bDurationsAmended, content, displayDurationVideoMin, displayDurationVideoMax);
            break;
        }
      }

      return bDurationsAmended;
    }

    private void CheckDurationBoundaries(ref bool bDurationsAmended, AssetContent content, float displayDurationMin, float displayDurationMax)
    {
      if (content.DisplayDuration != -1 && (content.DisplayDuration > displayDurationMax || content.DisplayDuration < displayDurationMin))
      {
        content.DisplayDuration = displayDurationMax;
        bDurationsAmended = true;
      }
    }

    private void ExtractThumbnails(List<AssetContent> assetContents)
    {
      List<Content> contents = new List<Content>();

      foreach (AssetContent assetContent in assetContents)
        contents.Add(assetContent);

      ExtractThumbnails(contents, MediaType.RawContent);
    }

    private void ExtractThumbnails(List<SlideContent> slideContents)
    {
      List<Content> contents = new List<Content>();

      foreach (SlideContent slideContent in slideContents)
        contents.Add(slideContent);

      ExtractThumbnails(contents, MediaType.Slide);
    }

    private void ExtractThumbnails(List<Content> contents, MediaType mediaType)
    {
      ImageExtractorThread thread = new ImageExtractorThread(contents, mediaType);
      thread.Run();      
    }

    public void AddSlideContent(int userID, int folderID, List<int> contentIDList)
    {
      List<SlideContent> slideContents = null;

      DAClient client = null;

      //using (TransactionScope tx = new TransactionScope())
      //{
        try
        {
          client = new DAClient();

          slideContents = client.AddSlideContent(userID, folderID, contentIDList);
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.ToString());
          throw ex;
        }
        finally
        {
          if (client != null)
            client.Dispose();
        }

        // copy from raw content to slide
        string assetContentPath = (string)System.Configuration.ConfigurationSettings.AppSettings["assetContentPath"];
        string thumbnailAssetContentPath = (string)System.Configuration.ConfigurationSettings.AppSettings["thumbnailAssetContentPath"];
        string slidePath = (string)System.Configuration.ConfigurationSettings.AppSettings["slidePath"];
        string thumbnailSlidePath = (string)System.Configuration.ConfigurationSettings.AppSettings["thumbnailSlidePath"];

        try
        {
          foreach (SlideContent content in slideContents)
          {
            string slidePathWithoutFilename = slidePath + content.SubDir;
            string thumbnailSlidePathWithoutFilename = thumbnailSlidePath + content.SubDir;

            // Create directories for slide file and thumbnail
            if (!Directory.Exists(slidePathWithoutFilename))
              Directory.CreateDirectory(slidePathWithoutFilename);

            if (!Directory.Exists(thumbnailSlidePathWithoutFilename))
              Directory.CreateDirectory(thumbnailSlidePathWithoutFilename);

            File.Copy(assetContentPath + content.CorrespondingAssetContentFilenameWithPath, slidePathWithoutFilename + "\\" + content.FilenameNoPath);
            File.Copy(thumbnailAssetContentPath + content.AssetImagePathWinFS, thumbnailSlidePathWithoutFilename + "\\" + content.ImageName);

            ExtractThumbnails(slideContents);
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex);
          throw ex;
        }

      //  tx.Complete();
      //}
    }

    public void RemoveChannelContent(int userID, int folderID, List<int> contentIDList)
    {
      DAClient client = null;

      try
      {
        client = new DAClient();

        client.RemoveChannelContent(userID, folderID, contentIDList);
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    public bool RemoveSlideContent(int userID, int slideFolderID, int slideID)
    {
      RemovableContent content = null;

      DAClient client = null;

      using (TransactionScope tx = new TransactionScope())
      {
        try
        {
          client = new DAClient();

          content = client.RemoveSlideContent(userID, slideFolderID, slideID);
        }
        finally
        {
          if (client != null)
            client.Dispose();
        }

        // if content isn't scheduled in a stream content is not null, delete file
        if (content != null)
        {
          string slidePath = (string)System.Configuration.ConfigurationSettings.AppSettings["slidePath"];
          string thumbnailSlidePath = (string)System.Configuration.ConfigurationSettings.AppSettings["thumbnailSlidePath"];

          DeleteFile(slidePath + content.FileName);
          DeleteFile(thumbnailSlidePath + content.ImagePathWinFS);
        }

        tx.Complete();
      }

      if (content == null)
        return false;

      return true;
    }

    public void AddPCStream(int userID, int pcID, List<int> contentIDList)
    {
      DAClient client = null;

      try
      {
        client = new DAClient();

        if (userID != -1)
          client.AddPCStream(userID, pcID, contentIDList);
        else
          client.AddPCStreamNotRegistered(pcID, contentIDList);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    public void RemovePCStream(int userID, int pcID, List<int> contentIDList)
    {
      DAClient client = null;

      try
      {
        client = new DAClient();

        if (userID != -1)
          client.RemovePCStreamRegistered(userID, pcID, contentIDList);
        else
          client.RemovePCStreamUnregistered(pcID, contentIDList);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    public int AddAssetContentFolder(int userID, string folderName)
    {
      int folderID;

      DAClient client = null;

      try
      {
        client = new DAClient();

        folderID = client.AddAssetContentFolder(userID, folderName);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return folderID;
    }

    public string EditAssetContentFolder(int userID, int folderID, string folderName)
    {
      DAClient client = null;

      try
      {
        client = new DAClient();

        client.EditAssetContentFolder(userID, folderID, folderName);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return "1";
    }

    public int RemoveAssetContentFolder(int userID, int folderID)
    {
      DAClient client = null;
      int filesLength;
      HashSet<RemovableContent> removables = null;

      try
      {
        client = new DAClient();

        removables = client.RemoveAssetContentFolder(userID, folderID, out filesLength);

        foreach (RemovableContent removableContent in removables)
        {
          DeleteFile((string)System.Configuration.ConfigurationSettings.AppSettings["assetContentPath"] + removableContent.FileName);
          DeleteFile((string)System.Configuration.ConfigurationSettings.AppSettings["thumbnailAssetContentPath"] + removableContent.ImagePathWinFS);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return filesLength;
    }

    public List<ChannelSimple> GetChannelMostPopular()
    {
      DAClient client = null;
      List<ChannelSimple> channelMostPopular;

      try
      {
        client = new DAClient();

        channelMostPopular = client.GetChannelMostPopular();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return channelMostPopular;
    }

    public List<ChannelSimple> GetChannelListByLetter(string activeLetter, SortChannelsBy sort)
    {
      DAClient client = null;
      List<ChannelSimple> channels;

      try
      {
        client = new DAClient();

        channels = client.GetChannelListByLetter(activeLetter, sort);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return channels;
    }

    public int AddPC(int userID, string pcName)
    {
      int pcID = -1;

      DAClient client = null;

      try
      {
        client = new DAClient();

        pcID = client.AddPC(userID, pcName);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return pcID;
    }

    public void RenamePC(int userID, int pcID, string pcName)
    {
      DAClient client = null;

      try
      {
        client = new DAClient();

        client.RenamePC(userID, pcID, pcName);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    public void EditChannelWeighting(int userID, int pcID, int channelID, int channelWeighting)
    {
      DAClient client = null;

      try
      {
        client = new DAClient();

        client.EditChannelWeighting(userID, pcID, channelID, channelWeighting);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    public Channel GetChannelDetailsFull(int userID, int channelID)
    {
      Channel channel = null;
      DAClient client = null;

      try
      {
        client = new DAClient();

        channel = client.GetChannelDetailsFull(userID, channelID);
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return channel;
    }

    public List<List<ChannelListChannel>> GetPCStreamsAll(int userID)
    {
      List<List<ChannelListChannel>> pcs = null;
      DAClient client = null;

      try
      {
        client = new DAClient();

        pcs = client.GetPCStreamsAll(userID);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return pcs;
    }

    public List<Channel> GetTop5MostPopular(int categoryID)
    {
      List<Channel> channels = null;
      DAClient client = null;

      try
      {
        client = new DAClient();

        channels = client.GetTop5MostPopular(categoryID);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return channels;
    }

    public string GetCategoryName(int categoryID)
    {
      string categoryName = null;

      DAClient client = null;

      try
      {
        client = new DAClient();

        categoryName = client.GetCategoryName(categoryID);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return categoryName;
    }

    public List<List<AssetContentListContent>> GetAssetContentAll(int userID)
    {
      List<List<AssetContentListContent>> assetContentFolders = null;

      DAClient client = null;

      try
      {
        client = new DAClient();

        assetContentFolders = client.GetAssetContentAll(userID);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return assetContentFolders;
    }

    public List<List<SlideListSlide>> GetSlideFoldersAll(int userID)
    {
      List<List<SlideListSlide>> slideFolders = null;

      DAClient client = null;

      try
      {
        client = new DAClient();

        slideFolders = client.GetSlideFoldersAll(userID);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return slideFolders;
    }

    public bool UnlockStream(int userID, int channelID, string channelPassword)
    {
      bool bChannelPasswordCorrect = false;

      DAClient client = null;

      try
      {
        client = new DAClient();

        bChannelPasswordCorrect = client.UnlockStream(userID, channelID, channelPassword);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return bChannelPasswordCorrect;
    }

    public int RemoveAssetContent(int userID, int assetContentFolderID, int assetContentID)
    {
      DAClient client = null;
      int fileLength = -1;

      using (TransactionScope tx = new TransactionScope())
      {
        try
        {
          client = new DAClient();

          RemovableContent removableFile = client.RemoveAssetContent(userID, assetContentFolderID, assetContentID);

          // removableFile will be null if user hadn't had access
          if (removableFile != null)
          {
            DeleteFile((string)System.Configuration.ConfigurationSettings.AppSettings["assetContentPath"] + removableFile.FileName);
            DeleteFile((string)System.Configuration.ConfigurationSettings.AppSettings["thumbnailAssetContentPath"] + removableFile.ImagePathWinFS);

            fileLength = removableFile.FileLength;
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.ToString());

          throw ex;
        }
        finally
        {
          if (client != null)
            client.Dispose();
        }

        tx.Complete();
      }

      return fileLength;
    }

    private void DeleteFile(string filename)
    {
      try
      {
        File.Delete(filename);
      }
      catch
      {
        // ignore
      }
    }

    public int AddSlideFolder(int userID, string folderName)
    {
      int slideFolderID = -1;

      DAClient client = null;

      try
      {
        client = new DAClient();

        slideFolderID = client.AddSlideFolder(userID, folderName);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return slideFolderID;
    }

    public string EditSlideFolder(int userID, int folderID, string folderName)
    {
      DAClient client = null;

      try
      {
        client = new DAClient();

        client.EditSlideFolder(userID, folderID, folderName);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return "1";
    }

    public bool RemoveSlideFolder(int userID, int folderID)
    {
      DAClient client = null;
      HashSet<RemovableContent> removables = null;

      try
      {
        client = new DAClient();

        removables = client.RemoveSlideFolder(userID, folderID);

        if (removables == null)
        {
          Console.WriteLine("Can't remove");
          return false;
        }

        foreach (RemovableContent removableContent in removables)
        {
          DeleteFile((string)System.Configuration.ConfigurationSettings.AppSettings["slidePath"] + removableContent.FileName);
          DeleteFile((string)System.Configuration.ConfigurationSettings.AppSettings["thumbnailSlidePath"] + removableContent.ImagePathWinFS);
        }

        Console.WriteLine("Can remove");

        return true;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        Console.WriteLine("In Finally");
        if (client != null)
          client.Dispose();
      }
    }

    public void AddChannel(int userID, int? categoryID, string channelName, string description,
      string longDescription, string keywords, bool bLocked, string password, bool bAcceptPasswordRequests)
    {
      DAClient client = null;
      string channelGUID = null;

      try
      {
        client = new DAClient();

        channelGUID = client.AddChannel(userID, categoryID, channelName, description, longDescription, keywords,
          bLocked, password, bAcceptPasswordRequests);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      if (channelGUID != null)
      {
        try
        {
          CreateDefaultThumbnailForChannel(channelGUID);
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.ToString());

          throw ex;
        }
      }
    }

    public void EditChannel(int userID, int channelID, int? categoryID, string channelName, string description,
      string longDescription, string keywords, bool bLocked, string password, bool bAcceptPasswordRequests,
      ChannelPrivacyOptions options)
    {
      DAClient client = null;

      try
      {
        client = new DAClient();

        client.EditChannel(userID, channelID, categoryID, channelName, description, longDescription, keywords,
          bLocked, password, bAcceptPasswordRequests, options);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    public void RemoveChannel(int userID, int channelID)
    {
      DAClient client = null;
      string channelThumbnailWithPartialPath = null;

      //using (TransactionScope ts = new TransactionScope())
      //{
        try
        {
          client = new DAClient();

          channelThumbnailWithPartialPath = client.RemoveChannel(userID, channelID);

          if (channelThumbnailWithPartialPath != null)
          {
            string thumbnailChannelPath = (string)System.Configuration.ConfigurationSettings.AppSettings["thumbnailChannelPath"];

            File.Delete(thumbnailChannelPath + channelThumbnailWithPartialPath.Replace("/", "\\"));
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex);
          throw ex;
        }
        finally
        {
          if (client != null)
            client.Dispose();
        }

      //  ts.Complete();
      //}
    }

    public void EditChannelThumbnail(int userID, int channelID, string slideImagePath)
    {
      string thumbnailSlidePath = (string)System.Configuration.ConfigurationSettings.AppSettings["thumbnailSlidePath"];
      string thumbnailChannelPath = (string)System.Configuration.ConfigurationSettings.AppSettings["thumbnailChannelPath"];

      string thumbnailSlidePathWithFilename = thumbnailSlidePath + slideImagePath.Replace("/", "\\");
      string thumbnailChannelPathWithoutFilename;

      DAClient client = null;
      string channelGUIDSuffix;
      string oldChannelThumbnailPath;

      // need to create a thumbnail with a different name and delete the old one as keeping the same filename causes browser caching problems.
      string thumbnailGUID = System.Guid.NewGuid().ToString();

      using (TransactionScope ts = new TransactionScope())
      {
        try
        {
          client = new DAClient();

          client.EditThumbnailGUID(userID, channelID, thumbnailGUID, out channelGUIDSuffix, out oldChannelThumbnailPath);

          thumbnailChannelPathWithoutFilename = thumbnailChannelPath + channelGUIDSuffix;

          // don't check and create channel thumbnail directory; assume it exists as channel already has thumbnail
          File.Copy(thumbnailSlidePathWithFilename, thumbnailChannelPathWithoutFilename + "\\" + thumbnailGUID + ".jpg", true); // thumbnails generated from aurigma are always jpg

          // delete old thumbnail
          File.Delete(thumbnailChannelPath + "\\" + oldChannelThumbnailPath.Replace("/", "\\"));
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex);
          throw ex;
        }
        finally
        {
          if (client != null)
            client.Dispose();
        }

        ts.Complete();
      }
    }

    public void EditSlideContentProperties(int userID, int slideID, string title, string creator, string caption,
      DateTime? date, string URL, float displayDuration)
    {
      SlideProperties slide = new SlideProperties();

      slide.SlideID = slideID;
      slide.Name = title;
      slide.Creator = creator;
      slide.Caption = caption;
      slide.UserGivenDate = date;
      slide.URL = URL;
      slide.DisplayDuration = displayDuration;

      DAClient client = null;

      try
      {
        client = new DAClient();

        client.EditSlideContentProperties(userID, slide);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    public SlideProperties GetSlideProperties(int userID, int slideID)
    {
      DAClient client = null;
      SlideProperties slideProperties = null;

      try
      {
        client = new DAClient();

        slideProperties = client.GetSlideProperties(userID, slideID);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return slideProperties;
    }

    public void MoveRawContent(int userID, int oldFolderID, int newFolderID, List<int> contentIDList)
    {
      DAClient client = null;

      try
      {
        client = new DAClient();

        client.MoveRawContent(userID, oldFolderID, newFolderID, contentIDList);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    public void MoveSlideContent(int userID, int oldFolderID, int newFolderID, List<int> slideIDList)
    {
      DAClient client = null;

      try
      {
        client = new DAClient();

        client.MoveSlideContent(userID, oldFolderID, newFolderID, slideIDList);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    public void MovePCChannels(int userID, int oldPCID, int newPCID, List<int> channelIDList)
    {
      DAClient client = null;

      try
      {
        client = new DAClient();

        client.MovePCChannels(userID, oldPCID, newPCID, channelIDList);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    public void EditChannelSlideProperties(int userID, int channelSlideID, string url, float displayDuration, string dayOfWeekCode, string[] startEndDateTimes)
    {
      string scheduleString = GetScheduleString(dayOfWeekCode, startEndDateTimes);

      string presentationString = GetPresentationString(dayOfWeekCode, startEndDateTimes);

      DAClient client = null;

      try
      {
        client = new DAClient();

        client.EditChannelSlideProperties(userID, channelSlideID, url, displayDuration, scheduleString, presentationString);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    private string GetScheduleString(string dayOfWeekCode, string[] startEndDateTimes)
    {
      HashSet<string> daysOfWeek = ConvertDaysOfWeekToScheduleSyntax(dayOfWeekCode);

      return ConvertStartEndDateTimesToScheduleSyntax(startEndDateTimes, daysOfWeek); 
    }

    private HashSet<string> ConvertDaysOfWeekToScheduleSyntax(string dayOfWeekCode)
    {
      Dictionary<int, string> daysOfWeek = GetDaysOfWeek();

      HashSet<string> daysOfWeekSchedule = new HashSet<string>();

      for (int i = 0; i < 7; i++)
      {
        if (dayOfWeekCode[i] == '1')
          daysOfWeekSchedule.Add(" and dayofweek = " + daysOfWeek[i]);
      }

      return daysOfWeekSchedule;
    }

    private string ConvertStartEndDateTimesToScheduleSyntax(string[] startEndDateTimes, HashSet<string> daysOfWeek)
    {
      if (daysOfWeek.Count == 0)
        return "";

      StringBuilder sb = new StringBuilder();

      if (startEndDateTimes == null)
      {
        foreach (string dayOfWeek in daysOfWeek)
        {
          sb.Append(dayOfWeek);
          sb.Append("||");
        }

        sb.Remove(sb.Length - 2, 2);

        return sb.ToString();
      }

      foreach (string dayOfWeek in daysOfWeek)
      {
        foreach (string startEndDateTime in startEndDateTimes)
        {
          string[] datesTimes = startEndDateTime.Split(new string[] { "||" }, StringSplitOptions.None);

          if (!string.IsNullOrEmpty(datesTimes[0]))
          {
            sb.Append("date >= ");
            sb.Append(datesTimes[0]);
          }

          if (!string.IsNullOrEmpty(datesTimes[1]))
          {
            sb.Append(" and date <= ");
            sb.Append(datesTimes[1]);
          }

          if (!string.IsNullOrEmpty(datesTimes[2]))
          {
            sb.Append(" and time >= ");
            sb.Append(datesTimes[2].Replace(":", ""));
          }

          if (!string.IsNullOrEmpty(datesTimes[3]))
          {
            sb.Append(" and time <= ");
            sb.Append(datesTimes[3].Replace(":", ""));
          }

          sb.Append(dayOfWeek);
          sb.Append(" || ");
        }
      }

      sb.Remove(sb.Length - 4, 4);

      return sb.ToString();
    }

    private Dictionary<int, string> GetDaysOfWeek()
    {
      Dictionary<int, string> dict = new Dictionary<int, string>();

      dict.Add(0, "monday");
      dict.Add(1, "tuesday");
      dict.Add(2, "wednesday");
      dict.Add(3, "thursday");
      dict.Add(4, "friday");
      dict.Add(5, "saturday");
      dict.Add(6, "sunday");

      return dict;
    }

    private string GetPresentationString(string dayOfWeekCode, string[] startEndDateTimes)
    {
      StringBuilder sb = new StringBuilder();

      sb.Append("[");

      foreach (char c in dayOfWeekCode)
      {
        sb.Append("\"");
        sb.Append(c);
        sb.Append("\",");
      }

      sb.Remove(sb.Length - 1, 1);
      sb.Append("]");

      if (startEndDateTimes != null)
      {
        foreach (string startEndDateTime in startEndDateTimes)
        {
          string[] datesTimes = startEndDateTime.Split(new string[] { "||" }, StringSplitOptions.None);

          sb.Append(",[\"");

          if (!string.IsNullOrEmpty(datesTimes[0]))
            sb.Append(datesTimes[0]);

          sb.Append("\",\"");

          if (!string.IsNullOrEmpty(datesTimes[1]))
            sb.Append(datesTimes[1]);

          sb.Append("\",\"");

          if (!string.IsNullOrEmpty(datesTimes[2]))
            sb.Append(datesTimes[2]);

          sb.Append("\",\"");

          if (!string.IsNullOrEmpty(datesTimes[3]))
            sb.Append(datesTimes[3]);
  
          sb.Append("\"]");
        }
      }

      return sb.ToString();
    }

    public ChannelSlideProperties GetChannelSlideProperties(int userID, int channelsSlideID)
    {
      DAClient client = null;

      ChannelSlideProperties channelSlideProperties = null;

      try
      {
        client = new DAClient();

        channelSlideProperties = client.GetChannelSlideProperties(userID, channelsSlideID);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return channelSlideProperties;
    }

    public void SetCategory(int userID, int channelID, int categoryID)
    {
      DAClient client = null;

      try
      {
        client = new DAClient();

        client.SetCategory(userID, channelID, categoryID);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    public void GetPreviewFrames(int userID, string mediaType, int mediaID, out string subDir, out string[] files, out PreviewType previewType)
    {
      DAClient client = null;

      string mediaGUID;
      string fileNameWithSubdir;
      string pathToFrames;
      string pathToFile;

      switch (mediaType)
      {
        case "R":
          pathToFrames = System.Configuration.ConfigurationSettings.AppSettings["previewFramesAssetContentPath"];
          pathToFile = System.Configuration.ConfigurationSettings.AppSettings["assetContentPath"];
          break;
        case "S":
          goto case "C";
        case "C":
          pathToFrames = System.Configuration.ConfigurationSettings.AppSettings["previewFramesSlidePath"];
          pathToFile = System.Configuration.ConfigurationSettings.AppSettings["slidePath"];
          break;
        default:
          throw new ArgumentException("Invalid Media Type");
      }

      try
      {
        client = new DAClient();

        client.GetDataForPreviewFrames(userID, mediaType, mediaID, out subDir, out mediaGUID, 
          out fileNameWithSubdir, out previewType);
      }
      catch (Exception ex)
      {
        _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      if (previewType == PreviewType.Video)
      {
        string[] filenamesWithPath = Directory.GetFiles(pathToFrames + subDir, mediaGUID + "*");
        files = null;

        int length = filenamesWithPath.Length;

        if (length > 0)
        {
          files = new string[length];

          for (int i = 0; i < length; i++)
            files[i] = Path.GetFileName(filenamesWithPath[i]);
        }

        return;
      }

      files = new string[] { Path.GetFileName(fileNameWithSubdir) };      
    }

    public List<PC> GetSubscriptionsNotRegistered(string PcProfileToken)
    {
      DAClient client = null;

      try
      {
        client = new DAClient();

        return client.GetSubscriptionsNotRegistered(PcProfileToken);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    public List<PC> GetSubscriptions(int userID)
    {
      DAClient client = null;

      try
      {
        client = new DAClient();

        return client.GetSubscriptions(userID);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    public List<Channel> GetChannelsByUserID(int userID)
    {
      DAClient client = null;

      try
      {
        client = new DAClient();

        return client.GetChannelsByUserID(userID);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    public Channel GetChannelToDownload(int userID, int channelID)
    {
      DAClient client = null;

      try
      {
        client = new DAClient();

        return client.GetChannelToDownload(userID, channelID);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    public void RemoveTemporaryPCProfilesNotRegistered(string pcProfileToken)
    {
      DAClient client = null;

      try
      {
        client = new DAClient();

        client.RemoveTemporaryPCProfilesNotRegistered(pcProfileToken);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    public void RemoveTemporaryPCProfiles(int userID)
    {
      DAClient client = null;

      try
      {
        client = new DAClient();

        client.RemoveTemporaryPCProfiles(userID);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    public void SendPasswordReminder(string email)
    {
      DAClient client = null;
      string newPassword = Path.GetRandomFileName().Replace(".", "");;

      try
      {
        client = new DAClient();

        client.SetPassword(email, newPassword);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      // send reminder e-mail
      PasswordEmailSender emailSender = new PasswordEmailSender(email, newPassword);

      try
      {
        emailSender.SendPasswordReminderEmail();
      }
      catch (Exception ex)
      {
        _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
      }
    }

    public List<LocationNameValue> GetCountries()
    {
      DAClient client = null;

      try
      {
        client = new DAClient();

        return client.GetCountries();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    public UserDetails GetUserDetails(int userID)
    {
      DAClient client = null;

      try
      {
        client = new DAClient();

        return client.GetUserDetails(userID);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    public List<LocationNameValue> GetChildGeoTTNodes(int parentLocationID)
    {
      DAClient client = null;

      try
      {
        client = new DAClient();

        return client.GetChildGeoTTNodes(parentLocationID);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    public List<KeyValuePair<int, string>> GetSocioEconomicStatuses(string seType)
    {
      DAClient client = null;

      try
      {
        client = new DAClient();

        return client.GetSocioEconomicStatuses(seType);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    public void SendPasswordRequest(int channelID, string name, string emailAddress, string message)
    {
      string channelCreatorEmailAddress = null;
      string channelCreatorName = null;
      string channelName = null;

      DAClient client = null;

      try
      {
        client = new DAClient();

        client.GetChannelCreatorDetailsForPasswordRequest(channelID, out channelCreatorEmailAddress, out channelCreatorName,
          out channelName);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      PasswordRequestSender emailSender = new PasswordRequestSender(emailAddress,
        channelCreatorName,
        channelCreatorEmailAddress,
        name,
        message,
        channelName);

      try
      {
        emailSender.SendEmail();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
       // _eventLog.WriteEntry(ex.ToString());
      }
    }

    public bool EditUserDetails(int userID, string emailAddress, string password, string firstName, string lastName,
      string gender, DateTime dob, int townCityID, int occupationSectorID, int employmentLevelID,
        int annualHouseholdIncomeID)
    {
      DAClient client = null;

      bool bUpdatePassword = true;

      if (string.IsNullOrEmpty(password))
        bUpdatePassword = false;

      try
      {
        client = new DAClient();

        return client.EditUserDetails(userID, emailAddress, password, firstName, lastName, 
          gender, dob, townCityID, occupationSectorID, employmentLevelID, annualHouseholdIncomeID, bUpdatePassword);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }
    }

    public bool RemoveUserAccount(int userID)
    {
      DAClient client = null;

      string assetContentPath = (string)System.Configuration.ConfigurationSettings.AppSettings["assetContentPath"];
      string thumbnailAssetContentPath = (string)System.Configuration.ConfigurationSettings.AppSettings["thumbnailAssetContentPath"];
      string previewFramesAssetContentPath = (string)System.Configuration.ConfigurationSettings.AppSettings["previewFramesAssetContentPath"];
      string previewFramesSlidePath = (string)System.Configuration.ConfigurationSettings.AppSettings["previewFramesSlidePath"];
      string slidePath = (string)System.Configuration.ConfigurationSettings.AppSettings["slidePath"];
      string thumbnailSlidePath = (string)System.Configuration.ConfigurationSettings.AppSettings["thumbnailSlidePath"];
      string thumbnailChannelPath = (string)System.Configuration.ConfigurationSettings.AppSettings["thumbnailChannelPath"];

      bool bHasLinkedPCs = false;

      HashSet<AssetContent> assetContentFiles = null;
      HashSet<SlideContent> slideFiles = null;
      HashSet<string> channelThumbnails = null;

      using (TransactionScope ts = new TransactionScope())
      {
        try
        {
          client = new DAClient();

          client.GetUserAccountAssets(userID, out bHasLinkedPCs, out assetContentFiles,
            out slideFiles, out channelThumbnails);

          if (bHasLinkedPCs)
            return true;

          // file information retrieved and is in memory. Now clear the database
          client.RemoveUserAccount(userID);

          // Raw content
          foreach (AssetContent ac in assetContentFiles)
          {
            // file
            try
            {
              File.Delete(assetContentPath + ac.FileName);
            }
            catch (Exception ex)
            {
              _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
            }

            // thumbnail
            try
            {
              File.Delete(thumbnailAssetContentPath + ac.ImagePathWinFS);
            }
            catch (Exception ex)
            {
              _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
            }

            // preview frames
            string[] previewFrames = Directory.GetFiles(previewFramesAssetContentPath, Path.GetFileNameWithoutExtension(ac.FileNameNoPath) + "*");

            foreach (string file in previewFrames)
            {
              try
              {
                File.Delete(file);
              }
              catch (Exception ex)
              {
                _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
              }
            }
          }

          foreach (SlideContent sc in slideFiles)
          {
            // file
            try
            {
              File.Delete(slidePath + sc.FileName);
            }
            catch (Exception ex)
            {
              _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
            }

            // thumbnail
            try
            {
              File.Delete(thumbnailSlidePath + sc.ImagePathWinFS);
            }
            catch (Exception ex)
            {
              _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
            }

            // preview frames
            string[] previewFrames = Directory.GetFiles(previewFramesSlidePath, Path.GetFileNameWithoutExtension(sc.FileNameNoPath) + "*");

            foreach (string file in previewFrames)
            {
              try
              {
                File.Delete(file);
              }
              catch (Exception ex)
              {
                _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
              }
            }
          }

          // channels
          foreach (string thumbnail in channelThumbnails)
          {
            try
            {
              File.Delete(thumbnailChannelPath + thumbnail);
            }
            catch (Exception ex)
            {
              _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
            }
          }
        }
        catch (Exception ex)
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
          throw ex;
        }
        finally
        {
          if (client != null)
            client.Dispose();
        }

        ts.Complete();
      }

      return false;
    }

    public void SendContactEmail(string name, string emailAddress, string subject, string message)
    {
      ContactEmailSender sender = new ContactEmailSender(emailAddress, subject, name, message);

      try
      {
        sender.Send();
      }
      catch (Exception ex)
      {
        _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
      }
    }
  }
}