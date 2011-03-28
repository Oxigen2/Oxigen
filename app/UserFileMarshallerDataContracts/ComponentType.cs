using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.DataContracts.UserFileMarshaller
{
  /// <summary>
  /// The software component to check
  /// </summary>
  [DataContract]
  public enum ComponentType
  {
    [EnumMember]
    Asset,

    [EnumMember]
    AssetLevel,

    [EnumMember]
    AssetType,

    [EnumMember]
    AdvertCondition,

    [EnumMember]
    FileChecksumCalculator,

    [EnumMember]
    Channel,

    [EnumMember]
    ContentExchanger,

    [EnumMember]
    FilePathAccessor,

    [EnumMember]
    FileCryprography,

    [EnumMember]
    FileIO,

    [EnumMember]
    FlashObject,

    [EnumMember]
    FlashPlayer,

    [EnumMember]
    GeneralData,

    [EnumMember]
    Image,

    [EnumMember]
    ImagePlayer,

    [EnumMember]
    LogExchanger,

    [EnumMember]
    Playlist,

    [EnumMember]
    Plugin,

    [EnumMember]
    ScreenSaver,

    [EnumMember]
    ScreenSaverGuardian,

    [EnumMember]
    SoftwareInstaller,

    [EnumMember]
    SoftwareUpdater,

    [EnumMember]
    AssetScheduling,

    [EnumMember]
    DemographicRange,

    [EnumMember]
    UserInfo,

    [EnumMember]
    Video,

    [EnumMember]
    VideoPlayer,

    [EnumMember]
    WebPage,

    [EnumMember]
    WebPagePlayer,

    [EnumMember]
    ServerCycleConnectAttempt,

    [EnumMember]
    ServerCycleConnectAttemptDownload,

    [EnumMember]
    ServerCycleConnectAttemptRelay,

    [EnumMember]
    ServerCycleConnectAttemptUserMgmt,

    [EnumMember]
    Errors
  }
}
