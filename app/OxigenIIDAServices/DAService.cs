using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.SOAStructures;
using OxigenIIAdvertising.ServiceContracts.DAServices;
using System.Transactions;
using OxigenIIAdvertising.DataAccess;
using System.Data.SqlClient;
using OxigenIIAdvertising.Demographic;
using System.ServiceModel;
using System.Diagnostics;

namespace OxigenIIAdvertising.Services
{
  public class DAService : IDAService
  {
    private EventLog _eventLog = null;

    public DAService()
    {
      _eventLog = new EventLog();
      _eventLog.Log = String.Empty;
      _eventLog.Source = "Oxigen Data Access Host";
    }

    public PageChannelData GetChannelListByCategoryID(int userID, int categoryID, int startChannelNo, int endChannelNo, 
      OxigenIIAdvertising.SOAStructures.SortChannelsBy sortBy)
    {
      List<ChannelListChannel> channelList = new List<ChannelListChannel>();

      int noPages = -1;

      SqlDatabase sqlDatabase = null;
      SqlDataReader channelReader = null;
      SqlDataReader pageReader = null;
      PageChannelData pageChannelData = null;

      string storedProcedure;

      switch (sortBy)
      {
        case SortChannelsBy.MostRecent:
          storedProcedure = "dp_getChannelListByCategoryIDMostRecent";
          break;
        case SortChannelsBy.Popularity:
          storedProcedure = "dp_getChannelListByCategoryIDPopularity";
          break;
        default:
          storedProcedure = "dp_getChannelListByCategoryIDAlpha";
          break;
      }

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);
        sqlDatabase.AddInputParameter("@CategoryID", categoryID);
        sqlDatabase.AddInputParameter("@StartChannelNo", startChannelNo);
        sqlDatabase.AddInputParameter("@EndChannelNo", endChannelNo);

        channelReader = sqlDatabase.ExecuteReader(storedProcedure);

        while (channelReader.Read())
        {
          ChannelListChannel channel = new ChannelListChannel();

          channel.ChannelID = channelReader.GetInt32(channelReader.GetOrdinal("CHANNEL_ID"));
          channel.ChannelName = channelReader.GetString(channelReader.GetOrdinal("ChannelName"));
          channel.ImagePath = channelReader.GetString(channelReader.GetOrdinal("ImagePath"));
          channel.PrivacyStatus = (ChannelPrivacyStatus)Enum.Parse(typeof(ChannelPrivacyStatus), channelReader.GetString(channelReader.GetOrdinal("PrivacyStatus")));
          channel.AcceptPasswordRequests = channelReader.GetBoolean(channelReader.GetOrdinal("bAcceptPasswordRequests"));

          channelList.Add(channel);
        }

        channelReader.Dispose();

        sqlDatabase.ClearParameters();

        sqlDatabase.AddInputParameter("@CategoryID", categoryID);

        pageReader = sqlDatabase.ExecuteReader("dp_getNoPagesPerChannelPerCategory");

        if (pageReader.Read())
          noPages = pageReader.GetInt32(pageReader.GetOrdinal("NoPages"));

        pageReader.Dispose();

        pageChannelData = new PageChannelData(noPages, channelList);
      }
      catch (Exception exception)
      {
        _eventLog.WriteEntry(exception.ToString(), EventLogEntryType.Error);
        throw exception;
      }
      finally
      {
        if (channelReader != null && !channelReader.IsClosed)
          channelReader.Dispose();

        if (pageReader != null && !pageReader.IsClosed)
          pageReader.Dispose();

        sqlDatabase.Dispose();
      }

