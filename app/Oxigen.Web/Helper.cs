using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oxigen.Core.Installer;
using OxigenIIAdvertising.SOAStructures;
using System.Web.SessionState;
using System.Diagnostics;

namespace OxigenIIPresentation
{
    #region ASP.Net script reference helpers
    public class ScriptReference : WebControl
    {
        /* Constructors. */

        public ScriptReference()
            : base(HtmlTextWriterTag.Script) {
        }


        [UrlProperty, Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor")]
        public string Src {
            get {
                string src = (string)ViewState["Src"];
                if (src != null) {
                    return src;
                }
                return string.Empty;
            }
            set {
                ViewState["Src"] = value;
            }
        }

        public bool Defer {
            get {
                object def = ViewState["Defer"];
                return null == def ? false : (bool)def;
            }
            set {
                ViewState["Defer"] = value;
            }
        }

        public string Charset {
            get {
                string charset = (string)ViewState["Charset"];
                if (charset != null) {
                    return charset;
                }
                return string.Empty;
            }
            set {
                ViewState["Charset"] = value;
            }
        }

        /* Protected Methods. */

        protected override void AddAttributesToRender(HtmlTextWriter writer) {
            base.AddAttributesToRender(writer);
            string src = Src;
            if (src.Length > 0) {
                string resolved = src;
                resolved = Page.ResolveUrl(resolved);
                if (!src.StartsWith("http://") && !src.StartsWith("https://"))
                    resolved = ChecksumHelper.AppendChecksum(Context, resolved);


                writer.AddAttribute(HtmlTextWriterAttribute.Src, resolved);
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");

            if (Defer)
                writer.AddAttribute("defer", "defer");
            string charset = Charset;
            if (charset.Length > 0) {
                writer.AddAttribute("charset", charset);
            }
        }

        protected override void RenderContents(HtmlTextWriter writer) {
        }
    }


    public class StylesheetReference : WebControl
    {
        public StylesheetReference()
            : base(HtmlTextWriterTag.Link) {
        }

        [UrlProperty, Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor")]
        public string Href {
            get {
                string src = (string)ViewState["Href"];
                if (src != null) {
                    return src;
                }
                return string.Empty;
            }
            set {
                ViewState["Href"] = value;
            }
        }

        public string Media {
            get {
                string media = (string)ViewState["Media"];
                if (media != null) {
                    return media;
                }
                return string.Empty;
            }
            set {
                ViewState["Media"] = value;
            }
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer) {
            base.AddAttributesToRender(writer);
            string href = Href;
            if (href.Length > 0) {
                string resolved = Page.ResolveUrl(href);
                resolved = ChecksumHelper.AppendChecksum(Context, resolved);
                writer.AddAttribute(HtmlTextWriterAttribute.Href, resolved);
            }
            string media = Media;
            if (media.Length > 0) {
                writer.AddAttribute("media", media);
            }
            writer.AddAttribute("type", "text/css");
            writer.AddAttribute("rel", "stylesheet");
        }
    }



    public static class ChecksumHelper
    {
        static Dictionary<string, string> _checksums = new Dictionary<string, string>();

        public static string AppendChecksum(HttpContext context, string href) {
            string sum = GetChecksum(context, href);
            href += href.Contains("?") ? "&amp;_len=" : "?_len=";
            href += sum;
            return href;
        }

        static string GetChecksum(HttpContext context, string href) {
            string checksum;
            bool hasChecksum = _checksums.TryGetValue(href, out checksum);
            if (hasChecksum)
                return checksum;
            string localPath = context.Server.MapPath(href.Split('?')[0]);
            _checksums[href] = new FileInfo(localPath).Length.ToString();
            return _checksums[href];
        }
    }

    #endregion
    
    #region MVC URLHelper Extensions

  public static class URLHelperExtension
  {
      public static HtmlString Script(this UrlHelper helper, string contentPath) {
          return new HtmlString(string.Format("<script type='text/javascript' src='{0}'></script>", LatestContent(helper, contentPath)));
      }

      public static string LatestContent(this UrlHelper helper, string contentPath) {
          string file = HttpContext.Current.Server.MapPath(contentPath);
          if (File.Exists(file)) {
              var dateTime = File.GetLastWriteTime(file);
              contentPath = string.Format("{0}?v={1}", contentPath, dateTime.Ticks);
          }
          return helper.Content(contentPath);
      }

      public static HtmlString Css(this UrlHelper helper, string contentPath) {
          return new HtmlString(string.Format("<link rel='stylesheet' type='text/css' href='{0}' media='screen' />", LatestContent(helper, contentPath)));
      }

  }
    #endregion

    public static class Url
    {
        public static string For(InstallerSetup installerSetup)
        {
            return "http://download.oxigen.net/installer/" + installerSetup.UrlKey;
        }
    }


  /// <summary>
  /// Provides helper methods for user access
  /// </summary>
  public static class Helper
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

      RunProcessAndWaitForExit(HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) + "Bin\\Oxigen.SelfExtractorCreator.exe", convertedPCName + " \"" + tempInstallersPathTemp + "\\\"");
      // sign the self-extractor
      RunProcessAndWaitForExit(System.Configuration.ConfigurationSettings.AppSettings["signToolPath"], System.Configuration.ConfigurationSettings.AppSettings["signToolArguments"] + "\"" + tempInstallersPathTemp + convertedPCName + ".exe\" >> " + System.Configuration.ConfigurationSettings.AppSettings["debugPath"]);

      return convertedPCName;
    }

    private static void RunProcessAndWaitForExit(string fileName, string arguments)
    {
      Process process = null;

      ProcessStartInfo startInfo = new ProcessStartInfo(fileName, arguments);
      startInfo.RedirectStandardError = true;
      startInfo.UseShellExecute = false;
      process = Process.Start(startInfo);
      string error = process.StandardError.ReadToEnd();
      process.WaitForExit();
      //throw new Exception(process.ExitCode.ToString() + error + arguments);
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
