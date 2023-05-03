using Microsoft.VisualStudio.TestTools.UnitTesting;
using MZ_WorkerService.Models.Mantiz.ConsultaBasesInternas;

namespace WorkerService.Tests.UnitTests
{
    [TestClass]
    public class TestConsultaBasesInternas
    {

        [TestMethod]
        public void TestMethodGetApiRequest()
        {
            //Preparación

            var objMz = new ConsultaBIRequest()
            {
                idCliente = "1097236",
                Version = "1",
                IdServicio = "ConsultaBasesInternas"
            };

            var obj = new MZ_WorkerService.Models.Mantiz.ConsultaBasesInternas.ConsultaBasesInternas()
            {
                Request = objMz,
            };

            MZ_WorkerService.Services.ConsultaBasesInternas cstBsInternas = new MZ_WorkerService.Services.ConsultaBasesInternas(null!);


            //Ejecución

            cstBsInternas.GetApiRequest(obj);


            //Verificación
            Assert.IsNotNull(cstBsInternas);
        }


        [TestMethod]
        public void TestMethodGetMantizResponse()
        {

            //Preparación

            var cstBIRequest = new ConsultaBIRequest()
            {
                idCliente = "1097236",
                Version = "1",
                IdServicio = "ConsultaBasesInternas"
            };


            var obj = new MZ_WorkerService.Models.Mantiz.ConsultaBasesInternas.ConsultaBasesInternas()
            {
                Request = cstBIRequest
            };


            MZ_WorkerService.Services.ConsultaBasesInternas cstBsInternas = new MZ_WorkerService.Services.ConsultaBasesInternas(null!);


            //Ejecución

            cstBsInternas.GetMantizResponse(obj);


            //Verificación
            Assert.IsNotNull(cstBsInternas);

        }


        [TestMethod]
        public void TestMethodSetNullMZResponse()
        {
            //Preparación

            MZ_WorkerService.Services.ConsultaBasesInternas cstBsInternas = new MZ_WorkerService.Services.ConsultaBasesInternas(null!);

            //Ejecución

            cstBsInternas.GetMantizResponse(null!);

            //Verificación

            Assert.IsNotNull(cstBsInternas);
        }


        [TestMethod]
        public void TestMethodSetNullVersion()
        {
            //Preparación

            var cstBIRequest = new ConsultaBIRequest()
            {
                Version = "",
            };

            var obj = new MZ_WorkerService.Models.Mantiz.ConsultaBasesInternas.ConsultaBasesInternas()
            {
                Request = cstBIRequest
            };

            MZ_WorkerService.Services.ConsultaBasesInternas cstBsInternas = new MZ_WorkerService.Services.ConsultaBasesInternas(null!);


            //Ejecución

            cstBsInternas.GetMantizResponse(obj);

            //Verificación

            Assert.IsNotNull(cstBsInternas);
        }

        [TestMethod]
        public void TestMethodValidateMZRqstSetNullIdClient()
        {
            //Preparación

            var cstBIRequest = new ConsultaBIRequest()
            {
                idCliente = ""
            };

            var obj = new MZ_WorkerService.Models.Mantiz.ConsultaBasesInternas.ConsultaBasesInternas()
            {
                Request = cstBIRequest
            };

            MZ_WorkerService.Services.ConsultaBasesInternas cstBsInternas = new MZ_WorkerService.Services.ConsultaBasesInternas(null!);


            //Ejecución

            cstBsInternas.ValidateMantizRequest(obj);

            //Verificación

            Assert.IsNotNull (cstBsInternas);

        }

        [TestMethod]
        public void TestMethodGetMZResponse()
        {
            //Preparación

            var cstBIRequest = new ConsultaBIRequest()
            {
                Version= "1.0.0",
                idCliente = "00",
                
            };

            var obj = new MZ_WorkerService.Models.Mantiz.ConsultaBasesInternas.ConsultaBasesInternas()
            {
                Request = cstBIRequest
            };

            MZ_WorkerService.Services.ConsultaBasesInternas cstBsInternas = new MZ_WorkerService.Services.ConsultaBasesInternas(null!);


            //Ejecución

            cstBsInternas.GetMantizResponse(obj);

            //Verificación

            Assert.IsNotNull(cstBsInternas);
        }




        [TestMethod]
        public void TestEstimadorIngresosCustomerNotFound()
        {

            //Preparación

            var cstBIRequest = new ConsultaBIRequest()
            {
                idCliente = "01",
                Version = "1",
                IdServicio = "ConsultaBasesInternas"
            };


            var obj = new MZ_WorkerService.Models.Mantiz.ConsultaBasesInternas.ConsultaBasesInternas()
            {
                Request = cstBIRequest
            };


            MZ_WorkerService.Services.ConsultaBasesInternas cstBsInternas = new MZ_WorkerService.Services.ConsultaBasesInternas(null!);


            //Ejecución

            cstBsInternas.GetMantizResponse(obj);


            //Verificación
            Assert.IsNotNull(cstBsInternas);

        }



    }
}
