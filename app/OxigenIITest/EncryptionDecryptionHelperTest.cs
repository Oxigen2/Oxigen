using OxigenIIAdvertising.EncryptionDecryption;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace OxigenIITest
{
    
    
    /// <summary>
    ///This is a test class for EncryptionDecryptionHelperTest and is intended
    ///to contain all EncryptionDecryptionHelperTest Unit Tests
    ///</summary>
  [TestClass()]
  public class EncryptionDecryptionHelperTest
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
    ///A test for TryIfAssetDecryptable
    ///</summary>
    [TestMethod()]
    public void TryIfAssetDecryptableTestFileNotFound()
    {
      string filePath = @"C:\OxigenIIData\Assets\fgwruiowhegi.jpg";
      string password = "password";
      bool expected = false;
      bool actual = EncryptionDecryptionHelper.TryIfFileDecryptable(filePath, password);
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///A test for TryIfAssetDecryptable
    ///</summary>
    [TestMethod()]
    public void TryIfAssetDecryptableTestClearFile()
    {
      string filePath = @"C:\OxigenIIData\Assets\decrypted\advert1100.jpg";
      string password = "password";
      bool expected = false;
      bool actual = EncryptionDecryptionHelper.TryIfFileDecryptable(filePath, password);
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///A test for TryIfAssetDecryptable
    ///</summary>
    [TestMethod()]
    public void TryIfAssetDecryptableTestWrongPassword()
    {
      string filePath = @"C:\OxigenIIData\Assets\advert1100.jpg";
      string password = "password1";
      bool expected = false;
      bool actual = EncryptionDecryptionHelper.TryIfFileDecryptable(filePath, password);
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///A test for TryIfAssetDecryptable
    ///</summary>
    [TestMethod()]
    public void TryIfAssetDecryptableTestGoodPasswordAndFile()
    {
      string filePath = @"C:\OxigenIIData\Assets\advert1100.jpg";
      string password = "password";
      bool expected = true;
      bool actual = EncryptionDecryptionHelper.TryIfFileDecryptable(filePath, password);
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///A test for TryIfAssetDecryptable
    ///</summary>
    [TestMethod()]
    [ExpectedException(typeof(System.IO.DirectoryNotFoundException))]
    public void TryIfAssetDecryptableTestDirectoryNotFound()
    {
      string filePath = @"C:\OxigenIIData\Assets1\advert1100.jpg";
      string password = "password";
      bool actual = EncryptionDecryptionHelper.TryIfFileDecryptable(filePath, password);
      Assert.Fail("Expected exception not thrown");
    }

    /// <summary>
    ///A test for TryIfAssetDecryptable
    ///</summary>
    [TestMethod()]
    public void TryIfAssetDecryptableTest()
    {
      string filePath = string.Empty; 
      string password = string.Empty; 
      bool expected = false; 
      bool actual;
      actual = EncryptionDecryptionHelper.TryIfFileDecryptable(filePath, password);
      Assert.AreEqual(expected, actual);
      Assert.Inconclusive("Verify the correctness of this test method.");
    }
  }
}
