using Microsoft.Win32;

namespace Setup.ClientLoggers
{
  public class PersistentClientLogger : ClientLogger
  {
    public PersistentClientLogger()
    {
      RegistryKey oxigen = null;

      if (!GenericRegistryAccess.RegistryKeyExists(RegistryBranch.HKLM_LOCAL_MACHINE__SOFTWARE_OxigenRef))
      {
        _userRef = System.Guid.NewGuid().ToString();
        Log("Registry__HKLM_LOCAL_MACHINE__SOFTWARE_OxigenRef");
        oxigen = GenericRegistryAccess.CreateRegistryKey(RegistryBranch.HKLM_LOCAL_MACHINE__SOFTWARE_OxigenRef);
      }
      else
        oxigen = GenericRegistryAccess.GetRegistryKey(RegistryBranch.HKLM_LOCAL_MACHINE__SOFTWARE_OxigenRef);

      if (oxigen.GetValue("userRef") == null)
      {
        if (_userRef == null)
          _userRef = System.Guid.NewGuid().ToString();

        Log("Registry__HKLM_LOCAL_MACHINE__SOFTWARE_OxigenRef_Oxigen");
        oxigen.SetValue("userRef", _userRef);
      }
      else
        _userRef = oxigen.GetValue("userRef").ToString();
    }
  }
}
