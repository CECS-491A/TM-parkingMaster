using System.Web.Http;

namespace ParkingMaster.Manager
{
    public class WebApiConfigure
    {
        public static void Register(HttpConfiguration config)
        {


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { controller = "Testing", id = RouteParameter.Optional }
            );
        }
    }
}