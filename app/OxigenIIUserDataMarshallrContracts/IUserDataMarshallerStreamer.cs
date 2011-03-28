using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ServiceErrorReporting;
using InterCommunicationStructures;

namespace OxigenIIAdvertising.ServiceContracts.UserDataMarshaller
{
  [ServiceContract(Namespace = "http://oxigen.net")]
  public interface IUserDataMarshallerStreamer
  {
    [OperationContract]
    StreamErrorWrapper GetAppDataFiles(AppDataFileParameterMessage appDataFileParameterMessage);

    [OperationContract]
    StreamErrorWrapper GetAssetFile(AssetFileParameterMessage assetFileParameterMessage);

    [OperationContract]
    SimpleErrorWrapperMessage ProcessLogData(LogDataParameterMessage logDataParameterMessage);
  }
}
