using System.Xml.Serialization;

namespace MZ_WorkerService.Models.Mantiz.ConsultaModeva
{
    [XmlRoot("Root")]
    public class ConsultaModevaG
    {
        [XmlAttribute("IdLogTarea")]
        public int IdLogTarea { get; set; }
        public ConsultaModevaRequest? Request { get; set; }
        public ConsultaModevaResponse? Response { get; set; }
    }
}
