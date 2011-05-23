using System;
using System.Configuration;
using System.Diagnostics;
using System.ServiceProcess;
using System.ServiceModel;
using OxigenIIAdvertising.Services;
using OxigenIIAdvertising.ServiceContracts.BLServices;

namespace BLHostService
{
  public partial class HostService : ServiceBase
  {
    public HostService()
    {
      InitializeComponent();
      this.ServiceName = ConfigurationManager.AppSettings["serviceName"];
      eventLog.Log = String.Empty;
      eventLog.Source = "Oxigen Business Logic Host";
      eventLog.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 7);
      eventLog.MaximumKilobytes = 1024;
    }

    ServiceHost _selfHost;

    protected override void OnStart(string[] args)
    {
      Uri baseAddress = new Uri(System.Configuration.ConfigurationSettings.AppSettings["baseAddress"]);

      _selfHost = new ServiceHost(typeof(BLService), baseAddress);

      NetNamedPipeBinding binding = new NetNamedPipeBinding();
      binding.TransactionFlow = true;
      
      try
      {
        _selfHost.AddServiceEndpoint(typeof(IBLService), binding, baseAddress);

        _selfHost.Open();

        eventLog.WriteEntry("Service has been started", EventLogEntryType.Information);
      }
      catch (Exception ex)
      {
        _selfHost.Abort();
        eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);

        this.Stop();
      }
    }

    protected override void OnStop()
    {
      try
      {
        _selfHost.Close();
      }
      catch (Exception ex)
      {
        _selfHost.Abort();
        eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
      }

      eventLog.WriteEntry("Service has been stopped", EventLogEntryType.Information);
    }
  }
}
