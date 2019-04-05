using System.Web.Http;
using System.Web.Http.Cors;
using System.Net.Http.Headers;

namespace ParkingMaster.Manager
{
    public class WebApiConfigure
    {
        public static void Register(HttpConfiguration config)
        {
            // Enable CORS globally accross all controllers
            var cors = new EnableCorsAttribute(origins: "*", headers: "*", methods: "*");

            // Web API configuration and services
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.EnableCors(cors);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}