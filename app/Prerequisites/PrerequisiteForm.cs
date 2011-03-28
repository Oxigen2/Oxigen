using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prerequisites
{
  public partial class PrerequisiteForm : Form
  {
    private bool _bPrerequisitesFail = false;

    public PrerequisiteForm()
    {
      InitializeComponent();
    }

    public bool PrerequisitesFail
    {
      get
      {
        return _bPrerequisitesFail;
      }
    }

    private void PrerequisiteForm_Load(object sender, EventArgs e)
    {
      if (!IsQuickTimeInstalled())
        qtReq.Text = "Cross";
      else
        qtReq.Text = "Tick";

      if (!IsFlashInstalled())
        flashReq.Text = "Cross";
      else
        flashReq.Text = "Tick";

      if (!IsWindowsMediaPlayerInstalled())
        wmpReq.Text = "Cross";
      else
        wmpReq.Text = "Tick";

      if (_bPrerequisitesFail)
      {
        btnNext.Enabled = false;
        btnCancel.Text = "Exit";
      }
    }

    private bool IsQuickTimeInstalled()
    {
      return true;
    }

    private bool IsFlashInstalled()
    {
      return true;
    }

    private bool IsWindowsMediaPlayerInstalled()
    {
      return true;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      string statusMsg;

      if (_bPrerequisitesFail)
        statusMsg = "Prerequisites have not been met.";
      else
        statusMsg = "You have chosen to exit the installation.";

      MessageBox.Show(statusMsg + "\r\nYour System has not been modified.", "Message");

      this.Close();
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
