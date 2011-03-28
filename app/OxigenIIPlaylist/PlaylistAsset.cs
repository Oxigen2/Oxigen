using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OxigenIIAdvertising.EncryptionDecryption;

namespace OxigenIIAdvertising.AppData
{
  /// <summary>
  /// Playable asset seen by the screensaver application
  /// </summary>
  public abstract class PlaylistAsset
  {
    protected long _assetID;
    protected PlayerType _playerType;
    protected string _assetFilename;
    protected float _displayLength;
    protected string _clickDestination;
    protected string _assetWebSite;
    protected string[] _scheduleInfo;
    protected DateTime _startDateTime;
    protected DateTime _endDateTime;

    /// <summary>
    /// The unique ID of the asset
    /// </summary>
    public long AssetID
    {
      get { return _assetID; }
      set { _assetID = value; }
    }

    /// <summary>
    /// The type of the player to play the asset
    /// </summary>
    public PlayerType PlayerType
    {
      get { return _playerType; }
      set { _playerType = value; }
    }

    /// <summary>
    /// the file name of the asset
    /// </summary>
    public string AssetFilename
    {
      get { return _assetFilename; }
      set { _assetFilename = value; }
    }

    /// <summary>
    /// The display length of the asset, in seconds
    /// </summary>
    public float DisplayLength
    {
      get { return _displayLength; }
      set { _displayLength = value; }
    }

    /// <summary>
    /// The click-through URL to take the user to if asset is clicked on when on display
    /// </summary>
    public string ClickDestination
    {
      get { return _clickDestination; }
      set { _clickDestination = value; }
    }

    /// <summary>
    /// Temporal scheduling info
    /// </summary>
    public string[] ScheduleInfo
    {
      get { return _scheduleInfo; }
      set { _scheduleInfo = value; }
    }

    /// <summary>
    /// Gets or sets the lower date and time the asset can be shown
    /// </summary>
    public DateTime StartDateTime
    {
      get { return _startDateTime; }
      set { _startDateTime = value; }
    }

    /// <summary>
    /// Gets or sets the upper date and time the asset must be shown
    /// </summary>
    public DateTime EndDateTime
    {
      get { return _endDateTime; }
      set { _endDateTime = value; }
    }

    /// <summary>
    /// Gets or sets the web site to show on the screen saver for web assets
    /// </summary>
    public string AssetWebSite
    {
      get { return _assetWebSite; }
      set { _assetWebSite = value; }
    }

    public PlaylistAsset() { }

    /// <summary>
    /// Decrypts an asset file from disk using the specified decryption password
    /// </summary>
    /// <param name="assetPath">path of the asset file to decrypt</param>
    /// <param name="decryptionPassword">the password to use for decryption</param>
    /// <returns>a MemoryStream with the decrypted file</returns>
    /// <exception cref="System.Security.Cryptography.CryptographicException">Encrypted file is corrupted</exception>
    /// <exception cref="PathTooLongException"></exception>
    /// <exception cref="DirectoryNotFoundException"></exception>
    /// <exception cref="IOException"></exception>
    /// <exception cref="UnauthorizedAccessException"></exception>
    /// <exception cref="FileNotFoundException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="System.Security.SecurityException"></exception>
    public MemoryStream DecryptAssetFile(string assetPath, string decryptionPassword)
    {
      byte[] encryptedBuffer = File.ReadAllBytes(assetPath);

      byte[] decryptedBuffer = Cryptography.Decrypt(encryptedBuffer, decryptionPassword);

      return new MemoryStream(decryptedBuffer);
    }

    public string GetAssetFilenameGUIDSuffix()
    {
      return _assetFilename.Substring(_assetFilename.LastIndexOf("_") + 1, 1);
    }
  }
}
