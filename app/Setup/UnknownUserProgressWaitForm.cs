using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using OxigenIIAdvertising.ServerConnectAttempt;
using Setup.UserManagementServicesLive;

namespace Setup
{
  public partial class UnknownUserProgressWaitForm : UnknownProgressWaitForm
  {
    string _userGUID;
    SimpleErrorWrapper _wrapper = null;
    private object _lockObj = new object();

    public UnknownUserProgressWaitForm(string message, string userGUID) : base(message)
    {
      _userGUID = userGUID;

      InitializeComponent();
    }

    void UnknownUserProgressWaitForm_Shown(object sender, System.EventArgs e)
    {
      
    }

    private void MatchUserGUIDWithDB()
    {
      BasicHttpBinding_IUserManagementServicesNonStreamer client = null;

      lock (_lockObj)
      {
        try
        {
          string url = SetupHelper.GetResponsiveServer(ServerType.MasterGetConfig, "masterConfig",
            "UserManagementServices.svc");

          if (string.IsNullOrEmpty(url))
          {
            _wrapper = SetupHelper.GetGenericErrorConnectingWrapper();
            return;
          }

          client = new UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer();

          client.Url = url;

          _wrapper = client.GetMatchedUserGUID(_userGUID, "password");
        }
        catch (System.Net.WebException)
        {
          _wrapper = SetupHelper.GetGenericErrorConnectingWrapper();
        }
        finally
        {
          if (client != null)
          {
            try
            {
              client.Dispose();
            }
            catch
            {
              client.Abort();
            }
          }
        }
      }
    }
  }
}
