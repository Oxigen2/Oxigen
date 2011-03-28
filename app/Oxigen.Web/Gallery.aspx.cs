using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace OxigenIIPresentation
{
  public partial class Gallery : System.Web.UI.Page
  {
    //This variable specifies relative path to the folder, where the gallery with uploaded files is located.
    //Do not forget about the slash in the end of the folder name.
    public string galleryPath = "Gallery/";

    private void Page_Load(System.Object sender, System.EventArgs e)
    {
      XmlDocument descriptions = new XmlDocument();
      descriptions.Load(Server.MapPath(galleryPath + "Descriptions.xml"));

      DataList1.DataSource = descriptions.DocumentElement.ChildNodes;
      DataList1.DataBind();
    }

    public string EncodeFileName(string value)
    {
      return Server.UrlEncode(value).Replace("+", "%20");
    }
  }
}
