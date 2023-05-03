using Microsoft.VisualStudio.TestTools.UnitTesting;
using MZ_WorkerService.Models.Api;
using MZ_WorkerService.Models.Api.ConsultaMontoSolicitado;
using MZ_WorkerService.Models.Api.ConsultaOfertaMaxima;
using MZ_WorkerService.Models.Mantiz;
using MZ_WorkerService.Models.Mantiz.ConsultaBasesInternas;
using MZ_WorkerService.Models.Mantiz.ConsultaModeva;
using MZ_WorkerService.Models.Mantiz.ConsultaMontoSolicitado;
using MZ_WorkerService.Models.Mantiz.ConsultaOfertaMaxima;
using System.Collections.Generic;

namespace WorkerService.Tests.UnitTests
{
    [TestClass]
    public class TestModels
    {

        [TestMethod]
        public void TestApiCsltaOfertaMaxmResponse()
        {
            //Preparación

            var cstOftMxResponse = new ApiConsultaOfertaMaximaResponse();

            cstOftMxResponse.consultaOfertaMaxima = new List<ApiResponseConsultaMax>();

            cstOftMxResponse.consultaOfertaMaxima.Add(new ApiResponseConsultaMax
            {
                cuota = 60,
                montoMaximo = 300,
                plazo = 12,
                tasa = 12
            });

            //Ejecución

            var obj = cstOftMxResponse;

            //Verificación

            Assert.IsNotNull(obj);
        }


        [TestMethod]
        public void TestApiResponse()
        {
            //Preparación
            var apiResps = new ApiResponse()
            {
                Codigo = "0102",
                Descripcion = "Success Response API"
            };
 
            //Verificación

            Assert.IsNotNull(apiResps); 
        }


        [TestMethod]
        public void TestRoot()
        {
            //Preparación
            Root rt = new Root();

            var cstOftMxResponse = new ApiConsultaOfertaMaximaResponse();

            cstOftMxResponse.consultaOfertaMaxima = new List<ApiResponseConsultaMax>();

            cstOftMxResponse.consultaOfertaMaxima.Add(new ApiResponseConsultaMax
            {
                cuota = 60,
                montoMaximo = 300,
                plazo = 12,
                tasa = 12
            });

            //Ejecución
            rt.ResponseAPI = cstOftMxResponse;

            //Verificación

            Assert.IsNotNull(rt.ResponseAPI);
        }

        [TestMethod]
        public void TestConstMontoSoltdResponse()
        {
            //Preparación

            var cstMntSoltdResponse = new ConsultaMontoSolicitadoResponse();

            cstMntSoltdResponse.Ofertas = new List<OfertaSolicitada>();

            cstMntSoltdResponse.Ofertas.Add(new OfertaSolicitada{
                cuotaMes = "112.00",
                cuotaSeguroDesempleo = "100.00",
                cuotaKI = "01010",
                cuotaSeguroDeuda = "50.00",
                montoSolicitado = "2000.00",
                plazo = "24",
                tasaEfectiva = "01",
                tasaNominal = "02"
            });

            //Ejecución

            var obj = cstMntSoltdResponse;

            //Verificación

            Assert.IsNotNull(obj);
        }


        [TestMethod]
        public void TestConstOftMxResponse()
        {
            //Preparación
            var cstOftMxRspns = new ConsultaOfertMaxResponse();

            cstOftMxRspns.Ofertas = new List<OfertListModel>();

            cstOftMxRspns.Ofertas.Add(new OfertListModel
            {
                cuota = "60.99",
                montoMaximo = "100.00",
                plazo = "12",
                tasa = "10.05"
            });

            //Ejecución

            var obj = cstOftMxRspns;

            //Verificación

            Assert.IsNotNull(obj);
        }

