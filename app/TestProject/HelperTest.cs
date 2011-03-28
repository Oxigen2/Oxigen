using OxigenIIPresentation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System;
using Setup;

namespace TestProject
{
    
    
    /// <summary>
    ///This is a test class for HelperTest and is intended
    ///to contain all HelperTest Unit Tests
    ///</summary>
  [TestClass()]
  public class HelperTest
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
    ///A test for DateWithinLimits
    ///</summary>
    // Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
    // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
    // whether you are testing a page, web service, or a WCF service.
    [TestMethod()]
    public void DateWithinLimitsTest1()
    {
      DateTime? date = new DateTime(234, 2, 2);
      bool expected = false;
      bool actual;
      actual = Helper.DateWithinLimits(date);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    public void DateWithinLimitsTest2()
    {
      DateTime? date = new DateTime(2343, 2, 2);
      bool expected = true;
      bool actual;
      actual = Helper.DateWithinLimits(date);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    public void DateWithinLimitsTest3()
    {
      DateTime? date = new DateTime(5, 2, 2);
      bool expected = false;
      bool actual;
      actual = Helper.DateWithinLimits(date);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    public void DateWithinLimitsTest4()
    {
      DateTime? date = new DateTime(2000, 2, 2);
      bool expected = true;
      bool actual;
      actual = Helper.DateWithinLimits(date);
      Assert.AreEqual(expected, actual);
    }
  }
}
