using Microsoft.AspNetCore.Mvc;

namespace API.PONCHADOR.Modelo
{
    public class ResArchivo
    {
        public bool Exito { get; set; } = false;
        public string Mensaje { get; set; } = string.Empty;
        public FileResult Data { get; set; }
    }
}
