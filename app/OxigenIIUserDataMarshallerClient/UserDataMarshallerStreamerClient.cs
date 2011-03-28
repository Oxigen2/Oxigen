using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.ServiceContracts.UserDataMarshaller;
using System.ServiceModel;
using ServiceErrorReporting;
using InterCommunicationStructures;
using ProxyClientBaseLib;

namespace OxigenIIAdvertising.UserDataMarshallerServiceClient
{
  public class UserDataMarshallerStreamerClient : ProxyClientBase<IUserDataMarshallerStreamer>, IUserDataMarshallerStreamer
  {
    public StreamErrorWrapper GetAppDataFiles(AppDataFileParameterMessage appDataFileParameterMessage)
    {
      return Channel.GetAppDataFiles(appDataFileParameterMessage);
    }

    public StreamErrorWrapper GetAssetFile(AssetFileParameterMessage assetFileParameterMessage)
    {
      return Channel.GetAssetFile(assetFileParameterMessage);
    }

    public SimpleErrorWrapperMessage ProcessLogData(LogDataParameterMessage logDataParameterMessage)
    {
      return Channel.ProcessLogData(logDataParameterMessage);
    }
  }
}
