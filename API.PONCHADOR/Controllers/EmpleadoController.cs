using System;
using API.PONCHADOR.Modelo;
using API.PONCHADOR.Modelo.DTOs;
using API.PONCHADOR.Servicios.Empleados;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.PONCHADOR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private ICRUDEmpleado _empleadosServices;
        private RUsuario _rUsuario;

        public EmpleadoController(ICRUDEmpleado empleadosServices)
        {
            //...
            _empleadosServices = empleadosServices;
            _rUsuario = new RUsuario();
        }

        [HttpGet("listar-empleados")]
        public ActionResult<RUsuario> ListarEmpleado()
        {
            //... Crear Pruebas Unitarias
            return _empleadosServices.ListadoDeEmpleados();
        }
        [HttpGet("perfil-empleado-por-id/{idEmpleado}")]
        public ActionResult<RUsuario> PerfilEmpleadoPorId(string idEmpleado)
        {
            //... Crear Pruebas Unitarias
            return _empleadosServices.PerfilEmpleado(idEmpleado: idEmpleado);
        }
        [HttpGet("perfil-empleado-por-cedula/{cedula}")]
        public ActionResult<RUsuario> PerfilEmpleadoPorCedula(string cedula)
        {
            //... Crear Pruebas Unitarias
            return _empleadosServices.PerfilEmpleado(cedulaEmpleado: cedula);
        }
        [HttpGet("bloquear-empleado/{idEmpleado}")]
        public ActionResult<RUsuario> BloquearEmpleado(string idEmpleado)
        {
            //... Crear Pruebas Unitarias
            return _empleadosServices.BloquearEmpleado(idEmpleado);
        }
        [HttpPost("nuevo-usuario")]
        public ActionResult<RUsuario> NuevoUsuario([FromBody] UsuarioDTO nuevoUsuario)
        {
            //... Crear Pruebas Unitarias
            if (nuevoUsuario == null)
            {
                return BadRequest("Debe completar los campos.");
            }

            _rUsuario = _empleadosServices.NuevoUsuario(nuevoUsuario);
            if (_rUsuario.Respuesta == false && _rUsuario.Data == null)
            {
                return BadRequest($"Ocurrio un error: {_rUsuario.Mensaje}");
            }

            return _rUsuario;
        }

        [HttpGet("listar-usuarios")]
        public ActionResult<RUsuario> ListadoUsuarios()
        {
            //... Crear Pruebas Unitarias
            return _empleadosServices.ListarUsuarios();
        }

        [HttpGet("bloquear-usuarios/{idUsuario}")]
        public ActionResult<RUsuario> BloquearUsuarios(string idUsuario)
        {
            //... Crear Pruebas Unitarias
            return _empleadosServices.BloquearUsuario(usuarioId: idUsuario);
        }

        [HttpGet("listar-cargos-empleados")]
        public ActionResult<RUsuario> ListadoDeCargos()
        {
            return _empleadosServices.CargosEmpleados();
        }

        [HttpGet("listar-roles")]
        public ActionResult<RUsuario> ListarRoles()
        {
            return _empleadosServices.RolesUsuarios();
        }
    }
}
