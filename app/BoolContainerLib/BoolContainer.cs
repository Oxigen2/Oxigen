using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoolContainerLib
{
  /// <summary>
  /// Acts as a container for a boolean type. Can be used for thread operations where locking is required.
  /// </summary>
  public class BoolContainer
  {
    private bool _bIsTrue;

    /// <summary>
    /// Gets or sets the value of the contained boolean
    /// </summary>
    public bool IsTrue
    {
      get { return _bIsTrue; }
      set { _bIsTrue = value; }
    }

    public BoolContainer(bool bIsTrue)
    {
      _bIsTrue = bIsTrue;
    }
  }
}
