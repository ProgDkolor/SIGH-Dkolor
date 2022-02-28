using System;

namespace API.PONCHADOR.Modelo
{
    public class ComprobantePago
    {
        public string id { get; set; }
        public string descripcionNomina { get; set; }
        public int tipo { get; set; }
        public string cedula { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime fechaEntrada { get; set; }
        public int cargo { get; set; }
        public int concepto { get; set; }
        public decimal cantidad { get; set; }
        public decimal salarioMensual { get; set; }
        public string tipoNomina { get; set; }
        public string descripcionCargo { get; set; }
        public int? medioPago { get; set; }
        public string descripcionMedioPago { get; set; }
        public string cuentaBancaria { get; set; }
        public string descripcionPago { get; set; }
        public decimal ingreso { get; set; }
        public decimal descuento { get; set; }
        public string descripcionConcepto { get; set; }
    }
}
