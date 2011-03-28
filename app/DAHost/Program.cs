using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using OxigenIIAdvertising.Services;
using OxigenIIAdvertising.ServiceContracts.DAServices;

namespace DAHost
{
  class Program
  {
    static void Main(string[] args)
    {
      ServiceHost selfHost = new ServiceHost(typeof(DAService));

      try
      {
        selfHost.Open();

        Console.WriteLine("Service DAService is on.");
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
