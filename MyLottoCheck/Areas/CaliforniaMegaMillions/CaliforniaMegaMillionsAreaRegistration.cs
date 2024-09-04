using System.Web.Mvc;

namespace MyLottoCheck.Areas.CaliforniaMegaMillions
{
    public class CaliforniaMegaMillionsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CaliforniaMegaMillions";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            //context.MapRoute(
            //name: "CaliforniaMegaMillions",
            //url: "{controller}/{action}/{id}",
            //defaults: new
            //{
            //    area = "CaliforniaMegaMillions",
            //    controller = "Home",
            //    action = "Index",
            //    id = UrlParameter.Optional
            //});

            context.MapRoute(
                "CaliforniaMegaMillions",
                "CaliforniaMegaMillions/{controller}/{action}/{id}",
                new { area = "CaliforniaMegaMillions", controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}