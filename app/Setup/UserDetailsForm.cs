using System;
using System.Windows.Forms;
using Setup.ClientLoggers;
using Setup.UserManagementServicesLive;

namespace Setup
{
  public partial class UserDetailsForm : SetupForm
  {
    bool _bGeoDropDownsSet = false;
    private object _lockObj = new object();
    private SimpleErrorWrapper _wrapper = null;

    public UserDetailsForm()
    {
      InitializeComponent();
    }

    private void UpdateExistingUserDetailsForm_Load(object sender, EventArgs e)
    {
      SetupHelper.SetupAgeGenderControls(rbMale, rbFemale, ddDay, ddMonth, ddYear);
      SetupHelper.SetupGeoDropDowns(ddCountry, ddState, ddTownCity);
      SetupHelper.SetupSocioEconomicDropDowns(ddOccupationSector, ddEmploymentLevel, ddAnnualHouseholdIncome);

      if (!AppDataSingleton.Instance.ExistingUserDetailsDataRetrieved)
      {
        SimpleErrorWrapper wrapper = UpdateDetailsWithExistingData();

        if (wrapper.ErrorStatus == ErrorStatus1.Failure)
        {
          MessageBox.Show(wrapper.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          _bGeoDropDownsSet = true;
          return;
        }

        if (!AppDataSingleton.Instance.UserInfoExists)
          lblSubtitle.Text = "Please enter your details.";

        AppDataSingleton.Instance.ExistingUserDetailsDataRetrieved = true;        
      }

      SetupHelper.SetFormControlsToDownloadedDetails(txtFirstName, txtLastName, rbMale, rbFemale, ddDay, ddMonth, ddYear,
        ddCountry, ddState, ddTownCity, ddOccupationSector, ddEmploymentLevel, ddAnnualHouseholdIncome);

      _bGeoDropDownsSet = true;
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
      AppDataSingleton.Instance.FirstName = txtFirstName.Text.Trim();
      AppDataSingleton.Instance.LastName = txtLastName.Text.Trim();

      if (SetupHelper.IsDateValid(ddDay, ddMonth, ddYear))
        SetupHelper.SaveAge(ddDay, ddMonth, ddYear);

      if (rbMale.Checked || rbFemale.Checked)
        SetupHelper.SaveGender(rbMale);
     
      if (SetupHelper.IsGeoValid(ddCountry, ddState, ddTownCity))
        SetupHelper.SaveGeo(ddCountry, ddState, ddTownCity);
      
      if (SetupHelper.SocioEconomicStatusValid(ddOccupationSector, ddEmploymentLevel, ddAnnualHouseholdIncome))
        SetupHelper.SaveSocioEconomicStatus(ddOccupationSector, ddEmploymentLevel, ddAnnualHouseholdIncome);
     
      SetupHelper.OpenForm<ExistingUserPromptForm>(this);
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(txtFirstName.Text.Trim()))
      {
        MessageBox.Show("Please enter your First Name", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }

      if (string.IsNullOrEmpty(txtLastName.Text.Trim()))
      {
        MessageBox.Show("Please enter your Last Name", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }

      if (!rbMale.Checked && !rbFemale.Checked)
      {
        MessageBox.Show("Please select your Gender", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }

      if (!SetupHelper.IsDateValid(ddDay, ddMonth, ddYear))
      {
        MessageBox.Show("Please select your Date of Birth", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }

      if (!SetupHelper.IsGeoValid(ddCountry, ddState, ddTownCity))
      {
        MessageBox.Show("Please select your Location", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }

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

      AppDataSingleton.Instance.FirstName = txtFirstName.Text;
      AppDataSingleton.Instance.LastName = txtLastName.Text;
      SetupHelper.SaveAgeGender(rbMale, ddDay, ddMonth, ddYear);
      SetupHelper.SaveGeo(ddCountry, ddState, ddTownCity);
      SetupHelper.SaveSocioEconomicStatus(ddOccupationSector, ddEmploymentLevel, ddAnnualHouseholdIncome);

      SetupHelper.OpenForm<PCNameStreamSubscriptionsForm>(this);     
    }

    private SimpleErrorWrapper UpdateDetailsWithExistingData()
    {
      UserManagementServicesLive.UserInfo userInfo = null;
      SimpleErrorWrapper wrapper = null;

      try
      {
          using (var client = new UserDataManagementClient())
          {
              wrapper = client.GetExistingUserDetails(AppDataSingleton.Instance.User.UserGUID,
                                                      AppDataSingleton.Instance.Password,
                                                      "password", out userInfo);
          }
      }
      catch (System.Net.WebException ex)
      {
        AppDataSingleton.Instance.SetupLogger.WriteError(ex);
        return SetupHelper.GetGenericErrorConnectingWrapper();
      }
   
      if (wrapper.ErrorStatus == ErrorStatus1.Failure)
        return wrapper;

      if (string.IsNullOrEmpty(userInfo.Gender))
        AppDataSingleton.Instance.UserInfoExists = false;
      else
        AppDataSingleton.Instance.UserInfoExists = true;

      AppDataSingleton.Instance.FirstName = userInfo.FirstName;
      AppDataSingleton.Instance.LastName = userInfo.LastName;
      AppDataSingleton.Instance.Gender = userInfo.Gender;
      AppDataSingleton.Instance.DOB = userInfo.DOB;
      AppDataSingleton.Instance.DOBDay = userInfo.DOB.Day;
      AppDataSingleton.Instance.DOBMonth = userInfo.DOB.Month;
      AppDataSingleton.Instance.DOBYear = userInfo.DOB.Year;
      AppDataSingleton.Instance.TownCity = userInfo.TownCity;
      AppDataSingleton.Instance.State = userInfo.State;
      AppDataSingleton.Instance.Country = userInfo.Country;
      AppDataSingleton.Instance.OccupationSector = userInfo.OccupationSector;
      AppDataSingleton.Instance.EmploymentLevel = userInfo.EmploymentLevel;
      AppDataSingleton.Instance.AnnualHouseholdIncome = userInfo.AnnualHouseholdIncome;

      return wrapper;
    }

    private void ddCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (!_bGeoDropDownsSet)
        return;

      SetupHelper.SetStateTownCityDropDownsByCountry((ComboBox)sender, ddState, ddTownCity, lblState);
    }

    private void ddState_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (!_bGeoDropDownsSet)
        return;

      SetupHelper.SetTownCityDropDownByState(ddCountry, (ComboBox)sender, ddTownCity);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      SetupHelper.ExitConfirmNoChanges();
    }

    private void ddYear_Click(object sender, EventArgs e)
    {
      if (((ComboBox)sender).SelectedIndex == 0)
        ((ComboBox)sender).SelectedItem = 1985;
    }

    private void Form_Shown(object sender, EventArgs e)
    {
      ClientLogger logger = new PersistentClientLogger();
      logger.Log("5-UserDetails");
    }
  }
}
