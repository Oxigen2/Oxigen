namespace OxigenScreenSaverProductAggregator
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
      this.currentScreenSaverProductAggregatorServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
      this.currentScreenSaverProductAggregatorServiceInstaller = new System.ServiceProcess.ServiceInstaller();
      // 
      // currentScreenSaverProductAggregatorServiceProcessInstaller
      // 
      this.currentScreenSaverProductAggregatorServiceProcessInstaller.Password = null;
      this.currentScreenSaverProductAggregatorServiceProcessInstaller.Username = null;
      // 
      // currentScreenSaverProductAggregatorServiceInstaller
      // 
      this.currentScreenSaverProductAggregatorServiceInstaller.Description = "Checks for user uploaded current screen saver product information and aggregates " +
          "it";
      this.currentScreenSaverProductAggregatorServiceInstaller.ServiceName = "Oxigen Current Screen Saver Product Aggregator";
      this.currentScreenSaverProductAggregatorServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
      // 
      // ProjectInstaller
      // 
      this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.currentScreenSaverProductAggregatorServiceProcessInstaller,
            this.currentScreenSaverProductAggregatorServiceInstaller});

    }

    #endregion

    private System.ServiceProcess.ServiceProcessInstaller currentScreenSaverProductAggregatorServiceProcessInstaller;
    private System.ServiceProcess.ServiceInstaller currentScreenSaverProductAggregatorServiceInstaller;
  }
}