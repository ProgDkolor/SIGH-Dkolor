<script lang="ts">
  import {nominaPrevia,verModalNominaPrevia,verModalLoading,empresaSeleccionada,cedulaUsuario, accionesAplicables} from '../../Modelo/Datos'
  import LabelHorizontal from '../../Componentes/LabelHorizontal.svelte'
            //TIPOS FUNCIONES
    import TituloPagina from "../../Componentes/TituloPagina.svelte";
    import * as Servicios from '../../Servicios/AccesoApi'
    import type * as Tipos from '../../Modelo/Tipos'
    import * as Util from '../../Servicios/Utilidades'
    import {fade} from 'svelte/transition'
    import * as Enu from '../../Modelo/Enumeraciones'
    import { onMount } from 'svelte';
    
  //VARIABLES
  let periodoNomina:string='';
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
    const limpiar=()=>{
      consultaNominas=[];
    }
    const procesarParaAprobacion=async ()=>{
      validaNomina();
      try{
        if(!eNominaElegida)
    {
      let datos:Tipos.RData={
        empresa: parseInt($empresaSeleccionada),
            sp: "ActualizarCondicionNomina",
            modelo: "RDataRegistro",
            dataParametros: [
              {
                parametro: "@idNomina",
                tipo: Enu.DbType.String,
                valor: nominaElegida
              },
              {
                parametro: "@condicion",
                tipo: Enu.DbType.Int32,
                valor: "4"
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
                  Util.VerMensaje("Atencion","Nomina procesada para aprobacion con exito.");
                  cargarNominasRegistradasPostRegistro();
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
    const options = {
		mask: '0000'
  };
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
   const consultaPeriodo=async ()=>{
    validaPeriodo();
    try{
        if(!ePeriodoNomina)
      {
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
  const cargarNominasRegistradasPostRegistro=async ()=>{
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
            }else
            {
                console.log("error al consultar nominas pendientes de procesar "+res.mensaje);
    
            }
            console.log(res);	
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

    <TituloPagina Titulo="GENERAR NOMINA PREVIA"></TituloPagina>
    
    
    <section class="w-full">
    <article class="rounded-b-2xl rounded-t-2xl p-3 mx-[5%]">
    
      <button on:click={()=>cargarNominasRegistradas()} class="btn btn-sm mx-1 bg-green-700 font-bold border-none hover:bg-white hover:text-green-700">Consultar Nominas Registradas</button>
    
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
              {/if}
        </section>
        {#if consultaNominas.length>0}
          <section class="flex flex-row-reverse py-2  rounded-b-2xl pr-2">
            <button for="modalCitas" on:click={procesarParaAprobacion} class="btn btn-md mx-1 font-bold bg-red-700 border-none hover:bg-white hover:text-green-700">Procesar Para Aprobacion</button>
            <button for="modalCitas" on:click={generarNominaPrevia} class="btn btn-md mx-1 font-bold hover:bg-white border-none hover:text-green-700">Generar Nomina Previa</button>
        </section>
        {/if}
        </article>
        
    </section>
    
    </main>
{:else if  permisosCrud.c == 1 || permisosCrud.r == 1}
   <!-- else if content here -->

   <main in:fade="{{duration: 500}}">

    <TituloPagina Titulo="GENERAR NOMINA PREVIA"></TituloPagina>
    
    
    <section class="w-full">
    <article class="rounded-b-2xl rounded-t-2xl p-3 mx-[5%]">
    
      <button on:click={()=>cargarNominasRegistradas()} class="btn btn-sm mx-1 bg-green-700 font-bold border-none hover:bg-white hover:text-green-700">Consultar Nominas Registradas</button>
    
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
              {/if}
        </section>
        {#if consultaNominas.length>0}
          <section class="flex flex-row-reverse py-2  rounded-b-2xl pr-2">
            <button for="modalCitas" on:click={procesarParaAprobacion} class="btn btn-md mx-1 font-bold bg-red-700 border-none hover:bg-white hover:text-green-700">Procesar Para Aprobacion</button>
            <button for="modalCitas" on:click={generarNominaPrevia} class="btn btn-md mx-1 font-bold hover:bg-white border-none hover:text-green-700">Generar Nomina Previa</button>
        </section>
        {/if}
        </article>
        
    </section>
    
    </main>
{:else}
   <!-- else content here -->
   <p>
     No posee permisos para acceder a esta opcion
   </p>
{/if}
