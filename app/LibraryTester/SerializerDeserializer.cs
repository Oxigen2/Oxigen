using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OxigenIIAdvertising.XMLSerializer;
using System.IO;
using OxigenIIAdvertising.InclusionExclusionRules;
using OxigenIIAdvertising.AppData;
using OxigenIIAdvertising.LogStats;
using OxigenIIAdvertising.UserSettings;
using OxigenIIAdvertising.Demographic;
using InterCommunicationStructures;
using OxigenIIAdvertising;

namespace LibraryTester
{
  public partial class SerializerDeserializer : Form
  {
    private string _userGUID = "";
    private string _systemPassPhrase = "";
    private string _password = "password";

    private string _playlistDataPath = "";
    private string _generalDataPath = "";
    private string _advertDataPath = "";
    private string _demographicDataPath = "";
    private string _userChannelSubscriptionsPath = "";
    private string _advertAssetPath = "";
    private string _contentAssetPath = "";
    private string _channelDataPath = "";

    private bool _bCrypt;

    public SerializerDeserializer()
    {
      InitializeComponent();
      SetGlobals();
    }

    private void SetGlobals()
    {
      _userGUID = "f6b6d2f2-d5f1-4f4a-a76b-4636d62b164d_m";
      _systemPassPhrase = "password";
      _password = "password";
      _bCrypt = true;

      if (_bCrypt)
      {
        _generalDataPath = System.Configuration.ConfigurationSettings.AppSettings["generalDataPath"];
        _playlistDataPath = System.Configuration.ConfigurationSettings.AppSettings["playlistDataPath"];
        _advertDataPath = System.Configuration.ConfigurationSettings.AppSettings["advertDataPath"];
        _demographicDataPath = System.Configuration.ConfigurationSettings.AppSettings["demographicDataPath"];
        _userChannelSubscriptionsPath = System.Configuration.ConfigurationSettings.AppSettings["userChannelSubscriptionsPath"];
        _contentAssetPath = System.Configuration.ConfigurationSettings.AppSettings["contentAssetPath"];
        _advertAssetPath = System.Configuration.ConfigurationSettings.AppSettings["advertAssetPath"];
        _channelDataPath = System.Configuration.ConfigurationSettings.AppSettings["channelDataPath"];

        return;
      }

      _generalDataPath = System.Configuration.ConfigurationSettings.AppSettings["generalDataPathDec"];
      _playlistDataPath = System.Configuration.ConfigurationSettings.AppSettings["playlistDataPathDec"];
      _advertDataPath = System.Configuration.ConfigurationSettings.AppSettings["advertDataPathDec"];
      _demographicDataPath = System.Configuration.ConfigurationSettings.AppSettings["demographicDataPathDec"];
      _userChannelSubscriptionsPath = System.Configuration.ConfigurationSettings.AppSettings["userChannelSubscriptionsPathDec"];
      _contentAssetPath = System.Configuration.ConfigurationSettings.AppSettings["contentAssetPathDec"];
      _advertAssetPath = System.Configuration.ConfigurationSettings.AppSettings["advertAssetPathDec"];
      _channelDataPath = System.Configuration.ConfigurationSettings.AppSettings["channelDataPathDec"];
    }


    private void button1_Click(object sender, EventArgs e)
    {
      DemographicData dg = new DemographicData();
      dg.GeoDefinition = "0.1.2";
      dg.Gender = new string[] { "male" };
      dg.MinAge = 32;
      dg.MaxAge = 32;
      dg.SocioEconomicgroup = new string[] { "a1" };
      dg.GeoDefinition = "0.1.1.3.4";

      Serializer.Serialize(dg, _demographicDataPath, "password");

      MessageBox.Show("Serialization successful");
    }

    private void button2_Click(object sender, EventArgs e)
    {
      DemographicData dg = (DemographicData)Serializer.Deserialize(typeof(DemographicData), _demographicDataPath, "password");

      MessageBox.Show("Deserialization successful");
    }

    private void btnGeneralData_Click(object sender, EventArgs e)
    {
      GeneralData gd = new GeneralData();
      gd.SoftwareMajorVersionNumber = 1;
      gd.SoftwareMinorVersionNumber = 0;
      gd.Properties.Add("logExchangerProcessingInterval", "1800");
      gd.Properties.Add("contentExchangerProcessingInterval", "180");
      gd.Properties.Add("softwareUpdaterProcessingInterval", "43200");
      gd.Properties.Add("serverTimeout", "5000"); // milliseconds
      gd.Properties.Add("primaryDomainName", ".oxigen.net");
      gd.Properties.Add("secondaryDomainName", ".oxigen.net");
      gd.Properties.Add("advertDisplayThreshold", "0.1");
      gd.Properties.Add("logTimerInterval", "20");
      gd.Properties.Add("protectedContentTime", "25");
      gd.Properties.Add("maxLines", "10");
      gd.Properties.Add("noAssetDisplayLength", "15");
      gd.Properties.Add("requestTimeout", "4");
      gd.Properties.Add("dateTimeDiffTolerance", "5"); // minutes
      gd.Properties.Add("daysToKeepAssetFiles", "8");

      gd.NoServers.Add("relayLog", "4");
      gd.NoServers.Add("relayConfig", "4");
      gd.NoServers.Add("relayChannelAssets", "4");
      gd.NoServers.Add("relayChannelData", "4");
      gd.NoServers.Add("masterConfig", "4");
      gd.NoServers.Add("download", "4");

      string file = @"c:\oxigen\a\ss_general_data.dat";

      Serializer.Serialize(gd, file, "password");
      
      string checksum = OxigenIIAdvertising.FileChecksumCalculator.ChecksumCalculator.GetChecksum(file);

      File.WriteAllText(@"c:\users\michalikonstantinidi\desktop\ss_general_data.chk", checksum);
    }

