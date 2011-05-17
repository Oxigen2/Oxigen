using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProxyClientBaseLib;
using OxigenIIAdvertising.ServiceContracts.UserManagementServices;
using ServiceErrorReporting;
using OxigenSU;

namespace OxigenIIAdvertising.UserManagementServicesServiceClient
{
  public class UserManagementServicesNonStreamerClient : ProxyClientBase<IUserManagementServicesNonStreamer>
  {
    //
    // Not used
    //

    //public SimpleErrorWrapper GetUserExistsByUserCredentials(string emailAddress, string password, out string userGUID, string systemPassPhrase)
    //{
    //  throw new NotImplementedException();
    //}

    //public SimpleErrorWrapper RegisterNewUser(string emailAddress, string password, string firstName, string lastName, string gender, DateTime dob, string townCity, string state, string country, string occupationSector, string employmentLevel, string annualHouseholdIncome, string userGUID, string machineGUID, int softwareMajorVersionNumber, int softwareMinorVersionNumber, string macAddress, string machineName, OxigenIIAdvertising.AppData.ChannelSubscriptions channelSubscriptions, string systemPassPhrase)
    //{
    //  throw new NotImplementedException();
    //}

    //public SimpleErrorWrapper GetExistingUserDetails(string userGUID, string password, out OxigenIIUserMgmtServicesService.UserInfo userInfo, string systemPassPhrase)
    //{
    //  throw new NotImplementedException();
    //}

    //public SimpleErrorWrapper UpdateUserAccount(string emailAddress, string password, string firstName, string lastName, string gender, DateTime dob, string townCity, string state, string country, string occupationSector, string employmentLevel, string annualHouseholdIncome, OxigenIIAdvertising.AppData.ChannelSubscriptions channelSubscriptions, int softwareMajorVersionNumber, int softwareMinorVersionNumber, string machineGUID, string newPcName, string macAddress, string systemPassPhrase)
    //{
    //  throw new NotImplementedException();
    //}

    //public SimpleErrorWrapper GetPcListForInstallerEmail(string emailAddress, out List<OxigenIIAdvertising.SOAStructures.PcInfo> pcs, string systemPassPhrase)
    //{
    //  throw new NotImplementedException();
    //}

    //public SimpleErrorWrapper GetPcListForInstallerGUID(string userGUID, out List<OxigenIIAdvertising.SOAStructures.PcInfo> pcs, string systemPassPhrase)
    //{
    //  throw new NotImplementedException();
    //}

    //public SimpleErrorWrapper LinkPCAndSubscriptionsExistingPC(string userGUID, int pcID, string machineGUID, string macAddress, int softwareMajorVersionNumber, int softwareMinorVersionNumber, OxigenIIAdvertising.AppData.ChannelSubscriptions channelSubscriptions, string systemPassPhrase)
    //{
    //  throw new NotImplementedException();
    //}

    //public SimpleErrorWrapper CheckEmailAddressExists(string emailAddress, string systemPassPhrase)
    //{
    //  throw new NotImplementedException();
    //}

    //public SimpleErrorWrapper EditSubscriptions(string userGUID, string machineGUID, OxigenIIAdvertising.AppData.ChannelSubscriptions channelSubscriptions, string systemPassPhrase)
    //{
    //  throw new NotImplementedException();
    //}

    //public SimpleErrorWrapper GetPCSubscriptionsByPCID(string userGUID, int pcID, out OxigenIIAdvertising.AppData.ChannelSubscriptions channelSubscriptions, string systemPassPhrase)
    //{
    //  throw new NotImplementedException();
    //}

    //public SimpleErrorWrapper GetPCSubscriptionsByMachineGUID(string userGUID, string machineGUID, out OxigenIIAdvertising.AppData.ChannelSubscriptions channelSubscriptions, string systemPassPhrase)
    //{
    //  throw new NotImplementedException();
    //}

    //public SimpleErrorWrapper GetMatchedUserGUID(string userGUID, string systemPassPhrase)
    //{
    //  throw new NotImplementedException();
    //}

    //public SimpleErrorWrapper GetMatchedMachineGUID(string userGUID, string machineGUID, string systemPassPhrase)
    //{
    //  throw new NotImplementedException();
    //}

    //public SimpleErrorWrapper CompareMACAddresses(string macAddressClient, string userGUID, int softwareMajorVersionNumber, int softwareMinorVersionNumber, out string newMachineGUID, out bool bMatch, string systemPassPhrase)
    //{
    //  throw new NotImplementedException();
    //}

    //public SimpleErrorWrapper SendEmailReminder(string emailAddress, string systemPassPhrase)
    //{
    //  throw new NotImplementedException();
    //}

    //public SimpleErrorWrapper RegisterSoftwareUninstall(string userGUID, string machineGUID, string password)
    //{
    //  throw new NotImplementedException();
    //}

    //public void SendErrorReport(string macAddress, string exceptionDetails)
    //{
    //  throw new NotImplementedException();
    //}
  }
}
