using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.ServiceContracts.UserManagementServices;
using ProxyClientBaseLib;
using ServiceErrorReporting;

namespace OxigenSU
{
  public class UserManagementServicesNonStreamerSUClient : ProxyClientBase<IUserManagementServicesNonStreamer>, IUserManagementServicesNonStreamer
  {
    public void SendErrorReport(string macAddress, string exceptionDetails)
    {
      SendErrorReport(macAddress, exceptionDetails);
    }

    public StringErrorWrapper CreatePCIfNotExists(string userGUID, string macAddress, string machineName, int majorVersionNumber, int minorVersionNumber, string systemPassPhrase)
    {
      return CreatePCIfNotExists(userGUID, macAddress, machineName, majorVersionNumber, minorVersionNumber, systemPassPhrase);
    }
  }
}