    private void btnDeserializeGeneralData_Click(object sender, EventArgs e)
    {
      GeneralData gd = null;

      try
      {
        gd = (GeneralData)Serializer.Deserialize(typeof(GeneralData), _generalDataPath, "password");
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
        return;
      }

      string prop = "";

      if (gd.Properties.TryGetValue("advertDisplayThreshold", out prop))
        MessageBox.Show(prop, "Deserialization Successful");
    }

    private void btnSerializeChannelSubscriptions_Click(object sender, EventArgs e)
    {
      //ChannelSubscriptions channelSubscriptions = new ChannelSubscriptions();

      //channelSubscriptions.SubscriptionSet.Add(new ChannelSubscription { ChannelID = 1000, ChannelName = "a", ChannelWeightingNormalised = 0.1F });
      //channelSubscriptions.SubscriptionSet.Add(new ChannelSubscription { ChannelID = 1001, ChannelName = "a", ChannelWeightingNormalised = 0.1F });
      //channelSubscriptions.SubscriptionSet.Add(new ChannelSubscription { ChannelID = 1002, ChannelName = "a", ChannelWeightingNormalised = 0.1F });
      //channelSubscriptions.SubscriptionSet.Add(new ChannelSubscription { ChannelID = 1003, ChannelName = "a", ChannelWeightingNormalised = 0.1F });
      //channelSubscriptions.SubscriptionSet.Add(new ChannelSubscription { ChannelID = 1004, ChannelName = "a", ChannelWeightingNormalised = 0.1F });

      //Serializer.Serialize(channelSubscriptions, @"C:\users\michalikonstantinidi\desktop\channelsubscriptions.dat", "password");
    }

    private void btnDeserializeChannelSubscriptions_Click(object sender, EventArgs e)
    {
      ChannelSubscriptions channelSubscriptions = (ChannelSubscriptions)Serializer.Deserialize(typeof(ChannelSubscriptions), _userChannelSubscriptionsPath,  "password");

      string output = "";

      foreach (ChannelSubscription subscription in channelSubscriptions.SubscriptionSet)
      {
        output += "Channel ID: " + subscription.ChannelID + ", Channel Weighting: " + subscription.ChannelWeightingUnnormalised + "\r\n";
      }
    }

