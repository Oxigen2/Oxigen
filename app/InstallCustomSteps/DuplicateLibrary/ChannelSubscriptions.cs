using System;
using System.Collections.Generic;
using System.Text;

namespace InstallCustomSteps.DuplicateLibrary
{
  /// <summary>
  /// Cut-down version of OxigenIIAdvertising.AppData.ChannelSubscriptions
  /// for use on local machine during Setup
  /// </summary>
  public class ChannelSubscriptions
  {
    private ChannelSubscription[] _subscriptionSet;

    /// <summary>
    /// The HashSet that holds the user's channel subscriptions
    /// </summary>
    public ChannelSubscription[] SubscriptionSet
    {
      get { return _subscriptionSet; }
      set { _subscriptionSet = value; }
    }
  }
}
