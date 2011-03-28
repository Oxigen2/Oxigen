namespace OxigenUninstallInfoAggregator
{
  partial class ProjectInstaller
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.uninstallInfoAggregatorServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
      this.uninstallInfoAggregatorServiceInstaller = new System.ServiceProcess.ServiceInstaller();
      // 
      // uninstallInfoAggregatorServiceProcessInstaller
      // 
      this.uninstallInfoAggregatorServiceProcessInstaller.Password = null;
      this.uninstallInfoAggregatorServiceProcessInstaller.Username = null;
      // 
      // uninstallInfoAggregatorServiceInstaller
      // 
      this.uninstallInfoAggregatorServiceInstaller.Description = "Checks for user uploaded software uninstall info and aggregates it";
      this.uninstallInfoAggregatorServiceInstaller.ServiceName = "Oxigen Uninstall Info Aggregator";
      this.uninstallInfoAggregatorServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
      // 
      // ProjectInstaller
      // 
      this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.uninstallInfoAggregatorServiceProcessInstaller,
            this.uninstallInfoAggregatorServiceInstaller});

    }

    #endregion

    private System.ServiceProcess.ServiceProcessInstaller uninstallInfoAggregatorServiceProcessInstaller;
    private System.ServiceProcess.ServiceInstaller uninstallInfoAggregatorServiceInstaller;
  }
}