    private void btnSerializeChannelData_Click(object sender, EventArgs e)
    {
      //ChannelData channelData = new ChannelData();

      //Channel channel1 = new Channel();
      //channel1.ChannelID = 1000;
      //channel1.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.23.45.2.3.4.5"));
      //channel1.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.3.4.5"));
      //channel1.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.5.6.7.3"));
      //channel1.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.23.45"));
      //channel1.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0"));
      //channel1.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.23"));
      //channel1.ChannelDefinitions = "0.1";
      //channel1.VotingThreshold = 0.8F;
      //channel1.ChannelAssets.Add(new ChannelAsset(1001, PlayerType.VideoQT, "Stupeflix-RxfEcyE5wv_a.mp4", 50, "http://www.facebook.com/home.php?#!/album.php?aid=-3&id=749547019", "http://www.facebook.com/home.php?#!/album.php?aid=-3&id=749547019", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel1.ChannelAssets.Add(new ChannelAsset(1002, PlayerType.VideoQT, "Stupeflix-OTiuiyoZud-480x270_a.mp4", 37, "http://www.facebook.com/home.php?#!/album.php?aid=125606&id=749547019", "http://www.facebook.com/home.php?#!/album.php?aid=125606&id=749547019", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
  
      //channelData.Channels.Add(channel1);

      //Channel channel2 = new Channel();
      //channel2.ChannelID = 1001;

      //channel2.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.23.45.2.3.4.5"));
      //channel2.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.3.4.5"));
      //channel2.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.5.6.7.3"));
      //channel2.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.23.45"));
      //channel2.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0"));
      //channel2.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.23"));

      //channel2.ChannelDefinitions = "0.1";
      //channel2.VotingThreshold = 0.2F;

      //channel2.ChannelAssets.Add(new ChannelAsset(1004, PlayerType.Flash, "APR_2009_PaddockGirls_1_a.swf", 8, "http://www.motogp.com/en/photos", "http://www.motogp.com/en/photos", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel2.ChannelAssets.Add(new ChannelAsset(1005, PlayerType.Flash, "APR_20091004_Estoril_a.swf", 8, "http://www.motogp.com/en/photos", "http://www.motogp.com/en/photos", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel2.ChannelAssets.Add(new ChannelAsset(1006, PlayerType.Flash, "APR_20091018_PhillipIsland_a.swf", 8, "http://www.motogp.com/en/photos", "http://www.motogp.com/en/photos", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel2.ChannelAssets.Add(new ChannelAsset(1007, PlayerType.Flash, "APR_20091025_Sepang_a.swf", 8, "http://www.motogp.com/en/photos", "http://www.motogp.com/en/photos", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel2.ChannelAssets.Add(new ChannelAsset(1008, PlayerType.Flash, "logo_a.swf", 8, "", "", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel2.ChannelAssets.Add(new ChannelAsset(1009, PlayerType.Flash, "RiderProfile_CaseyStoner_a.swf", 8, "http://www.motogp.com/en/riders/Casey+Stoner", "http://www.motogp.com/en/riders/Casey+Stoner", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel2.ChannelAssets.Add(new ChannelAsset(1010, PlayerType.Flash, "RiderProfile_DaniPedrosa_a.swf", 8, "http://www.motogp.com/en/riders/Dani+Pedrosa", "http://www.motogp.com/en/riders/Dani+Pedrosa", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel2.ChannelAssets.Add(new ChannelAsset(1011, PlayerType.Flash, "RiderProfile_ValentinoRossi_a.swf", 8, "http://www.motogp.com/en/riders/Valentino+Rossi", "http://www.motogp.com/en/riders/Valentino+Rossi", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel2.ChannelAssets.Add(new ChannelAsset(1025, PlayerType.Flash, "APR_20091108_Valencia_a.swf", 8, "http://www.motogp.com/en/photos", "http://www.motogp.com/en/photos", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel2.ChannelAssets.Add(new ChannelAsset(1012, PlayerType.VideoQT, "01_Qatar_a.mov", 53, "http://www.motogp.com/en/videos", "http://www.motogp.com/en/videos", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel2.ChannelAssets.Add(new ChannelAsset(1013, PlayerType.VideoQT, "02_Japan_a.mov", 58, "http://www.motogp.com/en/videos", "http://www.motogp.com/en/videos", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel2.ChannelAssets.Add(new ChannelAsset(1014, PlayerType.VideoQT, "03_Spain_a.mov", 54, "http://www.motogp.com/en/videos", "http://www.motogp.com/en/videos", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel2.ChannelAssets.Add(new ChannelAsset(1015, PlayerType.VideoQT, "04_French_a.mov", 62, "http://www.motogp.com/en/videos", "http://www.motogp.com/en/videos", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel2.ChannelAssets.Add(new ChannelAsset(1016, PlayerType.VideoQT, "05_Italy_a.mov", 64, "http://www.motogp.com/en/videos", "http://www.motogp.com/en/videos", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel2.ChannelAssets.Add(new ChannelAsset(1050, PlayerType.VideoQT, "06_Catalunya_a.mov", 64, "http://www.motogp.com/en/videos", "http://www.motogp.com/en/videos", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      
      //channelData.Channels.Add(channel2);

      //Channel channel3 = new Channel();
      //channel3.ChannelID = 1002;

      //channel2.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.23.45.2.3.4.5"));
      //channel3.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.3.4.5"));
      //channel3.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.5.6.7.3"));
      //channel3.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.23.45"));
      //channel3.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0"));
      //channel3.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.23"));

      //channel3.ChannelDefinitions = "0.1";
      //channel3.VotingThreshold = 0.2F;

      //channel3.ChannelAssets.Add(new ChannelAsset(1017, PlayerType.VideoQT, "101201090_a.mov", 143, "http://www.theeclipsefilm.com/", "http://www.theeclipsefilm.com/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1018, PlayerType.VideoQT, "101343567_a.mov", 97, "http://www.wallstreetmoneyneversleeps.com/", "http://www.wallstreetmoneyneversleeps.com/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1019, PlayerType.VideoQT, "100255233_a.mov", 101, "http://www.ateam-movie.com/", "http://www.ateam-movie.com/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1020, PlayerType.VideoQT, "karate-kid_a.mov", 131, "http://en.wikipedia.org/wiki/The_Karate_Kid", "http://en.wikipedia.org/wiki/The_Karate_Kid", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1021, PlayerType.VideoNonQT, "bf_trlr_020210_wmvhd_a.wmv", 31, "http://www.brooklynsfinestthemovie.com/", "http://www.brooklynsfinestthemovie.com/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1022, PlayerType.VideoNonQT, "karatekid_trlr1_010510_wmvhd_a.wmv", 117, "http://www.karatekid-themovie.com/", "http://www.karatekid-themovie.com/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1023, PlayerType.VideoQT, "avatar_trl_103009_qthighwide_a.mov", 211, "http://www.avatarmovie.com/", "http://www.avatarmovie.com/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1024, PlayerType.VideoQT, "99125463_a.mov", 40, "http://www.themarmadukemovie.com/", "http://www.themarmadukemovie.com/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1062, PlayerType.VideoQT, "alice_wonderland_trl_121609_qthighwide_a.mov", 118, "http://adisney.go.com/disneypictures/aliceinwonderland/", "http://adisney.go.com/disneypictures/aliceinwonderland/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1063, PlayerType.VideoNonQT, "centurion_trl1_021110_wmvhd_a.wmv", 129, "http://www.imdb.com/title/tt1020558/", "http://www.imdb.com/title/tt1020558/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1064, PlayerType.VideoNonQT, "dis_oceans_tease_042909_wmvhd_a.wmv",118, "http://www.disney.com/oceans", "http://www.disney.com/oceans", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1065, PlayerType.VideoQT, "greenbacks_trlr_120209_qthighwide_a.mov", 149, "http://www.imdb.com/title/tt1234654/", "http://www.imdb.com/title/tt1234654/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1066, PlayerType.VideoQT, "greenzone_trla_110509_qthighwide_a.mov", 125, "http://www.greenzonemovie.com", "http://www.greenzonemovie.com", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1067, PlayerType.VideoQT, "hp_halfblood_tease_082808_qthighwide_a.mov", 94, "http://www.imdb.com/title/tt0417741/ ", "http://www.imdb.com/title/tt0417741/ ", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1068, PlayerType.VideoQT, "inception_f2_ov_sd_qt_high_qthighwide_a.mov", 72, "http://inceptionmovie.warnerbros.com", "http://inceptionmovie.warnerbros.com", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1069, PlayerType.VideoQT, "ironman2_trl_121609_qthighwide_a.mov",150 , "http://ironmanmovie.marvel.com", "http://ironmanmovie.marvel.com", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1070, PlayerType.VideoQT, "karate-kid_a.mov", 131, "http://www.imdb.com/title/tt0087538", "http://www.imdb.com/title/tt0087538", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1071, PlayerType.VideoNonQT, "karatekid_trlr1_010510_wmvhd_a.wmv", 118, "http://www.karatekid-themovie.com", "http://www.karatekid-themovie.com", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1072, PlayerType.VideoQT, "kickass_hitgirl_redtrl_122109_qthighwide_a.mov", 82, "http://www.imdb.com/title/tt1250777", "http://www.imdb.com/title/tt1250777", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1073, PlayerType.VideoQT, "lastsong_trlr1_112009_qthighwide_a.mov", 149, "http://touchstone_a.movies.go.com/thelastsong/", "http://touchstone_a.movies.go.com/thelastsong/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1074, PlayerType.VideoQT, "loser_trlr_020110_qthighwide_a.mov",152 , "http://www.the-losers.com", "http://www.the-losers.com", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1075, PlayerType.VideoQT, "pj_trlr_2_020410_qthighwide_a.mov", 98, "http://www.percyjacksonthemovie.com", "http://www.percyjacksonthemovie.com", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1076, PlayerType.VideoQT, "runaways_trlr_012510_qthighwide_a.mov", 49, "runaways_trlr_012510_qthighwide_a.mov", "runaways_trlr_012510_qthighwide_a.mov", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1077, PlayerType.VideoQT, "thebabies_trlr_120209_qthighwide_a.mov",146 , "http://www.filminfocus.com/film/babies/", "http://www.filminfocus.com/film/babies/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1078, PlayerType.VideoNonQT, "thecrazies_trlr_020210_wmvhd_a.wmv", 32, "http://www.thecrazies-movie.com/", "http://www.thecrazies-movie.com/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1079, PlayerType.VideoQT, "toystory3_trailer_060909_qthighwide_a.mov", 103, "http://disney.go.com/toystory/", "http://disney.go.com/toystory/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1080, PlayerType.VideoQT, "wolfman_trl1_082609_qthighwide_a.mov", 145, "http://www.thewolfmanmovie.com", "http://www.thewolfmanmovie.com", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel3.ChannelAssets.Add(new ChannelAsset(1081, PlayerType.VideoQT, "yellowhandkerchief_trlr1_111009_qthighwide_a.mov", 127, "http://www.theyellowhandkerchief.com/", "http://www.theyellowhandkerchief.com/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));

      //channelData.Channels.Add(channel3);

      //Channel channel4 = new Channel();
      //channel4.ChannelID = 1003;

      //channel2.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.23.45.2.3.4.5"));
      //channel4.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.3.4.5"));
      //channel4.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.5.6.7.3"));
      //channel4.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.23.45"));
      //channel4.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0"));
      //channel4.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.23"));

      //channel4.ChannelDefinitions = "0.1";
      //channel4.VotingThreshold = 0.2F;

      //channel4.ChannelAssets.Add(new ChannelAsset(1100, PlayerType.Image, "Hannah_01_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1101, PlayerType.Image, "Hannah_02_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1102, PlayerType.Image, "Hannah_03_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1103, PlayerType.Image, "Hannah_04_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1104, PlayerType.Image, "Hannah_05_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1105, PlayerType.Image, "Hannah_06_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1106, PlayerType.Image, "Hannah_07_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1107, PlayerType.Image, "Hannah_08_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1108, PlayerType.Image, "Hannah_09_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1109, PlayerType.Image, "Hannah_10_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1110, PlayerType.Image, "Hannah_11_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1111, PlayerType.Image, "Hannah_12_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1112, PlayerType.Image, "Hannah_13_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1113, PlayerType.Image, "Hannah_14_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1114, PlayerType.Image, "Hannah_15_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1115, PlayerType.Image, "Hannah_16_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1116, PlayerType.Image, "Hannah_17_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1117, PlayerType.Image, "Hannah_18_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1118, PlayerType.Image, "Hannah_19_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1119, PlayerType.Image, "Hannah_20_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1120, PlayerType.Image, "Hannah_21_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1121, PlayerType.Image, "Hannah_22_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1122, PlayerType.Image, "Hannah_23_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1123, PlayerType.Image, "Hannah_24_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1124, PlayerType.Image, "Hannah_25_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1125, PlayerType.Image, "Hannah_26_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1126, PlayerType.Image, "Hannah_27_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1127, PlayerType.Image, "Hannah_28_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1128, PlayerType.Image, "Hannah_29_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1129, PlayerType.Image, "Hannah_30_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1130, PlayerType.Image, "Hannah_31_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1131, PlayerType.Image, "Hannah_32_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1132, PlayerType.Image, "Hannah_33_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1133, PlayerType.Image, "Hannah_34_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1134, PlayerType.Image, "Hannah_35_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1135, PlayerType.Image, "Hannah_36_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1136, PlayerType.Image, "Hannah_37_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1137, PlayerType.Image, "Hannah_38_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1138, PlayerType.Image, "Hannah_39_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel4.ChannelAssets.Add(new ChannelAsset(1139, PlayerType.Image, "Hannah_40_a.jpg", 5, "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", "http://www.facebook.com/#!/profile.php?id=509202812&ref=search&sid=749547019.837449344..1", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));

      //channelData.Channels.Add(channel4);

      //Channel channel5 = new Channel();
      //channel5.ChannelID = 1004;

      //channel2.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.23.45.2.3.4.5"));
      //channel5.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.3.4.5"));
      //channel5.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.5.6.7.3"));
      //channel5.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.23.45"));
      //channel5.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0"));
      //channel5.InclusionExclusionList.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.23"));

      //channel5.ChannelDefinitions = "0.1";
      //channel5.VotingThreshold = 0.2F;

      //channel5.ChannelAssets.Add(new ChannelAsset(1200, PlayerType.Image, "DSCN0073_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1201, PlayerType.Image, "DSCN0117_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1202, PlayerType.Image, "DSCN0727_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1203, PlayerType.Image, "DSCN0968_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1204, PlayerType.Image, "DSCN1034_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1205, PlayerType.Image, "DSCN1102_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1206, PlayerType.Image, "DSCN1203_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1207, PlayerType.Image, "DSCN1404_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1208, PlayerType.Image, "DSCN1435_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1209, PlayerType.Image, "DSCN1466_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1210, PlayerType.Image, "DSCN1530_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1211, PlayerType.Image, "DSCN1626_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1212, PlayerType.Image, "DSCN2226_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1213, PlayerType.Image, "DSCN2264_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1214, PlayerType.Image, "DSCN2446_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1215, PlayerType.Image, "DSCN2624_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1216, PlayerType.Image, "DSCN2804_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1217, PlayerType.Image, "DSCN3007_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1218, PlayerType.Image, "DSCN3843_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1219, PlayerType.Image, "DSCN3844_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1220, PlayerType.Image, "DSCN3851_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1221, PlayerType.Image, "DSCN3978_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1222, PlayerType.Image, "DSCN4336_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1223, PlayerType.Image, "DSCN4345_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1224, PlayerType.Image, "DSCN4807_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1225, PlayerType.Image, "DSCN5113_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1226, PlayerType.Image, "DSCN5505_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1227, PlayerType.Image, "DSCN5635_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1228, PlayerType.Image, "DSC_0013_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1229, PlayerType.Image, "DSC_0017_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1230, PlayerType.Image, "DSC_0035_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1231, PlayerType.Image, "DSC_0044_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1232, PlayerType.Image, "DSC_0054_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1233, PlayerType.Image, "DSC_0064_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1234, PlayerType.Image, "DSC_0070_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1235, PlayerType.Image, "DSC_0071_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1236, PlayerType.Image, "DSC_0080_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1237, PlayerType.Image, "DSC_0080a_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1238, PlayerType.Image, "DSC_0094_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1239, PlayerType.Image, "DSC_0100_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1240, PlayerType.Image, "DSC_0109_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1241, PlayerType.Image, "DSC_0111_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1242, PlayerType.Image, "DSC_0129_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1243, PlayerType.Image, "DSC_0129b_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1244, PlayerType.Image, "DSC_0132_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1245, PlayerType.Image, "DSC_0153_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1246, PlayerType.Image, "DSC_0156_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1247, PlayerType.Image, "DSC_0161_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1248, PlayerType.Image, "DSC_0169_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1249, PlayerType.Image, "DSC_0190_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1250, PlayerType.Image, "DSC_0205_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1251, PlayerType.Image, "DSC_0208_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1252, PlayerType.Image, "DSC_0215_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1253, PlayerType.Image, "DSC_0248_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1254, PlayerType.Image, "DSC_0271_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1255, PlayerType.Image, "DSC_0301_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1256, PlayerType.Image, "DSC_0311_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1257, PlayerType.Image, "DSC_0336_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1258, PlayerType.Image, "DSC_0356_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1259, PlayerType.Image, "DSC_0368_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1260, PlayerType.Image, "DSC_0401_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1261, PlayerType.Image, "DSC_0436_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1262, PlayerType.Image, "DSC_0441_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1263, PlayerType.Image, "DSC_0450_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1264, PlayerType.Image, "DSC_0471_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1265, PlayerType.Image, "DSC_0482_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1266, PlayerType.Image, "DSC_0513_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1267, PlayerType.Image, "DSC_0519_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1268, PlayerType.Image, "DSC_0582_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1269, PlayerType.Image, "DSC_0606_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1270, PlayerType.Image, "DSC_0611_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1271, PlayerType.Image, "DSC_0630_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1272, PlayerType.Image, "DSC_0639_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1273, PlayerType.Image, "DSC_0659_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1274, PlayerType.Image, "DSC_0680_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1275, PlayerType.Image, "DSC_0680a_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1276, PlayerType.Image, "DSC_0689_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1277, PlayerType.Image, "DSC_0704_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1278, PlayerType.Image, "DSC_0745_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1279, PlayerType.Image, "DSC_0756_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1280, PlayerType.Image, "DSC_0766a_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1281, PlayerType.Image, "DSC_0777_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1282, PlayerType.Image, "DSC_0808_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1283, PlayerType.Image, "DSC_0814_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1284, PlayerType.Image, "DSC_0828_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1285, PlayerType.Image, "DSC_0839_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1286, PlayerType.Image, "DSC_0854_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1287, PlayerType.Image, "DSC_0864_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1288, PlayerType.Image, "DSC_0867_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1289, PlayerType.Image, "DSC_0898_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1290, PlayerType.Image, "DSC_0929_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1291, PlayerType.Image, "DSC_0935_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1292, PlayerType.Image, "DSC_0937_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1293, PlayerType.Image, "DSC_0979_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1294, PlayerType.Image, "DSC_1004_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1295, PlayerType.Image, "DSC_1015_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1296, PlayerType.Image, "DSC_1039_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1297, PlayerType.Image, "DSC_1077_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1298, PlayerType.Image, "DSC_1119_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1299, PlayerType.Image, "DSC_1211_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));
      //channel5.ChannelAssets.Add(new ChannelAsset(1300, PlayerType.Image, "DSC_1243_a.jpg", 5, "http://www.flickr.com/photos/31360297@N06/", "http://www.flickr.com/photos/31360297@N06/", new string[] { "time > 0001" }, AssetLevel.Premium, new DateTime(2009, 9, 9), new DateTime(2010, 7, 25)));

      //channelData.Channels.Add(channel5);

      //foreach (Channel channel in channelData.Channels)
      //  Serializer.Serialize(channel, _channelDataPath + channel.ChannelID + "_channel.dat", "password");
    }

