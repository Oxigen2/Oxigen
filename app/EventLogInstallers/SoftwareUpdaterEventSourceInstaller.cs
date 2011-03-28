using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Configuration.Install;

namespace EventLogInstallers
{
  [RunInstaller(true)]
  public class SoftwareUpdaterEventSourceInstaller : Installer
  {
    public const string EventSource = "OxigenSU";

    public SoftwareUpdaterEventSourceInstaller()
    {
      EventLogInstaller eventLogInstaller = new EventLogInstaller();
      eventLogInstaller.Source = EventSource;
      Installers.Add(eventLogInstaller);
    }
  }
}
