using API.PONCHADOR.Modelo;
using API.PONCHADOR.Modelo.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Data.SqlClient;
using Dapper;

namespace API.PONCHADOR.Servicios
{
    public class SAutenticacion : IAutenticacion
    {
        private IConfiguration _configuration;
        private Usuarios _usuarios;
        private string _connection;

        public SAutenticacion(IConfiguration configuration)
        {
            //...
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("NOMINA_MASTER_Desarrollo");
            _usuarios = new Usuarios();
        }
        public Token ConstruirToken(EmpleadoToken empleadoToken)
        {
            var claims = new List<Claim>{
            new Claim( JwtRegisteredClaimNames.UniqueName, "aquí agregamos un valor único" ),
            new Claim( "id", empleadoToken.Id ),
            new Claim( "usuario", empleadoToken.Usuario ),
            new Claim( "CargoEmpleadoId", empleadoToken.CargoEmpleadoId.ToString() ),
            new Claim( "RolUsuarioId", empleadoToken.RolUsuarioId.ToString() )
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTkey"])); // Invocó la llave desde el appsettings

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // Encripto el token

            var expiration = DateTime.UtcNow.AddHours(1); // Determino el tiempo de duración.

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims, // es la información segura que guardamos más arriba.
                expires: expiration,
                signingCredentials: creds);

            return new Token()
            {
                // La clase que es se esta retornando fue la creada en el curso
                TokenJSON = new JwtSecurityTokenHandler().WriteToken(token),
                FechaDeExpiracion = expiration
            };
        }
    }
}
