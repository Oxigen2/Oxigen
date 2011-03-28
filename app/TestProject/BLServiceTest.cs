using OxigenIIAdvertising.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TestProject
{


  /// <summary>
  ///This is a test class for BLServiceTest and is intended
  ///to contain all BLServiceTest Unit Tests
  ///</summary>
  [TestClass()]
  public class BLServiceTest
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

    [TestMethod()]
    [DeploymentItem("OxigenIISitesBLServices.dll")]
    public void GetUserScheduleStringTest()
    {
      BLService_Accessor target = new BLService_Accessor();
      string dayOfWeekCode = "1010100";
      string[] startEndDateTimes = new string[] { "10/02/2010||10/02/2011||10:00||20:01", "06/11/2009||10/02/2015||09:30||12:01" };
      string expected = "date >= 10/02/2010 and date <= 10/02/2011 and time >= 1000 and time <= 2001 and dayofweek = monday ||" +
        " date >= 06/11/2009 and date <= 10/02/2015 and time >= 0930 and time <= 1201 and dayofweek = monday ||" +
        " date >= 10/02/2010 and date <= 10/02/2011 and time >= 1000 and time <= 2001 and dayofweek = wednesday ||" +
        " date >= 06/11/2009 and date <= 10/02/2015 and time >= 0930 and time <= 1201 and dayofweek = wednesday ||" +
        " date >= 10/02/2010 and date <= 10/02/2011 and time >= 1000 and time <= 2001 and dayofweek = friday ||" +
        " date >= 06/11/2009 and date <= 10/02/2015 and time >= 0930 and time <= 1201 and dayofweek = friday";

      string actual;
      actual = target.GetScheduleString(dayOfWeekCode, startEndDateTimes);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    [DeploymentItem("OxigenIISitesBLServices.dll")]
    public void GetUserScheduleStringTestNoDateTime()
    {
      BLService_Accessor target = new BLService_Accessor();
      string dayOfWeekCode = "1010100";
      string[] startEndDateTimes = null;
      string expected = " and dayofweek = monday|| and dayofweek = wednesday|| and dayofweek = friday";

      string actual;
      actual = target.GetScheduleString(dayOfWeekCode, startEndDateTimes);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    [DeploymentItem("OxigenIISitesBLServices.dll")]
    public void GetUserScheduleStringTestNoStartDateTimeNoEndDateTime()
    {
      BLService_Accessor target = new BLService_Accessor();
      string dayOfWeekCode = "1010100";
      string[] startEndDateTimes = new string[] { "10/01/2010||10/02/2011||20:00||", "10/01/2010||10/02/2011||||22:00" };
      string expected = "date >= 10/01/2010 and date <= 10/02/2011 and time >= 2000 and dayofweek = monday ||" +
        " date >= 10/01/2010 and date <= 10/02/2011 and time <= 2200 and dayofweek = monday ||" +
        " date >= 10/01/2010 and date <= 10/02/2011 and time >= 2000 and dayofweek = wednesday ||" +
        " date >= 10/01/2010 and date <= 10/02/2011 and time <= 2200 and dayofweek = wednesday ||" +
        " date >= 10/01/2010 and date <= 10/02/2011 and time >= 2000 and dayofweek = friday ||" +       
        " date >= 10/01/2010 and date <= 10/02/2011 and time <= 2200 and dayofweek = friday";

      string actual;
      actual = target.GetScheduleString(dayOfWeekCode, startEndDateTimes);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    [DeploymentItem("OxigenIISitesBLServices.dll")]
    public void GetUserScheduleStringTestNoTimes()
    {
      BLService_Accessor target = new BLService_Accessor();
      string dayOfWeekCode = "1010100";
      string[] startEndDateTimes = new string[] { "10/01/2010||10/02/2011||||", "10/01/2010||10/02/2012||||" };
      string expected = "date >= 10/01/2010 and date <= 10/02/2011 and dayofweek = monday ||" +
        " date >= 10/01/2010 and date <= 10/02/2012 and dayofweek = monday ||" +
        " date >= 10/01/2010 and date <= 10/02/2011 and dayofweek = wednesday ||" +
        " date >= 10/01/2010 and date <= 10/02/2012 and dayofweek = wednesday ||" +
        " date >= 10/01/2010 and date <= 10/02/2011 and dayofweek = friday ||" +
        " date >= 10/01/2010 and date <= 10/02/2012 and dayofweek = friday";

      string actual;
      actual = target.GetScheduleString(dayOfWeekCode, startEndDateTimes);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    [DeploymentItem("OxigenIISitesBLServices.dll")]
    public void GetScheduleStringTestNoDaysOfWeek()
    {
      BLService_Accessor target = new BLService_Accessor();
      string dayOfWeekCode = "0000000";
      string[] startEndDateTimes = new string[] { "10/01/2010||10/02/2011||||", "10/01/2010||10/02/2012||||" };
      string expected = "";

      string actual;
      actual = target.GetScheduleString(dayOfWeekCode, startEndDateTimes);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    [DeploymentItem("OxigenIISitesBLServices.dll")]
    public void GetPresentationStringTestNoDaysOfWeek()
    {
      BLService_Accessor target = new BLService_Accessor();
      string dayOfWeekCode = "0000000";
      string[] startEndDateTimes = new string[] { "10/01/2010||10/02/2011||||", "10/01/2010||10/02/2012||||" };
      string expected = @"[""0"",""0"",""0"",""0"",""0"",""0"",""0""],[""10/01/2010"",""10/02/2011"","""",""""],[""10/01/2010"",""10/02/2012"","""",""""]";

      string actual;
      actual = target.GetPresentationString(dayOfWeekCode, startEndDateTimes);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    [DeploymentItem("OxigenIISitesBLServices.dll")]
    public void GetPresentationStringTestNoTimes()
    {
      BLService_Accessor target = new BLService_Accessor();
      string dayOfWeekCode = "1010100";
      string[] startEndDateTimes = new string[] { "10/01/2010||10/02/2011||||", "10/01/2010||10/02/2012||||" };
      string expected = @"[""1"",""0"",""1"",""0"",""1"",""0"",""0""],[""10/01/2010"",""10/02/2011"","""",""""],[""10/01/2010"",""10/02/2012"","""",""""]";

      string actual;
      actual = target.GetPresentationString(dayOfWeekCode, startEndDateTimes);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    [DeploymentItem("OxigenIISitesBLServices.dll")]
    public void GetPresentationStringTestNoOneEndDateNoTimes()
    {
      BLService_Accessor target = new BLService_Accessor();
      string dayOfWeekCode = "1010100";
      string[] startEndDateTimes = new string[] { "10/01/2010||||||", "10/01/2010||10/02/2012||||" };
      string expected = @"[""1"",""0"",""1"",""0"",""1"",""0"",""0""],[""10/01/2010"","""","""",""""],[""10/01/2010"",""10/02/2012"","""",""""]";

      string actual;
      actual = target.GetPresentationString(dayOfWeekCode, startEndDateTimes);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    [DeploymentItem("OxigenIISitesBLServices.dll")]
    public void GetUserScheduleStringTestNoScheduleGiven()
    {
      BLService_Accessor target = new BLService_Accessor();
      string dayOfWeekCode = "1010100";
      string[] startEndDateTimes = null;
      string expected = " and dayofweek = monday||" +
        " and dayofweek = wednesday||" +
        " and dayofweek = friday";

      string actual;
      actual = target.GetScheduleString(dayOfWeekCode, startEndDateTimes);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    [DeploymentItem("OxigenIISitesBLServices.dll")]
    public void GetPresetnatonStringTestNoScheduleGiven()
    {
      BLService_Accessor target = new BLService_Accessor();
      string dayOfWeekCode = "1010100";
      string[] startEndDateTimes = null;
      string expected = @"[""1"",""0"",""1"",""0"",""1"",""0"",""0""]";

      string actual;
      actual = target.GetPresentationString(dayOfWeekCode, startEndDateTimes);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    [DeploymentItem("OxigenIISitesBLServices.dll")]
    public void GetPresentationStringTest()
    {
      BLService_Accessor target = new BLService_Accessor();
      string dayOfWeekCode = "1010100";
      string[] startEndDateTimes = new string[] { "10/02/2010||10/02/2011||10:00||20:01", "06/11/2009||10/02/2015||09:30||12:01" };
      string expected = "[\"1\",\"0\",\"1\",\"0\",\"1\",\"0\",\"0\"],[\"10/02/2010\",\"10/02/2011\",\"10:00\",\"20:01\"],[\"06/11/2009\",\"10/02/2015\",\"09:30\",\"12:01\"]";
      string actual;
      actual = target.GetPresentationString(dayOfWeekCode, startEndDateTimes);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    [DeploymentItem("OxigenIISitesBLServices.dll")]
    public void GetPresentationStringTestNoDateTime()
    {
      BLService_Accessor target = new BLService_Accessor();
      string dayOfWeekCode = "1010100";
      string[] startEndDateTimes = null;
      string expected = "[\"1\",\"0\",\"1\",\"0\",\"1\",\"0\",\"0\"]";
      string actual;
      actual = target.GetPresentationString(dayOfWeekCode, startEndDateTimes);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    [DeploymentItem("OxigenIISitesBLServices.dll")]
    public void GetPresentationStringTestOneDateTime()
    {
      BLService_Accessor target = new BLService_Accessor();
      string dayOfWeekCode = "1010100";
      string[] startEndDateTimes = new string[] { "10/02/2010||10/02/2011||10:00||20:01" };
      string expected = "[\"1\",\"0\",\"1\",\"0\",\"1\",\"0\",\"0\"],[\"10/02/2010\",\"10/02/2011\",\"10:00\",\"20:01\"]";
      string actual;
      actual = target.GetPresentationString(dayOfWeekCode, startEndDateTimes);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    [DeploymentItem("OxigenIISitesBLServices.dll")]
    public void GetScheduleTest()
    {
      BLService_Accessor target = new BLService_Accessor();
      string startDate = "";
      string endDate = "";
      string startTime = "10:00";
      string endTime = "20:00";
      string dayOfWeekCode = "0100000";
      string schedule;
      string scheduleExpected = " and time >= 1000 and time <= 2000 and dayofweek = tuesday";
      string presentationSchedule;
      string presentationScheduleExpected = "[\"0\",\"1\",\"0\",\"0\",\"0\",\"0\",\"0\"],[\"\",\"\",\"10:00\",\"20:00\"]";
      target.GetSimpleSchedule(startDate, endDate, startTime, endTime, dayOfWeekCode, out schedule, out presentationSchedule);
      Assert.AreEqual(scheduleExpected, schedule);
      Assert.AreEqual(presentationScheduleExpected, presentationSchedule);
    }

    [TestMethod()]
    [DeploymentItem("OxigenIISitesBLServices.dll")]
    public void GetScheduleTestNothingFilledIn()
    {
      BLService_Accessor target = new BLService_Accessor();
      string startDate = "";
      string endDate = "";
      string startTime = "";
      string endTime = "";
      string dayOfWeekCode = "0000000";
      string schedule;
      string scheduleExpected = "time > 0000";
      string presentationSchedule;
      string presentationScheduleExpected = "[\"1\",\"1\",\"1\",\"1\",\"1\",\"1\",\"1\"],[\"" + System.DateTime.Now.ToShortDateString() + "\",\"" + System.DateTime.Now.AddYears(2).ToShortDateString() + "\",\"00:01\",\"23:59\"]";
      target.GetSimpleSchedule(startDate, endDate, startTime, endTime, dayOfWeekCode, out schedule, out presentationSchedule);
      Assert.AreEqual(scheduleExpected, schedule);
      Assert.AreEqual(presentationScheduleExpected, presentationSchedule);
    }

    [TestMethod()]
    [DeploymentItem("OxigenIISitesBLServices.dll")]
    public void GetScheduleTestDaysOnly()
    {
      BLService_Accessor target = new BLService_Accessor();
      string startDate = "";
      string endDate = "";
      string startTime = "";
      string endTime = "";
      string dayOfWeekCode = "0011000";
      string schedule;
      string scheduleExpected = " and dayofweek = wednesday ||  and dayofweek = thursday";
      string presentationSchedule;
      string presentationScheduleExpected = "[\"0\",\"0\",\"1\",\"1\",\"0\",\"0\",\"0\"],[\"\",\"\",\"\",\"\"]";
      target.GetSimpleSchedule(startDate, endDate, startTime, endTime, dayOfWeekCode, out schedule, out presentationSchedule);
      Assert.AreEqual(scheduleExpected, schedule);
      Assert.AreEqual(presentationScheduleExpected, presentationSchedule);
    }

    [TestMethod()]
    [DeploymentItem("OxigenIISitesBLServices.dll")]
    public void GetSimpleDBStringTestAllFull()
    {
      BLService_Accessor target = new BLService_Accessor();
      string startDate = "10/10/2010";
      string endDate = "10/11/2011";
      string startTime = "20:00";
      string endTime = "20:20";
      string dayOfWeekCode = "1111100";
      string expected = "date >= 10/10/2010 and date <= 10/11/2011 and time >= 2000 and time <= 2020 and dayofweek = monday || " +
        "date >= 10/10/2010 and date <= 10/11/2011 and time >= 2000 and time <= 2020 and dayofweek = tuesday || " +
        "date >= 10/10/2010 and date <= 10/11/2011 and time >= 2000 and time <= 2020 and dayofweek = wednesday || " +
        "date >= 10/10/2010 and date <= 10/11/2011 and time >= 2000 and time <= 2020 and dayofweek = thursday || " +
        "date >= 10/10/2010 and date <= 10/11/2011 and time >= 2000 and time <= 2020 and dayofweek = friday";
      string actual;
      actual = target.GetSimpleDBString(startDate, endDate, startTime, endTime, dayOfWeekCode);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    [DeploymentItem("OxigenIISitesBLServices.dll")]
    public void GetSimpleDBStringTestNoEndTime()
    {
      BLService_Accessor target = new BLService_Accessor();
      string startDate = "10/10/2010";
      string endDate = "10/11/2011";
      string startTime = "20:00";
      string endTime = "";
      string dayOfWeekCode = "1111100";
      string expected = "date >= 10/10/2010 and date <= 10/11/2011 and time >= 2000 and dayofweek = monday || " +
        "date >= 10/10/2010 and date <= 10/11/2011 and time >= 2000 and dayofweek = tuesday || " +
        "date >= 10/10/2010 and date <= 10/11/2011 and time >= 2000 and dayofweek = wednesday || " +
        "date >= 10/10/2010 and date <= 10/11/2011 and time >= 2000 and dayofweek = thursday || " +
        "date >= 10/10/2010 and date <= 10/11/2011 and time >= 2000 and dayofweek = friday";
      string actual;
      actual = target.GetSimpleDBString(startDate, endDate, startTime, endTime, dayOfWeekCode);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    [DeploymentItem("OxigenIISitesBLServices.dll")]
    public void GetSimpleDBStringTestNoStartEndTime()
    {
      BLService_Accessor target = new BLService_Accessor();
      string startDate = "10/10/2010";
      string endDate = "10/11/2011";
      string startTime = "";
      string endTime = "";
      string dayOfWeekCode = "1111100";
      string expected = "date >= 10/10/2010 and date <= 10/11/2011 and dayofweek = monday || " +
        "date >= 10/10/2010 and date <= 10/11/2011 and dayofweek = tuesday || " +
        "date >= 10/10/2010 and date <= 10/11/2011 and dayofweek = wednesday || " +
        "date >= 10/10/2010 and date <= 10/11/2011 and dayofweek = thursday || " +
        "date >= 10/10/2010 and date <= 10/11/2011 and dayofweek = friday";
      string actual;
      actual = target.GetSimpleDBString(startDate, endDate, startTime, endTime, dayOfWeekCode);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    [DeploymentItem("OxigenIISitesBLServices.dll")]
    public void GetSimpleDBStringTestOnlyStartDate()
    {
      BLService_Accessor target = new BLService_Accessor();
      string startDate = "10/10/2010";
      string endDate = "";
      string startTime = "";
      string endTime = "";
      string dayOfWeekCode = "1111100";
      string expected = "date >= 10/10/2010 and dayofweek = monday || " +
        "date >= 10/10/2010 and dayofweek = tuesday || " +
        "date >= 10/10/2010 and dayofweek = wednesday || " +
        "date >= 10/10/2010 and dayofweek = thursday || " +
        "date >= 10/10/2010 and dayofweek = friday";
      string actual;
      actual = target.GetSimpleDBString(startDate, endDate, startTime, endTime, dayOfWeekCode);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    [DeploymentItem("OxigenIISitesBLServices.dll")]
    public void GetSimpleDBStringTestNoDaysOfWeek()
    {
      BLService_Accessor target = new BLService_Accessor();
      string startDate = "10/10/2010";
      string endDate = "";
      string startTime = "";
      string endTime = "";
      string dayOfWeekCode = "0000000";
      string expected = "";
      string actual;
      actual = target.GetSimpleDBString(startDate, endDate, startTime, endTime, dayOfWeekCode);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    [DeploymentItem("OxigenIISitesBLServices.dll")]
    public void GetSimpleDBStringTestOneDayOfWeekOnlyStartDate()
    {
      BLService_Accessor target = new BLService_Accessor();
      string startDate = "10/10/2010";
      string endDate = "";
      string startTime = "";
      string endTime = "";
      string dayOfWeekCode = "0010000";
      string expected = "date >= 10/10/2010 and dayofweek = wednesday";
      string actual;
      actual = target.GetSimpleDBString(startDate, endDate, startTime, endTime, dayOfWeekCode);
      Assert.AreEqual(expected, actual);
    }
    
    [TestMethod()]
    [DeploymentItem("OxigenIISitesBLServices.dll")]
    public void GetSimplePresentationStringTestAllFull()
    {
      BLService_Accessor target = new BLService_Accessor(); 
      string startDate = "10/10/2010";
      string endDate = "10/11/2011";
      string startTime = "20:00";
      string endTime = "20:20";
      string dayOfWeekCode = "1111100";
      string expected = @"[""1"",""1"",""1"",""1"",""1"",""0"",""0""],[""10/10/2010"",""10/11/2011"",""20:00"",""20:20""]";
      string actual;
      actual = target.GetSimplePresentationString(startDate, endDate, startTime, endTime, dayOfWeekCode);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    [DeploymentItem("OxigenIISitesBLServices.dll")]
    public void GetSimplePresentationStringOnlyStartDate()
    {
      BLService_Accessor target = new BLService_Accessor();
      string startDate = "10/10/2010";
      string endDate = "";
      string startTime = "";
      string endTime = "";
      string dayOfWeekCode = "1111100";
      string expected = @"[""1"",""1"",""1"",""1"",""1"",""0"",""0""],[""10/10/2010"","""","""",""""]";
      string actual;
      actual = target.GetSimplePresentationString(startDate, endDate, startTime, endTime, dayOfWeekCode);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    [DeploymentItem("OxigenIISitesBLServices.dll")]
    public void GetSimplePresentationStringNoDaysOfWeek()
    {
      BLService_Accessor target = new BLService_Accessor();
      string startDate = "10/10/2010";
      string endDate = "";
      string startTime = "";
      string endTime = "20:00";
      string dayOfWeekCode = "0000000";
      string expected = @"[""0"",""0"",""0"",""0"",""0"",""0"",""0""],[""10/10/2010"","""","""",""20:00""]";
      string actual;
      actual = target.GetSimplePresentationString(startDate, endDate, startTime, endTime, dayOfWeekCode);
      Assert.AreEqual(expected, actual);
    }
  }
}