        [TestMethod]
        public void TestConstMntSltRqst()
        {
            //Preparación
            var cnstMntSltdRequet = new ConsultaMontoSolicitadoRequest()
            {
                clienteG = "G1",
                edadCliente = "26",
                EdadmasPlazoPol = "",
                esAprobados = "true",
                fuenteIngreso = "asalariado",
                IdServicio = "",
                IdSolicitud = "",
                ingresosCliente = "",
                montoMaximo = "",
                montoSolicitado = "",
                plazoMaximo = "",
                plazoSolicitado = "",
                porcSeguroCesantia = "",
                porcSeguroDeuda = "",
                producto = "",
                relacionCuota = "",
                tasaExcepcion = "",
                tasaPivote = "",
                //Usuario = "",
                Version = ""

            };

            //Ejecución
            var obj = cnstMntSltdRequet;


            //Verificación

            Assert.IsNotNull(obj);

        }

        [TestMethod]
        public void TestCnstOftMxRqst()
        {
            //Preparación
            var cstOftMxRequest = new ConsultaOfertMaxRequest()
            {
                porcSeguroCesantia = "01",
                Version = "2",
                //Usuario = "",
                tasaPivote = "",
                clienteG = "",
                edadCliente = "",
                EdadmasPlazoPol = "",
                esAprobados = "true",
                fuenteIngreso = "asalariado",
                IdServicio = "",
                IdSolicitud = "",
                ingresosCliente = "",
                montoMaximo = "",
                plazoMaximo = "",
                plazoSolicitado = "",
                porcSeguroDeuda = "",
                producto = "",
                relacionCuota = "",
                tasaExcepcion = "",
            };

            //Ejecución

            var obj = cstOftMxRequest;

            //Verificación

            Assert.IsNotNull(obj);
        }


        [TestMethod]
        public void TestApiConstMntSltResponse()
        {
            //Preparación
            var cnsMntSltResponse = new ApiConsultaMontoSolicitadoResponse();

            cnsMntSltResponse.consultaMontoSolicitado = new List<ApiResponseMontoSolicitado>();

            cnsMntSltResponse.consultaMontoSolicitado.Add(new ApiResponseMontoSolicitado
            {

                cuotaKI = 12,
                cuotaMes = 23,
                cuotaSeguroDesempleo = 500,
                cuotaSeguroDeuda = 12,
                montoSolicitado = 500,
                plazo = 12,
                tasaEfectiva = 3,
                tasanominal = 1

            });

            //Ejecución

            var obj = cnsMntSltResponse;

            //Verificación

            Assert.IsNotNull(obj);
        }

        [TestMethod]
        public void TestMantizRoot()
        {
            //Preparación

            var mtzResonse = new MantizResponse()
            {
                CodigoRespuesta = "500",
                MensajeRespuesta = "Success"
            };

            var mtzRequest = new MantizRequest()
            {
                IdServicio = "",
                IdSolicitud = "",
                Version = ""
            };

            var obj = new MantizRoot()
            {
                IdLogTarea = 0,
                Response = mtzResonse,
                Request = mtzRequest
            };

            //Verificación
            Assert.IsNotNull(obj);

        }

        [TestMethod]
        public void TestConsultaBasesInternas()
        {
            //Preparación

            var obj = new ConsultaBasesInternas()
            {
                IdLogTarea = 0,
                Response = null,
                Request = null,
            };

            //Verificación
            Assert.IsNotNull(obj);

        }



        [TestMethod]
        public void TestConsultaModevaG()
        {
            //Preparación

            var obj = new ConsultaModevaG()
            {
                IdLogTarea = 0,
                Response = null,
                Request = null,
            };

            //Verificación
            Assert.IsNotNull(obj);

        }


        [TestMethod]
        public void TestConsultaMontoSolicitadoModel()
        {
            //Preparación

            var obj = new ConsultaMontoSolicitadoModel()
            {
                IdLogTarea = 0,
                Response = null,
                Request = null,
            };

            //Verificación
            Assert.IsNotNull(obj);

        }


        [TestMethod]
        public void TestConsultaOfertaMaximaModel()
        {
            //Preparación

            var obj = new ConsultaOfertaMaximaModel()
            {
                IdLogTarea = 0,
                Response = null,
                Request = null,
            };

            //Verificación
            Assert.IsNotNull(obj);

        }

    }
}
