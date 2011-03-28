using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.ServiceContracts.UserDataMarshaller;
using ServiceErrorReporting;
using ProxyClientBaseLib;

namespace OxigenSU
{
  public class UserDataMarshallerSUClient : ProxyClientBase<IUserDataMarshallerSU>, IUserDataMarshallerSU
  {
    public SimpleErrorWrapper SetCurrentVersionInfo(string userGUID, string machineGUID, string version, string systemPassPhrase)
    {
      return Channel.SetCurrentVersionInfo(userGUID, machineGUID, version, systemPassPhrase);
    }
  }
}
