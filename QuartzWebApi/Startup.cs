#if NET48
//using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Web.Http;
using Microsoft.Owin.Extensions;
using Owin;
#endif

#if NET6_0
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
#endif

using Quartz;

namespace QuartzWebApi;

public class Startup
{
    IScheduler _scheduler;

#if NET48
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
#endif

#if NET6_0
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseUrls("http://localhost:5000", "https://localhost:5001"); // Specify your desired URLs
                webBuilder.UseStartup<Startup>();
            });


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
#endif
}

#if NET48
public class JsonContentNegotiator(MediaTypeFormatter formatter) : IContentNegotiator
{
    public ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)  
    {  
        var result = new ContentNegotiationResult(formatter, new MediaTypeHeaderValue("application/json"));  
        return result;  
    }  
}
#endif
