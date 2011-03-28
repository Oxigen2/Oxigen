using OxigenIIAdvertising.LogWriter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OxigenIIAdvertising.FileLocker;
using System.IO;

namespace OxigenIITest
{


  /// <summary>
  ///This is a test class for LogFileWriterTest and is intended
  ///to contain all LogFileWriterTest Unit Tests
  ///</summary>
  [TestClass()]
  public class LockerTest
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
    ///A test for ReadDecryptFile
    ///</summary>
    [TestMethod()]
    public void ReadDecryptFileTest()
    {
      FileStream fileStream = null; 
      FileStream fileStreamExpected = null; 
      string inputPath = string.Empty; 
      string decryptionPassword = string.Empty; 
      bool bCritical = false; 
      MemoryStream expected = null; 
      MemoryStream actual;
      actual = Locker.ReadDecryptFile(ref fileStream, inputPath, decryptionPassword, bCritical);
      Assert.AreEqual(fileStreamExpected, fileStream);
      Assert.AreEqual(expected, actual);
      Assert.Inconclusive("Verify the correctness of this test method.");
    }
  }
}
