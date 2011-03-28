using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProxyClientBaseLib;
using OxigenIIAdvertising.ServiceContracts.UserManagementServices;
using ServiceErrorReporting;
using System.ServiceModel;

namespace InstallCustomSteps
{
  public class UserManagementServicesNonStreamerClient : ProxyClientBase<IUserManagementServicesNonStreamer>
  {
    public StringErrorWrapper AddSubscriptionsAndNewPC(string userGUID, string macAddress, string machineName,
      int majorVersionNumber, int minorVersionNumber, string[][] subscriptions, string systemPassPhrase)
    {
      return Channel.AddSubscriptionsAndNewPC(userGUID, macAddress, machineName,
      majorVersionNumber, minorVersionNumber, subscriptions, systemPassPhrase);
    }
  }
}
