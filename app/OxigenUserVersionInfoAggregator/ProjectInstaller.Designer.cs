namespace OxigenUserVersionInfoAggregator
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
      this.userVersionInfoProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
      this.userVersionInfoServiceProcessInstaller = new System.ServiceProcess.ServiceInstaller();
      // 
      // userVersionInfoProcessInstaller
      // 
      this.userVersionInfoProcessInstaller.Password = null;
      this.userVersionInfoProcessInstaller.Username = null;
      // 
      // userVersionInfoServiceProcessInstaller
      // 
      this.userVersionInfoServiceProcessInstaller.Description = "Checks for user uploaded software version information and aggregates it";
      this.userVersionInfoServiceProcessInstaller.ServiceName = "Oxigen User Version Info Aggregator";
      this.userVersionInfoServiceProcessInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
      // 
      // ProjectInstaller
      // 
      this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.userVersionInfoProcessInstaller,
            this.userVersionInfoServiceProcessInstaller});

    }

    #endregion

    private System.ServiceProcess.ServiceProcessInstaller userVersionInfoProcessInstaller;
    private System.ServiceProcess.ServiceInstaller userVersionInfoServiceProcessInstaller;
  }
}