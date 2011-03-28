using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;

namespace EventLogInstallers
{
  [RunInstaller(true)]
  public class OxigenTrayEventSourceInstaller : Installer
  {
    public const string EventSource = "Oxigen Tray";

    public OxigenTrayEventSourceInstaller()
    {
      EventLogInstaller eventLogInstaller = new EventLogInstaller();
      eventLogInstaller.Source = EventSource;
      Installers.Add(eventLogInstaller);
    }
  }
}
