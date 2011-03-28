using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceErrorReporting;
using System.ServiceModel;

namespace OxigenIIAdvertising.ServiceContracts.UserDataMarshaller
{
  [ServiceContract(Namespace = "http://oxigen.net")]
  public interface IUserDataMarshallerSU
  {
    [OperationContract]
    SimpleErrorWrapper SetCurrentVersionInfo(string userGUID, string machineGUID, string version, string systemPassPhrase);
  }
}
