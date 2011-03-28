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
  public interface IUserDataMarshaller
  {
    [OperationContract]
    DateTimeErrorWrapper GetCurrentServerTime(string systemPassPhrase);

    [OperationContract]
    SimpleErrorWrapper RegisterSoftwareUninstall(string systemPassPhrase, string userGUID, string machineGUID);

    [OperationContract]
    SimpleErrorWrapper RegisterHeartBeat(string systemPassPhrase, string userGUID, string machineGUID);

    [OperationContract]
    SimpleErrorWrapper SetCurrentScreenSaverProduct(string systemPassPhrase, string userGUID, string machineGUID, string screenSaverName);
  }
}
