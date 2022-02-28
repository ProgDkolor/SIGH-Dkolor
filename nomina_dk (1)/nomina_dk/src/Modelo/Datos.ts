

import { writable,readable } from 'svelte/store';
import type { Rutas } from './Rutas';
import type * as Tipos from './Tipos'
export const listadoMeses =readable([
    { id: 1, name: 'Enero' },
     { id: 2, name: 'Febrero'}, 
     { id: 3, name: 'Marzo'}, 
     { id: 4, name: 'Abril'}, 
     { id: 5, name: 'Mayo'}, 
     { id: 6, name: 'Junio'},
     { id: 7, name: 'Julio'}, 
     { id: 8, name: 'Agosto'}, 
     { id: 9, name: 'Septiembre'}, 
     { id: 10, name: 'Octubre'}, 
     { id: 11, name: 'Noviembre'}, 
     { id: 12, name: 'Diciembre'}, 
    ]);
    export const tiposNomina =readable([
        { id: 1, name: 'General' },
        ]);
    export const listadoQuincena =readable([
        1,2
        ]);    

export const nominaPrevia=writable<Tipos.GeneraNominaPrevia[]>([]);
export const dataNovedad=writable<Tipos.ConsultaNovedad[]>([]);
export const frecuenciaConceptos=writable<Tipos.Listado[]>([]);
export const listadoEmpresas=writable<Tipos.Empresas[]>([]);
export const empresaSeleccionada=writable<string>('');
export const RespuestaUsuarios=writable<Tipos.Usuarios[]>([]);
export const verModalNominaPrevia=writable<boolean>(false);
export const verModalBolantePago=writable<boolean>(false);
export const verModalReporteNovedad=writable<boolean>(false);
export const tituloModalReporteNovedad=writable<string>('');
export const verModalCambioClave=writable<boolean>(false);
export const verModalLoading=writable<boolean>(false);
export const verModalConfirmacion=writable<boolean>(false);
export const mensajeModalConfirmacion=writable<string>('');
export const tituloModalConfirmacion=writable<string>('');
export const accionModalConfirmacion=writable<any>(null);
export const accionModalMensaje=writable<any>(null);
export const LoginActivo=writable<boolean>(false);
export const verModalMensaje=writable<boolean>(false);
export const mensajeModal=writable<string>('');
export const tituloModal=writable<string>('');
export const EEmpresa=writable<boolean>(false);
export const tituloModalComprobantePago=writable<string>('');
export const nombreEmpresa=writable<string>('');
export const cedulaUsuario=writable<string>('');
export const rolUsuarioId = writable<number>(); //este es el rol del usuario.
export const rutas = writable<any>();
export const modulosDisponibles = writable<number[]>();


export let eNomina=writable<Tipos.ERegistroNomina>({
    anio:false,
    mes:false,
    tipo:false,
    quincena:false,
    nota:false
});
export let fNomina=writable<Tipos.DRegistroNomina>({
  anio:'',
  mes:'',
  tipo:'',
  quincena:'',
  nota:''
});

  export let usuarioActual=writable<Tipos.UsuarioActivo>({
    usuario: "",
    nombre: ""
  });
  /* ------------------------------------ Trabajando Token ------------------------------------ */
  export let token = writable<Tipos.Token>({
    TokenJSON: "",
    FechaDeExpiracion: null
  });

  export let tokenEmpleado = writable<Tipos.TokenEmpleado>({
    Id: "",
    Usuario: "",
    Correo: "",
    CargoEmpleadoId: 0,
    RolUsuarioId: 0
  });
  /* ------------------------------------ Trabajando Token ------------------------------------ */

  
  /* ------------------------------------ Trabajando Menu ------------------------------------ */
  
  export const accionesAplicables = writable<any>();
  /* ------------------------------------ Trabajando Menu ------------------------------------ */



  /* ------------------------------------ Trabajando Usuarios ------------------------------------ */
  export const listaDeUsuarios = writable<any>();
  export const usuarios = writable<any>();
  /* ------------------------------------ Trabajando Usuarios ------------------------------------ */

  export let fDescuentoEmpleado=writable<Tipos.DRegistroBeneficioEmpleado>({
    empleado:'',
    beneficio:'',
    frecuencia:'',
    monto:'',
    nota:''
  });
  export let eDescuentoEmpleado=writable<Tipos.ERegistroBeneficioEmpleado>({
    empleado:false,
    beneficio:false,
    frecuencia:false,
    monto:false,
    nota:false
  });
export let bolantePago=writable<Tipos.ComprobantePago[]>([]);

