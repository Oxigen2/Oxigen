using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.InclusionExclusionRules;
using OxigenIIAdvertising.AppData;
using XmlSerializableSortableGenericList;

namespace OxigenIIAdvertising.TaxonomySearch
{
  public static class TreeSearch
  {
    /// <summary>
    /// Checks if an advert is to be included in the playlist by checking the channel classifications against the advert's rules
    /// (adverts selecting which channels they 'want' to be featured in).
    /// </summary>
    /// <param name="advertInclusionsExclusions">The list that holds the inclusion and exclusion rules. It has already been sorted from least specific to most specific.</param>
    /// <param name="channelDefinitions">All the channel definitions of the channels to which the end user is subscribed</param>
    /// <returns>true means advert is included in the playlist</returns>
    public static bool IncludeByChannelClassifications(HashSet<string> channelDefinitions, XmlSortableGenericList<InclusionExclusionRule> advertInclusionsExclusions)
    {
      // initialize the 'decision lollipop'
      bool bIncluded = true;

      foreach (string definition in channelDefinitions)
      {
        bIncluded = true;

        foreach (InclusionExclusionRule inclusionExclusionRule in advertInclusionsExclusions)
        {
          if (definition.StartsWith(inclusionExclusionRule.Rule))
            bIncluded = inclusionExclusionRule.IncEx == IncludeExclude.Include;
        }

        if (bIncluded)
          return true;
      }

      return false;
    }

    /// <summary>
    /// Checks if an advert is to be included in the playlist by checking the advert's classifications against the channel's rules
    /// (channels selecting which adverts to feature).
    /// The list that holds the inclusion and exclusion rules.It has already been sorted from least specific to most specific.
    /// </summary>
    /// <param name="advertDefinitions">All the advert definitions of all the adverts in the Advert Taxonomy tree</param>
    /// <param name="channelData">The Channel Data with advert exclusion/inclusion listings. The listings are already sorted from less to more specific (per channel and not overall).</param>
    /// <param name="channelSubscriptions">User's channel subscription class. Used to extract channel weighting information.</param>
    /// <returns>true means advert is included in the playlist</returns>
    public static bool IncludeByAdvertClassifications(HashSet<string> advertDefinitions, ChannelData channelData, ChannelSubscriptions channelSubscriptions)
    {
      // initialize the 'decision lolipop'
      bool bInclude = true;

      // initialize a Dictionary with channel inclusions exclusions
      Dictionary<Channel, bool> channelInclusionsExclusions = new Dictionary<Channel, bool>();

      // fill in the dictionary of channel inclusions exclusions with all the channels.
      // default all inclusions/exclusions to true, as bool is value-type
      foreach (Channel channel in channelData.Channels)
        channelInclusionsExclusions.Add(channel, true);

      // keep the voting thresholds of the channels who vote to exclude the advert
      HashSet<float> excludingVotingThresholds = new HashSet<float>();

      float currentChannelWeighting  = 0F;
      float totalChannelWeighting = 0F;
      float totalExcludeChannelWeighting = 0F;
      float averageExclusionPercentage = 0F;
      
      // Algorithm starts here
      foreach (Channel channel in channelData.Channels)
      {
        channelInclusionsExclusions[channel] = true;

        foreach (string advertDefinition in advertDefinitions)
        {
          bInclude = true;

          foreach (InclusionExclusionRule inclusionExclusionRule in channel.InclusionExclusionList)
          {
            if (advertDefinition.StartsWith(inclusionExclusionRule.Rule))
              bInclude = inclusionExclusionRule.IncEx == IncludeExclude.Include;
          }

          // Until either an Advert Classification (Definition) is classsified as Excluded
          if (!bInclude)
          {
            channelInclusionsExclusions[channel] = false;

            break;
          }
        }
      }

      // calculate the AEP
      foreach (Channel channel in channelData.Channels)
      {
        currentChannelWeighting = (float)channelSubscriptions.GetChannelWeightingUnnormalisedByChannelID(channel.ChannelID);

        totalChannelWeighting += currentChannelWeighting;

        // The channel weightings between 0 - 100
        if (!channelInclusionsExclusions[channel])
          totalExcludeChannelWeighting += currentChannelWeighting;
      }

      averageExclusionPercentage = (totalExcludeChannelWeighting / totalChannelWeighting) * 100F;

      foreach (Channel channel in channelData.Channels)
      {
        if (!channelInclusionsExclusions[channel])
        {
          // The voting thresholds are between 0 - 100
          if (averageExclusionPercentage >= channel.VotingThreshold)
            return false;
        }
      }

      return true;
    }
  }
}