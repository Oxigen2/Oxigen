using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Setup.ClientLoggers;
using Setup.Properties;
using System.Threading;

namespace Setup
{
  public partial class RegistrationForm4 : SetupForm
  {
    public RegistrationForm4()
    {
      InitializeComponent();

      SetupHelper.SetupSocioEconomicDropDowns(ddOccupationSector, ddEmploymentLevel, ddAnnualHouseholdIncome);
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
      SetupHelper.SaveSocioEconomicStatus(ddOccupationSector, ddEmploymentLevel, ddAnnualHouseholdIncome);

      SetupHelper.OpenForm<RegistrationForm3>(this);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      SetupHelper.ExitNoChanges();
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      if (ddOccupationSector.SelectedIndex == 0)
      {
        MessageBox.Show("Please select your Occupational Sector", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }

      if (ddEmploymentLevel.SelectedIndex == 0)
      {
        MessageBox.Show("Please select your Employment Level", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }

      if (ddAnnualHouseholdIncome.SelectedIndex == 0)
      {
        MessageBox.Show("Please select your Annual Household Income", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }

      SetupHelper.SaveSocioEconomicStatus(ddOccupationSector, ddEmploymentLevel, ddAnnualHouseholdIncome);

      SetupHelper.OpenForm<PCFormNewUser>(this);     
    }

    private void Form_Shown(object sender, EventArgs e)
    {
      ClientLogger logger = new PersistentClientLogger();
      logger.Log("5.8-Registration4");
    }
  }
}
