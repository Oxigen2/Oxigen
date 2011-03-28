using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using OxigenIIAdvertising.ServiceContracts.UserManagementServices;
using ServiceErrorReporting;
using InterCommunicationStructures;
using ProxyClientBaseLib;

namespace OxigenIIAdvertising.UserManagementServicesServiceClient
{
  public class UserManagementServicesClient : ProxyClientBase<IUserManagementServicesStreamer>, IUserManagementServicesStreamer
  {
    public StreamErrorWrapper GetAppDataFiles(AppDataFileParameterMessage appDataFileParameterMessage)
    {
      return Channel.GetAppDataFiles(appDataFileParameterMessage);
    }
  }
}
