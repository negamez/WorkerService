using Microsoft.VisualStudio.TestTools.UnitTesting;
using MZ_WorkerService.Models.Mantiz.ConsultaOfertaMaxima;
using MZ_WorkerService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.Tests.UnitTests
{
    [TestClass]
    public class TestConsultaOfertaMaxima
    {

        [TestMethod]
        public void TestGetApiRequest()
        {
            //Preparación

            var cstOftMaxRequest = new ConsultaOfertMaxRequest()
            {
                clienteG = "0202"
            };

            var cstOftMxmModel = new ConsultaOfertaMaximaModel()
            {
                Request = cstOftMaxRequest
            };

             ConsultaOfertaMaxima cstOftMaxima = new ConsultaOfertaMaxima(null!);

            //Ejecución

            cstOftMaxima.GetApiRequest(cstOftMxmModel);


            //Verificación

            Assert.IsNotNull(cstOftMaxima);
        }

        [TestMethod]
        public void TesGetMantizResponse()
        {
            //Declaración

            var cstOftMaxReqst = new ConsultaOfertMaxRequest()
            {
                Version = "2",
                clienteG = "0202",
                fuenteIngreso = "Asalariado",
                ingresosCliente = "1000.00",
                producto = "CCASH",
                IdServicio = "ConsultaOfertaMaxima"
            };


            var cstOftMxmModel = new ConsultaOfertaMaximaModel()
            {
                Request = cstOftMaxReqst
            };

            ConsultaOfertaMaxima cstOftMaxima = new ConsultaOfertaMaxima(null!);

            //Ejecución

            cstOftMaxima.GetMantizResponse(cstOftMxmModel);


            //Verificación

            Assert.IsNotNull(cstOftMaxima);

        }


        [TestMethod]
        public void TestValidateMantizRequest()
        {
            //Declaración

            var cstOftMaxReqt = new ConsultaOfertMaxRequest()
            {
                Version = "",
                clienteG = "",
                fuenteIngreso = "",
                ingresosCliente = "",
                producto = "",
                IdServicio = ""
            };


            var cstOftMxModl = new ConsultaOfertaMaximaModel()
            {
                Request = cstOftMaxReqt
            };

            ConsultaOfertaMaxima cstOftMaxim = new ConsultaOfertaMaxima(null!);

            //Ejecución

            cstOftMaxim.ValidateMantizRequest(cstOftMxModl);

            //Verificación

            Assert.IsNotNull(cstOftMaxim);

        }

        [TestMethod]
        public void TestSetNullVersion()
        {
            //Preparación
            var cstOftMaxReqst = new ConsultaOfertMaxRequest()
            {
                Version = "",
            };


            var cstOftMxmModel = new ConsultaOfertaMaximaModel()
            {
                Request = cstOftMaxReqst
            };

            ConsultaOfertaMaxima cstOftMaxima = new ConsultaOfertaMaxima(null!);


            //Ejecución

            cstOftMaxima.GetMantizResponse(cstOftMxmModel);

            //Verificación

            Assert.IsNotNull(cstOftMaxima);
        }

        [TestMethod]
        public void TestMethodGetMZResponseSetNull()
        {
            //Preparación

            ConsultaOfertaMaxima cstOftMaxima = new ConsultaOfertaMaxima(null!);

            //Ejecición

            cstOftMaxima.GetMantizResponse(null!);

            //Verificación

            Assert.IsNotNull(cstOftMaxima);
        }

        [TestMethod]
        public void TestMethodGetApiResponse()
        {
            //Preparación

            var cstOftMaxReqt = new ConsultaOfertMaxRequest()
            {
                Version = "2",
                clienteG = "2",
                producto = "TDC",
                ingresosCliente = "1000.00",
                plazoSolicitado = "12",
                plazoMaximo = "12",
                relacionCuota = "12-12",
                edadCliente = "25",
                fuenteIngreso = "asalariado",
                montoMaximo = "1000.00",
                porcSeguroCesantia = "null",
                porcSeguroDeuda = "12",
                tasaExcepcion = "12",
                EdadmasPlazoPol = "12",
                tasaPivote = "12"
            };


            var cstOftMxModl = new ConsultaOfertaMaximaModel()
            {
                Request = cstOftMaxReqt
            };

            ConsultaOfertaMaxima cstOftMaxim = new ConsultaOfertaMaxima(null!);

            //Ejecución
            cstOftMaxim.GetMantizResponse(cstOftMxModl);

            //Verificación
            Assert.IsNotNull(cstOftMaxim);
        }

    }
}
