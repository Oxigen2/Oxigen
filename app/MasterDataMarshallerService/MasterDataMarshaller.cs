using System;
using System.Configuration;
using System.Diagnostics;
using System.ServiceProcess;
using System.ServiceModel;

namespace MasterDataMarshallerService
{
  public partial class MasterDataMarshaller : ServiceBase
  {
    public MasterDataMarshaller()
    {
      InitializeComponent();
      this.ServiceName = ConfigurationManager.AppSettings["serviceName"];
      eventLog.Log = String.Empty;
      eventLog.Source = "Oxigen Master Data Marshaller";
      eventLog.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 7);
      eventLog.MaximumKilobytes = 1024;
    }

    ServiceHost _selfHost;

    protected override void OnStart(string[] args)
    {
      _selfHost = new ServiceHost(typeof(OxigenIIAdvertising.RelayServers.MasterDataMarshaller));
     
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
