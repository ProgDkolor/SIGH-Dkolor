using System.Collections.Generic;

namespace API.PONCHADOR.Modelo
{
    public class ResListado
    {
        public bool Exito { get; set; } = false;
        public string Mensaje { get; set; } = string.Empty;
        public IEnumerable<Listado> Data { get; set; }
    }
}
