using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using OxigenIIAdvertising.Exceptions;

namespace RegistryAccess
{
  public static class GenericRegistryAccess
  {
    /// <summary>
    /// Gets the registry key from a string
    /// </summary>
    /// <param name="path">the registry key to retrieve as a string</param>
    /// <returns>a Microsoft.Win32.Registrykey object</returns>
    /// <exception cref="ArgumentException">name is longer than the maximum length allowed.</exception>
    /// <exception cref="ArgumentNullException">name is null</exception>
    /// <exception cref="ObjectDisposedException">The Microsoft.Win32.RegistryKey is closed (closed keys cannot be accessed).</exception>
    /// <exception cref="System.Security.SecurityException">The user does not have the permissions required to read the registry key.</exception>
    /// <exception cref="NullReferenceException">Key not found</exception>
    public static RegistryKey GetRegistryKey(string path)
    {
      string[] registryKeys = path.Split('\\');

      int noKeys = registryKeys.Length;

      RegistryKey registryKey = GetRoot(registryKeys[0]);

      for (int i = 1; i < noKeys; i++)
        registryKey = registryKey.OpenSubKey(registryKeys[i]);

      return registryKey;
    }

    private static RegistryKey GetRoot(string root)
    {
      switch (root)
      {
        case "HKEY_CLASSES_ROOT":
          return Registry.ClassesRoot;
        case "HKEY_CURRENT_CONFIG":
          return Registry.CurrentConfig;
        case "HKEY_CURRENT_USER":
          return Registry.CurrentUser;
        case "HKEY_DYN_DATA":
          return Registry.DynData;
        case "HKEY_LOCAL_MACHINE":
          return Registry.LocalMachine;
        case "HKEY_PERFORMANCE_DATA":
          return Registry.PerformanceData;
        case "HKEY_USERS":
          return Registry.Users;
        default:
          throw new RegistryException("Root key not found: " + root);
      }
    }
  }
}
