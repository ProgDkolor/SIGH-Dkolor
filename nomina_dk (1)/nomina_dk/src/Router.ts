
import Inicio from './Componentes/Inicio.svelte';
import Login from './Paginas/Login.svelte';

//PAGINAS EMPLEADOS
import Empleados from './Paginas/Empleados.svelte';
import CrearEmpleado from './Paginas//Empleados/CrearEmpleado.svelte';
import ConsultarEmpleado from './Paginas//Empleados/ConsultarEmpleado.svelte';
import AsignarBeneficio from './Paginas//Empleados/AsignarBeneficio.svelte';
import AsignarDescuento from './Paginas//Empleados/AsignarDescuento.svelte';
import ComprobantePago from './Paginas//Empleados/ComprobantePago.svelte';
import ComprobantePagoNominaPrevia from './Paginas//Empleados/ComprobantePagoNominaPrevia.svelte';
import ActualizarEmpleado from './Paginas/Empleados/ActualizarEmpleado.svelte';
import ListarEmpleados from './Paginas/Empleados/ListarEmpleados.svelte';
import NuevoUsuario from './Paginas/Empleados/NuevoUsuario.svelte';
import ListarUsuarios from './Paginas/Empleados/ListarUsuarios.svelte';
import GestionarAccionesDePersonal from './Paginas/Empleados/GestionarAccionesDePersonal.svelte';
import GestionarCargaDeDocumentos from './Paginas/Empleados/GestionarCargaDeDocumentos.svelte';


//PAGINAS DE NOMINA
import Nomina from './Paginas/Nomina.svelte';
import CrearNomina from './Paginas/Nomina/CrearNomina.svelte';
import ConsultarNomina from './Paginas/Nomina/ConsultarNomina.svelte';
import GenerarNominaPrevia from './Paginas/Nomina/GenerarNominaPrevia.svelte';
import GenerarNominaDefinitiva from './Paginas/Nomina/GenerarNominaDefinitiva.svelte';
import ProcesarParaAprobacion from './Paginas/Nomina/ProcesarParaAprobacion.svelte';
import ProcesoPagoNomina from './Paginas/Nomina/ProcesoPagoNomina.svelte';
import ReportesNomina from './Paginas/Nomina/ReportesNomina.svelte';
import Novedades from './Paginas/Nomina/Novedades.svelte';


//PAGINAS DE NOMINA NOVEDADES
import PagoHorasExtras from './Paginas/Nomina/Novedades/PagoHorasExtras.svelte';
import PagoVacaciones from './Paginas/Nomina/Novedades/PagoVacaciones.svelte';
import DescuentoDiaTrabajo from './Paginas/Nomina/Novedades/DescuentoDiaTrabajo.svelte';

//PAGINAS CONFIGURACION
import Configuracion from './Paginas/Configuracion.svelte';


const routes = {
  '/': Login,
  //Configuracion
  '/Configuracion': Configuracion,
  //Empleados
  '/empleados/CrearEmpleado': CrearEmpleado,
  '/empleados/ConsultarEmpleado': ConsultarEmpleado,
  '/empleados/AsignarBeneficio': AsignarBeneficio,
  '/empleados/AsignarDescuento': AsignarDescuento,
  '/empleados/ActualizarEmpleado': ActualizarEmpleado,
  '/empleados/ComprobantePago': ComprobantePago,
  '/empleados/ComprobantePagoNominaPrevia': ComprobantePagoNominaPrevia,
  '/empleados/ListarEmpleados': ListarEmpleados,
  '/empleados/NuevoUsuario': NuevoUsuario, // Guardar esta base de datos al finalizar el modulo.
  '/empleados/GestionarUsuario': ListarUsuarios, // Guardar esta base de datos al finalizar el modulo.
  '/empleados/GestionarAccionesDePersonal': GestionarAccionesDePersonal, // Guardar esta base de datos al finalizar el modulo.
  '/empleados/GestionarCargaDeDocumentos': GestionarCargaDeDocumentos, // Guardar esta base de datos al finalizar el modulo.


  //Nomina
  '/nomina': Nomina,
  '/nomina/Crear': CrearNomina,
  '/nomina/Consultar': ConsultarNomina,
  '/nomina/Novedades': Novedades,
  '/nomina/ProcesarParaAprobacion': ProcesarParaAprobacion,
  '/nomina/GenerarNominaPrevia': GenerarNominaPrevia,
  '/nomina/GenerarNominaDefinitiva': GenerarNominaDefinitiva,
  '/nomina/ProcesoPagoNomina': ProcesoPagoNomina,
  '/nomina/ReportesNomina': ReportesNomina,
  '/nomina/Novedades/pagoHorasExtras': PagoHorasExtras,
  '/nomina/Novedades/pagoVacaciones': PagoVacaciones,
  '/nomina/Novedades/descuentoDiaTrabajo': DescuentoDiaTrabajo,
  '/empleados': Empleados,
  '*': Inicio,
}
  
  export { routes }
