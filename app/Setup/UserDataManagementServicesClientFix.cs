using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Setup.UserManagementServicesLive;

namespace Setup.UserManagementServicesLive
{
    public partial class BasicHttpBinding_IUserManagementServicesNonStreamer : System.Web.Services.Protocols.SoapHttpClientProtocol, IDisposable
    {

        void IDisposable.Dispose()
        {
            try
            {
                base.Dispose();
            }
            catch
            {
                Abort();
            }
        }

    }
}
namespace Setup
{
    public class UserDataManagementClient : BasicHttpBinding_IUserManagementServicesNonStreamer
    {
        public UserDataManagementClient() : base()
        {
            Proxy = WebProxy.GetDefaultProxy();
           
            if (Proxy != null)
                Proxy.Credentials = CredentialCache.DefaultCredentials;
        }
    }

}
