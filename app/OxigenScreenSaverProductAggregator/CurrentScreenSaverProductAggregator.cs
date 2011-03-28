using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using OxigenIIAdvertising.DataAggregators;
using InterCommunicationStructures;

namespace OxigenScreenSaverProductAggregator
{
  public partial class CurrentScreenSaverProductAggregator : ServiceBase
  {
    Timer _timer = null;

    Appender _simpleAggregator = null;

    public CurrentScreenSaverProductAggregator()
    {
      InitializeComponent();

      eventLog.Log = String.Empty;
      eventLog.Source = "Oxigen Current Screen Saver Product Aggregator";
      eventLog.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 7);
      eventLog.MaximumKilobytes = 1024;
    }

    protected override void OnStart(string[] args)
    {     
      string nonAggregatedCurrentScreenSaverProductInfoPath = System.Configuration.ConfigurationSettings.AppSettings["nonAggregatedCurrentScreenSaverProductInfoPath"];
      string aggregatedCurrentScreenSaverProductInfoPath = System.Configuration.ConfigurationSettings.AppSettings["aggregatedCurrentScreenSaverProductInfoPath"];
      int approxMaxNoRows = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["approxMaxNoRows"]);
      int timerInterval = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["timerInterval"]) * 60 * 1000;

      _simpleAggregator = new Appender(nonAggregatedCurrentScreenSaverProductInfoPath, 
        aggregatedCurrentScreenSaverProductInfoPath, "current_screensaver_info", approxMaxNoRows, eventLog);

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

        _simpleAggregator.Execute();
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
      _simpleAggregator = null;
      eventLog.WriteEntry("Service has stopped");
    }  
  }
}
