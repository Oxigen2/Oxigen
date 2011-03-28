using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using OxigenIIAdvertising.Services;
using OxigenIIAdvertising.ServiceContracts.BLServices;
using System.ServiceModel.Description;

namespace BLHost
{
  class Program
  {
    static void Main(string[] args)
    {
      Uri baseAddress = new Uri(System.Configuration.ConfigurationSettings.AppSettings["baseAddress"]);

      ServiceHost selfHost = new ServiceHost(typeof(BLService), baseAddress);

      NetNamedPipeBinding binding = new NetNamedPipeBinding();
      binding.TransactionFlow = true;

      try
      {
        selfHost.AddServiceEndpoint(typeof(IBLService), binding, baseAddress);

        selfHost.Open();

        Console.WriteLine("Service BLService is on.");
        Console.WriteLine("Press <ENTER> to terminate service.");
        Console.WriteLine();
        Console.ReadLine();
        Console.WriteLine("Service terminated.");

        selfHost.Close();
      }
      catch (CommunicationException ce)
      {
        Console.WriteLine(ce.ToString());
        selfHost.Abort();
      }
    }
  }
}
