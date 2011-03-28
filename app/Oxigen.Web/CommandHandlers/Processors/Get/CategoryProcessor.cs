using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using OxigenIIAdvertising.SOAStructures;
using OxigenIIAdvertising.BLClients;
using System.Text;
using System.Web.SessionState;

namespace OxigenIIPresentation.CommandHandlers.Processors.Get
{
  public class CategoryProcessor : GetCommandProcessor
  {
    public CategoryProcessor(HttpSessionState session) : base(session) { }

    internal override string Execute(NameValueCollection commandParameters)
    {
      int categoryID;

      string error = ValidateIntParameter(commandParameters, "categoryId", out categoryID);

      if (error != string.Empty)
        return error;

      List<Category> categoryList;

      BLClient client = null;

      // call WCF BLL Method
      try
      {
        client = new BLClient();

        categoryList = client.GetCategoryList(categoryID);
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

      return Flatten(categoryList);
    }

    private string Flatten(List<Category> categoryList)
    {
      if (categoryList == null)
        return String.Empty;

      StringBuilder sb = new StringBuilder();

      foreach (Category category in categoryList)
      {
        sb.Append(category.CategoryID);
        sb.Append(",,");
        sb.Append(category.CategoryName);
        sb.Append(",,");
        sb.Append(category.HasChildren);
        sb.Append("||");
      }

      return sb.ToString().TrimEnd(new char[] { '|' });
    }
  }
}
