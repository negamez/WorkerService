using Elastic.Apm.SerilogEnricher;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace MZ_WorkerService.Logging
{
    public class Logger
    {
        private LogEventLevel LoggerMinimumLevel { get; set; }
        private bool LogToEventLog { get; set; }
        private bool LogToFile { get; set; }
        private bool LogToConsole { get; set; }
        private bool LogToElasticSearch { get; set; }

        public Logger()
        {
            LoggerMinimumLevel = (LogEventLevel)Worker.Configuration!.GetValue<int>("Logging:MinimumLevel");
            LogToEventLog = Worker.Configuration!.GetValue<bool>("Logging:LogToEventLog");
            LogToFile = Worker.Configuration!.GetValue<bool>("Logging:LogToFile");
            LogToConsole = Worker.Configuration!.GetValue<bool>("Logging:LogToConsole");
            LogToElasticSearch = Worker.Configuration!.GetValue<bool>("Logging:LogToElasticSearch");
        }

        public Serilog.ILogger ConfigureLogger()
        {
            var configuration = new LoggerConfiguration();

            // Nivel mínimo que se escribe a todos los destinos.
            configuration = configuration.MinimumLevel.Is(LoggerMinimumLevel);

            if (LogToConsole)
            {
                configuration = configuration.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} {ThreadId}] {Message:lj}{NewLine}{Exception}");
            }

            if (LogToFile)
            {
                var logPath = Path.Combine(Worker.Configuration!.GetValue<string>("Logging:FilePath")!, "MZ_WorkerService-.txt");
                configuration = configuration.WriteTo.File(logPath,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] [{ThreadId}] {Message:lj}{NewLine}{Exception}",
                    fileSizeLimitBytes: 33554432, // Límite de tamaño. 33554432 Bytes = 32MB
                    rollingInterval: RollingInterval.Day, // Un archivo por día.
                    rollOnFileSizeLimit: true, // Crear un nuevo archivo al llegar al límite de tamaño.
                    retainedFileCountLimit: null); // Retener todos los archivos indefinidamente.
            }

            if (LogToEventLog)
            {
                var eventSource = Worker.Configuration!.GetValue<string>("Logging:EventLogName");
                configuration = configuration.WriteTo.EventLog(eventSource,
                    logName: eventSource,
                    outputTemplate: "[{ThreadId}] {Message}{NewLine}{Exception}");
            }

            if (LogToElasticSearch)
            {
                configuration = configuration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(Worker.Configuration!.GetValue<string>("Logging:ElasticSearchUrl")!))
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = (Worker.Configuration!.GetValue<string>("Logging:IndexName")?.ToLower() ?? "logstash") + "-{0:yyyy.MM.dd}"
                });
            }

            var logger = configuration
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId()
                .Enrich.WithExceptionDetails()
                .Enrich.WithElasticApmCorrelationInfo()
                .CreateLogger();

            return logger;
        }
    }
}
