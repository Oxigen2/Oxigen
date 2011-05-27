using System;
using System.IO;
using System.Reflection;
using System.Text;
using log4net;
using log4net.Repository.Hierarchy;
using NUnit.Framework;
using OxigenIIUserDataMarshallrService;
using Rhino.Mocks;
using SharpArch.Testing.NUnit;
using System.Collections.Generic;


namespace Tests.Oxigen.UserDataMarshaller
{
  [TestFixture]
  public class ClientDataLoggerTests
  {
    [SetUp]
    public void SetUp()
    {

    }
    [Test]
    public void LogNormalImpressionsOrClicksTest()
    {
      // Establish Context
      Stream stream = new MemoryStream(Encoding.Default.GetBytes(
        @"23/02/2011 00:00:00|9935|1
          23/02/2011 00:00:00|1492|1
          11/02/2011 00:00:00|10685|1
          21/03/2011 00:00:00|11442|1"));

      ILog logForNet = MockRepository.GenerateMock<ILog>();

      ClientDataLogger.SystemTime.Now = () => DateTime.Parse("2011-05-24 17:40:57");
      logForNet.Expect(r => r.Info("2011-05-24 17:40:57|PCGUID|ChannelID|2011-02-23|9935|1"));
      logForNet.Expect(r => r.Info("2011-05-24 17:40:57|PCGUID|ChannelID|2011-02-23|1492|1"));
      logForNet.Expect(r => r.Info("2011-05-24 17:40:57|PCGUID|ChannelID|2011-03-21|11442|1"));
      logForNet.Expect(r => r.Info("2011-05-24 17:40:57|PCGUID|ChannelID|2011-02-11|10685|1"));

      //Act
      var clientDateLogger = new ClientDataLogger();
      clientDateLogger.LogImpressionsOrClicks(logForNet, "PCGUID", "ChannelID", stream);

      //Assert
      logForNet.VerifyAllExpectations();
    }

    [Test]
    public void CanHandleExtraNewLineAtEndOfImpressionsOrClicksTest()
    {
      // Establish Context
      Stream stream = new MemoryStream(Encoding.Default.GetBytes(
        @"23/02/2011 00:00:00|9935|1
          "));

      ILog logForNet = MockRepository.GenerateMock<ILog>();

      ClientDataLogger.SystemTime.Now = () => DateTime.Parse("2011-05-24 17:40:57");
      logForNet.Expect(r => r.Info("2011-05-24 17:40:57|PCGUID|ChannelID|2011-02-23|9935|1"));


      //Act
      var clientDateLogger = new ClientDataLogger();
      clientDateLogger.LogImpressionsOrClicks(logForNet, "PCGUID", "ChannelID", stream);

      //Assert
      logForNet.VerifyAllExpectations();
    }

    [Test]
    public void Ignore12HourDateFormatForImpressionsOrClicksTest()
    {
      // Establish Context
      Stream stream = new MemoryStream(Encoding.Default.GetBytes(
        @"12/18/2010 12:00:00 AM|9935|1
          23/02/2011 00:00:00|9935|1"));

      ILog logForNet = MockRepository.GenerateStrictMock<ILog>();

      ClientDataLogger.SystemTime.Now = () => DateTime.Parse("2011-05-24 17:40:57");
      logForNet.Expect(r => r.Info("2011-05-24 17:40:57|PCGUID|ChannelID|2011-02-23|9935|1"));

      //Act
      var clientDateLogger = new ClientDataLogger();
      clientDateLogger.LogImpressionsOrClicks(logForNet, "PCGUID", "ChannelID", stream);

      //Assert
      logForNet.VerifyAllExpectations();
    }

    [Test]
    public void LogGeneralUsageTest()
    {
      // Establish Context
      Stream stream = new MemoryStream(Encoding.Default.GetBytes(@"<?xml version=""1.0"" encoding=""us-ascii""?>
          <UsageCount xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
            <MachineGUID>563173AD-8835-4138-B4B0-FF7C5BD8D163_Y</MachineGUID>
            <UserGUID>71536A57-C890-4FDB-9A31-5359116B0F4D_Z</UserGUID>
            <NoClicks>1</NoClicks>
            <NoScreenSaverSessions>1</NoScreenSaverSessions>
            <TotalPlayTime>94</TotalPlayTime>
          </UsageCount>"));
 
      ILog logForNet = MockRepository.GenerateMock<ILog>();

      ClientDataLogger.SystemTime.Now = () => DateTime.Parse("2011-05-24 17:40:57");
      logForNet.Expect(r => r.Info("2011-05-24 17:40:57|563173AD-8835-4138-B4B0-FF7C5BD8D163_Y|1|1|94"));
      
      //Act
      var clientDateLogger = new ClientDataLogger();
      clientDateLogger.LogGeneralUsage(logForNet, stream);

      //Assert
      logForNet.VerifyAllExpectations();
    }

    [Test]
    public void HandleWhiteSpacesForLogGeneralUsageTest()
    {
      // Establish Context
      byte[] usageCountBytes =
        Encoding.Default.GetBytes(@"<?xml version=""1.0"" encoding=""us-ascii""?>
          <UsageCount xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
            <MachineGUID>563173AD-8835-4138-B4B0-FF7C5BD8D163_Y</MachineGUID>
            <UserGUID>71536A57-C890-4FDB-9A31-5359116B0F4D_Z</UserGUID>
            <NoClicks>1</NoClicks>
            <NoScreenSaverSessions>1</NoScreenSaverSessions>
            <TotalPlayTime>94</TotalPlayTime>
          </UsageCount>");

      byte[] arrayWithExtraSpace = new byte[usageCountBytes.Length + 1];

      Array.Copy(usageCountBytes, arrayWithExtraSpace, usageCountBytes.Length);
      arrayWithExtraSpace[usageCountBytes.Length] = 0x00;

      Stream stream = new MemoryStream(arrayWithExtraSpace);

 
      ILog logForNet = MockRepository.GenerateMock<ILog>();

      ClientDataLogger.SystemTime.Now = () => DateTime.Parse("2011-05-24 17:40:57");
      logForNet.Expect(r => r.Info("2011-05-24 17:40:57|563173AD-8835-4138-B4B0-FF7C5BD8D163_Y|1|1|94"));
      
      //Act
      var clientDateLogger = new ClientDataLogger();
      clientDateLogger.LogGeneralUsage(logForNet, stream);

      //Assert
      logForNet.VerifyAllExpectations();
    }
  

    [Test]
    public void LogAdImpressionOrClickChannelProportionTest()
    {
      // Establish Context
      Stream stream = new MemoryStream(Encoding.Default.GetBytes(
        @"1103|1000|0.7692307
          1103|1001|3.076923
          1103|1002|3.846154
          1103|1003|1.538461
          1103|1004|0.7692307"));

      ILog logForNet = MockRepository.GenerateMock<ILog>();

      ClientDataLogger.SystemTime.Now = () => DateTime.Parse("2011-05-24 17:40:57");
      logForNet.Expect(r => r.Info("2011-05-24 17:40:57|PCGUID|1103|1000|0.7692307"));
      logForNet.Expect(r => r.Info("2011-05-24 17:40:57|PCGUID|1103|1001|3.076923"));
      logForNet.Expect(r => r.Info("2011-05-24 17:40:57|PCGUID|1103|1002|3.846154"));
      logForNet.Expect(r => r.Info("2011-05-24 17:40:57|PCGUID|1103|1004|0.7692307"));

      //Act
      var clientDateLogger = new ClientDataLogger();
      clientDateLogger.LogAdImpressionOrClickChannelProportion(logForNet, "PCGUID", stream);

      //Assert
      logForNet.VerifyAllExpectations();
    }

    [Test]
    public void LogLatestSoftwareVersionInfoTest()
    {
      // Establish Context
      ILog logForNet = MockRepository.GenerateMock<ILog>();

      ClientDataLogger.SystemTime.Now = () => DateTime.Parse("2011-05-24 17:40:57");
      logForNet.Expect(r => r.Info("2011-05-24 17:40:57|PCGUID|1.8"));

      //Act
      var clientDateLogger = new ClientDataLogger();
      clientDateLogger.LogLatestSoftwareVersionInfo(logForNet, "PCGUID", "1.8");

      //Assert
      logForNet.VerifyAllExpectations();
    }

    private static byte[] GetResourceBytes(string resourceNameArg)
    {
      Assembly assembly = Assembly.GetExecutingAssembly();
      Stream stream = assembly.GetManifestResourceStream(resourceNameArg);
      var bytes = new byte[stream.Length];
      stream.Read(bytes, 0, (int)stream.Length);
      return bytes;
    }
  }
}
