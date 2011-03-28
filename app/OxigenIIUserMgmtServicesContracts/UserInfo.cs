using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIUserMgmtServicesService
{
  [DataContract]
  public class UserInfo
  {
    private string _firstName; 
    private string _lastName;
    private string _gender;
    private DateTime _dob;
    private string _country;
    private string _state;
    private string _townCity;
    private string _occupationSector;
    private string _employmentLevel;
    private string _annualHouseholdIncome;

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
      get
      {
        return _gender;
      }
      set
      {
        _gender = value;
      }
    }

    [DataMember]
    public DateTime DOB
    {
      get
      {
        return _dob;
      }
      set
      {
        _dob = value;
      }
    }

    [DataMember]
    public string Country
    {
      get
      {

        return _country;
      }
      set
      {
        _country = value;
      }
    }

    [DataMember]
    public string State
    {
      get
      {
        return _state;
      }
      set
      {
        _state = value;
      }
    }

    [DataMember]
    public string TownCity
    {
      get
      {
        return _townCity;
      }
      set
      {
        _townCity = value;
      }
    }

    [DataMember]
    public string OccupationSector
    {
      get
      {
        return _occupationSector;
      }
      set
      {
        _occupationSector = value;
      }
    }

    [DataMember]
    public string EmploymentLevel
    {
      get
      {
        return _employmentLevel;
      }
      set
      {
        _employmentLevel = value;
      }
    }

    [DataMember]
    public string AnnualHouseholdIncome
    {
      get
      {
        return _annualHouseholdIncome;
      }
      set
      {
        _annualHouseholdIncome = value;
      }
    }
  }
}
