using System;
using System.Collections.Generic;
using System.Text;

namespace Setup
{
  internal static class SilentMerger
  {
    private static bool ValidateArgs(string[] args)
    {
      if (args.Length < 2)
        return false;

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

      if (!ValidateArgs(args))
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
      string machineGUID = SetupHelper.GetFromRegistry("pcGUID");

      try
      {
          using (var client = new UserDataManagementClient())
          {

              client.RemoveStreamsFromSilentMergeByMachineGUID(machineGUID,
                                                               AppDataSingleton.Instance.ChannelSubscriptionsToUpload,
                                                               "password");
          }
      }
      catch (System.Net.WebException)
      {
      }
    }

    private static void ReplaceStreams()
    {
      string machineGUID = SetupHelper.GetFromRegistry("pcGUID");

      try
      {
          using (var client = new UserDataManagementClient())
          {
              client.ReplaceStreamsFromSilentMergeByMachineGUID(machineGUID,
                                                                AppDataSingleton.Instance.ChannelSubscriptionsToUpload,
                                                                "password");
          }
      }
      catch (System.Net.WebException)
      {
      }
    }

    private static void AddStreams()
    {
      string machineGUID = SetupHelper.GetFromRegistry("pcGUID");
      
      try
      {
          using (var client = new UserDataManagementClient())
          {
              client.AddStreamsFromSilentMergeByMachineGUID(machineGUID,
                                                            AppDataSingleton.Instance.ChannelSubscriptionsToUpload,
                                                            "password");
          }
      }
      catch (System.Net.WebException)
      {
      }
    }
  }
}
