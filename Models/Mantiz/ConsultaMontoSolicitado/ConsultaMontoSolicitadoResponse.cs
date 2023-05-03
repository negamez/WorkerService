
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MZ_WorkerService.Models.Mantiz.ConsultaMontoSolicitado
{
    public class ConsultaMontoSolicitadoResponse  : MantizResponse
    {
        [XmlArray("Ofertas")]
        [XmlArrayItem("OfertaSolicitada")]
        public List<OfertaSolicitada>? Ofertas { get; set; }

    }
    public class OfertaSolicitada
    {
        public string? plazo { get; set; }
        public string? cuotaMes { get; set; }
        public string? cuotaKI { get; set; }
        public string? tasaNominal { get; set; }
        public string? tasaEfectiva { get; set; }
        public string? cuotaSeguroDeuda { get; set; }
        public string? cuotaSeguroDesempleo { get; set; }
        public string? montoSolicitado { get; set; }


    }
}


