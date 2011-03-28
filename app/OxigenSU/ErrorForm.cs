using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace OxigenSU
{
  public partial class ErrorForm : Form
  {
    private string _exception;

    public ErrorForm(string exception)
    {
      InitializeComponent();
      _exception = exception;
    }

    private void btnNoSend_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void btnSend_Click(object sender, EventArgs e)
    {
      FormCollection openForms = Application.OpenForms;

      foreach (Form openForm in openForms)
        openForm.Hide();

      UserManagementServicesNonStreamerSUClient client = null;

      try
      {
        string macAddress = GetMACAddress();

        client = new UserManagementServicesNonStreamerSUClient();

        client.Endpoint.Address = new System.ServiceModel.EndpointAddress("https://master-getconfig-a-1.oxigen.net/UserManagementServices.svc");

        client.SendErrorReport(macAddress, _exception);
      }
      catch
      {
        // ignore       
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      Application.Exit();
    }

    internal static string GetMACAddress()
    {
      ManagementObjectSearcher query = null;
      ManagementObjectCollection queryCollection = null;

      try
      {
        query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled='TRUE'");

        queryCollection = query.Get();

        foreach (ManagementObject mo in queryCollection)
        {
          if (mo["MacAddress"] != null)
            return (mo["MacAddress"]).ToString();
        }
      }
      catch (Exception ex)
      {
        return ex.ToString();
      }

      return String.Empty;
    }

  }
}
