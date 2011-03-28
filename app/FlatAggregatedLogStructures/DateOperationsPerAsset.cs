using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxigenIIAdvertising.FlatAggregatedLogStructures
{
  /// <summary>
  /// Represents an aggregated click/log per date per asset.
  /// In a single file, the pair of OperationDateTime and NoOperations should be unique
  /// </summary>
  public struct DateOperationsPerAsset
  {
    private DateTime _operationDate;
    private long _assetID;
    private AssetType _assetType;
    private int _noOperations;

    /// <summary>
    /// Date the operation occurred. Time is irrelevant.
    /// </summary>
    public DateTime OperationDate
    {
      get { return _operationDate; }
      set { _operationDate = value; }
    }

    /// <summary>
    /// The unique database ID of the asset that was clicked or viewed.
    /// </summary>
    public long AssetID
    {
      get { return _assetID; }
      set { _assetID = value; }
    }

    /// <summary>
    /// Asset is an advert or a content
    /// </summary>
    public AssetType AssetType
    {
      get { return _assetType; }
      set { _assetType = value; }
    }

    /// <summary>
    /// Number of clicks or impressions for that asset in the particular operation date.
    /// </summary>
    public int NoOperations
    {
      get { return _noOperations; }
      set { _noOperations = value; }
    }
  }
}
