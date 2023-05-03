using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZ_WorkerService.Models.Api.ConsultaOfertaMaxima
{
    public class ApiConsultaOfertaMaximaRequest : ApiRequest
    {
        public string? idCliente { get; set; }
    }
}
