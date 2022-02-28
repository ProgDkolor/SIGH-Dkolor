using API.PONCHADOR.Modelo;
using API.PONCHADOR.Modelo.DTOs;
using System.Collections.Generic;

namespace API.PONCHADOR.Servicios
{
    public interface IControlDeRutas
    {
        RRutas BuscarOpcionesDiponible(ParametrosFiltrosPorRolDTO parametrosFiltrosPorRol);
    }
}
