using System;

namespace API.PONCHADOR.Modelo
{
    public class ReporteNomina
    {
        public string Modulo { get; set; }
        public string Dependencia { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public DateTime FechaIngreso { get; set; }
        public decimal Sueldo { get; set; }
        public decimal SueldoQuincenal { get; set; }
        public decimal PagoEfectivo { get; set; }
        public decimal Transferencia { get; set; }
        public decimal Cheque { get; set; }
        public decimal Descuento { get; set; }
        public decimal ISR { get; set; }
        public decimal AFP { get; set; }
        public decimal TSS_SFS { get; set; }
        public decimal SueldoNeto { get; set; }
        public string CuentaBanco { get; set; }
        public int IdDepartamento { get; set; }
        public int IdModulo { get; set; }
    }
}
