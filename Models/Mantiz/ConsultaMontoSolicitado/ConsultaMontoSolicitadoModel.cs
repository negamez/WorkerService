using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MZ_WorkerService.Models.Mantiz.ConsultaMontoSolicitado
{
    [XmlRoot("Root")]
    public class ConsultaMontoSolicitadoModel
    {


        [XmlAttribute("IdLogTarea")]
        public int IdLogTarea { get; set; }
        public ConsultaMontoSolicitadoRequest? Request { get; set; }
        public ConsultaMontoSolicitadoResponse? Response { get; set; }

    }
}
