using Setup;

namespace Oxigen.Tests32
{
    public class MockInstallationPrerequisiteProvider : IInstallationPrerequisiteProvider
    {
        private PrerequisiteStatus _ramPrerequisiteStatus;
        private PrerequisiteStatus _dotNet35PrerequisiteStatus;
        private PrerequisiteStatus _flashActiveXPrerequisiteStatus;
        private PrerequisiteStatus _wmpPrerequisiteStatus;
        private PrerequisiteStatus _qtPrerequisiteStatus;

        public PrerequisiteStatus RamPrerequisiteStatus
        {
            get { return _ramPrerequisiteStatus; }
            set { _ramPrerequisiteStatus = value; }
        }

        public PrerequisiteStatus DotNet35PrerequisiteStatus
        {
            get { return _dotNet35PrerequisiteStatus; }
            set { _dotNet35PrerequisiteStatus = value; }
        }

        public PrerequisiteStatus FlashActiveXPrerequisiteStatus
        {
            get { return _flashActiveXPrerequisiteStatus; }
            set { _flashActiveXPrerequisiteStatus = value; }
        }

        public PrerequisiteStatus WMPPrerequisiteStatus
        {
            get { return _wmpPrerequisiteStatus; }
            set { _wmpPrerequisiteStatus = value; }
        }

        public PrerequisiteStatus QTPrerequisiteStatus
        {
            get { return _qtPrerequisiteStatus; }
            set { _qtPrerequisiteStatus = value; }
        }
    }
}
