using System.Collections.Generic;
using System.ServiceModel;
using InterCommunicationStructures;

namespace OxigenIIAdvertising.ServiceContracts.MasterDataMarshaller
{
  [ServiceContract(Namespace = "http://oxigen.net", SessionMode = SessionMode.Required)]
  public interface IMasterDataMarshaller
  {
    [OperationContract(IsInitiating = true, IsTerminating = false)]
    MachineVersionInfo[] GetMachineVersionInfo(int fileLimit);

    [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = true)]
    void DeleteSoftwareInfoFiles(HashSet<string> machineGUIDSFailedToUpdate);
  }
}
