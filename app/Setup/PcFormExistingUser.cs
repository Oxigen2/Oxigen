using System;
using System.Windows.Forms;
using Setup.ClientLoggers;
using Setup.UserManagementServicesLive;

namespace Setup
{
  public partial class PcFormExistingUser : SetupForm
  {
    private object _lockObj = new object();
    SimpleErrorWrapper _wrapper = null;

    public PcFormExistingUser()
    {
      InitializeComponent();
    }

    private void PCListForm_Load(object sender, EventArgs e)
    {
      Setup.UserManagementServicesLive.PcInfo[] pcs = null;  

      if (string.IsNullOrEmpty(AppDataSingleton.Instance.NewPCName))
        txtPCName.Text = Environment.MachineName;
      else
        txtPCName.Text = AppDataSingleton.Instance.NewPCName;
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      string pcName = txtPCName.Text.Trim();

      if (string.IsNullOrEmpty(pcName))
      {
        MessageBox.Show("Please enter a name for your PC", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        return;
      }

      AppDataSingleton.Instance.NewPCName = pcName;
      AppDataSingleton.Instance.NewPC = true;    

      AppDataSingleton.Instance.User.MachineGUID = System.Guid.NewGuid().ToString().ToUpper() + "_" + SetupHelper.GetRandomLetter();
            
      if (AppDataSingleton.Instance.FileDetectedSubscriptionsFound)
      {
        AppDataSingleton.Instance.ChannelSubscriptionsToUpload.SubscriptionSet = SetupHelper.GetChannelSubscriptionsNetFromLocal(AppDataSingleton.Instance.FileDetectedChannelSubscriptionsLocal.SubscriptionSet);

        SetupHelper.OpenForm<MergeChannelsForm>(this);
        return;
      }      

      SetupHelper.OpenForm<UpdateExistingUserDetailsForm>(this);
    }
    
    private void btnCancel_Click(object sender, EventArgs e)
    {
      SetupHelper.ExitNoChanges();
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
      if (AppDataSingleton.Instance.Repair)
      {
        SetupHelper.OpenForm<PrerequisitesForm>(this);
        return;
      }

      SetupHelper.OpenForm<CredentialsForm>(this);
    }

    private void Form_Shown(object sender, EventArgs e)
    {
      ClientLogger logger = new PersistentClientLogger();
      logger.Log("5.2-PCFormExistingUser");
    }
  }
}