    private void btnSerializeAdverts_Click(object sender, EventArgs e)
    {
      AdvertList advertList = new AdvertList();

      AdvertAsset advertAsset = new AdvertAsset();

      advertAsset.AssetID = 1100;
      advertAsset.AssetWebSite = "";
      advertAsset.ClickDestination = "http://fructis.garnier.ca/en/product_fs.asp?product_id=26";
      advertAsset.DemoRequirements = new string[] { "age > 0" };
      advertAsset.DisplayDuration = 15;
      advertAsset.EndDateTime = new DateTime(2010, 7, 25);
      advertAsset.FrequencyCap = 800;
      advertAsset.PlayerType = PlayerType.Flash;
      advertAsset.Weighting = 8;
      advertAsset.AssetFilename = "GarnierManga_a.swf";
      advertAsset.ScheduleInfo = new string[] { "time > 0000 and time < 2359" };
      advertAsset.StartDateTime = new DateTime(2009, 12, 1);

      advertList.Adverts.Add(advertAsset);

      advertAsset = new AdvertAsset();

      advertAsset.AssetID = 1101;
      advertAsset.AssetWebSite = "";
      advertAsset.ClickDestination = "http://www.australia.com";
      advertAsset.DemoRequirements = new string[] { "age > 0" };
      advertAsset.DisplayDuration = 15;
      advertAsset.EndDateTime = new DateTime(2010, 7, 25);
      advertAsset.FrequencyCap = 800;
      advertAsset.PlayerType = PlayerType.Flash;
      advertAsset.Weighting = 8;
      advertAsset.AssetFilename = "TourismAustralia_a.swf";
      advertAsset.ScheduleInfo = new string[] { "time > 0000 and time < 2359" };
      advertAsset.StartDateTime = new DateTime(2009, 12, 1);

      advertList.Adverts.Add(advertAsset);

      advertAsset = new AdvertAsset();

      advertAsset.AssetID = 1103;
      advertAsset.AssetWebSite = "";
      advertAsset.ClickDestination = "http://www.wispagoldmessages.com/";
      advertAsset.DemoRequirements = new string[] { "age > 0" };
      advertAsset.DisplayDuration = 10;
      advertAsset.EndDateTime = new DateTime(2010, 7, 25);
      advertAsset.FrequencyCap = 800;
      advertAsset.PlayerType = PlayerType.Image;
      advertAsset.Weighting = 8;
      advertAsset.AssetFilename = "WispaGold_ss_a.jpg";
      advertAsset.ScheduleInfo = new string[] { "time > 0000 and time < 2359" };
      advertAsset.StartDateTime = new DateTime(2009, 12, 1);

      advertList.Adverts.Add(advertAsset);

      advertAsset = new AdvertAsset();

      advertAsset.AssetID = 1104;
      advertAsset.AssetWebSite = "";
      advertAsset.ClickDestination = "http://www.nivea.com/products/show/520";
      advertAsset.DemoRequirements = new string[] { "age > 0" };
      advertAsset.DisplayDuration = 15;
      advertAsset.EndDateTime = new DateTime(2010, 7, 25);
      advertAsset.FrequencyCap = 800;
      advertAsset.PlayerType = PlayerType.Flash;
      advertAsset.Weighting = 8;
      advertAsset.AssetFilename = "Nivea_a.swf";
      advertAsset.ScheduleInfo = new string[] { "time > 0000 and time < 2359" };
      advertAsset.StartDateTime = new DateTime(2009, 12, 1);

      advertList.Adverts.Add(advertAsset);

      advertAsset = new AdvertAsset();

      advertAsset.AssetID = 1105;
      advertAsset.AssetWebSite = "";
      advertAsset.ClickDestination = "http://www.potnoodle.com/pier/";
      advertAsset.DemoRequirements = new string[] { "age > 0" };
      advertAsset.DisplayDuration = 10;
      advertAsset.EndDateTime = new DateTime(2010, 7, 25);
      advertAsset.FrequencyCap = 800;
      advertAsset.PlayerType = PlayerType.Image;
      advertAsset.Weighting = 8;
      advertAsset.AssetFilename = "PotNoodle_ss_a.jpg";
      advertAsset.ScheduleInfo = new string[] { "time > 0000 and time < 2359" };
      advertAsset.StartDateTime = new DateTime(2009, 12, 1);

      advertList.Adverts.Add(advertAsset);

      advertAsset = new AdvertAsset();

      advertAsset.AssetID = 1106;
      advertAsset.AssetWebSite = "";
      advertAsset.ClickDestination = "http://www.channel4.com/programmes/desperate-housewives";
      advertAsset.DemoRequirements = new string[] { "age > 0" };
      advertAsset.DisplayDuration = 10;
      advertAsset.EndDateTime = new DateTime(2010, 7, 25);
      advertAsset.FrequencyCap = 800;
      advertAsset.PlayerType = PlayerType.Flash;
      advertAsset.Weighting = 8;
      advertAsset.AssetFilename = "C4_20090223_DesperateHousewives_a.swf";
      advertAsset.ScheduleInfo = new string[] { "time > 0000 and time < 2359" };
      advertAsset.StartDateTime = new DateTime(2009, 12, 1);

      advertList.Adverts.Add(advertAsset);
            
      advertAsset = new AdvertAsset();

      advertAsset.AssetID = 1107;
      advertAsset.AssetWebSite = "";
      advertAsset.ClickDestination = "http://www.asuitthatfits.com/";
      advertAsset.DemoRequirements = new string[] { "age > 0" };
      advertAsset.DisplayDuration = 15;
      advertAsset.EndDateTime = new DateTime(2010, 7, 25);
      advertAsset.FrequencyCap = 800;
      advertAsset.PlayerType = PlayerType.Flash;
      advertAsset.Weighting = 8;
      advertAsset.AssetFilename = "ASTF_ss_1280x1024_a.swf";
      advertAsset.ScheduleInfo = new string[] { "time > 0000 and time < 2359" };
      advertAsset.StartDateTime = new DateTime(2009, 12, 1);

      advertList.Adverts.Add(advertAsset);

      advertAsset = new AdvertAsset();

      advertAsset.AssetID = 1108;
      advertAsset.AssetWebSite = "";
      advertAsset.ClickDestination = "http://www.alertchallenge.com";
      advertAsset.DemoRequirements = new string[] { "age > 0" };
      advertAsset.DisplayDuration = 15;
      advertAsset.EndDateTime = new DateTime(2010, 7, 25);
      advertAsset.FrequencyCap = 800;
      advertAsset.PlayerType = PlayerType.Flash;
      advertAsset.Weighting = 8;
      advertAsset.AssetFilename = "LucozadeAlert_a.swf";
      advertAsset.ScheduleInfo = new string[] { "time > 0000 and time < 2359" };
      advertAsset.StartDateTime = new DateTime(2009, 12, 1);

      advertList.Adverts.Add(advertAsset);


      Serializer.Serialize(advertList, _advertDataPath, "password");
    }

