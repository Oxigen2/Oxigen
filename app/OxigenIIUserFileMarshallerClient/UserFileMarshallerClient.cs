using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using OxigenIIAdvertising.ServiceContracts.UserFileMarshaller;

namespace OxigenIIUserFileMarshallerClient
{
  public class UserFileMarshallerClient : ClientBase<IUserFileMarshaller>, IUserFileMarshaller
  {
    public OxigenIIAdvertising.DataContracts.UserFileMarshaller.ComponentInfoCollectionErrorWrapper GetLatestComponentVersionNumber(string systemPassPhrase)
    {
      return Channel.GetLatestComponentVersionNumber(systemPassPhrase);
    }
  }
}
