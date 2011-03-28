using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OxigenIIAdvertising.UserDataMarshallerServiceClient;
using System.ServiceModel;
using ServiceErrorReporting;
using System.IO;
using OxigenIIAdvertising.XMLSerializer;
using System.Xml.Serialization;
using OxigenIIAdvertising.UserManagementServicesServiceClient;
using InterCommunicationStructures;
using OxigenIIAdvertising.LoggerInfo;
using OxigenIIMasterDataMarshallerClient;
using System.Diagnostics;
using OxigenIIAdvertising.DataAggregators;
using OxigenIIAdvertising.AppData;
using OxigenIIAdvertising.DAClients;
using OxigenIIAdvertising.Demographic;

namespace LibraryTester
{
  public partial class TryWebService : Form
  {
    public TryWebService()
    {
      InitializeComponent();
    }

    EventLog log;
    
    Logger _logger = new Logger("Tester", @"C:\OxigenData\SettingsData\OxigenDebug.txt", LoggingMode.Debug);

    private void btnClick_Click(object sender, EventArgs e)
    {
      //UserDataMarshallerClient client = new UserDataMarshallerClient();
      //client.Endpoint.Address = new EndpointAddress(@"http://relay-logs-a-1.obs-group.co.uk:81/Userdatamarshaller.svc");

      //DateTimeErrorWrapper dt = client.GetCurrentServerTime("password");

      //client.Dispose();

      try
      {

        DAClient client = new DAClient();

        DemographicData dg = client.GetUserDemographicData("31350825-4D31-48E3-8AFE-0F32C1D53096_A");

        client.Dispose();

        MessageBox.Show(dg.SocioEconomicgroup[0]);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
    }

    private void SaveStreamAndDispose(Stream stream, string filePath)
    {
      FileStream fileStream = null;

      try
      {
        fileStream = new FileStream(filePath, FileMode.OpenOrCreate);

        _logger.WriteMessage(DateTime.Now.ToString() + " successfully opened a filestream for " + filePath);

        SaveStream(stream, fileStream);

        _logger.WriteMessage(DateTime.Now.ToString() + " successfully saved " + filePath);
      }
      catch (Exception ex)
      {
        _logger.WriteError(ex);
      }
      finally
      {
        if (stream != null)
          stream.Dispose();

        if (fileStream != null)
          fileStream.Dispose();
      }
    }

    private void SaveStream(Stream readStream, FileStream writeStream)
    {
      int length = 256;
      byte[] buffer = new byte[length];
      int bytesRead = readStream.Read(buffer, 0, length);

      while (bytesRead > 0)
      {
        writeStream.Write(buffer, 0, bytesRead);
        bytesRead = readStream.Read(buffer, 0, length);
      }
    }

    public MemoryStream SerializeToMemoryStream(object obj)
    {
      MemoryStream ms = new MemoryStream();
      TextWriter writer = null;

      XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());

      try
      {
        writer = new StreamWriter(ms); 

        xmlSerializer.Serialize(writer, obj);

        ms.Position = 0;
      }
      finally
      {
        //if (writer != null)
        //  writer.Dispose();
      }

      return ms;
    }
  }
}
