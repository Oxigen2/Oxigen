using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;

namespace OxigenIIAdvertising.FileRights
{
  /// <summary>
  /// Provides methods that check if current user has write/create/delete permissions.
  /// Methods are decoupled i.e. different method for checking files, different method for checking directories
  /// as overhead will be reduced to what is needed.
  /// </summary>
  public static class FileDirectoryRightsChecker
  {
    /// <summary>
    /// Checks if current user can create a directory under a path. Non recursive.
    /// </summary>
    /// <param name="directoryPath">path to create a directory in</param>
    /// <returns>true if successful, false otherwise</returns>
    public static bool CanCreateDeleteDirectories(string directoryPath)
    {
      string directoryName = directoryPath + "\\" + System.Guid.NewGuid().ToString();

      try
      {
        Directory.CreateDirectory(directoryName);
      }
      catch
      {
        return false;
      }

      try
      {
        Directory.Delete(directoryName, true);
      }
      catch
      {
        return false;
      }

      return true;
    }

    /// <summary>
    /// Checks if all files in a directory are readable/writable for the current user and recursively checks all subdirectories.
    /// Also checks if current user has create/delete permissions for directories
    /// </summary>
    /// <param name="rootDirectory">Starting path to check the files in</param>
    /// <returns>false if there is at least one file which doesn't fulfill the above rights, true otherwise</returns>
    public static bool AreFilesReadableWritable(string rootDirectory)
    {
      WindowsIdentity currentIdentity = WindowsIdentity.GetCurrent();
      string username = currentIdentity.Name;
      IdentityReferenceCollection currentUsersGroups = currentIdentity.Groups.Translate(typeof(NTAccount));

      string[] subDirectories = Directory.GetDirectories(rootDirectory);

      foreach (string directory in subDirectories)
      {
        string[] files = Directory.GetFiles(directory);

        foreach (string file in files)
        {
          FileSystemRights fsr = GetFilePermissions(username, file, currentUsersGroups);

          if (!IsReadableWritable(fsr))
            return false;
        }

        // check if can create/delete files;
        if (!CanCreateDeleteFiles(directory))
          return false;
      }

      return true;
    }

    /// <summary>
    /// Checks if given file is readable/writable for the current user
    /// The access rights that are expected of each file are: WriteData, ReadData, AppendData, CreateFile
    /// </summary>
    /// <param name="filePath">file to check for rights</param>
    /// <returns>true if file is readable/writable/deletable, false otherwise</returns>
    public static bool IsSingleFileReadableWritable(string filePath)
    {
      WindowsIdentity currentIdentity = WindowsIdentity.GetCurrent();
      string username = currentIdentity.Name;
      IdentityReferenceCollection currentUsersGroups = currentIdentity.Groups.Translate(typeof(NTAccount));

      FileSystemRights fsr = GetFilePermissions(username, filePath, currentUsersGroups);

      if (!IsReadableWritable(fsr))
        return false;

      return true;
    }

    private static FileSystemRights GetFilePermissions(string user, string path, IdentityReferenceCollection currentUsersGroups)
    {
      if (!File.Exists(path))
        return 0;

      string identityReference = user.ToLower();

      FileSecurity fileSecurity = File.GetAccessControl(path, AccessControlSections.Access);

      AuthorizationRuleCollection fsRules = fileSecurity.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));

      foreach (FileSystemAccessRule fsRule in fsRules)
      {
        string fsRuleReference = fsRule.IdentityReference.Value.ToLower();

        if (fsRuleReference == identityReference || IsUserInGroup(fsRuleReference, currentUsersGroups))
          return (fsRule.FileSystemRights);
      }

      return 0;
    }

    // checks if a group belongs to the current user's groups
    private static bool IsUserInGroup(string fsRuleReference, IdentityReferenceCollection currentUsersGroups)
    {
      foreach (IdentityReference currentUsersIndividualGroup in currentUsersGroups)
      {
        if (fsRuleReference == currentUsersIndividualGroup.Value.ToLower())
          return true;
      }

      return false;
    }

    private static bool IsReadableWritable(FileSystemRights rightsFound)
    {
      // File rights current user must have
      FileSystemRights writeAccess = FileSystemRights.WriteData | FileSystemRights.ReadData | FileSystemRights.AppendData;

      return ((rightsFound & writeAccess) == writeAccess);
    }

    private static bool CanCreateDeleteFiles(string directoryPath)
    {
      string fileName = directoryPath + "\\" + System.Guid.NewGuid().ToString();

      try
      {
        File.Create(fileName).Dispose();
      }
      catch
      {
        return false;
      }

      try
      {
        File.Delete(fileName);
      }
      catch
      {
        return false;
      }

      return true;
    }
  }
}
