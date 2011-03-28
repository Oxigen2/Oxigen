using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using OxigenIIAdvertising.DataContracts.UserFileMarshaller;

namespace OxigenIIAdvertising.ServiceContracts.UserFileMarshaller
{
  [ServiceContract]
  public interface IUserFileMarshaller
  {
    [OperationContract]
    ComponentInfoCollectionErrorWrapper GetLatestComponentVersionNumber(string systemPassPhrase);
  }
}
