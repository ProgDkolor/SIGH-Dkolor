using System.Collections.Generic;

namespace API.PONCHADOR.Modelo
{
    public class RData
    {
        public int Empresa { get; set; }
        public string Sp { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public List<ParametrosJson> DataParametros { get; set; }
    }
}
