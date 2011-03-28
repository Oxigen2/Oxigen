using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ServiceProcess;
using RegistryAccess;
using Microsoft.Win32;
using SimpleInstaller.Properties;
using System.Diagnostics;
using System.Threading;

namespace SimpleInstaller
{
  public class Installer
  {
    internal static void Install(string selectedDataPath, string selectedSettingsDataPath, 
      string selectedBinariesPath, string systemFolder, string winDir, string programFiles, string serviceName)
    {
      Directory.CreateDirectory(selectedBinariesPath);

      Directory.CreateDirectory(selectedDataPath + "Assets\\");
      Directory.CreateDirectory(selectedDataPath + "ChannelData\\");
      Directory.CreateDirectory(selectedSettingsDataPath);
      Directory.CreateDirectory(selectedDataPath + "Temp\\");
      Directory.CreateDirectory(selectedDataPath + "Other\\"); // logs

      WriteFiles(selectedSettingsDataPath, selectedBinariesPath, systemFolder);

      UpdateConfigurationFilesWithPaths(selectedDataPath, selectedBinariesPath, systemFolder);

      InstallSSGService(winDir, programFiles, serviceName);

      Thread.Sleep(3000);

      InstallEventLog();

      StartSSGService(serviceName);
    }

    private static void WriteFiles(string selectedSettingsDataPath, string selectedBinariesPath, string systemFolder)
    {
      // Data
      File.WriteAllBytes(selectedSettingsDataPath + "ss_general_data.dat", Resources.ss_general_data);

      // Bin
      File.WriteAllBytes(selectedBinariesPath + "AdvertList.dll", Resources.AdvertList);
      File.WriteAllBytes(selectedBinariesPath + "AssetLevel.dll", Resources.AssetLevel);
      File.WriteAllBytes(selectedBinariesPath + "Assets.dll", Resources.Assets);
      File.WriteAllBytes(selectedBinariesPath + "AssetScheduling.dll", Resources.AssetScheduling);
      File.WriteAllBytes(selectedBinariesPath + "AssetType.dll", Resources.AssetType);
      File.WriteAllBytes(selectedBinariesPath + "ChannelData.dll", Resources.ChannelData);
      File.WriteAllBytes(selectedBinariesPath + "ChannelSubscriptions.dll", Resources.ChannelSubscriptions);
      File.WriteAllBytes(selectedBinariesPath + "OxigenCE.exe", Resources.OxigenCE);
      File.WriteAllText(selectedBinariesPath + "OxigenCE.exe.config", Resources.OxigenCE_exe, Encoding.ASCII);
      File.WriteAllBytes(selectedBinariesPath + "ProxyClientBaseLib.dll", Resources.ProxyClientBaseLib);
      File.WriteAllBytes(selectedBinariesPath + "DemographicRange.dll", Resources.DemographicRange);
      File.WriteAllBytes(selectedBinariesPath + "Demographic.dll", Resources.Demographic);
      File.WriteAllBytes(selectedBinariesPath + "EncryptionDecryption.dll", Resources.EncryptionDecryption);
      File.WriteAllBytes(selectedBinariesPath + "Exceptions.dll", Resources.Exceptions);
      File.WriteAllBytes(selectedBinariesPath + "FileChecksumCalculator.dll", Resources.FileChecksumCalculator);
      File.WriteAllBytes(selectedBinariesPath + "FileLocker.dll", Resources.FileLocker);
      File.WriteAllBytes(selectedBinariesPath + "FileRightsChecker.dll", Resources.FileRightsChecker);
      File.WriteAllBytes(selectedBinariesPath + "GeneralData.dll", Resources.GeneralData);
      File.WriteAllBytes(selectedBinariesPath + "InclusionExclusionRules.dll", Resources.InclusionExclusionRules);
      File.WriteAllBytes(selectedBinariesPath + "InterCommunicationStructures.dll", Resources.InterCommunicationStructures);
      File.WriteAllBytes(selectedBinariesPath + "LinqToHashSet.dll", Resources.LinqToHashSet);
      File.WriteAllBytes(selectedBinariesPath + "LogEntry.dll", Resources.LogEntry);
      File.WriteAllBytes(selectedBinariesPath + "OxigenLE.exe", Resources.OxigenLE);
      File.WriteAllText(selectedBinariesPath + "OxigenLE.exe.config", Resources.OxigenLE_exe, Encoding.ASCII);
      File.WriteAllBytes(selectedBinariesPath + "LogFileReader.dll", Resources.LogFileReader);
      File.WriteAllBytes(selectedBinariesPath + "LoggerInfo.dll", Resources.LoggerInfo);
      File.WriteAllBytes(selectedBinariesPath + "LogStats.dll", Resources.LogStats);
      File.WriteAllBytes(selectedBinariesPath + "OxigenSingletons.dll", Resources.OxigenSingletons);
      File.WriteAllBytes(selectedBinariesPath + "OxigenCompiledRegexes.dll", Resources.OxigenCompiledRegexes);
      File.WriteAllBytes(selectedBinariesPath + "ScreenSaverConfig.exe", Resources.ScreenSaverConfig);
      File.WriteAllText(selectedBinariesPath + "ScreenSaverConfig.exe.config", Resources.ScreenSaverConfig_exe, Encoding.ASCII);
      File.WriteAllBytes(selectedBinariesPath + "OxigenServiceFactory.dll", Resources.OxigenServiceFactory);
      File.WriteAllBytes(selectedBinariesPath + "OxigenSingletons.dll", Resources.OxigenSingletons);
      File.WriteAllBytes(selectedBinariesPath + "OxigenTray.exe", Resources.OxigenTray);
      File.WriteAllText(selectedBinariesPath + "OxigenTray.exe.config", Resources.OxigenTray_exe);
      File.WriteAllBytes(selectedBinariesPath + "OxigenService.exe", Resources.OxigenService);
      File.WriteAllText(selectedBinariesPath + "OxigenService.exe.config", Resources.OxigenService_exe, Encoding.ASCII);
      File.WriteAllBytes(selectedBinariesPath + "OxigenUserDataMarshallerClient.dll", Resources.OxigenUserDataMarshallerClient);
      File.WriteAllBytes(selectedBinariesPath + "OxigenUserDataMarshallrContracts.dll", Resources.OxigenUserDataMarshallrContracts);
      File.WriteAllBytes(selectedBinariesPath + "OxigenUserFileMarDataContracts.dll", Resources.OxigenUserFileMarDataContracts);
      File.WriteAllBytes(selectedBinariesPath + "OxigenUserFileMarshallerClient.dll", Resources.OxigenUserFileMarshallerClient);
      File.WriteAllBytes(selectedBinariesPath + "OxigenUserMgmtServicesClient.dll", Resources.OxigenUserMgmtServicesClient);
      File.WriteAllBytes(selectedBinariesPath + "OxigenUserMgmtServicesContracts.dll", Resources.OxigenUserMgmtServicesContracts);
      File.WriteAllBytes(selectedBinariesPath + "PlayerType.dll", Resources.PlayerType);
      File.WriteAllBytes(selectedBinariesPath + "Playlist.dll", Resources.Playlist);
      File.WriteAllBytes(selectedBinariesPath + "PlaylistLogic.dll", Resources.PlaylistLogic);
      File.WriteAllBytes(selectedBinariesPath + "RegistryAccess.dll", Resources.RegistryAccess);
      File.WriteAllBytes(selectedBinariesPath + "ServerCycleConnectAttempt.dll", Resources.ServerCycleConnectAttempt);
      File.WriteAllBytes(selectedBinariesPath + "TaxonomySearch.dll", Resources.TaxonomySearch);
      File.WriteAllBytes(selectedBinariesPath + "TimeSpanObjectWrapper.dll", Resources.TimeSpanObjectWrapper);
      File.WriteAllBytes(selectedBinariesPath + "UserSettings.dll", Resources.UserSettings);
      File.WriteAllBytes(selectedBinariesPath + "WCFErrorReporting.dll", Resources.WCFErrorReporting);
      File.WriteAllBytes(selectedBinariesPath + "XmlSerializableGenericDictionary.dll", Resources.XmlSerializableGenericDictionary);
      File.WriteAllBytes(selectedBinariesPath + "XmlSerializableSortableGenericList.dll", Resources.XmlSerializableSortableGenericList);
      File.WriteAllBytes(selectedBinariesPath + "XMLSerializer.dll", Resources.XMLSerializer);

      // System
      File.WriteAllBytes(systemFolder + "AdvertList.dll", Resources.AdvertList);
      File.WriteAllBytes(systemFolder + "AssetLevel.dll", Resources.AssetLevel);
      File.WriteAllBytes(systemFolder + "Assets.dll", Resources.Assets);
      File.WriteAllBytes(systemFolder + "AssetScheduling.dll", Resources.AssetScheduling);
      File.WriteAllBytes(systemFolder + "AxInterop.QTOControlLib.dll", Resources.AxInterop_QTOControlLib);
      File.WriteAllBytes(systemFolder + "AxInterop.ShockwaveFlashObjects.dll", Resources.AxInterop_ShockwaveFlashObjects);
      File.WriteAllBytes(systemFolder + "AxInterop.WMPLib.dll", Resources.AxInterop_WMPLib);
      File.WriteAllBytes(systemFolder + "BoolContainerLib.dll", Resources.BoolContainerLib);
      File.WriteAllBytes(systemFolder + "ChannelData.dll", Resources.ChannelData);
      File.WriteAllBytes(systemFolder + "ChannelSubscriptions.dll", Resources.ChannelSubscriptions);
      File.WriteAllBytes(systemFolder + "DemographicRange.dll", Resources.DemographicRange);
      File.WriteAllBytes(systemFolder + "Demographic.dll", Resources.Demographic);
      File.WriteAllBytes(systemFolder + "EncryptionDecryption.dll", Resources.EncryptionDecryption);
      File.WriteAllBytes(systemFolder + "Exceptions.dll", Resources.Exceptions);
      File.WriteAllBytes(systemFolder + "FileLocker.dll", Resources.FileLocker);
      File.WriteAllBytes(systemFolder + "FileRightsChecker.dll", Resources.FileRightsChecker);
      File.WriteAllBytes(systemFolder + "GeneralData.dll", Resources.GeneralData);
      File.WriteAllBytes(systemFolder + "InclusionExclusionRules.dll", Resources.InclusionExclusionRules);
      File.WriteAllBytes(systemFolder + "LinqToHashSet.dll", Resources.LinqToHashSet);
      File.WriteAllBytes(systemFolder + "InternetConnectionCheck.dll", Resources.InternetConnectionCheck);
      File.WriteAllBytes(systemFolder + "Interop.QTOControlLib.dll", Resources.Interop_QTOControlLib);
      File.WriteAllBytes(systemFolder + "Interop.QTOLibrary.dll", Resources.Interop_QTOLibrary);
      File.WriteAllBytes(systemFolder + "Interop.SHDocVw.dll", Resources.Interop_SHDocVw);
      File.WriteAllBytes(systemFolder + "Interop.ShockwaveFlashObjects.dll", Resources.Interop_ShockwaveFlashObjects);
      File.WriteAllBytes(systemFolder + "Interop.WMPLib.dll", Resources.Interop_WMPLib);
      File.WriteAllBytes(systemFolder + "LinqToHashSet.dll", Resources.LinqToHashSet);
      File.WriteAllBytes(systemFolder + "LogEntry.dll", Resources.LogEntry);
      File.WriteAllBytes(systemFolder + "LogFileWriter.dll", Resources.LogFileWriter);
      File.WriteAllBytes(systemFolder + "LoggerInfo.dll", Resources.LoggerInfo);
      File.WriteAllBytes(systemFolder + "LogStats.dll", Resources.LogStats);
      File.WriteAllBytes(systemFolder + "LogWriteOperations.dll", Resources.LogWriteOperations);
      File.WriteAllBytes(systemFolder + "NoAssetsAnimator.dll", Resources.NoAssetsAnimator);
      File.WriteAllBytes(systemFolder + "OxigenCompiledRegexes.dll", Resources.OxigenCompiledRegexes);
      File.WriteAllBytes(systemFolder + "OxigenStopwatch.dll", Resources.OxigenStopwatch);
      File.WriteAllBytes(systemFolder + "Oxigen.scr", Resources.Oxigen);
      File.WriteAllText(systemFolder + "Oxigen.scr.config", Resources.Oxigen_scr, Encoding.ASCII);
      File.WriteAllBytes(systemFolder + "OxigenSingletons.dll", Resources.OxigenSingletons);
      File.WriteAllBytes(systemFolder + "PlayerType.dll", Resources.PlayerType);
      File.WriteAllBytes(systemFolder + "Playlist.dll", Resources.Playlist);
      File.WriteAllBytes(systemFolder + "PlaylistAssetPicker.dll", Resources.PlaylistAssetPicker);
      File.WriteAllBytes(systemFolder + "PlaylistLogic.dll", Resources.PlaylistLogic);
      File.WriteAllBytes(systemFolder + "RegistryAccess.dll", Resources.RegistryAccess);
      File.WriteAllBytes(systemFolder + "TaxonomySearch.dll", Resources.TaxonomySearch);
      File.WriteAllBytes(systemFolder + "UserSettings.dll", Resources.UserSettings);
      File.WriteAllBytes(systemFolder + "WCFErrorReporting.dll", Resources.WCFErrorReporting);
      File.WriteAllBytes(systemFolder + "XmlSerializableGenericDictionary.dll", Resources.XmlSerializableGenericDictionary);
      File.WriteAllBytes(systemFolder + "XmlSerializableSortableGenericList.dll", Resources.XmlSerializableSortableGenericList);
      File.WriteAllBytes(systemFolder + "XMLSerializer.dll", Resources.XMLSerializer);
    }

