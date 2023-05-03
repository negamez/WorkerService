using Elastic.Apm.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MZ_WorkerService.Models.Mantiz.ConsultaModeva;
using MZ_WorkerService.Services;

namespace WorkerService.Tests.UnitTests
{
    [TestClass]
    public class TestConsultaModeva
    {

        [TestMethod]
        public void TestValidateMantizRequest()
        {
            //Preparación

            var cstMdvRequest = new ConsultaModevaRequest()
            {

                idCliente = "3029843",
                IdServicio = "ConsultaModeva",
                Version = "1"

            };

            var cstModevaG = new ConsultaModevaG()
            {
                Request = cstMdvRequest
            };

            ConsultaModeva cstModeva = new ConsultaModeva(null!);

            //Ejecución

            cstModeva.ValidateMantizRequest(cstModevaG);

            //Verificación

            Assert.IsNotNull(cstModeva);
        }

        [TestMethod]
        public void TestMethodGetApiRequest()
        {
            //Preparación

            ConsultaModeva cstModeva = new ConsultaModeva(null!);


            var cstMdvRequest = new ConsultaModevaRequest(){ 

                idCliente = "3029843",
                IdServicio = "ConsultaModeva",
                Version = "1"

            };

            var obj = new ConsultaModevaG()
            {
                Request = cstMdvRequest
            };

            //Ejecución

            cstModeva.GetApiRequest(obj);


            //Verificación

            Assert.IsNotNull(cstModeva);
        }



        [TestMethod]
        public void TestMethodGetMantizResponse()
        {

            //Preparación

            ConsultaModeva cstModeva = new ConsultaModeva(null!);


            var cstMdvRequest = new ConsultaModevaRequest()
            {

                idCliente = "3029843",
                IdServicio = "ConsultaModeva",
                Version = "1"

            };

            var obj = new ConsultaModevaG()
            {
                Request = cstMdvRequest
            };

            //Ejecución

            cstModeva.GetMantizResponse(obj);

            //Verificación

            Assert.IsNotNull(cstModeva);
        }


        [TestMethod]
        public void TestMethodGetMZResponseSetNull() {

            //Preparación
            ConsultaModeva cstModeva = new ConsultaModeva(null!);

            //Ejecución
            cstModeva.GetMantizResponse(null!);

            //Verificación
            Assert.IsNotNull(cstModeva);

        }

        [TestMethod]
        public void TestSetNullVersion()
        {
            //Preparación


            ConsultaModeva cstModeva = new ConsultaModeva(null!);


            var cstMdvRequest = new ConsultaModevaRequest()
            {
                Version = ""
            };

            var obj = new ConsultaModevaG()
            {
                Request = cstMdvRequest
            };

            //Ejecución

            cstModeva.GetMantizResponse(obj);

            //Verificación

            Assert.IsNotNull (cstModeva);

        }


        [TestMethod]
        public void TestGetApiResponse()
        {
            //Preparación

            ConsultaModeva cstModeva = new ConsultaModeva(null!);

            var cstMdvRequest = new ConsultaModevaRequest()
            {
                idCliente = "00"
            };

            var obj = new ConsultaModevaG()
            {
                Request = cstMdvRequest
            };

            //Ejecución

            cstModeva.GetMantizResponse(obj);

            //Verificación
            Assert.IsNotNull(cstModeva);
        }

        [TestMethod]
        public void TestMethodVlTMZRQ()
        {
            //Preparación

            ConsultaModeva cstModeva = new ConsultaModeva(null!);

            var cstMdvRequest = new ConsultaModevaRequest()
            {
                idCliente = ""
            };

            var obj = new ConsultaModevaG()
            {
                Request = cstMdvRequest
            };

            //Ejecución

            cstModeva.ValidateMantizRequest(obj);

            //Verificación

            Assert.IsNotNull(cstModeva);
        }

    }

}
