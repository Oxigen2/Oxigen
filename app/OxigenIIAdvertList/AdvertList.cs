using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.AppData;
using OxigenIIAdvertising.InclusionExclusionRules;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.AppData
{
  /// <summary>
  /// Object to hold Advert assets. Maps to ss_adcond_data.dat
  /// </summary>
  [Serializable]
  public class AdvertList
  {
    private HashSet<AdvertAsset> _adverts;

    /// <summary>
    /// HashSet that holds the Advert Asset objects
    /// </summary>
    public HashSet<AdvertAsset> Adverts
    {
      get { return _adverts; }
      set { _adverts = value; }
    }

    /// <summary>
    /// Instantiating an AdvertList object will instantiate the _adverts HashSet that will hold
    /// the underlying AdvertAsset objects
    /// </summary>
    public AdvertList()
    {
      _adverts = new HashSet<AdvertAsset>();
    }
  }
}
