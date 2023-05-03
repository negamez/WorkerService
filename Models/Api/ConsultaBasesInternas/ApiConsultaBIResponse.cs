namespace MZ_WorkerService.Models.Api.ConsultaBasesInternas
{   
    public class ApiConsultaBIResponse : ApiResponse
    {
        public double INGRESOFINAL { get; set; }
        public int ANTIGUEDADLABORALFINAL { get; set; }
        public string? FUENTEINFORMACION { get; set; }
        public string? NOMBRE { get; set; }
        public string? CodARFinal { get; set; }

    }
}