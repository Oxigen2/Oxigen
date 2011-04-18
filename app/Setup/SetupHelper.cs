using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using Setup.Properties;
using System.IO;
using Setup.UserManagementServicesLive;
using System.Management;
using System.Reflection;
using System.Security.Principal;

namespace Setup
{
  /// <summary>
  /// Provides business logic methods for the installation/user registration process
  /// </summary>
  internal static class SetupHelper
  {
    /// <summary>
    /// Notifies the user and exits the installer application when no changes have been made to the target system.
    /// </summary>
    internal static void ExitNoChanges()
    {
      if (AppDataSingleton.Instance.OneFormClosed)
        return;

      if (MessageBox.Show("Are you sure you want to exit Setup?\r\nYour system has not been modified. Please click OK to exit.", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
      {
        AppDataSingleton.Instance.OneFormClosed = true;

        Application.Exit();
      }
    }

    /// <summary>
    /// Checks registry key HKEY_LOCAL_MACHINE\SOFTWARE\Oxigen to see if oxigen exists in this computer
    /// </summary>
    /// <returns>true if registry key found, false otherwise</returns>
    internal static bool OxigenExists()
    {
      return GenericRegistryAccess.RegistryKeyExists(@"HKEY_LOCAL_MACHINE\SOFTWARE\Oxigen") ||
        GenericRegistryAccess.RegistryKeyExists(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Oxigen");
    }

    internal static bool OlderOxigenExists()
    {
      return GenericRegistryAccess.RegistryValueExists(@"HKEY_LOCAL_MACHINE\SOFTWARE\Oxigen", "path") ||
        GenericRegistryAccess.RegistryValueExists(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Oxigen", "path");
    }

    /// <summary>
    /// Opens a new form
    /// </summary>
    /// <typeparam name="T">The Form type to open</typeparam>
    /// <param name="callerForm">Reference to the caller form.</param>
    internal static void OpenForm<T>(Form callerForm) where T : OxigenForm, new()
    {
      T form = new T();
      form.Show();
      callerForm.Dispose();
    }

    private static void FillDOBDropDown(ComboBox ddDay, ComboBox ddMonth, ComboBox ddYear)
    {
      FillNumberDropDown(ddDay, 1, 31);
      FillMonthDropDown(ddMonth);
      FillNumberDropDown(ddYear, 1930, DateTime.Now.Year - 10);
    }

    internal static void FillNumberDropDown(ComboBox combo, int minValue, int maxValue)
    {
      combo.Items.Clear();

      combo.Items.Add("--");

      for (int i = minValue; i <= maxValue; i++)
        combo.Items.Add(i);
    }

    private static void FillMonthDropDown(ComboBox combo)
    {
      MonthForDD[] months = new MonthForDD[]
      {
        new MonthForDD(1, "January"),
        new MonthForDD(2, "February"),
        new MonthForDD(3, "March"),
        new MonthForDD(4, "April"),
        new MonthForDD(5, "May"),
        new MonthForDD(6, "June"),
        new MonthForDD(7, "July"),
        new MonthForDD(8, "August"),
        new MonthForDD(9, "September"),
        new MonthForDD(10, "October"),
        new MonthForDD(11, "November"),
        new MonthForDD(12, "December")
      };

      combo.Items.Add("--");

      foreach (MonthForDD m in months)
        combo.Items.Add(m);

      combo.DisplayMember = "Month";
      combo.ValueMember = "NumericMonth";
    }

    internal static MonthForDD GetSelectedMonthForDD(ComboBox ddMonth, int numericMonth)
    {
      foreach (object monthObj in ddMonth.Items)
      {
        if (monthObj is MonthForDD)
        {
          MonthForDD month = (MonthForDD)monthObj;

          if (month.NumericMonth == numericMonth)
            return month;
        }
      }

      return new MonthForDD();
    }

    private static List<Country> GetGeoList()
    {
      string flatGeoList = Resources.OxigenCountryList;

      string[] splitOnNewlines = flatGeoList.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

      List<Country> countries = new List<Country>();
      Country country = null;
      GeoL1Element geoL1element = null;

      foreach (string s in splitOnNewlines)
      {
        if (s.EndsWith(",,"))
        {
          country = new Country(s.Replace(",,", ""));
          countries.Add(country);
        }

        if (s.StartsWith(",") && s.EndsWith(","))
        {
          geoL1element = new GeoL1Element(s.Replace(",", ""));
          country.GeoL1Elements.Add(geoL1element);
        }

        if (s.StartsWith(",,"))
          geoL1element.GeoL2Elements.Add(s.Replace(",,", ""));
      }

      return countries;
    }

    private static Country GetCountry(ComboBox ddCountry, string countryName)
    {
      foreach (object countryObj in ddCountry.Items)
      {
        if (countryObj is Country)
        {
          Country country = (Country)countryObj;

          if (country.Name == countryName)
            return country;
        }
      }

      return null;
    }

    private static GeoL1Element GetStateFromGeoL1Element(ComboBox ddState, string geoL1ElementName)
    {
      foreach (object elementObj in ddState.Items)
      {
        if (elementObj is GeoL1Element)
        {
          GeoL1Element element = (GeoL1Element)elementObj;

          if (element.Name == geoL1ElementName)
            return element;
        }
      }

      return null;
    }

    private static GeoL1Element GetTownCityFromGeoL1Element(ComboBox ddTownCity, string geoL1ElementName)
    {
      foreach (object elementObj in ddTownCity.Items)
      {
        if (elementObj is GeoL1Element)
        {
          GeoL1Element element = (GeoL1Element)elementObj;

          if (element.Name == geoL1ElementName)
            return element;
        }
      }

      return null;
    }

    internal static void SetupGeoDropDowns(ComboBox ddCountry, ComboBox ddState, ComboBox ddTownCity)
    {
      List<Country> geoList = SetupHelper.GetGeoList();

      ddCountry.Items.Add("--");
      ddState.Items.Add("--");
      ddTownCity.Items.Add("--");

      foreach (Country country in geoList)
        ddCountry.Items.Add(country);

      ddCountry.DisplayMember = "Name";

      ddCountry.SelectedIndex = 0;
      ddState.SelectedIndex = 0;
      ddTownCity.SelectedIndex = 0;
    }

    internal static string[] GetArrayFromList(string resource)
    {
      return resource.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
    }

    internal static void SetStateTownCityDropDownsByCountry(ComboBox ddCountry, ComboBox ddState, ComboBox ddTownCity, Label lblState)
    {
      if (!(ddCountry.SelectedItem is Country))
        return;

      Country country = (Country)ddCountry.SelectedItem;

      if (country.Name == "United States")
      {
        ddState.Items.Clear();
        ddState.Items.Add("--");

        foreach (GeoL1Element geoL1Element in country.GeoL1Elements)
          ddState.Items.Add(geoL1Element);

        ddState.DisplayMember = "Name";
        ddState.SelectedIndex = 0;
        lblState.Enabled = true;
        ddState.Enabled = true;
      }
      else
      {
        ddState.Items.Clear();
        ddState.Items.Add("--");
        ddState.SelectedIndex = 0;

        ddTownCity.Items.Clear();
        ddTownCity.Items.Add("--");

        foreach (GeoL1Element geoL1Element in country.GeoL1Elements)
          ddTownCity.Items.Add(geoL1Element);

        ddTownCity.DisplayMember = "Name";

        ddTownCity.SelectedIndex = 0;
        lblState.Enabled = false;
        ddState.Enabled = false;
      }
    }

    internal static void SetTownCityDropDownByState(ComboBox ddCountry, ComboBox ddState, ComboBox ddTownCity)
    {
      if (ddCountry.SelectedItem is Country && ((Country)ddCountry.SelectedItem).Name == "United States")
      {
        if (ddState.SelectedItem is GeoL1Element)
        {
          GeoL1Element geo1Element = (GeoL1Element)ddState.SelectedItem;

          ddTownCity.Items.Clear();
          ddTownCity.Items.Add("--");

          foreach (string townCity in geo1Element.GeoL2Elements)
            ddTownCity.Items.Add(townCity);

          ddTownCity.SelectedIndex = 0;
        }
      }
    }

    internal static void SaveGeo(ComboBox ddCountry, ComboBox ddState, ComboBox ddTownCity)
    {
      string country = ((Country)ddCountry.SelectedItem).Name;

      AppDataSingleton.Instance.Country = country;

      if (country == "United States")
      {
        AppDataSingleton.Instance.State = ((GeoL1Element)ddState.SelectedItem).Name;
        AppDataSingleton.Instance.TownCity = (string)ddTownCity.SelectedItem;
      }
      else
        AppDataSingleton.Instance.TownCity = ((GeoL1Element)ddTownCity.SelectedItem).Name;
    }

    internal static void SaveSocioEconomicStatus(ComboBox ddOccupationSector, ComboBox ddEmploymentLevel, ComboBox ddAnnualHouseholdIncome)
    {
      AppDataSingleton.Instance.OccupationSector = (string)ddOccupationSector.SelectedItem;
      AppDataSingleton.Instance.EmploymentLevel = (string)ddEmploymentLevel.SelectedItem;
      AppDataSingleton.Instance.AnnualHouseholdIncome = (string)ddAnnualHouseholdIncome.SelectedItem;
    }

    internal static void SaveGender(RadioButton rbMale)
    {
      string gender;

      if (rbMale.Checked)
        gender = "male";
      else
        gender = "female";

      AppDataSingleton.Instance.Gender = gender;
    }

    internal static void SaveAgeGender(RadioButton rbMale, ComboBox ddDay, ComboBox ddMonth, ComboBox ddYear)
    {
      SaveAge(ddDay, ddMonth, ddYear);
      SaveGender(rbMale);
    }

    internal static void SaveAge(ComboBox ddDay, ComboBox ddMonth, ComboBox ddYear)
    {
      int numericalMonth = ((MonthForDD)ddMonth.SelectedItem).NumericMonth;

      AppDataSingleton.Instance.DOB = new DateTime((int)ddYear.SelectedItem, numericalMonth, (int)ddDay.SelectedItem);
      AppDataSingleton.Instance.DOBDay = (int)ddDay.SelectedItem;
      AppDataSingleton.Instance.DOBMonth = numericalMonth;
      AppDataSingleton.Instance.DOBYear = (int)ddYear.SelectedItem;
    }

    internal static void SetupSocioEconomicDropDowns(ComboBox ddOccupationSector, ComboBox ddEmploymentLevel, ComboBox ddAnnualHouseholdIncome)
    {
      string[] occupations = GetArrayFromList(Resources.OxigenOccupations);
      string[] employmentLevels = GetArrayFromList(Resources.OxigenEmploymentLevels);
      string[] annualHouseholdIncomes = GetArrayFromList(Resources.AnnualHouseholdIncome);

      ddOccupationSector.Items.Add("--");

      foreach (string s in occupations)
        ddOccupationSector.Items.Add(s);

      ddEmploymentLevel.Items.Add("--");

      foreach (string s in employmentLevels)
        ddEmploymentLevel.Items.Add(s);

      ddAnnualHouseholdIncome.Items.Add("--");

      foreach (string s in annualHouseholdIncomes)
        ddAnnualHouseholdIncome.Items.Add(s);

      if (!string.IsNullOrEmpty(AppDataSingleton.Instance.OccupationSector))
        ddOccupationSector.SelectedItem = AppDataSingleton.Instance.OccupationSector;
      else
        ddOccupationSector.SelectedIndex = 0;

      if (!string.IsNullOrEmpty(AppDataSingleton.Instance.EmploymentLevel))
        ddEmploymentLevel.SelectedItem = AppDataSingleton.Instance.EmploymentLevel;
      else
        ddEmploymentLevel.SelectedIndex = 0;

      if (!string.IsNullOrEmpty(AppDataSingleton.Instance.AnnualHouseholdIncome))
        ddAnnualHouseholdIncome.SelectedItem = AppDataSingleton.Instance.AnnualHouseholdIncome;
      else
        ddAnnualHouseholdIncome.SelectedIndex = 0;
    }

    internal static void SetupAgeGenderControls(RadioButton rbMale, RadioButton rbFemale, ComboBox ddDay,
      ComboBox ddMonth, ComboBox ddYear)
    {
      FillDOBDropDown(ddDay, ddMonth, ddYear);

      if (AppDataSingleton.Instance.Gender == "male")
        rbMale.Checked = true;

      if (AppDataSingleton.Instance.Gender == "female")
        rbFemale.Checked = true;

      if (AppDataSingleton.Instance.DOBMonth > 0)
        ddMonth.SelectedItem = SetupHelper.GetSelectedMonthForDD(ddMonth, AppDataSingleton.Instance.DOBMonth);
      else
        ddMonth.SelectedIndex = 0;

      if (AppDataSingleton.Instance.DOBYear > 0)
        ddYear.SelectedItem = AppDataSingleton.Instance.DOBYear;
      else
        ddYear.SelectedIndex = 0;

      if (AppDataSingleton.Instance.DOBDay > 0)
        ddDay.SelectedItem = AppDataSingleton.Instance.DOBDay;
      else
        ddDay.SelectedIndex = 0;
    }

    internal static char GetRandomLetter()
    {
        string rand = Path.GetRandomFileName();

        short alpha = (short)'A';
        short zed = (short)'Z';

        foreach (char c in rand)
        {
            char cUpper = c.ToString().ToUpper()[0];

            if ((short)cUpper >= alpha && (short)cUpper <= zed)
                return cUpper;
        }

        return 'Z';
    }

    internal static void SetFormControlsToDownloadedDetails(TextBox txtFirstName, TextBox txtLastName, RadioButton rbMale, RadioButton rbFemale,
      ComboBox ddDay, ComboBox ddMonth, ComboBox ddYear,
      ComboBox ddCountry, ComboBox ddState, ComboBox ddTownCity,
      ComboBox ddOccupationSector, ComboBox ddEmploymentLevel, ComboBox ddAnnualHouseholdIncome)
    {
      txtFirstName.Text = AppDataSingleton.Instance.FirstName;
      txtLastName.Text = AppDataSingleton.Instance.LastName;

      // pick a variable to check at random. if data not in database, then no variables have been filled
      // Apart from flat vaariables also check for a variable from the Location Taxnomy Tree as otherwisw soem accounts
      // will fail from the transition (from flat-text locations to Foreign Key to Location Taxonomy Tree)
      if (string.IsNullOrEmpty(AppDataSingleton.Instance.Gender) || string.IsNullOrEmpty(AppDataSingleton.Instance.Country))
        return;

      if (AppDataSingleton.Instance.Gender == "male")
        rbMale.Checked = true;

      if (AppDataSingleton.Instance.Gender == "female")
        rbFemale.Checked = true;

      ddMonth.SelectedItem = GetSelectedMonthForDD(ddMonth, AppDataSingleton.Instance.DOBMonth);
      ddYear.SelectedItem = AppDataSingleton.Instance.DOBYear;
      ddDay.SelectedItem = AppDataSingleton.Instance.DOBDay;

      SetupCountryStateTownCity(ddCountry, ddState, ddTownCity);

      ddOccupationSector.SelectedItem = AppDataSingleton.Instance.OccupationSector;
      ddEmploymentLevel.SelectedItem = AppDataSingleton.Instance.EmploymentLevel;
      ddAnnualHouseholdIncome.SelectedItem = AppDataSingleton.Instance.AnnualHouseholdIncome;
    }

    public static void SetupCountryStateTownCity(ComboBox ddCountry, ComboBox ddState, ComboBox ddTownCity)
    {
      Country selectedCountry = GetCountry(ddCountry, AppDataSingleton.Instance.Country);

      ddCountry.SelectedItem = GetCountry(ddCountry, AppDataSingleton.Instance.Country);

      if (AppDataSingleton.Instance.Country == "United States")
      {
        ddState.Items.Add("--");

        foreach (GeoL1Element l1element in selectedCountry.GeoL1Elements)
          ddState.Items.Add(l1element);

        ddState.DisplayMember = "Name";
        ddState.Enabled = true;

        GeoL1Element selectedState = SetupHelper.GetStateFromGeoL1Element(ddState, AppDataSingleton.Instance.State);

        ddState.SelectedItem = selectedState;

        ddTownCity.Items.Add("--");

        foreach (string s in selectedState.GeoL2Elements)
          ddTownCity.Items.Add(s);

        ddTownCity.SelectedItem = AppDataSingleton.Instance.TownCity;
      }
      else
      {
        ddTownCity.Items.Add("--");

        foreach (GeoL1Element l1element in selectedCountry.GeoL1Elements)
          ddTownCity.Items.Add(l1element);

        ddTownCity.DisplayMember = "Name";

        GeoL1Element selectedTownCity = SetupHelper.GetTownCityFromGeoL1Element(ddTownCity, AppDataSingleton.Instance.TownCity);

        ddTownCity.SelectedItem = selectedTownCity;
      }
    }

    internal static void ShowCommunicationAnimatingText(Label lblSubtitle, string holdMessage, System.Threading.Thread thread)
    {
      string originalText = lblSubtitle.Text;

      lblSubtitle.Text = holdMessage;
      Application.DoEvents();

      while (thread.IsAlive)
      {
        for (int i = 0; i < 10; i++)
        {
          lblSubtitle.Text += ".";
          System.Threading.Thread.Sleep(500);
          Application.DoEvents();
        }

        lblSubtitle.Text = holdMessage;
      }

      lblSubtitle.Text = originalText;
    }

    internal static string SimplifyBytes(long bytes)
    {
      int counter = 0;
      long convertedBytes = bytes;

      while (true)
      {
        if (convertedBytes >= 1024L)
        {
          convertedBytes = convertedBytes / 1024L;

          counter++;
        }
        else
          break;
      }

      switch (counter)
      {
        case 0:
          return bytes + " bytes";
        case 1:
          return convertedBytes + " KB";
        case 2:
          return convertedBytes + " MB";
        case 3:
          return convertedBytes + " GB";
        case 4:
          return convertedBytes + " TB";
        default:
          return "";
      }
    }

    internal static bool DriveExistsAndFixed(string path)
    {
      string driveName = path.Substring(0, 1);

      DriveInfo[] dis = DriveInfo.GetDrives();

      foreach (DriveInfo di in dis)
      {
        if (di.DriveType == DriveType.Fixed && di.Name.StartsWith(driveName))
          return true;
      }

      return false;
    }

    internal static void UpdateConfigurationFilesWithPaths(string binariesPath, string dataPath, string systemFolder)
    {
      UpdateOneConfigFileWithPaths(systemFolder + "Oxigen.scr.config", dataPath, binariesPath, "");
      UpdateOneConfigFileWithPaths(binariesPath + "OxigenLE.exe.config", dataPath, binariesPath, "");
      UpdateOneConfigFileWithPaths(binariesPath + "OxigenCE.exe.config", dataPath, binariesPath, "");
      UpdateOneConfigFileWithPaths(binariesPath + "ScreenSaverConfig.exe.config", dataPath, binariesPath, "");
      UpdateOneConfigFileWithPaths(binariesPath + "OxigenService.exe.config", dataPath, binariesPath, "");
      UpdateOneConfigFileWithPaths(binariesPath + "OxigenTray.exe.config", dataPath, binariesPath, systemFolder);
      UpdateOneConfigFileWithPaths(binariesPath + "OxigenSU.exe.config", dataPath, binariesPath, systemFolder);
    }

    private static void UpdateOneConfigFileWithPaths(string configFilePath, string selectedDataPath,
      string selectedBinariesPath, string systemFolder)
    {
      string xmlFile = File.ReadAllText(configFilePath);

      int part1Length = xmlFile.LastIndexOf("<appSettings>") + "<appSettings>".Length;

      string part1 = xmlFile.Substring(0, part1Length);

      int part2Length = xmlFile.Length - xmlFile.LastIndexOf("</appSettings>");

      string part2 = xmlFile.Substring(xmlFile.LastIndexOf("</appSettings>"), part2Length);

      StringBuilder newConfig = new StringBuilder(part1);

      newConfig.Append("<add key=\"AppDataPath\" value=\"");
      newConfig.Append(selectedDataPath);
      newConfig.Append("\"/>");
      newConfig.Append("<add key=\"BinariesPath\" value=\"");
      newConfig.Append(selectedBinariesPath);
      newConfig.Append("\"/>");

      if (systemFolder != "")
      {
        newConfig.Append("<add key=\"WindowsSystemPath\" value=\"");
        newConfig.Append(systemFolder);
        newConfig.Append("\"/>");
      }

      newConfig.Append(part2);

      File.WriteAllText(configFilePath, newConfig.ToString(), Encoding.ASCII);
    }

    internal static void InstallMSI(OxigenForm callerForm)
    {
      AppDataSingleton.Instance.SetupLogger.WriteTimestampedMessage("msiexec.exe /i \"Oxigen.msi\" /qn ALLUSERS=1 TARGETDIR=\"" + AppDataSingleton.Instance.BinariesPath + "\" DATAANDSETTINGS=\"" + AppDataSingleton.Instance.DataPath + "\" PCGUID=" + AppDataSingleton.Instance.User.MachineGUID + " USERGUID=" + AppDataSingleton.Instance.User.UserGUID + " REBOOT=ReallySuppress");
        
      Process process = null;

      ProcessStartInfo startInfo = new ProcessStartInfo("msiexec.exe", "/i \"Oxigen.msi\" /qn ALLUSERS=1 TARGETDIR=\"" + AppDataSingleton.Instance.BinariesPath + "\" DATAANDSETTINGS=\"" + AppDataSingleton.Instance.DataPath + "\" PCGUID=" + AppDataSingleton.Instance.User.MachineGUID + " USERGUID=" + AppDataSingleton.Instance.User.UserGUID + " REBOOT=ReallySuppress");

      try
      {
        process = Process.Start(startInfo);
      }
      catch
      {
        // environment variables have been tampered with by user
        MessageBox.Show("Cannot find msiexec.exe. Please contact your system administrator.\r\nYour system has not been modified. Installation will now exit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        callerForm.Dispose();
        Application.Exit();
        return;
      }

      process.WaitForExit();
    }

    internal static void UninstallMSI(OxigenForm callerForm)
    {
      Process process = null;

      ProcessStartInfo startInfo = new ProcessStartInfo("msiexec.exe", "/x \"{05AB04BD-B62E-4A98-9DA0-9650699CAF8E}\" /qn REBOOT=ReallySuppress");

      try
      {
        process = Process.Start(startInfo);
      }
      catch
      {
        // environment variables have been tampered with by user
        MessageBox.Show("Cannot find msiexec.exe. Please contact your system administrator.\r\nYour system has not been modified. Installation will now exit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        callerForm.Dispose();
        Application.Exit();
        return;
      }

      process.WaitForExit();
    }

    static OxigenIIAdvertising.LoggerInfo.Logger _log = new OxigenIIAdvertising.LoggerInfo.Logger("Install Process", Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Debug.txt", OxigenIIAdvertising.LoggerInfo.LoggingMode.Debug);

    private static string GetBinaryPathAboveBin() 
    {
        if (GenericRegistryAccess.RegistryKeyExists("HKEY_LOCAL_MACHINE\\Software\\Oxigen"))
            return (string)GenericRegistryAccess.GetRegistryValue("HKEY_LOCAL_MACHINE\\Software\\Oxigen", "ProgramPath");
        else
            return (string)GenericRegistryAccess.GetRegistryValue("HKEY_LOCAL_MACHINE\\Software\\Wow6432Node\\Oxigen", "ProgramPath");
    }

    internal static void DoPostMSIInstallSteps()
    {
      // Post-MSI steps      
      string systemFolder = GetSystemDirectory() + "\\";

      string binariesPath = null;
      string dataPath = null;

      if (GenericRegistryAccess.RegistryKeyExists("HKEY_LOCAL_MACHINE\\Software\\Oxigen"))
      {
        binariesPath = (string)GenericRegistryAccess.GetRegistryValue("HKEY_LOCAL_MACHINE\\Software\\Oxigen", "ProgramPath") + "bin\\";
        dataPath = (string)GenericRegistryAccess.GetRegistryValue("HKEY_LOCAL_MACHINE\\Software\\Oxigen", "DataSettingsPath") + "data\\";
      }
      else
      {
        binariesPath = (string)GenericRegistryAccess.GetRegistryValue("HKEY_LOCAL_MACHINE\\Software\\Wow6432Node\\Oxigen", "ProgramPath") + "bin\\";
        dataPath = (string)GenericRegistryAccess.GetRegistryValue("HKEY_LOCAL_MACHINE\\Software\\Wow6432Node\\Oxigen", "DataSettingsPath") + "data\\";
      }

      // Update App.config files with the paths needed for the application suite to work.   
      try
      {
        UpdateConfigurationFilesWithPaths(binariesPath, dataPath, systemFolder);
      }
      catch (Exception ex)
      {
        _log.WriteError(ex.ToString());
        throw ex;
      }

      // copy ss_general_data.dat and ss_channel_subscriptions.dat, create user settings
      Serializer.Serialize(AppDataSingleton.Instance.GeneralData, dataPath + "SettingsData\\ss_general_data.dat", "password");

      // User settings
      AppDataSingleton.Instance.User.DefaultDisplayDuration = AppDataSingleton.Instance.SilentMode ? AppDataSingleton.Instance.User.DefaultDisplayDuration : 5F;
      AppDataSingleton.Instance.User.FlashVolume = 1;
      AppDataSingleton.Instance.User.MuteFlash = false;
      AppDataSingleton.Instance.User.MuteVideo = false;
      AppDataSingleton.Instance.User.VideoVolume = 50;
      AppDataSingleton.Instance.User.CanUpdate = AppDataSingleton.Instance.SilentMode ? AppDataSingleton.Instance.User.CanUpdate : true;

      Serializer.Serialize(AppDataSingleton.Instance.User, dataPath + "SettingsData\\UserSettings.dat", "password");
    }

    public static void RenameExeToSCR()
    {
      RenameExeToSCR(GetSystemDirectory());
    }

    public static void RenameExeToSCR(string systemFolder)
    {
      try
      {
        File.Move(systemFolder + "Oxigen.exe", systemFolder + "Oxigen.scr");
        File.Move(systemFolder + "Oxigen.exe.config", systemFolder + "Oxigen.scr.config");
      }
      catch (Exception ex)
      {
        _log.WriteError(ex.ToString());
        throw ex;
      }
    }

    public static void RenameSCRToExe()
    {
      RenameSCRToExe(GetSystemDirectory());
    }

    public static void RenameSCRToExe(string systemFolder)
    {
      try
      {
        File.Move(systemFolder + "Oxigen.scr", systemFolder + "Oxigen.exe");
        File.Move(systemFolder + "Oxigen.scr.config", systemFolder + "Oxigen.exe.config");
      }
      catch (Exception ex)
      {
        _log.WriteError(ex.ToString());
        throw ex;
      }
    }

    internal static void DeleteAllFilesDirectories(string path)
    {
      if (!Directory.Exists(path))
        return;

      try
      {
        Directory.Delete(path, true);
      }
      catch
      {
        // ignore
      }
    }

    internal static void RemoveAllFiles(string binDir, string systemDir, string dataDir)
    {
      if (Directory.Exists(dataDir))
        DeleteAllFilesDirectories(dataDir);

      if (Directory.Exists(binDir))
        DeleteAllFilesDirectories(binDir);

      // check which system32 files belong to Oxigen and delete them
      // non-DLL Oxigen files
      if (File.Exists(systemDir + "\\Oxigen.scr.config"))
        File.Delete(systemDir + "\\Oxigen.scr.config");

      if (File.Exists(systemDir + "\\Oxigen.scr"))
        File.Delete(systemDir + "\\Oxigen.scr");

      // remove interop files
      if (!File.Exists(systemDir + "\\AxInterop.QTOControlLib.dll"))
        File.Delete(systemDir + "\\AxInterop.QTOControlLib.dll");

      if (!File.Exists(systemDir + "\\AxInterop.ShockwaveFlashObjects.dll"))
        File.Delete(systemDir + "\\AxInterop.ShockwaveFlashObjects.dll");

      if (!File.Exists(systemDir + "\\AxInterop.QTOControlLib.dll"))
        File.Delete(systemDir + "\\AxInterop.QTOControlLib.dll");

      if (!File.Exists(systemDir + "\\AxInterop.WMPLib.dll"))
        File.Delete(systemDir + "\\AxInterop.WMPLib.dll");

      if (!File.Exists(systemDir + "\\Interop.QTOControlLib.dll"))
        File.Delete(systemDir + "\\Interop.QTOControlLib.dll");

      if (!File.Exists(systemDir + "\\Interop.QTOLibrary.dll"))
        File.Delete(systemDir + "\\Interop.QTOLibrary.dll");

      if (!File.Exists(systemDir + "\\Interop.SHDocVw.dll"))
        File.Delete(systemDir + "\\Interop.SHDocVw.dll");

      if (!File.Exists(systemDir + "\\Interop.ShockwaveFlashObjects.dll"))
        File.Delete(systemDir + "\\Interop.ShockwaveFlashObjects.dll");

      if (!File.Exists(systemDir + "\\Interop.WMPLib.dll"))
        File.Delete(systemDir + "\\Interop.WMPLib.dll");

      if (!File.Exists(systemDir + "\\Flash10i.ocx"))
        File.Delete(systemDir + "\\Flash10i.ocx");

      if (!File.Exists(systemDir + "\\QTOControl.dll"))
        File.Delete(systemDir + "\\QTOControl.dll");

      if (!File.Exists(systemDir + "\\QTOLibrary.dll"))
        File.Delete(systemDir + "\\QTOLibrary.dll");

      // delete Oxigen-issued DLLs
      string[] files = Directory.GetFiles(systemDir, "*.dll");

      int length = files.Length;

      for (int i = 0; i < length; i++)
      {
        if (Path.GetFileName(files[i]).StartsWith("Oxigen"))
          File.Delete(files[i]);
      }
    }

    private static bool IsOxigenFile(string file)
    {
      Assembly oxigenAssembly = null;

      try
      {
        oxigenAssembly = Assembly.LoadFile(file);

        object[] attributes = oxigenAssembly.GetCustomAttributes(true);

        foreach (Attribute a in attributes)
        {
          if (a is AssemblyCompanyAttribute)
          {
            string company = (a as AssemblyCompanyAttribute).Company;

            return company == "Oxigen";
          }
        }
      }
      catch
      {
        return false;
      }

      return false;
    }

    internal static string GetMACAddress()
    {
      ManagementObjectSearcher query = null;
      ManagementObjectCollection queryCollection = null;

      try
      {
        query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled='TRUE'");

        queryCollection = query.Get();

        foreach (ManagementObject mo in queryCollection)
        {
          if (mo["MacAddress"] != null)
            return (mo["MacAddress"]).ToString();
        }
      }
      catch (Exception ex)
      {
        return ex.ToString();
      }

      return String.Empty;
    }

    internal static string GetPcGUIDFromRegistry()
    {
      string pcGUID;

      try
      {
        pcGUID = (string)GenericRegistryAccess.GetRegistryValue("HKEY_LOCAL_MACHINE\\Software\\Oxigen", "pcGUID");
      }
      catch
      {
        return null;
      }

      return pcGUID;
    }

    internal static string GetUserGUIDFromRegistry()
    {
      string userGUID;

      try
      {
        userGUID = (string)GenericRegistryAccess.GetRegistryValue("HKEY_LOCAL_MACHINE\\Software\\Oxigen", "userGUID");
      }
      catch
      {
        return null;
      }

      return userGUID;
    }

    internal static void ShowMessage(Label label, string message)
    {
      label.Text = message;
      Application.DoEvents();
    }

    internal static void CopySetup()
    {
      // do not copy setup if this is a repair. On repair, setup runs from the target location.
      if (AppDataSingleton.Instance.Repair)
        return;

      // 64-bit / MSI issue workaround
      // On 64 bit machines, you tell msiexec.exe to install Oxigen on the default program files location, it makes its own choice whether to install it on Program Files or Program Files (x86) folder.
      // Therefore, we can't make use of AppDataSingleton.Instance.BinariesPath top copy Setup to install dir. Solution: Access the registry
      File.Copy("Setup.exe", GetBinaryPathAboveBin() + "\\Setup.exe", true);
    }

    internal static void RemoveSetup()
    {
      if (Environment.GetEnvironmentVariable("windir") == null)
        throw new MissingFieldException("WinDir variable does not exist");

      string installerDir = Environment.GetEnvironmentVariable("windir") + "\\Installer\\OxigenSetup\\";

      if (Directory.Exists(installerDir))
        Directory.Delete(installerDir, true);
    }

    internal static bool IsDateValid(ComboBox ddDay, ComboBox ddMonth, ComboBox ddYear)
    {
      if (!(ddDay.SelectedItem is int) || !(ddMonth.SelectedItem is MonthForDD) || !(ddYear.SelectedItem is int))
        return false;

      int day = (int)ddDay.SelectedItem;
      int month = ((MonthForDD)ddMonth.SelectedItem).NumericMonth;
      int year = (int)ddYear.SelectedItem;

      try
      {
        DateTime dt = new DateTime(year, month, day);
      }
      catch
      {
        return false;
      }

      return true;
    }

    internal static bool IsGeoValid(ComboBox ddCountry, ComboBox ddState, ComboBox ddTownCity)
    {
      if (ddCountry.SelectedItem is string)
        return false;

      if (((Country)ddCountry.SelectedItem).Name == "United States")
      {
        if (ddState.SelectedItem is string || (string)ddTownCity.SelectedItem == "--")
          return false;

        return true;
      }
      else
      {
        if (ddTownCity.SelectedItem is string && (string)ddTownCity.SelectedItem == "--")
          return false;

        return true;
      }
    }

    internal static bool SocioEconomicStatusValid(ComboBox ddOccupationSector, ComboBox ddEmploymentLevel, ComboBox ddAnnualHouseholdIncome)
    {
      if (ddOccupationSector.SelectedIndex == 0 || ddEmploymentLevel.SelectedIndex == 0
        || ddAnnualHouseholdIncome.SelectedIndex == 0)
        return false;

      return true;
    }

    internal static UserManagementServicesLive.ChannelSubscription[] GetChannelSubscriptionsNetFromLocal(Setup.DuplicateLibrary.ChannelSubscription[] channelSubscriptionsLocal)
    {
      UserManagementServicesLive.ChannelSubscription[] channelSubscriptionsNet = new Setup.UserManagementServicesLive.ChannelSubscription[channelSubscriptionsLocal.Length];

      int count = 0;

      foreach (DuplicateLibrary.ChannelSubscription csLocal in channelSubscriptionsLocal)
      {
        UserManagementServicesLive.ChannelSubscription csNet = new Setup.UserManagementServicesLive.ChannelSubscription();
        csNet.ChannelGUID = csLocal.ChannelGUID;
        csNet.ChannelIDSpecified = true;
        csNet.ChannelID = csLocal.ChannelID;
        csNet.ChannelName = csLocal.ChannelName;
        csNet.ChannelWeightingUnnormalised = csLocal.ChannelWeightingUnnormalised;
        csNet.ChannelWeightingUnnormalisedSpecified = true;

        channelSubscriptionsNet[count] = csNet;
        count++;
      }

      return channelSubscriptionsNet;
    }

    internal static DuplicateLibrary.ChannelSubscription[] GetChannelSubscriptionsLocalFromNet(Setup.UserManagementServicesLive.ChannelSubscription[] channelSubscriptionsNet)
    {
      DuplicateLibrary.ChannelSubscription[] channelSubscriptionsLocal = new Setup.DuplicateLibrary.ChannelSubscription[channelSubscriptionsNet.Length];

      int count = 0;

      foreach (UserManagementServicesLive.ChannelSubscription csNet in channelSubscriptionsNet)
      {
        DuplicateLibrary.ChannelSubscription csLocal = new Setup.DuplicateLibrary.ChannelSubscription();
        csLocal.ChannelGUID = csNet.ChannelGUID;
        csLocal.ChannelID = csNet.ChannelID;
        csLocal.ChannelName = csNet.ChannelName;
        csLocal.ChannelWeightingUnnormalised = csNet.ChannelWeightingUnnormalised;

        channelSubscriptionsLocal[count] = csLocal;
        count++;
      }

      return channelSubscriptionsLocal;
    }

    internal static SimpleErrorWrapper GetGenericErrorConnectingWrapper()
    {
      return new Setup.UserManagementServicesLive.SimpleErrorWrapper
      {
        ErrorCode = "CONN",
        ErrorStatus = ErrorStatus1.Failure,
        ErrorSeverity = ErrorSeverity1.Retriable,
        Message = "Unable to communicate with Oxigen servers. Please check your internet connection or try again later."
      };
    }

    internal static bool HasAdminRights()
    {
      WindowsPrincipal pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
      return pricipal.IsInRole(WindowsBuiltInRole.Administrator);
    }

    private const int CSIDL_COMMON_DOCUMENTS = 0x002e;

    internal static string GetDefaultDataFolder() {
        StringBuilder path = new StringBuilder(260);

        SHGetSpecialFolderPath(IntPtr.Zero, path, CSIDL_COMMON_DOCUMENTS, false);

        return path.ToString();
    }

    internal static string GetSystemDirectory()
    {
      StringBuilder path = new StringBuilder(260);
      SHGetSpecialFolderPath(IntPtr.Zero, path, 0x0029, false);
      return path.ToString();
    }

    [System.Runtime.InteropServices.DllImport("shell32.dll")]
    private static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner,
       [System.Runtime.InteropServices.Out] StringBuilder lpszPath, int nFolder, bool fCreate);

    public static string GetResponsiveServer(ServerType serverType, string typeOfNoServersToLookAt,
      string endpointSuffix)
    {
      return ResponsiveServerDeterminator.GetResponsiveURI(serverType,
            int.Parse(AppDataSingleton.Instance.GeneralData.NoServers[typeOfNoServersToLookAt]),
            int.Parse(AppDataSingleton.Instance.GeneralData.Properties["serverTimeout"]),
            SetupHelper.GetRandomLetter().ToString(),
            AppDataSingleton.Instance.GeneralData.Properties["primaryDomainName"],
            AppDataSingleton.Instance.GeneralData.Properties["secondaryDomainName"],
            endpointSuffix);
    }

    public static string GetResponsiveServer(ServerType serverType, 
                                                int noServers, 
                                                string letter,
                                                string endpointSuffix)
    {
      return ResponsiveServerDeterminator.GetResponsiveURI(serverType,
            noServers,
            int.Parse(AppDataSingleton.Instance.GeneralData.Properties["serverTimeout"]),
            letter,
            AppDataSingleton.Instance.GeneralData.Properties["primaryDomainName"],
            AppDataSingleton.Instance.GeneralData.Properties["secondaryDomainName"],
            endpointSuffix);
    }

    internal static void KillProcess(string processName)
    {
      Process[] processes = Process.GetProcessesByName(processName);

      foreach (Process process in processes)
      {
        process.Kill();
        process.WaitForExit();
      }
    }

    internal static bool PrerequisitesMet()
    {
      return (IsRamSufficient() != RAMStatus.CannotInstall) && DotNet35SExists() && FlashActiveXExists()
        && QuickTimeRightVersionExists() && WindowsMediaPlayerRightVersionExists(); 
    }    

    internal static RAMStatus IsRamSufficient()
    {
      long physicalRAM = GetPhysicalRam();

      if (physicalRAM >= 1033741824)
        return RAMStatus.Good;

      if (physicalRAM >= 496870912)
        return RAMStatus.Marginal;

      return RAMStatus.CannotInstall;
    }

    internal static bool DotNet35SExists()
    {
      return GenericRegistryAccess.RegistryKeyExists(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5");
    }

    internal static long GetPhysicalRam()
    {
      ObjectQuery winQuery = new ObjectQuery("SELECT * FROM Win32_ComputerSystem");
      ManagementObjectSearcher searcher = new ManagementObjectSearcher(winQuery);
      foreach (ManagementObject item in searcher.Get())
        return long.Parse(item["TotalPhysicalMemory"].ToString());

      return -1;
    }

    internal static bool FlashActiveXExists()
    {
      return GenericRegistryAccess.RegistryKeyExists(@"HKEY_LOCAL_MACHINE\SOFTWARE\Macromedia\FlashPlayerActiveX") ||
        GenericRegistryAccess.RegistryKeyExists(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Macromedia\FlashPlayerActiveX");
    }

    internal static bool QuickTimeRightVersionExists()
    {
      return FileRightVersionExists(@"HKEY_LOCAL_MACHINE\SOFTWARE\Apple Computer, Inc.\QuickTime", "InstallDir", "QuickTimePlayer.exe", 7)
      || FileRightVersionExists(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Apple Computer, Inc.\QuickTime", "InstallDir", "QuickTimePlayer.exe", 7);
    }

    internal static bool WindowsMediaPlayerRightVersionExists()
    {
      return FileRightVersionExists(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\MediaPlayer", "Installation Directory", "wmplayer.exe", 10);
    }

    internal static bool FileRightVersionExists(string registryKey, string registryValue, string executableToCheck, int minimumVersion)
    {
      // get the installation path of the executable
      RegistryKey installationPathKey = GenericRegistryAccess.GetRegistryKey(registryKey);

      if (installationPathKey == null)
        return false;

      object installationPathValue = installationPathKey.GetValue(registryValue);

      if (installationPathValue == null)
        return false;

      string executableFullPath = installationPathValue.ToString() + "\\" + executableToCheck;

      int version;
      string stringVersion;

      try
      {
        FileVersionInfo fv = FileVersionInfo.GetVersionInfo(executableFullPath);

        stringVersion = fv.FileVersion.Split(new char[] { '.' })[0];
      }
      catch
      {
        return false;
      }

      if (!int.TryParse(stringVersion, out version))
        return false;

      if (version >= minimumVersion)
        return true;

      return false;
    }

    internal static void SetRegistryForModifyUninstall()
    {
      string uninstallString = GetBinaryPathAboveBin() + "Setup.exe /u";

      GenericRegistryAccess.SetRegistryValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{05AB04BD-B62E-4A98-9DA0-9650699CAF8E}", "UninstallString", uninstallString);
      GenericRegistryAccess.SetRegistryValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{05AB04BD-B62E-4A98-9DA0-9650699CAF8E}", "WindowsInstaller", 0);

      string modifyPath = GetBinaryPathAboveBin() + "Setup.exe /u"; // when we do Repair install, change this to /m

      GenericRegistryAccess.SetRegistryValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{05AB04BD-B62E-4A98-9DA0-9650699CAF8E}", "ModifyPath", modifyPath);
    }

    internal static bool GetValuesFromRegistry(ref string userGUID, ref string machineGUID,
      ref string dataSettingsPath, ref string binaryPath)
    {
      object dataSettingsPathObj = GenericRegistryAccess.GetRegistryValue("HKEY_LOCAL_MACHINE\\Software\\Oxigen", "DataSettingsPath");

      if (dataSettingsPathObj == null) // registry has been tampered with and data path has not been found
        return false;

      dataSettingsPath = (string)dataSettingsPathObj;

      object userGUIDObj = GenericRegistryAccess.GetRegistryValue("HKEY_LOCAL_MACHINE\\Software\\Oxigen", "userGUID");

      if (userGUIDObj == null)
        return false;

      userGUID = (string)userGUIDObj;

      object machineGUIDObj = GenericRegistryAccess.GetRegistryValue("HKEY_LOCAL_MACHINE\\Software\\Oxigen", "pcGUID");

      if (machineGUIDObj == null)
        return false;

      machineGUID = (string)machineGUIDObj;

      object binaryPathObj = GenericRegistryAccess.GetRegistryValue("HKEY_LOCAL_MACHINE\\Software\\Oxigen", "ProgramPath");

      if (binaryPathObj == null)
        return false;

      binaryPath = (string)binaryPathObj;

      return true;
    }

    internal static UserManagementServicesLive.SimpleErrorWrapper SendUninstallInfo(string userGUID, string machineGUID)
    {
      UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer client = null;
      UserManagementServicesLive.SimpleErrorWrapper wrapper = null;

      try
      {
        string url = SetupHelper.GetResponsiveServer(ServerType.MasterGetConfig, "masterConfig", "UserManagementServices.svc");

        if (string.IsNullOrEmpty(url))
        {
            AppDataSingleton.Instance.SetupLogger.WriteTimestampedMessage("Registering uninstall: Couldn't find a responsive URL.");
            return SetupHelper.GetGenericErrorConnectingWrapper();
        }
          client = new Setup.UserManagementServicesLive.BasicHttpBinding_IUserManagementServicesNonStreamer();

        client.Url = url;

        wrapper = client.RegisterSoftwareUninstall(userGUID, machineGUID, "password");
      }
      catch (System.Net.WebException ex)
      {
          AppDataSingleton.Instance.SetupLogger.WriteError(ex);
        return SetupHelper.GetGenericErrorConnectingWrapper();
      }
      finally
      {
        if (client != null)
        {
          try
          {
            client.Dispose();
          }
          catch
          {
            client.Abort();
          }
        }
      }

      return wrapper;
    }

    internal static void KillApplications()
    {
      SetupHelper.KillProcess("OxigenService");
      SetupHelper.KillProcess("OxigenCE");
      SetupHelper.KillProcess("OxigenLE");
      SetupHelper.KillProcess("OxigenSU");
      SetupHelper.KillProcess("OxigenTray");
      SetupHelper.KillProcess("Oxigen");
    }


    internal static bool IsSufficientSpace(string path, long maximumSpace)
    {
      DriveInfo di = new DriveInfo(path);

      long freeSpace = di.AvailableFreeSpace;

      return freeSpace - maximumSpace >= 0;
    }

    internal static bool CheckDriveExists(string path)
    {
      if (string.IsNullOrEmpty(path))
        return false;

      try
      {
        DriveInfo di = new DriveInfo(AppDataSingleton.Instance.BinariesPath);
        string driveName = di.Name;
      }
      catch (DriveNotFoundException)
      {
        return false;
      }

      return true;
    }
  }

  internal enum RAMStatus
  {
    Good,
    Marginal,
    CannotInstall
  }

  public struct MonthForDD
  {
    private int _numericMonth;
    private string _month;

    public int NumericMonth
    {
      get { return _numericMonth; }
      set { _numericMonth = value; }
    }

    public string Month
    {
      get { return _month; }
      set { _month = value; }
    }

    public MonthForDD(int numericMonth, string month)
    {
      _numericMonth = numericMonth;
      _month = month;
    }
  }

  public class Country
  {
    private string _name;
    private List<GeoL1Element> _geoL1Elements;

    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }

    public List<GeoL1Element> GeoL1Elements
    {
      get { return _geoL1Elements; }
      set { _geoL1Elements = value; }
    }

    public Country(string name)
    {
      _name = name;
      _geoL1Elements = new List<GeoL1Element>();
    }
  }

  public class GeoL1Element
  {
    private string _name;
    private List<string> _geoL2elements;

    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }

    public List<string> GeoL2Elements
    {
      get { return _geoL2elements; }
      set { _geoL2elements = value; }
    }

    public GeoL1Element(string name)
    {
      _name = name;

      _geoL2elements = new List<string>();
    }
  }
}