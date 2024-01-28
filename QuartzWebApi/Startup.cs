using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Owin;
using Quartz;

namespace QuartzWebApi;

public class Startup
{
    IScheduler _scheduler;

    public void Configuration(IAppBuilder appBuilder)
    {
        var httpConfiguration = new HttpConfiguration();
        httpConfiguration.Formatters.Clear();
        httpConfiguration.Formatters.Add(new JsonMediaTypeFormatter());

        var jsonFormatter = new JsonMediaTypeFormatter();
        httpConfiguration.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(jsonFormatter));

        httpConfiguration.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "scheduler/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
        );

        appBuilder.UseWebApi(httpConfiguration);
        appBuilder.UseStageMarker(PipelineStage.MapHandler);
    }
}

public class JsonContentNegotiator(MediaTypeFormatter formatter) : IContentNegotiator
{
    public ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)  
    {  
        var result = new ContentNegotiationResult(formatter, new MediaTypeHeaderValue("application/json"));  
        return result;  
    }  
}