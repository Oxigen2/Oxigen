using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxigenIIAdvertising.LogStats
{
  /// <summary>
  /// Clicks or Impressions of an asset per Date
  /// </summary>
  public class OperationsPerDate
  {
    private long _assetID;
    private int _noOperations;

    /// <summary>
    /// The Unique ID of the Asset
    /// </summary>
    public long AssetID
    {
      get { return _assetID; }
      set { _assetID = value; }
    }

    /// <summary>
    /// Number of click or impressions per OperationDate
    /// </summary>
    public int NoOperations
    {
      get { return _noOperations; }
      set { _noOperations = value; }
    }

    /// <summary>
    /// Constructor for OperationsPerDate
    /// </summary>
    public OperationsPerDate() { }
  }
}
