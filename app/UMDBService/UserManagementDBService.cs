using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using OxigenIIMasterDataMarshallerClient;
using InterCommunicationStructures;
using System.IO;
using OxigenIIAdvertising.DAClients;

namespace UMDBService
{
  public partial class UserManagementDBService : ServiceBase
  {
    Timer _timer = null;

    public UserManagementDBService()
    {
      InitializeComponent();

      eventLog.Log = String.Empty;
      eventLog.Source = "Oxigen UMDB Service";
    }

    protected override void OnStart(string[] args)
    {
      int uninstallTimeout = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["uninstallTimeout"]) * 60 * 1000;

      _timer = new Timer();
      _timer.Interval = uninstallTimeout;
      _timer.AutoReset = false;
      _timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

      _timer.Start();

      eventLog.WriteEntry("Service has started");
    }

    void timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      // irrelevant
    }

    protected override void OnStop()
    {
      _timer.Stop();
      _timer.Dispose();
      eventLog.WriteEntry("Service has stopped");
    }
  }
}
