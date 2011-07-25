using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.ContentExchanger;

namespace Oxigen.Tests32
{
    public class IOExceptionTempToPermFileMover : ITempToPermFileMover
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
                    File.Copy(tempLocalPath, permLocalPath);
                    throw new IOException("Deliberate Exception");
                }
                catch (Exception)
                {
                    if (File.Exists(tempLocalPath))
                        File.Delete(tempLocalPath);

                    throw;
                }
            }
        }

        private string GetPermLocalPath(string tempLocalPath)
        {
            return tempLocalPath.Substring(0, tempLocalPath.Length - TEMP_FILE_SUFFIX.Length);
        }
    }
}
