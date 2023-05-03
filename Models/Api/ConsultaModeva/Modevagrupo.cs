namespace MZ_WorkerService.Models.Api.ConsultaModeva
{
    public class Modevagrupo
    {
        public string? IngestionYear { get; set; }
        public string? IngestionMonth { get; set; }
        public string? IngestionDay { get; set; }
        public string? IdCliente { get; set; }
        public string? GModeva { get; set; }
        public string? Escala { get; set; }
        public string? GFinal { get; set; }
        public string? ModevaTipo { get; set; }
        public string? Activo { get; set; }
        public string? Migrar { get; set; }
    }

    public class Root
    {
        public List<Modevagrupo>? Modevagrupos { get; set; }
    }


}
