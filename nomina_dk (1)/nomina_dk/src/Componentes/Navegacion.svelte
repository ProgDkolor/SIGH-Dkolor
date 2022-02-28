<script lang="ts">
  import {location} from 'svelte-spa-router';
  import {push, pop, replace} from 'svelte-spa-router';
  import { LoginActivo,empresaSeleccionada, listadoEmpresas,nombreEmpresa,usuarioActual,verModalCambioClave 
  ,verModalConfirmacion,accionModalConfirmacion, cedulaUsuario, rolUsuarioId, rutas, modulosDisponibles} from '../Modelo/Datos';
  import { onMount } from 'svelte';
  import * as Util from '../Servicios/Utilidades';
  import type * as Tipos from '../Modelo/Tipos';
  import * as Servicios from '../Servicios/AccesoApi';
  import logoDkolor from '../assets/img/LogoDkolor-white.png';
  import * as Modulos from '../Enums/Modulos';
  // let dataArchivo:Tipos.DReporteNomina={
  //   empresa: 1,
  //   idNomina: "02EF338D-F662-4683-96DA-DC3E29161050",
  //   idReporte: 4,
  //   nomina: "nomina nombre",
  //   reporte: "nomina reporte",
  // };

  onMount(
    // Este bloque de código se ejecuta cuando una persona recarga la página.
    // esto se debe a que el método onMount, se ejecuta al cargar la pagina la primera vez.
    // y al recargarla nueva vez, el valor de la variable "$loginActivo" cambia su valor de true - false
        ()=>{
          confirmaLogin();
        }
    );

    const confirmaLogin=()=>{
        if(!$LoginActivo){
          push('/');
        }
    }

  const salirSistema=()=>{
    $accionModalConfirmacion=(res)=>{
      if(res)
      {
        $LoginActivo=false;
        $empresaSeleccionada='';
        push('/');
      }
    };
    Util.VerMensajeConfirmacion("Confirme","Estas seguro que deseas salir del sistema?")

  }

  console.log($modulosDisponibles);

</script>

  
{#if $modulosDisponibles != null}
   <!-- content here -->
   <div class={$LoginActivo? "navbar mb-2 bg-base-300 fixed w-full z-50" :"h-0 w-0 hidden"}>
    <div class="flex-1 px-2 lg:flex-none ">
      <button class="text-lg text-gray-500 font-bold">
              {#if $nombreEmpresa.length>0}
                <img src={logoDkolor} alt="imagen" class="bg-navy-600 p-2">
                <!-- {$nombreEmpresa} -->
              {/if}
            </button>
            <span class="bg-gray-700 rounded-none text-white border-none tracking-wider  badge badge-sm absolute top-0 p-2 m-1 left-0">BUILD 0.1</span>
    </div> 
  
    <div class="tabs tabs-boxed bg-base-300">
      <!-- Se debe consultar el rol que contiene este empleado y dependiendo de este llamada las opciones.-->
        {#if $modulosDisponibles.length == Modulos.ModulosActuales.ModuloGeneral} <!-- Este parametro no debe ser calculado por el rol, sino mas bien por el modulo -->
           <!-- El sistema entra aquí en caso de tener el rol de Administrador. -->
           <button 
            class={$location.includes('/nomina') ? "tab tab-lg bg-navy-600 rounded-xl text-white" : "tab tab-lg text-green-600 font-light hover:bg-navy-500 hover:rounded-2xl hover:text-white hover:font-semibold transition-all"} 
            on:click={()=> push('/nomina')}>
              Nomina
          </button>
          <button 
            class={$location.includes('/empleados') ? "tab tab-lg bg-navy-600 rounded-xl text-white" : "tab tab-lg text-green-600 font-light hover:bg-navy-500 hover:rounded-2xl hover:text-white hover:font-semibold transition-all"} 
            on:click={()=> push('/empleados')}>
              Empleados
          </button>
            
        {:else if  $modulosDisponibles[0] == Modulos.ModulosActuales.ModuloNomina && $modulosDisponibles[1] == Modulos.ModulosActuales.ModuloRRHH}
            <!-- Presentara el botón Nomina -->
            <button 
              class={$location.includes('/nomina') ? "tab tab-lg bg-navy-600 rounded-xl text-white" : "tab tab-lg text-green-600 font-light hover:bg-navy-500 hover:rounded-2xl hover:text-white hover:font-semibold transition-all"} 
              on:click={()=> push('/nomina')}>
                Nomina
            </button>
            <button 
            class={$location.includes('/empleados') ? "tab tab-lg bg-navy-600 rounded-xl text-white" : "tab tab-lg text-green-600 font-light hover:bg-navy-500 hover:rounded-2xl hover:text-white hover:font-semibold transition-all"} 
            on:click={()=> push('/empleados')}>
              Empleados
            </button>
        {:else if  $modulosDisponibles[0] == Modulos.ModulosActuales.ModuloRRHH} <!-- Este parametro no debe ser calculado por el rol, sino mas bien por el modulo -->
           <!-- Presentara el botón Empleados -->
            <button 
            class={$location.includes('/empleados') ? "tab tab-lg bg-navy-600 rounded-xl text-white" : "tab tab-lg text-green-600 font-light hover:bg-navy-500 hover:rounded-2xl hover:text-white hover:font-semibold transition-all"} 
            on:click={()=> push('/empleados')}>
              Empleados
            </button>
            
        {:else if  $modulosDisponibles[0] == Modulos.ModulosActuales.ModuloNomina}
          <!-- Presentara el botón Nomina -->
            <button 
            class={$location.includes('/nomina') ? "tab tab-lg bg-navy-600 rounded-xl text-white" : "tab tab-lg text-green-600 font-light hover:bg-navy-500 hover:rounded-2xl hover:text-white hover:font-semibold transition-all"} 
            on:click={()=> push('/nomina')}>
              Nomina
          </button>
        {:else}
           <!-- else content here -->
        {/if}
        <!-- <button class={$location.includes('/configuracion') ?"tab tab-lg bg-red-600 rounded-xl text-white" :"tab tab-lg text-green-600 font-light hover:bg-navy-500 hover:rounded-2xl hover:text-white hover:font-semibold transition-all"} on:click={()=>push('/configuracion')}>Configuracion</button> -->
    </div>
    <div class="flex justify-end flex-1 px-2">
      <div class="dropdown dropdown-hover">
        <div tabindex="0" class="m-1 btn btn-ghost bg-gray-300 hover:bg-navy-500 hover:text-white">{$usuarioActual.nombre}</div> 
        <div tabindex="0" class="p-2  shadow menu dropdown-content bg-navy-100 rounded-sm w-full">
          <button class="text-sm p-1 hover:rounded-sm hover:bg-gray-400 hover:text-white" on:click={()=>$verModalCambioClave=true}>Cambiar Clave</button>
        </div>
      </div>
      <button class="btn btn-square btn-ghost bg-gray-300 hover:bg-red-600 hover:text-white p-2 text-red-600 font-extrabold" on:click={()=> salirSistema()}>
        SALIR
      </button>
    </div>
  </div>
{:else}
   <!-- else content here -->
{/if}