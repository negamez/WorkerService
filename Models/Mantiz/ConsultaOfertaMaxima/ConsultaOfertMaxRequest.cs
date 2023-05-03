

namespace MZ_WorkerService.Models.Mantiz.ConsultaOfertaMaxima
{
    public class ConsultaOfertMaxRequest : MantizRequest
    {
        public string? producto { get; set; }
        public string? clienteG { get; set; }
        public string? ingresosCliente { get; set; }
        public string? plazoMaximo { get; set; }
        public string? relacionCuota { get; set; }
        public string? edadCliente { get; set; }
        public string? fuenteIngreso { get; set; }
        public string? montoMaximo { get; set; }
        public string? tasaExcepcion { get; set; }
        public string? porcSeguroDeuda { get; set; }
        public string? porcSeguroCesantia { get; set; }
        public string? esAprobados { get; set; }
        public string? EdadmasPlazoPol { get; set; }
        public string? tasaPivote { get; set; }
        public string? plazoSolicitado { get; set; }
    }
}
