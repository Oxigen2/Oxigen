using System.Web.Mvc;
using System.Web.Routing;

namespace Oxigen.Web.Controllers
{
    public class RouteRegistrar
    {
        public static void RegisterRoutesTo(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
            routes.IgnoreRoute("get.ox");
            routes.IgnoreRoute("put.ox");
            routes.IgnoreRoute("");
            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }  // Parameter defaults
            );
        }
    }

    public class RootAreaRegistration : System.Web.Mvc.AreaRegistration
    {
        public override string AreaName {
            get {
                return "Root";
            }
        }

        public override void RegisterArea(System.Web.Mvc.AreaRegistrationContext context) {
            context.MapRoute(
                    "Root_default",
                    "{controller}/{action}/{id}",
                    new { action = "Index", id = "" }
                );
        }
    }

}
