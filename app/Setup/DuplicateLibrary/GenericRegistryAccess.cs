﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace Setup
{
  /// <summary>
  /// Provides methods to access / modify the registry.
  /// On missing registry key or values, an exception is not thrown as
  /// situations like keys already gone or user not having permission we don't treat as fatal errors.
  /// </summary>
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
      {
        registryKey = registryKey.OpenSubKey(registryKeys[i], true);

        if (registryKey == null)
          return null;
      }

      return registryKey;
    }

    /// <summary>
    /// Gets a value from the registry given the specified key
    /// </summary>
    /// <param name="keyPath">Key to look up a value of</param>
    /// <param name="value">a value inside the key</param>
    public static object GetRegistryValue(string keyPath, string value)
    {
      RegistryKey registryKey = GetRegistryKey(keyPath);

      if (registryKey == null)
        return null;

      return registryKey.GetValue(value);
    }

    /// <summary>
    /// Checks if registry key exists
    /// </summary>
    /// <param name="path">the registry key to retrieve as a string</param>
    /// <returns>true fi registry key exists, false otherwise</returns>
    public static bool RegistryKeyExists(string path)
    {
      return GetRegistryKey(path) != null;
    }

    /// <summary>
    /// Deletes a value from a registry key
    /// </summary>
    /// <param name="keyPath">Registry Key</param>
    /// <param name="value">Registry value</param>
    /// <exception cref="NullReferenceException"></exception>
    /// <exception cref="IOException"></exception>
    /// <exception cref="System.Security.SecurityException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="System.ObjectDisposedException"></exception>
    /// <exception cref="System.UnauthorizedAccessException"></exception>
    public static void DeleteRegistryValue(string keyPath, string value)
    {
      RegistryKey key = GetRegistryKey(keyPath);
      
      if (key == null)
        return;

      key.DeleteValue(value);
    }

    /// <summary>
    /// Deletes a registry key
    /// </summary>
    /// <param name="keyPath">Registry Key to delete</param>
    /// <exception cref="NullReferenceException"></exception>
    /// <exception cref="IOException"></exception>
    /// <exception cref="System.Security.SecurityException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="System.ObjectDisposedException"></exception>
    /// <exception cref="System.UnauthorizedAccessException"></exception>
    public static void DeleteRegistryKey(string keyPath)
    {
      int lastIndexOfBackslash = keyPath.LastIndexOf("\\");

      string parentKey = keyPath.Substring(0, lastIndexOfBackslash);
      string keyToDelete = keyPath.Substring(lastIndexOfBackslash + 1, keyPath.Length - lastIndexOfBackslash - 1);

      RegistryKey key = GetRegistryKey(parentKey);

      if (key != null)
        key.DeleteSubKey(keyToDelete, false);
    }

    /// <summary>
    /// Sets a value inside the registry key
    /// </summary>
    /// <param name="keyPath">The path to the registry key to set a value in</param>
    /// <param name="valueName">Name of the value</param>
    /// <param name="valueValue">Value of the value</param>
    /// <exception cref="NullReferenceException"></exception>
    /// <exception cref="IOException"></exception>
    /// <exception cref="System.Security.SecurityException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static void SetRegistryValue(string keyPath, string valueName, object valueValue)
    {
      RegistryKey key = GetRegistryKey(keyPath);

      if (key == null)
        throw new NullReferenceException("Key " + keyPath + " is null");
   
      key.SetValue(valueName, valueValue);
    }

    /// <summary>
    /// Sets a value inside the registry key
    /// </summary>
    /// <param name="keyPath">The path to the registry key to set a value in</param>
    /// <param name="valueName">Name of the value</param>
    /// <param name="valueValue">Value of the value</param>
    /// <param name="kind">Kind of registry value</param>
    /// <exception cref="NullReferenceException"></exception>
    /// <exception cref="IOException"></exception>
    /// <exception cref="System.Security.SecurityException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static void SetRegistryValue(string keyPath, string valueName, object valueValue, RegistryValueKind kind)
    {
      RegistryKey key = GetRegistryKey(keyPath);

      if (key == null)
        throw new NullReferenceException("Key " + keyPath + " is null");

      key.SetValue(valueName, valueValue, kind);
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
          throw new ArgumentException("Root key not found: " + root);
      }
    }

    internal static bool RegistryValueExists(string keyPath, string value)
    {
      RegistryKey key = GetRegistryKey(keyPath);

      if (key == null)
        return false;

      return key.GetValue(value) != null;
    }
  }
}
