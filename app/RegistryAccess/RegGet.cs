using System;
using System.Collections.Generic;
using Microsoft.Win32;
using OxigenIIAdvertising.Exceptions;
using WindowsVersionDetection;
using System.Text.RegularExpressions;

namespace RegistryAccess
{
  /// <summary>
  /// Provides static methods that inquire the registry for information
  /// </summary>
  public static class RegGet
  {
    /// <summary>
    /// Gets the path of the default web browser for the current user from the registry. Tested in Windows XP Professional
    /// </summary>
    /// <returns>the fully qualified path in the system where the default browser resides</returns>
    /// <exception cref="ArgumentNullException">name is null</exception>
    /// <exception cref="ObjectDisposedException">The Microsoft.Win32.RegistryKey is closed (closed keys cannot be accessed).</exception>
    /// <exception cref="System.Security.SecurityException">The user does not have the permissions required to read the registry key.</exception>
    /// <exception cref="NullReferenceException">Key not found</exception>
    /// <exception cref="RegistryException">Default browser information not found</exception>
    public static string GetDefaultBrowserAppPath()
    {
      Regex pcNameFilePattern = new Regex(@"[a-zA-Z]:\\[a-zA-Z](?:(?:\w|\s|\\)+).[a-zA-Z]+", RegexOptions.Compiled);

      object defaultBrowserAppPathObj = GenericRegistryAccess.GetRegistryKey(GetDefaultBrowswerRegistryKeyString()).GetValue("");

      if (defaultBrowserAppPathObj == null)
        throw new RegistryException("Default Browser information not found");

      string rawEntry = defaultBrowserAppPathObj.ToString();
      string browserPath = pcNameFilePattern.Match(rawEntry).Value;

      if (browserPath == "")
        throw new ArgumentException("Could not extract browser information");
      
      return browserPath;
    }

    private static string GetDefaultBrowswerRegistryKeyString()
    {
      WindowsVersion version = VersionDetect.GetWindowsVersion();

      switch (version)
      {
        case WindowsVersion.WindowsXP:
          return @"HKEY_CURRENT_USER\Software\Classes\http\shell\open\command";
        default:
          // DEFERRED: support more Windows operating systems
          throw new ApplicationException("Other Operating systems not yet supported");
      }
    }
  }
}
