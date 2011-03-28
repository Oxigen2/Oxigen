using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.InclusionExclusionRules;
using XmlSerializableSortableGenericList;

namespace OxigenIIAdvertising.AppData
{
  /// <summary>
  /// Channel object with asset information. Maps to [ChannelID property]_channel.dat
  /// </summary>
  [Serializable]
  public class Channel
  {
    private int _channelID;
    private string _channelGUID;
    private string _channelDefinitions;
    private XmlSortableGenericList<InclusionExclusionRule> _inclusionExclusionList;
    private float _votingThreshold;
    private HashSet<ChannelAsset> _channelAssets;

    /// <summary>
    /// The unique ID of the Channel
    /// </summary>
    public int ChannelID
    {
      get { return _channelID; }
      set { _channelID = value; }
    }

    /// <summary>
    /// The Global Unique Identifier of the Channel
    /// </summary>
    public string ChannelGUID
    {
      get { return _channelGUID; }
      set { _channelGUID = value; }
    }

    /// <summary>
    /// The taxonomy tree definitions, used for taxonomy tree search
    /// the channel can be in more than one nodes in the Channel Taxonomy tree. If this is the case,
    /// this property will contain all the definitions in a pipe-delimited format.
    /// </summary>
    public string ChannelDefinitions
    {
      get { return _channelDefinitions; }
      set { _channelDefinitions = value; }
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
    public float VotingThreshold
    {
      get { return _votingThreshold; }
      set { _votingThreshold = value; }
    }

    /// <summary>
    /// HashSet that holds content (non-advertising) assets
    /// </summary>
    public HashSet<ChannelAsset> ChannelAssets
    {
      get { return _channelAssets; }
      set { _channelAssets = value; }
    }

    /// <summary>
    /// Instantiating a channel will create the underlying HashSet of Assets and the inclusion and exclusion rules
    /// </summary>
    public Channel()
    {
      _channelAssets = new HashSet<ChannelAsset>();
      _inclusionExclusionList = new XmlSortableGenericList<InclusionExclusionRule>();
    }
  }
}
