﻿using System.Web.Mvc;
using System.Web.Routing;

namespace Oxigen.Web.Controllers
{
    public class RouteRegistrar
    {
        public static void RegisterRoutesTo(RouteCollection routes)
        {
            routes.Add(new MasterLegacyUrlRoute());
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
            routes.IgnoreRoute("get.ox");
            routes.IgnoreRoute("put.ox");
            routes.IgnoreRoute("");
            routes.MapRoute("Log", "log/{logName}/{userRef}/{message}", new { controller = "Logs", action = "Log" });
            routes.MapRoute("Installer", "installer/{subscription}", new { controller = "Download", action = "Installer" });
            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }  // Parameter defaults
            );
        }
    }

}
