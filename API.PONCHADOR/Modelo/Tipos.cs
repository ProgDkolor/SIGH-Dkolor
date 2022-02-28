using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API.PONCHADOR.Modelo
{
    //public class DetallePagosNomina
    //{
    //    public string Empleado { get; set; }
    //    public int MedioPago { get; set; }
    //    public int Banco { get; set; }
    //    public string CuentaBancaria { get; set; }
    //    public decimal Valor { get; set; }
    //    public DateTime Fecha { get; set; }
    //    public string Usuario { get; set; }
    //    public int TipoPago { get; set; }
    //    public int IdCuentaEmpresa { get; set; }
    //    public string DescripcionTipoPago { get; set; }
    //    public string CuentaEmpresa { get; set; }
    //}
    //public class ReporteNomina
    //{
    //    public string Modulo { get; set; }
    //    public string Dependencia { get; set; }
    //    public string Nombre { get; set; }
    //    public string Cedula { get; set; }
    //    public DateTime FechaIngreso { get; set; }
    //    public decimal Sueldo { get; set; }
    //    public decimal SueldoQuincenal { get; set; }
    //    public decimal PagoEfectivo { get; set; }
    //    public decimal Transferencia { get; set; }
    //    public decimal Cheque { get; set; }
    //    public decimal Descuento { get; set; }
    //    public decimal ISR { get; set; }
    //    public decimal AFP { get; set; }
    //    public decimal TSS_SFS { get; set; }
    //    public decimal SueldoNeto { get; set; }
    //    public string CuentaBanco { get; set; }
    //    public int IdDepartamento { get; set; }
    //    public int IdModulo { get; set; }
    //}
    //public class ReporteComisionCriterio
    //{
    //    public string Id { get; set; }
    //    public string Cedula { get; set; }
    //    public decimal MontoTotal { get; set; }
    //    public decimal Comision { get; set; }
    //    public int Modulo { get; set; }
    //    public int Estacion { get; set; }
    //    public decimal Recibido { get; set; }
    //    public decimal Devuelta { get; set; }
    //    public string Usuario { get; set; }
    //    public DateTime Fecha { get; set; }
    //    public string Recibo { get; set; }
    //    public bool Estado { get; set; }
    //    public int Nivel { get; set; }
    //    public string Codigo { get; set; }
    //    public string DescripcionServicio { get; set; }
    //    public string Nombre { get; set; }
    //    public string NombreUsuario { get; set; }
    //    public int CodigoUsuario { get; set; }
    //    public string TipoDocumento { get; set; }
    //    public string DescripcionModulo { get; set; }
    //    public string Referencia { get; set; }
    //}
    //public class Conceptos
    //{
    //    public int id { get; set; }
    //    public string descripcion { get; set; }
    //    public int tipo { get; set; }
    //    public int categoria { get; set; }
    //    public int calculo { get; set; }
    //    public decimal valor { get; set; }
    //    public bool estado { get; set; }
    //}
    //public class Empleado
    //{
    //    public Guid id { get; set; }
    //    public int codigo { get; set; }
    //    public string cedula { get; set; }
    //    public string nombre { get; set; }
    //    public string apellido { get; set; }
    //    public int cargo { get; set; }
    //    public DateTime fechaEntrada { get; set; }
    //    public DateTime fechaRegistro { get; set; }
    //    public decimal salario { get; set; }
    //    public int contrato { get; set; }
    //    public bool estado { get; set; }
    //    public int situacion { get; set; }
    //    public int? medioPago { get; set; }
    //    public string cuentaBancaria { get; set; }
    //    public int? banco { get; set; }
    //    public int? departamento { get; set; }
    //    public string telefono { get; set; }
    //    public string correo { get; set; }
    //    public string direccion { get; set; }
    //    public string nota { get; set; }
    //    public int? modulo { get; set; }
    //}
    //public class GeneraNominaPrevia
    //{
    //    public string Periodo { get; set; }
    //    public DateTime? Fecha { get; set; }
    //    public string Modulo { get; set; }
    //    public string Dependencia { get; set; }
    //    public string Cedula { get; set; }
    //    public string Nombre { get; set; }
    //    public string Apellidos { get; set; }
    //    public string CuentaBanco { get; set; }
    //    public DateTime? FechaIngreso { get; set; }
    //    public decimal? Sueldo { get; set; }
    //    public decimal? SueldoQuincenal { get; set; }
    //    public decimal? PagoEfectivo { get; set; }
    //    public decimal? transferencia { get; set; }
    //    public decimal? cheque { get; set; }
    //    public decimal? Dietas { get; set; }
    //    public decimal? HorasExtras { get; set; }
    //    public decimal? ISR { get; set; }
    //    public decimal? AFP { get; set; }
    //    public decimal? TSS_SFS { get; set; }
    //    public decimal? sueldoNeto { get; set; }
    //    public string descripcion { get; set; }
    //    public string cargoEmpleado { get; set; }
    //}
    //public class ListadoNomina
    //{
    //    public Guid id { get; set; }
    //    public string descripcion { get; set; }
    //}
    //public class ComprobantePago
    //{
    //    public string id { get; set; }
    //    public string descripcionNomina { get; set; }
    //    public int tipo { get; set; }
    //    public string cedula { get; set; }
    //    public string nombre { get; set; }
    //    public string apellido { get; set; }
    //    public DateTime fechaEntrada { get; set; }
    //    public int cargo { get; set; }
    //    public int concepto { get; set; }
    //    public decimal cantidad { get; set; }
    //    public decimal salarioMensual { get; set; }
    //    public string tipoNomina { get; set; }
    //    public string descripcionCargo { get; set; }
    //    public int? medioPago { get; set; }
    //    public string descripcionMedioPago { get; set; }
    //    public string cuentaBancaria { get; set; }
    //    public string descripcionPago { get; set; }
    //    public decimal ingreso { get; set; }
    //    public decimal descuento { get; set; }
    //    public string descripcionConcepto { get; set; }
    //}
    //public class Empresas
    //{
    //    public int id { get; set; }
    //    public string urlApi { get; set; }
    //    public string nombre { get; set; }
    //    public string bd { get; set; }
    //}

    //public class RData
    //{
    //    public int Empresa { get; set; } 
    //    public string Sp { get; set; } = string.Empty;
    //    public string Modelo { get; set; } = string.Empty;
    //    public List<ParametrosJson> DataParametros { get; set; }
    //}
    //public class RDataRegistro
    //{
    //    public int Resultado { get; set; } 
    //    public string Mensaje { get; set; } 
    //    public string Id { get; set; }
    //}
    public class ResData<T>
    {
        public bool Exito { get; set; } = false;
        public string Mensaje { get; set; } = string.Empty;
        public IEnumerable<T> Data { get; set; }
    }

    //public class ResArchivo
    //{
    //    public bool Exito { get; set; } = false;
    //    public string Mensaje { get; set; } = string.Empty;
    //    public FileResult Data { get; set; }
    //}

    //public class ParametrosDescarga {
    //    public int Empresa { get; set; }
    //    public string IdNomina { get; set; }
    //    public int IdReporte { get; set; }
    //    public string Nomina { get; set; }
    //    public string Reporte { get; set; }
    //}

    //public class Parametros {
    //    public string Parametro { get; set; }
    //    public DbType Tipo { get; set; }
    //    public string Valor { get; set; }
    //}
    //public class ParametrosJson
    //{
    //    public string Parametro { get; set; }
    //    public int Tipo { get; set; }
    //    public string Valor { get; set; }
    //}
    //public class Listado {
    //    public int Id { get; set; }
    //    public string Descripcion { get; set; }
    //    public bool Estado { get; set; }
    //}
    //public class ResListado
    //{
    //    public bool Exito { get; set; } = false;
    //    public string Mensaje { get; set; } = string.Empty;
    //    public IEnumerable<Listado> Data { get; set; }
    //}
    //public class ResPonchadas
    //{
    //    public bool Exito { get; set; } = false;
    //    public string Mensaje { get; set; } = string.Empty;
    //    public IEnumerable<Ponchadas> Data { get; set; }
    //}
    //public class ResEmpresas
    //{
    //    public bool Exito { get; set; } = false;
    //    public string Mensaje { get; set; } = string.Empty;
    //    public IEnumerable<Empresas> Data { get; set; }
    //}
    //public class Ponchadas
    //{
    //    public int Id { get; set; }
    //    public string Nombre { get; set; }
    //    public string Apellido { get; set; }
    //    public string Cargo { get; set; }
    //    public decimal Salario { get; set; }
    //    public DateTime Fecha { get; set; }
    //    public string Entrada { get; set; }
    //    public string InicioAlmuerzo { get; set; }
    //    public string FinAlmuerzo { get; set; }
    //    public string Salida { get; set; }
    //    public string Calculo1 { get; set; }
    //    public string Calculo2 { get; set; }
    //    public string Total { get; set; }
    //    public int Horas { get; set; }
    //    public int Minutos { get; set; }
    //    public int Extras { get; set; }
    //    public decimal SalarioDiario { get; set; }
    //    public decimal SalarioHora { get; set; }
    //    public decimal MontoExtra { get; set; }
    //}
}
