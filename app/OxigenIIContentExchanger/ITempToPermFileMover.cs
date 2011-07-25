using System;
using System.IO;

namespace OxigenIIAdvertising.ContentExchanger
{
    public interface ITempToPermFileMover
    {
        string TempFileSuffix { get; }
        void TryMoveFromTempToPerm(string tempLocalPath);
    }

    public class TempToPermFileMover : ITempToPermFileMover
    {
        private const string TEMP_FILE_SUFFIX = "1";

        public string TempFileSuffix
        {
            get { return TEMP_FILE_SUFFIX; }
        }

        public void TryMoveFromTempToPerm(string tempLocalPath)
        {
            if (File.Exists(tempLocalPath) && (new FileInfo(tempLocalPath)).Length > 0)
            {
                string permLocalPath = GetPermLocalPath(tempLocalPath);

                try
                {
                    File.Copy(tempLocalPath, permLocalPath, true);
                    File.Delete(tempLocalPath);
                }
                catch (Exception)
                {
                    if (File.Exists(tempLocalPath))
                        File.Delete(tempLocalPath);

                    throw;
                }
            }
        }

        protected string GetPermLocalPath(string tempLocalPath)
        {
            return tempLocalPath.Substring(0, tempLocalPath.Length - TEMP_FILE_SUFFIX.Length);
        }
    }
}
