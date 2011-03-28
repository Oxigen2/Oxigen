﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ServiceErrorReporting;
using InterCommunicationStructures;
using System.IO;

namespace OxigenIIAdvertising.ServiceContracts.MasterDataMarshaller
{
  [ServiceContract(Namespace = "http://oxigen.net")]
  public interface IMasterDataMarshallerStreamer
  {
    [OperationContract]
    void SetAppDataFiles(AppDataFileStreamParameterMessage appDataFileStreamParameterMessage);

    [OperationContract]
    void SetAssetFile(AssetFileStreamParameterMessage appDataFileStreamParameterMessage);

    [OperationContract]
    StreamMessage GetLogData(LogAggregatedDataParameterMessage logAggregatedDataParameterMessage);

    [OperationContract]
    StreamMessage GetCurrentScreenSaverProducts(MasterParameterMessage authentication);

    [OperationContract]
    Stream GetSoftwareUninstalls(string systemPassPhrase);

    [OperationContract]
    StreamMessage GetUserHeartbeats(MasterParameterMessage authentication);
  }
}
