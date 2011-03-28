using OxigenIIAdvertising.PlaylistLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OxigenIIAdvertising.AppData;
using OxigenIIAdvertising.AssetScheduling;
using OxigenIIAdvertising.XMLSerializer;
using System;

namespace OxigenIITest
{
    
    
    /// <summary>
    ///This is a test class for PlaylistAssetPickerTest and is intended
    ///to contain all PlaylistAssetPickerTest Unit Tests
    ///</summary>
  [TestClass()]
  public class PlaylistAssetPickerTest
  {


    private TestContext testContextInstance;

    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext
    {
      get
      {
        return testContextInstance;
      }
      set
      {
        testContextInstance = value;
      }
    }

    #region Additional test attributes
    // 
    //You can use the following additional attributes as you write your tests:
    //
    //Use ClassInitialize to run code before running the first test in the class
    //[ClassInitialize()]
    //public static void MyClassInitialize(TestContext testContext)
    //{
    //}
    //
    //Use ClassCleanup to run code after all tests in a class have run
    //[ClassCleanup()]
    //public static void MyClassCleanup()
    //{
    //}
    //
    //Use TestInitialize to run code before running each test
    //[TestInitialize()]
    //public void MyTestInitialize()
    //{
    //}
    //
    //Use TestCleanup to run code after each test has run
    //[TestCleanup()]
    //public void MyTestCleanup()
    //{
    //}
    //
    #endregion


    /// <summary>
    ///A test for PickRandomAdvertPlaylistAsset
    ///</summary>
    //[TestMethod()]
    //public void PickRandomAdvertPlaylistAssetTest()
    //{
    //  Playlist playlist = (Playlist)Serializer.Deserialize(typeof(Playlist), @"c:\OxigenIIAppDataSamples\AppData\ss_play_list.dat", true, "password");
    //  AssetScheduler assetScheduler = new AssetScheduler();
    //  PlaylistAssetPicker playlistAssetPicker = new PlaylistAssetPicker(playlist, assetScheduler);
    //  AdvertPlaylistAsset actual = playlistAssetPicker.PickRandomAdvertPlaylistAsset();
    //  string scheduleinfo = ArrayToString(actual.ScheduleInfo);
    //  Assert.Inconclusive("Advert assets are picked at random. Check randomness of selected asset: ID " + actual.AssetFilename + ", Schedule info: " + scheduleinfo);
    //}

    /// <summary>
    ///A test for PickRandomContentPlaylistAsset
    ///</summary>
    [TestMethod()]
    public void PickRandomContentPlaylistAssetTest()
    {
      Playlist playlist = (Playlist)Serializer.Deserialize(typeof(Playlist), @"c:\OxigenIIAppDataSamples\AppData\ss_play_list.dat", true, "password");
      AssetScheduler assetScheduler = new AssetScheduler();
      ContentPlaylistAsset actual;

      PlaylistAssetPicker cpp = new PlaylistAssetPicker(playlist, assetScheduler);

      actual = cpp.SelectAsset();

      Assert.IsNotNull(actual);
    }

    /// <summary>
    ///A test for PickRandomContentPlaylistAsset
    ///Must return null as there are no content assets available for sunday
    ///</summary>
    [TestMethod()]
    public void PickRandomContentPlaylistAssetTestWithNoAvailableAsset()
    {
      Playlist playlist = (Playlist)Serializer.Deserialize(typeof(Playlist), @"c:\OxigenIIAppDataSamples\AppData\ss_play_list.dat", true, "password");
      AssetScheduler assetScheduler = new AssetScheduler();
      ContentPlaylistAsset actual;

      PlaylistAssetPicker cpp = new PlaylistAssetPicker(playlist, assetScheduler);

      actual = cpp.SelectAsset();

      Assert.AreEqual(null, actual);
    }

    private string ArrayToString(string[] array)
    {
      string s = "";

      foreach (string str in array)
      {
        s += str;
        s += ", ";
      }

      return s;
    }

    /// <summary>
    ///A test for PickRandomContentPlaylistAsset
    ///</summary>
    [TestMethod()]
    public void PickRandomContentPlaylistAssetTest1()
    {
      Playlist playlist = null; // TODO: Initialize to an appropriate value
      AssetScheduler assetScheduler = null; // TODO: Initialize to an appropriate value
      PlaylistAssetPicker target = new PlaylistAssetPicker(playlist, assetScheduler); // TODO: Initialize to an appropriate value
      ContentPlaylistAsset expected = null; // TODO: Initialize to an appropriate value
      ContentPlaylistAsset actual;
      actual = target.SelectAsset();
      Assert.AreEqual(expected, actual);
      Assert.Inconclusive("Verify the correctness of this test method.");
    }
  }
}
