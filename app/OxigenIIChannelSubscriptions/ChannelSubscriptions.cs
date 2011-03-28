using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.AppData
{
  /// <summary>
  /// Contains channel subscription data
  /// </summary>
  [Serializable]
  [DataContract]
  public class ChannelSubscriptions
  {
    private HashSet<ChannelSubscription> _subscriptionSet;

    /// <summary>
    /// The HashSet that holds the user's channel subscriptions
    /// </summary>
    [DataMember]
    public HashSet<ChannelSubscription> SubscriptionSet
    {
      get { return _subscriptionSet; }
      set { _subscriptionSet = value; }
    }

    /// <summary>
    /// Instantiating an object will instantiate a new HashSet to hold the user's subscriptions
    /// </summary>
    public ChannelSubscriptions()
    {
      _subscriptionSet = new HashSet<ChannelSubscription>();
    }

    /// <summary>
    /// Searches the SubscriptionList HashSet for the channel weighting for the channel with that ID
    /// </summary>
    /// <param name="channelID">The unique ID of the channel to search the channel weighting</param>
    /// <returns>the channel weighting of the channel or 0 if no such ID is found</returns>
    public float GetChannelWeightingUnnormalisedByChannelID(long channelID)
    {
      foreach (ChannelSubscription cs in _subscriptionSet)
      {
        if (cs.ChannelID == channelID)
          return cs.ChannelWeightingUnnormalised;
      }

      return 0F;
    }

    /// <summary>
    /// Normalizes the weightings of the channel subscriptions to be used in log calculations
    /// </summary>
    public void NormalizeChannelWeightings()
    {
      float sum = 0F;

      foreach (ChannelSubscription cs in _subscriptionSet)
        sum += cs.ChannelWeightingUnnormalised;

      foreach (ChannelSubscription cs in _subscriptionSet)
        cs.ChannelWeightingNormalised = cs.ChannelWeightingUnnormalised / sum;
    }
  }
}
