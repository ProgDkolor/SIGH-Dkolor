using System.Collections.Generic;

namespace API.PONCHADOR.Modelo
{
    public class ResEmpresas
    {
        public bool Exito { get; set; } = false;
        public string Mensaje { get; set; } = string.Empty;
        public IEnumerable<Empresas> Data { get; set; }
    }
}
