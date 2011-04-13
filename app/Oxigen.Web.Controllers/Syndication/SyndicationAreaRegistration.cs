
namespace Oxigen.Web.Controllers.Syndication
{

public class SyndicationAreaRegistration : System.Web.Mvc.AreaRegistration
{
    public override string AreaName {
        get {
            return "Syndication";
        }
    }

    public override void RegisterArea(System.Web.Mvc.AreaRegistrationContext context) {
        context.MapRoute(
                "Syndication_default",
                "Syndication/{controller}/{action}/{id}",
                new { action = "Index", id = "" }
            );
    }
}

} 