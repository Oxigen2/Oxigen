using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace OxigenIIAdvertising.UserInfo
{
  public class UserMain
  {
    private string m_strFullName;
    private string m_strEmailAddress;
    private DemographicData m_UserDemographicData;
        
    public string FullName
    {
      get { return m_strFullName; }
      set { m_strFullName = value; }
    }
    
    public string EmailAddress
    {
      get { return m_strEmailAddress; }
      set { m_strEmailAddress = value; }
    }
    
    public DemographicData UserDemographicData
    {
      get { return m_UserDemographicData; }
      set { m_UserDemographicData = value; }
    }
  }
}
