using System;

namespace API.PONCHADOR.Modelo
{
    public class Ponchadas
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cargo { get; set; }
        public decimal Salario { get; set; }
        public DateTime Fecha { get; set; }
        public string Entrada { get; set; }
        public string InicioAlmuerzo { get; set; }
        public string FinAlmuerzo { get; set; }
        public string Salida { get; set; }
        public string Calculo1 { get; set; }
        public string Calculo2 { get; set; }
        public string Total { get; set; }
        public int Horas { get; set; }
        public int Minutos { get; set; }
        public int Extras { get; set; }
        public decimal SalarioDiario { get; set; }
        public decimal SalarioHora { get; set; }
        public decimal MontoExtra { get; set; }
    }
}
