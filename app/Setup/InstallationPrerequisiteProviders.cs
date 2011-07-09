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
}
