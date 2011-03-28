using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ServiceErrorReporting;
using InterCommunicationStructures;

namespace OxigenIIDownloadServers
{
  [ServiceContract(Namespace = "http://oxigen.net")]
  public interface IUserFileMarshaller
  {
    [OperationContract]
    StreamErrorWrapper GetComponent(ComponentParameterMessage message);
  }
}