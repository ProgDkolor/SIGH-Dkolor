using API.PONCHADOR.Modelo;
using API.PONCHADOR.Modelo.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace API.PONCHADOR.Servicios.Empleados
{
    public class SCRUDEmpleado : ICRUDEmpleado
    {
        private IMapper _mapper;
        private IConfiguration _configuration;
        private string _connectionNomina_Master_Desarrollo;
        private string _connectionNomina_DK_Desarrollo;

        private Usuarios _usuarios;
        private RUsuario _rUsuario;
        private List<UsuarioDTO> _listadoDeUsuarios;
        private DynamicParameters _dynamicParameters;

        public SCRUDEmpleado(IMapper mapper, IConfiguration configuration)
        {
            //...
            _mapper = mapper;
            _configuration = configuration;
            _connectionNomina_Master_Desarrollo = _configuration.GetConnectionString("NOMINA_MASTER_Desarrollo");
            _connectionNomina_DK_Desarrollo = _configuration.GetConnectionString("NOMINA_DK_Desarrollo");

            _usuarios = new Usuarios();
            _rUsuario = new RUsuario();
            _listadoDeUsuarios = new List<UsuarioDTO>();
            _dynamicParameters = new DynamicParameters();
        }
        public RUsuario BloquearEmpleado(string idEmpleado)
        {
            try
            {
                var queryParaCambiarEstado = "update empleados set estado = @estado where id = @id";
                var empleado = BuscarEmpleado(id: idEmpleado);
                var queryParaConsultarElEstadoQueFueAgregadoAlEmpleado = "select e.id, e.nombre, e.apellido, e.cedula, es.nombre from empleados e inner join tblEstadoEmpleado es on e.estado = es.estado where e.id = @id";
                using (var db = new SqlConnection(_connectionNomina_DK_Desarrollo))
                {
                    _rUsuario.Respuesta = true;
                    if (empleado.Data.estado == true) //... Hago la conversion ya que en base de datos retorna un bit.
                    {
                        _rUsuario.Mensaje = "Se cambio el estado a Inactivo";
                        db.Query(queryParaCambiarEstado, new { estado = false, id = idEmpleado });
                        _rUsuario.Data = db.Query<dynamic>(queryParaConsultarElEstadoQueFueAgregadoAlEmpleado, new { id = idEmpleado });
                    }
                    else if (empleado.Data.estado == false) //... Hago la conversion ya que en base de datos retorna un bit.
                    {
                        _rUsuario.Mensaje = "Se cambio el estado a Activo";
                        db.Query(queryParaCambiarEstado, new { estado = true, id = idEmpleado });
                        _rUsuario.Data = db.Query<dynamic>(queryParaConsultarElEstadoQueFueAgregadoAlEmpleado, new { id = idEmpleado });
                    }
                }
            }
            catch (Exception e)
            {
                _rUsuario.Respuesta = false;
                _rUsuario.Mensaje = e.Message;
                _rUsuario.Data = null;
            }
            return _rUsuario;
        }

        public RUsuario BloquearUsuario(string usuarioId)
        {
            try
            {
                var usuario = BuscarUsuario(id: usuarioId);
                var queryParaCambiarEstado = "update usuarios set estado = @estado where id = @id";
                // La consulta que contiene la variable "resultadoDelCambio" nos mostrara el estado al que se cambio.
                var resultadoDelCambio = "select u.id, u.usuario, u.nombre, u.apellido, e.nombre from usuarios u inner join tblEstadoUsuario e on u.estado = e.estado where u.id = @id";

                using (var db = new SqlConnection(_connectionNomina_Master_Desarrollo))
                {
                    _rUsuario.Respuesta = true;
                    if ((int)EnumEstado.Activo == usuario.Data.estado)
                    {
                        _rUsuario.Mensaje = "Se cambio el estado a Inactivo";
                        db.Query(queryParaCambiarEstado, new { estado = (int)EnumEstado.Inactivo, id = usuarioId });
                        _rUsuario.Data = db.Query<dynamic>(resultadoDelCambio, new { id = usuarioId });
                    }
                    else if ((int)EnumEstado.Inactivo == usuario.Data.estado)
                    {
                        _rUsuario.Mensaje = "Se cambio el estado a Activo";
                        db.Query(queryParaCambiarEstado, new { estado = (int)EnumEstado.Activo, id = usuarioId });
                        _rUsuario.Data = db.Query<dynamic>(resultadoDelCambio, new { id = usuarioId });
                    }
                }
            }
            catch (Exception e)
            {
                _rUsuario.Respuesta = false;
                _rUsuario.Mensaje = e.Message;
                _rUsuario.Data = null;
            }
            return _rUsuario;
        }

        public RUsuario BuscarEmpleado(string cedula = null, string id = null)
        {
            try
            {
                if (cedula != string.Empty && cedula != null)
                {
                    var query = "select * from empleados where cedula = @cedula";
                    using (var db = new SqlConnection(_connectionNomina_DK_Desarrollo))
                    {
                        _rUsuario.Respuesta = true;
                        _rUsuario.Mensaje = "Exito";
                        _rUsuario.Data = db.QueryFirst<dynamic>(query, new { cedula = cedula });
                    }
                }
                if (id != string.Empty && id != null)
                {

                    var query = "select * from empleados where id = @id";
                    using (var db = new SqlConnection(_connectionNomina_DK_Desarrollo))
                    {
                        _rUsuario.Respuesta = true;
                        _rUsuario.Mensaje = "Exito";
                        _rUsuario.Data = db.QueryFirst<dynamic>(query, new { id = id });
                    }
                }
            }
            catch (Exception e)
            {
                _rUsuario.Respuesta=false;
                _rUsuario.Mensaje=e.Message;
                _rUsuario.Data=null;
            }
            return _rUsuario;
        }

        public RUsuario BuscarUsuario(string cedula = null, string id = null)
        {
            try
            {
                if (cedula != string.Empty && cedula != null)
                {
                    var query = "select * from usuarios where cedula = @cedulaUsuario";
                    using (var db = new SqlConnection(_connectionNomina_Master_Desarrollo))
                    {
                        var usuario = db.QueryFirst<dynamic>(query, new { cedulaUsuario = cedula });
                        _rUsuario.Data = usuario;
                    }
                    _rUsuario.Respuesta = true;
                    _rUsuario.Mensaje = "El usuario existe";
                }
                if (id != string.Empty && id != null)
                {
                    var query = "select * from usuarios where id = @id";
                    using (var db = new SqlConnection(_connectionNomina_Master_Desarrollo))
                    {
                        var usuario = db.QueryFirst<dynamic>(query, new { id = id });
                        _rUsuario.Data = usuario;
                    }
                    _rUsuario.Respuesta = true;
                    _rUsuario.Mensaje = "El usuario existe";
                }
            }
            catch (Exception e)
            {
                return new RUsuario { Respuesta = false, Mensaje = e.Message, Data = null };
            }
            return _rUsuario;
        }

        public RUsuario CargosEmpleados()
        {
            try
            {
                var queryParaConsultarCargos = "select * from cargoEmpleados";
                using (var db = new SqlConnection(_connectionNomina_DK_Desarrollo))
                {
                    _rUsuario.Respuesta = true;
                    _rUsuario.Mensaje = "Exito";
                    _rUsuario.Data = db.Query<dynamic>(queryParaConsultarCargos);
                }
            }
            catch (Exception e)
            {
                _rUsuario.Respuesta = false;
                _rUsuario.Mensaje = e.Message;
                _rUsuario.Data = null;
            }
            return _rUsuario;
        }

        public RUsuario EstadoEmpleado()
        {
            try
            {
                var query = "select * from tblEstadoEmpleado";
                using (var db = new SqlConnection(_connectionNomina_DK_Desarrollo))
                {
                    _rUsuario.Respuesta = true;
                    _rUsuario.Mensaje = "Exito";
                    _rUsuario.Data = db.Query(query);
                }
            }
            catch (Exception e)
            {
                _rUsuario.Respuesta=false;
                _rUsuario.Mensaje = e.Message;
                _rUsuario.Data = null;
            }
            return _rUsuario;
        }

        public RUsuario EstadoUsuario()
        {
            try
            {
                var query = "select * from tblEstadoUsuario";
                using (var db = new SqlConnection(_connectionNomina_Master_Desarrollo))
                {
                    _rUsuario.Respuesta = true;
                    _rUsuario.Mensaje = "Exito";
                    _rUsuario.Data = db.Query(query);
                }
            }
            catch (Exception e)
            {
                _rUsuario.Respuesta = false;
                _rUsuario.Mensaje = e.Message;
                _rUsuario.Data = null;
            }
            return _rUsuario;
        }

        public RUsuario ListadoDeEmpleados()
        {
            try
            {
                var queryParaBuscarListadoDeUsuario = "select * from empleados";
                using (var db = new SqlConnection(_connectionNomina_DK_Desarrollo))
                {
                    _rUsuario.Respuesta = true;
                    _rUsuario.Mensaje = "Exito";
                    _rUsuario.Data = db.Query<dynamic>(queryParaBuscarListadoDeUsuario);
                }
            }
            catch (Exception e)
            {
                _rUsuario.Respuesta = false;
                _rUsuario.Mensaje = e.Message;
                _rUsuario.Data = null;
            }
            return _rUsuario;
        }

        public RUsuario ListarUsuarios()
        {
            try
            {
                var query =
                    "select u.*, eu.nombre[EstadoActual], rol.nombre[NombreDelRol] from usuarios u inner join tblEstadoUsuario eu on u.estado = eu.estado inner join tblRol rol on u.rolUsuarioId = rol.id";
                using (var db = new SqlConnection(_connectionNomina_Master_Desarrollo))
                {
                    _rUsuario.Respuesta = true;
                    _rUsuario.Mensaje = "Exito";
                    _rUsuario.Data = db.Query<dynamic>(query);
                }
            }
            catch (Exception e)
            {
                _rUsuario.Respuesta=false;
                _rUsuario.Mensaje = e.Message;
                _rUsuario.Data=null;
            }
            return _rUsuario;
        }

        public RUsuario NuevoUsuario(UsuarioDTO nuevoUsuario)
        {
            var consultarSiExisteUnUsuario = BuscarUsuario(nuevoUsuario.Cedula);

            try
            {
                //...
                if (consultarSiExisteUnUsuario.Respuesta == true)
                {
                    _dynamicParameters.Add("@usuario", nuevoUsuario.Usuario);
                    _dynamicParameters.Add("@nombre", nuevoUsuario.Nombre);
                    _dynamicParameters.Add("@apellido", nuevoUsuario.Apellido);
                    _dynamicParameters.Add("@clave", nuevoUsuario.Clave);
                    _dynamicParameters.Add("@perfil", 1);
                    _dynamicParameters.Add("@estado", 1);
                    _dynamicParameters.Add("@cedula", nuevoUsuario.Cedula);
                    _dynamicParameters.Add("@modulo", 1);
                    _dynamicParameters.Add("@cargoEmpleadoId", nuevoUsuario.CargoEmpleadosId);
                    _dynamicParameters.Add("@rolUsuarioId", nuevoUsuario.RolUsuarioId);

                    var query = "select * from usuarios order by fecha desc";

                    using (var db = new SqlConnection(_connectionNomina_Master_Desarrollo))
                    {
                        //...
                        db.Query("spAgregarNuevoUsuario", _dynamicParameters, commandType: CommandType.StoredProcedure);

                        _rUsuario.Respuesta = true;
                        _rUsuario.Mensaje = "Datos registrado con exito.";
                        var ultimoUsuarioRegistrado = db.QueryFirst<dynamic>(query);
                        _rUsuario.Data = ultimoUsuarioRegistrado;
                    }
                }
            }
            catch (Exception e)
            {
                _rUsuario.Respuesta = false;
                _rUsuario.Mensaje = e.Message;
                _rUsuario.Data = null;
            }
            return _rUsuario;
        }

        public RUsuario PerfilEmpleado(string idEmpleado = "", string cedulaEmpleado = "")
        {
            try
            {
                if (idEmpleado != String.Empty && idEmpleado != null)
                {
                    var queryParaBuscarElEmpleado = "select * from empleados where id = @id";
                    using (var db = new SqlConnection(_connectionNomina_DK_Desarrollo))
                    {
                        _rUsuario.Respuesta = true;
                        _rUsuario.Mensaje = "Exito";
                        _rUsuario.Data = db.QueryFirst(queryParaBuscarElEmpleado, new { id = idEmpleado });
                    }
                }
                if (cedulaEmpleado != String.Empty && idEmpleado != null)
                {
                    var queryParaBuscarElEmpleado = "select * from empleados where cedula = @cedula";
                    using (var db = new SqlConnection(_connectionNomina_DK_Desarrollo))
                    {
                        _rUsuario.Respuesta = true;
                        _rUsuario.Mensaje = "Exito";
                        _rUsuario.Data = db.QueryFirst(queryParaBuscarElEmpleado, new { cedula = cedulaEmpleado });
                    }
                }
            }
            catch (Exception e)
            {
                _rUsuario.Respuesta = false;
                _rUsuario.Mensaje=e.Message;
                _rUsuario.Data=null;
            }
            return _rUsuario;
        }

        public RUsuario RolesUsuarios()
        {
            try
            {
                var queryParaBuscarRolesDeLosUsuarios = "select * from tblRol";
                using (var db = new SqlConnection(_connectionNomina_Master_Desarrollo))
                {
                    _rUsuario.Respuesta = true;
                    _rUsuario.Mensaje = "Exito";
                    _rUsuario.Data = db.Query<dynamic>(queryParaBuscarRolesDeLosUsuarios);
                }
            }
            catch (Exception e)
            {
                _rUsuario.Respuesta =false;
                _rUsuario.Mensaje = e.Message;
                _rUsuario.Data = null;
            }
            return _rUsuario;
        }
    }
}
