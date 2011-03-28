using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using OxigenIIAdvertising.Demographic;
using System.Collections.ObjectModel;
using OxigenIIAdvertising.InclusionExclusionRules;
using System.Text.RegularExpressions;

namespace OxigenIIAdvertising.DemographicRange
{
  /// <summary>
  /// Class to check demographic question answer that use the condition syntax used in OxigenII.
  /// </summary>
  public class DemographicRangeVerifier
  {
    private Hashtable _htDemographicQuestions = null;
    private string[] _comparisonOperators = null;
    private Hashtable _genders;
    private DemographicData _demographicData = null;
    Regex _equalityOperators = new Regex(@"(?:<=|>=|!=|<|>|=)", RegexOptions.Compiled);

    /// <summary>
    /// Initializes a DemographicAgeVerifier object and loads up the condition syntax
    /// and demographic keywords used in OxigenII.
    /// </summary>
    /// <param name="demographicData">Single or mutliple users' demographic data</param>
    public DemographicRangeVerifier(DemographicData demographicData)
    {
      _htDemographicQuestions = GetDemographicQuestions();
      _comparisonOperators = GetComparisonOperators();
      _genders = GetGenders();
      _demographicData = demographicData;
    }

    /// <summary>
    /// Uses an OR syntax to determine if asset in question is playable. If one positive condition is found,
    /// the asset is playable.
    /// </summary>
    /// <param name="inputConditionCollection">all input conditions for the asset</param>
    /// <returns>true if asset is playable, false otherwise</returns>
    public bool IsAssetDemoSyntaxPlayable(string[] inputConditionCollection)
    {
      // if no asset conditions, then include
      if (inputConditionCollection == null)
        return true;

      foreach (string inputCondition in inputConditionCollection)
      {
        if (IsAssetDemoSyntaxPlayableIndividualLine(inputCondition))
          return true;
      }

      return false;
    }

    private bool IsAssetDemoSyntaxPlayableIndividualLine(string inputCondition)
    {
      inputCondition = inputCondition.Replace(" ", "").ToLower();

      string[] conditions = inputCondition.Split(new string[] { "and" }, StringSplitOptions.RemoveEmptyEntries);

      string[] comparisonOperators = GetComparisonOperators();

      string variable = "";
      string value = "";
      string comparisonOperator = "";

      foreach (string condition in conditions)
      {
        comparisonOperator = (_equalityOperators.Match(condition)).Value;

        string[] operands = _equalityOperators.Split(condition); // if comparison operator not found, condition won't split

        if (operands.Length != 2)
          return false;
        else
        {
          variable = operands[0];
          value = operands[1];

          if (!_htDemographicQuestions.Contains(variable))
            return false;

          if (!DemographicVerifyingPassed(variable, value, comparisonOperator))
            return false;
        }
      }
     
      return true;
    }

    private bool AgePlayable(string value, string comparisonOperator)
    {
      int numericalValue;

      if (!int.TryParse(value, out numericalValue))
        return false;

      switch (comparisonOperator)
      {
        case "=":
          return numericalValue >= _demographicData.MinAge && numericalValue <= _demographicData.MaxAge;

        case ">=":
          return _demographicData.MaxAge >= numericalValue;

        case "<=":
          return _demographicData.MinAge <= numericalValue;

        case ">":
          return _demographicData.MaxAge > numericalValue;

        case "<":
          return _demographicData.MinAge < numericalValue;

        case "!=":
          return _demographicData.MinAge == _demographicData.MaxAge && _demographicData.MinAge != numericalValue;
      }

      return false;
    }

    private bool GenderPlayable(string value, string comparisonOperator)
    {
      if (comparisonOperator != "=")
        return false;

      string[] condGender = value.Split(',');

      return AreStringArraysIntersected(_demographicData.Gender, condGender);
    }

    private bool SocioEconomicGroupPlayable(string value, string comparisonOperator)
    {
      if (comparisonOperator != "=")
        return false;

      string[] condSocioEconomicGroup = value.Split(',');

      return AreStringArraysIntersected(_demographicData.SocioEconomicgroup, condSocioEconomicGroup);
    }

    private bool GeoTaxonomyPlayable(string value, string comparisonOperator)
    {
      if (comparisonOperator != "=")
        return false;

      return _demographicData.GeoDefinition.StartsWith(value);
    }

    private bool DemographicVerifyingPassed(string variable, string value, string comparisonOperator)
    {
      switch (variable)
      {
        case "age":
          return AgePlayable(value, comparisonOperator);

        case "gender":
          return GenderPlayable(value, comparisonOperator);

        case "socioeconomicgroup":
          return SocioEconomicGroupPlayable(value, comparisonOperator);

        case "geo":
          return GeoTaxonomyPlayable(value, comparisonOperator);
      }

      return false;
    }

    /// <summary>
    /// Checks if the two arrays of string have common strings. The method does a lazy evaluation (returns true if one match is found).
    /// </summary>
    /// <param name="sa1">the first array of strings</param>
    /// <param name="sa2">the second array of strings</param>
    /// <returns>true if the two arrays are intersected in one point</returns>
    private bool AreStringArraysIntersected(string[] sa1, string[] sa2)
    {
      foreach (string s1 in sa1)
      {
        if (Array.IndexOf(sa2, s1) != -1)
          return true;
      }

      return false;
    }

    private Hashtable GetDemographicQuestions()
    {
      Hashtable ht = new Hashtable();

      ht.Add("age", "");
      ht.Add("gender", "");
      ht.Add("socioeconomicgroup", "");
      ht.Add("geo", "");

      return ht;
    }

    private Hashtable GetGenders()
    {
      Hashtable ht = new Hashtable();

      ht.Add("male", "");
      ht.Add("female", "");

      return ht;
    }

    private string[] GetComparisonOperators()
    {
      string[] comparisonOperators = new string[] { ">=", "<=", ">", "<", "!=", "=" };

      return comparisonOperators;
    }
  }
}
