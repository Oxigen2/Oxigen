namespace Oxigen.DurationDetectors
{
    public interface IFileDurationDetector
    {
        double GetDurationInSeconds(string path);
    }
}