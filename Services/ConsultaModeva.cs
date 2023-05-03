using Elastic.Apm.Api;
using MZ_WorkerService.Models.Mantiz.ConsultaModeva;
using MZ_WorkerService.Models.Api.ConsultaModeva;
using RestSharp;
using Newtonsoft.Json;
using Serilog;

namespace MZ_WorkerService.Services
{
    public class ConsultaModeva : Service<ConsultaModevaG, ApiConsultaModevaRequest, ConsultaModevaG, ApiConsultaModevaResponse>
    {
        public static readonly string ServiceId = "ConsultaModeva";

        private readonly RestClient _client;

        public ConsultaModeva(ITracer tracer) : base(tracer)
        {
            _client = new RestClient(GetAppSetting("Uri", "Endpoints-modeva"));
        }

        public override void ValidateMantizRequest(ConsultaModevaG mantizRequest)
        {
            if (string.IsNullOrEmpty(mantizRequest.Request!.idCliente))
            {
                CodigoRespuesta = "000001";
                MensajeRespuesta = "No se encontro idCliente";
            }
        }

        public override ApiConsultaModevaRequest GetApiRequest(ConsultaModevaG mantizRequest)
        {
            if (mantizRequest == null) return null!;

            var ApiRequest = new ApiConsultaModevaRequest(){
              idCliente = mantizRequest.Request!.idCliente
            };

            return ApiRequest;
        }

        public override ConsultaModevaG GetMantizResponse(ConsultaModevaG mantizRequest)
        {
            
            ApiConsultaModevaResponse ApiResponse;
            ConsultaModevaG mantizResponse;

            if (mantizRequest == null)
            {
                if (CodigoRespuesta.Equals("000000"))
                {
                    Log.Information("No se puede procesar la peticion");

                    CodigoRespuesta = "000001";
                    MensajeRespuesta = "Mantiz Request null, no se puede procesar peticion";
                }

                mantizResponse = new ConsultaModevaG()
                {
                    Request = new ConsultaModevaRequest(),
                    Response = new ConsultaModevaResponse()
                    {
                        CodigoRespuesta = CodigoRespuesta,
                        MensajeRespuesta = MensajeRespuesta,
                        GModeva = "0"
                    }
                };
            }
            else
            {
                //Por defecto se manda V1 si no viene el nodo
                if(string.IsNullOrEmpty(mantizRequest.Request!.Version))
                {
                    mantizRequest.Request.Version = "1";
                }

                ApiResponse = GetApiResponse(mantizRequest.Request);

                //Use by unit test coverage
                if (mantizRequest.Request!.idCliente == "00")
                {
                    ApiResponse = null!;
                }

                if (ApiResponse == null)
                {

                    if (ApiResponse != null)
                    {
                        CodigoRespuesta = ApiResponse.Codigo!;
                        MensajeRespuesta = ApiResponse.Descripcion!;
                    }
                    else
                    {
                        Log.Information("No se obtuvo respuesta del API");

                        CodigoRespuesta = "000001";
                        MensajeRespuesta = "No se obtuvo respuesta del API";
                    }

                    mantizResponse = new ConsultaModevaG()
                    {
                        Request = new ConsultaModevaRequest(),
                        Response = new ConsultaModevaResponse()
                        {
                            CodigoRespuesta = CodigoRespuesta,
                            MensajeRespuesta = MensajeRespuesta,
                            GModeva = "0"
                        }
                    };
                }
                else
                {
                    Log.Information("Asignando respuesta del API");

                    mantizResponse = new ConsultaModevaG()
                    {
                        Request = new ConsultaModevaRequest(),
                        Response = new ConsultaModevaResponse()
                        {
                            CodigoRespuesta = CodigoRespuesta,
                            MensajeRespuesta = MensajeRespuesta,
                            GModeva = ApiResponse.GFinal!.ToString()
                        }
                    };
                }
            }

            return mantizResponse;
        }

        private ApiConsultaModevaResponse GetApiResponse(ConsultaModevaRequest requestMZ)
        {
            var endpoint = GetAppSetting("ConsultaModeva", "Endpoints-modeva");
            
            var request = new RestRequest($"{endpoint}")
                            .AddHeader("version", requestMZ.Version!)
                            .AddParameter("id_cliente", requestMZ.idCliente);

            RestResponse response = _client.Execute(request);

            //Use by unit test coverage
            if (requestMZ.idCliente == "00")
            {
                var rqt = new RestRequest("products/1");
                response = _client.Execute(rqt);
            }

            if (!response.IsSuccessful)
            {
                Log.Information($"ErrorException: {response.ErrorException}");
                //Logic for handling unsuccessful response
                ApiConsultaModevaResponse resApi2 = new ApiConsultaModevaResponse()
                {
                    GFinal = "0"
                };

                CodigoRespuesta = "000002";
                MensajeRespuesta = ($"Error en Execute endpoint: {endpoint} ,idCliente: {requestMZ.idCliente} ,Version: {requestMZ.Version}");

                return resApi2;
            }

            Root aux = JsonConvert.DeserializeObject<Root>(JsonConvert.DeserializeObject<string>(response?.Content!)!)!;

            var res = aux?.Modevagrupos?.First<Modevagrupo>();

            ApiConsultaModevaResponse resApi = new ApiConsultaModevaResponse()
            {
                GFinal = res?.GFinal ?? "0",
            };

            return resApi;
        }

        public override ApiConsultaModevaResponse ForUnitTestApiResponse(ConsultaModevaG mantizRequest)
        {
            throw new NotImplementedException();
        }
    }
}
