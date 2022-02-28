using System;

namespace API.PONCHADOR.Modelo
{
    public class GeneraNominaPrevia
    {
        public string Periodo { get; set; }
        public DateTime? Fecha { get; set; }
        public string Modulo { get; set; }
        public string Dependencia { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string CuentaBanco { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public decimal? Sueldo { get; set; }
        public decimal? SueldoQuincenal { get; set; }
        public decimal? PagoEfectivo { get; set; }
        public decimal? transferencia { get; set; }
        public decimal? cheque { get; set; }
        public decimal? Dietas { get; set; }
        public decimal? HorasExtras { get; set; }
        public decimal? ISR { get; set; }
        public decimal? AFP { get; set; }
        public decimal? TSS_SFS { get; set; }
        public decimal? sueldoNeto { get; set; }
        public string descripcion { get; set; }
        public string cargoEmpleado { get; set; }
    }
}
