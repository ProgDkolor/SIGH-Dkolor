<script lang="ts">
  import {location} from 'svelte-spa-router'
  import { LoginActivo,empresaSeleccionada, listadoEmpresas,nombreEmpresa,usuarioActual,verModalCambioClave 
  ,verModalConfirmacion,accionModalConfirmacion, cedulaUsuario, rolUsuarioId, rutas, accionesAplicables} from '../Modelo/Datos';

  let opcionActiva:string="bg-navy-500 font-medium";

  // console.log(`id ruta: ${$rutas[0].id} --rutas ${$rutas[0].ruta} --permisos ruta: ${$rutas[0].permisosPorRolId}`);
  // console.log(`id ruta: ${JSON.stringify($rutas[0])}`);
</script>

<ul class="menu w-64 border bg-base-200 menu-compact lg:menu-normal rounded-box m-4">
  {#each $rutas as item}
  <!-- content here -->
    {#if item.moduloId == 2}
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
    <li>
      <a href={`/#/empleados/GestionarUsuario`} class={$location.includes( '/empleados/GestionarUsuario' ) ? opcionActiva :""}>
          Gestionar Usuarios
      </a>
    </li>
    <li>
      <a href={`/#/empleados/GestionarAccionesDePersonal`} class={$location.includes( '/empleados/GestionarAccionesDePersonal' ) ? opcionActiva :""}>
          Acciones de Personal
      </a>
    </li>
    <li>
      <a href={`/#/empleados/GestionarCargaDeDocumentos`} class={$location.includes( '/empleados/GestionarCargaDeDocumentos' ) ? opcionActiva :""}>
        Cargar Documento
      </a>
    </li>
  </ul>
  

  <!-- <ul class="menu w-64 border bg-base-200 menu-compact lg:menu-normal rounded-box m-4">
    <li>
        <a href="/#/empleados/CrearEmpleado" class={$location.includes('empleados/CrearEmpleado') ? opcionActiva :""}>
            Crear Empleado
          </a>
    </li> 
    <li>
      <a href="/#/empleados/ConsultarEmpleado" class={$location.includes('empleados/ConsultarEmpleado') ? opcionActiva :""}>
            Consultar Empleado
          </a>
    </li> 
    <li>
      <a href="/#/empleados/AsignarBeneficio" class={$location.includes('empleados/AsignarBeneficio') ? opcionActiva :""}>
            Asignar Beneficio
          </a>
    </li>
    <li>
        <a href="/#/empleados/AsignarDescuento" class={$location.includes('empleados/AsignarDescuento') ? opcionActiva :""}>
              Asignar Descuento
            </a>
      </li>
      <li>
        <a href="/#/empleados/ComprobantePago" class={$location==='/empleados/ComprobantePago' ? opcionActiva :""}>
              Comprobante De Pago
            </a>
      </li>
      <li>
        <a href="/#/empleados/ComprobantePagoNominaPrevia" class={$location==='/empleados/ComprobantePagoNominaPrevia' ? opcionActiva :""}>
            Comprobante De Pago Nomina Previa
            </a>
      </li>
      <li>
        <a href="/#/empleados/ListarEmpleados" class={$location==='/empleados/ListarEmpleados' ? opcionActiva :""}>
            Listado Empleado
            </a>
      </li>
  </ul> -->

