using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxigenIIAdvertising.ContentExchanger
{
  public class ExchangeStatus
  {
    private bool _bLowDiskSpace = false;
    private bool _bLowAssetSpace = false;
    private bool _bContentDownloaded = false;
    private bool _bExitWithError = false;

    public bool LowDiskSpace
    {
      get { return _bLowDiskSpace; }
      set { _bLowDiskSpace = value; }
    }
    
    public bool LowAssetSpace
    {
      get { return _bLowAssetSpace; }
      set { _bLowAssetSpace = value; }
    }

    public bool ContentDownloaded
    {
      get { return _bContentDownloaded; }
      set { _bContentDownloaded = value; }
    }

    public bool ExitWithError
    {
      get { return _bExitWithError; }
      set { _bExitWithError = value; }
    }
  }

  public struct ProcessStatus
  {
    private float _overallProgress;
    private float _taskProgress;
    private string _taskMessage;

    public float OverallProgress
    {
      get { return _overallProgress; }
      set { _overallProgress = value; }
    }

    public float TaskProgress
    {
      get { return _taskProgress; }
      set { _taskProgress = value; }
    }

    public string TaskMessage
    {
      get { return _taskMessage; }
      set { _taskMessage = value; }
    }

    public ProcessStatus(float taskProgress, float overallProgress, string taskMessage)
      : this()
    {
      _taskProgress = taskProgress;
      _overallProgress = overallProgress;
      _taskMessage = taskMessage;
    }
  }
}
