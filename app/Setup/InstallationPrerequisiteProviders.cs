using System;

namespace Setup
{
    public interface IInstallationPrerequisiteProvider
    {
        PrerequisiteStatus RamPrerequisiteStatus { get; }
        PrerequisiteStatus DotNet35PrerequisiteStatus { get; }
        PrerequisiteStatus FlashActiveXPrerequisiteStatus { get; }
        PrerequisiteStatus WMPPrerequisiteStatus { get; }
        PrerequisiteStatus QTPrerequisiteStatus { get; }
    }

    public class RealInstallationPrerequisiteProvider : IInstallationPrerequisiteProvider
    {
        public PrerequisiteStatus RamPrerequisiteStatus
        {
            get 
            {
                RAMStatus status = SetupHelper.IsRamSufficient();

                if (status == RAMStatus.EqualOrAboveRecommended)
                    return PrerequisiteStatus.Exists;
                
                if (status == RAMStatus.BetweenMandatoryAndRecommended)
                    return PrerequisiteStatus.BetweenMandatoryAndRecommended;
                
                return PrerequisiteStatus.DoesNotExist;
            }
        }

        public PrerequisiteStatus DotNet35PrerequisiteStatus
        {
            get
            {
                if (SetupHelper.DotNet35Exists())
                    return PrerequisiteStatus.Exists;

                return PrerequisiteStatus.DoesNotExist;
            }
        }

        public PrerequisiteStatus FlashActiveXPrerequisiteStatus
        {
            get
            {
                if (SetupHelper.FlashActiveXExists())
                    return PrerequisiteStatus.Exists;

                return PrerequisiteStatus.DoesNotExist;
            }
        }

        public PrerequisiteStatus WMPPrerequisiteStatus
        {
            get
            {
                if (SetupHelper.WindowsMediaPlayerRightVersionExists())
                    return PrerequisiteStatus.Exists;

                return PrerequisiteStatus.DoesNotExist;
            }
        }

        public PrerequisiteStatus QTPrerequisiteStatus
        {
            get
            {
                if (SetupHelper.QuickTimeRightVersionExists())
                    return PrerequisiteStatus.Exists;

                return PrerequisiteStatus.DoesNotExist;
            }
        }
    }

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
