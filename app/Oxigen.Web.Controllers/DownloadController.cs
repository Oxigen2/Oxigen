using System;
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

            if (!Directory.Exists(installersPath))
            {
                var tempInstallPath = rootInstallersPath + Guid.NewGuid() + "\\";
                Directory.CreateDirectory(tempInstallPath);
            
                // Create custom Setup.ini file

                System.IO.File.WriteAllText(tempInstallPath + "Setup.ini", subscription.GetSetupText());
                System.IO.File.Copy(rootInstallersPath + "Setup.exe", tempInstallPath + "Setup.exe");
                System.IO.File.Copy(rootInstallersPath + "Oxigen.msi", tempInstallPath + "Oxigen.msi");


                RunProcessAndWaitForExit(
                    System.Web.HttpContext.Current.Request.MapPath(
                        System.Web.HttpContext.Current.Request.ApplicationPath) + "Bin\\Oxigen.SelfExtractorCreator.exe",
                    subscription.ExtractorFileName + " \"" + tempInstallPath + "\\\"");
                // sign the self-extractor
                RunProcessAndWaitForExit(System.Configuration.ConfigurationSettings.AppSettings["signToolPath"],
                                         System.Configuration.ConfigurationSettings.AppSettings["signToolArguments"] +
                                         "\"" + tempInstallPath + exeName + "\" >> " +
                                         System.Configuration.ConfigurationSettings.AppSettings["debugPath"]);
                System.IO.File.Delete(tempInstallPath + "Setup.ini");
                System.IO.File.Delete(tempInstallPath + "Setup.exe");
                System.IO.File.Delete(tempInstallPath + "Oxigen.msi");

                try
                {
                    Directory.Move(tempInstallPath, installersPath);
                }
                catch (IOException)
                {
                    //file must have been just created by a different request so just delete the temp folder
                    System.IO.File.Delete(tempInstallPath + exeName);
                    Directory.Delete(tempInstallPath);
                }

            }

            var logEntry = new LogEntry("Installer Download") {
                UserRef = Session.SessionID,
                Message = subscription.FolderName,
                IpAddress = Request.ServerVariables["REMOTE_ADDR"]
            };
            logEntryRepository.SaveOrUpdate(logEntry);
            return File(installersPath + exeName, "application/octet-stream", exeName);
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
