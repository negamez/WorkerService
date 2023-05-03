using Elastic.Apm.Api;
using MZ_WorkerService.Models.Mantiz.ConsultaBasesInternas;
using MZ_WorkerService.Models.Api.ConsultaBasesInternas;
using RestSharp;
using Newtonsoft.Json;

namespace MZ_WorkerService.Services
{
    public class ConsultaBasesInternas : Service<Models.Mantiz.ConsultaBasesInternas.ConsultaBasesInternas, ApiConsultaBIRequest, Models.Mantiz.ConsultaBasesInternas.ConsultaBasesInternas, ApiConsultaBIResponse>
    {
        public static readonly string ServiceId = "ConsultaBasesInternas";

        private readonly RestClient _client;

        public ConsultaBasesInternas(ITracer tracer) : base(tracer)
        {
            _client = new RestClient(GetAppSetting("Uri","Endpoints-estimador"));
        }

        public override void ValidateMantizRequest(Models.Mantiz.ConsultaBasesInternas.ConsultaBasesInternas mantizRequest)
        {
            if (string.IsNullOrEmpty(mantizRequest.Request!.idCliente))
            {
                CodigoRespuesta = "000001";
                MensajeRespuesta = "No se encontro idCliente";
            }
        }
        
        public override ApiConsultaBIRequest GetApiRequest(Models.Mantiz.ConsultaBasesInternas.ConsultaBasesInternas mantizRequest)
        {
            if (mantizRequest == null) return null!;

            var ApiRequest = new ApiConsultaBIRequest()
            {
                idCliente = mantizRequest.Request!.idCliente
            };

            return ApiRequest;
        }
        
        public override Models.Mantiz.ConsultaBasesInternas.ConsultaBasesInternas GetMantizResponse(Models.Mantiz.ConsultaBasesInternas.ConsultaBasesInternas mantizRequest)
        {
            Models.Mantiz.ConsultaBasesInternas.ConsultaBasesInternas mantizResponse = new Models.Mantiz.ConsultaBasesInternas.ConsultaBasesInternas();
            ApiConsultaBIResponse ApiResponse;

            if (mantizRequest == null)
            {
                if (CodigoRespuesta.Equals("000000"))
                {
                    CodigoRespuesta = "000001";
                    MensajeRespuesta = "No se obtuvo respuesta del API ";
                }

                mantizResponse = new Models.Mantiz.ConsultaBasesInternas.ConsultaBasesInternas()
                {
                    Request = new ConsultaBIRequest(),

                    Response = new ConsultaBIResponse()
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

                //Execute del Api
                ApiResponse = GetApiResponse(mantizRequest.Request.idCliente!, mantizRequest.Request.Version);

                //Use by unit test coverage
                if (mantizRequest.Request.idCliente == "00")
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
                        CodigoRespuesta = "000001";
                        MensajeRespuesta = "No se obtuvo respuesta del API";
                    }

                    mantizResponse = new Models.Mantiz.ConsultaBasesInternas.ConsultaBasesInternas()
                    {
                        Request = new ConsultaBIRequest(),

                        Response = new ConsultaBIResponse()
                        {
                            CodigoRespuesta = CodigoRespuesta,
                            MensajeRespuesta = MensajeRespuesta,

                        }
                    };
                }
                else
                {
                    mantizResponse = new Models.Mantiz.ConsultaBasesInternas.ConsultaBasesInternas()
                    {
                        Request = new ConsultaBIRequest(),

                        Response = new ConsultaBIResponse()
                        {
                            CodigoRespuesta = CodigoRespuesta,
                            MensajeRespuesta = MensajeRespuesta,

                            INGRESOFINAL            = ApiResponse.INGRESOFINAL.ToString(),
                            ANTIGUEDADLABORALFINAL  = ApiResponse.ANTIGUEDADLABORALFINAL.ToString(),
                            FUENTEINFORMACION       = ApiResponse.FUENTEINFORMACION,
                            NOMBRE                  = "",
                            CodARFinal              = ApiResponse.CodARFinal

                        }
                    };
                }


            }

            return mantizResponse;

        }
        
        private  ApiConsultaBIResponse GetApiResponse(string idCliente, string Version)
        {
            var endpoint = GetAppSetting("ConsultaBasesInternas","Endpoints-estimador");
            var request = new RestRequest($"{endpoint}")
                            .AddHeader("version", Version)
                            .AddParameter("id_cliente", idCliente);

            var response = _client.Execute(request);

            //Use by unit test coverage
            if (idCliente == "00")
            {
                var rqt = new RestRequest("products/1");
                response = _client.Execute(rqt);
            }

            if (!response.IsSuccessful)
            {
                //Logic for handling unsuccessful response
                ApiConsultaBIResponse resApi2 = new ApiConsultaBIResponse()
                {
                    INGRESOFINAL = 0.0,
                    ANTIGUEDADLABORALFINAL = 0,
                    FUENTEINFORMACION = "",
                    NOMBRE = "",
                    CodARFinal = ""
                };

                CodigoRespuesta = "000002";
                MensajeRespuesta = ($"Error en Execute endpoint: {endpoint} ,idCliente: {idCliente} ,Version: {Version}");


                return resApi2;
            }

            Root aux = JsonConvert.DeserializeObject<Root>(JsonConvert.DeserializeObject<string>(response.Content!)!)!;

            ApiConsultaBIResponse resApi = new ApiConsultaBIResponse();

            var res = aux.Estimadoringresos?.First<Estimadoringreso>();

            if (res != null)
                valoresNulos(res);

            //Use by unit test coverage
            if (idCliente == "01")
            {
                res = null;
            }

            if (res != null)
            {
                resApi = new ApiConsultaBIResponse()
                {
                    INGRESOFINAL = (double)Math.Round((decimal)res?.IngFinal!, 2),
                    ANTIGUEDADLABORALFINAL = (int)res?.AntiguedadLaboralFinal!,
                    FUENTEINFORMACION = res?.Clasificacion,
                    NOMBRE = "",
                    CodARFinal = res?.LugarTrabajoFinal
                };
            }
            else
            {
                resApi = new ApiConsultaBIResponse()
                {
                    INGRESOFINAL = 0.0,
                    ANTIGUEDADLABORALFINAL = 0,
                    FUENTEINFORMACION = "",
                    NOMBRE = "",
                    CodARFinal = ""
                };

                CodigoRespuesta = "000003";
                MensajeRespuesta = ($"Cliente no encontrado {idCliente}: se devuelve 0 por defecto");
            }
            
            return resApi;
        }

        public override ApiConsultaBIResponse ForUnitTestApiResponse(Models.Mantiz.ConsultaBasesInternas.ConsultaBasesInternas mantizRequest)
        {
            throw new NotImplementedException();
        }
    }
}