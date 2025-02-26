using System.Net.Http.Headers;
using System.Web.Http;

using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using MyLottoCheck.Models;
namespace MyLottoCheck
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            //// Web API routes
            //config.MapHttpAttributeRoutes();

            // Web API2 odata configuration and services
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<CaliforniaMegaMillionsAllWinningNumber>("CaliforniaMegaMillionsWinningNumbers");
            config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
