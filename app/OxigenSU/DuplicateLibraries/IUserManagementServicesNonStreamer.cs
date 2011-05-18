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
    void SendErrorReport(string machineGUID, string exceptionDetails);
  }
}
