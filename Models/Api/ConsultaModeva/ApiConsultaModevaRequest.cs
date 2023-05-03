using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZ_WorkerService.Models.Api.ConsultaModeva
{
    public class ApiConsultaModevaRequest : ApiRequest
    {
        public string? idCliente { get; set; }

        public string? idProducto { get; set; }
    }
}
