<script lang="ts">

  import {location} from 'svelte-spa-router'
  import { LoginActivo,empresaSeleccionada, listadoEmpresas,nombreEmpresa,usuarioActual,verModalCambioClave 
  ,verModalConfirmacion,accionModalConfirmacion, cedulaUsuario, rolUsuarioId, rutas, accionesAplicables} from '../Modelo/Datos';

  let opcionActiva:string="bg-navy-500 font-medium";

  console.log(`id ruta: ${$rutas[0].id} --rutas ${$rutas[0].ruta} --permisos ruta: ${$rutas[0].permisosPorRolId}`);
  console.log(`id ruta: ${JSON.stringify($rutas[0])}`);
</script>

<ul class="menu w-64 border bg-base-200 menu-compact lg:menu-normal rounded-box m-4">
  {#each $rutas as item}
     <!-- content here -->
     {#if item.moduloId == 1}
        <!-- content here -->
          <li>
            <a href={`/#${item.ruta}`} class={$location.includes( item.ruta ) ? opcionActiva :""}>
              {item.nombre}
            </a>
          </li>
     {:else}
        <!-- else content here -->
     {/if}
  {/each}
</ul>


<!-- <ul class="menu w-64 border bg-base-200 menu-compact lg:menu-normal rounded-box m-4">
  <li>
    <a href="/#/nomina/crear" class={$location.includes('crear') ? opcionActiva :""}>
        Crear Nomina
    </a>
  </li>
    
  <li>
    <a href="/#/nomina/Novedades" class={$location.includes('nomina/Novedades') ? opcionActiva :""}>
        Novedades Nomina
    </a>
    <ul class="menu">
      <li>
        <a href="/#/nomina/novedades/pagoHorasExtras" class={$location.includes('pagoHorasExtras') ? opcionActiva :""}>
          Pago Horas Extras
        </a>
      </li>
      <li>
        <a href="/#/nomina/novedades/pagoVacaciones" class={$location.includes('pagoVacaciones') ? opcionActiva :""}>
          Pago Vacaciones
        </a>
      </li>
      <li>
        <a href="/#/nomina/novedades/descuentoDiaTrabajo" class={$location.includes('descuentoDiaTrabajo') ? opcionActiva :""}>
          Descuento Dia Trabajo
        </a>
      </li>
    </ul>
  </li>

    <li>
      <a href="/#/nomina/consultar" class={$location.includes('/nomina/consultar') ? opcionActiva :""}>
            Consultar Nomina
          </a>
    </li>

    <li>
      <a href="/#/nomina/generarNominaPrevia" class={$location.includes('nomina/generarNominaPrevia') ? opcionActiva :""}>
        Generar Nomina Previa
      </a>
    </li>

    <li>
      <a href="/#/nomina/generarNominaDefinitiva" class={$location.includes('nomina/generarNominaDefinitiva') ? opcionActiva :""}>
        Generar Nomina Definitiva
      </a>
    </li>

    <li>
      <a href="/#/nomina/procesoPagoNomina" class={$location.includes('nomina/procesoPagoNomina') ? opcionActiva :""}>
        Proceso Pago Nomina
      </a>
    </li>
</ul> -->
