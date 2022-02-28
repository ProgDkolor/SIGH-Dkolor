
<script lang="ts">
    import {
        verModalMensaje,
        mensajeModal,
        tituloModal,
        accionModalMensaje
    } from '../Modelo/Datos'
    const respuestaModal=()=>{
        if($accionModalMensaje!==null)
        {
            $accionModalMensaje();
        }
        $verModalMensaje=false;
    }
</script>

<div class={$verModalMensaje ?"modal bg-transparent z-[99999] modal-open" : "modal"} >
    <div class="p-0 modal-box rounded-t-2xl">

      <article class="bg-white rounded-b-2xl rounded-t-2xl p-0">
        <div class="bg-base-300 rounded-t-2xl p-1 relative">
            <p class="py-1 text-sm font-medium leading-6 tracking-wide text-center rounded-lg md:text-base">
              {$tituloModal}
            </p> 
        </div>
        <section class="p-5 text-center">
          {$mensajeModal}
        </section>
        <button on:click={()=> $verModalMensaje=false}  class="btn absolute -top-1 right-0 bg-transparent hover:bg-transparent hover:text-red-600 border-none text-2xl pr-5 pt-0">X</button>
       
        <section class="px-2 pt-0 pb-2">
          
          
          </section>  
          <section class="flex flex-row-reverse py-2 bg-base-300 rounded-b-2xl pr-2">
            <button on:click={()=> respuestaModal()}  class="btn btn-sm mx-1 bg-green-700 font-bold border-none hover:bg-white hover:text-green-700">Entendido</button>
        </section>

        </article>
    </div>
  </div>