using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration.Install;
using System.ComponentModel;

namespace Prerequisites
{
  [RunInstaller(true)]
  public class PrerequisiteInstaller : Installer
  {
    public PrerequisiteInstaller()
      : base()
    {
    }

    public override void Install(System.Collections.IDictionary stateSaver)
    {
      base.Install(stateSaver);

      PrerequisiteForm form = new PrerequisiteForm();
      form.ShowDialog();

      bool bPrerequisitesFail = form.PrerequisitesFail;

      form.Dispose();

      if (bPrerequisitesFail)
        this.Rollback(stateSaver);
    }
  }
}
