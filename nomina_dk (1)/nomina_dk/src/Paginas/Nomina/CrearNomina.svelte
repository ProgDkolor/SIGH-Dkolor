<script lang="ts">
  import TituloPagina from "../../Componentes/TituloPagina.svelte";
  import { imask,MaskedInput } from 'svelte-imask';
  import {
    listadoMeses,
    tiposNomina,
    listadoQuincena,
    verModalLoading,
    eNomina,
    fNomina,
    empresaSeleccionada,
    cedulaUsuario,
    accionesAplicables
  } from '../../Modelo/Datos'
  import type * as Tipos from '../../Modelo/Tipos'
  import * as Servicios from '../../Servicios/AccesoApi'
  import * as Util from '../../Servicios/Utilidades'
  import {fade} from 'svelte/transition'
  import LabelHorizontal from '../../Componentes/LabelHorizontal.svelte'

  let resNomina:Tipos.RDataRegistro[]=[];
  
  const validaAnio=()=>{
    console.log('entre');
    if($fNomina.anio.toString().length<4){
        $eNomina.anio=true;
    }else{
      $eNomina.anio=false;
    }
  }
  const validaTipo=()=>{
    if($fNomina.tipo.toString()===''){
        $eNomina.tipo=true;
    }else{
      $eNomina.tipo=false;
    }
  }
  const validaQuincena=()=>{
    if($fNomina.quincena.toString()===''){
        $eNomina.quincena=true;
    }else{
      $eNomina.quincena=false;
    }
  }

  const validaMes=()=>{
    if($fNomina.mes.toString()===''){
        $eNomina.mes=true;
    }else{
      $eNomina.mes=false;
    }
  }

  const limpiar=()=>{
      $fNomina={
      anio:'',
      mes:'',
      tipo:'',
      quincena:'',
      nota:''
    };
  }

  const registrarNomina=async ()=>{
      validaTipo();
      validaAnio();
      validaMes();
      validaQuincena();
      try{
        if(!$eNomina.tipo && !$eNomina.anio && !$eNomina.mes &&!$eNomina.quincena)
    {
      let datos:Tipos.RData={
            empresa:parseInt($empresaSeleccionada),
            sp: "RegistroNomina",
            modelo: "RDataRegistro",
            dataParametros: [
              {
                parametro: "@empleado",
                tipo: 16,
                valor: $cedulaUsuario
              },
              {
                parametro: "@anio",
                tipo: 11,
                valor: $fNomina.anio.toString()
              },
              {
                parametro: "@mes",
                tipo: 11,
                valor: $fNomina.mes.toString()
              },
              {
                parametro: "@quincena",
                tipo: 11,
                valor: $fNomina.quincena.toString()
              },
              {
                parametro: "@tipo",
                tipo: 11,
                valor: $fNomina.tipo.toString()
              },
              {
                parametro: "@nota",
                tipo: 16,
                valor: $fNomina.nota
              }
            ]
          } 
          $verModalLoading=true;
          let res=await Servicios.RegistroBd(datos);
          $verModalLoading=false;
          if(res.exito)
          {
            resNomina=res.data;
              if(resNomina.length>0)
              {
                if(resNomina[0].resultado==0)
                {
                  limpiar();
                  Util.VerMensaje("Atencion","Nomina registada con exito.");
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
    } else
    {
      Util.verNotificacion("Complete los campos requeridos.");
    }
      }
      catch(error)
      {
        Util.VerMensaje("Error Encontrado","Error en la comunicacion con el web service.");
      }
       
    }
    const options = {
		mask: '0000'
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

  <TituloPagina Titulo="CREAR NOMINA"></TituloPagina>

  <section class="w-[700px] mt-1">
    <article class=" rounded-b-2xl rounded-t-2xl p-3 mx-[5%]">
      <section class="mt-1">

        <div class="form-control mb-1">
          <div class="input-group input-group-sm relative">
            <LabelHorizontal>Tipo Nomina</LabelHorizontal>
            <select bind:value={$fNomina.tipo}  name="cMes" on:change={validaTipo}  class="select select-bordered font-normal text-lg text-gray-500 w-[400px]">
              <option disabled={true} selected={true}></option> 
              {#each $tiposNomina as t }
                <option value={t.id}>{t.name}</option> 
              {/each}
              </select>
              {#if $eNomina.tipo}
                <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
            {/if}
          </div>
        </div>

        <div class="form-control mb-1">
          <div class="input-group relative">
            <LabelHorizontal>Año</LabelHorizontal>
            <MaskedInput 
              bind:value={$fNomina.anio} 
              options={options}
              type="text"
              on:complete={()=>validaAnio()}
              class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tAnio" 
            />
            {#if $eNomina.anio}
                <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
            {/if}
          </div>
        </div>

        <div class="form-control mb-1">
          <div class="input-group relative">
            <LabelHorizontal>Mes</LabelHorizontal>
            <select name="cMes" bind:value={$fNomina.mes} on:change={validaMes} class="select select-bordered font-normal text-lg text-gray-500 w-[400px]">
              <option disabled={true} selected={true}></option> 
              {#each $listadoMeses as m }
                <option value={m.id}>{m.name}</option> 
              {/each}
              </select>
              {#if $eNomina.mes}
                <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
            {/if}
          </div>
        </div>

        <div class="form-control mb-1">
          <div class="input-group relative">
            <LabelHorizontal>Quincena</LabelHorizontal>
            <select name="cQuincena" bind:value={$fNomina.quincena} on:change={validaQuincena}  class="select select-bordered font-normal text-lg text-gray-500 w-[400px]">
              <option disabled={true} selected={true}></option> 
              {#each $listadoQuincena as q }
                <option value={q}>{q}</option> 
              {/each}
              </select>
              {#if $eNomina.quincena}
                <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
            {/if}
          </div>
        </div>

        <div class="form-control mb-1">
          <div class="input-group">
            <LabelHorizontal>Nota</LabelHorizontal>
            <textarea bind:value={$fNomina.nota} class="textarea h-[150px] textarea-bordered text-lg w-[400px]" placeholder=""></textarea>
          </div>
        </div>

        
      </section>

      <section class="flex justify-end">
              <button on:click={()=>registrarNomina()} class="btn btn-md mt-1 mx-1 bg-green-700 font-bold border-none hover:bg-white hover:text-green-700">Registrar</button>
      </section>

  
        <section class="pt-0">
            
              
        </article>
        
    </section>

</main>
   
{:else if  permisosCrud.c == 1}
   <!-- else if content here -->
   
<main in:fade="{{duration: 500}}">

  <TituloPagina Titulo="CREAR NOMINA"></TituloPagina>

  <section class="w-[700px] mt-1">
    <article class=" rounded-b-2xl rounded-t-2xl p-3 mx-[5%]">
      <section class="mt-1">

        <div class="form-control mb-1">
          <div class="input-group input-group-sm relative">
            <LabelHorizontal>Tipo Nomina</LabelHorizontal>
            <select bind:value={$fNomina.tipo}  name="cMes" on:change={validaTipo}  class="select select-bordered font-normal text-lg text-gray-500 w-[400px]">
              <option disabled={true} selected={true}></option> 
              {#each $tiposNomina as t }
                <option value={t.id}>{t.name}</option> 
              {/each}
              </select>
              {#if $eNomina.tipo}
                <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
            {/if}
          </div>
        </div>

        <div class="form-control mb-1">
          <div class="input-group relative">
            <LabelHorizontal>Año</LabelHorizontal>
            <MaskedInput 
              bind:value={$fNomina.anio} 
              options={options}
              type="text"
              on:complete={()=>validaAnio()}
              class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tAnio" 
            />
            {#if $eNomina.anio}
                <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
            {/if}
          </div>
        </div>

        <div class="form-control mb-1">
          <div class="input-group relative">
            <LabelHorizontal>Mes</LabelHorizontal>
            <select name="cMes" bind:value={$fNomina.mes} on:change={validaMes} class="select select-bordered font-normal text-lg text-gray-500 w-[400px]">
              <option disabled={true} selected={true}></option> 
              {#each $listadoMeses as m }
                <option value={m.id}>{m.name}</option> 
              {/each}
              </select>
              {#if $eNomina.mes}
                <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
            {/if}
          </div>
        </div>

        <div class="form-control mb-1">
          <div class="input-group relative">
            <LabelHorizontal>Quincena</LabelHorizontal>
            <select name="cQuincena" bind:value={$fNomina.quincena} on:change={validaQuincena}  class="select select-bordered font-normal text-lg text-gray-500 w-[400px]">
              <option disabled={true} selected={true}></option> 
              {#each $listadoQuincena as q }
                <option value={q}>{q}</option> 
              {/each}
              </select>
              {#if $eNomina.quincena}
                <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
            {/if}
          </div>
        </div>

        <div class="form-control mb-1">
          <div class="input-group">
            <LabelHorizontal>Nota</LabelHorizontal>
            <textarea bind:value={$fNomina.nota} class="textarea h-[150px] textarea-bordered text-lg w-[400px]" placeholder=""></textarea>
          </div>
        </div>

        
      </section>

      <section class="flex justify-end">
              <button on:click={()=>registrarNomina()} class="btn btn-md mt-1 mx-1 bg-green-700 font-bold border-none hover:bg-white hover:text-green-700">Registrar</button>
      </section>

  
        <section class="pt-0">
            
              
        </article>
        
    </section>

</main>
   
{:else}
   <!-- else content here -->
   <p>No posee permisos.</p>
{/if}
