using System.ComponentModel.DataAnnotations;

namespace API.PONCHADOR.Modelo.DTOs
{
    public class EmpleadoToken
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Usuario { get; set; }
        [Required]
        public int CargoEmpleadoId { get; set; }
        [Required]
        public int RolUsuarioId { get; set; }
    }
}
