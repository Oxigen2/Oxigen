using System;
using System.Windows.Forms;
using Setup.ClientLoggers;

namespace Setup
{
  public partial class RegistrationForm2 : SetupForm
  {
    public RegistrationForm2()
    {
      InitializeComponent();

      SetupHelper.SetupAgeGenderControls(rbMale, rbFemale, ddDay, ddMonth, ddYear);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      SetupHelper.ExitConfirmNoChanges();
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
      SetupHelper.OpenForm<RegistrationForm1>(this);
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(txtFirstName.Text.Trim()))
      {
        MessageBox.Show("Please enter your first name");
        return;
      }

      if (string.IsNullOrEmpty(txtLastName.Text.Trim()))
      {
        MessageBox.Show("Please enter your last name");
        return;
      }

      if (!rbMale.Checked && !rbFemale.Checked)
      {
        MessageBox.Show("Please select your Gender");
        return;
      }

      if (!SetupHelper.IsDateValid(ddDay, ddMonth, ddYear))
      {
        MessageBox.Show("Please enter your Date of Birth");
        return;
      }

      SetupHelper.SaveAgeGender(rbMale, ddDay, ddMonth, ddYear);

      AppDataSingleton.Instance.FirstName = txtFirstName.Text.Trim();
      AppDataSingleton.Instance.LastName = txtLastName.Text.Trim();
      
      SetupHelper.OpenForm<RegistrationForm3>(this);
    }

    private void ddYear_Click(object sender, EventArgs e)
    {
      if (((ComboBox)sender).SelectedIndex == 0)
        ((ComboBox)sender).SelectedItem = 1985;
    }

    private void Form_Shown(object sender, EventArgs e)
    {
      ClientLogger logger = new PersistentClientLogger();
      logger.Log("5.6-Registration2");
    }
  }
}
