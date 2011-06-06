using System.Web.Mvc;
using System.Web.Routing;

namespace Oxigen.Web.Assets
{
  public class RouteRegistrar
  {
    public static void RegisterRoutesTo(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
      routes.IgnoreRoute("");

      routes.MapRoute(
        "Default",                                // Route name
        "{action}/{filename}",
        new { controller = "Servecontent" });     // URL with parameters
    }
  }
}