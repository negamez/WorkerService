using System.Xml.Serialization;
namespace MZ_WorkerService.Models.Mantiz.ConsultaOfertaMaxima
{
    [XmlRoot("Root")]
    public class ConsultaOfertaMaximaModel
    {
        [XmlAttribute("IdLogTarea")]
        public int IdLogTarea { get; set; }
        public ConsultaOfertMaxRequest? Request { get; set; }
        public ConsultaOfertMaxResponse? Response { get; set; }
    }
}
