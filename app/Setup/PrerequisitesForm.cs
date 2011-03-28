using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Setup.Properties;
using System.Threading;
using System.Management;
using Microsoft.Win32;
using System.Diagnostics;

namespace Setup
{
  public partial class PrerequisitesForm : SetupForm
  {
    bool _bPrerequisitesMet = true;

    public PrerequisitesForm()
    {
      InitializeComponent();

      lnkFlash.Text = "";
      lnkNET.Text = "";
      lnkQuickTime.Text = "";
      lnkWMP.Text = "";
    }

    private void PrerequisitesForm_Shown(object sender, EventArgs e)
    {
      Application.DoEvents();
      Thread.Sleep(500);

      RAMStatus status = SetupHelper.IsRamSufficient();

      if (status == RAMStatus.Good)
        ramIndicator.Image = Resources.tick;
      else if (status == RAMStatus.Marginal)
      {
        _bPrerequisitesMet = true;
        ramIndicator.Image = Resources.questionmark;
      }
      else
      {
        _bPrerequisitesMet = false;
        ramIndicator.Image = Resources.cross;
      }

      Application.DoEvents();
      Thread.Sleep(500);

      if (SetupHelper.DotNet35SExists())
        dotNetIndicator.Image = Resources.tick;
      else
      {
        _bPrerequisitesMet = false;
        lnkNET.Text = "Download .NET Framework 3.5";
        dotNetIndicator.Image = Resources.cross;
      }

      Application.DoEvents();
      Thread.Sleep(500);

      if (SetupHelper.FlashActiveXExists())
        flashIndicator.Image = Resources.tick;
      else
      {
        _bPrerequisitesMet = false;
        lnkFlash.Text = "Download Adobe Flash";
        flashIndicator.Image = Resources.cross;
      }

      Application.DoEvents();
      Thread.Sleep(500);

      if (SetupHelper.QuickTimeRightVersionExists())
        quickTimeIndicator.Image = Resources.tick;
      else
      {
        _bPrerequisitesMet = false;
        lnkQuickTime.Text = "Download Quicktime";
        quickTimeIndicator.Image = Resources.cross;
      }

      Application.DoEvents();
      Thread.Sleep(500);

      if (SetupHelper.WindowsMediaPlayerRightVersionExists())
        wmpIndicator.Image = Resources.tick;
      else
      {
        _bPrerequisitesMet = false;
        lnkWMP.Text = "Download Windows Media Player";
        wmpIndicator.Image = Resources.cross;
      }

      if (_bPrerequisitesMet)
      {
        btnNext.Enabled = true;
        lblStatus.Text = "Required hardware and software are installed and Oxigen can run on this computer.";
      }
      else
        lblStatus.Text = "Your computer does not meet all of the specified requirements to run Oxigen. Installation will now abort.";   
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      SetupHelper.ExitNoChanges();
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
      if (AppDataSingleton.Instance.Repair)
      {
        SetupHelper.OpenForm<OxigenExistsForm>(this);
        return;
      }

      SetupHelper.OpenForm<TermsAndConditionsForm>(this);
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      SetupHelper.OpenForm<ExistingUserPromptForm>(this);
    }

    private void linkNET_Clicked(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.microsoft.com/downloads/details.aspx?FamilyID=9cfb2d51-5ff4-4491-b0e5-b386f32c0992");

      Application.Exit();
    }

    private void linkFlash_Clicked(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://get.adobe.com/flashplayer/");

      Application.Exit();
    }

    private void linkQT_Clicked(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.apple.com/quicktime/download/");

      Application.Exit();
    }

    private void linkWMP_Clicked(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.microsoft.com/windows/windowsmedia/default.mspx");

      Application.Exit();
    }    
  }
}
