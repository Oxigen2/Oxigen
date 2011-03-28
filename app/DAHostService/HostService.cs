using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using OxigenIIAdvertising.Services;
using OxigenIIAdvertising.ServiceContracts.DAServices;

namespace DAHostService
{
  public partial class HostService : ServiceBase
  {
    public HostService()
    {
      InitializeComponent();

      eventLog.Log = String.Empty;
      eventLog.Source = "Oxigen Data Access Host";
      eventLog.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 7);
      eventLog.MaximumKilobytes = 1024;
    }

    ServiceHost _selfHost;

    protected override void OnStart(string[] args)
    {
      _selfHost = new ServiceHost(typeof(DAService));

      try
      {
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
