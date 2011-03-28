using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ServiceErrorReporting;

namespace OxigenIIAdvertising.ServiceContracts.UserManagementServices
{
  [ServiceContract(Namespace = "http://oxigen.net")]
  public interface IUserManagementServicesNonStreamer
  {
    [OperationContract]
    void SendErrorReport(string macAddress, string exceptionDetails);

    [OperationContract]
    StringErrorWrapper CreatePCIfNotExists(string userGUID, string macAddress, string machineName, int majorVersionNumber, int minorVersionNumber, string systemPassPhrase);
  }
}
