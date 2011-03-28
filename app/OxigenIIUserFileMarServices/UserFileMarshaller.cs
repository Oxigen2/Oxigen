using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.ServiceContracts.UserFileMarshaller;
using OxigenIIAdvertising.DataContracts.UserFileMarshaller;
using ServiceErrorReporting;

namespace OxigenIIAdvertising.RelayServers
{
  public class UserFileMarshaller : IUserFileMarshaller
  {
    /// <summary>
    /// Gets a list of all updatable components with their latest version information
    /// </summary>
    /// <param name="systemPassPhrase">Pass phrase to authenticate across services</param>
    /// <returns>a ComponentInfoCollectionErrorWrapper with the list of all updatable components and their latest version information or with error information</returns>
    public ComponentInfoCollectionErrorWrapper GetLatestComponentVersionNumber(string systemPassPhrase)
    {
      ComponentInfoCollectionErrorWrapper componentInfoCollectionErrorWrapper = new ComponentInfoCollectionErrorWrapper();

      if (systemPassPhrase != "password")
      {
        componentInfoCollectionErrorWrapper.ErrorCode = "ERR:001";
        componentInfoCollectionErrorWrapper.Message = "Authentication failure";
        componentInfoCollectionErrorWrapper.ErrorSeverity = ErrorSeverity.Retriable;
        componentInfoCollectionErrorWrapper.ErrorStatus = ErrorStatus.Failure;

        return componentInfoCollectionErrorWrapper;
      }

      ComponentInfo componentInfo = new ComponentInfo();

      componentInfo.ComponentVersion = "1.05";
      componentInfo.ComponentURL = "http://www.tempuri.org";

      componentInfo.OtherURLs = new HashSet<string>();
      componentInfo.OtherURLs.Add("http://www.google.com");

      HashSet<ComponentInfo> componentCollection = new HashSet<ComponentInfo>();

      componentCollection.Add(componentInfo);

      componentInfoCollectionErrorWrapper.ErrorStatus = ErrorStatus.Success;
      componentInfoCollectionErrorWrapper.ReturnComponentInfoCollection = componentCollection;

      return componentInfoCollectionErrorWrapper;
    }
  }
}
