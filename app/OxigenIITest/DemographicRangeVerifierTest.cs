using OxigenIIAdvertising.DemographicRange;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using OxigenIIAdvertising.InclusionExclusionRules;
using OxigenIIAdvertising.Demographic;

namespace OxigenIITest
{
    
    
    /// <summary>
    ///This is a test class for DemographicRangeVerifierTest and is intended
    ///to contain all DemographicRangeVerifierTest Unit Tests
    ///</summary>
  [TestClass()]
  public class DemographicRangeVerifierTest
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

    /// <summary>
    ///A test for IsAssetPlayableIndividualLine
    ///</summary>
    [TestMethod()]
    [DeploymentItem("DemographicRange.dll")]
    public void IsAssetPlayableIndividualLineTestLeadingTrailingConjunction()
    {
      DemographicData demographicData = null;
      DemographicRangeVerifier demographicRangeVerifier = null;

      demographicData = new DemographicData();

      demographicData.SocioEconomicgroup = new string[] { "a1", "a2" };
      demographicData.MaxAge = 30;
      demographicData.MinAge = 20;
      demographicData.GeoDefinition = "0.1.2";
      demographicData.Gender = new string[] { "Male" };

      demographicRangeVerifier = new DemographicRangeVerifier(demographicData);

      PrivateObject demographicRangeVerifierPrivate = new PrivateObject(demographicRangeVerifier);
      DemographicRangeVerifier_Accessor target = new DemographicRangeVerifier_Accessor(demographicRangeVerifierPrivate);
      bool expected = false;
      bool actual;
      actual = target.IsAssetDemoSyntaxPlayableIndividualLine("age=20 and gender=male,female and socioeconomicgroup=a3,a2");
      Assert.AreEqual(expected, actual, "Verify the correctness of this test method.");
    }

