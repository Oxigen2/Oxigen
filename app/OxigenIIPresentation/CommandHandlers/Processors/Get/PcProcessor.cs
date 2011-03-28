using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using OxigenIIAdvertising.BLClients;
using OxigenIIAdvertising.SOAStructures;
using System.Text;
using System.Web.SessionState;

namespace OxigenIIPresentation.CommandHandlers.Processors.Get
{
  public class PcProcessor : GetCommandProcessor
  {
    public PcProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(NameValueCollection commandParameters)
    {
      int userID = -1;
      string pcProfileToken = null;

      Helper.TryGetUserID(_session, out userID);

      if (userID == -1 && _session["PcProfileToken"] == null)
      {
        pcProfileToken = System.Guid.NewGuid().ToString();

        _session.Add("PcProfileToken", pcProfileToken);
      }
      else
        pcProfileToken = (string)_session["PcProfileToken"];

      List<PC> endUserMachineList;

      BLClient client = null;

      try
      {
        client = new BLClient();

        endUserMachineList = client.GetPcList(userID, pcProfileToken);
      }
      catch (Exception exception)
      {
        return ErrorWrapper.SendError(exception.Message);
      }
      finally
      {
        if (client != null)
          client.Dispose();
      }

      return Flatten(endUserMachineList);
    }

    private string Flatten(List<PC> endUserMachineList)
    {
      if (endUserMachineList == null)
        return String.Empty;

      StringBuilder sb = new StringBuilder();

      foreach (PC endUserMachine in endUserMachineList)
      {
        sb.Append(endUserMachine.PCID);
        sb.Append(",,");
        sb.Append(endUserMachine.Name);
        sb.Append(",,");
        sb.Append(endUserMachine.LinkedToClient ? "0" : "1");
        sb.Append("||");
      }

      return sb.ToString().TrimEnd(new char[] { '|' });
    }
  }
}
