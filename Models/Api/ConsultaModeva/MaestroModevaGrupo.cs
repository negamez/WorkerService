using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZ_WorkerService.Models.Api.ConsultaModeva
{
    public class MaestroModevaGrupo
    {
        public class MaestroModevaItem
        {
            public string? IngestionYear { get; set; }
            public string? IngestionMonth { get; set; }
            public string? IngestionDay { get; set; }
            public string? IdCliente { get; set; }
            public string? ModevaTipo { get; set; }
            public string? Producto { get; set; }
            public string? GOriginacionFinal { get; set; }
            public string? Activo { get; set; }
            public string? Migrar { get; set; }
        }

        public class RootMaestro
        {
            public List<MaestroModevaItem>? MaestroModevaItem { get; set; }
        }


    }
}
