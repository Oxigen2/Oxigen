using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxigenIIAdvertising.InclusionExclusionRules
{
  /// <summary>
  /// Inclusion/Exclusion rule to be used for age, gender, socioeconomic group and geographical filtering.
  /// 
  /// Sorting is not needed for applying filtering rules in this case.
  /// This class is not used in pre-sorted Lists (lists sorted upon deserialization),
  /// and is a computationally cheaper structure as it does not implement IComparable and does not override CompareTo().
  /// </summary>
  public class InclusionExclusionRuleSyntax
  {
    private IncludeExclude _incEx;
    private string _rule;

    /// <summary>
    /// Include or Exclude the Rule member
    /// </summary>
    public IncludeExclude IncEx
    {
      get { return _incEx; }
      set { _incEx = value; }
    }

    /// <summary>
    /// Rule to filter against
    /// </summary>
    public string Rule
    {
      get { return _rule; }
      set { _rule = value; }
    }

    // used in serialization / deserializaton
    private InclusionExclusionRuleSyntax() { }

    /// <summary>
    /// Constructor for InclusionExclusionRuleSyntax
    /// </summary>
    /// <param name="incEx">include or exclude rule</param>
    /// <param name="rule">the rule to include or exclude</param>
    public InclusionExclusionRuleSyntax(IncludeExclude incEx, string rule)
    {
      _incEx = incEx;
      _rule = rule;
    }
  }
}
