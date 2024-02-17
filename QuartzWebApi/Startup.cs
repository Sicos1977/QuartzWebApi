#if NET48
//using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Web.Http;
using Owin;
using Microsoft.Owin.Hosting;
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

using ILogger = Microsoft.Extensions.Logging.ILogger;
using Quartz;
// ReSharper disable UnusedMember.Global

namespace QuartzWebApi;

#if NET48
public class Startup
#endif
#if NET6_0
public class Startup
#endif
{

#if NET48
    private static IScheduler _scheduler;
    private static ILogger _logger;

    /// <summary>
    ///     Start the Web API and return a disposable object to stop it
    /// </summary>
    /// <param name="baseAddress">The base address, e.g. http://localhost:45000</param>
    /// <param name="scheduler"><see cref="IScheduler"/></param>
    /// <param name="logger">><see cref="ILogger"/></param>
    /// <returns></returns>
    public static IDisposable Start(string baseAddress, IScheduler scheduler, ILogger logger)
    {
        _scheduler = scheduler;
        _logger = logger;
        return WebApp.Start<Startup>(url: baseAddress);
    }

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
        var controllerFactory = new Func<HttpRequestMessage, IHttpController>(_ => new SchedulerController(_scheduler, _logger));
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
    ///     Starts Kestrel 
    /// </summary>
    /// <param name="baseAddress">The base address, e.g. http://localhost:45000</param>
    /// <param name="scheduler"><see cref="IScheduler"/></param>
    /// <param name="logger">><see cref="ILogger"/></param>
    /// <returns></returns>
    public static void Start(string baseAddress, IScheduler scheduler, ILogger logger)
    {
        var builder = new WebHostBuilder()
            .UseKestrel()
            .ConfigureServices(services => services.AddSingleton(scheduler))
            .UseStartup<Startup>()
            .UseUrls(baseAddress)
            .ConfigureLogging(logging  => logging.AddConsole());

        if (logger != null)
            builder.ConfigureServices(services => services.AddSingleton(logger));

        builder.Build().Run();
    }

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

#if NET48
internal class JsonContentNegotiator(MediaTypeFormatter formatter) : IContentNegotiator
{
    public ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)  
    {  
        var result = new ContentNegotiationResult(formatter, new MediaTypeHeaderValue("application/json"));  
        return result;  
    }  
}

internal class CustomHttpControllerActivator(Func<HttpRequestMessage, IHttpController> controllerFactory) : IHttpControllerActivator
{
    private readonly Func<HttpRequestMessage, IHttpController> _controllerFactory = controllerFactory ?? throw new ArgumentNullException(nameof(controllerFactory));

    public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
    {
        return _controllerFactory(request);
    }
}
#endif