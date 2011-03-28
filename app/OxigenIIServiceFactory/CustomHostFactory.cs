using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Activation;
using System.ServiceModel;

namespace OxigenIIServiceFactory
{
  // This class and CustomHost are helpers for debugging.
  // They remove the base addresses that IIS is providing.
  public class CustomHostFactory : ServiceHostFactory
  {
    protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
    {
      ServiceHost customServiceHost = new ServiceHost(serviceType, baseAddresses[0]);
      return customServiceHost;
    }
  }
}
