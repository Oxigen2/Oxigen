using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxigenIIAdvertising.LogStats
{
  /// <summary>
  /// For an asset, it holds information on either impressions per date or clicks per date
  /// </summary>
  public class DateOperationsPerAsset
  {
    private DateTime _operationDateTime;
    private List<OperationsPerDate> _operationsPerDateTime;

    /// <summary>
    /// Date of Operation. According to the business logic, this contains either day, month and year alone.
    /// </summary>
    public DateTime OperationDateTime
    {
      get { return _operationDateTime; }
      set { _operationDateTime = value; }
    }

    /// <summary>
    /// Impressions or Clicks per Date for this Asset
    /// </summary>
    public List<OperationsPerDate> OperationsPerDateTime
    {
      get { return _operationsPerDateTime; }
      set { _operationsPerDateTime = value; }
    }

    /// <summary>
    /// constructor for DateOperationsPerAsset
    /// </summary>
    public DateOperationsPerAsset() 
    {
      _operationsPerDateTime = new List<OperationsPerDate>();
    }
  }
}
