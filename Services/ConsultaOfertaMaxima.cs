
using Elastic.Apm.Api;
using MZ_WorkerService.Models.Api.ConsultaOfertaMaxima;
using MZ_WorkerService.Models.Mantiz.ConsultaOfertaMaxima;
using Newtonsoft.Json;
using RestSharp;
using Serilog;

namespace MZ_WorkerService.Services
{
    public class ConsultaOfertaMaxima : Service<ConsultaOfertaMaximaModel, ApiConsultaOfertaMaximaRequest, ConsultaOfertaMaximaModel, ApiConsultaOfertaMaximaResponse>
    {   
        public static readonly string ServiceId = "ConsultaOfertaMaxima";

        private readonly RestClient _client;

        public ConsultaOfertaMaxima(ITracer tracer) : base(tracer)
        {
            _client = new RestClient(GetAppSetting("Uri", "Endpoints-CalculoOferta"));
        }

        public override ApiConsultaOfertaMaximaResponse ForUnitTestApiResponse(ConsultaOfertaMaximaModel mantizRequest)
        {
            throw new NotImplementedException();
        }

        public override ApiConsultaOfertaMaximaRequest GetApiRequest(ConsultaOfertaMaximaModel mantizRequest)
        {
            if (mantizRequest == null) return null!;

            var ApiRequest = new ApiConsultaOfertaMaximaRequest()
            {
                idCliente = mantizRequest.Request!.clienteG
            };

            return ApiRequest;
        }

        public override ConsultaOfertaMaximaModel GetMantizResponse(ConsultaOfertaMaximaModel mantizRequest)
        {
            ConsultaOfertaMaximaModel mantizResponse = new ConsultaOfertaMaximaModel();
            ApiConsultaOfertaMaximaResponse ApiResponse;

            if (mantizRequest == null)
            {
                if (CodigoRespuesta.Equals("000000"))
                {
                    CodigoRespuesta = "000001";
                    MensajeRespuesta = "No se obtuvo respuesta del API ";
                }

                mantizResponse = new ConsultaOfertaMaximaModel()
                {
                    Request = new ConsultaOfertMaxRequest(),

                    Response = new ConsultaOfertMaxResponse()
                    {
                        CodigoRespuesta = CodigoRespuesta,
                        MensajeRespuesta = MensajeRespuesta,

                    }
                };
            }
            else
            {
                //Por defecto se manda V1 si no viene el nodo
                if (string.IsNullOrEmpty(mantizRequest.Request!.Version))
                {
                    mantizRequest.Request.Version = "1";
                }
                ValidateMantizRequest(mantizRequest);
                Log.Information("llamando la API CONSULTA OFERTA MAXIMA*********************");
                //Execute del Api
                ApiResponse = GetApiResponse(mantizRequest.Request);

                if (ApiResponse == null)
                {
                    
                        CodigoRespuesta = "000001";
                        MensajeRespuesta = "No se obtuvo respuesta del API";


                    mantizResponse = new ConsultaOfertaMaximaModel()
                    {
                        Request = new ConsultaOfertMaxRequest(),

                        Response = new ConsultaOfertMaxResponse()
                        {
                            CodigoRespuesta = CodigoRespuesta,
                            MensajeRespuesta = MensajeRespuesta,
                            Ofertas = new List<OfertListModel>()
                        }
                    };
                }
                else
                {
                    List<OfertListModel> ofertasAux = new List<OfertListModel>();
                    //se hace esta cosa porque en el server da error usar linq
                    foreach (var item in ApiResponse.consultaOfertaMaxima!)
                    {
                        ofertasAux.Add(new OfertListModel()
                        {
                            cuota = item.cuota.ToString(),
                            plazo = item.plazo.ToString(),
                            montoMaximo = item.montoMaximo.ToString(),
                            tasa = item.tasa.ToString()

                        });
                    }
                    mantizResponse = new ConsultaOfertaMaximaModel()
                    {
                        Request = new ConsultaOfertMaxRequest(),

                        Response = new ConsultaOfertMaxResponse()
                        {
                            CodigoRespuesta = CodigoRespuesta,
                            MensajeRespuesta = MensajeRespuesta,
                            Ofertas = ofertasAux
                        }
                    };
                }
            }
            return mantizResponse;
        }

        public override void ValidateMantizRequest(Models.Mantiz.ConsultaOfertaMaxima.ConsultaOfertaMaximaModel mantizRequest)
        {
            if (!ValidateObject(mantizRequest))
            {
                CodigoRespuesta = "000001";
                MensajeRespuesta = "the Request is not valid some field are empty ";
            }
        }

        private ApiConsultaOfertaMaximaResponse GetApiResponse(ConsultaOfertMaxRequest requestMZ)
        {
            if (!ValidateObject(requestMZ))
            {
                return null!;
            }

            var endpoint = GetAppSetting("ConsultaOfertaMaxima", "Endpoints-CalculoOferta");
            var request = new RestRequest($"{endpoint}")
                            .AddHeader("x-version", requestMZ.Version ??"1")
                            .AddParameter("producto", requestMZ.producto ??"0")
                            .AddParameter("clienteG", requestMZ.clienteG)
                            .AddParameter("ingresosCliente", requestMZ.ingresosCliente)
                            .AddParameter("plazoMaximo", requestMZ.plazoMaximo)
                            .AddParameter("relacionCuota", requestMZ.relacionCuota)
                            .AddParameter("edadCliente", requestMZ.edadCliente)
                            .AddParameter("fuenteIngreso", requestMZ.fuenteIngreso)
                            .AddParameter("montoMaximo", requestMZ.montoMaximo)
                            .AddParameter("tasaExcepcion", requestMZ.tasaExcepcion ?? "0")
                            .AddParameter("porcSeguroDeuda", requestMZ.porcSeguroDeuda)
                            .AddParameter("porcSeguroCesantia", requestMZ.porcSeguroCesantia)
                            .AddParameter("esAprobados", requestMZ.esAprobados)
                            .AddParameter("EdadmasPlazoPol", requestMZ.EdadmasPlazoPol)
                            .AddParameter("tasaPivote", requestMZ.tasaPivote)
                            .AddParameter("plazoSolicitado", requestMZ.plazoSolicitado);

            var response = _client.Execute(request);
            Log.Information( "URL: "+response.ResponseUri);
            if (!response.IsSuccessful)
            {
                Log.Information("no se tiene respuesta de API");
                //Logic for handling unsuccessful response

                CodigoRespuesta = "000002";
                MensajeRespuesta = ($"Error en Execute endpoint: {endpoint} Version: {requestMZ.Version}");


                return null!;
            }

            var aux = JsonConvert.DeserializeObject<List<ApiResponseConsultaMax>>(response.Content!);

            ApiConsultaOfertaMaximaResponse resApi = new ApiConsultaOfertaMaximaResponse();

            if (aux!.Count != 0)
            {
                resApi = new ApiConsultaOfertaMaximaResponse()
                {
                   consultaOfertaMaxima = aux
                };
            }
            else
            {
                resApi = new ApiConsultaOfertaMaximaResponse()
                {
                    Codigo = "000003",
                    Descripcion = "Not Found"
                };

                CodigoRespuesta = "000003";
                MensajeRespuesta = ($"oferta no encontrada se devuelve vacia por defecto");
            }

            return resApi;

        }
    }
}
