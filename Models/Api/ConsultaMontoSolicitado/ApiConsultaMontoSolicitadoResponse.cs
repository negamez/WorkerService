using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZ_WorkerService.Models.Api.ConsultaMontoSolicitado
{
    public class ApiConsultaMontoSolicitadoResponse : ApiResponse
    {
        public List<ApiResponseMontoSolicitado>? consultaMontoSolicitado;
    }


    public class ApiResponseMontoSolicitado
    {
        public int? plazo { get; set; }
        public decimal? cuotaMes { get; set; }
        public decimal? cuotaKI { get; set; }
        public decimal? tasanominal { get; set; }

        public decimal? tasaEfectiva { get; set; }

        public decimal? cuotaSeguroDeuda { get; set; }

        public decimal? cuotaSeguroDesempleo { get; set; }

        public decimal? montoSolicitado { get; set; }
    }

}
