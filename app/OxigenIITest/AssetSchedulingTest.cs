using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using OxigenIIAdvertising.AssetScheduling;
using OxigenIIAdvertising.InclusionExclusionRules;

namespace OxigenIITest
{
  /// <summary>
  /// Class to test asset temporal scheduling
  /// 
  /// Tests will pass only if current time is within expected limits
  /// </summary>
  [TestClass]
  public class AssetSchedulingTest
  {
    public AssetSchedulingTest()
    {
    }

    [TestInitialize]
    public void Init()
    {
       if (DateTime.Now.Month != 10 && DateTime.Now.Day != 9
          && DateTime.Now.Year != 2009 && DateTime.Now.Hour != 19)
        throw new ArgumentException("Day must set to Friday 9 Oct 2009, 7pm");
    }

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

    /// <summary>
    ///A test for IsAssetPlayableIndividualLine
    ///
    ///</summary>
    [TestMethod()]
    [DeploymentItem("OxigenIIAdvertising.AssetScheduler.dll")]
    public void IsAssetPlayableIndividualLine1()
    {
      AssetScheduler assetScheduling = new AssetScheduler();

      PrivateObject assetSchedulingPrivateObj = new PrivateObject(assetScheduling);
      AssetScheduler_Accessor target = new AssetScheduler_Accessor(assetSchedulingPrivateObj);

      string inputCondition = "time>1550 and time<2000";

      bool expected = true;
      bool actual;
      actual = target.IsAssetPlayableIndividualLine(inputCondition);
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///A test for IsAssetPlayableIndividualLine
    ///
    ///</summary>
    [TestMethod()]
    [DeploymentItem("OxigenIIAdvertising.AssetScheduler.dll")]
    public void IsAssetPlayableIndividualLine2()
    {
      AssetScheduler assetScheduling = new AssetScheduler();

      PrivateObject assetSchedulingPrivateObj = new PrivateObject(assetScheduling);
      AssetScheduler_Accessor target = new AssetScheduler_Accessor(assetSchedulingPrivateObj);

      string inputCondition = "time>1550 and time<2000 and dayofweek < Thursday";

      bool expected = false;
      bool actual;
      actual = target.IsAssetPlayableIndividualLine(inputCondition);
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///A test for IsAssetPlayableIndividualLine
    ///
    ///</summary>
    [TestMethod()]
    [DeploymentItem("OxigenIIAdvertising.AssetScheduler.dll")]
    public void IsAssetPlayable1()
    {
      AssetScheduler assetScheduling = new AssetScheduler();

      PrivateObject assetSchedulingPrivateObj = new PrivateObject(assetScheduling);
      AssetScheduler_Accessor target = new AssetScheduler_Accessor(assetSchedulingPrivateObj);

      string inputCondition = "time>1550 and time<2000 and dayofweek > Thursday";

      bool expected = true;
      bool actual;
      actual = target.IsAssetPlayableIndividualLine(inputCondition);
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///A test for IsAssetPlayableIndividualLine
    ///
    ///</summary>
    [TestMethod()]
    [DeploymentItem("OxigenIIAdvertising.AssetScheduler.dll")]
    public void IsAssetPlayable2()
    {
      AssetScheduler assetScheduling = new AssetScheduler();

      PrivateObject assetSchedulingPrivateObj = new PrivateObject(assetScheduling);
      AssetScheduler_Accessor target = new AssetScheduler_Accessor(assetSchedulingPrivateObj);

      string inputCondition = "time>1550 and time<2000 and dayofweek = Friday";

      bool expected = true;
      bool actual;
      actual = target.IsAssetPlayableIndividualLine(inputCondition);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    public void IsAssetPlayableTest3()
    {
      AssetScheduler target = new AssetScheduler();
      string[] inputConditionsCollection = new string[3];

      inputConditionsCollection[0] = "time>1550 and time<1730 and dayofweek < Thursday";
      inputConditionsCollection[1] = "time>1550 and time<1730 and dayofweek >= Thursday and month>=7 and month<=9";
      inputConditionsCollection[2] = "dayofmonth = 9";
      
      bool expected = true;
      bool actual;
      actual = target.IsAssetPlayable(inputConditionsCollection);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    public void IsAssetPlayableTest4()
    {
      AssetScheduler target = new AssetScheduler();
      string[] inputConditionsCollection = new string[3];

      inputConditionsCollection[0] = "time>1550 and time<1730 and dayofweek < Thursday";
      inputConditionsCollection[1] = "time>1550 and time<1730 and dayofweek >= Thursday and month>=7 and month<=9";
      inputConditionsCollection[2] = "dayofmonth = 8";

      bool expected = false;
      bool actual;
      actual = target.IsAssetPlayable(inputConditionsCollection);
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///IsAssetPlayableTest
    ///
    /// expected:true
    ///</summary>
    [TestMethod()]
    public void IsAssetPlayableTest5()
    {
      AssetScheduler target = new AssetScheduler();
      string[] inputConditionsCollection = new string[4];

      inputConditionsCollection[0] = "time>1550 and time<1730 and dayofweek < Thursday";
      inputConditionsCollection[1] = "dayofmonth > 8";
      inputConditionsCollection[2] = "time>1550 and time<1730 and dayofweek >= Thursday and month>=7 and month<=9";
      inputConditionsCollection[3] = "day < Thursday and month>=7 and month<=9";

      bool expected = true;
      bool actual;
      actual = target.IsAssetPlayable(inputConditionsCollection);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    public void IsAssetPlayableTest6()
    {
      AssetScheduler target = new AssetScheduler();
      string[] inputConditionsCollection = new string[4];

      inputConditionsCollection[0] = "time>1550 and time<1730 and dayofweek < Thursday";
      inputConditionsCollection[1] = "dayofmonth > 10";
      inputConditionsCollection[2] = "time>1550 and time<1730 and dayofweek >= Thursday and month>=7 and month<=9";
      inputConditionsCollection[3] = "day < Thursday and month>=7 and month<=9";

      bool expected = false;
      bool actual;
      actual = target.IsAssetPlayable(inputConditionsCollection);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    public void IsAssetPlayableTest7()
    {
      AssetScheduler target = new AssetScheduler();
      string[] inputConditionsCollection = new string[1];

      inputConditionsCollection[0] = " time > 1400 and time < 1900 ";

      bool expected = false;
      bool actual;
      actual = target.IsAssetPlayable(inputConditionsCollection);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    public void TestOverflow()
    {
      AssetScheduler target = new AssetScheduler();
      string[] inputConditionsCollection = new string[1];

      inputConditionsCollection[0] = "dayofmonth > 10000000000000000000000000000000000000000000000000000000000000000000000";

      bool expected = false;
      bool actual;
      actual = target.IsAssetPlayable(inputConditionsCollection);
      Assert.AreEqual(expected, actual);
    }
  }
}
