using System;

namespace API.PONCHADOR.Modelo
{
    public class Empleado
    {
        public Guid id { get; set; }
        public int codigo { get; set; }
        public string cedula { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int cargo { get; set; }
        public DateTime fechaEntrada { get; set; }
        public DateTime fechaRegistro { get; set; }
        public decimal salario { get; set; }
        public int contrato { get; set; }
        public bool estado { get; set; }
        public int situacion { get; set; }
        public int? medioPago { get; set; }
        public string cuentaBancaria { get; set; }
        public int? banco { get; set; }
        public int? departamento { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string direccion { get; set; }
        public string nota { get; set; }
        public int? modulo { get; set; }
    }
}
