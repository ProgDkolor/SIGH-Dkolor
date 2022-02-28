using API.PONCHADOR.Modelo;
using API.PONCHADOR.Modelo.DTOs;

namespace API.PONCHADOR.Servicios
{
    public interface IAutenticacion
    {
        Token ConstruirToken(EmpleadoToken empleadoToken);
    }
}
