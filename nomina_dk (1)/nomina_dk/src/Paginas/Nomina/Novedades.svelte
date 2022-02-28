<script lang="ts">
    import {location} from 'svelte-spa-router'
    import TituloPagina from "../../Componentes/TituloPagina.svelte";
    import {accionesAplicables} from '../../Modelo/Datos';


    let opcionActiva:string="bg-navy-500 font-medium";
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
   <TituloPagina Titulo="NOVEDADES NOMINA"></TituloPagina>
    

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
{:else if  permisosCrud.c == 1 || permisosCrud.r == 1}
   <!-- else if content here -->
   <TituloPagina Titulo="NOVEDADES NOMINA"></TituloPagina>
    

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
{:else}
   <!-- else content here -->
   <p>
     No posee permisos
   </p>
{/if}

  