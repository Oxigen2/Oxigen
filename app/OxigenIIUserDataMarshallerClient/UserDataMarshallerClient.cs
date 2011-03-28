using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using OxigenIIAdvertising.ServiceContracts.UserDataMarshaller;
using ServiceErrorReporting;
using ProxyClientBaseLib;
using InterCommunicationStructures;

namespace OxigenIIAdvertising.UserDataMarshallerServiceClient
{
  public class UserDataMarshallerClient : ProxyClientBase<IUserDataMarshaller>, IUserDataMarshaller
  {
    public DateTimeErrorWrapper GetCurrentServerTime(string systemPassPhrase)
    {
      return Channel.GetCurrentServerTime(systemPassPhrase);
    }

    public SimpleErrorWrapper RegisterHeartBeat(string systemPassPhrase, string userGUID, string machineGUID)
    {
      return Channel.RegisterHeartBeat(systemPassPhrase, userGUID, machineGUID);
    }

    public SimpleErrorWrapper RegisterSoftwareUninstall(string systemPassPhrase, string userGUID, string machineGUID)
    {
      return Channel.RegisterSoftwareUninstall(systemPassPhrase, userGUID, machineGUID);
    }

    public SimpleErrorWrapper SetCurrentScreenSaverProduct(string systemPassPhrase, string userGUID, string machineGUID, string screenSaverName)
    {
      return Channel.SetCurrentScreenSaverProduct(systemPassPhrase, userGUID, machineGUID, screenSaverName);
    }
  }
}
