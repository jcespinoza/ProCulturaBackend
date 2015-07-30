using System.Web.Http;

namespace ProCultura.Web.Api
{
    using System.Web.Http.Cors;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            var cors = new EnableCorsAttribute("localhost:8090, procultura.herokuapp.com", "*", "*");
            config.EnableCors(cors);
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling =
                Newtonsoft.Json.PreserveReferencesHandling.Objects;

            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
