using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Principal;
using System.Diagnostics;

namespace OxigenSU
{
  public class UpdatePrompter
  {
    ComponentListRetriever _clr = null;
    string _appDataPath = null;

    EventLog _log;

    public UpdatePrompter(ComponentListRetriever clr, string appDataPath)
    {
      _clr = clr;
      _appDataPath = appDataPath;

      _log = new EventLog();
      _log.Source = "OxigenSU";
      _log.Log = String.Empty;
    }

    /// <summary>
    /// If previous operation has found updates, then prompt to download here and act as per user input.
    /// </summary>
    public void PromptForUpdateIfExists()
    {
      if (_clr.ChangedComponents != null && _clr.ChangedComponents.Count > 0)
      {
        if (MessageBox.Show("There are new updates available. Do you want to download them now?", "Oxigen", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
        {
          Application.Exit();
          return;
        }

        // save temp file to non-UAC'ed directory
        if (!TrySerializeComponentList(_clr, _appDataPath))
        {
          Application.Exit();
          return;
        }

        if (!HasAdminRights())
        {
          // application needs to restart with elevated privileges. Cannot elevate privileges
          // in an already running process.
          if (RunElevated(Application.ExecutablePath))
          {
            Application.Exit();
            return;
          }
        }

        if (!AppDataSingleton.Instance.IsVerboseMode)
        {
          Application.Run(new ProgressForm());
          return;
        }
        else
        {
          ProgressForm progressform = new ProgressForm();
          progressform.Show();
        }
      }
    }

    private bool TrySerializeComponentList(ComponentListRetriever clr, string appDataPath)
    {
      try
      {
        Serializer.SerializeToClearText(clr.ChangedComponents, appDataPath + "\\SettingsData\\components.dat");
        return true;
      }
      catch
      {
        if (MessageBox.Show("Oxigen Updater has encountered an error. Please try again later", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
          return TrySerializeComponentList(clr, appDataPath);
        else
          return false;
      }
    }

    private bool HasAdminRights()
    {
      WindowsPrincipal pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
      return pricipal.IsInRole(WindowsBuiltInRole.Administrator);
    }

    private bool RunElevated(string fileName)
    {
      ProcessStartInfo processInfo = new ProcessStartInfo();
      processInfo.Verb = "runas";
      processInfo.FileName = fileName;

      try
      {
        Process.Start(processInfo);
        return true;
      }
      catch (System.ComponentModel.Win32Exception)
      {
        // Do nothing. Probably the user canceled the UAC window, so communicate that fact.
        return false;
      }
    }
  }
}
