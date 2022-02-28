using System;

namespace API.PONCHADOR.Modelo
{
    public class DetallePagosNomina
    {
        public string Empleado { get; set; }
        public int MedioPago { get; set; }
        public int Banco { get; set; }
        public string CuentaBancaria { get; set; }
        public decimal Valor { get; set; }
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; }
        public int TipoPago { get; set; }
        public int IdCuentaEmpresa { get; set; }
        public string DescripcionTipoPago { get; set; }
        public string CuentaEmpresa { get; set; }
    }
}
