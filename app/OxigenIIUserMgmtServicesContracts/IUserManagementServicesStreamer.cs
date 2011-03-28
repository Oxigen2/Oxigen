using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ServiceErrorReporting;
using InterCommunicationStructures;

namespace OxigenIIAdvertising.ServiceContracts.UserManagementServices
{
  [ServiceContract(Namespace = "http://oxigen.net")]
  public interface IUserManagementServicesStreamer
  {
    [OperationContract]
    StreamErrorWrapper GetAppDataFiles(AppDataFileParameterMessage appDataFileParameterMessage);
  }
}
