using API.PONCHADOR.Modelo;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using API.PONCHADOR.Modelo.DTOs;
using Dapper;
using System.Data.SqlClient;
using AutoMapper;

namespace API.PONCHADOR.Servicios
{
    public class SControlDeRutas : IControlDeRutas
    {
        private IConfiguration _configuration;
        private string _connection;
        private RRutas _rRutas;
        private IMapper _mapper;
        private RMenu _rMenu;
        private PermisoPorRol _permisoPorRol;
        private List<Rutas> _listaDeRutas;
        private List<Menu> _listadoMenu;
        private AccionesAplicablesEnLaRutaDTO _accionesAplicablesEnLaRuta;

        public SControlDeRutas(IConfiguration configuration, IMapper mapper)
        {
            //...
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("NOMINA_MASTER_Desarrollo");
            _rMenu = new RMenu();
            _listaDeRutas = new List<Rutas>();
            _listadoMenu = new List<Menu>();
            _rRutas = new RRutas();
            _mapper = mapper;
        }

        private AccionesAplicablesEnLaRutaDTO AccionesAplicables(string usuarioId)
        {
            //...
            try
            {
                var query = "select * from tblPermisosPorRol where IdUsuario = @usuarioId";
                using (var db = new SqlConnection(_connection))
                {
                    _permisoPorRol = (PermisoPorRol)db.QueryFirst<PermisoPorRol>(query, new { usuarioId = usuarioId });
                    _accionesAplicablesEnLaRuta = _mapper.Map<AccionesAplicablesEnLaRutaDTO>(_permisoPorRol);
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return _accionesAplicablesEnLaRuta;
        }

        private List<int> ModulosDisponiblesPorUsuario(int rolUsuarioId)
        {
            // Este método lo utilizo para buscar el modulo al que pertenece el
            List<int> modulos = new List<int>();
            int modulosDisponibles = 0;
            try
            {
                //...
                var query =
                    "select distinct ruta.ModuloId from tblRutas ruta inner join tblMenu menu on ruta.id = menu.rutaId inner join tblPermisosUsuarios permisosUsuarios on menu.rolId = @rolUsuarioId";
                
                using (var db = new SqlConnection(_connection))
                {
                    var resultadoDeModulos = (List<Rutas>)db.Query<Rutas>(query, new { rolUsuarioId = rolUsuarioId });
                    if (resultadoDeModulos.Count > 1)
                    {
                        resultadoDeModulos.ForEach(m => modulos.Add(m.ModuloId));
                    }
                    else
                    {
                        resultadoDeModulos.ForEach(m => modulos.Add(m.ModuloId));
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return modulos;
        }

        private RRutas ListadoDeRutas(int permisosPorRolId, int rolUsuarioId, string IdUsuario)
        {
            try
            {
                var query =
                    "select distinct ruta.id, ruta.nombre, ruta.ruta, ruta.ModuloId, menu.rolId from tblRutas ruta inner join tblMenu menu on ruta.id = menu.rutaId inner join tblPermisosUsuarios permisosUsuarios on menu.rolId = @rolUsuarioid";
                using (var db = new SqlConnection(_connection))
                {
                    //...
                    _listaDeRutas = (List<Rutas>)db.Query<Rutas>(query, new { rolUsuarioid = rolUsuarioId });
                    _rRutas.Mensaje = "Exito";
                    _rRutas.Resultado = true;
                    _rRutas.Data = _listaDeRutas;
                    _rRutas.ModulosDisponibles = ModulosDisponiblesPorUsuario(rolUsuarioId);
                    _rRutas.AplicablesEnLaRuta = AccionesAplicables(IdUsuario);
                }
            }
            catch (Exception e)
            {
                _rRutas.Resultado=false;
                _rRutas.Mensaje = e.Message;
            }
            return _rRutas;
        }

        private RRutas ListadoDeMenu(int rolUsuarioid, string empleadoId)
        {
            //... Buscamos el permiso por rol.
            try
            {
                var query 
                = "select * from tblPermisosPorRol where rolUsuarioid = @rolUsuarioid and IdUsuario = @empleadoId";
                using (var db = new SqlConnection(_connection))
                {
                    // Consultar el permiso.
                    _permisoPorRol = db.QueryFirst<PermisoPorRol>
                        (query, new { rolUsuarioid = rolUsuarioid, empleadoId = empleadoId });


                    //Consultar la tblMenu por permisosPorRol.
                }
                return ListadoDeRutas(_permisoPorRol.Id, _permisoPorRol.RolUsuarioId, _permisoPorRol.IdUsuario);
            }
            catch (Exception e)
            {
                _rRutas.Resultado=false;
                _rRutas.Mensaje=e.Message.ToString();
            }
            return _rRutas;
        }

        public RRutas BuscarOpcionesDiponible(ParametrosFiltrosPorRolDTO parametrosFiltrosPorRol)
        {
            _rRutas = ListadoDeMenu(parametrosFiltrosPorRol.RolUsuarioId, parametrosFiltrosPorRol.EmpleadoId);
            if (_rRutas.Data == null || _rRutas.Resultado == false)
            {
                return _rRutas;
            }
            return _rRutas;
        }
    }
}
