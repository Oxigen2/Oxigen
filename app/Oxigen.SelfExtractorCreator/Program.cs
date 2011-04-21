using System;
using System.Collections.Generic;
using System.Text;
using Ionic.Zip;

namespace Oxigen.SelfExtractorCreator
{
    class Program
    {
        static void Main(string[] args) {
            var convertedPCName = args[0];
            var tempInstallersPathTemp = args[1];

            using (ZipFile zf = new ZipFile()) {
                zf.AddDirectory(tempInstallersPathTemp);

                SelfExtractorSaveOptions options = new SelfExtractorSaveOptions();
                options.Copyright = "Oxigen";
                options.DefaultExtractDirectory = "%TEMP%\\Oxigen";
                options.Flavor = SelfExtractorFlavor.ConsoleApplication;
                options.ProductName = "Oxigen";
                options.Quiet = true;
                options.RemoveUnpackedFilesAfterExecute = true;
                options.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
                options.PostExtractCommandLine = "%TEMP%\\Oxigen\\Setup.exe";
                zf.SaveSelfExtractor(tempInstallersPathTemp + "\\" + convertedPCName + ".exe", options);
            }

        }
    }
}
