using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OxigenIIAdvertising.FileRights;
using System.IO;
using System.Security.Principal;
using System.Security.AccessControl;

namespace LibraryTester
{
  public partial class FilePermitChecker : Form
  {
    public FilePermitChecker()
    {
      InitializeComponent();
    }

    private void btnOpenDir_Click(object sender, EventArgs e)
    {
      folderBrowserDialog1.ShowDialog();

      if (folderBrowserDialog1.SelectedPath == null)
        return;

      string path = folderBrowserDialog1.SelectedPath;

      AreFilesReadableWritable(path);
    }

    public bool AreFilesReadableWritable(string rootDirectory)
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

          lblVerdict.Text += fsr.ToString();

          if (!IsReadableWritable(fsr))
            return false;
        }

        // check if can create/delete files;
        if (!CanCreateDeleteFiles(directory))
          return false;

        // check if can create/delete directories;
        if (!CanCreateDeleteDirectories(directory))
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
    public bool IsSingleFileReadableWritable(string filePath)
    {
      WindowsIdentity currentIdentity = WindowsIdentity.GetCurrent();
      string username = currentIdentity.Name;
      IdentityReferenceCollection currentUsersGroups = currentIdentity.Groups.Translate(typeof(NTAccount));

      FileSystemRights fsr = GetFilePermissions(username, filePath, currentUsersGroups);

      if (!IsReadableWritable(fsr))
        return false;

      return true;
    }

    private FileSystemRights GetFilePermissions(string user, string path, IdentityReferenceCollection currentUsersGroups)
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
    private bool IsUserInGroup(string fsRuleReference, IdentityReferenceCollection currentUsersGroups)
    {
      foreach (IdentityReference currentUsersIndividualGroup in currentUsersGroups)
      {
        if (fsRuleReference == currentUsersIndividualGroup.Value.ToLower())
          return true;
      }

      return false;
    }

    private bool IsReadableWritable(FileSystemRights rightsFound)
    {
      // File rights current user must have
      FileSystemRights writeAccess = FileSystemRights.WriteData | FileSystemRights.ReadData | FileSystemRights.AppendData;

      return ((rightsFound & writeAccess) == writeAccess);
    }

    private bool CanCreateDeleteFiles(string directoryPath)
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

    private bool CanCreateDeleteDirectories(string directoryPath)
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
  }
}
