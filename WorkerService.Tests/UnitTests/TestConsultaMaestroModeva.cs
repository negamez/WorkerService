using Elastic.Apm.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MZ_WorkerService.Models.Mantiz.ConsultaModeva;
using MZ_WorkerService.Services;

namespace WorkerService.Tests.UnitTests
{
    [TestClass]
    public class TestConsultaMaestroModeva
    {

        [TestMethod]
        public void TestMethodGetApiRequest()
        {

            //Preparación

            var objMz = new ConsultaModevaRequest()
            {
                idCliente = "1097236",
                idProducto = "PYME"
            };

            var obj = new ConsultaModevaG()
            {
                Request = objMz,
            };
            
            ConsultaMaestroModeva maestroModeva = new ConsultaMaestroModeva(null!);


           //Ejecución

            maestroModeva.GetApiRequest(obj);


            //Verificación
            Assert.IsNotNull(maestroModeva);

        }


        [TestMethod]
        public void TestMethodGetMantizResponse()
        {


            //Preparacion

            ConsultaMaestroModeva maestroMdv = new ConsultaMaestroModeva(null!);


            var request = new ConsultaModevaRequest() {

                idCliente = "892242",
                idProducto = "PYME",
                IdServicio = "ConsultaMaestroModeva",
                Version = "2"
            };


            var mdvG = new ConsultaModevaG()
            {
                Request = request,
            };



            //Ejecución

            maestroMdv.GetMantizResponse(mdvG);


            //Verificacion

            Assert.IsNotNull(maestroMdv);

        }


        [TestMethod]
        public void TestMethodGetMantizResponseSetNull()
        {
            //Preparación

            ConsultaMaestroModeva maestroMdv = new ConsultaMaestroModeva(null!);

            //Ejecución

            maestroMdv.GetMantizResponse(null!);

            //Verificación

            Assert.IsNotNull(maestroMdv);
        }

        [TestMethod]
        public void TestSetNullVersion()
        {
            //Preparación
            ConsultaMaestroModeva maestroMdv = new ConsultaMaestroModeva(null!);


            var request = new ConsultaModevaRequest()
            {

                idCliente = "892242",
                idProducto = "PYME",
                IdServicio = "ConsultaMaestroModeva",
                Version = ""
            };


            var mdvG = new ConsultaModevaG()
            {
                Request = request,
            };

            //Ejecución

            maestroMdv.GetMantizResponse(mdvG);

            //Verificación

            Assert.IsNotNull(maestroMdv);
        }

        [TestMethod]
        public void TestGetApiResponse()
        {
            //Preparación
            ConsultaMaestroModeva maestroMdv = new ConsultaMaestroModeva(null!);


            var request = new ConsultaModevaRequest()
            {
                idCliente = "00",
            };


            var mdvG = new ConsultaModevaG()
            {
                Request = request,
            };


            //Ejecución

            maestroMdv.GetMantizResponse(mdvG);

            //Verificación
            Assert.IsNotNull(maestroMdv);
        }
    }
}
