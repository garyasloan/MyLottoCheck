using System.Web.Mvc;
using System.Web.Routing;

namespace MyLottoCheck
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //routes.MapRoute(
            //name: "Default",
            //url: "",
            //defaults: new { area = "CaliforniaMegaMillions", controller = "Home", action = "Index" });

            //routes.MapRoute(
            //name: "CaliforniaMegaMillions1",
            //url: "{area}/{controller}/{action}",
            //defaults: new { area="CaliforniaMegaMillions", controller = "Home", action= "Index" });

            //routes.MapRoute(
            //name: "CaliforniaMegaMillions1",
            //url: "{area}/{controller}/{action}",
            //defaults: new { area = "CaliforniaMegaMillions", controller = "Home", action = "Index" });

            routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            ).DataTokens = new RouteValueDictionary(new { area = "CaliforniaMegaMillions" });

            routes.MapRoute(
            name: "Root",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
            name: "Logoff",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Account", action = "Logoff", id = UrlParameter.Optional });

            //routes.MapRoute(
            //name: "DefaultLogin",
            //url: "{controller}/{action}",
            //defaults: new { controller = "Account", action = "Login"});
        }
    }
}
