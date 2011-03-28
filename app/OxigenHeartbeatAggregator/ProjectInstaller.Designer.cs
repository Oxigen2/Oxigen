namespace OxigenHeartbeatAggregator
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
      this.heartbeatAggregatorServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
      this.heartbeatAggregatorServiceInstaller = new System.ServiceProcess.ServiceInstaller();
      // 
      // heartbeatAggregatorServiceProcessInstaller
      // 
      this.heartbeatAggregatorServiceProcessInstaller.Password = null;
      this.heartbeatAggregatorServiceProcessInstaller.Username = null;
      // 
      // heartbeatAggregatorServiceInstaller
      // 
      this.heartbeatAggregatorServiceInstaller.Description = "Checks for user uploaded heartbeat data and aggregates it";
      this.heartbeatAggregatorServiceInstaller.ServiceName = "Oxigen Heartbeat Aggregator";
      this.heartbeatAggregatorServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
      // 
      // ProjectInstaller
      // 
      this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.heartbeatAggregatorServiceProcessInstaller,
            this.heartbeatAggregatorServiceInstaller});

    }

    #endregion

    private System.ServiceProcess.ServiceProcessInstaller heartbeatAggregatorServiceProcessInstaller;
    private System.ServiceProcess.ServiceInstaller heartbeatAggregatorServiceInstaller;
  }
}