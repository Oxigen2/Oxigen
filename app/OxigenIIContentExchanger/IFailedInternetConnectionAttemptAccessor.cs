namespace OxigenIIAdvertising.ContentExchanger
{
    public interface IFailedInternetConnectionAttemptAccessor
    {
        void RecordFailedAttempt();
        int GetFailedAttempts();
        void ResetFailedAttempts();
    }
}