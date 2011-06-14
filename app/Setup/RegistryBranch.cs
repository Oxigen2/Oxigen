namespace Setup
{
  internal static class RegistryBranch
  {
    internal static readonly string HKLM_LOCAL_MACHINE__SOFTWARE_WOW6432Node = @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node";
    internal static readonly string HKLM_LOCAL_MACHINE__SOFTWARE = @"HKEY_LOCAL_MACHINE\SOFTWARE";
    internal static readonly string HKLM_LOCAL_MACHINE__SOFTWARE_WOW6432Node_Oxigen = @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Oxigen";
    internal static readonly string HKLM_LOCAL_MACHINE__SOFTWARE_Oxigen = @"HKEY_LOCAL_MACHINE\SOFTWARE\Oxigen";
    internal static readonly string HKLM_LOCAL_MACHINE__SOFTWARE_OxigenRef = @"HKEY_LOCAL_MACHINE\SOFTWARE\OxigenRef"; // no distinction between 32 and 64 bits
  }
}
