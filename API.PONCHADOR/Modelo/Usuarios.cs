using System;

namespace API.PONCHADOR.Modelo
{
    public class Usuarios
    {
        public string Id { get; set; }
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Clave { get; set; }
        public int perfil { get; set; }
        public DateTime Fecha { get; set; }
        public int Estado { get; set; }
        public string Cedula { get; set; }
        public int Modelo { get; set; }
        public int CargoEmpleadosId { get; set; }
        public int RolUsuarioId { get; set; }
    }
}
