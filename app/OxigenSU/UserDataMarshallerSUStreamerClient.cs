using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ServiceErrorReporting;
using OxigenIIAdvertising.ServiceContracts.UserDataMarshaller;
using InterCommunicationStructures;
using ProxyClientBaseLib;

namespace OxigenSU
{
  public class UserDataMarshallerSUStreamerClient : ProxyClientBase<IUserDataMarshallerSUStreamer>, IUserDataMarshallerSUStreamer
  {
    public StreamErrorWrapper GetComponentList(VersionParameterMessage message)
    {
      return Channel.GetComponentList(message);
    }
  }
}
