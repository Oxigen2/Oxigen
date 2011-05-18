using System.ServiceModel;
//TODO: possibly remove once CDN is implemented
namespace InterCommunicationStructures
{
  /// <summary>
  /// Web method parameter that holds asset information to be requested from the relay servers by a client
  /// </summary>
  [MessageContract]
  public class AssetFileParameterMessage : UserParameterMessage
  {
    private string _assetFileName;
    private AssetType _assetType;
    private string _checksum;

    /// <summary>
    /// Asset file name
    /// </summary>
    [MessageHeader]
    public string AssetFileName
    {
      get { return _assetFileName; }
      set { _assetFileName = value; }
    }

    /// <summary>
    /// Content or Advertisement
    /// </summary>
    [MessageHeader]
    public AssetType AssetType
    {
      get { return _assetType; }
      set { _assetType = value; }
    }

    /// <summary>
    /// Client side checksum of the file
    /// </summary>
    [MessageHeader]
    public string Checksum
    {
      get { return _checksum; }
      set { _checksum = value; }
    }
  }
}
