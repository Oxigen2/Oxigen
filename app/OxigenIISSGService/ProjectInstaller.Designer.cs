namespace OxigenIIAdvertising.SSGService
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
      this.oxigenServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
      this.oxigenServiceInstaller = new System.ServiceProcess.ServiceInstaller();
      // 
      // oxigenServiceProcessInstaller
      // 
      this.oxigenServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
      this.oxigenServiceProcessInstaller.Password = null;
      this.oxigenServiceProcessInstaller.Username = null;
      // 
      // oxigenServiceInstaller
      // 
      this.oxigenServiceInstaller.Description = "Monitors the machine\'s Oxigen Installation for updates and upgrades";
      this.oxigenServiceInstaller.ServiceName = "Oxigen Service";
      this.oxigenServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
      // 
      // ProjectInstaller
      // 
      this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.oxigenServiceProcessInstaller,
            this.oxigenServiceInstaller});

    }

    #endregion

    private System.ServiceProcess.ServiceProcessInstaller oxigenServiceProcessInstaller;
    private System.ServiceProcess.ServiceInstaller oxigenServiceInstaller;
  }
}