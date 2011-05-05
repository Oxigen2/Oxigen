using ProxyClientBaseLib;
using OxigenIIAdvertising.ServiceContracts.UserManagementServices;
using ServiceErrorReporting;

namespace OxigenIIAdvertising.UserManagementServicesServiceClient
{
  public class UserManagementServicesNonStreamerClient : ProxyClientBase<IUserManagementServicesNonStreamer>
  {
      // needed for software updater (version < 1.33)
    public StringErrorWrapper CreatePCIfNotExists(string userGUID, string macAddress, 
      string machineName, int majorVersionNumber, 
      int minorVersionNumber, string systemPassPhrase)
    {
      return Channel.CreatePCIfNotExists(userGUID, macAddress, 
        machineName, majorVersionNumber, minorVersionNumber, 
        systemPassPhrase);
    }
  }
}