    private static void UpdateConfigurationFilesWithPaths(string selectedDataPath, string selectedBinariesPath, string systemFolder)
    {
      UpdateConfigFileWithPaths(systemFolder + "\\Oxigen.scr.config", selectedDataPath, selectedBinariesPath, "");
      UpdateConfigFileWithPaths(selectedBinariesPath + "\\OxigenLE.exe.config", selectedDataPath, selectedBinariesPath, "");
      UpdateConfigFileWithPaths(selectedBinariesPath + "\\OxigenCE.exe.config", selectedDataPath, selectedBinariesPath, "");
      UpdateConfigFileWithPaths(selectedBinariesPath + "\\ScreenSaverConfig.exe.config", selectedDataPath, selectedBinariesPath, "");
      UpdateConfigFileWithPaths(selectedBinariesPath + "\\OxigenService.exe.config", selectedDataPath, selectedBinariesPath, "");
      UpdateConfigFileWithPaths(selectedBinariesPath + "\\OxigenTray.exe.config", selectedDataPath, selectedBinariesPath, systemFolder);
    }

    private static void UpdateConfigFileWithPaths(string path, string selectedDataPath, 
      string selectedBinariesPath, string systemFolder)
    {
      string xmlFile = File.ReadAllText(path);

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

      File.WriteAllText(path, newConfig.ToString(), Encoding.ASCII);
    }

