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
            logger.Log("2-Prerequisites");

            IInstallationPrerequisiteProvider installationPrerequisiteProvider;
            
            if (!AppDataSingleton.Instance.DebugMode)
                installationPrerequisiteProvider = new RealInstallationPrerequisiteProvider();
            else
            {
                installationPrerequisiteProvider = new MockInstallationPrerequisiteProvider()
                                                   {
                                                       DotNet35PrerequisiteStatus = PrerequisiteStatus.Exists,
                                                       FlashActiveXPrerequisiteStatus = PrerequisiteStatus.Exists,
                                                       QTPrerequisiteStatus = PrerequisiteStatus.DoesNotExist,
                                                       RamPrerequisiteStatus = PrerequisiteStatus.Exists,
                                                       WMPPrerequisiteStatus = PrerequisiteStatus.DoesNotExist
                                                   };
            }
            InstallationPrerequisiteCollection prerequisites = new InstallationPrerequisiteCollection(installationPrerequisiteProvider);

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
                lblStatus.Text = "To enjoy Oxigen to the maximum, it is recommended that you install the optional components above.";
                btnNext.Enabled = true;
            }
            else
            {
                lblStatus.Text = "Please follow the links above to install the required software.";
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
            System.Diagnostics.Process.Start("http://fpdownload.macromedia.com/get/flashplayer/current/install_flash_player_ax.exe");

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
