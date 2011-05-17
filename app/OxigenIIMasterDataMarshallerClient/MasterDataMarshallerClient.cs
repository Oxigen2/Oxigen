using System.Collections.Generic;
using InterCommunicationStructures;
using OxigenIIAdvertising.ServiceContracts.MasterDataMarshaller;
using ProxyClientBaseLib;

namespace OxigenIIMasterDataMarshallerClient
{
  public class MasterDataMarshallerClient : ProxyClientBase<IMasterDataMarshaller>, IMasterDataMarshaller
  {
    public MachineVersionInfo[] GetMachineVersionInfo(int fileLimit)
    {
      return Channel.GetMachineVersionInfo(fileLimit);
    }

    public void DeleteSoftwareInfoFiles(HashSet<string> machineGUIDSFailedToUpdate)
    {
      Channel.DeleteSoftwareInfoFiles(machineGUIDSFailedToUpdate);
    }
  }
}
