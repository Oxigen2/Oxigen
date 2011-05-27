using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using log4net;

namespace OxigenIIUserDataMarshallrService
{
  public class ClientDataLogger
  {
    public static class SystemTime
    {
      public static Func<DateTime> Now = () => DateTime.Now;
    }


    public void LogImpressionsOrClicks(ILog logForNet, string pcguid, string channelid, Stream stream)
    {
      
      var sr = new StreamReader(stream, Encoding.ASCII);

      var file = sr.ReadToEnd();
      try
      {
        var logs = file.Split('\n');
        foreach (string log in logs)
        {
          var trimlog = log.Trim();
          if (trimlog == "") continue;
          string[] columns = trimlog.Split('|');
          if (columns[0].EndsWith("AM")) continue;

          string row = SystemTime.Now().ToString("yyyy-MM-dd HH:mm:ss") + "|" + pcguid + "|" + channelid + "|" +
                       DateTime.Parse(columns[0]).ToString("yyyy-MM-dd") + "|" + columns[1] + "|" + columns[2];
          logForNet.Info(row);
        }
      }
      catch (Exception ex)
      {
        throw new Exception(file, ex);
      }

    }

    public void LogGeneralUsage(ILog logForNet, Stream stream)
    {
      try
      {
        var reader = new XmlTextReader(stream);
        
        var row = new string[5];
        row[0] = SystemTime.Now().ToString("yyyy-MM-dd HH:mm:ss");
        while (reader.Read())
        {
          if (reader.NodeType == XmlNodeType.Element)
          {
            var elementName = reader.Name;
            reader.Read();
            switch (elementName)
            {
              case "MachineGUID":
                row[1] = reader.Value;
                break;
              case "NoClicks":
                row[2] = reader.Value;
                break;
              case "NoScreenSaverSessions":
                row[3] = reader.Value;
                break;
              case "TotalPlayTime":
                row[4] = reader.Value;
                break;
            }
          }
        }

        logForNet.Info(String.Join("|", row));
      }
      catch (Exception ex)
      {
        stream.Position = 0;
        throw new Exception("XML was: " + new StreamReader(stream, Encoding.ASCII).ReadToEnd(), ex);
      }
    }

    public void LogAdImpressionOrClickChannelProportion(ILog logForNet, string pcguid, Stream stream)
    {
      var sr = new StreamReader(stream, Encoding.ASCII);

      string[] logs = sr.ReadToEnd().Split('\n');

      foreach (string log in logs)
      {
        var row = SystemTime.Now().ToString("yyyy-MM-dd HH:mm:ss") + "|" + pcguid + "|" + log.Trim();
        logForNet.Info(row);
      }
    }

    public void LogLatestSoftwareVersionInfo(ILog logForNet, string pcguid, string versionInfo)
    {
      logForNet.Info(SystemTime.Now().ToString("yyyy-MM-dd HH:mm:ss") + "|" + pcguid + "|" + versionInfo);
    }
  }
}
