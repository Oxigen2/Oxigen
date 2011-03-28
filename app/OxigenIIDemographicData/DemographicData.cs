using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OxigenIIAdvertising.Exceptions;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.Demographic
{  
  /// <summary>
  /// Class to hold demographic data of the user(s) in the client machine
  /// </summary>
  [Serializable]
  [DataContract]
  public class DemographicData
  {
    private string _geoDefinition;
    private int _minAge = -1;
    private int _maxAge = -1;
    private string[] _gender;
    private string[] _socioEconomicgroup;

    /// <summary>
    /// The end user's position in the Geographical Taxonomy Tree
    /// </summary>
    [DataMember]
    public string GeoDefinition
    {
      get { return _geoDefinition; }
      set { _geoDefinition = value; }
    }

    /// <summary>
    /// Minimum Age of the users in the client machine
    /// </summary>
    [DataMember]
    public int MinAge
    {
      get { return _minAge; }

      set 
      {
        if (_maxAge < _minAge && _minAge != -1 && _maxAge != -1)
          throw new ArgumentException("MinAge cannot be greater than MaxAge");

        _minAge = value; 
      }
    }

    /// <summary>
    /// Maximum Age of the users in the client machine
    /// </summary>
    [DataMember]
    public int MaxAge
    {
      get { return _maxAge; }

      set 
      {
        if (_maxAge < _minAge && _minAge != -1 && _maxAge != -1)
          throw new ArgumentException("MaxAge cannot be less than MinAge");

        _maxAge = value; 
      }
    }

    /// <summary>
    /// Genders of the users in the client machine
    /// </summary>
    [DataMember]
    public string[] Gender
    {
      get { return _gender; }
      set { _gender = value; }
    }

    /// <summary>
    /// Socioeconomic groups of the users in the client machine
    /// </summary>
    [DataMember]
    public string[] SocioEconomicgroup
    {
      get { return _socioEconomicgroup; }
      set { _socioEconomicgroup = value; }
    }
  }
}
