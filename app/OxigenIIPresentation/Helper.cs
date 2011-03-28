using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OxigenIIAdvertising.SOAStructures;
using System.Web.SessionState;
using Ionic.Zip;
using System.Diagnostics;

namespace OxigenIIPresentation
{
  /// <summary>
  /// Provides helper methods for user access
  /// </summary>
  internal static class Helper
  {
    /// <summary>
    /// Tries to get the user's user ID from the session variable.
    /// </summary>
    /// <param name="session">the Session object to check</param>
    /// <param name="userID">the variable in which to store the user ID</param>
    /// <returns>true if a user ID exists, false otherwise</returns>
    internal static bool TryGetUserID(HttpSessionState session, out int userID)
    {
      if (session["User"] == null)
      {
        userID = -1;
        return false;
      }

      userID = ((User)session["User"]).UserID;

      return true;
    }

    // format: command,,containerID,,ID1||ID2||ID3...
    internal static string GetIDs(HttpSessionState session, string[] parameters, out int userID, out int containerID, out List<int> contentIDList)
    {
      containerID = -1;
      contentIDList = null;

      if (!TryGetUserID(session, out userID))
        return String.Empty;

      if (parameters.Length < 3)
        return ErrorWrapper.SendError("Command parameters missing.");

      if (!int.TryParse(parameters[1], out containerID))
        return ErrorWrapper.SendError("Invalid Folder ID");

      return GetContentIDs(parameters[2], out contentIDList);
    }

    internal static string GetContentIDs(string IDs, out List<int> contentIDList)
    {
      contentIDList = new List<int>();

      string[] contentIDs = IDs.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);

      foreach (string stringID in contentIDs)
      {
        int contentID;

        if (!int.TryParse(stringID, out contentID))
          return ErrorWrapper.SendError("Invalid ID");

        contentIDList.Add(contentID);
      }

      return "1";
    }

    internal static string GetIDsMove(HttpSessionState session, string[] parameters, out int userID, out int oldContainerID, out int newContainerID, out List<int> contentIDList)
    {
      oldContainerID = -1;
      newContainerID = -1;
      contentIDList = new List<int>();

      if (!Helper.TryGetUserID(session, out userID))
        return String.Empty;

      if (parameters.Length < 4)
        return ErrorWrapper.SendError("Command parameters missing.");

      if (!int.TryParse(parameters[1], out oldContainerID))
        return ErrorWrapper.SendError("Invalid Old Folder ID");

      if (!int.TryParse(parameters[2], out newContainerID))
        return ErrorWrapper.SendError("Invalid New Folder ID");

      string[] contentIDs = parameters[3].Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);

      foreach (string stringID in contentIDs)
      {
        int contentID;

        if (!int.TryParse(stringID, out contentID))
          return ErrorWrapper.SendError("Invalid ID");

        contentIDList.Add(contentID);
      }

      return "1";
    }

    /// <summary>
    /// Gets the first several words from a string.
    /// </summary>
    /// <param name="input">String to process</param>
    /// <param name="numberWords">Number of words in the returned string if the original string has less words, the whole string is returned.</param>
    internal static string FirstWords(string input, int numberWords)
    {
      int words = numberWords;
      int length = input.Length;

      // Loop through entire summary.
      for (int i = 0; i < length; i++)
      {
        // Increment words on a space.
        if (input[i] == ' ')
          words--;

        // If we have no more words to display, return the substring.
        if (words == 0)
          return input.Substring(0, i);
      }

      return input;
    }
    
    internal static bool NullableDateTryParse(string text, out DateTime? dateTime)
    {
      DateTime dt;

      if (DateTime.TryParse(text, out dt))
      {
        dateTime = dt;
        return true;
      }
      else
      {
        dateTime = null;
        return false;
      }
    }

    internal static bool NullableIntTryParse(string text, out int? integer)
    {
      int i;

      if (int.TryParse(text, out i))
      {
        integer = i;
        return true;
      }
      else
      {
        integer = null;
        return false;
      }
    }

    internal static bool DateWithinLimits(DateTime? date)
    {
      if (date > new DateTime(1900, 1, 1) && date < new DateTime(9999, 12, 1))
        return true;

      return false;
    }

    internal static string CreateSelfExtractor(string pcName, string tempInstallersPathTemp)
    {
      string convertedPCName = pcName.Replace(" ", "_").Replace("\\", "_").Replace("/", "_");

      using (ZipFile zf = new ZipFile())
      {
        zf.AddDirectory(tempInstallersPathTemp);

        SelfExtractorSaveOptions options = new SelfExtractorSaveOptions();
        options.Copyright = "Oxigen";
        options.DefaultExtractDirectory = "%TEMP%\\Oxigen";
        options.Flavor = SelfExtractorFlavor.ConsoleApplication;
        options.ProductName = "Oxigen";
        options.Quiet = true;
        options.RemoveUnpackedFilesAfterExecute = true;
        options.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
        options.PostExtractCommandLine = "%TEMP%\\Oxigen\\Setup.exe";
        zf.SaveSelfExtractor(tempInstallersPathTemp + "\\" + convertedPCName + ".exe", options);
      }

      // sign the self-extractor
      RunProcessAndWaitForExit(System.Configuration.ConfigurationSettings.AppSettings["signToolPath"], System.Configuration.ConfigurationSettings.AppSettings["signToolArguments"] + "\"" + tempInstallersPathTemp + convertedPCName + ".exe\" >> " + System.Configuration.ConfigurationSettings.AppSettings["debugPath"]);

      return convertedPCName;
    }

    private static void RunProcessAndWaitForExit(string fileName, string arguments)
    {
      Process process = null;

      ProcessStartInfo startInfo = new ProcessStartInfo(fileName, arguments);

      process = Process.Start(startInfo);

      process.WaitForExit();
    }

    public static List<T> AddToFrontOfDropDown<T>(List<T> list, T insertObj)
    {
      if (list == null)
        return null;

      list.Insert(0, insertObj);

      return list;
    }
  }
}
