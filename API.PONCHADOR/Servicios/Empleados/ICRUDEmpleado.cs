using API.PONCHADOR.Modelo;
using API.PONCHADOR.Modelo.DTOs;
using System.Collections.Generic;

namespace API.PONCHADOR.Servicios.Empleados
{
    public interface ICRUDEmpleado
    {
        //Estos métodos hacen llamados a la tabla empleados.
        RUsuario BuscarEmpleado(string cedula = null, string id = null);
        RUsuario ListadoDeEmpleados();
        RUsuario PerfilEmpleado(string idEmpleado = "", string cedulaEmpleado = "");
        RUsuario BloquearEmpleado(string idEmpleado);
        RUsuario EstadoEmpleado();
        RUsuario CargosEmpleados();

        //Estos métodos hacen llamados a la tabla usuarios.
        RUsuario NuevoUsuario(UsuarioDTO nuevoUsuario);
        RUsuario BuscarUsuario(string cedula = null, string id = null);
        RUsuario ListarUsuarios();
        RUsuario BloquearUsuario(string usuarioId);
        RUsuario EstadoUsuario();
        RUsuario RolesUsuarios();
    }
}
