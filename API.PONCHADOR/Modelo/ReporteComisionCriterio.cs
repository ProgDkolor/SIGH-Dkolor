using System;

namespace API.PONCHADOR.Modelo
{
    public class ReporteComisionCriterio
    {
        public string Id { get; set; }
        public string Cedula { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal Comision { get; set; }
        public int Modulo { get; set; }
        public int Estacion { get; set; }
        public decimal Recibido { get; set; }
        public decimal Devuelta { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Recibo { get; set; }
        public bool Estado { get; set; }
        public int Nivel { get; set; }
        public string Codigo { get; set; }
        public string DescripcionServicio { get; set; }
        public string Nombre { get; set; }
        public string NombreUsuario { get; set; }
        public int CodigoUsuario { get; set; }
        public string TipoDocumento { get; set; }
        public string DescripcionModulo { get; set; }
        public string Referencia { get; set; }
    }
}
