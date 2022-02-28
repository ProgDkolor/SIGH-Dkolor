<script lang="ts">
    import {
        bolantePago,
        verModalBolantePago,
        tituloModalComprobantePago
    } from '../Modelo/Datos'
    import { Circle2 } from 'svelte-loading-spinners'
    const formateoFecha=(fecha:string)=>{
      let f:string[]=fecha.split('-');
      return `${f[2].substring(0,2)}-${f[1]}-${f[0]}`;
    }
</script>

    <div class={$verModalBolantePago ?"modal modal-open w-screen mx-auto h-screen m-0 p-0 " : "modal w-screen h-screen m-0 p-0"} >
      <div class=" p-0 rounded-t-2xl w-[80%] h-full overflow-y-auto">

        <article class="bg-white rounded-b-2xl rounded-t-2xl p-0">
          <div class="bg-navy-700 rounded-t-2xl p-1 relative">
              <p class="py-1 text-sm font-medium leading-6 tracking-wide text-center text-white rounded-lg md:text-base">
                {$tituloModalComprobantePago}
              </p> 
          </div>
          <button on:click={()=> $verModalBolantePago=false}  class="btn absolute -top-1 mr-[10%] right-0 bg-transparent hover:bg-transparent hover:text-red-600 border-none text-2xl pr-5 pt-0">X</button>
          {#if $bolantePago.length>0}
          <section class="mt-1">
            <h1 class="text-lg text-center bg-base-300 p-2 my-2">{$bolantePago[0].descripcionNomina}</h1>

            <div class="form-control">
              <div class="input-group input-group-sm">
                <span class="text-lg font-semibold w-[500px]">Cedula</span>
                <input class="input input-bordered input-sm w-full text-lg text-gray-500" value={ $bolantePago[0].cedula}  name="totalSueldoBruto" readonly/>
                <span class="text-lg font-semibold w-[500px]">Fecha Entrada</span>
                <input class="input input-bordered input-sm w-full text-lg text-gray-500" value={formateoFecha($bolantePago[0].fechaEntrada)}  name="totalSueldoNeto" readonly/>
              </div>
            </div>

            <div class="form-control">
                <div class="input-group input-group-sm">
                  <span class="text-lg font-semibold w-[500px]">Nombre</span>
                  <input class="input input-bordered input-sm w-full text-lg text-gray-500" value={ $bolantePago[0].nombre}  name="totalSueldoBruto" readonly/>
                  <span class="text-lg font-semibold w-[500px]">Cargo</span>
                  <input class="input input-bordered input-sm w-full text-lg text-gray-500" value={$bolantePago[0].descripcionCargo}  name="totalSueldoNeto" readonly/>
                </div>
              </div>

              <div class="form-control">
                <div class="input-group input-group-sm">
                  <span class="text-lg font-semibold w-[500px]">Apellido</span>
                  <input class="input input-bordered input-sm w-full text-lg text-gray-500" value={ $bolantePago[0].apellido}  name="totalSueldoBruto" readonly/>
                  <span class="text-lg font-semibold w-[500px]">Salario Mensual</span>
                  <input class="input input-bordered input-sm w-full text-lg text-gray-500" value={$bolantePago[0].salarioMensual.toLocaleString('es-DO',{minimumFractionDigits: 2})}  name="totalSueldoNeto" readonly/>
                </div>
              </div>

              <h1 class="text-lg text-center bg-base-300 p-2 my-2">DETALLE DE NOMINA</h1>
          </section>
          {/if}
          <section class="px-2 pt-0 pb-2">
            {#if $bolantePago.length>0}
            <section class="w-full">
            <div class="overflow-x-auto">
              <table class="table w-full table-compact mt-[15px]">
                <thead>
                  <tr>
                    <td></td>
                    <th>Concepto</th> 
                    <th class="text-right">Ingreso</th> 
                    <th class="text-right">Descuento<th> 
                  </tr>
                </thead> 
                <tbody>
                  {#each $bolantePago as n}
                  <tr>
                    <td></td>
                    <td>{@html n.descripcionConcepto}</td> 
                    <td class="text-right">{n.ingreso===0?'':n.ingreso.toLocaleString('es-DO',{minimumFractionDigits: 2})}</td> 
                    <td class="text-right">{n.descuento===0?'':n.descuento.toLocaleString('es-DO',{minimumFractionDigits: 2})}<td> 
                  
                  </tr>
                  {/each}
                </tbody> 
                <tfoot>
                  <tr>
                    <th></th> 
                    <th class="text-center">TOTALES</th> 
                    <th class="text-right">{$bolantePago.reduce((acumulado,elemento)=>acumulado+elemento.ingreso,0).toLocaleString('es-DO',{minimumFractionDigits: 2})}</th> 
                    <th class="text-right">{$bolantePago.reduce((acumulado,elemento)=>acumulado+elemento.descuento,0).toLocaleString('es-DO',{minimumFractionDigits: 2})}</th> 
                  </tr>
                  <tr>
                    <th></th> 
                    <th></th> 
                    <th class="text-right text-lg">MONTO NETO A COBRAR</th> 
                    <th class="text-right bg-gray-300 text-lg">{$bolantePago.reduce((acumulado,elemento)=>acumulado+elemento.ingreso+elemento.descuento,0).toLocaleString('es-DO',{minimumFractionDigits: 2})}</th> 
                  </tr>
                  <tr>
                    <th></th>
                    <th class="text-lg bg-gray-300">MEDIO DE PAGO</th> 
                    <th colspan="2" class="text-lg text-center">{$bolantePago[0].descripcionMedioPago}</th> 
                  </tr>
                </tfoot>
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
