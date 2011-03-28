using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.ServiceContracts.UserDataMarshaller;
using OxigenIIDownloadServers;
using InterCommunicationStructures;
using ServiceErrorReporting;
using ProxyClientBaseLib;

namespace OxigenSU
{
  public class UserFileMarshallerSUClient : ProxyClientBase<IUserFileMarshaller>, IUserFileMarshaller
  {
    public StreamErrorWrapper GetComponent(ComponentParameterMessage message)
    {
      return Channel.GetComponent(message);
    }
  }
}
