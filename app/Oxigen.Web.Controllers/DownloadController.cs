using System.Diagnostics;
using System.IO;
using System.Web.Mvc;
using Oxigen.Core.Installer;
using Oxigen.Core.Logger;
using Oxigen.Core.RepositoryInterfaces;
using SharpArch.Core;


namespace Oxigen.Web.Controllers
{
    [HandleError]
    public class DownloadController : Controller
    {
        private ILogEntryRepository logEntryRepository;

        public DownloadController(ILogEntryRepository logEntryRepository) {
            Check.Require(logEntryRepository != null, "logEntryRepository may not be null");

            this.logEntryRepository = logEntryRepository;
        }

        public ActionResult Installer(InstallerSetup subscription)
        {
            // create Custom dir
            string rootInstallersPath = System.Configuration.ConfigurationSettings.AppSettings["tempInstallersPath"];
            string installersPath = rootInstallersPath + subscription.FolderName + "\\";
            string exeName = subscription.ExtractorFileName + ".exe";
            string filePath = installersPath + exeName;

            if (!Directory.Exists(installersPath))
                Directory.CreateDirectory(installersPath);
            if (!System.IO.File.Exists(filePath))
            {
                // Create custom Setup.ini file

                System.IO.File.WriteAllText(installersPath + "Setup.ini", subscription.GetSetupText());
                System.IO.File.Copy(rootInstallersPath + "Setup.exe", installersPath + "Setup.exe");
                System.IO.File.Copy(rootInstallersPath + "Oxigen.msi", installersPath + "Oxigen.msi");


                RunProcessAndWaitForExit(
                    System.Web.HttpContext.Current.Request.MapPath(
                        System.Web.HttpContext.Current.Request.ApplicationPath) + "Bin\\Oxigen.SelfExtractorCreator.exe",
                    subscription.ExtractorFileName + " \"" + installersPath + "\\\"");
                // sign the self-extractor
                RunProcessAndWaitForExit(System.Configuration.ConfigurationSettings.AppSettings["signToolPath"],
                                         System.Configuration.ConfigurationSettings.AppSettings["signToolArguments"] +
                                         "\"" + filePath + "\" >> " +
                                         System.Configuration.ConfigurationSettings.AppSettings["debugPath"]);
                System.IO.File.Delete(installersPath + "Setup.ini");
                System.IO.File.Delete(installersPath + "Setup.exe");
                System.IO.File.Delete(installersPath + "Oxigen.msi");
            }

            var logEntry = new LogEntry("Installer Download") {
                UserRef = Session.SessionID,
                Message = subscription.FolderName,
                IpAddress = Request.ServerVariables["REMOTE_ADDR"]
            };
            logEntryRepository.SaveOrUpdate(logEntry);
            return File(filePath, "application/octet-stream", exeName);
        }

        private static void RunProcessAndWaitForExit(string fileName, string arguments) {
            var startInfo = new ProcessStartInfo(fileName, arguments);
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            var process = Process.Start(startInfo);
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();
            //throw new Exception(process.ExitCode.ToString() + error + arguments);
        }
    }
}
