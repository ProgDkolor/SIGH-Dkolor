<script lang="ts">
  import {bolantePago,
          verModalBolantePago,
          verModalLoading,
          tituloModalComprobantePago,
          empresaSeleccionada,
          accionesAplicables
        } from '../../Modelo/Datos'
        import LabelHorizontal from '../../Componentes/LabelHorizontal.svelte'
  import { imask,MaskedInput } from 'svelte-imask';
  import { onMount } from 'svelte';
  import TituloPagina from "../../Componentes/TituloPagina.svelte";
  import * as Servicios from '../../Servicios/AccesoApi'
  import type * as Tipos from '../../Modelo/Tipos'
  import * as Util from '../../Servicios/Utilidades'
  import {fade} from 'svelte/transition'
  import * as Enum from '../../Enums/Estados';
            //TIPOS FUNCIONES
  

    let listadoBancos:Tipos.Listado[]=[];
    let listadoCargos:Tipos.Listado[]=[];
    let listadoDepartamentos:Tipos.Listado[]=[];
    let listadoModulos:Tipos.Listado[]=[];
    let listadoMediosPago:Tipos.Listado[]=[];

    const formateoFecha=(fecha:string)=>{
    let f:string[]=fecha.split('-');
    return `${f[2].substring(0,2)}-${f[1]}-${f[0]}`;
  }
  const formateoFechaInput=(fecha:string)=>{
    let f:string[]=fecha.split('-');
    return `${f[0]}-${f[1]}-${f[2].substring(0,2)}`;
  }
    
  //VARIABLES
  const mascaraCedula = {
        mask: '000-0000000-0'
  };
  let fEmpleado:Tipos.FEmpleado={
      cedula: "",
      nombre: "",
      apellido: "",
      cargo: "",
      fechaEntrada: "",
      fechaRegistro: "",
      salario: "",
      salarioEspecial: "",
      medioPago: "",
      cuentaBancaria: "",
      banco: "",
      departamento: "",
      telefono: "",
      correo: "",
      direccion: "",
      nota: "",
      modulo: "",
  }
  let eEmpleado:Tipos.EEmpleado={
      cedula: false,
      nombre: false,
      apellido: false,
      cargo: false,
      fechaEntrada: false,
      salario: false,
      salarioEspecial: false,
      medioPago: false,
      banco: false,
      modulo: false,
      departamento:false
  }
  let cedula:string='';
  let eCedula:boolean=false;
  let nominaElegida:string='';
  let eNominaElegida:boolean=false;
  let consultaNominas:Tipos.ConsultaNominaPeriodo[]=[];
  let empleados:Tipos.Empleado[]=[];
  let editFormulario:boolean=true;

  //METODOS

  onMount(
      ()=>{
          cargaBancos();
          cargaCargosEmpleados();
          cargaModulos();
          cargaDepartamentos();
          cargaMediosPago();
      }
  );
  const limpiar=()=>{
      fEmpleado={
      cedula: "",
      nombre: "",
      apellido: "",
      cargo: "",
      fechaEntrada: "",
      fechaRegistro: "",
      salario: "",
      salarioEspecial: "",
      medioPago: "",
      cuentaBancaria: "",
      banco: "",
      departamento: "",
      telefono: "",
      correo: "",
      direccion: "",
      nota: "",
      modulo: "",
  }
  cedula='';
  editFormulario=true;
  empleados=[];

  }
  const soloNumerosSalario=(e)=> {
  let actual = fEmpleado.salario;
  let nuevo = e.target.value;

  if (Util.VerificaNumero(nuevo) && nuevo.length < 17) {
    fEmpleado.salario = nuevo;
  } else {
      e.target.value = actual;
  }
}

const soloNumerosSalarioEspecial=(e)=> {
  let actual = fEmpleado.salarioEspecial;
  let nuevo = e.target.value;

  if (Util.VerificaNumero(nuevo) && nuevo.length < 17) {
    fEmpleado.salarioEspecial = nuevo;
  } else {
      e.target.value = actual;
  }
}

