using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace OxigenIIAdvertising.SSGService
{
  [ServiceContract(Namespace = "http://oxigen.net")]
  public interface IOxigenService
  {
    [OperationContract]
    bool CanRunSU();
  }
}
