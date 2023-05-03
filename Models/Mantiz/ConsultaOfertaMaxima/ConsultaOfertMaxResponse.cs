

using System.Xml.Serialization;

namespace MZ_WorkerService.Models.Mantiz.ConsultaOfertaMaxima
{
    public class ConsultaOfertMaxResponse : MantizResponse
    {
        [XmlArray("Ofertas")]
        [XmlArrayItem("OfertaMaxima")]
        public List<OfertListModel>? Ofertas { get; set; }
    }


    public class OfertListModel
    {
        public string? plazo { get; set; }
        public string? montoMaximo { get; set; }
        public string? cuota { get; set; }
        public string? tasa { get; set; }
    }
}
