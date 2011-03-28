using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OxigenIIAdvertising.PlaylistLogic;
using OxigenIIAdvertising.XMLSerializer;
using OxigenIIAdvertising.AppData;

namespace LibraryTester
{
  public partial class PlayListTester : Form
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

    bool _bEncrypt = true;

    public PlayListTester()
    {
      InitializeComponent();
      SetGlobals();
    }

    private void SetGlobals()
    {
      _userGUID = "f6b6d2f2-d5f1-4f4a-a76b-4636d62b164d";
      _systemPassPhrase = "password";
      _password = "password";

      _generalDataPath = System.Configuration.ConfigurationSettings.AppSettings["generalDataPath"];
      _playlistDataPath = System.Configuration.ConfigurationSettings.AppSettings["playlistDataPath"];
      _advertDataPath = System.Configuration.ConfigurationSettings.AppSettings["advertDataPath"];
      _demographicDataPath = System.Configuration.ConfigurationSettings.AppSettings["demographicDataPath"];
      _userChannelSubscriptionsPath = System.Configuration.ConfigurationSettings.AppSettings["userChannelSubscriptionsPath"];
      _contentAssetPath = System.Configuration.ConfigurationSettings.AppSettings["contentAssetPath"];
      _advertAssetPath = System.Configuration.ConfigurationSettings.AppSettings["advertAssetPath"];
      _channelDataPath = System.Configuration.ConfigurationSettings.AppSettings["channelDataPath"];
    }

    private void button1_Click(object sender, EventArgs e)
    {
      //ChannelSubscriptions cs = null;
      
      //try
      //{
      //  cs = (ChannelSubscriptions)Serializer.Deserialize(typeof(ChannelSubscriptions), _userChannelSubscriptionsPath, _password);
      //}
      //catch (Exception ex)
      //{
      //  txt.Text = "Error getting channel subscriptions.\r\n" + ex.ToString();
      //  return;
      //}

      //Playlist playlist = null;

      //try
      //{
      //  playlist = PlaylistMaker.CreatePlaylist(_channelDataPath, _advertDataPath, _demographicDataPath, _generalDataPath, _playlistDataPath,
      //    _password, cs, new OxigenIIAdvertising.LoggerInfo.Logger("", @"C:\OxigenData\OxigenDebug.txt"));
      //}
      //catch (Exception ex)
      //{
      //  txt.Text = ex.ToString();
      //  return;
      //}

      //string output = "Channels: \r\n";

      //foreach (ChannelBucket cb in playlist.ChannelBuckets)
      //{
      //  output += "Average Play Time: " + cb.AveragePlayTime;
      //  output += "\r\nHigher Threshold: " + cb.HigherThresholdNormalised;
      //  output += "\r\nLower Threshold: " + cb.LowerThresholdNormalised;
      //  output += "\r\nPlaying Probability: " + cb.PlayingProbabilityNormalised;

      //  output += "\r\nPlaylist Assets:";

      //  foreach (PlaylistAsset pa in cb.ContentAssets)
      //  {
      //    output += "\r\nAsset ID: " + pa.AssetID;
      //    output += "\r\nAsset FileName: " + pa.AssetFilename;
      //    output += "\r\nClick Destination: " + pa.ClickDestination;
      //    output += "\r\nAsset Website: " + pa.AssetWebSite;
      //    output += "\r\nDisplay Length: " + pa.DisplayLength;
      //    output += "\r\nPlayer Type: " + pa.PlayerType;
      //  }
      //  output += "\r\n\r\n";
      //}

      //output += "\r\nAdverts:";

      //foreach (PlaylistAsset pa in playlist.AdvertBucket.AdvertAssets)
      //{
      //  output += "\r\nAsset ID: " + pa.AssetID;
      //  output += "\r\nFile Name: " + pa.AssetFilename;
      //  output += "\r\nClick Destination: " + pa.ClickDestination;
      //  output += "\r\n";
      //}

      //txt.Text = output;
    }

    private void PlayListTester_Load(object sender, EventArgs e)
    {

    }

    #region remove after end of testing
    //public struct ChannelAverageData
    //{
    //  private int _channelID;
    //  private float _averagePlayTime;
    //  private float _playingProbability;

    //  public int ChannelID
    //  {
    //    get { return _channelID; }
    //    set { _channelID = value; }
    //  }

    //  public float AveragePlayTime
    //  {
    //    get { return _averagePlayTime; }
    //    set { _averagePlayTime = value; }
    //  }

    //  public float PlayingProbability
    //  {
    //    get { return _playingProbability; }
    //    set { _playingProbability = value; }
    //  }

    //  public ChannelAverageData(int channelID, int averagePlayTime, int playingProbability)
    //  {
    //    _channelID = channelID;
    //    _averagePlayTime = averagePlayTime;
    //    _playingProbability = playingProbability;
    //  }
    //}
    #endregion
  }
}
