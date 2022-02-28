using API.PONCHADOR.Modelo;
using API.PONCHADOR.Modelo.DTOs;
using API.PONCHADOR.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.PONCHADOR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlDeRutasController : ControllerBase
    {
        private IControlDeRutas _controlDeRutas;
        private RMenu _menu;

        public ControlDeRutasController(IControlDeRutas controlDeRutas)
        {
            //...
            _controlDeRutas = controlDeRutas;
            _menu = new RMenu();
        }

        [HttpPost("consultar-rutas")]
        public ActionResult<RRutas> ListarRutasPermitidas([FromBody] ParametrosFiltrosPorRolDTO parametrosFiltrosPorRol)
        {
            //...
            return _controlDeRutas.BuscarOpcionesDiponible(parametrosFiltrosPorRol);
        }
    }
}
