using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSGContracts;

namespace OxigenService
{
  public class ScreensaverGuardianComm : IScreensaverGuardian
  {
    public void OpenBrowser(string url)
    {
      System.Diagnostics.Process.Start(url);
    }
  }
}
