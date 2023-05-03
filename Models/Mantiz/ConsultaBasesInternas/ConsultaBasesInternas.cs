using System.Xml.Serialization;

namespace MZ_WorkerService.Models.Mantiz.ConsultaBasesInternas
{
    [XmlRoot("Root")]
    public class ConsultaBasesInternas
    {
        [XmlAttribute("IdLogTarea")]
        public int IdLogTarea { get; set; }
        public ConsultaBIRequest? Request { get; set; }
        public ConsultaBIResponse? Response { get; set; }
    }
}