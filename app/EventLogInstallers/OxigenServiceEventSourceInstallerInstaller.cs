using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Configuration.Install;
using System.ComponentModel;

namespace EventLogInstallers
{
  [RunInstaller(true)]
  public class OxigenServiceEventSourceInstallerInstaller : Installer
  {
    public const string EventSource = "Oxigen Service";

    public OxigenServiceEventSourceInstallerInstaller()
    {
      EventLogInstaller eventLogInstaller = new EventLogInstaller();
      eventLogInstaller.Source = EventSource;
      Installers.Add(eventLogInstaller);
    }
  }
}
