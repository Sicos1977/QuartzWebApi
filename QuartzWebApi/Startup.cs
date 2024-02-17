
using Microsoft.Extensions.Logging;
using Quartz;
#if NET48
using Owin;
using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using QuartzWebApi.Controllers;
#endif

#if NET6_0
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
#endif

namespace QuartzWebApi;

/// <summary>
///     The startup class for OWIN or Kestrel
/// </summary>
internal class Startup
{
    internal static IScheduler Scheduler;
    internal static ILogger Logger;

#if NET48
    /// <summary>
    ///     Configure the Web API that is hosted by OWIN
    /// </summary>
    /// <param name="app"></param>
    public void Configuration(IAppBuilder app)
    {
        var config = new HttpConfiguration();
        config.MapHttpAttributeRoutes();
        config.Formatters.Clear();
        config.Formatters.Add(new JsonMediaTypeFormatter());
        var jsonFormatter = new JsonMediaTypeFormatter();
        config.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(jsonFormatter));
        var controllerFactory = new Func<HttpRequestMessage, IHttpController>(_ => new SchedulerController(Scheduler, Logger));
        config.Services.Replace(typeof(IHttpControllerActivator), new CustomHttpControllerActivator(controllerFactory));

        config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
        );

        app.UseErrorPage();
        app.UseWebApi(config);
    }
#endif

#if NET6_0
    /// <summary>
    ///     Configure the service
    /// </summary>
    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
        services.AddRouting();
        services.AddControllers();
        return services.BuildServiceProvider();
    }

    /// <summary>
    ///     Configure the application
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // Map controllers for API endpoints
        });
    }
#endif
}