using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxigenSU
{
  /// <summary>
  /// Enumeration of types of server to ping
  /// </summary>
  public enum ServerType
  {
    /// <summary>
    /// Relay server - send log (content)
    /// </summary>
    RelayLogCont,

    /// <summary>
    /// Relay server - send log (advert)
    /// </summary>
    RelayLogAdv,

    /// <summary>
    /// Relay server - logs (general)
    /// </summary>
    RelayLogs,

    /// <summary>
    /// Relay server - channel assets
    /// </summary>
    RelayChannelAssets,

    /// <summary>
    /// Relay server - channel data
    /// </summary>
    RelayChannelData,

    /// <summary>
    /// Relay server - get config
    /// </summary>
    RelayGetConfig,

    /// <summary>
    /// Master server - get config
    /// </summary>
    MasterGetConfig,

    /// <summary>
    /// Download servers - get file
    /// </summary>
    DownloadGetFile
  }
}
