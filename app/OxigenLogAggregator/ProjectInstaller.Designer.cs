namespace OxigenLogAggregator
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
      this.logAggregatorServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
      this.logAggregatorServiceInstaller = new System.ServiceProcess.ServiceInstaller();
      // 
      // logAggregatorServiceProcessInstaller
      // 
      this.logAggregatorServiceProcessInstaller.Password = null;
      this.logAggregatorServiceProcessInstaller.Username = null;
      // 
      // logAggregatorServiceInstaller
      // 
      this.logAggregatorServiceInstaller.Description = "Checks for new client uploaded log files and aggregates them.";
      this.logAggregatorServiceInstaller.ServiceName = "Oxigen Log Aggregator";
      this.logAggregatorServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
      // 
      // ProjectInstaller
      // 
      this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.logAggregatorServiceProcessInstaller,
            this.logAggregatorServiceInstaller});

    }

    #endregion

    private System.ServiceProcess.ServiceProcessInstaller logAggregatorServiceProcessInstaller;
    private System.ServiceProcess.ServiceInstaller logAggregatorServiceInstaller;
  }
}