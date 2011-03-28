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
    StringErrorWrapper AddSubscriptionsAndNewPC(string userGUID, string macAddress, string machineName,
      int majorVersionNumber, int minorVersionNumber, string[][] subscriptions, string systemPassPhrase);
  }
}
