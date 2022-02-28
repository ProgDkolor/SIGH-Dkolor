using API.PONCHADOR.Modelo;
using API.PONCHADOR.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;
using System.IO;
using System.Net.Http;

namespace API.PONCHADOR.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SqlController : ControllerBase
    {
        readonly ISql sql;
        public SqlController(ISql servicioServer)
        {
            sql = servicioServer;

        }

        [HttpGet]
        [Route("ReporteNomina")]
        public async Task<FileResult> ReporteNomina(int empresa, string nomina,int reporte,string descripcionNomina,string descripcionReporte)
        {
            FileContentResult archivo = null;
            try
            {
                //object res = null;
                var resEmpresas = await sql.ConsultaEmpresaId(empresa);
                if (resEmpresas.Exito)
                {
                    if (resEmpresas.Data.Count() > 0)
                    {
                        List<Parametros> p = new List<Parametros>() {
                            new Parametros(){
                                Parametro="@nomina",
                                Tipo=DbType.String,
                                Valor=nomina
                            },
                            new Parametros(){
                                Parametro="@reporte",
                                Tipo=DbType.Int32,
                                Valor=reporte.ToString()
                            }
                        };
                        ResData<ReporteNomina> resNomina =(ResData<ReporteNomina>) await sql.ConsultaBd<ReporteNomina>("ConsultaNominaTipoReporte", p, resEmpresas.Data.FirstOrDefault().bd);
                        if (resNomina.Exito)
                        {
                            if(resNomina.Data.Count()>0)
                            {
                                byte[] datos = Utilidades.GeneraExcelNomina(resNomina.Data.ToList(), descripcionReporte,descripcionNomina).ToArray();
                                archivo = new FileContentResult(datos, "application/vnd.ms-excel");
                            }
                            
                        }
                    }

                }

            }
            catch (Exception e)
            {
                
            }
            return archivo;
        }

        [HttpGet]
        [Route("ReportePagoBanco")]
        public async Task<FileResult> ReportePagoBanco(int empresa, string nomina, int banco)
        {
            
            FileContentResult archivo = null;
            try
            {
                var resEmpresas = await sql.ConsultaEmpresaId(empresa);
                if (resEmpresas.Exito)
                {
                    if (resEmpresas.Data.Count() > 0)
                    {
                        List<Parametros> p = new List<Parametros>() {
                            new Parametros(){
                                Parametro="@nomina",
                                Tipo=DbType.String,
                                Valor=nomina
                            },
                            new Parametros(){
                                Parametro="@banco",
                                Tipo=DbType.Int32,
                                Valor=banco.ToString()
                            }
                        };
                        ResData<DetallePagosNomina> resNomina = (ResData<DetallePagosNomina>)await sql.ConsultaBd<DetallePagosNomina>("ConsultaDetallePagosNomina", p, resEmpresas.Data.FirstOrDefault().bd);
                        if (resNomina.Exito)
                        {
                            if (resNomina.Data.Count() > 0)
                            {
                                byte[] datos = Utilidades.GeneraArchivoBanco(resNomina.Data.ToList());
                                archivo = new FileContentResult(datos, "application/octet-stream");
                            }

                        }
                    }

                }

            }
            catch (Exception e)
            {

            }
            return archivo;
        }



        [HttpPost]
        [Route("ConsultaBd")]
        public async Task<object> ConsultaBd(RData d)
        {
            object res = null;
            var resEmpresas = await sql.ConsultaEmpresaId(d.Empresa);
            if (resEmpresas.Exito)
            {
                if (resEmpresas.Data.Count() > 0)
                {
                    List<Parametros> p = Utilidades.ToParametros(d.DataParametros);

                    switch (d.Modelo)
                    {
                        case "ComprobantePago":
                            res = await sql.ConsultaBd<ComprobantePago>(d.Sp, p, resEmpresas.Data.FirstOrDefault().bd);
                            break;
                        case "Listado":
                            res = await sql.ConsultaBd<Listado>(d.Sp, p, resEmpresas.Data.FirstOrDefault().bd);
                            break;
                        case "ListadoNomina":
                            res = await sql.ConsultaBd<ListadoNomina>(d.Sp, p, resEmpresas.Data.FirstOrDefault().bd);
                            break;
                        case "GeneraNominaPrevia":
                            res = await sql.ConsultaBd<GeneraNominaPrevia>(d.Sp, p, resEmpresas.Data.FirstOrDefault().bd);
                            break;
                        case "Empleado":
                            res = await sql.ConsultaBd<Empleado>(d.Sp, p, resEmpresas.Data.FirstOrDefault().bd);
                            break;
                        case "dynamic":
                            res = await sql.ConsultaBd<dynamic>(d.Sp, p, resEmpresas.Data.FirstOrDefault().bd);
                            break;
                    }
                }

            }
            //List<Parametros> p = Utilidades.ToParametros(d.DataParametros);
            //object res = null;
            //switch (d.Modelo)
            //{
            //    case "ComprobantePago":
            //        res = await sql.ConsultaBd<ComprobantePago>(d.Sp, p);
            //        break;
            //    case "Listado":
            //        res = await sql.ConsultaBd<Listado>(d.Sp, p);
            //        break;
            //    case "ListadoNomina":
            //        res = await sql.ConsultaBd<ListadoNomina>(d.Sp, p);
            //        break;
            //    case "GeneraNominaPrevia":
            //        res = await sql.ConsultaBd<GeneraNominaPrevia>(d.Sp, p);
            //        break;
            //    case "Empleado":
            //        res = await sql.ConsultaBd<Empleado>(d.Sp, p);
            //        break;
            //    case "dynamic":
            //        res = await sql.ConsultaBd<dynamic>(d.Sp, p);
            //        break;
            //}
            return res;
        }


        [HttpPost]
        [Route("ConsultaBdMaster")]
        public async Task<object> ConsultaBdMaster(RData d)
        {
            object res = null;
            List<Parametros> p = Utilidades.ToParametros(d.DataParametros);

            switch (d.Modelo)
            {
                case "dynamic":
                    res = await sql.ConsultaBdMaster<dynamic>(d.Sp, p);
                    break;
            }
            return res;
        }

        [HttpGet]
        [Route("ListadoEmpresas")]
        public async Task<object> ListadoEmpresas()
        {
            var res = await sql.ListadoEmpresas();
            return res;
        }

        [HttpPost]
        [Route("RegistroBd")]
        public async Task<object> RegistroBd(RData d)
        {
            List<Parametros> p = Utilidades.ToParametros(d.DataParametros);
            object res = null;
            var resEmpresas = await sql.ConsultaEmpresaId(d.Empresa);
            if (resEmpresas.Exito)
            {
                if (resEmpresas.Data.Count() > 0)
                {
                    switch (d.Modelo)
                    {
                        case "RDataRegistro":
                            res = await sql.RegistroBd<RDataRegistro>(d.Sp, p, resEmpresas.Data.FirstOrDefault().bd);
                            break;
                    }
                }
            }

            return res;
        }

        [HttpPost]
        [Route("RegistroBdMaster")]
        public async Task<object> RegistroBdMaster(RData d)
        {
            List<Parametros> p = Utilidades.ToParametros(d.DataParametros);
            object res = null;
            switch (d.Modelo)
            {
                case "dynamic":
                    res = await sql.RegistroBdMaster<dynamic>(d.Sp, p);
                    break;
            }

            return res;
        }

        [HttpGet("ConsultaVersion")]
        public string ConsultaVersion()
        {
            return "1.0";
        }
    }
}