    private void btnDeserializeAdverts_Click(object sender, EventArgs e)
    {
      //AdvertList advertList = (AdvertList)Serializer.Deserialize(typeof(AdvertList), FilePath.AdCondDataFilePath, _bCrypt, "password");

      //string output = "";

      //foreach (AdvertAsset advertAsset in advertList.Adverts)
      //{
      //  output += advertAsset.AssetID + "\r\n";
      //  output += advertAsset.AssetFilename + "\r\n";
      //  output += advertAsset.ClickDestination + "\r\n";
               
      //  foreach (string demoRequirement in advertAsset.DemoRequirements)
      //  {
      //    output += demoRequirement + "\r\n";
      //  }

      //  output += advertAsset.DisplayDuration + "\r\n";
      //  output += advertAsset.EndDateTime + "\r\n";
      //  output += advertAsset.FrequencyCap + "\r\n";
      //  output += advertAsset.ProbabilityWeighting + "\r\n";

      //  foreach (string schedule in advertAsset.ScheduleInfo)
      //  {
      //    output += schedule + "\r\n";
      //  }

      //  output += advertAsset.StartDateTime + "\r\n";

      //  output += "\r\n";
      //}

      //OutputDisplay dd = new OutputDisplay();

      //dd.SetOutputData(output);
      //dd.ShowDialog();
    }    

