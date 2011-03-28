using System;
using System.Collections.Generic;
using System.Text;

namespace Setup
{
  internal static class SilentMerger
  {
    private static bool ValidateArgs(string[] args)
    {
      switch (args[1].ToLower())
      {
        case "/a":
          goto case "/d";
        case "/r":
          goto case "/d";
        case "/d":
          return true;
        default:
          return false;
      }
    }

    internal static void Merge(string[] args)
    {
      if (!SetupHelper.OxigenExists())
        return;

      if (args.Length < 2)
        return;

      if (AppDataSingleton.Instance.FileDetectedChannelSubscriptionsLocal.SubscriptionSet != null)
      {
        AppDataSingleton.Instance.ChannelSubscriptionsToUpload.SubscriptionSet = SetupHelper.GetChannelSubscriptionsNetFromLocal(AppDataSingleton.Instance.FileDetectedChannelSubscriptionsLocal.SubscriptionSet);
        
        switch (args[1].ToLower())
        {
          case "/a":
            AddStreams();
            break;
          case "/r":
            ReplaceStreams();
            break;
          case "/d":
            DeleteStreams();
            break;
          default:
            return;
        }
      }
    }

    private static void DeleteStreams()
    {
      string UMSUri = null;
      string macAddress = SetupHelper.GetMACAddress();

      if (!GetResponsiveURI(ref UMSUri))
        return;

      UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer client = null;

      try
      {
        client = new UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer();
        client.Url = UMSUri;

        client.RemoveStreamsFromSilentMerge(macAddress,
          AppDataSingleton.Instance.ChannelSubscriptionsToUpload, "password");
      }
      catch (System.Net.WebException)
      {
      }
      finally
      {
        if (client != null)
        {
          try
          {
            client.Dispose();
          }
          catch
          {
            client.Abort();
          }
        }
      }
    }

    private static void ReplaceStreams()
    {
      string UMSUri = null;
      string macAddress = SetupHelper.GetMACAddress();

      if (!GetResponsiveURI(ref UMSUri))
        return;

      UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer client = null;

      try
      {
        client = new UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer();
        client.Url = UMSUri;

        client.ReplaceStreamsFromSilentMerge(macAddress,
          AppDataSingleton.Instance.ChannelSubscriptionsToUpload, "password");
      }
      catch (System.Net.WebException)
      {
      }
      finally
      {
        if (client != null)
        {
          try
          {
            client.Dispose();
          }
          catch
          {
            client.Abort();
          }
        }
      }
    }

    private static void AddStreams()
    {
      string UMSUri = null;
      string macAddress = SetupHelper.GetMACAddress();

      if (!GetResponsiveURI(ref UMSUri))
        return;

      UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer client = null;

      try
      {
        client = new UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer();
        client.Url = UMSUri;

        client.AddStreamsFromSilentMerge(macAddress,
          AppDataSingleton.Instance.ChannelSubscriptionsToUpload, "password");
      }
      catch (System.Net.WebException)
      {
      }
      finally
      {
        if (client != null)
        {
          try
          {
            client.Dispose();
          }
          catch
          {
            client.Abort();
          }
        }
      }
    }

    private static bool GetResponsiveURI(ref string UMSUri)
    {
      UMSUri = SetupHelper.GetResponsiveServer(OxigenIIAdvertising.ServerConnectAttempt.ServerType.MasterGetConfig,
          int.Parse(AppDataSingleton.Instance.GeneralData.NoServers["masterConfig"]),
          SetupHelper.GetRandomLetter().ToString(),
          "UserManagementServices.svc");

      if (string.IsNullOrEmpty(UMSUri))
        return false;

      return true;
    }    
  }
}
