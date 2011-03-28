using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProxyClientBaseLib;
using OxigenIIAdvertising.ServiceContracts.UserManagementServices;
using ServiceErrorReporting;

namespace OxigenIIAdvertising.UserManagementServicesServiceClient
{
  public class UserManagementServicesNonStreamerClient : ProxyClientBase<IUserManagementServicesNonStreamer>
  {
    public StringErrorWrapper CreatePCIfNotExists(string userGUID, string macAddress, 
      string machineName, int majorVersionNumber, 
      int minorVersionNumber, string systemPassPhrase)
    {
      return Channel.CreatePCIfNotExists(userGUID, macAddress, 
        machineName, majorVersionNumber, minorVersionNumber, 
        systemPassPhrase);
    }

    public StringErrorWrapper AddSubscriptionsAndNewPC(string userGUID, string macAddress, string machineName,
  int majorVersionNumber, int minorVersionNumber, string[][] subscriptions, string systemPassPhrase)
    {
      return Channel.AddSubscriptionsAndNewPC(userGUID, macAddress, machineName,
      majorVersionNumber, minorVersionNumber, subscriptions, systemPassPhrase);
    }
  }
}