    /// <summary>
    /// Generates a random string with the given length
    /// </summary>
    /// <param name="size">Size of the string</param>
    /// <param name="lowerCase">If true, generate lowercase string</param>
    /// <returns>Random string</returns>
    private string RandomString(int size, bool lowerCase)
    {
      StringBuilder builder = new StringBuilder();
      Random random = new Random();
      char ch;
      for (int i = 0; i < size; i++)
      {
        ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
        builder.Append(ch);
      }
      if (lowerCase)
        return builder.ToString().ToLower();
      return builder.ToString();
    }

    private void btnSerializeUsageCount_Click(object sender, EventArgs e)
    {
      UsageCount uc = new UsageCount();
      uc.MachineGUID = "usersmachineguid_m";
      uc.UserGUID = "975634789562378946578_c";
      uc.NoClicks = 4;
      uc.NoScreenSaverSessions = 2;
      uc.TotalPlayTime = 60 * 25;

      Serializer.Serialize(uc, @"C:\OxigenData\Other\ss_usg_1.dat", "password");
    }

    private void btnDeserializeUsageCount_Click(object sender, EventArgs e)
    {
      UsageCount uc = (UsageCount)Serializer.Deserialize(typeof(UsageCount),  @"C:\usagecount.xml", "password");

      if (uc.MachineGUID == null)
        MessageBox.Show("UsageCount is empty");
      else
        MessageBox.Show("UsageCount has values");
    }

