using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSGContracts;
using ProxyClientBaseLib;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace OxigenIIAdvertising.ScreenSaver
{
  public class ScreensaverGuardianClient : ProxyClientBase<IScreensaverGuardian>, IScreensaverGuardian
  {
    public ScreensaverGuardianClient(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress) { }

    public void OpenBrowser(string url)
    {
      Channel.OpenBrowser(url);
    }
  }
}
