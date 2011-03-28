using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.InclusionExclusionRules;
using XmlSerializableSortableGenericList;

namespace OxigenIIAdvertising.AppData
{
  /// <summary>
  /// Advert Asset object
  /// </summary>
  [Serializable]
  public class AdvertAsset : Asset
  {
    private string _advertDefinitions;
    private int _frequencyCap;
    private float _weighting;
    private XmlSortableGenericList<InclusionExclusionRule> _inclusionExclusionList;
    private string _taxonomyInformation;

    /// <summary>
    /// The taxonomy tree definitions, used for taxonomy tree search.
    /// the channel can be in more than one nodes in the Channel Taxonomy tree. If this is the case,
    /// this property will contain all the definitions in a pipe-delimited format.
    /// </summary>
    public string AdvertDefinitions
    {
      get { return _advertDefinitions; }
      set { _advertDefinitions = value; }
    }
    
    /// <summary>
    /// Maximum times per day the  advert can be played
    /// </summary>
    public int FrequencyCap
    {
      get { return _frequencyCap; }
      set { _frequencyCap = value; }
    }
        
    /// <summary>
    /// Advert Weighting. Not normalised
    /// </summary>
    public float Weighting
    {
      get { return _weighting; }
      set { _weighting = value; }
    }       

    /// <summary>
    /// Advert Taxonomy Tree rules that the channel votes to include or exclude
    /// </summary>
    public XmlSortableGenericList<InclusionExclusionRule> InclusionExclusionList
    {
      get { return _inclusionExclusionList; }
      set { _inclusionExclusionList = value; }
    }

    /// <summary>
    /// Documentation not yet available for this property
    /// </summary>
    public string TaxonomyInformation
    {
      get { return _taxonomyInformation; }
      set { _taxonomyInformation = value; }
    }   

    public AdvertAsset()
    {
      _inclusionExclusionList = new XmlSortableGenericList<InclusionExclusionRule>();
    }
  }
}
