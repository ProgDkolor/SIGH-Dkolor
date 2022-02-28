using API.PONCHADOR.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.PONCHADOR.Servicios
{
    public interface ISql
    {
        Task<ResPonchadas> ConsultaPonchadas();
        Task<ResPonchadas> ConsultaPonchadasEmpleado(DateTime fechaInicio, DateTime fechaFinal, int empleado);
        Task<object> ConsultaBd<T>(string sp, List<Parametros> parametros, string bd);
        Task<object> RegistroBd<T>(string sp, List<Parametros> parametros, string bd);
        Task<ResEmpresas> ConsultaEmpresaId(int id);
        Task<ResEmpresas> ListadoEmpresas();
        Task<object> ConsultaBdMaster<T>(string sp, List<Parametros> parametros);
        Task<object> RegistroBdMaster<T>(string sp, List<Parametros> parametros);
    }
}
