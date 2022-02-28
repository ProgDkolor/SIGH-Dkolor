

<script lang="ts">
    import {
        verModalNominaPrevia,
        verModalLoading,
        nominaPrevia,
        verModalMensaje,
        mensajeModal,
        tituloModal
    } from '../Modelo/Datos'
    import { Circle2 } from 'svelte-loading-spinners'
    const formateoFecha=(fecha:string)=>{
      let f:string[]=fecha.split('-');
      return `${f[2].substring(0,2)}-${f[1]}-${f[0]}`;
    }
</script>
<!-- MODAL NOMINA PREVIA -->
    <div class={$verModalNominaPrevia ?"modal modal-open w-screen h-screen m-0 p-0" : "modal w-screen h-screen m-0 p-0"} >
      <div class=" p-0 rounded-t-2xl w-full h-full">

        <article class="bg-white rounded-b-2xl rounded-t-2xl p-0">
          <div class="bg-navy-700 rounded-t-2xl p-1 relative">
              <p class="py-1 text-sm font-medium leading-6 tracking-wide text-center text-white rounded-lg md:text-base">
                NOMINA PREVIA
              </p> 
          </div>
          <button on:click={()=> $verModalNominaPrevia=false}  class="btn absolute -top-1 right-0 bg-transparent hover:bg-transparent hover:text-red-600 border-none text-2xl pr-5 pt-0">X</button>
          {#if $nominaPrevia.length>0}
          <section class="mt-1">
            <div class="form-control">
              <div class="input-group input-group-sm">
                <span class="text-lg font-semibold w-[500px]">Fecha Registro</span>
                <input class="input input-bordered input-sm w-full text-lg text-gray-500" value={ formateoFecha($nominaPrevia[0].fecha)}  name="periodoNomina" readonly/>
                <span class="text-lg font-semibold w-[500px]">Descripcion</span>
                <input class="input input-bordered input-sm w-full text-lg text-gray-500" value={$nominaPrevia[0].descripcion}  name="descripcionNomina" readonly/>
              </div>
            </div>

            <div class="form-control">
              <div class="input-group input-group-sm">
                <span class="text-lg font-semibold w-[500px]">Total Suelto Bruto</span>
                <input class="input input-bordered input-sm w-full text-lg text-gray-500" value={ $nominaPrevia.reduce((acumulado,elemento)=>acumulado+elemento.sueldoQuincenal,0).toLocaleString('es-DO',{minimumFractionDigits: 2})}  name="totalSueldoBruto" readonly/>
                <span class="text-lg font-semibold w-[500px]">Total Sueldo Neto</span>
                <input class="input input-bordered input-sm w-full text-lg text-gray-500" value={$nominaPrevia.reduce((acumulado,elemento)=>acumulado+elemento.sueldoNeto,0).toLocaleString('es-DO',{minimumFractionDigits: 2})}  name="totalSueldoNeto" readonly/>
              </div>
            </div>
          </section>
          {/if}
          <section class="px-2 pt-0 pb-2">
            {#if $nominaPrevia.length>0}
            <section class="w-full">
            <div class="overflow-x-auto overflow-y-auto h-[75vh]">
              <table class="table w-full table-compact mt-[15px]">
                <thead>
                  <tr>
                    <td></td>
                    <th>Modulo</th> 
                    <th>Dependencia</th> 
                    <th>Cedula<th> 
                    <th>Nombre</th> 
                    <th>Apellido</th> 
                    <th>Cargo</th> 
                    <th>Cuenta Bancaria</th>
                    <th>Fecha Ingreso</th> 
                    <th>Sueldo Mensual</th> 
                    <th>Sueldo Quincenal</th> 
                    <th>Efectivo</th>
                    <th>Tranferencia</th> 
                    <th>Cheque</th> 
                    <th>Dieta</th> 
                    <th>Horas Extras</th>
                    <th>ISR</th>
                    <th>AFP</th>
                    <th>ARS</th>
                    <th>Sueldo Neto</th>
                  </tr>
                </thead> 
                <tbody class="">
                  {#each $nominaPrevia as n}
                  <tr class="hover">
                    <td></td>
                    <td>{n.modulo}</td> 
                    <td>{n.dependencia}</td> 
                    <td>{n.cedula}<td> 
                    <td>{n.nombre}</td> 
                    <td>{n.apellidos}</td> 
                    <td>{n.cargoEmpleado}</td> 
                    <td>{n.cuentaBanco}</td>
                    <td>{formateoFecha(n.fechaIngreso)}</td> 
                    <td class="text-right">{n.sueldo.toLocaleString('es-DO',{minimumFractionDigits: 2})}</td> 
                    <td class="text-right">{n.sueldoQuincenal.toLocaleString('es-DO',{minimumFractionDigits: 2})}</td> 
                    <td class="text-right">{n.pagoEfectivo.toLocaleString('es-DO',{minimumFractionDigits: 2})}</td>
                    <td class="text-right">{n.transferencia.toLocaleString('es-DO',{minimumFractionDigits: 2})}</td> 
                    <td class="text-right">{n.cheque.toLocaleString('es-DO',{minimumFractionDigits: 2})}</td> 
                    <td class="text-right">{n.dietas.toLocaleString('es-DO',{minimumFractionDigits: 2})}</td> 
                    <td class="text-right">{n.horasExtras.toLocaleString('es-DO',{minimumFractionDigits: 2})}</td>
                    <td class="text-right">{n.isr.toLocaleString('es-DO',{minimumFractionDigits: 2})}</td>
                    <td class="text-right">{n.afp.toLocaleString('es-DO',{minimumFractionDigits: 2})}</td>
                    <td class="text-right">{n.tsS_SFS.toLocaleString('es-DO',{minimumFractionDigits: 2})}</td>
                    <td class="text-right">{n.sueldoNeto.toLocaleString('es-DO',{minimumFractionDigits: 2})}</td>
                  
                  </tr>
                  {/each}
                </tbody> 
                <!-- <tfoot>
                  <tr>
                    <th></th> 
                    <th>Name</th> 
                    <th>Job</th> 
                    <th>company</th> 
                    <th>location</th> 
                    <th>Last Login</th> 
                    <th>Favorite Color</th>
                  </tr>
                </tfoot> -->
              </table>
            </div>
          </section>
            {/if}
            
            </section>  
            <section class="flex flex-row-reverse py-2 bg-navy-700 rounded-b-2xl pr-2">
              <!-- <button on:click={()=> $verModalNominaPrevia=false}  class="btn btn-md mx-1 bg-green-700 font-bold hover:bg-white hover:text-green-700">Entendido</button> -->
          </section>

          </article>
      </div>
    </div>

    <!-- VENTANA LOADING -->

    <div class={$verModalLoading ?"modal modal-open w-screen h-screen m-0 p-0" : "modal w-screen h-screen m-0 p-0"} >
      <div class="">
        
        <section class="">
          <Circle2 size="350" unit="px"></Circle2>  
        </section>
        <section class="p-3 mt-2">
          <h1 class="text-lg font-medium text-center">Cargando Datos</h1>
          <p class="text-md text-gray-600 text-center">Por favor espere...</p>
        </section>
      </div>    
    </div>


    <!-- MODAL MENSAJES AVISO -->
    



    
        