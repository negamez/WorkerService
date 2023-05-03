using Elastic.Apm.Api;
using Serilog;
using MZ_WorkerService.Models.Mantiz;
using MZ_WorkerService.Models.Api;
using MZ_WorkerService.Xml;
using System.Reflection;

namespace MZ_WorkerService.Services
{
    public abstract class Service<TMantizRequest, TApiRequest , TMantizResponse, TApiResponse>
        where TApiRequest : ApiRequest
        where TApiResponse : ApiResponse
    {
        protected ITracer _tracer;
        protected string CodigoRespuesta { get; set; }
        protected string MensajeRespuesta { get; set; }
        protected bool HasResponseList { get; set; }

        protected Service(ITracer tracer, bool hasResponseList = false)
        {
            _tracer = tracer;
            HasResponseList = hasResponseList;

            CodigoRespuesta = "000000";
            MensajeRespuesta = "Proceso Exitoso";
        }

        public virtual TMantizResponse Run(TMantizRequest mantizRequest)
        {
            TMantizResponse mantizResponse = default!;
            
            if (_tracer.CurrentTransaction is null)
            {
                _tracer.StartTransaction(string.Format("{0} ", "TMantizResponse Run"), ApiConstants.TypeRequest);
            }
            
            _tracer.CurrentTransaction!.CaptureSpan("GetMantizResponse", ApiConstants.TypeRequest, () =>
            {
                mantizResponse = GetMantizResponse(mantizRequest);
            });

            _tracer.CurrentTransaction.End();

            return mantizResponse;
        }

        public virtual TMantizResponse Run(string requestXml, MantizRoot request)
        {
            if (request == null) return default!;

            TMantizRequest mantizRequest = default!;

            TMantizResponse? mantizResponse;

            if (_tracer.CurrentTransaction is null)
            {
                _tracer.StartTransaction(string.Format("{0} ", "TMantizResponse Run"), ApiConstants.TypeRequest);
            }

            _tracer.CurrentTransaction!.CaptureSpan("GetMantizRequest", ApiConstants.TypeRequest, () =>
            {
                mantizRequest = GetMantizRequest(request!.Request!.IdServicio!, requestXml);
            });

            mantizResponse = Run(mantizRequest);

            return mantizResponse;
        }

        public virtual TMantizRequest GetMantizRequest(string idServicio, string mantizRequestXml)
        {
            Log.Information("Mantiz Request: {MantizRequestXml}", mantizRequestXml);
            var mantizRequest = Serializer<TMantizRequest>.DeserializeString(mantizRequestXml);

            if (mantizRequest == null)
            {
                CodigoRespuesta = "000001";
                MensajeRespuesta = "XML de request invalido";
            }

            return mantizRequest;
        }

        public virtual void ValidateMantizRequest(TMantizRequest mantizRequest)
        {
        }

        public abstract TApiRequest GetApiRequest(TMantizRequest mantizRequest);

        //Se creo para quitar code smell del tipo Major.
        public abstract TApiResponse ForUnitTestApiResponse(TMantizRequest mantizRequest);

        public abstract TMantizResponse GetMantizResponse(TMantizRequest mantizRequest);

        public string GetAppSetting(string attribute, string section)
        {
            var value = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build().GetSection(section)[attribute];
            return value!;
        }

        public static void valoresNulos<T>(T obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            Console.WriteLine($"Valores nulos en la clase {type.Name}:");
            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(obj)!;
                if (value == null)
                {
                    if (property.PropertyType == typeof(string))
                    {
                        property.SetValue(obj, "");
                    }
                    else if (property.PropertyType == typeof(double?))
                    {
                        property.SetValue(obj, 0.0);
                    }
                    else if (property.PropertyType == typeof(int))
                    {
                        property.SetValue(obj, 0);
                    }
                }
            }
        }

        public static bool ValidateObject<T>(T obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(obj)!;
                if (value == null)
                {
                    Console.WriteLine($"Valores nulos en la clase {type.Name}: {property.Name}");
                    return false;
                }
            }
            return true;
        }

    }
}
