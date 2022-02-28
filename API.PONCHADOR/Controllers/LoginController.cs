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
    public class LoginController : ControllerBase
    {
        private IAutenticacion _autenticacion;

        public LoginController(IAutenticacion autenticacion)
        {
            //...
            _autenticacion = autenticacion;
        }

        [HttpPost("GetToken")]
        public ActionResult<Token> GetToken([FromBody] EmpleadoToken empleadoToken)
        {
            //...
            return _autenticacion.ConstruirToken(empleadoToken);
        }
    }
}