    /// <summary>
    ///A test for IsAssetDemoSyntaxPlayableIndividualLine
    ///Expected: false
    ///</summary>
    [TestMethod()]
    [DeploymentItem("DemographicRange.dll")]
    public void IsAssetDemoSyntaxPlayableIndividualLineTestUnSuccessfulConditionsInclude()
    {
      DemographicData demographicData = new DemographicData();

      demographicData.SocioEconomicgroup = new string[] { "a1", "a2" };
      demographicData.MaxAge = 35;
      demographicData.MinAge = 30;
      demographicData.GeoDefinition = "0.1.2";
      demographicData.Gender = new string[] { "male" };

      DemographicRangeVerifier demographicRangeVerifier = new DemographicRangeVerifier(demographicData);

      PrivateObject demographicRangeVerifierPrivate = new PrivateObject(demographicRangeVerifier);
      DemographicRangeVerifier_Accessor target = new DemographicRangeVerifier_Accessor(demographicRangeVerifierPrivate);
      string ie = "age=20 and gender=male,female and socioeconomicgroup=a3,a2";

      bool expected = false;
      bool actual;
      actual = target.IsAssetDemoSyntaxPlayableIndividualLine(ie);
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///A test for IsAssetDemoSyntaxPlayableIndividualLine
    ///
    /// Expected: true
    ///</summary>
    [TestMethod()]
    [DeploymentItem("DemographicRange.dll")]
    public void IsAssetDemoSyntaxPlayableIndividualLineTestSuccessfulConditionsInclude()
    {
      DemographicData demographicData = new DemographicData();

      demographicData.SocioEconomicgroup = new string[] { "a1", "a2" };
      demographicData.MaxAge = 35;
      demographicData.MinAge = 30;
      demographicData.GeoDefinition = "0.1.2";
      demographicData.Gender = new string[] { "male" };

      DemographicRangeVerifier demographicRangeVerifier = new DemographicRangeVerifier(demographicData);

      PrivateObject demographicRangeVerifierPrivate = new PrivateObject(demographicRangeVerifier);
      DemographicRangeVerifier_Accessor target = new DemographicRangeVerifier_Accessor(demographicRangeVerifierPrivate);
      string ie = "age=34 and gender=male,female and socioeconomicgroup=a3,a2";

      bool expected = true;
      bool actual;
      actual = target.IsAssetDemoSyntaxPlayableIndividualLine(ie);
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///A test for IsAssetDemoSyntaxPlayableIndividualLine
    ///
    /// Expected: true
    ///</summary>
    [TestMethod()]
    [DeploymentItem("DemographicRange.dll")]
    public void IsAssetDemoSyntaxPlayableIndividualLineTestSuccessfulConditionsExclude()
    {
      DemographicData demographicData = new DemographicData();

      demographicData.SocioEconomicgroup = new string[] { "a1", "a2" };
      demographicData.MaxAge = 35;
      demographicData.MinAge = 30;
      demographicData.GeoDefinition = "0.1.2";
      demographicData.Gender = new string[] { "male" };

      DemographicRangeVerifier demographicRangeVerifier = new DemographicRangeVerifier(demographicData);

      PrivateObject demographicRangeVerifierPrivate = new PrivateObject(demographicRangeVerifier);
      DemographicRangeVerifier_Accessor target = new DemographicRangeVerifier_Accessor(demographicRangeVerifierPrivate);
      string ie = "age=34 and gender=male,female and socioeconomicgroup=a3,a2";

      bool expected = true;
      bool actual;
      actual = target.IsAssetDemoSyntaxPlayableIndividualLine(ie);
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///A test for IsAssetDemoSyntaxPlayableIndividualLine
    ///
    /// Expected: false
    ///</summary>
    [TestMethod()]
    [DeploymentItem("DemographicRange.dll")]
    public void IsAssetDemoSyntaxPlayableIndividualLineTestUnSuccessfulConditionsExclude()
    {
      DemographicData demographicData = new DemographicData();

      demographicData.SocioEconomicgroup = new string[] { "a1", "a2" };
      demographicData.MaxAge = 35;
      demographicData.MinAge = 30;
      demographicData.GeoDefinition = "0.1.2";
      demographicData.Gender = new string[] { "male" };

      DemographicRangeVerifier demographicRangeVerifier = new DemographicRangeVerifier(demographicData);

      PrivateObject demographicRangeVerifierPrivate = new PrivateObject(demographicRangeVerifier);
      DemographicRangeVerifier_Accessor target = new DemographicRangeVerifier_Accessor(demographicRangeVerifierPrivate);
      string ie = "age=34 and gender=female and socioeconomicgroup=a3,a2";

      bool expected = false;
      bool actual;
      actual = target.IsAssetDemoSyntaxPlayableIndividualLine(ie);
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///A test for IsAssetDemoSyntaxPlayableIndividualLine
    ///
    /// Expected: false
    ///</summary>
    [TestMethod()]
    [DeploymentItem("DemographicRange.dll")]
    public void IsAssetDemoSyntaxPlayableIndividualLineTestUnSuccessfulConditionsExcludeGTT()
    {
      DemographicData demographicData = new DemographicData();

      demographicData.SocioEconomicgroup = new string[] { "a1", "a2" };
      demographicData.MaxAge = 35;
      demographicData.MinAge = 30;
      demographicData.GeoDefinition = "0.1.2";
      demographicData.Gender = new string[] { "male" };

      DemographicRangeVerifier demographicRangeVerifier = new DemographicRangeVerifier(demographicData);

      PrivateObject demographicRangeVerifierPrivate = new PrivateObject(demographicRangeVerifier);
      DemographicRangeVerifier_Accessor target = new DemographicRangeVerifier_Accessor(demographicRangeVerifierPrivate);
      string ie = "age=34 and gender=male and geo=0.7.8 and socioeconomicgroup=a3,a2";

      bool expected = false;
      bool actual;
      actual = target.IsAssetDemoSyntaxPlayableIndividualLine(ie);
      Assert.AreEqual(expected, actual);
    }


    /// <summary>
    ///A test for IsAssetDemoSyntaxPlayableIndividualLine
    ///
    /// Expected: true
    ///</summary>
    [TestMethod()]
    [DeploymentItem("DemographicRange.dll")]
    public void IsAssetDemoSyntaxPlayableIndividualLineTestSuccessfulConditionsIncludeGTT()
    {
      DemographicData demographicData = new DemographicData();

      demographicData.SocioEconomicgroup = new string[] { "a1", "a2" };
      demographicData.MaxAge = 35;
      demographicData.MinAge = 30;
      demographicData.GeoDefinition = "0.1.2";
      demographicData.Gender = new string[] { "male" };

      DemographicRangeVerifier demographicRangeVerifier = new DemographicRangeVerifier(demographicData);

      PrivateObject demographicRangeVerifierPrivate = new PrivateObject(demographicRangeVerifier);
      DemographicRangeVerifier_Accessor target = new DemographicRangeVerifier_Accessor(demographicRangeVerifierPrivate);
      string ie = "age=34 and gender=male and geo=0.1 and socioeconomicgroup=a3,a2";

      bool expected = true;
      bool actual;
      actual = target.IsAssetDemoSyntaxPlayableIndividualLine(ie);
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///A test for IsAssetDemoSyntaxPlayableIndividualLine
    ///
    /// Expected: true
    ///</summary>
    [TestMethod()]
    [DeploymentItem("DemographicRange.dll")]
    public void IsAssetDemoSyntaxPlayableIndividualLineTestSuccessfulConditionsIncludeGTTDeeperRule()
    {
      DemographicData demographicData = new DemographicData();

      demographicData.SocioEconomicgroup = new string[] { "a1", "a2" };
      demographicData.MaxAge = 35;
      demographicData.MinAge = 30;
      demographicData.GeoDefinition = "0.1.2";
      demographicData.Gender = new string[] { "male" };

      DemographicRangeVerifier demographicRangeVerifier = new DemographicRangeVerifier(demographicData);

      PrivateObject demographicRangeVerifierPrivate = new PrivateObject(demographicRangeVerifier);
      DemographicRangeVerifier_Accessor target = new DemographicRangeVerifier_Accessor(demographicRangeVerifierPrivate);
      string ie = "age=34 and gender=male and geo=0.1.2.3 and socioeconomicgroup=a3,a2";

      bool expected = false;
      bool actual;
      actual = target.IsAssetDemoSyntaxPlayableIndividualLine(ie);
      Assert.AreEqual(expected, actual);
    }


    /// <summary>
    ///A test for IsAssetDemoSyntaxPlayable
    ///
    /// Expected: true
    ///</summary>
    [TestMethod()]
    public void IsAssetDemoSyntaxPlayableTestSuccessfulConditions()
    {
      DemographicData demographicData = new DemographicData();

      demographicData.SocioEconomicgroup = new string[] { "a1", "a2" };
      demographicData.MaxAge = 35;
      demographicData.MinAge = 30;
      demographicData.GeoDefinition = "0.1.2";
      demographicData.Gender = new string[] { "male" };

      DemographicRangeVerifier demographicRangeVerifier = new DemographicRangeVerifier(demographicData);

      string[] inputConditionCollection = new string[3];

      inputConditionCollection[0] = "age > 30 and gender=male and socioeconomicgroup=a2 and geo=0"; // will be met and excluded
      inputConditionCollection[1] = "age > 30 and gender=male and socioeconomicgroup=a2 and geo=0.1"; // will be met and included, loop should stop here
      inputConditionCollection[2] = "age > 35 and gender=male and socioeconomicgroup=a2 and geo=0.2"; // will be not met and included
      
      bool expected = true;
      bool actual;
      actual = demographicRangeVerifier.IsAssetDemoSyntaxPlayable(inputConditionCollection);
      Assert.AreEqual(expected, actual, "Verify the correctness of this test method.");
    }

    /// <summary>
    ///A test for IsAssetDemoSyntaxPlayable
    ///
    /// Expected: true
    ///</summary>
    [TestMethod()]
    public void IsAssetDemoSyntaxPlayableTestSuccessfulConditionsExcludeMetIncludeMet()
    {
      DemographicData demographicData = new DemographicData();

      demographicData.SocioEconomicgroup = new string[] { "a1", "a2" };
      demographicData.MaxAge = 35;
      demographicData.MinAge = 30;
      demographicData.GeoDefinition = "0.1.2";
      demographicData.Gender = new string[] { "male" };

      DemographicRangeVerifier demographicRangeVerifier = new DemographicRangeVerifier(demographicData);

      string[] inputConditionCollection = new string[3];

      inputConditionCollection[0] = "age > 30 and gender=male and socioeconomicgroup=a2 and geo=0"; // will be met and excluded
      inputConditionCollection[1] = "age < 33 and gender=male and socioeconomicgroup=a2 and geo=0.1"; // will be met and included, loop should stop here
      inputConditionCollection[2] = "age > 35 and gender=male and socioeconomicgroup=a2 and geo=0.2"; // will be not met and included

      bool expected = true;
      bool actual;
      actual = demographicRangeVerifier.IsAssetDemoSyntaxPlayable(inputConditionCollection);
      Assert.AreEqual(expected, actual, "Verify the correctness of this test method.");
    }

    /// <summary>
    /// A test for IsAssetDemoSyntaxPlayable
    ///
    /// Expected: true
    ///</summary>
    [TestMethod()]
    public void IsAssetDemoSyntaxPlayableTestSuccessfulConditionsIncludeExcludeSameMet()
    {
      DemographicData demographicData = new DemographicData();

      demographicData.SocioEconomicgroup = new string[] { "a1", "a2" };
      demographicData.MaxAge = 35;
      demographicData.MinAge = 30;
      demographicData.GeoDefinition = "0.1.2";
      demographicData.Gender = new string[] { "male" };

      DemographicRangeVerifier demographicRangeVerifier = new DemographicRangeVerifier(demographicData);

      string[] inputConditionCollection = new string[3];

      inputConditionCollection[0] = "age > 30 and gender=male and socioeconomicgroup=a2 and geo=0.1"; // will be met and excluded
      inputConditionCollection[1] = "age > 30 and gender=male and socioeconomicgroup=a2 and geo=0.1"; // will be met and excluded
      inputConditionCollection[2] = "age > 35 and gender=male and socioeconomicgroup=a2 and geo=0.3"; // will be not met and included

      bool expected = true;
      bool actual;
      actual = demographicRangeVerifier.IsAssetDemoSyntaxPlayable(inputConditionCollection);
      Assert.AreEqual(expected, actual, "Verify the correctness of this test method.");
    }

    /// <summary>
    /// A test for IsAssetDemoSyntaxPlayable
    ///
    /// Expected: true
    ///</summary>
    [TestMethod()]
    public void IsAssetDemoSyntaxPlayableTestSuccessfulConditionsExcludeIncludeSameMet()
    {
      DemographicData demographicData = new DemographicData();

      demographicData.SocioEconomicgroup = new string[] { "a1", "a2" };
      demographicData.MaxAge = 35;
      demographicData.MinAge = 30;
      demographicData.GeoDefinition = "0.1.2";
      demographicData.Gender = new string[] { "male" };

      DemographicRangeVerifier demographicRangeVerifier = new DemographicRangeVerifier(demographicData);

      string[] inputConditionCollection = new string[3];

      inputConditionCollection[0] = "age > 30 and gender=male and socioeconomicgroup=a2 and geo=0.1"; // will be met and excluded
      inputConditionCollection[1] = "age > 30 and gender=male and socioeconomicgroup=a2 and geo=0.1"; // will be met and excluded
      inputConditionCollection[2] = "age > 35 and gender=male and socioeconomicgroup=a2 and geo=0.3"; // will be not met and included

      bool expected = true;
      bool actual;
      actual = demographicRangeVerifier.IsAssetDemoSyntaxPlayable(inputConditionCollection);
      Assert.AreEqual(expected, actual, "Verify the correctness of this test method.");
    }

    /// <summary>
    ///A test for IsAssetDemoSyntaxPlayable
    ///</summary>
    [TestMethod()]
    public void IsAssetDemoSyntaxPlayableTest()
    {
      DemographicData demographicData = null; 
      DemographicRangeVerifier target = new DemographicRangeVerifier(demographicData); 
      string[] inputConditionCollection = null; 
      bool expected = false; 
      bool actual;
      actual = target.IsAssetDemoSyntaxPlayable(inputConditionCollection);
      Assert.AreEqual(expected, actual);
      Assert.Inconclusive("Verify the correctness of this test method.");
    }
  }
}
