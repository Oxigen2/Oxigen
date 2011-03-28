using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxigenIIAdvertising.UserInfo
{
  
  public class DemographicData
  {
    private int m_intAge;
    private Gender m_Gender;
    private string m_strSocioEconomicGroup;
    private string m_strCountry;
    private string m_strTown;

    public int Age
    {
      get { return m_intAge; }
      set { m_intAge = value; }
    }
    
    public Gender Gender
    {
      get { return m_Gender; }
      set { m_Gender = value; }
    }
    
    public string SocioEconomicGroup
    {
      get { return m_strSocioEconomicGroup; }
      set { m_strSocioEconomicGroup = value; }
    }
    
    public string Country
    {
      get { return m_strCountry; }
      set { m_strCountry = value; }
    }
    
    public string Town
    {
      get { return m_strTown; }
      set { m_strTown = value; }
    }
  }
    
  public enum Gender
  {
    Male,
    Female
  }
}
