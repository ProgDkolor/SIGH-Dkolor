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
             //TIPOS FUNCIONES
     import TituloPagina from "../../Componentes/TituloPagina.svelte";
     import * as Servicios from '../../Servicios/AccesoApi'
     import type * as Tipos from '../../Modelo/Tipos'
     import * as Util from '../../Servicios/Utilidades'
     import { onMount } from 'svelte';
     import {fade} from 'svelte/transition'
     
   //VARIABLES
   const marcaraPeriodo = {
         mask: '0000-00'
   };
   const mascaraCedula = {
         mask: '000-0000000-0'
   };
   let empleados:Tipos.Empleado[]=[];
   let periodoNomina:string='';
   let cedula:string='';
   let eCedula:boolean=false;
   let ePeriodoNomina:boolean=false;
   let nominaElegida:string='';
   let eNominaElegida:boolean=false;
   let consultaNominas:Tipos.ConsultaNominaPeriodo[]=[];
 
   //METODOS
   onMount(
        ()=>{
            cargarNominasRegistradas();
        }
    );

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

     const consultarComprobantePago=async()=>{
         validaCedula();
         validaNomina();
         try{
          if(!eNominaElegida && empleados.length>0){
               let datos:Tipos.RData={
                empresa: parseInt($empresaSeleccionada),
                 sp: "ConsultaBolantePagoNominaPreviaEmpleado",
                 modelo: "ComprobantePago",
                 dataParametros: [
                   {
                     parametro: "@idNomina",
                     tipo: 16,
                     valor: nominaElegida
                   },
                   {
                     parametro: "@empleado",
                     tipo: 16,
                     valor: empleados[0].cedula
                   }
                 ]
               }
               $tituloModalComprobantePago="COMPROBANTE DE PAGO NOMINA PREVIA";
               $verModalLoading=true;
               let res=await Servicios.ConsultaBd(datos);
               $verModalLoading=false;
         if(res.exito)
         {
           $bolantePago=res.data;
           console.log($bolantePago);
           $verModalBolantePago=true;
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
     const validaPeriodo=()=>{
       if(periodoNomina.length<4)
      {
         ePeriodoNomina=true;
      }else {
        ePeriodoNomina=false;
      }
     }
     const validaCedula=()=>{
       if(cedula.length<11)
      {
         eCedula=true;
      }else {
        eCedula=false;
      }
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
    const consultaPeriodo=async ()=>{
     validaPeriodo();
     try{
      if(!ePeriodoNomina)
      {
        console.log(periodoNomina);
       let datos:Tipos.RData={
        empresa: parseInt($empresaSeleccionada),
           "sp": "ConsultaNominaPeriodo",
           "modelo": "ListadoNomina",
           "dataParametros": [
             {
               "parametro": "@periodo",
               "tipo": 16,
               "valor": periodoNomina
             }
           ]
         } 
         $verModalLoading=true;
         let res=await Servicios.ConsultaBd(datos);
         $verModalLoading=false;
         if(res.exito)
         {
             consultaNominas=res.data;
             if(consultaNominas.length===0)
             {
               Util.VerMensaje("Atencion","No se encontraron registros de nomina con el periodo ingresado");
             }
            console.log("Exito");
            console.log(consultaNominas);
         }else
         {
           Util.VerMensaje("Ocurrio un error",res.mensaje);
 
         }
         console.log(res);	
       }
        }
        catch(error)
        {
          Util.VerMensaje("Error Encontrado","Error en la comunicacion con el web service."); 
        }
   };
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

  <TituloPagina Titulo="COMPROBANTE DE PAGO NOMINA PREVIA"></TituloPagina>
  
  
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
                class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tCedula" 
              />
              <button on:click={()=>consultaEmpleado()} class="btn">Consultar</button>
              {#if eCedula}
                  <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
              {/if}
            </div>
          </div>
 
          {#if empleados.length>0}
          <div class="form-control mb-1">
            <div class="input-group relative">
              <LabelHorizontal>Empleado</LabelHorizontal>
              <input 
                value={ `${empleados[0].nombre} ${empleados[0].apellido}`} 
                type="text"
                readonly
                class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tPeriodoNomina" 
              />
              
            </div>
          </div>
            {/if}
        
            {#if consultaNominas.length>0}
            <div class="form-control pt-3">
              <LabelHorizontal>Escoja Nomina</LabelHorizontal>
              {#each consultaNominas as s}
              <label class="cursor-pointer label">
                  <span class="label-text text-gray-800 font-semibold">{s.descripcion}</span> 
                  <input type="radio" bind:group={nominaElegida} on:change={validaNomina}  name="cNominaPrevia" checked={false} class="radio" value={s.id}>
                </label>
              {/each}
              {#if eNominaElegida}
              <span class="before:text-xs text-red-400">*Debe elegir la nomina</span>
              {/if}
            </div> 
            {/if}
      </section>
      {#if consultaNominas.length>0}
        <section class="flex flex-row-reverse py-2  rounded-b-2xl pr-2">
          <label for="modalCitas" on:click={consultarComprobantePago} class="btn btn-md mx-1 font-bold hover:bg-white hover:text-green-700">Consultar Comprobante Pago</label>
      </section>
      {/if}
      </article>
      
  </section>
 
  </main>
{:else if permisosCrud.c == 1 || permisosCrud.r == 1}
   <!-- else if content here -->
   
<main in:fade="{{duration: 500}}">

  <TituloPagina Titulo="COMPROBANTE DE PAGO NOMINA PREVIA"></TituloPagina>
  
  
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
                class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tCedula" 
              />
              <button on:click={()=>consultaEmpleado()} class="btn">Consultar</button>
              {#if eCedula}
                  <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
              {/if}
            </div>
          </div>
 
          {#if empleados.length>0}
          <div class="form-control mb-1">
            <div class="input-group relative">
              <LabelHorizontal>Empleado</LabelHorizontal>
              <input 
                value={ `${empleados[0].nombre} ${empleados[0].apellido}`} 
                type="text"
                readonly
                class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tPeriodoNomina" 
              />
              
            </div>
          </div>
            {/if}
        
            {#if consultaNominas.length>0}
            <div class="form-control pt-3">
              <LabelHorizontal>Escoja Nomina</LabelHorizontal>
              {#each consultaNominas as s}
              <label class="cursor-pointer label">
                  <span class="label-text text-gray-800 font-semibold">{s.descripcion}</span> 
                  <input type="radio" bind:group={nominaElegida} on:change={validaNomina}  name="cNominaPrevia" checked={false} class="radio" value={s.id}>
                </label>
              {/each}
              {#if eNominaElegida}
              <span class="before:text-xs text-red-400">*Debe elegir la nomina</span>
              {/if}
            </div> 
            {/if}
      </section>
      {#if consultaNominas.length>0}
        <section class="flex flex-row-reverse py-2  rounded-b-2xl pr-2">
          <label for="modalCitas" on:click={consultarComprobantePago} class="btn btn-md mx-1 font-bold hover:bg-white hover:text-green-700">Consultar Comprobante Pago</label>
      </section>
      {/if}
      </article>
      
  </section>
 
  </main>
{:else}
   <!-- else content here -->
   <p>No posee permisos para ver esta página.</p>
{/if}
