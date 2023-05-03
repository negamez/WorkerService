using Elastic.Apm.Api;
using MZ_WorkerService.Models.Api.ConsultaMontoSolicitado;
using MZ_WorkerService.Models.Mantiz.ConsultaMontoSolicitado;
using Newtonsoft.Json;
using RestSharp;
using Serilog;

namespace MZ_WorkerService.Services
{
    public class ConsultaMontoSolicitado : Service<ConsultaMontoSolicitadoModel, ApiConsultaMontoSolicitadoRequest, ConsultaMontoSolicitadoModel, ApiConsultaMontoSolicitadoResponse>
    {
        public static readonly string ServiceId = "ConsultaMontoSolicitado";

        private readonly RestClient _client;

        public ConsultaMontoSolicitado(ITracer tracer) : base(tracer)
        {
            _client = new RestClient(GetAppSetting("Uri", "Endpoints-CalculoOferta"));
        }

        public override ApiConsultaMontoSolicitadoResponse ForUnitTestApiResponse(ConsultaMontoSolicitadoModel mantizRequest)
        {
            throw new NotImplementedException();
        }

        public override ApiConsultaMontoSolicitadoRequest GetApiRequest(ConsultaMontoSolicitadoModel mantizRequest)
        {
            if (mantizRequest == null) return null!;

            var ApiRequest = new ApiConsultaMontoSolicitadoRequest()
            {
                idCliente = mantizRequest.Request!.clienteG
            };

            return ApiRequest;
        }

        public override ConsultaMontoSolicitadoModel GetMantizResponse(ConsultaMontoSolicitadoModel mantizRequest)
        {
            ConsultaMontoSolicitadoModel mantizResponse = new ConsultaMontoSolicitadoModel();
            ApiConsultaMontoSolicitadoResponse ApiResponse;

            if (mantizRequest == null)
            {
                if (CodigoRespuesta.Equals("000000"))
                {
                    CodigoRespuesta = "000001";
                    MensajeRespuesta = "No se obtuvo respuesta del API ";
                }

                mantizResponse = new ConsultaMontoSolicitadoModel()
                {
                    Request = new ConsultaMontoSolicitadoRequest(),

                    Response = new ConsultaMontoSolicitadoResponse()
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
                //Execute del Api
                Log.Information("llamando la API CONSULTA MONTO SOLICITADO*********************");
                ApiResponse = GetApiResponse(mantizRequest.Request);
                Log.Information( "");
                if (ApiResponse == null)
                {
                    
                        CodigoRespuesta = "000001";
                        MensajeRespuesta = "No se obtuvo respuesta del API";


                    mantizResponse = new ConsultaMontoSolicitadoModel()
                    {
                        Request = new ConsultaMontoSolicitadoRequest(),

                        Response = new ConsultaMontoSolicitadoResponse()
                        {
                            CodigoRespuesta = CodigoRespuesta,
                            MensajeRespuesta = MensajeRespuesta,
                            Ofertas = new List<OfertaSolicitada>()
                        }
                    };
                }
                else
                {
                    List<OfertaSolicitada> ofertaSolicidataAux = new List<OfertaSolicitada>();
                    foreach (var item in ApiResponse.consultaMontoSolicitado!)
                    {
                        ofertaSolicidataAux.Add(new OfertaSolicitada
                        {
                            plazo = item.plazo.ToString(),
                            cuotaMes = item.cuotaMes.ToString(),
                            cuotaKI = item.cuotaKI.ToString(),
                            tasaNominal = item.tasanominal.ToString(),
                            tasaEfectiva = item.tasaEfectiva.ToString(),
                            cuotaSeguroDeuda = item.cuotaSeguroDeuda.ToString(),
                            cuotaSeguroDesempleo = item.cuotaSeguroDesempleo.ToString(),
                            montoSolicitado = item.montoSolicitado.ToString(),
                        });
                    }
                    mantizResponse = new ConsultaMontoSolicitadoModel()
                    {
                        Request = new ConsultaMontoSolicitadoRequest(),

                        Response = new ConsultaMontoSolicitadoResponse()
                        {
                            CodigoRespuesta = CodigoRespuesta,
                            MensajeRespuesta = MensajeRespuesta,
                            Ofertas = ofertaSolicidataAux

                        }
                    };
                }

            }

            return mantizResponse;
        }

        private ApiConsultaMontoSolicitadoResponse GetApiResponse(ConsultaMontoSolicitadoRequest requestMZ)
        {  
            if (!ValidateObject(requestMZ))
            {
                return null!;
            }

            var endpoint = GetAppSetting("ConsultaMontoSolicitado", "Endpoints-CalculoOferta");
            var request = new RestRequest($"{endpoint}")
                            .AddHeader("x-version", requestMZ.Version ?? "1")
                            .AddParameter("producto", requestMZ.producto! ?? "0")
                            .AddParameter("clienteG", requestMZ.clienteG)
                            .AddParameter("ingresosCliente", requestMZ.ingresosCliente)
                            .AddParameter("plazoSolicitado", requestMZ.plazoSolicitado)
                            .AddParameter("plazoMaximo", requestMZ.plazoMaximo)
                            .AddParameter("relacionCuota", requestMZ.relacionCuota)
                            .AddParameter("edadCliente", requestMZ.edadCliente)
                            .AddParameter("fuenteIngreso", requestMZ.fuenteIngreso)
                            .AddParameter("montoMaximo", requestMZ.montoMaximo)
                            .AddParameter("montoSolicitado",requestMZ.montoSolicitado)
                            .AddParameter("tasaExcepcion", requestMZ.tasaExcepcion ?? "0")
                            .AddParameter("porcSeguroDeuda", requestMZ.porcSeguroDeuda)
                            .AddParameter("porcSeguroCesantia", requestMZ.porcSeguroCesantia)
                            .AddParameter("esAprobados", requestMZ.esAprobados)
                            .AddParameter("EdadmasPlazoPol", requestMZ.EdadmasPlazoPol)
                            .AddParameter("tasaPivote", requestMZ.tasaPivote);

            var response = _client.Execute(request);
            Log.Information("URL: "+response.ResponseUri);
            if (!response.IsSuccessful)
            {
                //Logic for handling unsuccessful response

                CodigoRespuesta = "000002";
                MensajeRespuesta = ($"Error en Execute endpoint: {endpoint} Version: {requestMZ.Version}");


                return null!;
            }

            var aux = JsonConvert.DeserializeObject<List<ApiResponseMontoSolicitado>>(response.Content!);

            ApiConsultaMontoSolicitadoResponse resApi = new ApiConsultaMontoSolicitadoResponse();

            if (aux!.Count != 0)
            {
                resApi = new ApiConsultaMontoSolicitadoResponse()
                {
                    consultaMontoSolicitado = aux
                };
            }
            else
            {
                resApi = new ApiConsultaMontoSolicitadoResponse()
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
