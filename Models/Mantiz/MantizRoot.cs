using System.Xml.Serialization;

namespace MZ_WorkerService.Models.Mantiz
{
    [XmlRoot("Root")]
    public class MantizRoot
    {
        [XmlAttribute("IdLogTarea")]
        public int IdLogTarea { get; set; }

        [XmlElement("Request")]
        public MantizRequest? Request { get; set; }

        [XmlElement("Response")]
        public MantizResponse? Response { get; set; }
    }
}