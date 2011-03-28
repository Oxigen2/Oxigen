using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.ServiceContracts.MasterDataMarshaller;
using System.ServiceModel;
using ServiceErrorReporting;
using InterCommunicationStructures;
using ProxyClientBaseLib;
using System.IO;

namespace OxigenIIMasterDataMarshallerClient
{
  public class MasterDataMarshallerStreamerClient : ProxyClientBase<IMasterDataMarshallerStreamer>, 
    IMasterDataMarshallerStreamer
  {
    public void SetAppDataFiles(AppDataFileStreamParameterMessage appDataFileStreamParameterMessage)
    {
      Channel.SetAppDataFiles(appDataFileStreamParameterMessage);
    }

    public void SetAssetFile(AssetFileStreamParameterMessage assetFileParameterMessage)
    {
      Channel.SetAssetFile(assetFileParameterMessage);
    }

    public StreamMessage GetLogData(LogAggregatedDataParameterMessage logAggregatedDataParameterMessage)
    {
      return Channel.GetLogData(logAggregatedDataParameterMessage);
    }

    public StreamMessage GetCurrentScreenSaverProducts(MasterParameterMessage authentication)
    {
      return Channel.GetCurrentScreenSaverProducts(authentication);
    }

    public Stream GetSoftwareUninstalls(string systemPassPhrase)
    {
      return Channel.GetSoftwareUninstalls(systemPassPhrase);
    }

    public StreamMessage GetUserHeartbeats(MasterParameterMessage authentication)
    {
      return Channel.GetUserHeartbeats(authentication);
    }
  }
}
