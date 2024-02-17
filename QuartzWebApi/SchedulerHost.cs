#if NET48
//using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using Microsoft.Owin.Hosting;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
#endif

#if NET6_0
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
#endif

using Microsoft.Extensions.Logging;
using Quartz;

namespace QuartzWebApi;

/// <summary>
///     Acts as a host for the Web API
/// </summary>
/// <remarks>
///    Create a new instance of the <see cref="SchedulerHost"/> class
/// </remarks>
/// <param name="baseAddress">The base address, e.g. http://localhost:45000</param>
/// <param name="scheduler"><see cref="IScheduler"/></param>
/// <param name="logger">><see cref="ILogger"/></param>
public class SchedulerHost(string baseAddress, IScheduler scheduler, ILogger logger)
{
    #region Fields
#if NET48
    private IDisposable _webApp;
#endif
#if NET6_0
    private IWebHost _webHost;
#endif    
    #endregion

    #region Start
    /// <summary>
    ///     Start the Web API
    /// </summary>
    public void Start()
    {
#if NET48
        Startup.Scheduler = scheduler;
        Startup.Logger = logger;
        _webApp = WebApp.Start<Startup>(baseAddress);
#endif

#if NET6_0
        var builder = new WebHostBuilder()
            .UseKestrel()
            .ConfigureServices(services => services.AddSingleton(scheduler))
            .UseStartup<Startup>()
            .UseUrls(baseAddress)
            .ConfigureLogging(logging  => logging.AddConsole());

        if (logger != null)
            builder.ConfigureServices(services => services.AddSingleton(logger));

        _webHost = builder.Build();
        _webHost.Start();
#endif
    }
    #endregion

    #region Stop
    /// <summary>
    ///     Stops the Web API
    /// </summary>
    public void Stop()
    {
#if NET48
        _webApp?.Dispose();
#endif
#if NET6_0
       _webHost?.StopAsync();
#endif
    }
    #endregion
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