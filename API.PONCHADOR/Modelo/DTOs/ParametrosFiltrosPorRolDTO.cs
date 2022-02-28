using System.ComponentModel.DataAnnotations;

namespace API.PONCHADOR.Modelo.DTOs
{
    public class ParametrosFiltrosPorRolDTO
    {
        public string EmpleadoId { get; set; }
        public int RolUsuarioId { get; set; }
    }
}
