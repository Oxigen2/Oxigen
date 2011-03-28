using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace SSGContracts
{
  [ServiceContract(Namespace = "http://oxigen.net")]
  public interface IScreensaverGuardian
  {
    [OperationContract]
    void OpenBrowser(string url);
  }
}
