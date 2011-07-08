using System;
using System.Windows.Forms;
using Setup.ClientLoggers;

namespace Setup
{
    public partial class PrerequisitesForm : SetupForm
    {
        public PrerequisitesForm()
        {
            InitializeComponent();

            lnkFlash.Text = "";
            lnkNET.Text = "";
            lnkQuickTime.Text = "";
            lnkWMP.Text = "";
        }


        private void PrerequisitesForm_Load(object sender, EventArgs e)
        {
            ClientLogger logger = new PersistentClientLogger();
            logger.Log("3-Prerequisites");

            InstallationPrerequisiteCollection prerequisites = new InstallationPrerequisiteCollection();

            prerequisites.Add(new DotNet35Prerequisite(lnkNET, dotNetIndicator));
            prerequisites.Add(new FlashActiveXPerequisite(lnkFlash, flashIndicator));
            prerequisites.Add(new QTPrerequisite(lnkQuickTime, quickTimeIndicator));
            prerequisites.Add(new RAMPrerequisite(ramIndicator));
            prerequisites.Add(new WMPPrerequisite(lnkWMP, wmpIndicator));

            prerequisites.CheckAllPrerequisites();

            AppDataSingleton.Instance.PrerequisitesFullyMet = prerequisites.PrerequisitesFullyMet;

            if (prerequisites.PrerequisitesFullyMet)
            {
                SetupHelper.OpenForm<ExistingUserPromptForm>(this);
                return;
            }

            prerequisites.LogNotMetPrerequisites(logger);
            prerequisites.SetVisualIndicators();

            if (prerequisites.CanContinueWithInstallation)
            {
                lblStatus.Text = "It is recommended that you install some optional components before you continue.";
                btnNext.Enabled = true;
            }
            else
            {
                lblStatus.Text = "Your computer does not meet all of the specified requirements to run Oxigen. Installation will now abort.";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetupHelper.ExitConfirmNoChanges();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (AppDataSingleton.Instance.Repair)
            {
                SetupHelper.OpenForm<OxigenExistsForm>(this);
                return;
            }

            SetupHelper.OpenForm<WelcomeForm>(this);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            SetupHelper.OpenForm<ExistingUserPromptForm>(this);
        }

        private void linkNET_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.microsoft.com/downloads/en/details.aspx?FamilyID=ab99342f-5d1a-413d-8319-81da479ab0d7");

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
