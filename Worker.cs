using Experimental.System.Messaging;
using Serilog;
using Serilog.Context;
using MZ_WorkerService.Extensions;
using MZ_WorkerService.Logging;
using MZ_WorkerService.Models.Mantiz;
using MZ_WorkerService.Queue;
using MZ_WorkerService.Services;
using MZ_WorkerService.Xml;
using System.Globalization;
using System.Text;

namespace MZ_WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public static IConfiguration? Configuration { get; private set; }

        // Diccionario que contiene los servicios registrados
        private Dictionary<string, Type>? Services { get; set; }

        // Lista que contiene las tareas que se estan ejecutando.
        private List<Task> ServiceTasks { get; set; }

        public Worker(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration)
        {
            _serviceScopeFactory = serviceScopeFactory;
            Configuration = configuration;
            Log.Logger = new Logger().ConfigureLogger();
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            ServiceTasks = new List<Task>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            RegisterServices();
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                // Esperar a que terminen todas las tareas en ejecucion, por un tiempo determinado.
                Task.WaitAll(ServiceTasks.ToArray(), 3000);
            }
            catch (Exception e)
            {
                Log.Error(e, "Error al esperar a que terminen los servicios");
            }

            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Quitar de la lista todas las tareas que ya terminaron de ejecutarse.
                    ServiceTasks.RemoveAll(t => t.IsCompleted);

                    var mq = new MantizMQ();

                    // Leer de la cola el mensaje y su ID.
                    var requestXml = await mq.Listen(TimeSpan.FromSeconds(5), stoppingToken);
                    var messageId = mq.GetMessageId();
                    var request = new MantizRoot();
                    
                    if (string.IsNullOrEmpty(requestXml))
                    {
                        continue;
                    }

                    request = Serializer<MantizRoot>.DeserializeString(requestXml);

                    if (request is null)
                    {
                        var response = new MantizRoot
                        {
                            Response = new MantizResponse
                            {
                                CodigoRespuesta = "000001",
                                MensajeRespuesta = "Request invalido"
                            }
                        };

                        var responseXml = Serializer<MantizRoot>.SerializeToString(response);
                        
                        var mqResponse = new MantizMQ(isResponse: true);
                        mqResponse.Write(responseXml, messageId);

                        continue;
                    }

                    if (string.IsNullOrEmpty(request.Request?.IdServicio))
                    {
                        Log.Error("IdServicio vacio");
                        continue;
                    }

                    // Agregar las propiedades IdSolicitud y IdServicio al contexto del log.
                    // Estas propiedades pueden utilizarse en los destinos del log.
                    using (LogContext.PushProperty("IdSolicitud", request.Request?.IdSolicitud))
                    using (LogContext.PushProperty("IdServicio", request.Request?.IdServicio))
                    using (LogContext.PushProperty("Version", request.Request?.Version))
                    using (LogContext.PushProperty("IdLogTarea", request?.IdLogTarea))
                    {
                        Log.Information("Peticion recibida de Mantiz. IdSolicitud: {IdSolicitud}, IdServicio: {IdServicio}, Version: {Version}",
                            request?.Request?.IdSolicitud, request?.Request?.IdServicio);

                        // Iniciar una tarea en segundo plano para ejecutar el servicio.
                        var task = Task.Run(() => RunService(messageId, requestXml, request!), stoppingToken);

                        // Agregar la tarea a la lista de las tareas en ejecucion.
                        ServiceTasks.Add(task);
                    }
                }
                catch (MessageQueueException e)
                {
                    Log.Error(e, "Error al conectarse con la cola");
                    Thread.Sleep(5000);
                }
                catch (Exception e)
                {
                    Log.Error(e, "Error al ejecutar el servicio");
                }
            }
        }

        private void RunService(string messageId, string requestXml, MantizRoot request)
        {
            try
            {
                using IServiceScope scope = _serviceScopeFactory.CreateScope();

                if (Services!.TryGetValue(request.Request!.IdServicio!, out Type? serviceType))
                {
                    var service = scope.ServiceProvider.GetRequiredService(serviceType);

                    var mantizResponse = serviceType.GetMethod("Run", new Type[] { typeof(string), typeof(MantizRoot) })?.Invoke(service, new object[] { requestXml, request });

                    var mantizResponseType = mantizResponse!.GetType();
                    var mantizResponseXml = Serializer.SerializeToString(mantizResponse, mantizResponseType);
                    Log.Information("Mantiz Response: {MantizResponseXml}", mantizResponseXml);

                    var mq = new MantizMQ(isResponse: true);

                    mq.Write(mantizResponseXml, messageId);
                }
                else
                {
                    Log.Error("IdServicio no registrado: {IdServicio}", request.Request.IdServicio);
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Error al ejecutar el servicio");
            }
        }

        private void RegisterServices()
        {
            Log.Information("Registrando Servicios");

            Services = new Dictionary<string, Type>();

            var Types = typeof(Service<,,,>).Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOfRawGeneric(typeof(Service<,,,>)))
                .ToList();

            foreach (var t in Types)
            {
                try
                {
                    var mantizServiceId = (string)t.GetField("ServiceId")!.GetValue(null)!;

                    Services.Add(mantizServiceId, t);
                }
                catch (Exception e)
                {
                    Log.Error(e, "Error al registrar servicio con la clase {ServiceClass}", t.Name);
                }
            }

            Log.Information("Registrado {CantidadServicios} Servicios", Services.Count);
        }

    }
}