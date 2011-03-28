using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace OxigenSU
{
  public partial class VerboseModeForm : Form
  {
    const int MF_BYPOSITION = 0x400;

    [DllImport("User32")]
    private static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);

    [DllImport("User32")]
    private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

    [DllImport("User32")]
    private static extern int GetMenuItemCount(IntPtr hWnd);

    private ComponentListRetriever _retriever;
    private string _appDataPath = null;

    public ComponentListRetriever Retriever
    {
      get { return _retriever; }
    }

    public VerboseModeForm(string appDataPath)
    {
      InitializeComponent();

      IntPtr hMenu = GetSystemMenu(this.Handle, false);
      int menuItemCount = GetMenuItemCount(hMenu);
      RemoveMenu(hMenu, menuItemCount - 1, MF_BYPOSITION);

      _retriever = new ComponentListRetriever();
      _appDataPath = appDataPath;
    }

    private void VerboseRunForm_Shown(object sender, EventArgs e)
    {
      Application.DoEvents();

      backgroundWorker.RunWorkerAsync();
    }

    private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      _retriever.Retrieve();
    }

    private void backgroundWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
    {
      if (_retriever.ChangedComponents == null || _retriever.ChangedComponents.Count == 0)
      {
        MessageBox.Show("No updates available at this time.", "Message", MessageBoxButtons.OK);

        Application.Exit();
      }
      else
      {
        this.Hide();
        UpdatePrompter prompter = new UpdatePrompter(_retriever, _appDataPath);
        prompter.PromptForUpdateIfExists();
      }
    }
  }
}
