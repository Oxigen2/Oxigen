using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;
using InterCommunicationStructures;
using log4net;
using OxigenIIAdvertising.DAClients;
using OxigenIIMasterDataMarshallerClient;

namespace UMDBService
{
  public partial class UserManagementDBService : ServiceBase
  {
    private Timer _softwareVersionInfoTimer = null;
    private readonly ILog _logger = LogManager.GetLogger(typeof (UserManagementDBService));

    public UserManagementDBService()
    {
      InitializeComponent();

      eventLog.Log = String.Empty;
      eventLog.Source = "Oxigen UMDB Service";
      log4net.Config.XmlConfigurator.Configure();
    }

    protected override void OnStart(string[] args)
    {
      _softwareVersionInfoTimer = new Timer();
      _softwareVersionInfoTimer.Interval = 10000; // ten seconds
      _softwareVersionInfoTimer.AutoReset = false;
      _softwareVersionInfoTimer.Elapsed += new ElapsedEventHandler(softwareVersionInfoTimer_Elapsed);
      _softwareVersionInfoTimer.AutoReset = false;

      _softwareVersionInfoTimer.Start();

      eventLog.WriteEntry("Service has started");
    }

    void softwareVersionInfoTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      int newTimerValue;
      int softwareVersionInfoFileLimit;
      int operationTimeout;

      try
      {
        // re-read timer interval
        if (!int.TryParse(System.Configuration.ConfigurationSettings.AppSettings["softwareVersionInfoTimeout"], out newTimerValue))
        {
          eventLog.WriteEntry("Could not retrieve new timer value for softwareVersionInfoTimer.", EventLogEntryType.Error);
          return;
        }

        // re-read file limit
        if (!int.TryParse(System.Configuration.ConfigurationSettings.AppSettings["softwareVersionInfoFileLimit"], out softwareVersionInfoFileLimit))
        {
          eventLog.WriteEntry("Could not retrieve new timer value for softwareVersionInfoFileLimit.", EventLogEntryType.Error);
          return;
        }

        if (!int.TryParse(System.Configuration.ConfigurationSettings.AppSettings["operationTimeout"], out operationTimeout))
        {
          eventLog.WriteEntry("Could not retrieve new timer value for softwareVersionInfoTimer.", EventLogEntryType.Error);
          return;
        }

        _softwareVersionInfoTimer.Interval = newTimerValue * 60 * 1000;

        eventLog.WriteEntry("softwareVersionInfoTimer interval set to " + newTimerValue * 60 * 1000 + " ms.", EventLogEntryType.Information);

        using (var mdmClient = new MasterDataMarshallerClient())
        {
          mdmClient.InnerChannel.OperationTimeout = TimeSpan.FromMinutes(operationTimeout);

          // this may take some time depending on the number of rows
          MachineVersionInfo[] mi = mdmClient.GetMachineVersionInfo(softwareVersionInfoFileLimit);

          _logger.Debug("Number of Machine Version Info Data retrieved: " + mi.Length);

          eventLog.WriteEntry("Number of Machine Version Info Data retrieved: " + mi.Length, EventLogEntryType.Information);

          HashSet<string> machineGUIDSFailedToUpdate = null;

          _logger.Debug("softwareVersionInfoTimer_Elapsed() Opening DA Client.");
          using (var daClient = new DAClient())
          {
            daClient.InnerChannel.OperationTimeout = TimeSpan.FromMinutes(operationTimeout);
            _logger.Debug("softwareVersionInfoTimer_Elapsed() DA Client opened.");
            machineGUIDSFailedToUpdate = daClient.UpdateSoftwareVersionInfo(mi);
            _logger.Debug("rows inserted.");
          }

          eventLog.WriteEntry(machineGUIDSFailedToUpdate.Count + " files failed to update.", EventLogEntryType.Information);

          _logger.Debug("Calling DeleteSoftwareInfoFiles()...");
          mdmClient.DeleteSoftwareInfoFiles(machineGUIDSFailedToUpdate);
          _logger.Debug("DeleteSoftwareInfoFiles() returned.");
        }
      }
      catch (Exception ex)
      {
        eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
      }
      finally
      {
        _softwareVersionInfoTimer.Start();
      }
    }

    protected override void OnStop()
    {
      _softwareVersionInfoTimer.Stop();
      _softwareVersionInfoTimer.Dispose();
      eventLog.WriteEntry("Service has stopped");
    }
  }
}
