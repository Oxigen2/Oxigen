using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using OxigenIIUserMgmtServicesService;
using OxigenIIAdvertising.SOAStructures;
using OxigenIIAdvertising.AppData;
using ServiceErrorReporting;

namespace OxigenIIAdvertising.ServiceContracts.UserManagementServices
{
  [ServiceContract(Namespace = "http://oxigen.net")]
  public interface IUserManagementServicesNonStreamer
  {
    [OperationContract]
    SimpleErrorWrapper GetUserExistsByUserCredentials(string emailAddress, string password, out string userGUID, string systemPassPhrase);

    [OperationContract]
    SimpleErrorWrapper RegisterNewUser(string emailAddress, string password, string firstName, string lastName, string gender, DateTime dob,
      string townCity, string state, string country, string occupationSector, string employmentLevel,
      string annualHouseholdIncome, string userGUID, string machineGUID, int softwareMajorVersionNumber, 
      int softwareMinorVersionNumber, string macAddress, string machineName, AppData.ChannelSubscriptions channelSubscriptions,
      string systemPassPhrase);

    [OperationContract]
    SimpleErrorWrapper GetExistingUserDetails(string userGUID, string password, out UserInfo userInfo, string systemPassPhrase); 

    [OperationContract]
    SimpleErrorWrapper UpdateUserAccount(string emailAddress, string password, string firstName, 
      string lastName, string gender, DateTime dob,
      string townCity, string state, string country, string occupationSector, string employmentLevel,
      string annualHouseholdIncome,
      AppData.ChannelSubscriptions channelSubscriptions,
      int softwareMajorVersionNumber,
      int softwareMinorVersionNumber,
      string machineGUID,
      string newPcName,
      string macAddress, 
      string systemPassPhrase);

    [OperationContract]
    SimpleErrorWrapper GetPcListForInstallerEmail(string emailAddress, out List<PcInfo> pcs, string systemPassPhrase);

    [OperationContract]
    SimpleErrorWrapper GetPcListForInstallerGUID(string userGUID, out List<PcInfo> pcs, string systemPassPhrase);

    [OperationContract]
    SimpleErrorWrapper LinkPCAndSubscriptionsExistingPC(string userGUID, int pcID, string machineGUID, string macAddress,
      int softwareMajorVersionNumber, int softwareMinorVersionNumber, ChannelSubscriptions channelSubscriptions, string systemPassPhrase);

    [OperationContract]
    SimpleErrorWrapper CheckEmailAddressExists(string emailAddress, string systemPassPhrase);

    [OperationContract]
    SimpleErrorWrapper EditSubscriptions(string userGUID, string machineGUID, ChannelSubscriptions channelSubscriptions,
      string systemPassPhrase);

    [OperationContract]
    SimpleErrorWrapper GetPCSubscriptionsByPCID(string userGUID, int pcID, out ChannelSubscriptions channelSubscriptions, 
      string systemPassPhrase);

    [OperationContract]
    SimpleErrorWrapper GetPCSubscriptionsByMachineGUID(string userGUID, string machineGUID, out ChannelSubscriptions channelSubscriptions,
      string systemPassPhrase);

    [OperationContract]
    SimpleErrorWrapper GetMatchedUserGUID(string userGUID, string systemPassPhrase);

    [OperationContract]
    SimpleErrorWrapper GetMatchedMachineGUID(string userGUID, string machineGUID, string systemPassPhrase);

    [OperationContract]
    SimpleErrorWrapper SendEmailReminder(string emailAddress, string systemPassPhrase);

    [OperationContract]
    SimpleErrorWrapper RegisterSoftwareUninstall(string userGUID, string machineGUID, string password);

    [OperationContract]
    void SendErrorReport(string macAddress, string exceptionDetails);

    [OperationContract]
    StringErrorWrapper CreatePCIfNotExists(string userGUID, string macAddress, string machineName,
      int majorVersionNumber, int minorVersionNumber, string systemPassPhrase);

    [OperationContract]
    SimpleErrorWrapper SyncWithServerNoPersonalDetails(string userGUID,
      string machineGUID,
      AppData.ChannelSubscriptions channelSubscriptions,
      int softwareMajorVersionNumber,
      int softwareMinorVersionNumber,
      string machineName,
      string macAddress,
      string systemPassPhrase);

    [OperationContract]
    StringErrorWrapper GetUserGUIDByUsername(string username, string systemPassPhrase);

    [OperationContract]
    SimpleErrorWrapper RemoveStreamsFromSilentMerge(string macAddress,
      ChannelSubscriptions channelSubscriptions,
      string systemPassPhrase);

    [OperationContract]
    SimpleErrorWrapper ReplaceStreamsFromSilentMerge(string macAddress,
      ChannelSubscriptions channelSubscriptions,
      string systemPassPhrase);

    [OperationContract]
    SimpleErrorWrapper AddStreamsFromSilentMerge(string macAddress,
      ChannelSubscriptions channelSubscriptions,
      string systemPassPhrase);

    [OperationContract]
    SimpleErrorWrapper RemoveStreamsFromSilentMergeByMachineGUID(string machineGUID, 
      ChannelSubscriptions channelSubscriptions,
      string systemPassPhrase);

    [OperationContract]
    SimpleErrorWrapper ReplaceStreamsFromSilentMergeByMachineGUID(string machineGUID,
      ChannelSubscriptions channelSubscriptions,
      string systemPassPhrase);

    [OperationContract]
    SimpleErrorWrapper AddStreamsFromSilentMergeByMachineGUID(string machineGUID,
      ChannelSubscriptions channelSubscriptions,
      string systemPassPhrase);

    [OperationContract]
    SimpleErrorWrapper CompareMACAddresses(string macAddressClient, string userGUID, int softwareMajorVersionNumber,
      int softwareMinorVersionNumber, out string newMachineGUID, out bool bMatch, string systemPassPhrase);

    [OperationContract]
    StringErrorWrapper AddSubscriptionsAndNewPC(string userGUID, string macAddress, string machineName,
      int majorVersionNumber, int minorVersionNumber, string[][] subscriptions, string systemPassPhrase);

    [OperationContract]
    StringErrorWrapper CheckIfPCExistsReturnGUID(string username, string macAddress, string systemPassPhrase);
  }
}
