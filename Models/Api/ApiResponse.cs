using System.Xml.Serialization;

namespace MZ_WorkerService.Models.Api
{   
    [XmlRoot("Response")]
    public class ApiResponse
    {
        [XmlElement("codigo")]
        public string? Codigo { get; set; }

        [XmlElement("descripcion")]
        public string? Descripcion { get; set; }

    }
}