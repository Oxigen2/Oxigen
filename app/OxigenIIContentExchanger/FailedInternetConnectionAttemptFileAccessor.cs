using System;
using System.Configuration;
using System.IO;

namespace OxigenIIAdvertising.ContentExchanger
{
    public class FailedInternetConnectionAttemptFileAccessor : IFailedInternetConnectionAttemptAccessor
    {
        public static class Config
        {
            public static Func<string> FailedInternetConnectionAttemptsFilePath = () => ConfigurationManager.AppSettings["AppDataPath"] + "SettingsData\\fca.dat";
        }
        
        public void RecordFailedAttempt()
        {
            if (!File.Exists(Config.FailedInternetConnectionAttemptsFilePath()))
            {
                File.WriteAllText(Config.FailedInternetConnectionAttemptsFilePath(), "1");
                return;
            }

            int noFailedAttempts = GetFailedAttempts();
            noFailedAttempts++;

            File.WriteAllText(Config.FailedInternetConnectionAttemptsFilePath(), noFailedAttempts.ToString());
        }

        public int GetFailedAttempts()
        {
            if (!File.Exists(Config.FailedInternetConnectionAttemptsFilePath()))
                return 0;

            string input = File.ReadAllText(Config.FailedInternetConnectionAttemptsFilePath());

            int noFailedAttempts;

            if (!int.TryParse(input, out noFailedAttempts))
            {
                File.Delete(Config.FailedInternetConnectionAttemptsFilePath());
                return 0;
            }

            return noFailedAttempts;
        }

        public void ResetFailedAttempts()
        {
            if (File.Exists(Config.FailedInternetConnectionAttemptsFilePath()))
                File.Delete(Config.FailedInternetConnectionAttemptsFilePath());
        }
    }
}
