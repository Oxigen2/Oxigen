using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.XMLSerializer;
using System.IO;
using OxigenIIAdvertising.AppData;
using OxigenIIAdvertising.FileLocker;
using System.Diagnostics;
using OxigenIIAdvertising.UserSettings;

namespace libutil
{
  class Program
  {
    static void Main(string[] args)
    {
      //User user = new User()
      //{
      //  AssetFolderSize = 117552893952L,
      //  CanUpdate = true,
      //  DefaultDisplayDuration = 5,
      //  FlashVolume = 1,
      //  MuteFlash = false,
      //  MuteVideo = false,
      //  VideoVolume = 50,
      //  UserGUID = "27440C84-50E0-4EC2-BE16-FEE30B285325_T",
      //  MachineGUID = "FFC56970-F19C-4BB9-9595-81073CDA7D7C_B",
      //  SoftwareMinorVersionNumber = 0,
      //  SoftwareMajorVersionNumber = 1
      //};

      //Serializer.Serialize(user, @"C:\Users\Public\Documents\Oxigen\data\SettingsData\UserSettings.dat", "password");

      GeneralData gd = new GeneralData();
      gd.SoftwareMajorVersionNumber = 1;
      gd.SoftwareMinorVersionNumber = 5;
      gd.Properties.Add("logExchangerProcessingInterval", "1800");
      gd.Properties.Add("contentExchangerProcessingInterval", "1800");
      gd.Properties.Add("softwareUpdaterProcessingInterval", "10800"); // 3h
      gd.Properties.Add("serverTimeout", "5000"); // milliseconds
      gd.Properties.Add("primaryDomainName", ".oxigen.net");
      gd.Properties.Add("secondaryDomainName", ".oxigen.net");
      gd.Properties.Add("advertDisplayThreshold", "0.1");
      gd.Properties.Add("logTimerInterval", "20");
      gd.Properties.Add("protectedContentTime", "25");
      gd.Properties.Add("maxLines", "10");
      gd.Properties.Add("noAssetDisplayLength", "15");
      gd.Properties.Add("requestTimeout", "4");
      gd.Properties.Add("dateTimeDiffTolerance", "5"); // minutes
      gd.Properties.Add("daysToKeepAssetFiles", "8");

      gd.NoServers.Add("relayLog", "1");
      gd.NoServers.Add("relayConfig", "1");
      gd.NoServers.Add("relayChannelAssets", "1");
      gd.NoServers.Add("relayChannelData", "1");
      gd.NoServers.Add("masterConfig", "4");
      gd.NoServers.Add("download", "1");

      string file = @"c:\oxigen\a\ss_general_data.dat";

      Serializer.Serialize(gd, file, "password");

      string checksum = OxigenIIAdvertising.FileChecksumCalculator.ChecksumCalculator.GetChecksum(file);

      File.WriteAllText(@"c:\oxigen\a\ss_general_data.chk", checksum);

      //FileStream fs = null;
      //MemoryStream memoryStream = null;

      //try
      //{

      //  memoryStream = Locker.ReadDecryptFile(ref fs, @"C:\Users\Public\Documents\Oxigen\data\SettingsData\UserSettings.dat", "password", false, true);
      //}
      //catch (Exception ex)
      //{
      //  Locker.ClearFileStream(ref fs);

      //  throw ex;
      //}

      //string decryptedLogs = "";

      //// if memory stream is not null (i.e. log files already exist)
      //if (memoryStream != null)
      //{
      //  StreamReader sr = new StreamReader(memoryStream);

      //  try
      //  {
      //    decryptedLogs = sr.ReadToEnd();
      //  }
      //  catch (Exception ex)
      //  {
      //    Locker.ClearFileStream(ref fs);

      //    sr.Close();
      //    sr.Dispose();
      //    memoryStream.Close();
      //    memoryStream.Dispose();

      //    throw ex;
      //  }

      //  sr.Close();
      //  sr.Dispose();
      //  memoryStream.Close();
      //  memoryStream.Dispose();
      //}

      //File.WriteAllText(@"C:\Users\Public\Documents\Oxigen\data\SettingsData\UserSettings.xml", decryptedLogs);
    }
  }
}
