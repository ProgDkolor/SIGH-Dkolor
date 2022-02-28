<script lang="ts">
    import {nominaPrevia,verModalNominaPrevia,verModalLoading,empresaSeleccionada
    ,dataNovedad,verModalReporteNovedad,tituloModalReporteNovedad,cedulaUsuario, accionesAplicables} from '../../Modelo/Datos'
    import LabelHorizontal from '../../Componentes/LabelHorizontal.svelte'
    import { onMount } from 'svelte';
              //TIPOS FUNCIONES
      import TituloPagina from "../../Componentes/TituloPagina.svelte";
      import * as Servicios from '../../Servicios/AccesoApi'
      import type * as Tipos from '../../Modelo/Tipos'
      import * as Util from '../../Servicios/Utilidades'
      import * as Enu from '../../Modelo/Enumeraciones'
      import {fade} from 'svelte/transition'
      
    //VARIABLES
    let periodoNomina:string='';
    let ePeriodoNomina:boolean=false;
    let nominaElegida:string='';
    let reporteElegido:string='';
    let eNominaElegida:boolean=false;
    let eReporteElegido:boolean=false;
    let consultaNominas:Tipos.ConsultaNominaPeriodo[]=[];
    onMount(
        ()=>{
            console.log('voy a buscar los datos');
            cargarNominasAprobadas();
        }
    );
  
    //METODOS
    const limpiar=()=>{
      consultaNominas=[];
    }
    const procesarNominaDefinitiva=async ()=>{
      validaNomina();
      try{
        if(!eNominaElegida)
    {
      let datos:Tipos.RData={
        empresa: parseInt($empresaSeleccionada),
            sp: "GeneraNominaDefinitiva",
            modelo: "RDataRegistro",
            dataParametros: [
              {
                parametro: "@idNomina",
                tipo: Enu.DbType.String,
                valor: nominaElegida
              },
              {
                parametro: "@empleado",
                tipo: Enu.DbType.String,
                valor: $cedulaUsuario
              },
              {
                parametro: "@nota",
                tipo: Enu.DbType.String,
                valor: "Actualizado dese la web"
              }
            ]
          } 
          $verModalLoading=true;
          let res=await Servicios.RegistroBd(datos);
          $verModalLoading=false;
          if(res.exito)
          {
           let resNomina=res.data;
              if(resNomina.length>0)
              {
                if(resNomina[0].resultado==0)
                {
                  limpiar();
                  Util.VerMensaje("Atencion","Nomina definitiva procesada con exito.");
                  cargarNominasAprobadasPostRegistro();
                }else{
                  Util.VerMensaje("Error al registrar",resNomina[0].mensaje);
                }
              }
             console.log("Exito");
             console.log(resNomina);
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
   
       
    };

      const generarNominaPrevia=async()=>{
          validaNomina();
          try{
            if(!eNominaElegida){
                let datos:Tipos.RData={
                  empresa: parseInt($empresaSeleccionada),
                  sp: "GeneraNominaPrevia",
                  modelo: "dynamic",
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
        if(periodoNomina.length<5)
       {
          ePeriodoNomina=true;
       }else {
         ePeriodoNomina=false;
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

      const validaReporteElegido=()=>{
       if(reporteElegido.length<1)
          {
              eReporteElegido =true;
              return;
          }
          else{
            eReporteElegido=false;
          }
      }
      const cargarNominasAprobadas=async ()=>{
        try{
          if(!ePeriodoNomina)
       {
        let datos:Tipos.RData={
          empresa: parseInt($empresaSeleccionada),
            sp: "ConsultaNominaCondicion",
            modelo: "ListadoNomina",
            dataParametros: [
              {
                parametro: "@condicion",
                tipo: 16,
                valor: "4"
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
                  Util.VerMensaje("Atencion","No se encontraron nominas en condicion APROBADA.");
              }
             console.log("Exito");
             console.log(consultaNominas);
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
       
    };

    const consultaReporteNovedad=async()=>{
          validaNomina();
          validaReporteElegido();
          try{
            if(!eNominaElegida && !eReporteElegido ){
              let datos:Tipos.RData;
              if(reporteElegido==="1")
              {
                $tituloModalReporteNovedad="HORAS EXTRAS REPORTADAS EN NOMINA";
                datos={
                  empresa: parseInt($empresaSeleccionada),
                  sp: "ConsultaHorasExtrasNomina",
                  modelo: "dynamic",
                  dataParametros: [
                    {
                      parametro: "@nomina",
                      tipo: 16,
                      valor: nominaElegida
                    }
                  ]
                }
              } else  if(reporteElegido==="2")
              {
                $tituloModalReporteNovedad="VACACIONES REPORTADAS EN NOMINA";
                datos={
                  empresa: parseInt($empresaSeleccionada),
                  sp: "ConsultaVacacionesNomina",
                  modelo: "dynamic",
                  dataParametros: [
                    {
                      parametro: "@nomina",
                      tipo: 16,
                      valor: nominaElegida
                    }
                  ]
                }
              } else  if(reporteElegido==="3")
              {
                $tituloModalReporteNovedad="DESCUENTO DE DIAS REPORTADOS EN NOMINA";
                datos={
                  empresa: parseInt($empresaSeleccionada),
                  sp: "ConsultaDescuentoDiaNomina",
                  modelo: "dynamic",
                  dataParametros: [
                    {
                      parametro: "@nomina",
                      tipo: 16,
                      valor: nominaElegida
                    }
                  ]
                }
              }
                
                $verModalLoading=true;
                let res=await Servicios.ConsultaBd(datos);
                $verModalLoading=false;
          if(res.exito)
          {
            $dataNovedad=res.data;
            if($dataNovedad.length>0)
            {
              $verModalReporteNovedad=true;
            }else{
              Util.VerMensaje("Atencion","No se encontraron registros.");
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
    

    const cargarNominasAprobadasPostRegistro=async ()=>{
      try{
        if(!ePeriodoNomina)
       {
        let datos:Tipos.RData={
          empresa: parseInt($empresaSeleccionada),
            sp: "ConsultaNominaCondicion",
            modelo: "ListadoNomina",
            dataParametros: [
              {
                parametro: "@condicion",
                tipo: 16,
                valor: "4"
              }
            ]
          } 
          $verModalLoading=true;
          let res=await Servicios.ConsultaBd(datos);
          $verModalLoading=false;
          if(res.exito)
          {
              consultaNominas=res.data;
          }else
          {
              console.log("error al cargar nominas procesadas para aprobacion");
  
          }
          console.log(res);	
        }
          }
          catch(error)
          {
            Util.VerMensaje("Error Encontrado","Error en la comunicacion con el web service."); 
          }
    };
     const consultaPeriodo=async ()=>{
      validaPeriodo();
      try{
        if(!ePeriodoNomina)
       {
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
             console.log("Exito");
             console.log(consultaNominas);
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
    };
    const actualizaPeriodo = ({ detail }) => {
      const value = detail.inputState.maskedValue;
        periodoNomina=value;
        validaPeriodo();
       }
  
       const OnEnterPeriodo = e => {
      if (e.charCode === 13) {
      console.log('enter presionado');
      consultaPeriodo();
      }
    };
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

    <TituloPagina Titulo="GENERAR NOMINA DEFINITIVA"></TituloPagina>
    
    
    <section class="w-full">
    <article class="rounded-b-2xl rounded-t-2xl p-3 mx-[5%]">
  
      <button on:click={()=>cargarNominasAprobadas()} class="btn btn-sm mx-1 bg-green-700 font-bold border-none hover:bg-white hover:text-green-700">Consultar Nominas Aprobadas</button>
    
        <section class="pt-0">
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
  
              <div class="form-control pt-3">
                <LabelHorizontal>Reporte Novedad</LabelHorizontal>
                <label class="cursor-pointer label">
                  <span class="label-text text-gray-800 font-semibold">Horas Extras</span> 
                  <input type="radio" bind:group={reporteElegido} on:change={validaReporteElegido}  name="cReporteNovedad" checked={false} class="radio" value={"1"}>
                </label>
  
                <label class="cursor-pointer label">
                  <span class="label-text text-gray-800 font-semibold">Vacaciones</span> 
                  <input type="radio" bind:group={reporteElegido} on:change={validaReporteElegido}  name="cReporteNovedad" checked={false} class="radio" value={"2"}>
                </label>
  
                <label class="cursor-pointer label">
                  <span class="label-text text-gray-800 font-semibold">Descuento Dia</span> 
                  <input type="radio" bind:group={reporteElegido} on:change={validaReporteElegido}  name="cReporteNovedad" checked={false} class="radio" value={"3"}>
                </label>
                {#if eReporteElegido}
                <span class="before:text-xs text-red-400">*Debe elegir el reporte</span>
                {/if}
              </div> 
              
  
              {/if}
        </section>
        {#if consultaNominas.length>0}
          <section class="flex  py-2  rounded-b-2xl pr-2">
            <button for="modalCitas" on:click={procesarNominaDefinitiva} class="btn btn-md hover:border hover:border-red-700 mx-1 font-bold bg-red-700 border-none hover:bg-white hover:text-green-700">Generar Nomina Definitiva</button>
            <button for="modalCitas" on:click={generarNominaPrevia} class="btn btn-md mx-1 font-bold hover:bg-white hover:text-green-700">Consultar Detalle</button>
            <button for="modalCitas" on:click={consultaReporteNovedad} class="btn btn-md bg-green-600 border-none hover:border-green-600 hover:border mx-1 font-bold hover:bg-white hover:text-green-700">Generar Reporte</button>
  
        </section>
        {/if}
        </article>
        
    </section>
  
  </main>
{:else if  permisosCrud.c == 1 || permisosCrud.r == 1}
   <!-- else if content here -->
   <main in:fade="{{duration: 500}}">

    <TituloPagina Titulo="GENERAR NOMINA DEFINITIVA"></TituloPagina>
    
    
    <section class="w-full">
    <article class="rounded-b-2xl rounded-t-2xl p-3 mx-[5%]">
  
      <button on:click={()=>cargarNominasAprobadas()} class="btn btn-sm mx-1 bg-green-700 font-bold border-none hover:bg-white hover:text-green-700">Consultar Nominas Aprobadas</button>
    
        <section class="pt-0">
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
  
              <div class="form-control pt-3">
                <LabelHorizontal>Reporte Novedad</LabelHorizontal>
                <label class="cursor-pointer label">
                  <span class="label-text text-gray-800 font-semibold">Horas Extras</span> 
                  <input type="radio" bind:group={reporteElegido} on:change={validaReporteElegido}  name="cReporteNovedad" checked={false} class="radio" value={"1"}>
                </label>
  
                <label class="cursor-pointer label">
                  <span class="label-text text-gray-800 font-semibold">Vacaciones</span> 
                  <input type="radio" bind:group={reporteElegido} on:change={validaReporteElegido}  name="cReporteNovedad" checked={false} class="radio" value={"2"}>
                </label>
  
                <label class="cursor-pointer label">
                  <span class="label-text text-gray-800 font-semibold">Descuento Dia</span> 
                  <input type="radio" bind:group={reporteElegido} on:change={validaReporteElegido}  name="cReporteNovedad" checked={false} class="radio" value={"3"}>
                </label>
                {#if eReporteElegido}
                <span class="before:text-xs text-red-400">*Debe elegir el reporte</span>
                {/if}
              </div> 
              
  
              {/if}
        </section>
        {#if consultaNominas.length>0}
          <section class="flex  py-2  rounded-b-2xl pr-2">
            <button for="modalCitas" on:click={procesarNominaDefinitiva} class="btn btn-md hover:border hover:border-red-700 mx-1 font-bold bg-red-700 border-none hover:bg-white hover:text-green-700">Generar Nomina Definitiva</button>
            <button for="modalCitas" on:click={generarNominaPrevia} class="btn btn-md mx-1 font-bold hover:bg-white hover:text-green-700">Consultar Detalle</button>
            <button for="modalCitas" on:click={consultaReporteNovedad} class="btn btn-md bg-green-600 border-none hover:border-green-600 hover:border mx-1 font-bold hover:bg-white hover:text-green-700">Generar Reporte</button>
  
        </section>
        {/if}
        </article>
        
    </section>
  
  </main>
{:else}
   <!-- else content here -->
   <p>
     No posee permisos
   </p>
{/if}