const activarEdicion=()=>{
    editFormulario=false;
}
const cancelarEdicion=()=>{
    editFormulario=true;
    cargaEmpleado();
}
const consultaEmpleado=async()=>{
      validaCedula();
      try{
        if(!eCedula){
              let datos:Tipos.RData={
                empresa: parseInt($empresaSeleccionada),
                sp: "ConsultaEmpleadoCedula",
                modelo: "dynamic",
                dataParametros: [
                  {
                    parametro: "@cedula",
                    tipo: 16,
                    valor: cedula.replaceAll('-','')
                  }
                ]
              }
              $verModalLoading=true;
              let res=await Servicios.ConsultaBd(datos);
              $verModalLoading=false;
        if(res.exito)
        {
          empleados=res.data;
          if(empleados.length==0){
            Util.VerMensaje("Atencion","No se encontro ningun empleado con la cedula ingresada");
          }else{
              cargaEmpleado();
          }
           console.log("Exito");
        }else
        {
            console.log("error");

        }
        console.log(res);	

        }
        }
        catch(error)
        {
          Util.VerMensaje("Error Encontrado","Error en la comunicacion con el web service."); 
        }
    }

  const actualizarEmpleado=async ()=>{
      validaCedula();
      validaNombre();
      validaApellido();
      validaCargo();
      validaDepartamento();
      validaFechaEntrada();
      validaModulo();
      validaMedioPago();
      validaSalario();
      validaSalarioEspecial();
      validaBanco();
   
    try{
      if(!eEmpleado.cedula && !eEmpleado.nombre && !eEmpleado.apellido && !eEmpleado.cargo && !eEmpleado.departamento &&
      !eEmpleado.fechaEntrada && !eEmpleado.modulo && !eEmpleado.medioPago && !eEmpleado.salario && !eEmpleado.salarioEspecial && !eEmpleado.banco)
  {
    let datos:Tipos.RData={
          empresa:parseInt($empresaSeleccionada),
          sp: "ActualizarEmpleado",
          modelo: "RDataRegistro",
          dataParametros: [
            {
              parametro: "@cedula",
              tipo: 16,
              valor: empleados[0].cedula
            },
            {
              parametro: "@nombre",
              tipo: 16,
              valor: fEmpleado.nombre.toString()
            },
            {
              parametro: "@apellido",
              tipo: 16,
              valor: fEmpleado.apellido.toString()
            },
            {
              parametro: "@cargo",
              tipo: 16,
              valor: fEmpleado.cargo.toString()
            },
            {
              parametro: "@fechaEntrada",
              tipo: 16,
              valor: fEmpleado.fechaEntrada.toString()
            },
            {
              parametro: "@salario",
              tipo: 16,
              valor: fEmpleado.salario.toString()
            },
            {
              parametro: "@salarioEspecial",
              tipo: 16,
              valor: fEmpleado.salarioEspecial.toString()
            },
            {
              parametro: "@medioPago",
              tipo: 16,
              valor: fEmpleado.medioPago.toString()
            },
            {
              parametro: "@cuentaBancaria",
              tipo: 16,
              valor: fEmpleado.cuentaBancaria.toString()
            },
            {
              parametro: "@banco",
              tipo: 16,
              valor: fEmpleado.banco.toString()
            },
            {
              parametro: "@departamento",
              tipo: 16,
              valor: fEmpleado.departamento.toString()
            },
            {
              parametro: "@telefono",
              tipo: 16,
              valor: fEmpleado.telefono.toString()
            },
            {
              parametro: "@correo",
              tipo: 16,
              valor: fEmpleado.correo.toString()
            },
            {
              parametro: "@direccion",
              tipo: 16,
              valor: fEmpleado.direccion.toString()
            },
            {
              parametro: "@nota",
              tipo: 16,
              valor: fEmpleado.nota.toString()
            },
            {
              parametro: "@modulo",
              tipo: 16,
              valor: fEmpleado.modulo.toString()
            },
          ]
        } 
        $verModalLoading=true;
        let res=await Servicios.RegistroBd(datos);
        $verModalLoading=false;
        if(res.exito)
        {
          let resEmpleado=res.data;
            if(resEmpleado.length>0)
            {
              if(resEmpleado[0].resultado==0)
              {
                limpiar();
                Util.VerMensaje("Atencion","Empleado registado con exito.");
              }else{
                Util.VerMensaje("Error al registrar",resEmpleado[0].mensaje);
              }
            }
           console.log("Exito");
           console.log(resEmpleado);
        }else
        {
          Util.VerMensaje("Atencion",res.mensaje);

        }
        console.log(res);	
  }
    }
    catch(error)
    {
      Util.VerMensaje("Error Encontrado","Error en la comunicacion con el web service.");
    }
     
  }
   
    
    const validaCedula=()=>{
      if(fEmpleado.cedula.length<11)
          eEmpleado.cedula=true;
      else
          eEmpleado.cedula=false; 
    }
    const validaNombre=()=>{
      if(fEmpleado.nombre.length<1)
          eEmpleado.nombre=true;
      else
          eEmpleado.nombre=false; 
    }
    const validaApellido=()=>{
      if(fEmpleado.apellido.length<1)
          eEmpleado.apellido=true;
      else
          eEmpleado.apellido=false; 
    }
    const validaCargo=()=>{
      if(fEmpleado.cargo.length<1)
          eEmpleado.cargo=true;
      else
          eEmpleado.cargo=false; 
    }
    const validaFechaEntrada=()=>{
      if(fEmpleado.fechaEntrada.length<1)
          eEmpleado.fechaEntrada=true;
      else
          eEmpleado.fechaEntrada=false; 
    }
    const validaSalario=()=>{
      if(fEmpleado.salario.toString().length<1)
          eEmpleado.salario=true;
      else
          eEmpleado.salario=false; 
    }

    const validaSalarioEspecial=()=>{
      if(fEmpleado.salarioEspecial.toString().length<1)
          eEmpleado.salarioEspecial=true;
      else
          eEmpleado.salarioEspecial=false; 
    }
    const validaMedioPago=()=>{
      if(fEmpleado.medioPago.toString().length<1)
          eEmpleado.medioPago=true;
      else
          eEmpleado.medioPago=false; 
    }
    const validaBanco=()=>{
      if(fEmpleado.banco.length<1)
          eEmpleado.banco=true;
      else
          eEmpleado.banco=false; 
    }
    const validaModulo=()=>{
      if(fEmpleado.modulo.length<1)
          eEmpleado.modulo=true;
      else
          eEmpleado.modulo=false; 
    }
    const validaDepartamento=()=>{
      if(fEmpleado.departamento.length<1)
          eEmpleado.departamento=true;
      else
          eEmpleado.departamento=false; 
    }
    const validaNomina=()=>{
     if(nominaElegida.length<1)
        {
            eNominaElegida =true;
            return;
        }
        else{
          eNominaElegida=false;
        }
    }
    const cargaEmpleado=()=>{
        if(empleados.length>0)
        {
          fEmpleado.cedula=empleados[0].cedula;
          fEmpleado.nombre=empleados[0].nombre;
          fEmpleado.apellido=empleados[0].apellido;
          fEmpleado.fechaEntrada=formateoFechaInput(empleados[0].fechaEntrada);
          fEmpleado.salario=empleados[0].salario.toString();
          fEmpleado.salarioEspecial=empleados[0].salarioEspecial.toString();
          fEmpleado.cargo=empleados[0].cargo;
          fEmpleado.modulo=empleados[0].modulo;
          fEmpleado.departamento=empleados[0].departamento;
          fEmpleado.telefono=empleados[0].telefono;
          fEmpleado.correo=empleados[0].correo;
          fEmpleado.direccion=empleados[0].direccion;
          fEmpleado.medioPago=empleados[0].medioPago;
          fEmpleado.banco=empleados[0].banco;
          fEmpleado.cuentaBancaria=empleados[0].cuentaBancaria;
          editFormulario=true;

        }
    }
  
    const cargaBancos=async ()=>{
    try{
      let datos:Tipos.RData={
          empresa: parseInt($empresaSeleccionada),
            sp: "ListadoBancos",
            modelo: "dynamic",
            dataParametros: []
          } 
          $verModalLoading=true;
          let res=await Servicios.ConsultaBd(datos);
          $verModalLoading=false;
          if(res.exito)
          {
              listadoBancos=res.data;
              if(listadoBancos.length==0)
              {
                  Util.VerMensaje("Atencion","No se encontraron bancos registrados.");
              }
             console.log("Exito");
             console.log(listadoBancos);
          }else
          {
              console.log("error");
  
          }
          console.log(res);	
        }
        catch(error)
        {
          Util.VerMensaje("Error Encontrado","Error en la comunicacion con el web service."); 
        }
    }

    const cargaDepartamentos=async ()=>{
    try{
      let datos:Tipos.RData={
          empresa: parseInt($empresaSeleccionada),
            sp: "ListadoDepartamentos",
            modelo: "dynamic",
            dataParametros: []
          } 
          $verModalLoading=true;
          let res=await Servicios.ConsultaBd(datos);
          $verModalLoading=false;
          if(res.exito)
          {
              listadoDepartamentos=res.data;
              if(listadoDepartamentos.length==0)
              {
                  Util.VerMensaje("Atencion","No se encontraron departamentos registrados.");
              }
             console.log("Exito");
             console.log(listadoDepartamentos);
          }else
          {
              console.log("error");
  
          }
          console.log(res);	
        }
        catch(error)
        {
          Util.VerMensaje("Error Encontrado","Error en la comunicacion con el web service."); 
        }
    }
    
    const cargaMediosPago=async ()=>{
    try{
      let datos:Tipos.RData={
          empresa: parseInt($empresaSeleccionada),
            sp: "ListadoMediosPago",
            modelo: "dynamic",
            dataParametros: []
          } 
          $verModalLoading=true;
          let res=await Servicios.ConsultaBd(datos);
          $verModalLoading=false;
          if(res.exito)
          {
              listadoMediosPago=res.data;
              if(listadoMediosPago.length==0)
              {
                  Util.VerMensaje("Atencion","No se encontraron medio de pago registrados.");
              }
             console.log("medio Pago");
             console.log(listadoMediosPago);
          }else
          {
              console.log("error");
  
          }
          console.log(res);	
        }
        catch(error)
        {
          Util.VerMensaje("Error Encontrado","Error en la comunicacion con el web service."); 
        }
    }

    const cargaCargosEmpleados=async ()=>{
    try{
      let datos:Tipos.RData={
          empresa: parseInt($empresaSeleccionada),
            sp: "ListadoCargoEmpleados",
            modelo: "dynamic",
            dataParametros: []
          } 
          $verModalLoading=true;
          let res=await Servicios.ConsultaBd(datos);
          $verModalLoading=false;
          if(res.exito)
          {
              listadoCargos=res.data;
              if(listadoCargos.length==0)
              {
                  Util.VerMensaje("Atencion","No se encontraron cargos de empleados registrados.");
              }
             console.log("Exito");
             console.log(listadoCargos);
          }else
          {
              console.log("error");
  
          }
          console.log(res);	
        }
        catch(error)
        {
          Util.VerMensaje("Error Encontrado","Error en la comunicacion con el web service."); 
        }
    }

    const cargaModulos=async ()=>{
    try{
      let datos:Tipos.RData={
          empresa: parseInt($empresaSeleccionada),
            sp: "ListadoModulos",
            modelo: "dynamic",
            dataParametros: []
          } 
          $verModalLoading=true;
          let res=await Servicios.ConsultaBd(datos);
          $verModalLoading=false;
          if(res.exito)
          {
              listadoModulos=res.data;
              if(listadoModulos.length==0)
              {
                  Util.VerMensaje("Atencion","No se encontraron modulos registrados.");
              }
             console.log("Exito");
             console.log(listadoModulos);
          }else
          {
              console.log("error");
  
          }
          console.log(res);	
        }
        catch(error)
        {
          Util.VerMensaje("Error Encontrado","Error en la comunicacion con el web service."); 
        }
    }

  const cargarNominasRegistradas=async ()=>{
    try{
      let datos:Tipos.RData={
          empresa: parseInt($empresaSeleccionada),
            sp: "ConsultaNominaCondicion",
            modelo: "ListadoNomina",
            dataParametros: [
              {
                parametro: "@condicion",
                tipo: 16,
                valor: "1"
              }
            ]
          } 
          $verModalLoading=true;
          let res=await Servicios.ConsultaBd(datos);
          $verModalLoading=false;
          if(res.exito)
          {
              consultaNominas=res.data;
              if(consultaNominas.length==0)
              {
                  Util.VerMensaje("Atencion","No se encontraron nominas registradas pendientes de procesar.");
              }
             console.log("Exito");
             console.log(consultaNominas);
          }else
          {
              console.log("error");
  
          }
          console.log(res);	
        }
        catch(error)
        {
          Util.VerMensaje("Error Encontrado","Error en la comunicacion con el web service."); 
        }
    };
    let tituloDeLaPagina:string = 'CREAR EMPLEADO';
    let permisosCrud: any = {
      "c": $accionesAplicables.c,
      "r": $accionesAplicables.r,
      "u": $accionesAplicables.u,
      "d": $accionesAplicables.d,
      "crud": $accionesAplicables.crud
    };