    private static void InstallSSGService(string winDir, string programFiles, string serviceName)
    {
      string cmdline = @"/C "" """ + winDir + @"Microsoft.NET\Framework\v2.0.50727\InstallUtil.exe"" """ + programFiles + @"Oxigen\bin\OxigenService.exe""";

      System.Diagnostics.Process.Start("cmd", cmdline);
    }

    private static void StartSSGService(string serviceName)
    {
      string cmdline = @"net start """ + serviceName + @""" "" ";

      System.Diagnostics.Process.Start("cmd", cmdline);
    }

    private static void InstallEventLog()
    {
      if (EventLog.Exists("Oxigen Services") || EventLog.SourceExists("Oxigen Service"))
      {
        if (EventLog.SourceExists("Oxigen Service"))
          EventLog.DeleteEventSource("Oxigen Service");

        if (EventLog.Exists("Oxigen Services"))
          EventLog.Delete("Oxigen Services");
      }

      EventLog.CreateEventSource("Oxigen Service", "Oxigen Services");

      Console.WriteLine("Event Log Oxigen Service created.");
    }

    //private static void InstallEventLog()
    //{
    //  if (EventLog.Exists("Oxigen Services") || EventLog.SourceExists("Oxigen Service"))
    //  {
    //    if (EventLog.SourceExists("Oxigen Service"))
    //      EventLog.DeleteEventSource("Oxigen Service");

    //    if (EventLog.Exists("Oxigen Services"))
    //      EventLog.Delete("Oxigen Services");
    //  }

    //  EventLog.CreateEventSource("Oxigen Service", "Oxigen Services");

    //  EventLog.DeleteEventSource("Oxigen Service");

    //  Console.WriteLine("Event Log Oxigen Service created."); 
    //}
  }
}
