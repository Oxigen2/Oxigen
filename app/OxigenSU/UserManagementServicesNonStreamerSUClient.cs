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
    public void SendErrorReport(string machineGUID, string exceptionDetails)
    {
      SendErrorReport(machineGUID, exceptionDetails);
    }
  }
}