    private void SerializerDeserializer_Load(object sender, EventArgs e)
    {

    }

    private void btnSerializeUserSettings_Click(object sender, EventArgs e)
    {
      User user = new User();

      user.LastTimeDiff = new TimeSpan(0);
      user.MachineGUID = "04B27F66-2EB9-4990-A70C-156725B256E5_A";
      user.UserGUID = "31350825-4D31-48E3-8AFE-0F32C1D53096_A";
      user.AssetFolderSize = 800 * 1024 * 1024;
      user.DefaultDisplayDuration = 5;
      user.FlashVolume = 100;
      user.VideoVolume = 100;
      user.MuteFlash = false;
      user.MuteVideo = false;
      Serializer.Serialize(user, @"C:\Users\MichaliKonstantinidi\Desktop\UserSettings.dat", "password");
    }

    private void button3_Click(object sender, EventArgs e)
    {
      HashSet<ComponentInfo> info = new HashSet<ComponentInfo>();

      ComponentInfo ci = new ComponentInfo()
      {
        File = "OxigenCE.exe",
        Location = ComponentLocation.BinaryFolder,
        MajorVersionNumber = 1,
        MinorVersionNumber = 1
      };

      info.Add(ci);

      ci = new ComponentInfo()
      {
        File = "this & <tag></tag> is a % file",
        Location = ComponentLocation.BinaryFolder,
        MajorVersionNumber = 1,
        MinorVersionNumber = 1
      };

      info.Add(ci);

      Serializer.SerializeToClearText(info, @"C:\Users\MichaliKonstantinidi\Desktop\ss_component_list.dat");
    }

