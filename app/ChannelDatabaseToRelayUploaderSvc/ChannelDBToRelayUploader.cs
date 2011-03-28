using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using ChannelDatabaseToRelayUploaderLib;

namespace ChannelDatabaseToRelayUploaderSvc
{
  public partial class ChannelDBToRelayUploader : ServiceBase
  {
    Timer _timer = null;

    ChannelDatabaseToRelayUploader _channelDatabaseToRelayUploader = null;

    public ChannelDBToRelayUploader()
    {
      InitializeComponent();

      eventLog.Log = String.Empty;
      eventLog.Source = "Oxigen Channel DB To Relay Uploader";
      eventLog.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 7);
      eventLog.MaximumKilobytes = 1024;
    }

    protected override void OnStart(string[] args)
    {
      int serverTimeout = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["serverTimeout"]);
      string systemPassPhrase = System.Configuration.ConfigurationSettings.AppSettings["systemPassPhrase"];
      string primaryDomainName = System.Configuration.ConfigurationSettings.AppSettings["primaryDomainName"];
      string secondaryDomainName = System.Configuration.ConfigurationSettings.AppSettings["secondaryDomainName"];
      int maxNoServers = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["maxNoServers"]);
      string cryptPassword = System.Configuration.ConfigurationSettings.AppSettings["cryptPassword"];
      string slidePath = System.Configuration.ConfigurationSettings.AppSettings["slidePath"];
      int timerInterval = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["timerInterval"]) * 60 * 1000;

      _channelDatabaseToRelayUploader = new ChannelDatabaseToRelayUploader(serverTimeout, systemPassPhrase, primaryDomainName,
        secondaryDomainName, maxNoServers, cryptPassword, slidePath, eventLog);

      _timer = new Timer();
      _timer.AutoReset = false;
      _timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

      _timer.Interval = timerInterval;
      _timer.Start();

      eventLog.WriteEntry("Service has started");
    }

    void timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        int timerInterval = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["timerInterval"]) * 60 * 1000;
        _timer.Interval = timerInterval;

        _channelDatabaseToRelayUploader.Execute();
      }
      catch (Exception ex)
      {
        eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
      }

      _timer.Start();
    }

    protected override void OnStop()
    {
      _timer.Stop();
      _timer.Dispose();
      _channelDatabaseToRelayUploader = null;
      eventLog.WriteEntry("Service has stopped");
    }
  }
}
