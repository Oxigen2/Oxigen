using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Setup.ClientLoggers;

namespace Setup
{
  public partial class InstallationPathsForm : SetupForm
  {
    private const long _binaryRequiredSpace = 15728640;
    private const long _dataRequiredSpace = 104857600;

    public InstallationPathsForm()
    {
      InitializeComponent();

      string defaultBinaryInstallationFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Oxigen";

      if (!string.IsNullOrEmpty(AppDataSingleton.Instance.BinariesPath))
        txtBinariesPath.Text = AppDataSingleton.Instance.BinariesPath;
      else
        txtBinariesPath.Text = defaultBinaryInstallationFolder;

      string defaultDataInstallationFolder = SetupHelper.GetDefaultDataFolder() + "\\Oxigen";

      if (!string.IsNullOrEmpty(AppDataSingleton.Instance.DataPath))
        txtDataPath.Text = AppDataSingleton.Instance.DataPath;
      else
        txtDataPath.Text = defaultDataInstallationFolder;
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      string binariesPath = txtBinariesPath.Text.Trim();
      string dataPath = txtDataPath.Text.Trim();

      if (string.IsNullOrEmpty(binariesPath) || string.IsNullOrEmpty(dataPath))
      {
        MessageBox.Show("Please enter a path in both fields.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }

      binariesPath = binariesPath.Substring(0, 1).ToUpper() + binariesPath.Substring(1, binariesPath.Length - 1);
      dataPath = dataPath.Substring(0, 1).ToUpper() + dataPath.Substring(1, dataPath.Length - 1);

      if (!SetupHelper.DriveExistsAndFixed(binariesPath) || !SetupHelper.DriveExistsAndFixed(dataPath))
      {
        MessageBox.Show("Please make sure both selected paths are in drives that exist and are fixed.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }

      if (binariesPath.Length == 1)
        binariesPath += ":\\";

      if (dataPath.Length == 1)
        dataPath += ":\\";

      if (!binariesPath.EndsWith("\\"))
        binariesPath += "\\";

      if (!dataPath.EndsWith("\\"))
        dataPath += "\\";

      if (!SetupHelper.IsSufficientSpace(binariesPath, _binaryRequiredSpace))
      {
        MessageBox.Show("Disk space for the Oxigen Program is insufficient. Please select a different location.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }

      if (!SetupHelper.IsSufficientSpace(dataPath, _dataRequiredSpace))
      {
        MessageBox.Show("Disk space for the Oxigen Data is insufficient. Please select a different location.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }

      AppDataSingleton.Instance.BinariesPath = binariesPath;
      AppDataSingleton.Instance.DataPath = dataPath;

      // available space for the Content Exchanger will be the free disk space 
      // of the disk the data will be stored minus 40MB.
      DriveInfo di = new DriveInfo(dataPath);
      long freeSpace = di.AvailableFreeSpace;
      AppDataSingleton.Instance.User.AssetFolderSize = freeSpace - 41943040L;

      SetupHelper.OpenForm<InstallConfirm>(this);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      SetupHelper.ExitConfirmNoChanges();
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
      if (AppDataSingleton.Instance.ExistingUser)
        SetupHelper.OpenForm<UpdateExistingUserDetailsForm>(this);
      else
        SetupHelper.OpenForm<RegistrationForm4>(this);
    }

    private void btnBrowseBinaries_Click(object sender, EventArgs e)
    {
      if (folderBrowserBinaries.ShowDialog() == DialogResult.OK)
        txtBinariesPath.Text = folderBrowserBinaries.SelectedPath;
    }

    private void btnBrowseData_Click(object sender, EventArgs e)
    {
      if (folderBrowserData.ShowDialog() == DialogResult.OK)
        txtDataPath.Text = folderBrowserData.SelectedPath;
    }

    private void txtDataPath_TextChanged(object sender, EventArgs e)
    {
      TextBox dataPathTextBox = (TextBox)sender;

      if (dataPathTextBox.Text.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles))
        || dataPathTextBox.Text.StartsWith(Environment.GetEnvironmentVariable("windir")))
      {
        lblValidation.Text = "Invalid location for program data";
        btnNext.Enabled = false;
      }
      else
      {
        lblValidation.Text = "";
        btnNext.Enabled = true;
      }
    }

    private void Form_Shown(object sender, EventArgs e)
    {
      ClientLogger logger = new PersistentClientLogger();
      logger.Log("6-InstallationPaths");
    }
  }
}
