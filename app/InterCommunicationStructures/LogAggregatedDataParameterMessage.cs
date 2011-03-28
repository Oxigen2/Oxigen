using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace InterCommunicationStructures
{
  /// <summary>
  /// Web method parameter to hold the aggregated log file info to be transferred to the master servers from the relay servers
  /// </summary>
  [MessageContract]
  public class LogAggregatedDataParameterMessage : MasterParameterMessage
  {
    private LogTypeAggregated _logTypeAggregated;

    /// <summary>
    /// The log type to be seeked in the relay server and transferred to the master servers
    /// </summary>
    [MessageHeader]
    public LogTypeAggregated LogTypeAggregated
    {
      get { return _logTypeAggregated; }
      set { _logTypeAggregated = value; }
    }
  }
}
