using Elastic.Apm.Extensions.Hosting;
using MZ_WorkerService;
using MZ_WorkerService.Extensions;
using MZ_WorkerService.Helpers;
using MZ_WorkerService.Services;
using Serilog;
using System.Net;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .UseElasticApm()
    .UseSerilog(Logger.ConfigureLogger)
    .ConfigureServices(services =>
    {
        services.AddHttpClient();
        services.AddHostedService<Worker>();

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

        var coreTypes = typeof(Service<,,,>).Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOfRawGeneric(typeof(Service<,,,>)))
                .ToList();

        foreach (var serviceType in coreTypes)
        {
            services.AddScoped(serviceType);
        }
       
    })
    .Build();



await host.RunAsync();
