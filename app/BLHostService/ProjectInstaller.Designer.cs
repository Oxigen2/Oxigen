namespace BLHostService
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
        this.serviceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
        this.serviceInstaller = new System.ServiceProcess.ServiceInstaller();
        // 
        // serviceProcessInstaller
        // 
        this.serviceProcessInstaller.Password = null;
        this.serviceProcessInstaller.Username = null;
        // 
        // serviceInstaller
        // 
        this.serviceInstaller.Description = "Hosts the named-pipes Business Logic Service for the Oxigen websites";
        this.serviceInstaller.DisplayName = "Oxigen Business Logic Host";
        this.serviceInstaller.ServiceName = "Oxigen Business Logic Host";
        this.serviceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
        this.serviceInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceInstaller_AfterInstall);
        // 
        // ProjectInstaller
        // 
        this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller,
            this.serviceInstaller});

    }

    #endregion

    private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller;
    private System.ServiceProcess.ServiceInstaller serviceInstaller;
  }
}