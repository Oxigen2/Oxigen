using System.Collections.ObjectModel;
using Setup.ClientLoggers;

namespace Setup
{
    public class InstallationPrerequisiteCollection
    {
        private readonly Collection<InstallationPrerequisite> _prerequisites = new Collection<InstallationPrerequisite>();
        private bool _prerequisitesFullyMet = true;
        private bool _canContinueWithInstallation = true;
        private IInstallationPrerequisiteProvider _installationPrerequisiteProvider;

        public InstallationPrerequisiteCollection(IInstallationPrerequisiteProvider installationPrerequisiteProvider)
        {
            _installationPrerequisiteProvider = installationPrerequisiteProvider;
        }

        public bool PrerequisitesFullyMet
        {
            get { return _prerequisitesFullyMet; }
        }

        public bool CanContinueWithInstallation
        {
            get { return _canContinueWithInstallation; }
        }

        public void Add(InstallationPrerequisite prerequisite)
        {
            _prerequisites.Add(prerequisite);
        }

        public void CheckAllPrerequisites()
        {
            foreach (InstallationPrerequisite prerequisite in _prerequisites)
            {
                PrerequisiteStatus status = prerequisite.GetPrerequisiteStatus(_installationPrerequisiteProvider);

                if (status == PrerequisiteStatus.Exists)
                    continue;
                
                if (status == PrerequisiteStatus.BetweenMandatoryAndRecommended)
                    _prerequisitesFullyMet = false;
                else if (status == PrerequisiteStatus.DoesNotExist && !prerequisite.IsMandatory)
                    _prerequisitesFullyMet = false;
                else
                {
                    _prerequisitesFullyMet = false;
                    _canContinueWithInstallation = false;
                }
            }
        }

        public void SetVisualIndicators()
        {
            foreach (InstallationPrerequisite prerequisite in _prerequisites)
            {
                if (prerequisite.PrerequisiteStatus == PrerequisiteStatus.Exists)
                {
                    prerequisite.SetGreenCheck();
                    prerequisite.HideDownloadLink();
                }
                else if (prerequisite.PrerequisiteStatus == PrerequisiteStatus.BetweenMandatoryAndRecommended)
                    prerequisite.SetAmberQuestionMark();
                else if (prerequisite.PrerequisiteStatus == PrerequisiteStatus.DoesNotExist && !prerequisite.IsMandatory)
                    prerequisite.SetAmberQuestionMark();
                else
                    prerequisite.SetRedCross();
            }
        }

        public void LogNotMetPrerequisites(ClientLogger logger)
        {
            foreach (InstallationPrerequisite prerequisite in _prerequisites)
            {
                if (prerequisite.PrerequisiteStatus == PrerequisiteStatus.DoesNotExist)
                    logger.Log(prerequisite.LogMessage);
            }
        }
    }
}