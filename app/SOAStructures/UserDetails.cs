using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.SOAStructures
{
  [DataContract]
  public class UserDetails
  {
    private string _emailAddress;
    private string _userPassword;
    private string _firstName;
    private string _lastName;
    private string _gender;
    private DateTime _dob;
    private int _countryID;
    private int _stateID;
    private int _townCityID;
    private int _occupationSectorID;
    private int _employmentLevelID;
    private int _annualHouseholdIncomeID;

    [DataMember]
    public string EmailAddress
    {
      get { return _emailAddress; }
      set { _emailAddress = value; }
    }

    [DataMember]
    public string UserPassword
    {
      get { return _userPassword; }
      set { _userPassword = value; }
    }
    
    [DataMember]
    public string FirstName
    {
      get { return _firstName; }
      set { _firstName = value; }
    }

    [DataMember]
    public string LastName
    {
      get { return _lastName; }
      set { _lastName = value; }
    }

    [DataMember]
    public string Gender
    {
      get { return _gender; }
      set { _gender = value; }
    }

    [DataMember]
    public DateTime Dob
    {
      get { return _dob; }
      set { _dob = value; }
    }

    [DataMember]
    public int CountryID
    {
      get { return _countryID; }
      set { _countryID = value; }
    }

    [DataMember]
    public int StateID
    {
      get { return _stateID; }
      set { _stateID = value; }
    }

    [DataMember]
    public int TownCityID
    {
      get { return _townCityID; }
      set { _townCityID = value; }
    }

    [DataMember]
    public int OccupationSectorID
    {
      get { return _occupationSectorID; }
      set { _occupationSectorID = value; }
    }

    [DataMember]
    public int EmploymentLevelID
    {
      get { return _employmentLevelID; }
      set { _employmentLevelID = value; }
    }

    [DataMember]
    public int AnnualHouseholdIncomeID
    {
      get { return _annualHouseholdIncomeID; }
      set { _annualHouseholdIncomeID = value; }
    }
  }
}