      return pageChannelData;
    }

    public List<Category> GetCategoryList(int categoryID)
    {
      List<Category> categoryList = new List<Category>();

      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@ParentCategoryID", categoryID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getCategoryChildList");

        while (sqlDataReader.Read())
        {
          Category category = new Category();

          category.CategoryID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CATEGORY_ID"));
          category.CategoryName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("CategoryName"));
          category.HasChildren = sqlDataReader.GetBoolean(sqlDataReader.GetOrdinal("bHasChildren"));

          categoryList.Add(category);
        }
      }
      finally
      {
        if (sqlDataReader != null)
          sqlDataReader.Close();

        sqlDatabase.Dispose();
      }

      return categoryList;
    }

    public Channel GetChannelDetailsSimple(int channelID)
    {
      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;

      Channel channel = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@ChannelID", channelID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getChannelSimple");

        if (sqlDataReader.Read())
        {
          channel = new Channel();

          channel.ChannelID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CHANNEL_ID"));
          channel.ChannelDescription = GetStringValueIfNotNull(sqlDataReader, "ChannelDescription");
          channel.PublisherDisplayName = GetStringValueIfNotNull(sqlDataReader, "DisplayName");
          channel.NoContents = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("NoContent"));
          channel.NoFollowers = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("NoFollowers"));
          channel.AddDate = sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("AddDate"));
        }
      }
      catch (Exception exception)
      {
        throw new Exception("Error: Reading Channel Details", exception);
      }
      finally
      {
        if (sqlDataReader != null)
          sqlDataReader.Close();

        sqlDatabase.Dispose();
      }

      return channel;
    }

    public List<PC> GetPcListRegistered(int userID)
    {
      List<PC> pcList = new List<PC>();

      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);

        // if no "real-world-linked" pc profile, create a temp one
        sqlDatabase.ExecuteNonQuery("dp_addTempPcProfileRegistered");

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getPCList");

        while (sqlDataReader.Read())
        {
          PC pc = new PC();

          pc.PCID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("PC_ID"));
          pc.Name = sqlDataReader.GetString(sqlDataReader.GetOrdinal("PcName"));
          pc.LinkedToClient = sqlDataReader.GetBoolean(sqlDataReader.GetOrdinal("bLinkedToClient"));
          pcList.Add(pc);
        }
      }
      catch (Exception exception)
      {
        throw new DataAccessException("Error: Reading PC List", exception);
      }
      finally
      {
        if (sqlDataReader != null)
          sqlDataReader.Close();

        sqlDatabase.Dispose();
      }

      return pcList;
    }

    public List<PC> GetPcListUnregistered(string pcProfileToken)
    {
      List<PC> pcList = new List<PC>();

      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@PCProfileToken", pcProfileToken);

        // if no temporary PC profile, create one
        sqlDatabase.ExecuteNonQuery("dp_addTempPcProfileUnregistered");

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getPCListNotRegistered");

        while (sqlDataReader.Read())
        {
          PC pc = new PC();

          pc.PCID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("PC_ID"));
          pc.Name = sqlDataReader.GetString(sqlDataReader.GetOrdinal("PcName"));
          pcList.Add(pc);
        }
      }
      catch (Exception exception)
      {
        Console.WriteLine(exception.ToString());
        throw new DataAccessException("Error: Reading PC List", exception);
      }
      finally
      {
        if (sqlDataReader != null)
          sqlDataReader.Close();

        sqlDatabase.Dispose();
      }

      return pcList;
    }

    private int GetChannelIDFromSearchString(string searchString)
    {
      string[] channeIDComponents = searchString.Split(new char[] { '_' });

      if (channeIDComponents.Length != 2)
        return -1;

      int channelID;

      if (!int.TryParse(channeIDComponents[0], out channelID))
        return -1;

      // if the channelID part of the string <channelID>_strm is an int, return ut
      return channelID;
    }

    public List<ChannelListChannel> Search(int userID, string text)
    {
      bool bSearchByChannelID = false;

      List<ChannelListChannel> categoryList = new List<ChannelListChannel>();

      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;

      try
      {
        sqlDatabase = new SqlDatabase(); 
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);

        if (text.EndsWith("_strm"))
        {
          // this is allegedly a search on a stream ID: check the search string again to see if there
          // is a valid channelID as part of it.
          int channelID = GetChannelIDFromSearchString(text);

          if (channelID != -1)
          {
            bSearchByChannelID = true;
            sqlDatabase.AddInputParameter("@ChannelID", channelID);
            sqlDataReader = sqlDatabase.ExecuteReader("dp_getChannelByChannelID");
          }
        }

        if (!bSearchByChannelID)
        {
          sqlDatabase.AddInputParameter("@Text", text);
          sqlDataReader = sqlDatabase.ExecuteReader("dp_getChannelListByText");
        }

        while (sqlDataReader.Read())
        {
          ChannelListChannel channel = new ChannelListChannel();

          channel.ChannelID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CHANNEL_ID"));
          channel.ChannelName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ChannelName"));
          channel.ImagePath = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ImagePath"));
          channel.PrivacyStatus = (ChannelPrivacyStatus)Enum.Parse(typeof(ChannelPrivacyStatus), sqlDataReader.GetString(sqlDataReader.GetOrdinal("PrivacyStatus")));
          channel.AcceptPasswordRequests = sqlDataReader.GetBoolean(sqlDataReader.GetOrdinal("bAcceptPasswordRequests"));

          categoryList.Add(channel);
        }
      }
      catch (Exception exception)
      {
        throw new DataAccessException("Error: Reading Channel List", exception);
      }
      finally
      {
        if (sqlDataReader != null)
          sqlDataReader.Close();

        sqlDatabase.Dispose();
      }

      return categoryList;
    }

    public PageChannelData GetPcStreamsRegistered(int userID, int pcID)
    {
      int noPages = -1;
      PageChannelData pageChannelData = null;
      List<ChannelListChannel> channelList = new List<ChannelListChannel>();

      SqlDatabase sqlDatabase = null;
      SqlDataReader channelReader = null;
      SqlDataReader pageReader = null;

      try
      {
        sqlDatabase = new SqlDatabase(); 
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);
        sqlDatabase.AddInputParameter("@PcID", pcID);

        channelReader = sqlDatabase.ExecuteReader("dp_getChannelListByPCID");

        while (channelReader.Read())
        {
          ChannelListChannel channel = new ChannelListChannel();

          channel.ChannelID = channelReader.GetInt32(channelReader.GetOrdinal("CHANNEL_ID"));
          channel.ChannelName = channelReader.GetString(channelReader.GetOrdinal("ChannelName"));
          channel.ImagePath = channelReader.GetString(channelReader.GetOrdinal("ImagePath"));
          channel.PrivacyStatus = (ChannelPrivacyStatus)Enum.Parse(typeof(ChannelPrivacyStatus), channelReader.GetString(channelReader.GetOrdinal("PrivacyStatus")));
          channel.ChannelWeightingUnnormalised = channelReader.GetInt32(channelReader.GetOrdinal("ChannelWeighting"));
          channel.AcceptPasswordRequests = channelReader.GetBoolean(channelReader.GetOrdinal("bAcceptPasswordRequests"));

          channelList.Add(channel);
        }

        channelReader.Dispose();

        sqlDatabase.RemoveParameter("@UserID");
        
        pageReader = sqlDatabase.ExecuteReader("dp_getPcChannelPages");

        if (pageReader.Read())
          noPages = pageReader.GetInt32(pageReader.GetOrdinal("NoPages"));

        pageReader.Dispose();

        pageChannelData = new PageChannelData(noPages, channelList);
      }
      catch (Exception exception)
      {
        Console.WriteLine(exception.ToString());
        throw new DataAccessException("Error: Reading Channel List", exception);
      }
      finally
      {
        if (channelReader != null && !channelReader.IsClosed)
          channelReader.Dispose();

        if (pageReader != null && !pageReader.IsClosed)
          pageReader.Dispose();

        sqlDatabase.Dispose();
      }

      return pageChannelData;
    }

    public PageChannelData GetPcStreamsUnregistered(int pcID)
    {
      int noPages = -1;
      PageChannelData pageChannelData = null;
      List<ChannelListChannel> channelList = new List<ChannelListChannel>();

      SqlDatabase sqlDatabase = null;
      SqlDataReader channelReader = null;
      SqlDataReader pageReader = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@PcID", pcID);

        channelReader = sqlDatabase.ExecuteReader("dp_getChannelListByPCIDUnregistered");

        while (channelReader.Read())
        {
          ChannelListChannel channel = new ChannelListChannel();

          channel.ChannelID = channelReader.GetInt32(channelReader.GetOrdinal("CHANNEL_ID"));
          channel.ChannelName = channelReader.GetString(channelReader.GetOrdinal("ChannelName"));
          channel.ImagePath = channelReader.GetString(channelReader.GetOrdinal("ImagePath"));
          channel.PrivacyStatus = (ChannelPrivacyStatus)Enum.Parse(typeof(ChannelPrivacyStatus), channelReader.GetString(channelReader.GetOrdinal("PrivacyStatus")));
          channel.ChannelWeightingUnnormalised = channelReader.GetInt32(channelReader.GetOrdinal("ChannelWeighting"));
          channel.AcceptPasswordRequests = channelReader.GetBoolean(channelReader.GetOrdinal("bAcceptPasswordRequests"));

          channelList.Add(channel);
        }

        channelReader.Dispose();

        pageReader = sqlDatabase.ExecuteReader("dp_getPcChannelPages");

        if (pageReader.Read())
          noPages = pageReader.GetInt32(pageReader.GetOrdinal("NoPages"));

        pageReader.Dispose();

        pageChannelData = new PageChannelData(noPages, channelList);
      }
      catch (Exception exception)
      {
        Console.WriteLine(exception.ToString());
        throw new DataAccessException("Error: Reading Channel List", exception);
      }
      finally
      {
        if (channelReader != null && !channelReader.IsClosed)
          channelReader.Dispose();

        if (pageReader != null && !pageReader.IsClosed)
          pageReader.Dispose();

        sqlDatabase.Dispose();
      }

      return pageChannelData;
    }

    public User Login(string username, string password)
    {
      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;

      try
      {
        sqlDatabase = new SqlDatabase(); 
        sqlDatabase.Open();

        sqlDatabase.AddReturnParameter();
        sqlDatabase.AddInputParameter("@Username", username);
        sqlDatabase.AddInputParameter("@Password", password);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getUserLogin");

        if (sqlDataReader.Read())
        {
          int userID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("USER_ID"));
          string firstName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("FirstName"));
          string lastName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("LastName"));
          string emailAddress = sqlDataReader.GetString(sqlDataReader.GetOrdinal("EmailAddress"));
          long usedBytes = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("UsedBytes"));
          long totalAvailableBytes = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("TotalAvailableBytes"));

          return new User(userID, username, password, firstName, lastName, usedBytes, totalAvailableBytes);
        }
        else
          return null;
      }
      catch (Exception exception)
      {
        Console.WriteLine(exception.ToString());

        throw exception;
      }
      finally
      {
        if (sqlDataReader != null)
          sqlDataReader.Close();

        sqlDatabase.Dispose();
      }
    }

  //  [OperationBehavior(TransactionScopeRequired = true)]
    public User Signup(string emailAddress, string password, string firstName, string lastName, out string channelGUID)
    {
      SqlDatabase sqlDatabase = null;
      User user = null;
      channelGUID = null;

      try
      {       
        sqlDatabase = new SqlDatabase();

        sqlDatabase.Open();

        sqlDatabase.AddReturnParameter();

        sqlDatabase.AddInputParameter("@EmailAddress", emailAddress);
        SqlParameter emailAddressExistParameter = sqlDatabase.AddOutputParameter("@EmailAddressExist", System.Data.SqlDbType.Bit, 1, null);

        sqlDatabase.ExecuteNonQuery("dp_getPublisherConsumerEmailAddressExist");

        if ((bool)emailAddressExistParameter.Value)
          return null;

        sqlDatabase.ClearParameters();

        sqlDatabase.AddInputParameter("@FirstName", firstName);
        sqlDatabase.AddInputParameter("@LastName", lastName);
        sqlDatabase.AddInputParameter("@EmailAddress", emailAddress);
        sqlDatabase.AddInputParameter("@Password", password);
        SqlParameter channelGUIDParameter = sqlDatabase.AddOutputParameter("@ChannelGUID", System.Data.SqlDbType.NVarChar, 40, null);
        SqlParameter userIDParameter = sqlDatabase.AddOutputParameter("@UserID", System.Data.SqlDbType.Int, 4, null);
        SqlParameter totalAvailableBytesParameter = sqlDatabase.AddOutputParameter("@TotalAvailableBytes", System.Data.SqlDbType.BigInt, 8, null);
        
        sqlDatabase.ExecuteNonQuery("dp_addPublisherConsumer");

        channelGUID = (string)channelGUIDParameter.Value;

        user = new User((int)userIDParameter.Value, emailAddress, 
          password, firstName, lastName, 0L, (long)totalAvailableBytesParameter.Value);
      }
      catch (Exception exception)
      {
        Console.WriteLine(exception.ToString());
        _eventLog.WriteEntry(exception.ToString(), EventLogEntryType.Error);
        throw new DataAccessException("Error: Inserting User", exception);
      }
      finally
      {
        sqlDatabase.Dispose();
      }

      return user;
    }

    public PageAssetContentData GetRawContent(int userID, int folderID)
    {
      PageAssetContentData pageAssetContentData = new PageAssetContentData();
      List<AssetContentListContent> contentList = new List<AssetContentListContent>();
      pageAssetContentData.AssetContents = contentList;

      SqlDatabase sqlDatabase = null;
      SqlDataReader assetContentReader = null;
      SqlDataReader pageReader = null;

      try
      {
        sqlDatabase = new SqlDatabase(); 
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);
        sqlDatabase.AddInputParameter("@AssetContentFolderID", folderID);

        assetContentReader = sqlDatabase.ExecuteReader("dp_getAssetContentByFolderID");

        while (assetContentReader.Read())
        {
          AssetContentListContent content = new AssetContentListContent();

          content.AssetContentID = assetContentReader.GetInt32(assetContentReader.GetOrdinal("ASSETCONTENT_ID"));
          content.Name = assetContentReader.GetString(assetContentReader.GetOrdinal("Name"));
          content.ImagePath = assetContentReader.GetString(assetContentReader.GetOrdinal("ImagePath"));

          contentList.Add(content);
        }

        assetContentReader.Dispose();

        pageReader = sqlDatabase.ExecuteReader("dp_getAssetContentFolderPages");

        if (pageReader.Read())
          pageAssetContentData.NoPages = pageReader.GetInt32(pageReader.GetOrdinal("NoPages"));

        pageReader.Dispose();
      }
      catch (Exception exception)
      {
        throw new DataAccessException("Error: Reading Content List", exception);
      }
      finally
      {
        if (assetContentReader != null && !assetContentReader.IsClosed)
          assetContentReader.Close();

        if (pageReader != null && !pageReader.IsClosed)
          pageReader.Close();

        sqlDatabase.Dispose();
      }

      return pageAssetContentData;
    }

    public AssetContentProperties GetRawContentProperties(int userID, int contentID)
    {
      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;
      AssetContentProperties contentProperties = null;

      try
      {
        sqlDatabase = new SqlDatabase(); 
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);
        sqlDatabase.AddInputParameter("@AssetContentID",contentID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getAssetContentPropertiesByContentID");

        if (sqlDataReader.Read())
        {
          contentProperties = new AssetContentProperties();

          contentProperties.AssetContentID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("ASSETCONTENT_ID"));
          contentProperties.Name = GetStringValueIfNotNull(sqlDataReader, "Name");
          contentProperties.Creator = GetStringValueIfNotNull(sqlDataReader, "Creator");
          contentProperties.Caption = GetStringValueIfNotNull(sqlDataReader, "Caption");
          contentProperties.UserGivenDate = GetNullableDateTimeValueIfNotNull(sqlDataReader, "UserGivenDate");
          contentProperties.URL = GetStringValueIfNotNull(sqlDataReader, "URL");
          contentProperties.DisplayDuration = (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("DisplayDuration"));
        }
      }
      catch (Exception exception)
      {
        Console.WriteLine(exception);
        throw new DataAccessException("Error: Reading Content Properties", exception);
      }
      finally
      {
        if (sqlDataReader != null)
          sqlDataReader.Close();

        sqlDatabase.Dispose();
      }

      return contentProperties;
    }

    public PageSlideData GetStreamSlides(int userID, int folderID)
    {
      SqlDatabase sqlDatabase = null;
      SqlDataReader streamSlidesReader = null;
      SqlDataReader pageReader = null;

      PageSlideData pageSlideData = new PageSlideData();
      List<SlideListSlide> slideList = new List<SlideListSlide>();
      pageSlideData.Slides = slideList;

      try
      {
        sqlDatabase = new SqlDatabase(); 
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);
        sqlDatabase.AddInputParameter("@SlideFolderID", folderID);

        streamSlidesReader = sqlDatabase.ExecuteReader("dp_getStreamSlidesBySlideFolderID");

        while (streamSlidesReader.Read())
        {
          SlideListSlide slide = new SlideListSlide();

          slide.SlideID = streamSlidesReader.GetInt32(streamSlidesReader.GetOrdinal("SLIDE_ID"));
          slide.SlideName = streamSlidesReader.GetString(streamSlidesReader.GetOrdinal("SlideName"));
          slide.ImagePath = streamSlidesReader.GetString(streamSlidesReader.GetOrdinal("ImagePath"));
          slide.Locked = streamSlidesReader.GetBoolean(streamSlidesReader.GetOrdinal("bLocked"));

          slideList.Add(slide);
        }

        streamSlidesReader.Dispose();

        pageReader = sqlDatabase.ExecuteReader("dp_getSlideFolderPages");

        if (pageReader.Read())
          pageSlideData.NoPages = pageReader.GetInt32(pageReader.GetOrdinal("NoPages"));

        pageReader.Dispose();
      }
      catch (Exception exception)
      {
        Console.WriteLine(exception.ToString());
        throw new Exception("Error: Reading Slide", exception);
      }
      finally
      {
        if (streamSlidesReader != null)
          streamSlidesReader.Dispose();

        if (pageReader != null && !pageReader.IsClosed)
          pageReader.Dispose();

        sqlDatabase.Dispose();
      }

      return pageSlideData;
    }

    public PageSlideData GetUserStreams(int userID, int channelID)
    {
      PageSlideData pageSlideData = new PageSlideData();
      List<SlideListSlide> slideList = new List<SlideListSlide>();
      pageSlideData.Slides = slideList;

      SqlDatabase sqlDatabase = null;
      SqlDataReader slideReader = null;
      SqlDataReader pageReader = null;
      
      try
      {
        sqlDatabase = new SqlDatabase(); 
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);
        sqlDatabase.AddInputParameter("@ChannelID", channelID);

        slideReader = sqlDatabase.ExecuteReader("dp_getChannelSlidesByChannelID");

        while (slideReader.Read())
        {
          SlideListSlide slide = new SlideListSlide();

          slide.SlideID = slideReader.GetInt32(slideReader.GetOrdinal("CHANNELSSLIDE_ID"));
          slide.SlideName = slideReader.GetString(slideReader.GetOrdinal("SlideName"));
          slide.ImagePath = slideReader.GetString(slideReader.GetOrdinal("ImagePath"));

          slideList.Add(slide);
        } 

        slideReader.Dispose();

        pageReader = sqlDatabase.ExecuteReader("dp_getChannelSlidePages");

        if (pageReader.Read())
        {
          pageSlideData.NoPages = pageReader.GetInt32(pageReader.GetOrdinal("NoPages"));
          pageSlideData.ChannelThumbnailPath = GetStringValueIfNotNull(pageReader, "ImagePath");
        }

        pageReader.Dispose();
      }
      catch (Exception exception)
      {
        Console.WriteLine(exception);
        throw exception;
      }
      finally
      {
        if (slideReader != null && !slideReader.IsClosed)
          slideReader.Dispose();

        if (pageReader != null && !pageReader.IsClosed)
          pageReader.Dispose();
        
        sqlDatabase.Dispose();
      }

      return pageSlideData;
    }

    public List<List<CreateContentGenericFolder>> GetFolders(int userID)
    {
      List<List<CreateContentGenericFolder>> folderListList = new List<List<CreateContentGenericFolder>>();

      List<CreateContentGenericFolder> folderList = null;

      int previousOrderNumber = -1;
      int currentOrderNumber = -1;

      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;

      try
      {
        sqlDatabase = new SqlDatabase(); 
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getAllFoldersByUserID");

        while (sqlDataReader.Read())
        {
          currentOrderNumber = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("OrderNumber"));

          if (previousOrderNumber != currentOrderNumber)
          {
            previousOrderNumber = currentOrderNumber;

            folderList = new List<CreateContentGenericFolder>();

            folderListList.Add(folderList);
          }

          CreateContentGenericFolder folder = new CreateContentGenericFolder();

          folder.FolderID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("FolderID"));
          folder.FolderName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("FolderName"));
          
          folderList.Add(folder);
        }
      }
      catch (Exception exception)
      {
        Console.WriteLine(exception.ToString());
        throw new DataAccessException("Error: Reading Folder List", exception);
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }

      return folderListList;
    }

    public void PutRawContentProperties(int userID, AssetContentProperties contentProperties)
    {
      using(TransactionScope ts = new TransactionScope())
      {
        SqlDatabase sqlDatabase = null;

        try
        {
          sqlDatabase = new SqlDatabase(); 
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@UserID", userID);
          sqlDatabase.AddInputParameter("@AssetContentID", contentProperties.AssetContentID);
          sqlDatabase.AddInputParameter("@Name", contentProperties.Name);
          sqlDatabase.AddInputParameter("@Creator", contentProperties.Creator);
          sqlDatabase.AddInputParameter("@Caption", contentProperties.Caption);
          sqlDatabase.AddInputParameter("@UserGivenDate", contentProperties.UserGivenDate);
          sqlDatabase.AddInputParameter("@URL", contentProperties.URL);
          sqlDatabase.AddInputParameter("@DisplayDuration", contentProperties.DisplayDuration);

          sqlDatabase.ExecuteNonQuery("dp_editAssetContent");
        }
        catch (Exception exception)
        {
          Console.WriteLine(exception);
          throw exception;
        }
        finally
        {
          sqlDatabase.Dispose();
        }

        ts.Complete();
      }
    }

    public ChannelProperties GetChannelProperties(int userID, int channelID)
    {
      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;

      ChannelProperties channelProperties = null;

      try
      {
        sqlDatabase = new SqlDatabase(); 
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);
        sqlDatabase.AddInputParameter("@ChannelID", channelID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getChannelPropertiesByChannelID");

        if (sqlDataReader.Read())
        {
          channelProperties = new ChannelProperties();

          channelProperties.ChannelID = channelID;
          channelProperties.CategoryID = sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("CATEGORY_ID")) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CATEGORY_ID"));
          channelProperties.Name = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ChannelName"));
          channelProperties.CategoryName = GetStringValueIfNotNull(sqlDataReader, "CategoryName");
          channelProperties.Description = GetStringValueIfNotNull(sqlDataReader, "ChannelDescription");
          channelProperties.LongDescription = GetStringValueIfNotNull(sqlDataReader, "ChannelLongDescription");
          channelProperties.Keywords = GetStringValueIfNotNull(sqlDataReader, "Keywords");
          channelProperties.Locked = sqlDataReader.GetBoolean(sqlDataReader.GetOrdinal("bLocked"));
          channelProperties.ChannelPassword = GetStringValueIfNotNull(sqlDataReader,"ChannelPassword");
          channelProperties.AcceptPasswordRequests = sqlDataReader.GetBoolean(sqlDataReader.GetOrdinal("bAcceptPasswordRequests"));
          channelProperties.HasAuthorizedUsers = sqlDataReader.GetBoolean(sqlDataReader.GetOrdinal("bHasAuthorizedUsers"));        
        }
      }
      catch (Exception exception)
      {
        Console.WriteLine(exception);
        throw exception;
      }
      finally
      {
        if (sqlDataReader != null)
          sqlDataReader.Close();

        sqlDatabase.Dispose();
      }

      return channelProperties;
    }

    public void EditChannel(int userID, int channelID, int? categoryID, string channelName, string description,
      string longDescription, string keywords, bool bLocked, string password, bool bAcceptPasswordRequests, 
      ChannelPrivacyOptions options)
    {
      SqlDatabase sqlDatabase = null;

      try
      {
        sqlDatabase = new SqlDatabase(); 
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);
        sqlDatabase.AddInputParameter("@ChannelID", channelID);
        sqlDatabase.AddInputParameter("@CategoryID", categoryID);
        sqlDatabase.AddInputParameter("@ChannelName", channelName);
        sqlDatabase.AddInputParameter("@ChannelDescription", description);
        sqlDatabase.AddInputParameter("@ChannelLongDescription", longDescription);
        sqlDatabase.AddInputParameter("@Keywords", keywords);
        sqlDatabase.AddInputParameter("@bLocked", bLocked);
        sqlDatabase.AddInputParameter("@Password", password);
        sqlDatabase.AddInputParameter("@bAcceptPasswordRequests", bAcceptPasswordRequests);
        sqlDatabase.AddInputParameter("@ChannelPrivacyOptions", options.ToString());

        sqlDatabase.ExecuteNonQuery("dp_editChannel");
      }
      catch (Exception exception)
      {
        Console.WriteLine(exception);
        throw exception;
      }
      finally
      {
        sqlDatabase.Dispose();
      }
    }

 //   [OperationBehavior(TransactionScopeRequired = true)]
    public void AddAssetContent(int userID, int contentFolderID, List<AssetContent> assetContents)
    {
      SqlDatabase sqlDatabase = null;

      try
      {
        sqlDatabase = new SqlDatabase(); 
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);
        sqlDatabase.AddInputParameter("@AssetContentFolderID", contentFolderID);
        sqlDatabase.AddInputParameter("@Name");
        sqlDatabase.AddInputParameter("@GUID");
        sqlDatabase.AddInputParameter("@Caption");
        sqlDatabase.AddInputParameter("@Creator");
        sqlDatabase.AddInputParameter("@Filename");
        sqlDatabase.AddInputParameter("@FilenameNoPath");
        sqlDatabase.AddInputParameter("@FilenameExtension");
        sqlDatabase.AddInputParameter("@ImagePath");
        sqlDatabase.AddInputParameter("@ImagePathWinFS");
        sqlDatabase.AddInputParameter("@SubDir");
        sqlDatabase.AddInputParameter("@ImageName");
        sqlDatabase.AddInputParameter("@URL");
        sqlDatabase.AddInputParameter("@UserGivenDate");
        sqlDatabase.AddInputParameter("@DisplayDuration");
        sqlDatabase.AddInputParameter("@Length");
        sqlDatabase.AddInputParameter("@PreviewType");

        foreach (AssetContent assetContent in assetContents)
        {
          sqlDatabase.EditInputParameter("@Name", assetContent.Name);
          sqlDatabase.EditInputParameter("@GUID", assetContent.FileNameWithoutExtension);
          sqlDatabase.EditInputParameter("@Caption", assetContent.Caption);
          sqlDatabase.EditInputParameter("@Creator", assetContent.Creator);
          sqlDatabase.EditInputParameter("@Filename", assetContent.FileName);
          sqlDatabase.EditInputParameter("@FilenameNoPath", System.IO.Path.GetFileName(assetContent.FileName));
          sqlDatabase.EditInputParameter("@FilenameExtension", assetContent.Extension);
          sqlDatabase.EditInputParameter("@ImagePath", assetContent.ImagePath);
          sqlDatabase.EditInputParameter("@ImagePathWinFS", assetContent.ImagePathWinFS);
          sqlDatabase.EditInputParameter("@SubDir", assetContent.SubDir);
          sqlDatabase.EditInputParameter("@ImageName", assetContent.ImageName);
          sqlDatabase.EditInputParameter("@URL", assetContent.Url);
          sqlDatabase.EditInputParameter("@UserGivenDate", assetContent.UserGivenDate);
          sqlDatabase.EditInputParameter("@DisplayDuration", assetContent.DisplayDuration);
          sqlDatabase.EditInputParameter("@Length", assetContent.Length);
          sqlDatabase.EditInputParameter("@PreviewType", assetContent.PreviewType.ToString());

          sqlDatabase.ExecuteNonQuery("dp_addAssetContent");
        }
      }
      finally
      {
        sqlDatabase.Dispose();
      }
    }

    [OperationBehavior(TransactionScopeRequired = true)]
    public List<SlideContent> AddSlideContent(int userID, int slideFolderID, List<int> contentIDList)
    {
      List<SlideContent> contents = new List<SlideContent>();

      SqlDatabase sqlDatabase = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);
        sqlDatabase.AddInputParameter("@SlideFolderID", slideFolderID);
        sqlDatabase.AddInputParameter("@AssetContentID");
        sqlDatabase.AddInputParameter("@GUID");
        SqlParameter assetFilenameParameter = sqlDatabase.AddOutputParameter("@AssetFilename", System.Data.SqlDbType.NVarChar, 255, null);
        SqlParameter subDirParameter = sqlDatabase.AddOutputParameter("@SubDir", System.Data.SqlDbType.Char, 1, null);
        SqlParameter slideImagePathWinFSParameter = sqlDatabase.AddOutputParameter("@SlideImagePathWinFS", System.Data.SqlDbType.NVarChar, 255, null);
        SqlParameter assetFilenameExtensionParameter = sqlDatabase.AddOutputParameter("@AssetFilenameExtension", System.Data.SqlDbType.NVarChar, 20, null);
        SqlParameter slideFilenameWithPathParameter = sqlDatabase.AddOutputParameter("@SlideFilenameWithPath", System.Data.SqlDbType.NVarChar, 100, null);
        SqlParameter slideFilenameNoPathParameter = sqlDatabase.AddOutputParameter("@SlideFilenameNoPath", System.Data.SqlDbType.NVarChar, 100, null);
        SqlParameter assetImagePathWinFSParam = sqlDatabase.AddOutputParameter("@AssetImagePathWinFS", System.Data.SqlDbType.NVarChar, 255, null);

        foreach (int contentID in contentIDList)
        {
          string GUID = System.Guid.NewGuid().ToString().ToUpper() + "_" + GetRandomLetter();

          sqlDatabase.EditInputParameter("@AssetContentID", contentID);
          sqlDatabase.EditInputParameter("@GUID", GUID);

          sqlDatabase.ExecuteNonQuery("dp_addSlide");

          contents.Add(new SlideContent()
          {
            CorrespondingAssetContentFilenameWithPath = (string)assetFilenameParameter.Value,
            SubDir = (string)subDirParameter.Value,
            Extension = (string)assetFilenameExtensionParameter.Value,
            FileName = (string)slideFilenameWithPathParameter.Value,
            FilenameNoPath = (string)slideFilenameNoPathParameter.Value,
            FileNameWithoutExtension =  GUID,
            ImagePathWinFS = (string)subDirParameter.Value + "\\" + GUID + ".jpg",
            ImageName = GUID + ".jpg",
            AssetImagePathWinFS = (string)assetImagePathWinFSParam.Value
          });
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw ex;
      }
      finally
      {
        sqlDatabase.Dispose();
      }

      return contents;
    }

 //   [OperationBehavior(TransactionScopeRequired = true)]
    public void AddChannelContent(int userID,
      int channelID,
      List<int> slideIDList,
      string schedule,
      string presentationSchedule,
      string url,
      float displayDuration,
      string newThumbnailGUID,
      out string slideThumbnailWithPartialPath,
      out string channelGUID)
    {
      SqlDatabase sqlDatabase = null;
      slideThumbnailWithPartialPath = null;
      channelGUID = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);
        sqlDatabase.AddInputParameter("@ChannelID", channelID);
        sqlDatabase.AddInputParameter("@NewThumbnailGUID", newThumbnailGUID);
        sqlDatabase.AddInputParameter("@SlideID", slideIDList[0]);
        SqlParameter slideThumbnailWithPartialPathParam = sqlDatabase.AddOutputParameter("@SlideThumbnailWithPartialPath", System.Data.SqlDbType.NVarChar, 255, null);
        SqlParameter channelGUIDParam = sqlDatabase.AddOutputParameter("@ChannelGUID", System.Data.SqlDbType.NVarChar, 40, null);

        sqlDatabase.ExecuteNonQuery("dp_editChannelThumbnailIfNotExists");

        if (slideThumbnailWithPartialPathParam.Value != DBNull.Value)
        {
          slideThumbnailWithPartialPath = (string)slideThumbnailWithPartialPathParam.Value;
          channelGUID = (string)channelGUIDParam.Value;
        }

        sqlDatabase.RemoveParameter("@SlideThumbnailWithPartialPath");
        sqlDatabase.RemoveParameter("@ChannelGUID");
        sqlDatabase.RemoveParameter("@NewThumbnailGUID");

        sqlDatabase.AddInputParameter("@Schedule", schedule);
        sqlDatabase.AddInputParameter("@PresentationConvertedSchedule", presentationSchedule);
        sqlDatabase.AddInputParameter("@URL", url);
        sqlDatabase.AddInputParameter("@DisplayDuration", displayDuration);

        foreach (int slideID in slideIDList)
        {
          sqlDatabase.EditInputParameter("@SlideID", slideID);

          sqlDatabase.ExecuteNonQuery("dp_addChannelContent");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
        throw ex;
      }
      finally
      {
        sqlDatabase.Dispose();
      }      
    }

    [OperationBehavior(TransactionScopeRequired = true)] 
    public RemovableContent RemoveSlideContent(int userID, int slideFolderID, int slideID)
    {
      RemovableContent content = null;

      SqlDatabase sqlDatabase = null;

      try
      {
        sqlDatabase = new SqlDatabase(); 
        sqlDatabase.Open();

        sqlDatabase.AddReturnParameter();
        sqlDatabase.AddInputParameter("@UserID", userID);
        sqlDatabase.AddInputParameter("@SlideFolderID", slideFolderID);
        sqlDatabase.AddInputParameter("@SlideID", slideID);
        SqlParameter filenameParameter = sqlDatabase.AddOutputParameter("@Filename", System.Data.SqlDbType.NVarChar, 255, null);
        SqlParameter imagePathParameter = sqlDatabase.AddOutputParameter("@ImagePath", System.Data.SqlDbType.NVarChar, 255, null);         

        sqlDatabase.ExecuteNonQuery("dp_removeSlide");

        if (sqlDatabase.ReturnValue == 1)
          content = new RemovableContent((string)filenameParameter.Value, (string)imagePathParameter.Value); 
      }
      catch (Exception exception)
      {
        Console.WriteLine(exception);
        throw exception;
      }
      finally
      {
        sqlDatabase.Dispose();
      }

      return content;
    }

    public void RemoveChannelContent(int userID, int channelID, List<int> slideIDList)
    {
      TransactionScope scope = null;

      try
      {
        scope = new TransactionScope();

        SqlDatabase sqlDatabase = null;

        try
        {
          sqlDatabase = new SqlDatabase(); 
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@UserID", userID);
          sqlDatabase.AddInputParameter("@ChannelID", channelID);
          sqlDatabase.AddInputParameter("@ChannelSlideID");
          
          foreach (int slideID in slideIDList)
          {
            sqlDatabase.EditInputParameter("@ChannelSlideID", slideID);

            sqlDatabase.ExecuteNonQuery("dp_removeChannelContent");
          }

          scope.Complete();
        }
        catch (Exception exception)
        {
          Console.WriteLine(exception);
          throw new DataAccessException("Error: Removing Stream", exception);
        }
        finally
        {
          sqlDatabase.Dispose();
        }
      }
      finally
      {
        scope.Dispose();
      }
    }

    public void AddPCStream(int userID, int pcID, List<int> channelIDList)
    {
      SqlDatabase sqlDatabase = null;

      using (TransactionScope ts = new TransactionScope())
      {
        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@UserID", userID);
          sqlDatabase.AddInputParameter("@PcID", pcID);
          sqlDatabase.AddInputParameter("@ChannelID");

          foreach (int channelID in channelIDList)
          {
            sqlDatabase.EditInputParameter("@ChannelID", channelID);

            sqlDatabase.ExecuteNonQuery("dp_addPCStream");
          }
        }
        catch (Exception exception)
        {
          _eventLog.WriteEntry(exception.ToString(), EventLogEntryType.Error);

          throw exception;
        }
        finally
        {
          sqlDatabase.Dispose();
        }

        ts.Complete();
      }
    }

    public void AddPCStreamNotRegistered(int pcID, List<int> channelIDList)
    {
      SqlDatabase sqlDatabase = null;

      using (TransactionScope ts = new TransactionScope())
      {
        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@PCID", pcID);
          sqlDatabase.AddInputParameter("@ChannelID");

          foreach (int channelID in channelIDList)
          {
            sqlDatabase.EditInputParameter("@ChannelID", channelID);

            sqlDatabase.ExecuteNonQuery("dp_addPCStreamNotRegistered");
          }
        }
        catch (Exception exception)
        {
          Console.WriteLine(exception.ToString());

          _eventLog.WriteEntry(exception.ToString(), EventLogEntryType.Error);

          throw exception;
        }
        finally
        {
          sqlDatabase.Dispose();
        }

        ts.Complete();
      }
    }

    public void RemovePCStreamRegistered(int userID, int pcID, List<int> channelIDList)
    {
      TransactionScope scope = null;

      try
      {
        scope = new TransactionScope();

        SqlDatabase sqlDatabase = null;

        try
        {
          sqlDatabase = new SqlDatabase(); 
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@UserID", userID);
          sqlDatabase.AddInputParameter("@PcID", pcID);
          sqlDatabase.AddInputParameter("@ChannelID");

          foreach (int channelID in channelIDList)
          {
            sqlDatabase.EditInputParameter("@ChannelID", channelID);

            sqlDatabase.ExecuteNonQuery("dp_removePCStreamRegistered");
          }

          scope.Complete();
        }
        catch (Exception exception)
        {
          Console.WriteLine(exception);
          throw new DataAccessException("Error: Removing PC Stream", exception);
        }
        finally
        {
          sqlDatabase.Dispose();
        }
      }
      finally
      {
        scope.Dispose();
      }
    }

    public void RemovePCStreamUnregistered(int pcID, List<int> channelIDList)
    {
      TransactionScope scope = null;

      try
      {
        scope = new TransactionScope();

        SqlDatabase sqlDatabase = null;

        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@PcID", pcID);
          sqlDatabase.AddInputParameter("@ChannelID");

          foreach (int channelID in channelIDList)
          {
            sqlDatabase.EditInputParameter("@ChannelID", channelID);

            sqlDatabase.ExecuteNonQuery("dp_removePCStreamUnregistered");
          }

          scope.Complete();
        }
        catch (Exception exception)
        {
          Console.WriteLine(exception);
          throw new DataAccessException("Error: Removing PC Stream", exception);
        }
        finally
        {
          sqlDatabase.Dispose();
        }
      }
      finally
      {
        scope.Dispose();
      }
    }

    public int AddAssetContentFolder(int userID, string folderName)
    {
      TransactionScope scope = null;

      try
      {
        scope = new TransactionScope();

        SqlDatabase sqlDatabase = null;

        try
        {
          sqlDatabase = new SqlDatabase(); 
          sqlDatabase.Open();

          sqlDatabase.AddReturnParameter();
          sqlDatabase.AddInputParameter("@UserID", userID);
          sqlDatabase.AddInputParameter("@FolderName", folderName);

          sqlDatabase.ExecuteNonQuery("dp_addAssetContentFolder");

          scope.Complete();
        }
        catch (Exception exception)
        {
          Console.WriteLine(exception);
          throw new DataAccessException("Error: Adding Folder", exception);
        }
        finally
        {
          sqlDatabase.Dispose();
        }

        return sqlDatabase.ReturnValue;
      }
      finally
      {
        scope.Dispose();
      }
    }

    public string EditAssetContentFolder(int userID, int folderID, string folderName)
    {
      TransactionScope scope = null;

      try
      {
        scope = new TransactionScope();

        SqlDatabase sqlDatabase = null;

        try
        {
          sqlDatabase = new SqlDatabase(); sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@UserID", userID);
          sqlDatabase.AddInputParameter("@FolderID", folderID);
          sqlDatabase.AddInputParameter("@FolderName", folderName);

          sqlDatabase.ExecuteNonQuery("dp_editAssetContentFolder");

          scope.Complete();
        }
        catch (Exception exception)
        {
          Console.WriteLine(exception);
          throw new DataAccessException("Error: Editing Asset Content Folder", exception);
        }
        finally
        {
          sqlDatabase.Dispose();
        }

        return "1";
      }
      finally
      {
        scope.Dispose();
      }
    }

    public HashSet<RemovableContent> RemoveAssetContentFolder(int userID, int folderID, out int filesLength)
    {
      HashSet<RemovableContent> removables = new HashSet<RemovableContent>();

      using (TransactionScope ts = new TransactionScope())
      {
        SqlDatabase sqlDatabase = null;
        SqlDataReader sqlDataReader = null;

        try
        {
          sqlDatabase = new SqlDatabase(); 
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@UserID", userID);
          sqlDatabase.AddInputParameter("@AssetContentFolderID", folderID);

          sqlDataReader = sqlDatabase.ExecuteReader("dp_getRemovableAssetContentsByAssetContentFolderID");

          while (sqlDataReader.Read())
          {
            RemovableContent content = new RemovableContent();

            content.FileName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("Filename"));
            content.ImagePathWinFS = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ImagePathWinFS"));

            removables.Add(content);
          }

          sqlDataReader.Dispose();

          SqlParameter filesLengthParameter = sqlDatabase.AddOutputParameter("@FilesLength", System.Data.SqlDbType.Int, 4, null);

          sqlDatabase.ExecuteNonQuery("dp_removeAssetContentFolder");

          filesLength = (int)filesLengthParameter.Value;
        }
        catch (Exception exception)
        {
          Console.WriteLine(exception);
          throw exception;
        }
        finally
        {
          if (sqlDataReader != null && !sqlDataReader.IsClosed)
            sqlDataReader.Dispose();

          sqlDatabase.Dispose();
        }

        ts.Complete();
      }

      return removables;
    }
    
    public List<ChannelSimple> GetChannelMostPopular()
    {
      List<ChannelSimple> channelList = new List<ChannelSimple>();

      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;

      try
      {
        sqlDatabase = new SqlDatabase(); 
        sqlDatabase.Open();

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getMostPopularChannels");

        while (sqlDataReader.Read())
        {
          ChannelSimple channel = new ChannelSimple();

          channel.ChannelID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CHANNEL_ID"));
          channel.ChannelName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ChannelName"));

          channelList.Add(channel);
        }
      }
      catch (Exception exception)
      {
        throw new DataAccessException("Error: Reading Channel List", exception);
      }
      finally
      {
        if (sqlDataReader != null)
          sqlDataReader.Close();

        sqlDatabase.Dispose();
      }

      return channelList;
    }

    public List<ChannelSimple> GetChannelListByLetter(string activeLetter, SortChannelsBy sort)
    {
      List<ChannelSimple> channelList = new List<ChannelSimple>();

      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;

      try
      {
        sqlDatabase = new SqlDatabase(); 
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@ChannelLetter", activeLetter);
        sqlDatabase.AddInputParameter("@SortBy", sort.ToString());

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getChannelsByLetter");

        while (sqlDataReader.Read())
        {
          ChannelSimple channel = new ChannelSimple();

          channel.ChannelID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CHANNEL_ID"));
          channel.ChannelName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ChannelName"));

          channelList.Add(channel);
        }
      }
      catch (Exception exception)
      {
        throw new DataAccessException("Error: Reading Channel List", exception);
      }
      finally
      {
        if (sqlDataReader != null)
          sqlDataReader.Close();

        sqlDatabase.Dispose();
      }

      return channelList;
    }

    public int AddPC(int userID, string pcName)
    {
      int pcID = -1;

      using (TransactionScope ts = new TransactionScope())
      {
        SqlDatabase sqlDatabase = null;

        try
        {
          sqlDatabase = new SqlDatabase(); 
          sqlDatabase.Open();

          sqlDatabase.AddReturnParameter();
          sqlDatabase.AddInputParameter("@UserID", userID);
          sqlDatabase.AddInputParameter("@PCName", pcName);

          sqlDatabase.ExecuteNonQuery("dp_addPC");

          pcID = sqlDatabase.ReturnValue;
        }
        catch (Exception exception)
        {
          throw new DataAccessException("Error: Adding PC", exception);
        }
        finally
        {
          sqlDatabase.Dispose();
        }

        ts.Complete();
      }

      return pcID;
    }

    public void RenamePC(int userID, int pcID, string PCName)
    {
      SqlDatabase sqlDatabase = null;

      using (TransactionScope ts = new TransactionScope())
      {
        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@UserID", userID);
          sqlDatabase.AddInputParameter("@PCID", pcID);
          sqlDatabase.AddInputParameter("@PCName", PCName);

          sqlDatabase.ExecuteNonQuery("dp_editPC");
        }
        catch (Exception exception)
        {
          throw new DataAccessException("Error: Editing PC", exception);
        }
        finally
        {
          sqlDatabase.Dispose();
        }

        ts.Complete();
      }
    }

    public void EditChannelWeighting(int userID, int pcID, int channelID, int channelWeighting)
    {
      using (TransactionScope ts = new TransactionScope())
      {
        SqlDatabase sqlDatabase = null;

        try
        {
          sqlDatabase = new SqlDatabase(); 
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@UserID", userID);
          sqlDatabase.AddInputParameter("@PCID", pcID);
          sqlDatabase.AddInputParameter("@ChannelID", channelID);
          sqlDatabase.AddInputParameter("@ChannelWeighting", channelWeighting);

          sqlDatabase.ExecuteNonQuery("dp_editChannelWeighting");

        }
        catch (Exception exception)
        {
          Console.WriteLine(exception);
          throw new DataAccessException("Error: Editing Channel Weighting", exception);
        }
        finally
        {
          sqlDatabase.Dispose();
        }

        ts.Complete();
      }
    }

    public Channel GetChannelDetailsFull(int userID, int channelID)
    {
      SqlDatabase sqlDatabase = null;
      SqlDataReader channelReader = null;
      SqlDataReader slideReader = null;

      Channel channel = null;

      List<SlideListSlide> slides = new List<SlideListSlide>();

      try
      {
        sqlDatabase = new SqlDatabase(); 
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);
        sqlDatabase.AddInputParameter("@ChannelID", channelID);

        channelReader = sqlDatabase.ExecuteReader("dp_getChannelFull");

        if (channelReader.Read())
        {
          channel = new Channel();

          channel.ChannelID = channelReader.GetInt32(channelReader.GetOrdinal("CHANNEL_ID"));
          channel.ChannelGUID = channelReader.GetString(channelReader.GetOrdinal("ChannelGUID"));
          channel.ChannelName = channelReader.GetString(channelReader.GetOrdinal("ChannelName"));
          channel.ChannelDescription = GetStringValueIfNotNull(channelReader, "ChannelDescription");
          channel.ChannelLongDescription = GetStringValueIfNotNull(channelReader, "ChannelLongDescription"); ;
          channel.PublisherDisplayName = channelReader.GetString(channelReader.GetOrdinal("DisplayName"));
          channel.NoContents = channelReader.GetInt32(channelReader.GetOrdinal("NoContent"));
          channel.NoFollowers = channelReader.GetInt32(channelReader.GetOrdinal("NoFollowers"));
          channel.ContentLastAddedDate = GetDateTimeValueIfNotNull(channelReader, "ContentLastAddedDate");
          channel.PrivacyStatus = (ChannelPrivacyStatus)Enum.Parse(typeof(ChannelPrivacyStatus), channelReader.GetString(channelReader.GetOrdinal("PrivacyStatus")));
          channel.AddDate = channelReader.GetDateTime(channelReader.GetOrdinal("AddDate"));
        }

        channelReader.Close();

        slideReader = sqlDatabase.ExecuteReader("dp_getChannelSlides");

        while (slideReader.Read())
        {
          SlideListSlide slide = new SlideListSlide();

          slide.SlideID = slideReader.GetInt32(slideReader.GetOrdinal("CHANNELSSLIDE_ID"));
          slide.SlideName = slideReader.GetString(slideReader.GetOrdinal("SlideName"));
          slide.ImagePath = slideReader.GetString(slideReader.GetOrdinal("ImagePath"));

          slides.Add(slide);
        }

        slideReader.Close();

        channel.Slides = slides;
      }
      catch (Exception exception)
      {
        Console.WriteLine(exception.ToString());
        _eventLog.WriteEntry(exception.ToString(), EventLogEntryType.Error);
        throw exception;
      }
      finally
      {
        if (channelReader != null || !channelReader.IsClosed)
          channelReader.Close();

        sqlDatabase.Dispose();
      }

      return channel;
    }

    public List<List<ChannelListChannel>> GetPCStreamsAll(int userID)
    {
      SqlDataReader sqlDataReader = null;

      List<List<ChannelListChannel>> pcs = new List<List<ChannelListChannel>>();

      List<ChannelListChannel> channels = null;

      int previousPCID = -1;
      int currentPCID = -1;

      SqlDatabase sqlDatabase = null;
      
      try
      {
        sqlDatabase = new SqlDatabase(); 
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getPcStreamsAll");

        while (sqlDataReader.Read())
        {
          currentPCID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("PC_ID"));

          if (previousPCID != currentPCID)
          {
            previousPCID = currentPCID;

            channels = new List<ChannelListChannel>();

            pcs.Add(channels);
          }

          if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("CHANNEL_ID")))
          {
            ChannelListChannel channel = new ChannelListChannel();

            channel.ChannelID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CHANNEL_ID"));
            channel.ChannelName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ChannelName"));
            channel.ImagePath = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ImagePath"));

            channels.Add(channel);
          }
        }
      }
      catch (Exception exception)
      {
        Console.WriteLine(exception);
        throw exception;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }

      return pcs;
    }

    public List<Channel> GetTop5MostPopular(int categoryID)
    {
      List<Channel> channels = new List<Channel>();
      SqlDataReader sqlDataReader = null;

      SqlDatabase sqlDatabase = null;

      try
      {
        sqlDatabase = new SqlDatabase(); 
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@CategoryID", categoryID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getChannelFullPopularTop5ByCategoryID");

        while (sqlDataReader.Read())
        {
          Channel channel = new Channel();

          channel.ChannelID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CHANNEL_ID"));
          channel.ChannelName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ChannelName"));
          channel.ChannelDescription = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ChannelDescription"));
          channel.ChannelLongDescription = sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("ChannelLongDescription")) ? String.Empty : sqlDataReader.GetString(sqlDataReader.GetOrdinal("ChannelLongDescription"));
          channel.NoContents = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("NoContent"));
          channel.NoFollowers = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("NoFollowers"));
          channel.AddDate = sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("AddDate"));
          channel.ImagePath = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ImagePath"));
          channel.PublisherDisplayName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("DisplayName"));

          channels.Add(channel);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }

      return channels;
    }

    public string GetCategoryName(int categoryID)
    {
      string categoryName = null;
      SqlDataReader sqlDataReader = null;

      SqlDatabase sqlDatabase = null;

      try
      {
        sqlDatabase = new SqlDatabase(); 
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@CategoryID", categoryID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getCategoryName");

        if (sqlDataReader.Read())
          categoryName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("CategoryName"));
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }

      return categoryName;
    }

    public List<List<AssetContentListContent>> GetAssetContentAll(int userID)
    {
      List<List<AssetContentListContent>> assetContentFolders = new List<List<AssetContentListContent>>();
      SqlDataReader sqlDataReader = null;

      List<AssetContentListContent> assetContents = null;

      int previousAssetContentFolderID = -1;
      int currentAssetContentFolderID = -1;

      SqlDatabase sqlDatabase = null;

      try
      {
        sqlDatabase = new SqlDatabase(); 
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getAssetContentFoldersAll");

        while (sqlDataReader.Read())
        {
          currentAssetContentFolderID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("ASSETCONTENTFOLDER_ID"));

          if (previousAssetContentFolderID != currentAssetContentFolderID)
          {
            previousAssetContentFolderID = currentAssetContentFolderID;

            assetContents = new List<AssetContentListContent>();

            assetContentFolders.Add(assetContents);            
          }

          if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("ASSETCONTENT_ID")))
          {          
            AssetContentListContent assetContent = new AssetContentListContent();

            assetContent.AssetContentID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("ASSETCONTENT_ID"));
            assetContent.Name = sqlDataReader.GetString(sqlDataReader.GetOrdinal("Name"));
            assetContent.ImagePath = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ImagePath"));
            
            assetContents.Add(assetContent);
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }

      return assetContentFolders;
    }

    public List<List<SlideListSlide>> GetSlideFoldersAll(int userID)
    {
      List<List<SlideListSlide>> slideFolders = new List<List<SlideListSlide>>();
      SqlDataReader sqlDataReader = null;

      List<SlideListSlide> slides = null;

      int previousSlideFolderID = -1;
      int currentSlideFolderID = -1;

      SqlDatabase sqlDatabase = null;

      try
      {
        sqlDatabase = new SqlDatabase(); 
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getSlideFoldersAll");

        while (sqlDataReader.Read())
        {
          currentSlideFolderID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("SLIDEFOLDER_ID"));

          if (previousSlideFolderID != currentSlideFolderID)
          {
            previousSlideFolderID = currentSlideFolderID;

            slides = new List<SlideListSlide>();

            slideFolders.Add(slides);
          }

          if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("SLIDE_ID")))
          {
            SlideListSlide slide = new SlideListSlide();

            slide.SlideID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("SLIDE_ID"));
            slide.SlideName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("SlideName"));
            slide.ImagePath = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ImagePath"));

            slides.Add(slide);
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose(); 
        
        sqlDatabase.Dispose();
      }

      return slideFolders;
    }

    public bool UnlockStream(int userID, int channelID, string channelPassword)
    {
      bool bUnlocked = false;

      SqlDataReader sqlDataReader = null;

      SqlDatabase sqlDatabase = null;

      try
      {
        sqlDatabase = new SqlDatabase(); 
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);
        sqlDatabase.AddInputParameter("@ChannelID", channelID);
        sqlDatabase.AddInputParameter("@ChannelPassword", channelPassword);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_addUnlockStream");

        if (sqlDataReader.Read())
          bUnlocked = sqlDataReader.GetBoolean(sqlDataReader.GetOrdinal("Unlocked"));
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }

      return bUnlocked;
    }

    [OperationBehavior(TransactionScopeRequired = true)] 
    public RemovableContent RemoveAssetContent(int userID, int assetContentFolderID, int assetContentID)
    {
      RemovableContent removablefile = null;
      SqlDatabase sqlDatabase = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);
        sqlDatabase.AddInputParameter("@AssetContentFolderID", assetContentFolderID);
        sqlDatabase.AddInputParameter("@AssetContentID", assetContentID);
        SqlParameter filenameParameter = sqlDatabase.AddOutputParameter("@Filename", System.Data.SqlDbType.NVarChar, 255, null);
        SqlParameter imagePathWinFSParameter = sqlDatabase.AddOutputParameter("@ImagePathWinFS", System.Data.SqlDbType.NVarChar, 255, null);
        SqlParameter fileLengthParameter = sqlDatabase.AddOutputParameter("@FileLength", System.Data.SqlDbType.Int, 4, null);

        sqlDatabase.ExecuteNonQuery("dp_removeAssetContent");
        
        removablefile = new RemovableContent((string)filenameParameter.Value, (string)imagePathWinFSParameter.Value, (int)fileLengthParameter.Value);
      }
      catch (Exception exception)
      {
        Console.WriteLine(exception);
        throw exception;
      }
      finally
      {
        sqlDatabase.Dispose();
      }    

      return removablefile;
    }

    public int AddSlideFolder(int userID, string folderName)
    {
      TransactionScope scope = null;
      int slideFolderID = -1;

      try
      {
        scope = new TransactionScope();

        SqlDatabase sqlDatabase = null;

        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddReturnParameter();
          sqlDatabase.AddInputParameter("@UserID", userID);
          sqlDatabase.AddInputParameter("@SlideFolderName", folderName);

          sqlDatabase.ExecuteNonQuery("dp_addSlideFolder");

          slideFolderID = sqlDatabase.ReturnValue;

          scope.Complete();
        }
        catch (Exception exception)
        {
          Console.WriteLine(exception);
          throw new DataAccessException("Error: Editing Channel Weighting", exception);
        }
        finally
        {
          sqlDatabase.Dispose();
        }
      }
      finally
      {
        scope.Dispose();
      }

      return slideFolderID;
    }

    public string EditSlideFolder(int userID, int folderID, string folderName)
    {
      TransactionScope scope = null;

      try
      {
        scope = new TransactionScope();

        SqlDatabase sqlDatabase = null;

        try
        {
          sqlDatabase = new SqlDatabase(); 
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@UserID", userID);
          sqlDatabase.AddInputParameter("@SlideFolderID", folderID);
          sqlDatabase.AddInputParameter("@SlideFolderName", folderName);

          sqlDatabase.ExecuteNonQuery("dp_editSlideFolder");

          scope.Complete();
        }
        catch (Exception exception)
        {
          Console.WriteLine(exception);
          throw new DataAccessException("Error: Editing Slide Folder", exception);
        }
        finally
        {
          sqlDatabase.Dispose();
        }
      }
      finally
      {
        scope.Dispose();
      }

      return "1";
    }

    public HashSet<RemovableContent> RemoveSlideFolder(int userID, int folderID)
    {
      HashSet<RemovableContent> removables = null;

      using (TransactionScope ts = new TransactionScope())
      {
        SqlDatabase sqlDatabase = null;
        SqlDataReader sqlDataReader = null;

        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@SlideFolderID", folderID);

          sqlDataReader = sqlDatabase.ExecuteReader("dp_getHasSlideFolderScheduledSlides");

          bool bHasScheduledSlides = sqlDataReader.HasRows;

          sqlDataReader.Dispose();

          if (bHasScheduledSlides)
            return null;

          removables = new HashSet<RemovableContent>();

          sqlDatabase.AddInputParameter("@UserID", userID);

          sqlDataReader = sqlDatabase.ExecuteReader("dp_getRemovableSlideContentsByAssetContentFolderID");

          while (sqlDataReader.Read())
          {
            RemovableContent content = new RemovableContent();

            content.FileName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("Filename"));
            content.ImagePathWinFS = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ImagePathWinFS"));

            removables.Add(content);
          }

          sqlDataReader.Dispose();

          sqlDatabase.ExecuteNonQuery("dp_removeSlideFolder");
        }
        catch (Exception exception)
        {
          Console.WriteLine(exception);
          throw exception;
        }
        finally
        {
          if (sqlDataReader != null && !sqlDataReader.IsClosed)
            sqlDataReader.Dispose();

          sqlDatabase.Dispose();
        }

        ts.Complete();
      }

      return removables;
    }

    public string AddChannel(int userID, int? categoryID, string channelName, string description,
      string longDescription, string keywords, bool bLocked, string password, bool bAcceptPasswordRequests)
    {
      TransactionScope scope = null;
      string channelGUID = System.Guid.NewGuid().ToString().ToUpper() + "_" + GetRandomLetter();

      try
      {
        scope = new TransactionScope();

        SqlDatabase sqlDatabase = null;
        
        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddReturnParameter();
          sqlDatabase.AddInputParameter("@UserID", userID);
          sqlDatabase.AddInputParameter("@CategoryID", categoryID);
          sqlDatabase.AddInputParameter("@ChannelName", channelName);
          sqlDatabase.AddInputParameter("@ChannelDescription", description);
          sqlDatabase.AddInputParameter("@ChannelLongDescription", longDescription);
          sqlDatabase.AddInputParameter("@Keywords", keywords);
          sqlDatabase.AddInputParameter("@bLocked", bLocked);
          sqlDatabase.AddInputParameter("@Password", password);
          sqlDatabase.AddInputParameter("@bAcceptPasswordRequests", bAcceptPasswordRequests);
          sqlDatabase.AddInputParameter("@ChannelGUID", bAcceptPasswordRequests);
          
          sqlDatabase.ExecuteNonQuery("dp_addChannel");

          scope.Complete();
        }
        catch (Exception exception)
        {
          Console.WriteLine(exception);
          throw exception;
        }
        finally
        {
          sqlDatabase.Dispose();
        }
      }
      finally
      {
        scope.Dispose();
      }

      return channelGUID;
    }

  //  [OperationBehavior(TransactionScopeRequired = true)] 
    public string RemoveChannel(int userID, int channelID)
    {
      string channelThumbnailWithPartialPath = null;

      SqlDatabase sqlDatabase = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);
        sqlDatabase.AddInputParameter("@ChannelID", channelID);
        SqlParameter channelThumbnailWithPartialPathParam = sqlDatabase.AddOutputParameter("@ChannelThumbnailWithPartialPath", System.Data.SqlDbType.NVarChar, 255, null);

        sqlDatabase.ExecuteNonQuery("dp_removeChannel");

        channelThumbnailWithPartialPath = channelThumbnailWithPartialPathParam.Value == DBNull.Value ? null : (string)channelThumbnailWithPartialPathParam.Value;
      }
      catch (Exception exception)
      {
        Console.WriteLine(exception);
        throw exception;
      }
      finally
      {
        sqlDatabase.Dispose();
      }

      return channelThumbnailWithPartialPath;
    }

    public List<OxigenIIAdvertising.AppData.Channel> GetChannelsDirty()
    {
      List<OxigenIIAdvertising.AppData.Channel> channels = new List<OxigenIIAdvertising.AppData.Channel>();
      OxigenIIAdvertising.AppData.Channel channel = null;
      SqlDataReader sqlDataReader = null;
      SqlDatabase sqlDatabase = null;
      string[] splitter = new string[] { "||" };
    
      int previousChannelID = -1;
      int currentChannelID = -1;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getChannelsDirty");

        while (sqlDataReader.Read())
        {
          currentChannelID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CHANNEL_ID"));

          if (previousChannelID != currentChannelID)
          {
            previousChannelID = currentChannelID;

            channel = new OxigenIIAdvertising.AppData.Channel();

            channel.ChannelID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CHANNEL_ID"));
            channel.ChannelGUID = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ChannelGUID"));
            channel.VotingThreshold = 0.5F; // TODO: make dynamic
            channel.ChannelDefinitions = sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("CTT")) ? null : sqlDataReader.GetString(sqlDataReader.GetOrdinal("CTT"));

            channels.Add(channel);
          }

          OxigenIIAdvertising.AppData.ChannelAsset asset = new OxigenIIAdvertising.AppData.ChannelAsset();

          //
          // TODO: Make Dynamic: VotingThreshold, AssetLevel, StartDateTime, EndDateTime
          //
          if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("SLIDE_ID")))
          {
            asset.AssetID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("SLIDE_ID"));
            asset.PlayerType = (OxigenIIAdvertising.AppData.PlayerType)Enum.Parse(typeof(OxigenIIAdvertising.AppData.PlayerType),
              sqlDataReader.GetString(sqlDataReader.GetOrdinal("PlayerType")));
            asset.AssetFilename = sqlDataReader.GetString(sqlDataReader.GetOrdinal("FilenameNoPath"));
            asset.AssetLevel = (OxigenIIAdvertising.AppData.ChannelDataAssetLevel)Enum.Parse(typeof(OxigenIIAdvertising.AppData.ChannelDataAssetLevel),
              sqlDataReader.GetString(sqlDataReader.GetOrdinal("AssetLevel")));
            asset.DisplayDuration = (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("DisplayDuration"));
            asset.AssetWebSite = sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("WebsiteURL")) ? "" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("WebsiteURL"));
            asset.ClickDestination = sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("ClickThroughURL")) ? "" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("ClickThroughURL"));
            asset.ScheduleInfo = sqlDataReader.GetString(sqlDataReader.GetOrdinal("Schedule")).Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            asset.StartDateTime = sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("StartDateTime"));
            asset.EndDateTime = sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("EndDateTime"));

            channel.ChannelAssets.Add(asset);    
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }

      return channels;
    }

    public DemographicData GetUserDemographicData(string userGUID)
    {
      DemographicData demographicData = null;
      SqlDataReader sqlDataReader = null;

      SqlDatabase sqlDatabase = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserGUID", userGUID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getUserDemographicData");

        if (sqlDataReader.Read())
        {
          demographicData = new DemographicData();
          demographicData.Gender = new string[] { sqlDataReader.GetString(sqlDataReader.GetOrdinal("Gender")) };
          demographicData.GeoDefinition = sqlDataReader.GetString(sqlDataReader.GetOrdinal("TaxonomyTree"));
          demographicData.MinAge = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("Age"));
          demographicData.MaxAge = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("Age"));
          demographicData.SocioEconomicgroup = new string[] { sqlDataReader.GetString(sqlDataReader.GetOrdinal("SocioEconomicGroup")) };
        }
      }
      catch (Exception ex)
      {
        _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);

        throw ex;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }

      return demographicData;
    }

    public OxigenIIAdvertising.AppData.ChannelSubscriptions GetUserChannelSubscriptions(string userGUID, string machineGUID)
    {
      OxigenIIAdvertising.AppData.ChannelSubscriptions channelSubscriptions = channelSubscriptions = new OxigenIIAdvertising.AppData.ChannelSubscriptions();
      SqlDataReader sqlDataReader = null;
      SqlDatabase sqlDatabase = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@PCGUID", machineGUID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getUserMachineChannelSubscriptions");

        while (sqlDataReader.Read())
        {
          OxigenIIAdvertising.AppData.ChannelSubscription subscription = new OxigenIIAdvertising.AppData.ChannelSubscription();
          
          subscription.ChannelID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CHANNEL_ID"));
          subscription.ChannelGUID = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ChannelGUID"));
          subscription.ChannelName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ChannelName"));
          subscription.ChannelWeightingUnnormalised = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("ChannelWeighting"));
          subscription.Locked = sqlDataReader.GetBoolean(sqlDataReader.GetOrdinal("bLocked"));

          channelSubscriptions.SubscriptionSet.Add(subscription);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
        throw new DataAccessException("Error: Getting Channel Subscriptions", ex);
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }

      return channelSubscriptions;
    }

    public List<SimpleFileInfo> GetAssetsDirty()
    {
      List<SimpleFileInfo> slides = new List<SimpleFileInfo>();

      SqlDataReader sqlDataReader = null;
      SqlDatabase sqlDatabase = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getSlidesDirty");

        while (sqlDataReader.Read())
        {
          SimpleFileInfo simpleFileInfo = new SimpleFileInfo();

          simpleFileInfo.FileID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("SLIDE_ID"));
          simpleFileInfo.FileNameWithPath = sqlDataReader.GetString(sqlDataReader.GetOrdinal("Filename"));
          simpleFileInfo.FilenameNoPath = sqlDataReader.GetString(sqlDataReader.GetOrdinal("FilenameNoPath"));

          slides.Add(simpleFileInfo);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }

      return slides;
    }
       
    public void EditChannelsMakeClean(HashSet<int> channelIDs)
    {
      TransactionScope scope = null;

      try
      {
        scope = new TransactionScope();

        SqlDatabase sqlDatabase = null;

        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@ChannelID");

          foreach (int channelID in channelIDs)
          {
            sqlDatabase.EditInputParameter("@ChannelID", channelID);

            sqlDatabase.ExecuteNonQuery("dp_editChannelMakeClean");
          }
          
          scope.Complete();
        }
        catch (Exception exception)
        {
          Console.WriteLine(exception);
          throw new DataAccessException("Error: Editing Channel", exception);
        }
        finally
        {
          sqlDatabase.Dispose();
        }
      }
      finally
      {
        scope.Dispose();
      }
    }

    public void EditSlidesMakeClean(HashSet<int> slideIDs)
    {
      using (TransactionScope ts = new TransactionScope())
      {
        SqlDatabase sqlDatabase = null;

        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@SlideID");

          foreach (int slideID in slideIDs)
          {
            sqlDatabase.EditInputParameter("@SlideID", slideID);

            sqlDatabase.ExecuteNonQuery("dp_editSlideMakeClean");
          }
        }
        catch (Exception exception)
        {
          Console.WriteLine(exception);
          throw exception;
        }
        finally
        {
          sqlDatabase.Dispose();
        }

        ts.Complete();
      }
    }

    [OperationBehavior(TransactionScopeRequired = true)]
    public void EditThumbnailGUID(int userID, int channelID, string thumbnailGUID,
      out string channelGUIDSuffix, out string oldChannelThumbnailPath)
    {
      SqlDatabase sqlDatabase = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);
        sqlDatabase.AddInputParameter("@ChannelID", channelID);
        sqlDatabase.AddInputParameter("@ThumbnailGUID", thumbnailGUID);
        SqlParameter channelGUIDSuffixParam = sqlDatabase.AddOutputParameter("@ChannelGUIDSuffix", System.Data.SqlDbType.Char, 1, null);
        SqlParameter oldChannelThumbnailPathParam = sqlDatabase.AddOutputParameter("@OldChannelThumbnailPath", System.Data.SqlDbType.NVarChar, 255, null);

        sqlDatabase.ExecuteNonQuery("dp_editChannelThumbnail");

        channelGUIDSuffix = (string)channelGUIDSuffixParam.Value;
        oldChannelThumbnailPath = (string)oldChannelThumbnailPathParam.Value;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        sqlDatabase.Dispose();
      }
    }

    private string GetStringValueIfNotNull(SqlDataReader dataReader, string columnName)
    {
      return dataReader.IsDBNull(dataReader.GetOrdinal(columnName)) ? "" : dataReader.GetString(dataReader.GetOrdinal(columnName));
    }

    private int GetInt32ValueIfNotNull(SqlDataReader dataReader, string columnName)
    {
      return dataReader.IsDBNull(dataReader.GetOrdinal(columnName)) ? -1 : dataReader.GetInt32(dataReader.GetOrdinal(columnName));
    }

    private DateTime? GetNullableDateTimeValueIfNotNull(SqlDataReader dataReader, string columnName)
    {
      if (dataReader.IsDBNull(dataReader.GetOrdinal(columnName)))
        return new DateTime?();

      return dataReader.GetDateTime(dataReader.GetOrdinal(columnName));
    }

    private DateTime GetDateTimeValueIfNotNull(SqlDataReader dataReader, string columnName)
    {
      if (dataReader.IsDBNull(dataReader.GetOrdinal(columnName)))
        return new DateTime();

      return dataReader.GetDateTime(dataReader.GetOrdinal(columnName));
    }

    public void EditSlideContentProperties(int userID, SlideProperties slide)
    {
      using (TransactionScope ts = new TransactionScope())
      {
        SqlDatabase sqlDatabase = null;

        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@UserID", userID);
          sqlDatabase.AddInputParameter("@SlideID", slide.SlideID);
          sqlDatabase.AddInputParameter("@Name", slide.Name);
          sqlDatabase.AddInputParameter("@Creator", slide.Creator);
          sqlDatabase.AddInputParameter("@Caption", slide.Caption);
          sqlDatabase.AddInputParameter("@UserGivenDate", slide.UserGivenDate);
          sqlDatabase.AddInputParameter("@URL", slide.URL);
          sqlDatabase.AddInputParameter("@DisplayDuration", slide.DisplayDuration);

          sqlDatabase.ExecuteNonQuery("dp_editSlide");       
        }
        catch (Exception exception)
        {
          Console.WriteLine(exception);
          throw exception;
        }
        finally
        {
          sqlDatabase.Dispose();
        }

        ts.Complete();
      }
    }

    public SlideProperties GetSlideProperties(int userID, int slideID)
    {
      SlideProperties slideProperties = new SlideProperties();

      SqlDataReader sqlDataReader = null;
      SqlDatabase sqlDatabase = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);
        sqlDatabase.AddInputParameter("@SlideID", slideID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getSlide");

        while (sqlDataReader.Read())
        {
          slideProperties.Name = sqlDataReader.GetString(sqlDataReader.GetOrdinal("SlideName"));
          slideProperties.Creator = GetStringValueIfNotNull(sqlDataReader, "Creator");
          slideProperties.Caption = GetStringValueIfNotNull(sqlDataReader, "Caption");
          slideProperties.URL = GetStringValueIfNotNull(sqlDataReader, "ClickThroughURL");
          slideProperties.UserGivenDate = GetNullableDateTimeValueIfNotNull(sqlDataReader, "UserGivenDate");
          slideProperties.DisplayDuration = (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("DisplayDuration"));
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }

      return slideProperties;
    }

    public void MoveRawContent(int userID, int oldFolderID, int newFolderID, List<int> contentIDList)
    {
      SqlDatabase sqlDatabase = null;

      using (TransactionScope ts = new TransactionScope())
      {
        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@UserID", userID);
          sqlDatabase.AddInputParameter("@OldFolderID", oldFolderID);
          sqlDatabase.AddInputParameter("@NewFolderID", newFolderID);
          sqlDatabase.AddInputParameter("@AssetContentID");

          foreach (int assetContentID in contentIDList)
          {
            sqlDatabase.EditInputParameter("@AssetContentID", assetContentID);

            sqlDatabase.ExecuteNonQuery("dp_editMoveAssetContent");
          }
        }
        catch (Exception exception)
        {
          Console.WriteLine(exception);
          throw exception;
        }
        finally
        {
          sqlDatabase.Dispose();
        } 

        ts.Complete();
      }
    }

    public void MoveSlideContent(int userID, int oldFolderID, int newFolderID, List<int> slideIDList)
    {
      SqlDatabase sqlDatabase = null;

      using (TransactionScope ts = new TransactionScope())
      {
        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@UserID", userID);
          sqlDatabase.AddInputParameter("@OldFolderID", oldFolderID);
          sqlDatabase.AddInputParameter("@NewFolderID", newFolderID);
          sqlDatabase.AddInputParameter("@SlideID");

          foreach (int slideID in slideIDList)
          {
            sqlDatabase.EditInputParameter("@SlideID", slideID);

            sqlDatabase.ExecuteNonQuery("dp_editMoveSlideContent");
          }
        }
        catch (Exception exception)
        {
          Console.WriteLine(exception);
          throw exception;
        }
        finally
        {
          sqlDatabase.Dispose();
        }

        ts.Complete();
      }
    }

    public void MovePCChannels(int userID, int oldPCID, int newPCID, List<int> channelIDList)
    {
      SqlDatabase sqlDatabase = null;

      using (TransactionScope ts = new TransactionScope())
      {
        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@UserID", userID);
          sqlDatabase.AddInputParameter("@OldPCID", oldPCID);
          sqlDatabase.AddInputParameter("@NewPCID", newPCID);
          sqlDatabase.AddInputParameter("@ChannelID");

          foreach (int channelID in channelIDList)
          {
            sqlDatabase.EditInputParameter("@ChannelID", channelID);

            sqlDatabase.ExecuteNonQuery("dp_editMovePCChannel");
          }
        }
        catch (Exception exception)
        {
          Console.WriteLine(exception);
          throw exception;
        }
        finally
        {
          sqlDatabase.Dispose();
        }

        ts.Complete();
      }
    }

    public void EditChannelSlideProperties(int userID, int channelSlideID, string url, float displayDuration, string scheduleString, string presentationString)
    {
      SqlDatabase sqlDatabase = null;

      using (TransactionScope ts = new TransactionScope())
      {
        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@UserID", userID);
          sqlDatabase.AddInputParameter("@ChannelSlideID", channelSlideID);
          sqlDatabase.AddInputParameter("@URL", url);
          sqlDatabase.AddInputParameter("@DisplayDuration", displayDuration);
          sqlDatabase.AddInputParameter("@Schedule", scheduleString);
          sqlDatabase.AddInputParameter("@PresentationConvertedSchedule", presentationString);

          sqlDatabase.ExecuteNonQuery("dp_editChannelSlide");
        }
        catch (Exception exception)
        {
          Console.WriteLine(exception);
          throw exception;
        }
        finally
        {
          sqlDatabase.Dispose();
        }

        ts.Complete();
      }
    }

    public ChannelSlideProperties GetChannelSlideProperties(int userID, int channelsSlideID)
    {
      ChannelSlideProperties channelSlideProperties = null;

      SqlDataReader sqlDataReader = null;
      SqlDatabase sqlDatabase = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);
        sqlDatabase.AddInputParameter("@ChannelsSlideID", channelsSlideID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getChannelSlideProperties");

        if (sqlDataReader.Read())
        {
          channelSlideProperties = new ChannelSlideProperties();
          channelSlideProperties.URL = GetStringValueIfNotNull(sqlDataReader, "ClickThroughURL");
          channelSlideProperties.DisplayDuration = (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("DisplayDuration"));
          channelSlideProperties.PresentationSchedule = GetStringValueIfNotNull(sqlDataReader, "PresentationConvertedSchedule");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());

        throw ex;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }

      return channelSlideProperties;
    }

    public void SetCategory(int userID, int channelID, int categoryID)
    {
      SqlDatabase sqlDatabase = null;

      using (TransactionScope ts = new TransactionScope())
      {
        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@UserID", userID);
          sqlDatabase.AddInputParameter("@ChannelID", channelID);
          sqlDatabase.AddInputParameter("@CategoryID", categoryID);

          sqlDatabase.ExecuteNonQuery("dp_editChannelCategory");
        }
        catch (Exception exception)
        {
          Console.WriteLine(exception);
          throw exception;
        }
        finally
        {
          sqlDatabase.Dispose();
        }

        ts.Complete();
      }
    
    }

    public void GetDataForPreviewFrames(int userID, string mediaType, int mediaID, out string subDir, 
      out string mediaGUID, out string fileNameWithSubdir, out PreviewType previewType)
    {
      string proc;

      switch (mediaType)
      {
        case "R":
          proc = "dp_getDataForPreviewAssetContent";
          break;
        case "S":
          proc = "dp_getDataForPreviewSlide";
          break;
        case "C":
          proc = "dp_getDataForPreviewChannelSlide";
          break;
        default:
          throw new ArgumentException("Invalid Media Type");
      }

      SqlDatabase sqlDatabase = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);
        sqlDatabase.AddInputParameter("@ID", mediaID);
        SqlParameter subDirParam = sqlDatabase.AddOutputParameter("@SubDir", System.Data.SqlDbType.Char, 1, null);
        SqlParameter mediaGUIDParam = sqlDatabase.AddOutputParameter("@MediaGUID", System.Data.SqlDbType.NVarChar, 50, null);
        SqlParameter fileNameWithSubdirParam = sqlDatabase.AddOutputParameter("@FilenameWithSubDir", System.Data.SqlDbType.NVarChar, 100, null);
        SqlParameter previewTypeParam = sqlDatabase.AddOutputParameter("@PreviewType", System.Data.SqlDbType.NVarChar, 10, null);

        sqlDatabase.ExecuteNonQuery(proc);

        subDir = (string)subDirParam.Value;
        mediaGUID = (string)mediaGUIDParam.Value;
        fileNameWithSubdir = (string)fileNameWithSubdirParam.Value;
        previewType = (PreviewType)Enum.Parse(typeof(PreviewType), (string)previewTypeParam.Value);
      }
      catch (Exception ex)
      {
        _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        throw ex;
      }
      finally
      {
        sqlDatabase.Dispose();
      }
    }

    public int GetUserExistsByUserCredentials(string emailAddress, string password, out string userGUID)
    {
      int result;
      userGUID = null;

      SqlDatabase sqlDatabase = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddReturnParameter();
        sqlDatabase.AddInputParameter("@EmailAddress", emailAddress);
        sqlDatabase.AddInputParameter("@Password", password);
        SqlParameter userGUIDParameter = sqlDatabase.AddOutputParameter("@UserGUID", System.Data.SqlDbType.NVarChar, 50, null);
        
        sqlDatabase.ExecuteNonQuery("dp_getUserExistsByUserCredentials");

        result = sqlDatabase.ReturnValue;

        if (userGUIDParameter.Value != DBNull.Value)
          userGUID = (string)userGUIDParameter.Value;
      }
      finally
      {
        sqlDatabase.Dispose();
      }

      return result;
    }

    public void GetExistingUserDetails(string userGUID, string password, out string firstName, out string lastName, 
      out string gender, out DateTime dob, out string country, out string state, out string townCity, 
      out string occupationSector, out string employmentLevel, out string annualHouseholdIncome)
    {
      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;

      firstName = null;
      lastName = null;
      gender = null;
      dob = new DateTime();
      country = null;
      state = null;
      townCity = null;
      occupationSector = null;
      employmentLevel = null;
      annualHouseholdIncome = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserGUID", userGUID);
        sqlDatabase.AddInputParameter("@Password", password);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getUserDetailsFromInstall");

        if (sqlDataReader.Read())
        {
          firstName = GetStringValueIfNotNull(sqlDataReader, "FirstName");
          lastName = GetStringValueIfNotNull(sqlDataReader, "LastName");
          gender = GetStringValueIfNotNull(sqlDataReader, "Gender");
          dob = GetDateTimeValueIfNotNull(sqlDataReader, "DOB");
          country = GetStringValueIfNotNull(sqlDataReader, "Country");
          state = GetStringValueIfNotNull(sqlDataReader, "State");
          townCity = GetStringValueIfNotNull(sqlDataReader, "TownCity");
          occupationSector = GetStringValueIfNotNull(sqlDataReader, "OccupationSector");
          employmentLevel = GetStringValueIfNotNull(sqlDataReader, "EmploymentLevel");
          annualHouseholdIncome = GetStringValueIfNotNull(sqlDataReader, "AnnualHouseholdIncome");
        }
      }
      catch (Exception ex)
      {
        // TODO: write to event log
        throw ex;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }
    }

    public void UpdateUserAccount(string emailAddress, string password, 
      string firstName, string lastName, string gender, DateTime dob, 
      string townCity, string state, string country, string occupationSector, string employmentLevel,
      string annualHouseholdIncome,
      AppData.ChannelSubscriptions channelSubscriptions,
      int softwareMajorVersionNumber,
      int softwareMinorVersionNumber,
      string machineGUID,
      string newPcName,
      string macAddress)
    {
      SqlDatabase sqlDatabase = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@EmailAddress", emailAddress);
        sqlDatabase.AddInputParameter("@Password", password);
        sqlDatabase.AddInputParameter("@FirstName", firstName);
        sqlDatabase.AddInputParameter("@LastName", lastName);
        sqlDatabase.AddInputParameter("@Gender", gender);
        sqlDatabase.AddInputParameter("@DOB", dob);
        sqlDatabase.AddInputParameter("@TownCity", townCity);
        sqlDatabase.AddInputParameter("@State", state);
        sqlDatabase.AddInputParameter("@Country", country);
        sqlDatabase.AddInputParameter("@OccupationSector", occupationSector);
        sqlDatabase.AddInputParameter("@EmploymentLevel", employmentLevel);
        sqlDatabase.AddInputParameter("@AnnualHouseholdIncome", annualHouseholdIncome);

        sqlDatabase.ExecuteNonQuery("dp_editUserDetails");

        sqlDatabase.ClearParameters();

        sqlDatabase.AddInputParameter("@EmailAddress", emailAddress);
        sqlDatabase.AddInputParameter("@MachineGUID", machineGUID);
        sqlDatabase.AddInputParameter("@MACAddress", macAddress);
        sqlDatabase.AddInputParameter("@PCName", newPcName);
        sqlDatabase.AddInputParameter("@SoftwareMajorVersionNumber", softwareMajorVersionNumber);
        sqlDatabase.AddInputParameter("@SoftwareMinorVersionNumber", softwareMinorVersionNumber);

        sqlDatabase.ExecuteNonQuery("dp_addPCByGUID");

        sqlDatabase.ClearParameters();

        // add sunbcriptions
        if (channelSubscriptions.SubscriptionSet != null)
        {
          sqlDatabase.AddInputParameter("@EmailAddress", emailAddress);
          sqlDatabase.AddInputParameter("@MachineGUID", machineGUID);
          sqlDatabase.AddInputParameter("@ChannelGUID");
          sqlDatabase.AddInputParameter("@ChannelWeighting");

          sqlDatabase.AddReturnParameter();

          foreach (OxigenIIAdvertising.AppData.ChannelSubscription cs in channelSubscriptions.SubscriptionSet)
          {
            sqlDatabase.EditInputParameter("@ChannelGUID", cs.ChannelGUID);
            sqlDatabase.EditInputParameter("@ChannelWeighting", cs.ChannelWeightingUnnormalised);

            sqlDatabase.ExecuteNonQuery("dp_addPCStreamByEmailAndGUIDsNoLinkNoDirty");

            if (sqlDatabase.ReturnValue < 0)
            {
              switch (sqlDatabase.ReturnValue)
              {
                case -1:
                  _eventLog.WriteEntry("An attempt was made to access dp_addPCStreamByGUIDsNoLinkNoDirty using an invalid user GUID. The user email address is " + emailAddress, EventLogEntryType.Warning);
                  throw new ActionNotSupportedException("Invalid user. Check event logs.");
                case -2:
                  _eventLog.WriteEntry("An attempt was made to access dp_addPCStreamByGUIDsNoLinkNoDirty using an invalid Machine GUID. The user GUID is " + machineGUID, EventLogEntryType.Warning);
                  break;
                case -3:
                  _eventLog.WriteEntry("An attempt was made to access dp_addPCStreamByGUIDsNoLinkNoDirty using an invalid Channel GUID. The channel GUID is " + cs.ChannelGUID, EventLogEntryType.Warning);
                  break;
                case -4:
                  _eventLog.WriteEntry("An attempt was made to access dp_addPCStreamByGUIDsNoLinkNoDirty using non-linked user GUID and maching GUID. The user Email Address is " + emailAddress + " and the machine GUID is " + machineGUID + ".", EventLogEntryType.Warning);
                  break;
              }

              return;
            }
          }
        }

        sqlDatabase.ClearParameters();

        sqlDatabase.AddInputParameter("@EmailAddress", emailAddress);
        sqlDatabase.AddInputParameter("@MachineGUID", machineGUID);
        sqlDatabase.ExecuteNonQuery("dp_editPCMakeDirtyEmailAndGUID");
      }
      catch (Exception ex)
      {
        _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        throw ex;
      }
      finally
      {
        sqlDatabase.Dispose();
      }
    }

    public List<PcInfo> GetPcListForInstallerEmail(string emailAddress)
    {
      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;
      List<PcInfo> pcs = new List<PcInfo>();

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@EmailAddress", emailAddress);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getPCsToLinkByEmail");

        while (sqlDataReader.Read())
        {
          PcInfo pcInfo = new PcInfo();
          pcInfo.PcID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("PC_ID"));
          pcInfo.PcName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("PcName"));

          pcs.Add(pcInfo);
        }
      }
      catch (Exception ex)
      {
        // TODO: write to event log
        throw ex;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }

      return pcs;
    }

    public List<PcInfo> GetPcListForInstallerGUID(string userGUID)
    {
      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;
      List<PcInfo> pcs = new List<PcInfo>();

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserGUID", userGUID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getPCsToLinkByGUID");

        while (sqlDataReader.Read())
        {
          PcInfo pcInfo = new PcInfo();
          pcInfo.PcID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("PC_ID"));
          pcInfo.PcName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("PcName"));

          pcs.Add(pcInfo);
        }
      }
      catch (Exception ex)
      {
        // TODO: write to event log
        throw ex;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }

      return pcs;
    }

    //TODO: delete
    public void LinkPCAndSubscriptionsExistingPC(string userGUID, int pcID, string machineGUID, string macAddress,
      int softwareMajorVersionNumber, int softwareMinorVersionNumber, 
      OxigenIIAdvertising.AppData.ChannelSubscriptions channelSubscriptions)
    {
      SqlDatabase sqlDatabase = null;

      using (TransactionScope ts = new TransactionScope())
      {
        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@UserGUID", userGUID);
          sqlDatabase.AddInputParameter("@MachineGUID", machineGUID);
          sqlDatabase.AddInputParameter("@MACAddress", macAddress);
          sqlDatabase.AddInputParameter("@SoftwareMajorVersionNumber", softwareMajorVersionNumber);
          sqlDatabase.AddInputParameter("@SoftwareMinorVersionNumber", softwareMinorVersionNumber);
          sqlDatabase.AddInputParameter("@PCID", pcID);

          sqlDatabase.ExecuteNonQuery("dp_removePCSubscriptionsLinkDirty");

          sqlDatabase.ClearParameters();

          if (channelSubscriptions.SubscriptionSet != null)
          {
            sqlDatabase.AddInputParameter("@UserGUID", userGUID);
            sqlDatabase.AddInputParameter("@MachineGUID", machineGUID);
            sqlDatabase.AddInputParameter("@ChannelGUID");
            sqlDatabase.AddInputParameter("@ChannelWeighting");

            sqlDatabase.AddReturnParameter();

            foreach (OxigenIIAdvertising.AppData.ChannelSubscription cs in channelSubscriptions.SubscriptionSet)
            {
              sqlDatabase.EditInputParameter("@ChannelGUID", cs.ChannelGUID);
              sqlDatabase.EditInputParameter("@ChannelWeighting", cs.ChannelWeightingUnnormalised);

              sqlDatabase.ExecuteNonQuery("dp_addPCStreamByGUIDsNoLinkNoDirty");

              if (sqlDatabase.ReturnValue < 0)
              {
                switch (sqlDatabase.ReturnValue)
                {
                  case -1:
                    _eventLog.WriteEntry("An attempt was made to access dp_addPCStreamByGUIDsNoLinkNoDirty using an invalid user GUID. The user GUID is " + userGUID, EventLogEntryType.Information);
                    throw new ActionNotSupportedException("Invalid user. Check event logs.");
                  case -2:
                    _eventLog.WriteEntry("An attempt was made to access dp_addPCStreamByGUIDsNoLinkNoDirty using an invalid Machine GUID. The user GUID is " + machineGUID, EventLogEntryType.Information);
                    break;
                  case -3:
                    _eventLog.WriteEntry("An attempt was made to access dp_addPCStreamByGUIDsNoLinkNoDirty using an invalid Channel GUID. The channel GUID is " + cs.ChannelGUID, EventLogEntryType.Information);
                    break;
                  case -4:
                    _eventLog.WriteEntry("An attempt was made to access dp_addPCStreamByGUIDsNoLinkNoDirty using non-linked user GUID and maching GUID. The user GUID is " + userGUID + " and the machine GUID is " + machineGUID + ".", EventLogEntryType.Warning);
                    break;
                  default:
                    Console.WriteLine(sqlDatabase.ReturnValue);
                    break;
                }

                return;
              }
            }

            sqlDatabase.ClearParameters();
          }

          sqlDatabase.AddInputParameter("@UserGUID", userGUID);
          sqlDatabase.AddInputParameter("@MachineGUID", machineGUID);
          sqlDatabase.ExecuteNonQuery("dp_editPCMakeDirtyGUID");
        }
        catch (Exception ex)
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
          throw ex;
        }
        finally
        {
          sqlDatabase.Dispose();
        }

        ts.Complete();
      }
    }

    [OperationBehavior(TransactionScopeRequired = true)]
    public void RegisterNewUser(string emailAddress, string password, string firstName, string lastName, string gender,
      DateTime dob, string townCity, string state, string country, string occupationSector, 
      string employmentLevel, string annualHouseholdIncome, string userGUID, string machineGUID,
      int softwareMajorVersionNumber, int softwareMinorVersionNumber, string macAddress,
      string machineName, AppData.ChannelSubscriptions channelSubscriptions, out string channelGUID)
    {
      SqlDatabase sqlDatabase = null;
      channelGUID = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@EmailAddress", emailAddress);
        sqlDatabase.AddInputParameter("@Password", password);
        sqlDatabase.AddInputParameter("@Gender", gender);
        sqlDatabase.AddInputParameter("@FirstName", firstName);
        sqlDatabase.AddInputParameter("@LastName", lastName);
        sqlDatabase.AddInputParameter("@DOB", dob);
        sqlDatabase.AddInputParameter("@TownCity", townCity);
        sqlDatabase.AddInputParameter("@State", state);
        sqlDatabase.AddInputParameter("@Country", country);
        sqlDatabase.AddInputParameter("@OccupationSector", occupationSector);
        sqlDatabase.AddInputParameter("@EmploymentLevel", employmentLevel);
        sqlDatabase.AddInputParameter("@AnnualHouseholdIncome", annualHouseholdIncome);
        sqlDatabase.AddInputParameter("@UserGUID", userGUID);
        sqlDatabase.AddInputParameter("@MachineGUID", machineGUID);
        sqlDatabase.AddInputParameter("@SoftwareMajorVersionNumber", softwareMajorVersionNumber);
        sqlDatabase.AddInputParameter("@SoftwareMinorVersionNumber", softwareMinorVersionNumber);
        sqlDatabase.AddInputParameter("@MACAddress", macAddress);
        sqlDatabase.AddInputParameter("@MachineName", machineName);
        SqlParameter channelGUIDParameter = sqlDatabase.AddOutputParameter("@ChannelGUID", System.Data.SqlDbType.NVarChar, 50, null);

        sqlDatabase.ExecuteNonQuery("dp_addUserAddPCLinkPC");

        channelGUID = (string)channelGUIDParameter.Value;

        sqlDatabase.ClearParameters();

        // add sunbcriptions
        if (channelSubscriptions.SubscriptionSet != null)
        {
          sqlDatabase.AddInputParameter("@UserGUID", userGUID);
          sqlDatabase.AddInputParameter("@MachineGUID", machineGUID);
          sqlDatabase.AddInputParameter("@ChannelGUID");
          sqlDatabase.AddInputParameter("@ChannelWeighting");

          sqlDatabase.AddReturnParameter();

          foreach (OxigenIIAdvertising.AppData.ChannelSubscription cs in channelSubscriptions.SubscriptionSet)
          {
            sqlDatabase.EditInputParameter("@ChannelGUID", cs.ChannelGUID);
            sqlDatabase.EditInputParameter("@ChannelWeighting", cs.ChannelWeightingUnnormalised);

            sqlDatabase.ExecuteNonQuery("dp_addPCStreamByGUIDsNoLinkNoDirty");

            if (sqlDatabase.ReturnValue < 0)
            {
              switch (sqlDatabase.ReturnValue)
              {
                case -1:
                  Console.WriteLine("An attempt was made to access dp_addPCStreamByGUIDsNoLinkNoDirty using an invalid user GUID. The user GUID is " + userGUID);
                  throw new ActionNotSupportedException("Invalid user. Check event logs.");
                case -2:
                  Console.WriteLine("An attempt was made to access dp_addPCStreamByGUIDsNoLinkNoDirty using an invalid Machine GUID. The user GUID is " + machineGUID);
                  break;
                case -3:
                  Console.WriteLine("An attempt was made to access dp_addPCStreamByGUIDsNoLinkNoDirty using an invalid Channel GUID. The channel GUID is " + cs.ChannelGUID);
                  break;
                case -4:
                  Console.WriteLine("An attempt was made to access dp_addPCStreamByGUIDsNoLinkNoDirty using non-linked user GUID and maching GUID. The user GUID is " + userGUID + " and the machine GUID is " + machineGUID + ".");
                  break;
              }

              return;
            }
          }
        }

        sqlDatabase.ClearParameters();

        sqlDatabase.AddInputParameter("@UserGUID", userGUID);
        sqlDatabase.AddInputParameter("@MachineGUID", machineGUID);
        sqlDatabase.ExecuteNonQuery("dp_editPCMakeDirtyGUID");
      }
      catch (Exception ex)
      {
        _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        throw ex;
      }
      finally
      {
        sqlDatabase.Dispose();
      }
    }

    public bool EmailAddressExists(string emailAddress)
    {
      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@EmailAddress", emailAddress);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getEmailAddressExists");

        return sqlDataReader.HasRows;          
      }
      catch (Exception ex)
      {
        // TODO: write to event log
        throw ex;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }
    }

    public void EditSubscriptionsByGUID(string userGUID, string machineGUID, OxigenIIAdvertising.AppData.ChannelSubscriptions channelSubscriptions)
    {
      SqlDatabase sqlDatabase = null;
      
      using (TransactionScope ts = new TransactionScope())
      {
        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@UserGUID", userGUID);
          sqlDatabase.AddInputParameter("@MachineGUID", machineGUID);

          sqlDatabase.ExecuteNonQuery("dp_removePCSubscriptionsDirty");

          sqlDatabase.AddInputParameter("@ChannelGUID");
          sqlDatabase.AddInputParameter("@ChannelWeighting");

          sqlDatabase.AddReturnParameter();

          foreach (OxigenIIAdvertising.AppData.ChannelSubscription cs in channelSubscriptions.SubscriptionSet)
          {
            sqlDatabase.EditInputParameter("@ChannelGUID", cs.ChannelGUID);
            sqlDatabase.EditInputParameter("@ChannelWeighting", cs.ChannelWeightingUnnormalised);

            sqlDatabase.ExecuteNonQuery("dp_addPCStreamByGUIDsNoLinkNoDirty");

            if (sqlDatabase.ReturnValue < 0)
            {
              switch (sqlDatabase.ReturnValue)
              {
                case -1:
                  _eventLog.WriteEntry("An attempt was made to access dp_addPCStreamByGUIDsNoLinkNoDirty using an invalid user GUID. The user GUID is " + userGUID, EventLogEntryType.Information);
                  throw new ActionNotSupportedException("Invalid user. Check event logs.");
                case -2:
                  _eventLog.WriteEntry("An attempt was made to access dp_addPCStreamByGUIDsNoLinkNoDirty using an invalid Machine GUID. The user GUID is " + machineGUID, EventLogEntryType.Information);
                  break;
                case -3:
                  _eventLog.WriteEntry("An attempt was made to access dp_addPCStreamByGUIDsNoLinkNoDirty using an invalid Channel GUID. The channel GUID is " + cs.ChannelGUID, EventLogEntryType.Information);
                  break;
                case -4:
                  _eventLog.WriteEntry("An attempt was made to access dp_addPCStreamByGUIDsNoLinkNoDirty using non-linked user GUID and maching GUID. The user GUID is " + userGUID + " and the machine GUID is " + machineGUID + ".", EventLogEntryType.Warning);
                  break;
              }
            }
          }

          sqlDatabase.RemoveParameter("@ChannelGUID");
          sqlDatabase.RemoveParameter("@ChannelWeighting");

          sqlDatabase.ExecuteNonQuery("dp_editPCMakeDirtyGUID");
        }
        finally
        {
          sqlDatabase.Dispose();
        }

        ts.Complete();
      }
    }

    public OxigenIIAdvertising.AppData.ChannelSubscriptions GetPCSubscriptionsByPCID(string userGUID, int pcID)
    {
      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;
      OxigenIIAdvertising.AppData.ChannelSubscriptions channelSubscriptions = new OxigenIIAdvertising.AppData.ChannelSubscriptions();

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserGUID", userGUID);
        sqlDatabase.AddInputParameter("@pcID", pcID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getPCChannelsByUserGUIDPCID");

        while (sqlDataReader.Read())
        {
          OxigenIIAdvertising.AppData.ChannelSubscription subscription = new OxigenIIAdvertising.AppData.ChannelSubscription();

          subscription.ChannelGUID = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ChannelGUID"));
          subscription.ChannelName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ChannelName"));

          channelSubscriptions.SubscriptionSet.Add(subscription);
        }
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }

      return channelSubscriptions;
    }

    public OxigenIIAdvertising.AppData.ChannelSubscriptions GetPCSubscriptionsByMachineGUID(string userGUID, string machineGUID)
    {
      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;
      OxigenIIAdvertising.AppData.ChannelSubscriptions channelSubscriptions = new OxigenIIAdvertising.AppData.ChannelSubscriptions();

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserGUID", userGUID);
        sqlDatabase.AddInputParameter("@MachineGUID", machineGUID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getPCChannelsByUserGUIDMachineGUID");

        while (sqlDataReader.Read())
        {
          OxigenIIAdvertising.AppData.ChannelSubscription subscription = new OxigenIIAdvertising.AppData.ChannelSubscription();

          subscription.ChannelGUID = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ChannelGUID"));
          subscription.ChannelName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ChannelName"));

          channelSubscriptions.SubscriptionSet.Add(subscription);
        }
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }

      return channelSubscriptions;
    }

    public bool GetMatchedUserGUID(string userGUID)
    {
      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserGUID", userGUID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getUserGUIDExists");

        return sqlDataReader.HasRows;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }
    }

    public bool GetMatchedMachineGUID(string userGUID, string machineGUID)
    {
      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserGUID", userGUID);
        sqlDatabase.AddInputParameter("@MachineGUID", machineGUID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getMachineGUIDExists");

        return sqlDataReader.HasRows;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }
    }

    public void CompareMACAddresses(string macAddressClient, string userGUID, int softwareMajorVersionNumber,
      int softwareMinorVersionNumber, out string newMachineGUID, out bool bMatch)
    {
      SqlDatabase sqlDatabase = null;

      newMachineGUID = System.Guid.NewGuid().ToString().ToUpper() + "_" + GetRandomLetter();

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@MacAddressClient", macAddressClient);
        sqlDatabase.AddInputParameter("@UserGUID", userGUID);
        sqlDatabase.AddInputParameter("@NewMachineGUID", newMachineGUID);
        sqlDatabase.AddInputParameter("@SoftwareMajorVersionNumber", softwareMajorVersionNumber);
        sqlDatabase.AddInputParameter("@SoftwareMinorVersionNumber", softwareMinorVersionNumber);
        SqlParameter bMatchParameter = sqlDatabase.AddOutputParameter("@bMatch", System.Data.SqlDbType.Bit, 1, null);

        sqlDatabase.ExecuteNonQuery("dp_editCompareMacAddressesLink");

        bMatch = (bool)bMatchParameter.Value;
      }
      catch (Exception ex)
      {
        _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);

        throw ex;
      }
      finally
      {
        sqlDatabase.Dispose();
      }
    }

    public void RemovePCFromUninstall(string userGUID, string machineGUID)
    {
      SqlDatabase sqlDatabase = null;

      using (TransactionScope ts = new TransactionScope())
      {
        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@UserGUID", userGUID);
          sqlDatabase.AddInputParameter("@MachineGUID", machineGUID);

          sqlDatabase.ExecuteNonQuery("dp_removePCFromUninstall");
        }
        catch (Exception ex)
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);

          throw ex;
        }
        finally
        {
          sqlDatabase.Dispose();
        }

        ts.Complete();
      }
    }

    private char GetRandomLetter()
    {
      Random random = new Random();

      return ((char)((short)'A' + random.Next(26)));
    }

    public List<PC> GetSubscriptionsNotRegistered(string PcProfileToken)
    {
      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;

      List<PC> pcs = new List<PC>();

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddReturnParameter();
        sqlDatabase.AddInputParameter("@PcProfileToken", PcProfileToken);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getPcSubscriptionsNotRegistered");

        int previousPCID = -1;
        int currentPCID = -1;
        PC pc = null;

        while (sqlDataReader.Read())
        {
          currentPCID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("PC_ID"));

          if (previousPCID != currentPCID)
          {
            previousPCID = currentPCID;

            pc = new PC();
            pc.PCID = currentPCID;
            pc.Name = sqlDataReader.GetString(sqlDataReader.GetOrdinal("PCName"));
            pc.Channels = new List<ChannelListChannel>();
            pcs.Add(pc);
          }

          ChannelListChannel channel = new ChannelListChannel();
          channel.ChannelID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CHANNEL_ID"));
          channel.ChannelGUID = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ChannelGUID"));
          channel.ChannelName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ChannelName"));
          channel.ChannelWeightingUnnormalised = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("ChannelWeighting"));

          pc.Channels.Add(channel);
        }
      }
      catch (Exception ex)
      {
        //_eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        Console.WriteLine(ex.ToString());
        throw ex;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }

      return pcs;
    }

    public List<PC> GetSubscriptions(int userID)
    {
      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;

      List<PC> pcs = new List<PC>();

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddReturnParameter();
        sqlDatabase.AddInputParameter("@UserID", userID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getPcSubscriptions");

        int previousPCID = -1;
        int currentPCID = -1;
        PC pc = null;

        while (sqlDataReader.Read())
        {
          currentPCID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("PC_ID"));

          if (previousPCID != currentPCID)
          {
            previousPCID = currentPCID;

            pc = new PC();
            pc.PCID = currentPCID;
            pc.Name = sqlDataReader.GetString(sqlDataReader.GetOrdinal("PCName"));
            pc.Channels = new List<ChannelListChannel>();
            pcs.Add(pc);
          }

          ChannelListChannel channel = new ChannelListChannel();
          channel.ChannelID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CHANNEL_ID"));
          channel.ChannelGUID = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ChannelGUID"));
          channel.ChannelName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ChannelName"));
          channel.ChannelWeightingUnnormalised = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("ChannelWeighting"));

          pc.Channels.Add(channel);
        }
      }
      catch (Exception ex)
      {
        //_eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        Console.WriteLine(ex.ToString());
        throw ex;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }

      return pcs;
    }

    public List<Channel> GetChannelsByUserID(int userID)
    {
      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;

      List<Channel> channels = new List<Channel>();

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddReturnParameter();
        sqlDatabase.AddInputParameter("@UserID", userID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getChannelsByUserID");

        while (sqlDataReader.Read())
        {
          Channel channel = new Channel();
          channel.ChannelID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CHANNEL_ID"));
          channel.ChannelGUID = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ChannelGUID"));
          channel.ChannelName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ChannelName"));

          channels.Add(channel);
        }
      }
      catch (Exception ex)
      {
        //_eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        Console.WriteLine(ex.ToString());
        throw ex;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }

      return channels;
    }

    public Channel GetChannelToDownload(int userID, int channelID)
    {
      Channel channel = new Channel();

      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddReturnParameter();
        sqlDatabase.AddInputParameter("@UserID", userID);
        sqlDatabase.AddInputParameter("@ChannelID", channelID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getChannelInfo");

        if (sqlDataReader.Read())
        {
          channel.ChannelID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CHANNEL_ID")); 
          channel.ChannelGUID = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ChannelGUID"));
          channel.ChannelName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ChannelName"));
        }
      }
      catch (Exception ex)
      {
        //_eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        Console.WriteLine(ex.ToString());
        throw ex;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }

      return channel;
    }

    public void RemoveTemporaryPCProfilesNotRegistered(string pcProfileToken)
    {
      SqlDatabase sqlDatabase = null;

      using (TransactionScope ts = new TransactionScope())
      {
        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@PcProfileToken", pcProfileToken);

          sqlDatabase.ExecuteNonQuery("dp_removeTemporaryPcProfilesNotRegistered");
        }
        catch (Exception ex)
        {
          //_eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
          Console.WriteLine(ex.ToString());
          throw ex;
        }
        finally
        {
          sqlDatabase.Dispose();
        }

        ts.Complete();
      }
    }

    public void RemoveTemporaryPCProfiles(int userID)
    {
      SqlDatabase sqlDatabase = null;

      using (TransactionScope ts = new TransactionScope())
      {
        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@UserID", userID);

          sqlDatabase.ExecuteNonQuery("dp_removeTemporaryPcProfiles");
        }
        catch (Exception ex)
        {
          //_eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
          Console.WriteLine(ex.ToString());
          throw ex;
        }
        finally
        {
          sqlDatabase.Dispose();
        }

        ts.Complete();
      }
    }

    public void SetPassword(string email, string newPassword)
    {
      SqlDatabase sqlDatabase = null;

      using (TransactionScope ts = new TransactionScope())
      {
        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@Email", email);
          sqlDatabase.AddInputParameter("@NewPassword", newPassword);

          sqlDatabase.ExecuteNonQuery("dp_editUserPassword");
        }
        catch (Exception ex)
        {
          //_eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
          Console.WriteLine(ex.ToString());
          throw ex;
        }
        finally
        {
          sqlDatabase.Dispose();
        }

        ts.Complete();
      }
    }

    public List<LocationNameValue> GetCountries()
    {
      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;

      List<LocationNameValue> countries = new List<LocationNameValue>();

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getCountries");

        while (sqlDataReader.Read())
        {
          countries.Add(new LocationNameValue((sqlDataReader.GetString(sqlDataReader.GetOrdinal("LocationName"))),
            sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("LOCATION_ID")), true));
        }
      }
      catch (Exception ex)
      {
        //_eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        Console.WriteLine(ex.ToString());
        throw ex;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }

      return countries;
    }

    public UserDetails GetUserDetails(int userID)
    {
      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;

      UserDetails userDetails = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getUserDetailsFromWebsite");

        if (sqlDataReader.Read())
        {
          userDetails = new UserDetails()
          {
            EmailAddress = sqlDataReader.GetString(sqlDataReader.GetOrdinal("Username")), // ones e-mail address is one's username
            FirstName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("FirstName")),
            LastName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("LastName")),
            Dob = GetDateTimeValueIfNotNull(sqlDataReader, "DOB"),
            Gender = GetStringValueIfNotNull(sqlDataReader, "Gender"),
            CountryID = GetInt32ValueIfNotNull(sqlDataReader, "CountryID"),
            StateID = GetInt32ValueIfNotNull(sqlDataReader, "StateID"),
            TownCityID = GetInt32ValueIfNotNull(sqlDataReader, "TownCityID"),
            EmploymentLevelID = GetInt32ValueIfNotNull(sqlDataReader, "EmploymentLevelID"),
            OccupationSectorID = GetInt32ValueIfNotNull(sqlDataReader, "OccupationSectorID"),
            AnnualHouseholdIncomeID = GetInt32ValueIfNotNull(sqlDataReader, "AnnualHouseholdIncomeID")
          };
        }
      }
      catch (Exception ex)
      {
    //    _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        Console.WriteLine(ex.ToString());
        throw ex;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }

      return userDetails;
    }

    public List<LocationNameValue> GetChildGeoTTNodes(int parentLocationID)
    {
      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;

      List<LocationNameValue> childGeoTTNodes = new List<LocationNameValue>();

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@ParentLocationID", parentLocationID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getChildGeoTTNodes");

        if (!sqlDataReader.HasRows)
          return null;

        while (sqlDataReader.Read())
        {
          childGeoTTNodes.Add(new LocationNameValue((sqlDataReader.GetString(sqlDataReader.GetOrdinal("LocationName"))),
            sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("LOCATION_ID")),
            sqlDataReader.GetBoolean(sqlDataReader.GetOrdinal("bHasChildren"))));
        }
      }
      catch (Exception ex)
      {
        _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        Console.WriteLine(ex.ToString());
        throw ex;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }

      return childGeoTTNodes;
    }

    public List<KeyValuePair<int, string>> GetSocioEconomicStatuses(string seType)
    {
      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;

      List<KeyValuePair<int, string>> socioEconomicStatuses = new List<KeyValuePair<int, string>>();

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@Type", seType);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getSocioEconomicStatuses");

        if (!sqlDataReader.HasRows)
          return null;

        while (sqlDataReader.Read())
        {
          KeyValuePair<int, string> socioEconomicStatus = new KeyValuePair<int, string>(
            sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("SOCIOECONOMICSTATUS_ID")),
            sqlDataReader.GetString(sqlDataReader.GetOrdinal("SocioEconomicValue")));
          
          socioEconomicStatuses.Add(socioEconomicStatus);
        }
      }
      catch (Exception ex)
      {
       // _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        Console.WriteLine(ex.ToString());
        throw ex;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }

      return socioEconomicStatuses;
    }

    public void GetChannelCreatorDetailsForPasswordRequest(int channelID, out string channelCreatorEmailAddress, 
      out string channelCreatorName, out string channelName)
    {
      channelCreatorEmailAddress = null;
      channelCreatorName = null;
      channelName = null;

      SqlDatabase sqlDatabase = null;
      SqlDataReader sqlDataReader = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@ChannelID", channelID);

        sqlDataReader = sqlDatabase.ExecuteReader("dp_getChannelCreatorDetailsForPasswordRequest");

        if (sqlDataReader.Read())
        {
          channelCreatorEmailAddress = sqlDataReader.GetString(sqlDataReader.GetOrdinal("EmailAddress"));
          channelCreatorName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("FirstName"));
          channelName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ChannelName"));
        }
      }
      catch (Exception ex)
      {
        // _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        Console.WriteLine(ex.ToString());
        throw ex;
      }
      finally
      {
        if (sqlDataReader != null && !sqlDataReader.IsClosed)
          sqlDataReader.Dispose();

        sqlDatabase.Dispose();
      }
    }

    public bool EditUserDetails(int userID,
      string emailAddress, 
      string password, 
      string firstName, 
      string lastName, 
      string gender, 
      DateTime dob, 
      int townCityID, 
      int occupationSectorID, 
      int employmentLevelID,
      int annualHouseholdIncomeID,
      bool bUpdatePassword)
    {
      SqlDatabase sqlDatabase = null;
      bool bEmailExists = false;

      using (TransactionScope ts = new TransactionScope())
      {
        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@UserID", userID);
          sqlDatabase.AddInputParameter("@EmailAddress", emailAddress);
          sqlDatabase.AddInputParameter("@Password", password);
          sqlDatabase.AddInputParameter("@FirstName", firstName);
          sqlDatabase.AddInputParameter("@LastName", lastName);
          sqlDatabase.AddInputParameter("@Gender", gender);
          sqlDatabase.AddInputParameter("@DOB", dob);
          sqlDatabase.AddInputParameter("@TownCityID", townCityID);
          sqlDatabase.AddInputParameter("@OccupationSectorID", occupationSectorID);
          sqlDatabase.AddInputParameter("@EmploymentLevelID", employmentLevelID);
          sqlDatabase.AddInputParameter("@AnnualHouseholdIncomeID", annualHouseholdIncomeID);
          sqlDatabase.AddInputParameter("@bUpdatePassword", bUpdatePassword);

          SqlParameter emailExistParam = sqlDatabase.AddOutputParameter("@bEmailAddressExists", System.Data.SqlDbType.Bit, 1, null);
          
          sqlDatabase.ExecuteNonQuery("dp_editUserDetailsFromWebsite");

          bEmailExists = (bool)emailExistParam.Value;
          
        }
        catch (Exception ex)
        {
          //_eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
          Console.WriteLine(ex.ToString());
          throw ex;
        }
        finally
        {
          sqlDatabase.Dispose();
        }

        ts.Complete();
      }

      if (bEmailExists)
         return true;

      return false;
    }

    [OperationBehavior(TransactionScopeRequired = true)]
    public void GetUserAccountAssets(int userID, out bool bHasLinkedPCs, out HashSet<AssetContent> assetContentFiles,
      out HashSet<SlideContent> slideFiles, out HashSet<string> channelThumbnails)
    {
      SqlDatabase sqlDatabase = null;
      SqlDataReader hasRealPCsReader = null;
      SqlDataReader assetContentReader = null;
      SqlDataReader slideReader = null;
      SqlDataReader channelThumbnailReader = null;

      assetContentFiles = new HashSet<AssetContent>();
      slideFiles = new HashSet<SlideContent>();
      channelThumbnails = new HashSet<string>();

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@UserID", userID);

        hasRealPCsReader = sqlDatabase.ExecuteReader("dp_getUserHasRealPCs");

        if (hasRealPCsReader.HasRows)
        {
          bHasLinkedPCs = true;
          return;
        }
        else
          bHasLinkedPCs = false;

        hasRealPCsReader.Dispose();

        assetContentReader = sqlDatabase.ExecuteReader("dp_getAssetContentFilenamesToDelete");

        while (assetContentReader.Read())
        {
          AssetContent ac = new AssetContent()
          {
            ImagePathWinFS = assetContentReader.GetString(assetContentReader.GetOrdinal("ImagePathWinFS")),
            FileName = assetContentReader.GetString(assetContentReader.GetOrdinal("Filename")),
            FileNameNoPath = assetContentReader.GetString(assetContentReader.GetOrdinal("FileNameNoPath"))
          };

          assetContentFiles.Add(ac);
        }

        assetContentReader.Dispose();

        slideReader = sqlDatabase.ExecuteReader("dp_getSlideFilenamesToDelete");

        while (slideReader.Read())
        {
          SlideContent sc = new SlideContent()
          {
            ImagePathWinFS = slideReader.GetString(slideReader.GetOrdinal("ImagePathWinFS")),
            FileName = slideReader.GetString(slideReader.GetOrdinal("Filename")),
            FileNameNoPath = slideReader.GetString(slideReader.GetOrdinal("FileNameNoPath"))
          };

          slideFiles.Add(sc);
        }

        slideReader.Dispose();

        channelThumbnailReader = sqlDatabase.ExecuteReader("dp_getChannelThumbnailsToDelete");

        while (channelThumbnailReader.Read())
        {
          if (!channelThumbnailReader.IsDBNull(channelThumbnailReader.GetOrdinal("ImagePath")))
            channelThumbnails.Add(channelThumbnailReader.GetString(channelThumbnailReader.GetOrdinal("ImagePath")));
        }

        channelThumbnailReader.Dispose();
      }
      catch (Exception ex)
      {
        _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        Console.WriteLine(ex.ToString());
        throw ex;
      }
      finally
      {
        if (hasRealPCsReader != null && !hasRealPCsReader.IsClosed)
          hasRealPCsReader.Dispose();

        if (assetContentReader != null && !assetContentReader.IsClosed)
          assetContentReader.Dispose();

        if (slideReader != null && !slideReader.IsClosed)
          slideReader.Dispose();

        if (channelThumbnailReader != null && !channelThumbnailReader.IsClosed)
          channelThumbnailReader.Dispose();

        sqlDatabase.Dispose();
      }
    }

    [OperationBehavior(TransactionScopeRequired = true)]
    public void RemoveUserAccount(int userID)
    {
      SqlDatabase sqlDatabase = null;

        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@UserID", userID);

          sqlDatabase.ExecuteNonQuery("dp_removeUserAccount");
        }
        catch (Exception ex)
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
          throw ex;
        }
        finally
        {
          sqlDatabase.Dispose();
        }
    }

    public string CreatePCIfNotExists(string userGUID, string macAddress, string machineName, int majorVersionNumber, int minorVersionNumber)
    {
      string machineGUID;

      SqlDatabase sqlDatabase = null;

      using (TransactionScope ts = new TransactionScope())
      {
        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@UserGUID", userGUID);
          sqlDatabase.AddInputParameter("@MacAddress", macAddress);
          sqlDatabase.AddInputParameter("@MachineName", machineName);
          sqlDatabase.AddInputParameter("@MajorVersionNumber", majorVersionNumber);
          sqlDatabase.AddInputParameter("@MinorVersionNumber", minorVersionNumber);
          SqlParameter machineGUIDParam = sqlDatabase.AddOutputParameter("@MachineGUID", System.Data.SqlDbType.NVarChar, 50, null);

          sqlDatabase.ExecuteNonQuery("dp_createPcIfNotExists");

          machineGUID = (string)machineGUIDParam.Value;
        }
        catch (Exception ex)
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);

          throw ex;
        }
        finally
        {
          sqlDatabase.Dispose();
        }

        ts.Complete();
      }

      return machineGUID;
    }

    public string AddSubscriptionsAndNewPC(string userGUID, 
      string macAddress, 
      string machineName, 
      int majorVersionNumber, 
      int minorVersionNumber, 
      string[][] subscriptions)
    {
      string machineGUID;

      SqlDatabase sqlDatabase = null;

      using (TransactionScope ts = new TransactionScope())
      {
        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@UserGUID", userGUID);
          sqlDatabase.AddInputParameter("@MacAddress", macAddress);
          sqlDatabase.AddInputParameter("@MachineName", machineName);
          sqlDatabase.AddInputParameter("@MajorVersionNumber", majorVersionNumber);
          sqlDatabase.AddInputParameter("@MinorVersionNumber", minorVersionNumber);
          SqlParameter machineGUIDParam = sqlDatabase.AddOutputParameter("@MachineGUID", System.Data.SqlDbType.NVarChar, 50, null);

          sqlDatabase.ExecuteNonQuery("dp_createPcIfNotExists");

          machineGUID = (string)machineGUIDParam.Value;

          if (subscriptions != null)
          {
            sqlDatabase.ClearParameters();

            sqlDatabase.AddInputParameter("@MachineGUID", machineGUID);
            sqlDatabase.AddInputParameter("@UserGUID", userGUID);

            sqlDatabase.ExecuteNonQuery("dp_removePCSubscriptions");

            sqlDatabase.AddInputParameter("@ChannelWeightingUnnormalized");
            sqlDatabase.AddInputParameter("@ChannelID");

            foreach (string[] subscriptionElements in subscriptions)
            {
              int channelID;
              int channelWeightingUnnormalized;

              if (int.TryParse(subscriptionElements[0], out channelID)
                && int.TryParse(subscriptionElements[3], out channelWeightingUnnormalized))
              {
                sqlDatabase.EditInputParameter("@ChannelWeightingUnnormalized", channelWeightingUnnormalized);
                sqlDatabase.EditInputParameter("@ChannelID", channelID);

                sqlDatabase.ExecuteNonQuery("dp_addSubscriptionByMachineGUID");
              }
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
          sqlDatabase.Dispose();
        }

        ts.Complete();
      }

      return machineGUID;
    }

    public string SyncWithServerNoPersonalDetails(string userGUID, 
      string machineGUID,
      string macAddress, 
      string machineName, 
      int softwareMajorVersionNumber, 
      int softwareMinorVersionNumber, 
      OxigenIIAdvertising.AppData.ChannelSubscriptions channelSubscriptions)
    {
      SqlDatabase sqlDatabase = null;

      using (TransactionScope ts = new TransactionScope())
      {
        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@UserGUID", userGUID);
          sqlDatabase.AddInputParameter("@MachineGUID", machineGUID);
          sqlDatabase.AddInputParameter("@MachineName", machineName);
          sqlDatabase.AddInputParameter("@MACAddress", macAddress);
          sqlDatabase.AddInputParameter("@SoftwareMajorVersionNumber", softwareMajorVersionNumber);
          sqlDatabase.AddInputParameter("@SoftwareMinorVersionNumber", softwareMinorVersionNumber);

          sqlDatabase.ExecuteNonQuery("dp_addPCByUserGUIDMachineGUID");

          sqlDatabase.ClearParameters();

          // add sunbcriptions
          if (channelSubscriptions.SubscriptionSet != null)
          {
            _eventLog.WriteEntry("channelSubscription.SubscriptionSet is not null", EventLogEntryType.Information);

            sqlDatabase.AddInputParameter("@UserGUID", userGUID);
            sqlDatabase.AddInputParameter("@MachineGUID", machineGUID);
            sqlDatabase.AddInputParameter("@ChannelGUID");
            sqlDatabase.AddInputParameter("@ChannelWeighting");

            sqlDatabase.AddReturnParameter();

            foreach (OxigenIIAdvertising.AppData.ChannelSubscription cs in channelSubscriptions.SubscriptionSet)
            {
              sqlDatabase.EditInputParameter("@ChannelGUID", cs.ChannelGUID);
              sqlDatabase.EditInputParameter("@ChannelWeighting", cs.ChannelWeightingUnnormalised);

              sqlDatabase.ExecuteNonQuery("dp_addPCStreamByUserGuidMachineGUIDChannelGUIDNoLinkNoDirty");

              _eventLog.WriteEntry("ReturnValue for dp_addPCStreamByUserGuidMachineGUIDChannelGUIDNoLinkNoDirty: " + sqlDatabase.ReturnValue);

              if (sqlDatabase.ReturnValue < 0)
              {
                switch (sqlDatabase.ReturnValue)
                {
                  case -1:
                    _eventLog.WriteEntry("An attempt was made to access dp_addPCStreamByGUIDsNoLinkNoDirty using an invalid user GUID. The user user GUID is " + userGUID, EventLogEntryType.Warning);
                    throw new ActionNotSupportedException("Invalid user. Check event logs.");
                  case -2:
                    _eventLog.WriteEntry("An attempt was made to access dp_addPCStreamByGUIDsNoLinkNoDirty using an invalid Machine GUID. The machine GUID is " + machineGUID + " " + "and the user GUID is " + userGUID, EventLogEntryType.Warning);
                    break;
                  case -3:
                    _eventLog.WriteEntry("An attempt was made to access dp_addPCStreamByGUIDsNoLinkNoDirty using an invalid Channel GUID. The channel GUID is " + cs.ChannelGUID, EventLogEntryType.Warning);
                    break;
                  case -4:
                    _eventLog.WriteEntry("An attempt was made to access dp_addPCStreamByGUIDsNoLinkNoDirty using non-linked user GUID and maching GUID. The user GUID is " + userGUID + " and the machine GUID is " + machineGUID + ".", EventLogEntryType.Warning);
                    break;
                }

                return null;
              }
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
          sqlDatabase.Dispose();
        }

        ts.Complete();
      }

      return machineGUID;
    }

    public string CheckIfPCExistsReturnGUID(string username, string macAddress)
    {
      SqlDatabase sqlDatabase = null;
      string userGUID = null;
      string machineGUID = null;

      try
      {
        sqlDatabase = new SqlDatabase();
        sqlDatabase.Open();

        sqlDatabase.AddInputParameter("@MACAddress", macAddress);
        sqlDatabase.AddInputParameter("@Username", username);
        SqlParameter userGUIDParam = sqlDatabase.AddOutputParameter("@UserGUID", System.Data.SqlDbType.NVarChar, 50, null);
        SqlParameter machineGUIDParam = sqlDatabase.AddOutputParameter("@MachineGUID", System.Data.SqlDbType.NVarChar, 50, null);

        sqlDatabase.ExecuteNonQuery("dp_getIfPCExistsReturnGUID");

        if (machineGUIDParam.Value != DBNull.Value)
        {
          machineGUID = (string)machineGUIDParam.Value;
          _eventLog.WriteEntry("machineGUIDParam.Value is not null", EventLogEntryType.Information);
        }
        else
        {
          machineGUID = System.Guid.NewGuid().ToString().ToUpper() + "_" + GetRandomLetter();
          _eventLog.WriteEntry("machineGUIDParam.Value is null", EventLogEntryType.Information);
        }

        userGUID = (string)userGUIDParam.Value;
      }
      catch (Exception ex)
      {
        _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
        throw ex;
      }
      finally
      {
        sqlDatabase.Dispose();
      }

      return userGUID + "|" + machineGUID;
    }

    public void RemoveStreamsFromSilentMerge(string macAddress, OxigenIIAdvertising.AppData.ChannelSubscriptions channelSubscriptions)
    {
      SqlDatabase sqlDatabase = null;

      using (TransactionScope ts = new TransactionScope())
      {
        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@MACAddress", macAddress);
          sqlDatabase.AddInputParameter("@ChannelGUID");
          sqlDatabase.AddInputParameter("@ChannelWeighting");

          foreach (OxigenIIAdvertising.AppData.ChannelSubscription cs in channelSubscriptions.SubscriptionSet)
          {
            sqlDatabase.EditInputParameter("@ChannelGUID", cs.ChannelGUID);
            sqlDatabase.EditInputParameter("@ChannelWeighting", cs.ChannelWeightingUnnormalised);

            sqlDatabase.ExecuteNonQuery("dp_removeStreamByMacAddress");
          }
        }
        catch (Exception ex)
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
          throw ex;
        }
        finally
        {
          sqlDatabase.Dispose();
        }

        ts.Complete();
      }
    }

    public void ReplaceStreamsFromSilentMerge(string macAddress, OxigenIIAdvertising.AppData.ChannelSubscriptions channelSubscriptions)
    {
      SqlDatabase sqlDatabase = null;

      using (TransactionScope ts = new TransactionScope())
      {
        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@MACAddress", macAddress);

          sqlDatabase.ExecuteNonQuery("dp_removeAllStreamsByMacAddress");
          
          sqlDatabase.AddInputParameter("@ChannelGUID");
          sqlDatabase.AddInputParameter("@ChannelWeighting");

          foreach (OxigenIIAdvertising.AppData.ChannelSubscription cs in channelSubscriptions.SubscriptionSet)
          {
            sqlDatabase.EditInputParameter("@ChannelGUID", cs.ChannelGUID);
            sqlDatabase.EditInputParameter("@ChannelWeighting", cs.ChannelWeightingUnnormalised);

            sqlDatabase.ExecuteNonQuery("dp_addStreamByMacAddress");
          }
        }
        catch (Exception ex)
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
          throw ex;
        }
        finally
        {
          sqlDatabase.Dispose();
        }

        ts.Complete();
      }
    }

    public void AddStreamsFromSilentMerge(string macAddress, OxigenIIAdvertising.AppData.ChannelSubscriptions channelSubscriptions)
    {
      SqlDatabase sqlDatabase = null;

      using (TransactionScope ts = new TransactionScope())
      {
        try
        {
          sqlDatabase = new SqlDatabase();
          sqlDatabase.Open();

          sqlDatabase.AddInputParameter("@MACAddress", macAddress);
          sqlDatabase.AddInputParameter("@ChannelGUID");
          sqlDatabase.AddInputParameter("@ChannelWeighting");

          foreach (OxigenIIAdvertising.AppData.ChannelSubscription cs in channelSubscriptions.SubscriptionSet)
          {
            sqlDatabase.EditInputParameter("@ChannelGUID", cs.ChannelGUID);
            sqlDatabase.EditInputParameter("@ChannelWeighting", cs.ChannelWeightingUnnormalised);

            sqlDatabase.ExecuteNonQuery("dp_addStreamByMacAddress");
          }
        }
        catch (Exception ex)
        {
          _eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
          throw ex;
        }
        finally
        {
          sqlDatabase.Dispose();
        }

        ts.Complete();
      }
    }
  }
}
