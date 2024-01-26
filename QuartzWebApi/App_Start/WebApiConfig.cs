using System;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json;

namespace QuartzWebApi;

public static class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
        // Web API configuration and services

        // Web API routes
        config.MapHttpAttributeRoutes();

        //config.Routes.MapHttpRoute(
        //    name: "DefaultApi",
        //    routeTemplate: "api/{controller}/{id}",
        //    defaults: new { id = RouteParameter.Optional }
        //);

        GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings
            .Add(new RequestHeaderMapping("Accept",
                "text/html",
                StringComparison.InvariantCultureIgnoreCase,
                true,
                "application/json"));

        config.Formatters.Remove(config.Formatters.XmlFormatter);
        config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings();
    }
}