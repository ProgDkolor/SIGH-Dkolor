using System;
using System.ComponentModel.DataAnnotations;

namespace API.PONCHADOR.Modelo.DTOs
{
    public class UsuarioDTO
    {
        public string Id { get; set; }
        [Required]
        public string Usuario { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Clave { get; set; }
        public int perfil { get; set; }
        public DateTime Fecha { get; set; }
        public int Estado { get; set; }
        [Required]
        [MinLength(11)]
        [MaxLength(11)]
        public string Cedula { get; set; }
        public int Modelo { get; set; }
        public int CargoEmpleadosId { get; set; }
        public int RolUsuarioId { get; set; }
    }
}
