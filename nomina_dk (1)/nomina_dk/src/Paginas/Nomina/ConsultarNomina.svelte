<script lang="ts">
  import {nominaPrevia,verModalNominaPrevia,verModalLoading,LoginActivo,empresaSeleccionada, accionesAplicables} from '../../Modelo/Datos'
  import LabelHorizontal from '../../Componentes/LabelHorizontal.svelte'
  import { imask } from 'svelte-imask';
  import { onMount } from 'svelte';
            //TIPOS FUNCIONES
    import TituloPagina from "../../Componentes/TituloPagina.svelte";
    import * as Servicios from '../../Servicios/AccesoApi'
    import type * as Tipos from '../../Modelo/Tipos'
    import * as Util from '../../Servicios/Utilidades'
    import {fade} from 'svelte/transition'
    import {push, pop, replace} from 'svelte-spa-router'
    
  //VARIABLES
  const marcaraPeriodo = {
		mask: '0000-00'
  };
  let periodoNomina:string='';
  let ePeriodoNomina:boolean=false;
  let nominaElegida:string='';
  let eNominaElegida:boolean=false;
  let eReporteElegido:boolean=false;
  let consultaNominas:Tipos.ConsultaNominaPeriodo[]=[];
  let listadoReportes:Tipos.Listado[]=[];
  let reporteElegido:string='';

  //METODOS
  onMount(
        ()=>{
            cargarReportes();
        }
    );
  const cargarReportes=async ()=>{
      try{
        let datos:Tipos.RData={
            empresa: parseInt($empresaSeleccionada),
              sp: "ListadoReportesNomina",
              modelo: "dynamic",
              dataParametros: []
            } 
            $verModalLoading=true;
            let res=await Servicios.ConsultaBd(datos);
            $verModalLoading=false;
            if(res.exito)
            {
              listadoReportes=res.data;
                if(listadoReportes.length==0)
                {
                    Util.VerMensaje("Atencion","No se encontraron bancos registrados.");
                }
               console.log("Exito");
               console.log(listadoReportes);
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
      const descargarReporte=async()=>{
        validaReporte();
        validaNomina();
        if(!eReporteElegido && !eNominaElegida)
        {
            let datos:Tipos.DReporteNomina={
            empresa: 1,
            idNomina: nominaElegida,
            idReporte: parseInt(reporteElegido),
            nomina: `${listadoReportes.find(l=>l.id===parseInt(reporteElegido)).descripcion} ${consultaNominas.find(n=>n.id===nominaElegida).descripcion.replaceAll('NOMINA ','')}`,
            reporte: listadoReportes.find(l=>l.id===parseInt(reporteElegido)).descripcion,
            nombreArchivo:`${listadoReportes.find(l=>l.id===parseInt(reporteElegido)).descripcion} ${consultaNominas.find(n=>n.id===nominaElegida).descripcion.replaceAll('NOMINA ','')}`
            };
            $verModalLoading=true;
            let res=await Servicios.DescargaReporteNomina(datos);
            $verModalLoading=false;
        }
        else if(eNominaElegida){
          Util.verNotificacion("Escoja la nomina del reporte a generar.");
        }
        else if(eReporteElegido){
          Util.verNotificacion("Escoja el tipo de reporte deseado.");
        }
      }
      const descargaArchivoBanco=async()=>{
        validaNomina();
        if(!eNominaElegida)
        {
          let datos:Tipos.DArchivoBanco={
          empresa: 1,
          nomina: nominaElegida,
          banco: 1,
          nombreArchivo:`${consultaNominas.find(n=>n.id===nominaElegida).descripcion}`
          };
          $verModalLoading=true;
          let res=await Servicios.DescargaArchivoBanco(datos);
          $verModalLoading=false;
        }else{
          Util.verNotificacion("Elija la nomina de la que se va a generar el archivo del banco.");
        }
      }
    const generarNominaPrevia=async()=>{
        validaNomina();
        try{
            if(!eNominaElegida){
                let datos:Tipos.RData={
                  empresa: parseInt($empresaSeleccionada),
                  sp: "GeneraNominaPrevia",
                  modelo: "GeneraNominaPrevia",
                  dataParametros: [
                    {
                      parametro: "@id",
                      tipo: 16,
                      valor: nominaElegida
                    }
                  ]
                }
                $verModalLoading=true;
                let res=await Servicios.ConsultaBd(datos);
                $verModalLoading=false;
          if(res.exito)
          {
            $nominaPrevia=res.data;
            $verModalNominaPrevia=true;
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
    const validaReporte=()=>{
        if(reporteElegido.length<1)
            eReporteElegido=true;
        else
            eReporteElegido=false;
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
      let datos:Tipos.RData={
        empresa: parseInt($empresaSeleccionada),
          "sp": "ConsultaNominaSometidasPeriodo",
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
              Util.VerMensaje("Atencion","No se encontraron registros de nomina completadas con el periodo ingresado");
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
    if(!$LoginActivo)
    {
      replace("/");
    }

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

   <main in:fade="{{duration: 2000}}">

    <TituloPagina Titulo="CONSULTAR NOMINA"></TituloPagina>
    
    
    <section class="w-full">
    <article class="rounded-b-2xl rounded-t-2xl p-3 mx-[5%]">
    
    
        <section class="pt-0">
    
          <div class="form-control mb-1">
            <div class="input-group relative">
              <LabelHorizontal>Periodo Nomina</LabelHorizontal>
              <input 
                bind:value={periodoNomina} 
                use:imask={marcaraPeriodo}
                type="text"
                on:keyup={()=>validaPeriodo()}
                class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tPeriodoNomina" 
              />
              <button on:click={()=>consultaPeriodo()} class="btn">Consultar</button>
              {#if ePeriodoNomina}
                  <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
              {/if}
            </div>
          </div>
          
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
        <div class="form-control pt-3">
          <LabelHorizontal>Escoja Reporte</LabelHorizontal>
    
          {#each listadoReportes as l}
            <label class="cursor-pointer label">
              <span class="label-text text-gray-800 font-semibold">{l.descripcion}</span> 
              <input type="radio" bind:group={reporteElegido} on:change={validaReporte}  name="dReporteNomina" checked={false} class="radio" value={l.id}>
            </label>
          {/each}
          {#if eReporteElegido}
          <span class="before:text-xs text-red-400">*Debe elegir el tipo de reporte</span>
          {/if}
          
        </div> 
    
    
          <section class="flex justify-end py-2  rounded-b-2xl pr-2">
            <button on:click={descargaArchivoBanco} class="btn btn-md mx-1 bg-green-600 border-none font-bold hover:bg-white hover:text-green-700">Descargar Archivo Banco</button>
            <button on:click={descargarReporte} class="btn btn-md mx-1 bg-red-600 border-none font-bold hover:bg-white hover:text-green-700">Descargar Reporte Seleccionado</button>
        </section>
        {/if}
        </article>
        
    </section>
    </main>
{:else if  permisosCrud.c == 1 || permisosCrud.r == 1}
   <!-- else if content here -->

   <main in:fade="{{duration: 2000}}">

    <TituloPagina Titulo="CONSULTAR NOMINA"></TituloPagina>
    
    
    <section class="w-full">
    <article class="rounded-b-2xl rounded-t-2xl p-3 mx-[5%]">
    
    
        <section class="pt-0">
    
          <div class="form-control mb-1">
            <div class="input-group relative">
              <LabelHorizontal>Periodo Nomina</LabelHorizontal>
              <input 
                bind:value={periodoNomina} 
                use:imask={marcaraPeriodo}
                type="text"
                on:keyup={()=>validaPeriodo()}
                class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tPeriodoNomina" 
              />
              <button on:click={()=>consultaPeriodo()} class="btn">Consultar</button>
              {#if ePeriodoNomina}
                  <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
              {/if}
            </div>
          </div>
          
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
        <div class="form-control pt-3">
          <LabelHorizontal>Escoja Reporte</LabelHorizontal>
    
          {#each listadoReportes as l}
            <label class="cursor-pointer label">
              <span class="label-text text-gray-800 font-semibold">{l.descripcion}</span> 
              <input type="radio" bind:group={reporteElegido} on:change={validaReporte}  name="dReporteNomina" checked={false} class="radio" value={l.id}>
            </label>
          {/each}
          {#if eReporteElegido}
          <span class="before:text-xs text-red-400">*Debe elegir el tipo de reporte</span>
          {/if}
          
        </div> 
    
    
          <section class="flex justify-end py-2  rounded-b-2xl pr-2">
            <button on:click={descargaArchivoBanco} class="btn btn-md mx-1 bg-green-600 border-none font-bold hover:bg-white hover:text-green-700">Descargar Archivo Banco</button>
            <button on:click={descargarReporte} class="btn btn-md mx-1 bg-red-600 border-none font-bold hover:bg-white hover:text-green-700">Descargar Reporte Seleccionado</button>
        </section>
        {/if}
        </article>
        
    </section>
    </main>
{:else}
   <!-- else content here -->
   <p>
     No posee permisos.
   </p>
{/if}
