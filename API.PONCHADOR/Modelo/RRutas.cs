using API.PONCHADOR.Modelo.DTOs;
using System.Collections.Generic;

namespace API.PONCHADOR.Modelo
{
    public class RRutas
    {
        public bool Resultado { get; set; }
        public string Mensaje { get; set; }
        public List<Rutas> Data { get; set; }
        public List<int> ModulosDisponibles { get; set; }
        public AccionesAplicablesEnLaRutaDTO AplicablesEnLaRuta { get; set; }
    }
}
