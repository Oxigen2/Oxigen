using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace OxigenIIAdvertising.InclusionExclusionRules
{
  /// <summary>
  /// Class with inclusion exclusion of assets in a taxonomy tree
  /// </summary>
  [Serializable]
  public class InclusionExclusionRule : IComparable<InclusionExclusionRule>
  {
    private IncludeExclude _incEx;
    private string _rule;
    private static Regex _rxDot = new Regex("\\.", RegexOptions.Compiled);

    /// <summary>
    /// Included or Excluded
    /// </summary>
    public IncludeExclude IncEx
    {
      get { return _incEx; }
      set { _incEx = value; }
    }

    /// <summary>
    /// Taxonomy Tree Rule. Tree nodes are separated with dots.
    /// </summary>
    public string Rule
    {
      get { return _rule; }
      set { _rule = value; }
    }

    // parameterless constructor necessary for serialization/deserialization
    private InclusionExclusionRule() { }

    /// <summary>
    /// Constructor for InclusionExclusionRule
    /// </summary>
    /// <param name="incEx">Include or Exclude</param>
    /// <param name="rule">Taxonomy Tree Rule. Tree nodes are separated with dots.</param>
    public InclusionExclusionRule(IncludeExclude incEx, string rule)
    {
      _incEx = incEx;
      _rule = rule;
    }

    /// <summary>
    /// Comparison method for sorting a list  which this InclusionExclusionRule could exist in.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CompareTo(InclusionExclusionRule other)
    {
      int intNoRuleDots = _rxDot.Matches(_rule).Count;
      int intNoRuleOther = _rxDot.Matches(other.Rule).Count;

      return intNoRuleDots.CompareTo(intNoRuleOther);
    }
  }

  /// <summary>
  /// Enum to specify if a the asset specified by a rule is to be included or excluded
  /// </summary>
  public enum IncludeExclude {Include, Exclude}
}
