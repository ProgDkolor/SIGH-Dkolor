using System.Collections.Generic;

namespace API.PONCHADOR.Modelo
{
    public class ResPonchadas
    {
        public bool Exito { get; set; } = false;
        public string Mensaje { get; set; } = string.Empty;
        public IEnumerable<Ponchadas> Data { get; set; }
    }
}
