using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using OxigenIIAdvertising.DataAggregators;
using System.Timers;
using InterCommunicationStructures;

namespace OxigenUserVersionInfoAggregator
{
  public partial class UserVersionInfoAggregator : ServiceBase
  {
    Timer _timer = null;

    SimpleAggregator _simpleAggregator = null;

    public UserVersionInfoAggregator()
    {
      InitializeComponent();

      eventLog.Source = "Oxigen User Version Info Aggregator";
      eventLog.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 7);
      eventLog.MaximumKilobytes = 1024;

      _timer = new Timer();
      _timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
    }

    void timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        int timerInterval = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["timerInterval"]) * 60 * 1000;
        _timer.Interval = timerInterval;

        _simpleAggregator.ExecuteAggregation();
      }
      catch (Exception ex)
      {
        eventLog.WriteEntry(ex.Message, EventLogEntryType.Error);
      }
    }

    protected override void OnStart(string[] args)
    {
      string nonAggregatedUserVersionInfoPath = System.Configuration.ConfigurationSettings.AppSettings["nonAggregatedUserVersionInfoPath"];
      string aggregatedserVersionInfoPath = System.Configuration.ConfigurationSettings.AppSettings["aggregatedUserVersionInfoPath"];
      int approxMaxNoRows = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["approxMaxNoRows"]);
      int timerInterval = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["timerInterval"]) * 60 * 1000;

      _simpleAggregator = new SimpleAggregator(nonAggregatedUserVersionInfoPath, aggregatedserVersionInfoPath, "user_version_info", approxMaxNoRows, typeof(ComponentInfoCollection));

      _timer.Interval = timerInterval;

      _timer.Start();

      eventLog.WriteEntry("Service has started");
    }

    protected override void OnStop()
    {
      _timer.Stop();
      _simpleAggregator = null;
      eventLog.WriteEntry("Service has stopped");
    }
  }
}
