using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Setup.ClientLoggers;

namespace Setup
{
  public partial class RegistrationForm3 : SetupForm
  {
    private bool _bDropDownsSet = false;

    public RegistrationForm3()
    {
      InitializeComponent();

      SetupHelper.SetupGeoDropDowns(ddCountry, ddState, ddTownCity);

      if (!string.IsNullOrEmpty(AppDataSingleton.Instance.Country))
        SetupHelper.SetupCountryStateTownCity(ddCountry, ddState, ddTownCity);

      _bDropDownsSet = true;
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
      if (SetupHelper.IsGeoValid(ddCountry, ddState, ddTownCity))
        SetupHelper.SaveGeo(ddCountry, ddState, ddTownCity);

      SetupHelper.OpenForm<RegistrationForm2>(this);
    }

    private void ddCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (!_bDropDownsSet)
        return;

      SetupHelper.SetStateTownCityDropDownsByCountry((ComboBox)sender, ddState, ddTownCity, lblState);
    }

    private void ddState_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (!_bDropDownsSet)
        return;

      SetupHelper.SetTownCityDropDownByState(ddCountry, (ComboBox)sender, ddTownCity);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      SetupHelper.ExitConfirmNoChanges();
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      if (!SetupHelper.IsGeoValid(ddCountry, ddState, ddTownCity))
      {
        MessageBox.Show("Please select your full Location.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }

      SetupHelper.SaveGeo(ddCountry, ddState, ddTownCity);

      SetupHelper.OpenForm<RegistrationForm4>(this);
    }

    private void Form_Shown(object sender, EventArgs e)
    {
      ClientLogger logger = new PersistentClientLogger();
      logger.Log("5.7-Registration3");
    }
  }
}
