using OxigenIIAdvertising.TaxonomySearch;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OxigenIIAdvertising.InclusionExclusionRules;
using XmlSerializableSortableGenericList;
using OxigenIIAdvertising.AppData;
using OxigenIIAdvertising.XMLSerializer;

namespace OxigenIITest
{
    
    
    /// <summary>
    ///This is a test class for TreeSearchTest and is intended
    ///to contain all TreeSearchTest Unit Tests
    ///</summary>
  [TestClass()]
  public class TreeSearchTest
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
    ///A test for IncludeByChannelClassifications
    ///</summary>
    [TestMethod()]
    public void IncludeByChannelClassificationsTestIncl()
    {
      HashSet<string> channelDefinitions = new HashSet<string>();
      channelDefinitions.Add("0.1.2.3.4");
      channelDefinitions.Add("0.1.2");
      channelDefinitions.Add("0.2.2");
      channelDefinitions.Add("0.1.3");
      channelDefinitions.Add("0.1.1");
            
      XmlSortableGenericList<InclusionExclusionRule> advertInclusionsExclusions = new XmlSortableGenericList<InclusionExclusionRule>();
      advertInclusionsExclusions.Add(new InclusionExclusionRule(IncludeExclude.Exclude, "0.1"));
      advertInclusionsExclusions.Add(new InclusionExclusionRule(IncludeExclude.Exclude, "0.1.2"));
      advertInclusionsExclusions.Add(new InclusionExclusionRule(IncludeExclude.Include, "0.1.1"));
                  
      bool expected = true;
      bool actual;
      actual = TreeSearch.IncludeByChannelClassifications(channelDefinitions, advertInclusionsExclusions);
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///A test for IncludeByChannelClassifications
    ///</summary>
    [TestMethod()]
    public void IncludeByChannelClassificationsTestExcl()
    {
      HashSet<string> channelDefinitions = new HashSet<string>();
      channelDefinitions.Add("0.1.2.3.4");
      channelDefinitions.Add("0.1.2");
      channelDefinitions.Add("0.1.3");
      channelDefinitions.Add("0.1.1");

      XmlSortableGenericList<InclusionExclusionRule> advertInclusionsExclusions = new XmlSortableGenericList<InclusionExclusionRule>();
      advertInclusionsExclusions.Add(new InclusionExclusionRule(IncludeExclude.Exclude, "0.1"));
      advertInclusionsExclusions.Add(new InclusionExclusionRule(IncludeExclude.Exclude, "0.1.2"));
      advertInclusionsExclusions.Add(new InclusionExclusionRule(IncludeExclude.Exclude, "0.1.1"));
      advertInclusionsExclusions.Add(new InclusionExclusionRule(IncludeExclude.Exclude, "0.2.1"));

      bool expected = false;
      bool actual;
      actual = TreeSearch.IncludeByChannelClassifications(channelDefinitions, advertInclusionsExclusions);
      Assert.AreEqual(expected, actual);
    }
  }
}
