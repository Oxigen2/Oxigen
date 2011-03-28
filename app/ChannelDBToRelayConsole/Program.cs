using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChannelDatabaseToRelayUploaderLib;
using System.Timers;

namespace ChannelDBToRelayConsole
{
  class Program
  {
    //static Timer _timer = null;

    static ChannelDatabaseToRelayUploader _channelDatabaseToRelayUploader = null;

    static void Main(string[] args)
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
  secondaryDomainName, maxNoServers, cryptPassword, slidePath, null);

      //_timer = new Timer();
      //_timer.AutoReset = false;
      //_timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

      //_timer.Interval = timerInterval;
      //_timer.Start();

      Console.WriteLine("Service has started");
      Console.WriteLine("Press <ENTER> to terminate.");
      Console.WriteLine();

      ProcessInput();
      
      Console.WriteLine("Service terminated.");
    }

    static void ProcessInput()
    {
      string s = Console.ReadLine();

      if (s.ToLower() == "a")
      {
        Execute();
        ProcessInput();
      }
    }

    static void Execute()
    {
      Console.WriteLine("Beginning transfer...");

      try
      {
        int timerInterval = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["timerInterval"]) * 60 * 1000;
        //_timer.Interval = timerInterval;

        _channelDatabaseToRelayUploader.Execute();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }

      //_timer.Start();

      Console.WriteLine("Transfer finished.");
    }
  }
}
