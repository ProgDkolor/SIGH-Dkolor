using API.PONCHADOR.Modelo;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API.PONCHADOR.Servicios
{
    public class Sql:ISql
    {
        readonly IConfiguration conf;
        public Sql(IConfiguration configuration)
        {
            conf = configuration;
        }
        public string conMaster()
        {
            //return ($"Data Source={conf.GetSection("ParametrosDb:HostDb").Value};uid=sa;pwd=Dk0l0r2020@;Initial Catalog=DBPonche");
            //return ($"Data Source={conf.GetSection("ParametrosDb:HostDb").Value};uid=registroWebMotor;pwd=Wmn@O!99fgZ;Initial Catalog=DKOLOR.SERVICIO");
            return ($"Data Source={conf.GetSection("ParametrosDb:HostDb").Value};uid=sa;pwd=mkgchalona123;Initial Catalog=NOMINA_MASTER");
        }
        public string conEmpresa(string bd)
        {
            return ($"Data Source=LOCALHOST;uid=sa;pwd=mkgchalona123;Initial Catalog={bd}");
        }
        public Object DameClase(string s)
        {
            dynamic res = null;
            if (s == "ResListado")
            {
                res = new ResListado();
            }
            return res;
        }
        public async Task<ResEmpresas> ConsultaEmpresaId(int id)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@id", id);
            ResEmpresas res = new ResEmpresas() { Exito = false };
            try
            {
                using (IDbConnection db = new SqlConnection(conMaster()))
                {
                    res.Exito = true;
                    res.Data = await db.QueryAsync<Empresas>("ConsultaEmpresaId", p, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                res.Exito = false;
                res.Mensaje = $"SERVIDOR BD\n {ex.Message}";
            }
            return (res);
        }

        public async Task<ResEmpresas> ListadoEmpresas()
        {
            ResEmpresas res = new ResEmpresas() { Exito = false };
            try
            {
                using (IDbConnection db = new SqlConnection(conMaster()))
                {
                    res.Exito = true;
                    res.Data = await db.QueryAsync<Empresas>("ListadoEmpresas", commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                res.Exito = false;
                res.Mensaje = $"SERVIDOR BD\n {ex.Message}";
            }
            return (res);
        }
        public async Task<ResPonchadas> ConsultaPonchadas()
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@fechaInicio","2021-12-1");
            p.Add("@fechaFin", "2021-12-15");
            p.Add("@empleado",760);
            ResPonchadas res = new ResPonchadas() { Exito = false };
            try
            {
                using (IDbConnection db = new SqlConnection(conMaster()))
                {
                    res.Exito = true;
                    res.Data = await db.QueryAsync<Ponchadas>("ConsultaPonchadasEmpleado", p, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                res.Exito = false;
                res.Mensaje = $"SERVIDOR BD\n {ex.Message}";
            }
            return (res);
        }
        public async Task<ResPonchadas> ConsultaPonchadasEmpleado(DateTime fechaInicio,DateTime fechaFinal,int empleado)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@fechaInicio", fechaInicio.ToString("yyyy-MM-d"));
            p.Add("@fechaFin", fechaFinal.ToString("yyyy-MM-d"));
            p.Add("@empleado",empleado);
            ResPonchadas res = new ResPonchadas() { Exito = false };
            try
            {
                using (IDbConnection db = new SqlConnection(conMaster()))
                {
                    res.Exito = true;
                    res.Data = await db.QueryAsync<Ponchadas>("ConsultaPonchadasEmpleado", p, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                res.Exito = false;
                res.Mensaje = $"SERVIDOR BD\n {ex.Message}";
            }
            return (res);
        }

            public async Task<object> ConsultaBdMaster<T>(string sp, List<Parametros> parametros)
        {
            DynamicParameters p = new DynamicParameters();
            parametros.ForEach(e => {
                p.Add(e.Parametro,e.Valor, e.Tipo);
            });
            dynamic res =new ResData<T>();
            try
            {
                using (IDbConnection db = new SqlConnection(conMaster()))
                {
                    res.Exito = true;
                    if (parametros.Count > 0)
                    {
                        res.Data = await db.QueryAsync<T>(sp, p, commandType: CommandType.StoredProcedure);
                    }
                    else {
                        res.Data = await db.QueryAsync<T>(sp, commandType: CommandType.StoredProcedure);
                    }
                }
            }
            catch (Exception ex)
            {
                res.Exito = false;
                res.Mensaje = $"SERVIDOR BD\n {ex.Message}";
            }
            return (res);
        }

        public async Task<object> ConsultaBd<T>(string sp, List<Parametros> parametros,string bd)
        {
            DynamicParameters p = new DynamicParameters();
            parametros.ForEach(e => {
                p.Add(e.Parametro, e.Valor, e.Tipo);
            });
            dynamic res = new ResData<T>();
            try
            {
                using (IDbConnection db = new SqlConnection(conEmpresa(bd)))
                {
                    res.Exito = true;
                    if (parametros.Count > 0)
                    {
                        res.Data = await db.QueryAsync<T>(sp, p, commandType: CommandType.StoredProcedure);
                    }
                    else
                    {
                        res.Data = await db.QueryAsync<T>(sp, commandType: CommandType.StoredProcedure);
                    }
                }
            }
            catch (Exception ex)
            {
                res.Exito = false;
                res.Mensaje = $"SERVIDOR BD\n {ex.Message}";
            }
            return (res);
        }

        public async Task<object> RegistroBdMaster<T>(string sp, List<Parametros> parametros)
        {
            DynamicParameters p = new DynamicParameters();
            parametros.ForEach(e => {
                p.Add(e.Parametro, e.Valor, e.Tipo);
            });
            dynamic res = new ResData<T>();
            try
            {
                using (IDbConnection db = new SqlConnection(conMaster()))
                {
                    res.Exito = true;
                    if (parametros.Count > 0)
                    {
                        res.Data = await db.QueryAsync<T>(sp, p, commandType: CommandType.StoredProcedure);
                    }
                    else
                    {
                        res.Data = await db.QueryAsync<T>(sp, commandType: CommandType.StoredProcedure);
                    }
                }
            }
            catch (Exception ex)
            {
                res.Exito = false;
                res.Mensaje = $"SERVIDOR BD\n {ex.Message}";
            }
            return (res);
        }

        public async Task<object> RegistroBd<T>(string sp, List<Parametros> parametros,string bd)
        {
            DynamicParameters p = new DynamicParameters();
            parametros.ForEach(e => {
                p.Add(e.Parametro, e.Valor, e.Tipo);
            });
            dynamic res = new ResData<T>();
            try
            {
                using (IDbConnection db = new SqlConnection(conEmpresa(bd)))
                {
                    res.Exito = true;
                    if (parametros.Count > 0)
                    {
                        res.Data = await db.QueryAsync<T>(sp, p, commandType: CommandType.StoredProcedure);
                    }
                    else
                    {
                        res.Data = await db.QueryAsync<T>(sp, commandType: CommandType.StoredProcedure);
                    }
                }
            }
            catch (Exception ex)
            {
                res.Exito = false;
                res.Mensaje = $"SERVIDOR BD\n {ex.Message}";
            }
            return (res);
        }
    }
}
