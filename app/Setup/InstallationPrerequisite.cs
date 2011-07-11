using System.Windows.Forms;
using Setup.Properties;

namespace Setup
{
    public abstract class InstallationPrerequisite
    {
        protected LinkLabel _linkLabel;
        private PictureBox _pictureBox;
        protected bool _isMandatory;
        protected string _logMessage;
        protected PrerequisiteStatus _prerequisiteStatus;

        protected InstallationPrerequisite(PictureBox pictureBox)
        {
            _pictureBox = pictureBox;
        }

        protected InstallationPrerequisite(LinkLabel linkLabel, PictureBox pictureBox)
        {
            _linkLabel = linkLabel;
            _pictureBox = pictureBox;
        }

        public LinkLabel LinkLabel
        {
            get { return _linkLabel; }
        }

        public PictureBox PictureBox
        {
            get { return _pictureBox; }
        }

        public bool IsMandatory
        {
            get { return _isMandatory; }
        }

        public string LogMessage
        {
            get { return _logMessage; }
        }

        public PrerequisiteStatus PrerequisiteStatus
        {
            get { return _prerequisiteStatus; }
        }

        public void SetAmberQuestionMark()
        {
            _pictureBox.Image = Resources.questionmark;
        }

        public void SetGreenCheck()
        {
            _pictureBox.Image = Resources.tick;
        }

        public void SetRedCross()
        {
            _pictureBox.Image = Resources.cross;
        }

        public void HideDownloadLink()
        {
            if (_linkLabel != null)
            _linkLabel.Visible = false;
        }

        public abstract PrerequisiteStatus GetPrerequisiteStatus(IInstallationPrerequisiteProvider installationPrerequisiteProvider);
    }

    public class WMPPrerequisite : InstallationPrerequisite
    {
        public WMPPrerequisite(LinkLabel linkLabel, PictureBox pictureBox)
            : base(linkLabel, pictureBox)
        {
            _isMandatory = false;
            _linkLabel.Text = "Download Windows Media Player";
            _logMessage = "Missing_WindowsMediaPlayer";
        }

        public override PrerequisiteStatus GetPrerequisiteStatus(IInstallationPrerequisiteProvider installationPrerequisiteProvider)
        {
            _prerequisiteStatus = installationPrerequisiteProvider.WMPPrerequisiteStatus;
            return _prerequisiteStatus;
        }
    }

    public class DotNet35Prerequisite : InstallationPrerequisite
    {
        public DotNet35Prerequisite(LinkLabel linkLabel, PictureBox pictureBox)
            : base(linkLabel, pictureBox)
        {
            _isMandatory = true;
            _linkLabel.Text = "Download .NET Framework 3.5";
            _logMessage = "Missing_NET35";
        }

        public override PrerequisiteStatus GetPrerequisiteStatus(IInstallationPrerequisiteProvider installationPrerequisiteProvider)
        {
            _prerequisiteStatus = installationPrerequisiteProvider.DotNet35PrerequisiteStatus;
            return _prerequisiteStatus;
        }
    }

    public class QTPrerequisite : InstallationPrerequisite
    {
        public QTPrerequisite(LinkLabel linkLabel, PictureBox pictureBox)
            : base(linkLabel, pictureBox)
        {
            _isMandatory = false;
            _linkLabel.Text = "Download QuickTime";
            _logMessage = "Missing_Quicktime";
        }

        public override PrerequisiteStatus GetPrerequisiteStatus(IInstallationPrerequisiteProvider installationPrerequisiteProvider)
        {
            _prerequisiteStatus = installationPrerequisiteProvider.QTPrerequisiteStatus;
            return _prerequisiteStatus;
        }
    }

    public class RAMPrerequisite : InstallationPrerequisite
    {
        public RAMPrerequisite(PictureBox pictureBox)
            : base(pictureBox)
        {
            _isMandatory = true;
            _logMessage = "Missing_EnoughRam";
        }

        public override PrerequisiteStatus GetPrerequisiteStatus(IInstallationPrerequisiteProvider installationPrerequisiteProvider)
        {
            _prerequisiteStatus =  installationPrerequisiteProvider.RamPrerequisiteStatus;
            return _prerequisiteStatus;
        }
    }

    public class FlashActiveXPerequisite : InstallationPrerequisite
    {
        public FlashActiveXPerequisite(LinkLabel linkLabel, PictureBox pictureBox)
            : base(linkLabel, pictureBox)
        {
            _isMandatory = true;
            _linkLabel.Text = "Download Adobe Flash";
            _logMessage = "Missing_Flash";
        }

        public override PrerequisiteStatus GetPrerequisiteStatus(IInstallationPrerequisiteProvider installationPrerequisiteProvider)
        {
            _prerequisiteStatus = installationPrerequisiteProvider.FlashActiveXPrerequisiteStatus;
            return _prerequisiteStatus;
        }
    }

    public enum PrerequisiteStatus
    {
        Exists,
        BetweenMandatoryAndRecommended,
        DoesNotExist
    }
}
