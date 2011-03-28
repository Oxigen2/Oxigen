using System;
using System.Collections.Generic;
using System.Text;

namespace InstallCustomSteps.DuplicateLibrary
{
  /// <summary>
  /// Object structure for user settings
  /// </summary>
  public class User
  {
    private int _timeUntilProgramToRunMinutes;
    private string _programToRun;
    private int _softwareMajorVersionNumber;
    private int _softwareMinorVersionNumber;
    private TimeSpan _lastTimeDiff;
    private int _flashVolume;
    private int _videoVolume;
    private long _assetFolderSize;
    private bool _bMuteFlash;
    private bool _bMuteVideo;
    private bool _bCanUpdate;
    private string _userGUID;
    private string _machineGUID;
    private float _defaultDisplayDuration;

    /// <summary>
    /// Program to run after the time in TimeUntilProgramToTunMinutes
    /// </summary>
    public string ProgramToRun
    {
      get { return _programToRun; }
      set { _programToRun = value; }
    }

    /// <summary>
    /// After this amount of time in minutes, run program in ProgramToRun
    /// </summary>
    public int TimeUntilProgramToRunMinutes
    {
      get { return _timeUntilProgramToRunMinutes; }
      set { _timeUntilProgramToRunMinutes = value; }
    }

    /// <summary>
    /// Gets or sets the last observed time difference between the user's last recorded time and the Relay Server's time
    /// </summary>
    public TimeSpan LastTimeDiff
    {
      get { return _lastTimeDiff; }
      set { _lastTimeDiff = value; }
    }

    /// <summary>
    /// Flash volume from 1(highest) to 100(lowest)
    /// </summary>
    public int FlashVolume
    {
      get { return _flashVolume; }
      set { _flashVolume = value; }
    }

    /// <summary>
    /// Video volume
    /// </summary>
    public int VideoVolume
    {
      get { return _videoVolume; }
      set { _videoVolume = value; }
    }

    /// <summary>
    /// Maximum overall size of the files on the target asset folder
    /// </summary>
    public long AssetFolderSize
    {
      get { return _assetFolderSize; }
      set { _assetFolderSize = value; }
    }

    /// <summary>
    /// True to mute flash player's sound
    /// </summary>
    public bool MuteFlash
    {
      get { return _bMuteFlash; }
      set { _bMuteFlash = value; }
    }

    /// <summary>
    /// True to mute video player's sound
    /// </summary>
    public bool MuteVideo
    {
      get { return _bMuteVideo; }
      set { _bMuteVideo = value; }
    }

    /// <summary>
    /// User's GUID
    /// </summary>
    public string UserGUID
    {
      get { return _userGUID; }
      set { _userGUID = value; }
    }

    /// <summary>
    /// Machine's GUID
    /// </summary>
    public string MachineGUID
    {
      get { return _machineGUID; }
      set { _machineGUID = value; }
    }

    /// <summary>
    /// True if software updater can run with this distribution. You may want to set this to
    /// false in serialized User objects that are shipped to universities and other closed network systems.
    /// </summary>
    public bool CanUpdate
    {
      get { return _bCanUpdate; }
      set { _bCanUpdate = value; }
    }

    /// <summary>
    /// Gets the letter which suffixes the user's GUID
    /// </summary>
    /// <returns>The letter which suffixes the user's GUID</returns>
    public string GetUserGUIDSuffix()
    {
      return _userGUID.Substring(_userGUID.LastIndexOf("_") + 1, 1);
    }

    /// <summary>
    /// Gets or sets the default display duration for Assets in seconds
    /// </summary>
    public float DefaultDisplayDuration
    {
      get { return _defaultDisplayDuration; }
      set { _defaultDisplayDuration = value; }
    }

    /// <summary>
    /// Major Version Number of the installed software
    /// </summary>
    public int SoftwareMajorVersionNumber
    {
      get { return _softwareMajorVersionNumber; }
      set { _softwareMajorVersionNumber = value; }
    }

    /// <summary>
    /// Minor Version Number of the installed software
    /// </summary>
    public int SoftwareMinorVersionNumber
    {
      get { return _softwareMinorVersionNumber; }
      set { _softwareMinorVersionNumber = value; }
    }

    /// <summary>
    /// Gets the Major.Minor version of the installed software
    /// </summary>
    public string SoftwareVersion
    {
      get
      {
        return String.Format("{0}.{1}", _softwareMajorVersionNumber, _softwareMinorVersionNumber);
      }
    }

    /// <summary>
    /// Gets the letter which suffixes the machine's GUID
    /// </summary>
    /// <returns>The letter which suffixes the machine's GUID</returns>
    public string GetMachineGUIDSuffix()
    {
      return _machineGUID.Substring(_machineGUID.LastIndexOf("_") + 1, 1);
    }
  }
}
