using System;
using System.Collections.Generic;

namespace MZ_WorkerService.Models.Api.ConsultaOfertaMaxima
{
    public class ApiConsultaOfertaMaximaResponse : ApiResponse
    {
       public List<ApiResponseConsultaMax>? consultaOfertaMaxima;
    }


    public class ApiResponseConsultaMax
    {
        public int?  plazo { get; set; }
        public decimal? montoMaximo { get; set; }
        public decimal? cuota { get; set; }
        public decimal? tasa { get; set; }
    }
}
