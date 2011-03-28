using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace OxigenLogAggregator
{
  public partial class LogAggregator : ServiceBase
  {
    Timer _timer = null;

    OxigenIIAdvertising.DataAggregators.LogAggregator _logAggregator = null;

    public LogAggregator()
    {
      InitializeComponent();

      eventLog.Log = String.Empty;
      eventLog.Source = "Oxigen Log Aggregator";
      eventLog.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 7);
      eventLog.MaximumKilobytes = 1024;
    }

    protected override void OnStart(string[] args)
    {
      string clientSideAggregatedLogsPath = System.Configuration.ConfigurationSettings.AppSettings["clientSideAggregatedLogsPath"];
      string serverSideAggregatedLogsPath = System.Configuration.ConfigurationSettings.AppSettings["serverSideAggregatedLogsPath"];
      int maxNoClientSideFiles = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["maxNoClientSideFiles"]);
      int timerInterval = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["timerInterval"]) * 60 * 1000;

      _logAggregator = new OxigenIIAdvertising.DataAggregators.LogAggregator(clientSideAggregatedLogsPath,
        serverSideAggregatedLogsPath, maxNoClientSideFiles, eventLog);

      _timer = new Timer();
      _timer.Interval = timerInterval;
      _timer.AutoReset = false;
      _timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

      _timer.Start();

      eventLog.WriteEntry("Service has started");
    }

    void timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        int timerInterval = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["timerInterval"]) * 60 * 1000;
        _timer.Interval = timerInterval;

        _logAggregator.Execute();
      }
      catch (Exception ex)
      {
        eventLog.WriteEntry(ex.Message, EventLogEntryType.Error);
      }

      _timer.Start();
    }

    protected override void OnStop()
    {
      _timer.Stop();
      _timer.Dispose();
      _logAggregator = null;
      eventLog.WriteEntry("Service has stopped");
    }
  }
}