    private void deserializecomponents_Click(object sender, EventArgs e)
    {
      HashSet<ComponentInfo> info = (HashSet<ComponentInfo>)Serializer.DeserializeClearText(typeof(HashSet<ComponentInfo>), @"C:\Users\MichaliKonstantinidi\Desktop\ss_component_list.dat");
    
      MessageBox.Show("Successful Deserialization");
    }    
  }

  public class ChannelSubscriptions
  {
    private ChannelSubscription[] _subscriptionSet;

    /// <summary>
    /// The HashSet that holds the user's channel subscriptions
    /// </summary>
    public ChannelSubscription[] SubscriptionSet
    {
      get { return _subscriptionSet; }
      set { _subscriptionSet = value; }
    }

    /// <summary>
    /// Instantiating an object will instantiate a new HashSet to hold the user's subscriptions
    /// </summary>
    public ChannelSubscriptions()
    {
      _subscriptionSet = new ChannelSubscription[100];
    }

    /// <summary>
    /// Searches the SubscriptionList HashSet for the channel weighting for the channel with that ID
    /// </summary>
    /// <param name="channelID">The unique ID of the channel to search the channel weighting</param>
    /// <returns>the channel weighting of the channel or 0 if no such ID is found</returns>
    public float GetChannelWeightingUnnormalisedByChannelID(long channelID)
    {
      foreach (ChannelSubscription cs in _subscriptionSet)
      {
        if (cs.ChannelID == channelID)
          return cs.ChannelWeightingUnnormalised;
      }

      return 0F;
    }

    /// <summary>
    /// Normalizes the weightings of the channel subscriptions to be used in log calculations
    /// </summary>
    public void NormalizeChannelWeightings()
    {
      float sum = 0F;

      foreach (ChannelSubscription cs in _subscriptionSet)
        sum += cs.ChannelWeightingUnnormalised;

      foreach (ChannelSubscription cs in _subscriptionSet)
        cs.ChannelWeightingNormalised = cs.ChannelWeightingUnnormalised / sum;
    }
  }
  public class ChannelSubscription
  {
    private long _channelID;
    private string _channelName;
    private string _channelGUID;
    private float _channelWeightingUnnormalised;
    private float _channelWeightingNormalised;

    /// <summary>
    /// Gets the unique ID of the channel in the database
    /// </summary>
    public long ChannelID
    {
      get { return _channelID; }
      set { _channelID = value; }
    }

    /// <summary>
    /// Gets the name of the channel
    /// </summary>
   public string ChannelName
    {
      get { return _channelName; }
      set { _channelName = value; }
    }

    /// <summary>
    /// Gets or sets the unnormalized channel weighting
    /// </summary>
    public float ChannelWeightingUnnormalised
    {
      get { return _channelWeightingUnnormalised; }
      set { _channelWeightingUnnormalised = value; }
    }

    /// <summary>
    /// Gets or sets the normalized channel weighting
    /// </summary>
    public float ChannelWeightingNormalised
    {
      get { return _channelWeightingNormalised; }
      set { _channelWeightingNormalised = value; }
    }

    /// <summary>
    /// Gets the channel GUID that identifies the channel's location across the relay servers
    /// </summary>
    public string ChannelGUID
    {
      get { return _channelGUID; }
      set { _channelGUID = value; }
    }

    /// <summary>
    /// Gets the letter which suffixes the channel's GUID
    /// </summary>
    /// <returns>The letter which suffixes the channel's GUID</returns>
    public string GetGUIDSuffix()
    {
      return _channelGUID.Substring(_channelGUID.LastIndexOf("_") + 1, 1);
    }

    public ChannelSubscription() { }
  }
}
