using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace InterCommunicationStructures
{
  /// <summary>
  /// The type of the asset
  /// </summary>
  [DataContract]
  public enum AssetType
  {
    /// <summary>
    /// Advertisement Asset
    /// </summary>
    [EnumMember]
    Advert,

    /// <summary>
    /// Non advertising-based Asset
    /// </summary>
    [EnumMember]
    Content
  }

  /// <summary>
  /// Types of aggregated logs to check
  /// </summary>
  [DataContract]
  public enum LogTypeAggregated
  {
    /// <summary>
    /// Number of clicks per date per asset
    /// </summary>
    [EnumMember]
    ClicksPerDatePerAsset,

    /// <summary>
    /// Number of impressions per date per asset
    /// </summary>
    [EnumMember]
    ImpressionsPerDatePerAsset,

    /// <summary>
    /// Number of weighted advert clicks per Channel
    /// </summary>
    [EnumMember]
    AdvertWeightedClicksPerChannel,

    /// <summary>
    /// Number of weighted operations per Channel
    /// </summary>
    [EnumMember]
    AdvertWeightedImpressionsPerChannel
  }

  /// <summary>
  /// Type of logs to check
  /// </summary>
  [DataContract]
  public enum LogType
  {
    /// <summary>
    /// Logs asset clicks
    /// </summary>
    [EnumMember]
    Clicks,

    /// <summary>
    /// Logs asset impressions
    /// </summary>
    [EnumMember]
    Impressions,

    /// <summary>
    /// Total clicks and impressions
    /// </summary>
    [EnumMember]
    Usage
  }

  /// <summary>
  /// The type of configuration
  /// </summary>
  [DataContract]
  public enum DataFileType
  {
    /// <summary>
    /// General application-wide settings
    /// </summary>
    [EnumMember]
    GeneralConfiguration,

    /// <summary>
    /// Conditions under which to display advertisements
    /// </summary>
    [EnumMember]
    AdvertConditions,

    /// <summary>
    /// Demographic data file for a specific user
    /// </summary>
    [EnumMember]
    DemographicData,

    /// <summary>
    /// Channel subscriptions for a specific user
    /// </summary>
    [EnumMember]
    ChannelSubscriptions,

    /// <summary>
    /// Channel and asset information for a specific channel
    /// </summary>
    [EnumMember]
    ChannelData
  }
}
