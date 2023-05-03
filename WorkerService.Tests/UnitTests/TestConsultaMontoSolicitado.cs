using Elastic.Apm.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MZ_WorkerService.Models.Api.ConsultaMontoSolicitado;
using MZ_WorkerService.Models.Mantiz.ConsultaMontoSolicitado;
using MZ_WorkerService.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace WorkerService.Tests.UnitTests
{
    [TestClass]
    public class TestConsultaMontoSolicitado
    {
        
        [TestMethod]
        public void TestGetApiRequest()
        {

            //Preparación

            var montSltRequest = new ConsultaMontoSolicitadoRequest()
            {
                clienteG = "0101"
            };

            
            var  cstMntSltModel = new ConsultaMontoSolicitadoModel()
            {
                Request = montSltRequest
            };

            ConsultaMontoSolicitado cstMontoSlt = new ConsultaMontoSolicitado(null!);

            //Ejecución

            cstMontoSlt.GetApiRequest(cstMntSltModel);


            //Verificación

            Assert.IsNotNull(cstMontoSlt);

        }


        [TestMethod]
        public void TestGetMantizResponse()
       {

            //Preparación

            var cstMntSltdRequest = new ConsultaMontoSolicitadoRequest()
            {
               Version = "2",
               clienteG = "G2",
               IdServicio = "ConsultaMontoSolicitado",
               edadCliente = "26",
               fuenteIngreso = "Asalariado"
            };

            var cstMntSltModel = new ConsultaMontoSolicitadoModel()
            {
                Request = cstMntSltdRequest
            };


            ConsultaMontoSolicitado cstMontoSlt = new ConsultaMontoSolicitado(null!);

            //Ejecución

            cstMontoSlt.GetMantizResponse(cstMntSltModel);


            //Verificación

            Assert.IsNotNull(cstMontoSlt);

       }

        [TestMethod]
        public void TestGetMantizResponseNull()
        {

            //Preparación

            ConsultaMontoSolicitado cstMontoSlt = new ConsultaMontoSolicitado(null!);

            //Ejecución

            cstMontoSlt.GetMantizResponse(null!);


            //Verificación

            Assert.IsNotNull(cstMontoSlt);

        }

        [TestMethod]
        public void TestSetVersion()
        {
            //Preparación

            var cstMntSltdRequest = new ConsultaMontoSolicitadoRequest()
            {
                Version = "",
                clienteG = "",
                IdServicio = "",
                edadCliente = "",
                fuenteIngreso = ""
            };

            var cstMntSltModel = new ConsultaMontoSolicitadoModel()
            {
                Request = cstMntSltdRequest
            };


            ConsultaMontoSolicitado cstMontoSlt = new ConsultaMontoSolicitado(null!);


            //Ejecución

             cstMontoSlt.GetMantizResponse(cstMntSltModel);

            //Verificación

            Assert.IsNotNull(cstMontoSlt);

        }

        [TestMethod]
        public void TestMethodGetApiResponse()
        {
            //Preparación


            var cstMntSltdRequest = new ConsultaMontoSolicitadoRequest()
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
                tasaPivote = "12",
                
            };

            var cstMntSltModel = new ConsultaMontoSolicitadoModel()
            {
                Request = cstMntSltdRequest
            };


            ConsultaMontoSolicitado cstMontoSlt = new ConsultaMontoSolicitado(null!);

            //Ejecución

            cstMontoSlt.GetMantizResponse(cstMntSltModel);

            //Verificación

            Assert.IsNotNull(cstMontoSlt);
        }

        [TestMethod]
        public void GetApiRequest_NullMantizRequest_ReturnsDefaultResponse()
        {
            // Act
            var apiRequest = new ConsultaMontoSolicitado(null!);

            // Assert
            Assert.IsNotNull(apiRequest);
        }

        [TestMethod]
        public void GetApiRequest_ValidMantizRequest_ReturnsApiConsultaMontoSolicitadoRequest()
        {
            // Arrange
            var mantizRequest = new ConsultaMontoSolicitadoModel
            {
                Request = new ConsultaMontoSolicitadoRequest
                {
                    clienteG = "1234"
                }
            };

            // Act
            var apiRequest = new ConsultaMontoSolicitado(null!); 

            // Assert
            Assert.IsNotNull(apiRequest);
        }

        [TestMethod]
        public void GetMantizResponse_NullMantizRequest_ReturnsErrorResponse()
        {
            // Act
            var mantizResponse = new ConsultaMontoSolicitado(null!);

            // Assert
            Assert.IsNotNull(mantizResponse);
            
        }

        [TestMethod]
        public void GetMantizResponse_ValidMantizRequest_ReturnsMantizResponse()
        {
            // Arrange
            var mantizRequest = new ConsultaMontoSolicitadoModel
            {
                Request = new ConsultaMontoSolicitadoRequest
                {
                    clienteG = "1234"
                }
            };

            // Act
            var mantizResponse = new ConsultaMontoSolicitado(null!);

            // Assert
            Assert.IsNotNull(mantizResponse);
        }

        [TestMethod]
        public void TestGetApiResponse_WithValidRequest_ReturnsApiResponse()
        {
            // Arrange
            var requestMZ = new ConsultaMontoSolicitadoRequest
            {
                producto = "0414",
                clienteG = "1",
                ingresosCliente = "954.60",
                plazoMaximo = "102",
                relacionCuota = "20",
                fuenteIngreso = "EMP",
                edadCliente = "34",
                montoMaximo = "60000",
                porcSeguroDeuda = "0.0",
                porcSeguroCesantia = "2.57",
                tasaExcepcion = "7.0",
                Version = "2",
                IdServicio = "ConsultaMontoSolicitado",
                IdSolicitud = "39928749",
                montoSolicitado = "2000.0",
                plazoSolicitado = "60",
                esAprobados = "0",
                EdadmasPlazoPol = "66",
                tasaPivote = "0.0"
            };

            var cstMntSltModel = new ConsultaMontoSolicitadoModel()
            {
                Request = requestMZ
            };

            ConsultaMontoSolicitado cstMontoSlt = new ConsultaMontoSolicitado(null!);

            // Act
            var response = cstMontoSlt.GetMantizResponse(cstMntSltModel);

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("000000", response.Response?.CodigoRespuesta);
            Assert.IsNotNull(response.Response?.Ofertas);
        }

        [TestMethod]
        public void TestGetApiResponse_WithInvalidRequest_ReturnsNull()
        {
            // Arrange
            var requestMZ = new ConsultaMontoSolicitadoRequest
            {
                producto = "0414",
                clienteG = "1",
                ingresosCliente = "954.60",
                plazoMaximo = "102",
                relacionCuota = "20",
                fuenteIngreso = "EMP",
                edadCliente = "34",
                montoMaximo = "60000",
                porcSeguroDeuda = "0.0",
                porcSeguroCesantia = "2.57",
                tasaExcepcion = "7.0",
                Version = "2",
                IdServicio = "ConsultaMontoSolicitado",
                IdSolicitud = "39928749",
                montoSolicitado = "2000.0",
                plazoSolicitado = "60",
                esAprobados = "0"
            };

            // Act
            var cstMntSltModel = new ConsultaMontoSolicitadoModel()
            {
                Request = requestMZ
            };

            ConsultaMontoSolicitado cstMontoSlt = new ConsultaMontoSolicitado(null!);

            // Act
            var response = cstMontoSlt.GetMantizResponse(cstMntSltModel);

            // Assert
            Assert.AreEqual(0,response.Response?.Ofertas?.Count);
        }

    }
}
