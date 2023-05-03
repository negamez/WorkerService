namespace MZ_WorkerService.Models.Api.ConsultaBasesInternas
{
    public class Estimadoringreso
    {
        public double? IngestionYear { get; set; }
        public double? IngestionMonth { get; set; }
        public double? IngestionDay { get; set; }
        public string? IdCliente { get; set; }
        public string? Clasificacion { get; set; }
        public double? IngFinal { get; set; }
        public double? AntiguedadLaboralFinal { get; set; }
        public string? LugarTrabajoFinal { get; set; }
        public object? IngresoSolicitudes { get; set; }
        public object? AntiguedadLaboralSolicitudes { get; set; }
        public object? LugarTrabajoSolicitudes { get; set; }
        public string? FlagSolicitudes { get; set; }
        public double? IngPlanilla { get; set; }
        public double? AntiguedadLaboralPlanilla { get; set; }
        public string? LugarTrabajoPlanilla { get; set; }
        public double? Activo { get; set; }
        public double? Migrar { get; set; }
    }

    public class Root
    {
        public List<Estimadoringreso>? Estimadoringresos { get; set; }
    }


}
