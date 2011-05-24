using System;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.Reflection;


namespace BLHostService
{
  [RunInstaller(true)]
  public partial class ProjectInstaller : Installer
  {
    public ProjectInstaller()
    {
      InitializeComponent();

      string serviceName = GetConfigurationValue("serviceName");

      serviceInstaller.ServiceName = serviceName;
      serviceInstaller.DisplayName = serviceName; 
    }

    private static string GetConfigurationValue(string key)
    {
      Assembly service = Assembly.GetAssembly(typeof(ProjectInstaller));
      Configuration config = ConfigurationManager.OpenExeConfiguration(service.Location);

      if (config.AppSettings.Settings[key] != null)
        return config.AppSettings.Settings[key].Value;

      throw new IndexOutOfRangeException("Settings collection does not contain the requested key: " + key);
    }
  }
}
