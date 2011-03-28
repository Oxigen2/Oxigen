using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OxigenIIAdvertising.BLClients;
using OxigenIIAdvertising.SOAStructures;
using System.Text.RegularExpressions;

namespace OxigenIIPresentation
{
  public partial class EditDetails : System.Web.UI.Page
  {
    private Regex _emailRegex = new Regex(@"^(([^<>()[\]\\.,;:\s@\""]+(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", RegexOptions.Compiled);

    protected void Page_Load(object sender, EventArgs e)
    {
      if (Session["User"] == null)
        Response.Redirect("Home.aspx");

      BLClient client = null;
      List<LocationNameValue> countries = null;
      List<LocationNameValue> states = null;
      List<LocationNameValue> townCities = null;
      List<KeyValuePair<int, string>> occupationSectors = null;
      List<KeyValuePair<int, string>> employmentLevels = null;
      List<KeyValuePair<int, string>> annualHouseholdIncomes = null;

      UserDetails userDetails = null;

      if (!IsPostBack)
      {
        try
        {
          client = new BLClient();

          userDetails = client.GetUserDetails(((User)Session["User"]).UserID);

          // geographical dropdowns
          countries = Helper.AddToFrontOfDropDown<LocationNameValue>(client.GetCountries(), new LocationNameValue("Please select one ¬", -1, true));
          states = Helper.AddToFrontOfDropDown<LocationNameValue>(client.GetChildGeoTTNodes(userDetails.CountryID), new LocationNameValue("Please select one ¬", -1, true));

          if (HasChildren(states))
            townCities = Helper.AddToFrontOfDropDown<LocationNameValue>(client.GetChildGeoTTNodes(userDetails.StateID), new LocationNameValue("Please select one ¬", -1, false));
          else
          {
            townCities = states;
            states = null;
          }

          // Socio-economic status dropdowns
          occupationSectors = Helper.AddToFrontOfDropDown<KeyValuePair<int, string>>(client.GetSocioEconomicStatuses("O"), new KeyValuePair<int, string>(-1, "Please Select One ¬"));
          employmentLevels = Helper.AddToFrontOfDropDown<KeyValuePair<int, string>>(client.GetSocioEconomicStatuses("L"), new KeyValuePair<int, string>(-1, "Please Select One ¬"));
          annualHouseholdIncomes = Helper.AddToFrontOfDropDown<KeyValuePair<int, string>>(client.GetSocioEconomicStatuses("I"), new KeyValuePair<int, string>(-1, "Please Select One ¬"));
        }
        finally
        {
          if (client != null)
            client.Dispose();
        }

        ddlCountry.DataSource = countries;
        ddlCountry.DataTextField = "Name";
        ddlCountry.DataValueField = "Value";
        ddlCountry.DataBind();

        if (states != null)
        {
          StatePanel.Visible = true;
          ddlState.DataSource = states;
          ddlState.DataTextField = "Name";
          ddlState.DataValueField = "Value";
          ddlState.DataBind();
        }
        else
          StatePanel.Visible = false;

        ddlTownCities.DataSource = townCities;
        ddlTownCities.DataTextField = "Name";
        ddlTownCities.DataValueField = "Value";
        ddlTownCities.DataBind();

        ddlSector.DataSource = occupationSectors;
        ddlSector.DataTextField = "Value";
        ddlSector.DataValueField = "Key"; 
        ddlSector.DataBind();

        ddlLevel.DataSource = employmentLevels;
        ddlLevel.DataTextField = "Value";
        ddlLevel.DataValueField = "Key";
        ddlLevel.DataBind();

        ddlIncome.DataSource = annualHouseholdIncomes;
        ddlIncome.DataTextField = "Value";
        ddlIncome.DataValueField = "Key"; 
        ddlIncome.DataBind();

        PopulateDOBDropDowns();

        // populate user settings
        txtEmail.Text = userDetails.EmailAddress;
        txtFirstName.Text = userDetails.FirstName;
        txtLastName.Text = userDetails.LastName;
        rbGenderMale.Checked = userDetails.Gender == "male";
        rbGenderFemale.Checked = userDetails.Gender == "female";
        dobDay.SelectedValue = userDetails.Dob.Day.ToString();
        dobMonth.SelectedValue = userDetails.Dob.Month.ToString();
        dobYear.SelectedValue = userDetails.Dob.Year.ToString();
        ddlCountry.SelectedValue = userDetails.CountryID.ToString();
        ddlState.SelectedValue = userDetails.StateID.ToString();
        ddlTownCities.SelectedValue = userDetails.TownCityID.ToString();
        ddlSector.SelectedValue = userDetails.OccupationSectorID.ToString();
        ddlLevel.SelectedValue = userDetails.EmploymentLevelID.ToString();
        ddlIncome.SelectedValue = userDetails.AnnualHouseholdIncomeID.ToString();
      }
    }

    private void PopulateDOBDropDowns()
    {
      dobDay.Items.Add(new ListItem("¬", "¬"));

      for (int i = 0; i <= 31; i++)
        dobDay.Items.Add(new ListItem(i.ToString(), i.ToString()));

      FillMonthDropDown(dobMonth);

      dobYear.Items.Add(new ListItem("¬", "¬"));

      for (int i = 1930; i < DateTime.Now.Year - 10; i++)
        dobYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
    }

    private static void FillMonthDropDown(DropDownList ddMonth)
    {
      ddMonth.Items.Add(new ListItem("¬", "-1"));
      ddMonth.Items.Add(new ListItem("January", "1"));
      ddMonth.Items.Add(new ListItem("February", "2"));
      ddMonth.Items.Add(new ListItem("March", "3"));
      ddMonth.Items.Add(new ListItem("April", "4"));
      ddMonth.Items.Add(new ListItem("May", "5"));
      ddMonth.Items.Add(new ListItem("June", "6"));
      ddMonth.Items.Add(new ListItem("July", "7"));
      ddMonth.Items.Add(new ListItem("August", "8"));
      ddMonth.Items.Add(new ListItem("September", "9"));
      ddMonth.Items.Add(new ListItem("October", "10"));
      ddMonth.Items.Add(new ListItem("November", "11"));
      ddMonth.Items.Add(new ListItem("December", "12"));
    }

    public void Countries_SelectedIndexChanged(object sender, EventArgs e)
    {
      BLClient client = null;
      List<LocationNameValue> states = null;
      List<LocationNameValue> townCities = null;

      try
      {
        client = new BLClient();

        states = Helper.AddToFrontOfDropDown<LocationNameValue>(client.GetChildGeoTTNodes(int.Parse(((DropDownList)sender).SelectedValue)), new LocationNameValue("Please select one ¬", -1, true));

        if (HasChildren(states))
          townCities = Helper.AddToFrontOfDropDown<LocationNameValue>(new List<LocationNameValue>(), new LocationNameValue("Please select one ¬", -1, false));
        else
        {
          townCities = states;
          states = null;
        }
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      if (states != null)
      {
        StatePanel.Visible = true;
        ddlState.DataSource = states;
        ddlState.DataTextField = "Name";
        ddlState.DataValueField = "Value";
        ddlState.DataBind();
      }
      else
        StatePanel.Visible = false;

      ddlTownCities.DataSource = townCities;
      ddlTownCities.DataTextField = "Name";
      ddlTownCities.DataValueField = "Value";
      ddlTownCities.DataBind();
    }

    public void States_SelectedIndexChanged(object sender, EventArgs e)
    {
      BLClient client = null;
      List<LocationNameValue> townCities = null;

      try
      {
        client = new BLClient();

        townCities = Helper.AddToFrontOfDropDown<LocationNameValue>(client.GetChildGeoTTNodes(int.Parse(((DropDownList)sender).SelectedValue)), new LocationNameValue("Please select one ¬", -1, true));
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }      

      ddlTownCities.DataSource = townCities;
      ddlTownCities.DataTextField = "Name";
      ddlTownCities.DataValueField = "Value";
      ddlTownCities.DataBind();
    }

    // convenient way to ascertain if the items in a Location-related drop-down list have children in the
    // geocraphical taxonomy tree. All items' HasChildren value is the same, so check only the first.
    private bool HasChildren(List<LocationNameValue> states)
    {
      if (states == null)
        return false;

      return ((LocationNameValue)states[1]).HasChildren;
    }

    public void AsyncPostbackError(object sender, AsyncPostBackErrorEventArgs e)
    {
      scm.AsyncPostBackErrorMessage = e.Exception.Message;
    }

    public void Next_Click(object sender, EventArgs e)
    {
      string password1 = txtPassword1.Text.Trim();
      string password2 = txtPassword2.Text.Trim();
      int townCityID = -1;
      int occupationSectorID = -1;
      int employmentLevelID = -1;
      int annualHouseholdIncomeID = -1;
      string gender = null;

      DateTime dob;

      if (!_emailRegex.IsMatch(txtEmail.Text))
      {
        ValidationMessage.Text = "Please enter a valid e-mail address.";
        return;
      }
      else
        ValidationMessage.Text = "";

      if (password1 != password2)
      {
        ValidationMessage.Text = "Passwords do not match";
        return;
      }
      else
        ValidationMessage.Text = "";

      if (!rbGenderFemale.Checked && !rbGenderMale.Checked)
      {
        ValidationMessage.Text = "Please select your gender";
        return;
      }
      else
      {
        gender = rbGenderFemale.Checked ? "female" : "male";
        ValidationMessage.Text = "";
      }

      try
      {
        dob = new DateTime(int.Parse(dobYear.SelectedValue), int.Parse(dobMonth.SelectedValue), int.Parse(dobDay.SelectedValue));
        ValidationMessage.Text = "";
      }
      catch
      {
        ValidationMessage.Text = "Please enter a valid Date of Birth";
        return;
      }
      
      if (ddlCountry.SelectedValue == "-1")
      {
        ValidationMessage.Text = "Please select your country";
        return;
      }
      else
        ValidationMessage.Text = "";

      if (ddlState.Visible && ddlState.SelectedValue == "-1")
      {
        ValidationMessage.Text = "Please select your state";
        return;
      }
      else
        ValidationMessage.Text = "";

      if (!int.TryParse(ddlTownCities.SelectedValue, out townCityID) ||  ddlTownCities.SelectedValue == "-1")
      {
        ValidationMessage.Text = "Please select your town or city";
        return;
      }
      else
        ValidationMessage.Text = "";

      if (!int.TryParse(ddlSector.SelectedValue, out occupationSectorID) || ddlSector.SelectedValue == "-1")
      {
        ValidationMessage.Text = "Please select your occupation sector";
        return;
      }
      else
        ValidationMessage.Text = "";

      if (!int.TryParse(ddlLevel.SelectedValue, out employmentLevelID) || ddlLevel.SelectedValue == "-1")
      {
        ValidationMessage.Text = "Please select your employment level";
        return;
      }
      else
        ValidationMessage.Text = "";

      if (!int.TryParse(ddlIncome.SelectedValue, out annualHouseholdIncomeID) || ddlIncome.SelectedValue == "-1")
      {
        ValidationMessage.Text = "Please select your Annual Household Income";
        return;
      }
      else
        ValidationMessage.Text = "";

      // all inputs are ok

      bool bEmailExists = false;
      BLClient client = null;

      try
      {
        client = new BLClient();

        bEmailExists = client.EditUserDetails(((User)Session["User"]).UserID, txtEmail.Text, password1, txtFirstName.Text, txtLastName.Text,
          gender, dob, townCityID, occupationSectorID, employmentLevelID, annualHouseholdIncomeID);
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      if (bEmailExists)
      {
        ValidationMessage.Text = "This e-mail address already exists on our system.";
        return;
      }
      else
        ValidationMessage.Text = "";

      Response.Redirect("Home.aspx");
    }

    public void DeleteAccount_Click(object sender, EventArgs e)
    {
      BLClient client = null;

      bool bHasActivePCs = false;

      try
      {
        client = new BLClient();

        bHasActivePCs = client.RemoveUserAccount(((User)Session["User"]).UserID);
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      if (bHasActivePCs)
      {
        ValidationMessage.Text = "Please uninstall your software before deleting your account.";
        return;
      }

      // deletion successful
      Session.Remove("User");
      Response.Redirect("Home.aspx");
    }
  }
}