</script>

{#if permisosCrud.crud == 1}
   <!-- content here -->
   <main in:fade="{{duration: 500}}">

    <TituloPagina Titulo="CONSULTA EMPLEADO"></TituloPagina>
    
    
    <section class="w-full">
    <article class="rounded-b-2xl rounded-t-2xl p-3 mx-[5%]">
    
    
        <section class="pt-0">
    
          <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Cedula Empleado</LabelHorizontal>
                <MaskedInput 
                  bind:value={cedula} 
                  options={mascaraCedula}
                  type="text"
                  autocomplete="off"
                  class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tCedula" />
                <button on:click={()=>consultaEmpleado()} class="btn">Consultar</button>
                {#if empleados.length>0}
                    {#if editFormulario}
                        <button on:click={()=>activarEdicion()} class="btn bg-red-600 border-none">Activar Edicion</button>
                    {:else}
                        <button on:click={()=>cancelarEdicion()} class="btn bg-blue-700 border-none">Cancelar Edicion</button>
                    {/if}
    
                {/if}
                {#if eCedula}
                    <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                {/if}
              </div>
            </div>
    
            {#if empleados.length>0}
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Nombre</LabelHorizontal>
                <input 
                  bind:value={fEmpleado.nombre}
                  type="text"
                  on:keyup={validaNombre}
                  autocomplete="off"
                  readonly={editFormulario ? true: false}
                  class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tNombre" />
                  {#if eEmpleado.nombre}
                      <div class="text-xs absolute ml-3 top-0 
                      left-0 block text-red-400">*Campo Requerido*</div>
                  {/if}
              </div>
            </div>
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Apellido</LabelHorizontal>
                <input 
                  bind:value={fEmpleado.apellido}
                  type="text"
                  on:keyup={validaApellido}
                  autocomplete="off"
                  readonly={editFormulario ? true: false}
                  class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tApellido" />
                  {#if eEmpleado.apellido}
                      <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                  {/if}
              </div>
            </div>
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Fecha Entrada</LabelHorizontal>
                <input 
                  bind:value={fEmpleado.fechaEntrada}
                  type="date"
                  on:change={validaFechaEntrada}
                  autocomplete="off"
                  readonly={editFormulario ? true: false}
                  class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tFechaEntrada" />
                  {#if eEmpleado.fechaEntrada}
                      <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                  {/if}
              </div>
            </div>
    
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Salario</LabelHorizontal>
                <input 
                value={fEmpleado.salario}
                  type="text"
                  on:keyup={validaSalario}
                  autocomplete="off"
                  on:input|preventDefault={soloNumerosSalario}
                  readonly={editFormulario ? true: false}
                  class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tSalario" />
                  {#if eEmpleado.salario}
                      <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                  {/if}
              </div>
            </div>
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Salario Especial</LabelHorizontal>
                <input 
                value={fEmpleado.salarioEspecial}
                  type="text"
                  on:keyup={validaSalarioEspecial}
                  on:input|preventDefault={soloNumerosSalarioEspecial}
                  autocomplete="off"
                  readonly={editFormulario ? true: false}
                  class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tSalarioEspecial" />
                  {#if eEmpleado.salarioEspecial}
                      <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                  {/if}
              </div>
            </div>
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Cargo</LabelHorizontal>
                <select name="cCargo"  bind:value={fEmpleado.cargo} 
                 on:change={validaCargo} 
                 disabled={editFormulario ? true: false}
                 autocomplete="off"
                 class="select select-bordered font-normal 
                 text-lg text-gray-500 w-[400px]
                 disabled:select disabled:select-bordered 
                 disabled:text-gray-500 disabled:text-lg
                 disabled:font-normal">
                  <option disabled={true} selected={true}></option> 
                  {#each listadoCargos as l }
                    <option value={l.id}>{l.descripcion}</option> 
                  {/each}
                  </select>
                  {#if eEmpleado.cargo}
                      <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                  {/if}
                 
              </div>
            </div>
    
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Modulo</LabelHorizontal>
                <select name="cModulo"  bind:value={fEmpleado.modulo} 
                on:change={validaModulo}
                disabled={editFormulario ? true: false}  
                autocomplete="off"
                class="select select-bordered font-normal 
                text-lg text-gray-500 w-[400px]
                disabled:select disabled:select-bordered 
                 disabled:text-gray-500 disabled:text-lg
                 disabled:font-normal">
                  <option disabled={true} selected={true}></option> 
                  {#each listadoModulos as l }
                    <option value={l.id}>{l.descripcion}</option> 
                  {/each}
                  </select>
                  {#if eEmpleado.modulo}
                      <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                  {/if}
              </div>
            </div>
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Departamento</LabelHorizontal>
                <select name="cDepartamento"  bind:value={fEmpleado.departamento} 
                on:change={validaDepartamento} 
                disabled={editFormulario ? true: false}
                autocomplete="off"
                class="select select-bordered font-normal 
                text-lg text-gray-500 w-[400px]
                disabled:select disabled:select-bordered 
                 disabled:text-gray-500 disabled:text-lg
                 disabled:font-normal">
                  <option disabled={true} selected={true}></option> 
                  {#each listadoDepartamentos as l }
                    <option value={l.id}>{l.descripcion}</option> 
                  {/each}
                  </select>
                  {#if eEmpleado.departamento}
                      <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                  {/if}
              </div>
            </div>
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Telefono</LabelHorizontal>
                <input 
                bind:value={fEmpleado.telefono}
                readonly={editFormulario ? true: false}
                autocomplete="off"
                  type="text"
                  class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tTelefono" />
              </div>
            </div>
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Correo</LabelHorizontal>
                <input 
                bind:value={fEmpleado.correo} 
                readonly={editFormulario ? true: false}
                autocomplete="off"
                  type="text"
                  class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tCorreo" />
              </div>
            </div>
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Direccion</LabelHorizontal>
                <textarea
                  rows="2" 
                  bind:value={fEmpleado.direccion}
                  readonly={editFormulario ? true: false}
                  autocomplete="off"
                  type="text"
                  class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tDireccion" />
              </div>
            </div>
              
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Medio Pago</LabelHorizontal>
                <select name="cMedioPago"  bind:value={fEmpleado.medioPago} 
                on:change={validaMedioPago} 
                disabled={editFormulario ? true: false}
                autocomplete="off"
                class="select select-bordered 
                font-normal text-lg text-gray-500 w-[400px]
                 disabled:select disabled:select-bordered 
                 disabled:text-gray-500 disabled:text-lg
                 disabled:font-normal">
                  <option disabled={true} selected={true}></option> 
                  {#each listadoMediosPago as l }
                    <option value={l.id}>{l.descripcion}</option> 
                  {/each}
                  </select>
                  {#if eEmpleado.medioPago}
                      <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                  {/if}
              </div>
            </div>
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Banco</LabelHorizontal>
                <select name="cBanco"  bind:value={fEmpleado.banco} 
                on:change={validaBanco} 
                disabled={editFormulario ? true: false}
                autocomplete="off"
                class="select select-bordered 
                font-normal text-lg text-gray-500 w-[400px]
                disabled:select disabled:select-bordered 
                 disabled:text-gray-500 disabled:text-lg
                 disabled:font-normal">
                  <option disabled={true} selected={true}></option> 
                  {#each listadoBancos as l }
                    <option value={l.id}>{l.descripcion}</option> 
                  {/each}
                  </select>
                  {#if eEmpleado.banco}
                      <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                  {/if}
              </div>
            </div>
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Cuenta Bancaria</LabelHorizontal>
                <input 
                bind:value={fEmpleado.cuentaBancaria}
                readonly={editFormulario ? true: false}
                autocomplete="off"
                  type="text"
                  class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tCuentaBancaria" />
              </div>
            </div>
            {/if}
        </section>
    
        <section class="flex justify-end">
          {#if !editFormulario}
              <button on:click={()=>actualizarEmpleado()} class="btn btn-md mt-1 mx-1 bg-green-700 font-bold border-none hover:bg-white hover:text-green-700">Actualizar Empleado</button>
          {/if}
      </section>
        </article>
        
    </section>
    
    </main>
{:else if permisosCrud.c == 1 || permisosCrud.r == 1}
   <!-- else if content here -->
   <main in:fade="{{duration: 500}}">

    <TituloPagina Titulo="CONSULTA EMPLEADO"></TituloPagina>
    
    
    <section class="w-full">
    <article class="rounded-b-2xl rounded-t-2xl p-3 mx-[5%]">
    
    
        <section class="pt-0">
    
          <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Cedula Empleado</LabelHorizontal>
                <MaskedInput 
                  bind:value={cedula} 
                  options={mascaraCedula}
                  type="text"
                  autocomplete="off"
                  class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tCedula" />
                <button on:click={()=>consultaEmpleado()} class="btn">Consultar</button>
                {#if empleados.length>0}
                    {#if editFormulario}
                        <button on:click={()=>activarEdicion()} class="btn bg-red-600 border-none">Activar Edicion</button>
                    {:else}
                        <button on:click={()=>cancelarEdicion()} class="btn bg-blue-700 border-none">Cancelar Edicion</button>
                    {/if}
    
                {/if}
                {#if eCedula}
                    <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                {/if}
              </div>
            </div>
    
            {#if empleados.length>0}
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Nombre</LabelHorizontal>
                <input 
                  bind:value={fEmpleado.nombre}
                  type="text"
                  on:keyup={validaNombre}
                  autocomplete="off"
                  readonly={editFormulario ? true: false}
                  class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tNombre" />
                  {#if eEmpleado.nombre}
                      <div class="text-xs absolute ml-3 top-0 
                      left-0 block text-red-400">*Campo Requerido*</div>
                  {/if}
              </div>
            </div>
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Apellido</LabelHorizontal>
                <input 
                  bind:value={fEmpleado.apellido}
                  type="text"
                  on:keyup={validaApellido}
                  autocomplete="off"
                  readonly={editFormulario ? true: false}
                  class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tApellido" />
                  {#if eEmpleado.apellido}
                      <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                  {/if}
              </div>
            </div>
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Fecha Entrada</LabelHorizontal>
                <input 
                  bind:value={fEmpleado.fechaEntrada}
                  type="date"
                  on:change={validaFechaEntrada}
                  autocomplete="off"
                  readonly={editFormulario ? true: false}
                  class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tFechaEntrada" />
                  {#if eEmpleado.fechaEntrada}
                      <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                  {/if}
              </div>
            </div>
    
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Salario</LabelHorizontal>
                <input 
                value={fEmpleado.salario}
                  type="text"
                  on:keyup={validaSalario}
                  autocomplete="off"
                  on:input|preventDefault={soloNumerosSalario}
                  readonly={editFormulario ? true: false}
                  class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tSalario" />
                  {#if eEmpleado.salario}
                      <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                  {/if}
              </div>
            </div>
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Salario Especial</LabelHorizontal>
                <input 
                value={fEmpleado.salarioEspecial}
                  type="text"
                  on:keyup={validaSalarioEspecial}
                  on:input|preventDefault={soloNumerosSalarioEspecial}
                  autocomplete="off"
                  readonly={editFormulario ? true: false}
                  class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tSalarioEspecial" />
                  {#if eEmpleado.salarioEspecial}
                      <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                  {/if}
              </div>
            </div>
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Cargo</LabelHorizontal>
                <select name="cCargo"  bind:value={fEmpleado.cargo} 
                 on:change={validaCargo} 
                 disabled={editFormulario ? true: false}
                 autocomplete="off"
                 class="select select-bordered font-normal 
                 text-lg text-gray-500 w-[400px]
                 disabled:select disabled:select-bordered 
                 disabled:text-gray-500 disabled:text-lg
                 disabled:font-normal">
                  <option disabled={true} selected={true}></option> 
                  {#each listadoCargos as l }
                    <option value={l.id}>{l.descripcion}</option> 
                  {/each}
                  </select>
                  {#if eEmpleado.cargo}
                      <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                  {/if}
                 
              </div>
            </div>
    
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Modulo</LabelHorizontal>
                <select name="cModulo"  bind:value={fEmpleado.modulo} 
                on:change={validaModulo}
                disabled={editFormulario ? true: false}  
                autocomplete="off"
                class="select select-bordered font-normal 
                text-lg text-gray-500 w-[400px]
                disabled:select disabled:select-bordered 
                 disabled:text-gray-500 disabled:text-lg
                 disabled:font-normal">
                  <option disabled={true} selected={true}></option> 
                  {#each listadoModulos as l }
                    <option value={l.id}>{l.descripcion}</option> 
                  {/each}
                  </select>
                  {#if eEmpleado.modulo}
                      <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                  {/if}
              </div>
            </div>
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Departamento</LabelHorizontal>
                <select name="cDepartamento"  bind:value={fEmpleado.departamento} 
                on:change={validaDepartamento} 
                disabled={editFormulario ? true: false}
                autocomplete="off"
                class="select select-bordered font-normal 
                text-lg text-gray-500 w-[400px]
                disabled:select disabled:select-bordered 
                 disabled:text-gray-500 disabled:text-lg
                 disabled:font-normal">
                  <option disabled={true} selected={true}></option> 
                  {#each listadoDepartamentos as l }
                    <option value={l.id}>{l.descripcion}</option> 
                  {/each}
                  </select>
                  {#if eEmpleado.departamento}
                      <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                  {/if}
              </div>
            </div>
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Telefono</LabelHorizontal>
                <input 
                bind:value={fEmpleado.telefono}
                readonly={editFormulario ? true: false}
                autocomplete="off"
                  type="text"
                  class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tTelefono" />
              </div>
            </div>
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Correo</LabelHorizontal>
                <input 
                bind:value={fEmpleado.correo} 
                readonly={editFormulario ? true: false}
                autocomplete="off"
                  type="text"
                  class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tCorreo" />
              </div>
            </div>
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Direccion</LabelHorizontal>
                <textarea
                  rows="2" 
                  bind:value={fEmpleado.direccion}
                  readonly={editFormulario ? true: false}
                  autocomplete="off"
                  type="text"
                  class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tDireccion" />
              </div>
            </div>
              
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Medio Pago</LabelHorizontal>
                <select name="cMedioPago"  bind:value={fEmpleado.medioPago} 
                on:change={validaMedioPago} 
                disabled={editFormulario ? true: false}
                autocomplete="off"
                class="select select-bordered 
                font-normal text-lg text-gray-500 w-[400px]
                 disabled:select disabled:select-bordered 
                 disabled:text-gray-500 disabled:text-lg
                 disabled:font-normal">
                  <option disabled={true} selected={true}></option> 
                  {#each listadoMediosPago as l }
                    <option value={l.id}>{l.descripcion}</option> 
                  {/each}
                  </select>
                  {#if eEmpleado.medioPago}
                      <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                  {/if}
              </div>
            </div>
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Banco</LabelHorizontal>
                <select name="cBanco"  bind:value={fEmpleado.banco} 
                on:change={validaBanco} 
                disabled={editFormulario ? true: false}
                autocomplete="off"
                class="select select-bordered 
                font-normal text-lg text-gray-500 w-[400px]
                disabled:select disabled:select-bordered 
                 disabled:text-gray-500 disabled:text-lg
                 disabled:font-normal">
                  <option disabled={true} selected={true}></option> 
                  {#each listadoBancos as l }
                    <option value={l.id}>{l.descripcion}</option> 
                  {/each}
                  </select>
                  {#if eEmpleado.banco}
                      <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                  {/if}
              </div>
            </div>
    
            <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Cuenta Bancaria</LabelHorizontal>
                <input 
                bind:value={fEmpleado.cuentaBancaria}
                readonly={editFormulario ? true: false}
                autocomplete="off"
                  type="text"
                  class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tCuentaBancaria" />
              </div>
            </div>
            {/if}
        </section>
    
        <section class="flex justify-end">
          {#if !editFormulario}
              <button on:click={()=>actualizarEmpleado()} class="btn btn-md mt-1 mx-1 bg-green-700 font-bold border-none hover:bg-white hover:text-green-700">Actualizar Empleado</button>
          {/if}
      </section>
        </article>
        
    </section>
    
    </main>
{:else}
   <!-- else content here -->
   <p>no tiene permisos para ver esta opcion</p>
{/if}

