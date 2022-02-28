<script lang="ts">
    import {nominaPrevia,verModalNominaPrevia,verModalLoading,empresaSeleccionada} from '../../Modelo/Datos'
    import LabelHorizontal from '../../Componentes/LabelHorizontal.svelte'
    import { onMount } from 'svelte';
      import MaskInput from "svelte-input-mask/MaskInput.svelte";
              //TIPOS FUNCIONES
      import TituloPagina from "../../Componentes/TituloPagina.svelte";
      import * as Servicios from '../../Servicios/AccesoApi'
      import type * as Tipos from '../../Modelo/Tipos'
      import * as Util from '../../Servicios/Utilidades'
      import {fade} from 'svelte/transition'
      
    //VARIABLES
    let periodoNomina:string='';
    let ePeriodoNomina:boolean=false;
    let nominaElegida:string='';
    let eNominaElegida:boolean=false;
    let consultaNominas:Tipos.ConsultaNominaPeriodo[]=[];
    onMount(
        ()=>{
            console.log('voy a buscar los datos');
            cargarNominasAprobadas();
        }
    );
  
    //METODOS
      const generarNominaPrevia=async()=>{
          validaNomina();
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
      const cargarNominasAprobadas=async ()=>{
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
    };
     const consultaPeriodo=async ()=>{
      validaPeriodo();
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
  </script>

<main in:fade="{{duration: 500}}">

  <TituloPagina Titulo="PROCESAR PARA APROBACION"></TituloPagina>
  
  
  <section class="w-full">
  <article class="bg-base-200 rounded-b-2xl rounded-t-2xl p-3 mx-[5%]">

    <button on:click={()=>cargarNominasAprobadas()} class="btn btn-sm mx-1 bg-green-700 font-bold border-none hover:bg-white hover:text-green-700">Consultar Nominas Aprobadas</button>
  
      <section class="pt-0">
            {#if consultaNominas.length>0}
            <div class="form-control pt-3">
              <LabelHorizontal>Esoja Nomina</LabelHorizontal>
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
          <label for="modalCitas" on:click={generarNominaPrevia} class="btn btn-md mx-1 font-bold hover:bg-white hover:text-green-700">Generar</label>
      </section>
      {/if}
      </article>
      
  </section>

  </main>