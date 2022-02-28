

<script lang="ts">
    import {
        verModalConfirmacion,
        accionModalConfirmacion,
        mensajeModalConfirmacion,
        tituloModalConfirmacion
    } from '../Modelo/Datos'
    
    const respuestaModal=(resp:boolean)=>{
        $accionModalConfirmacion(resp);
        $verModalConfirmacion=false;
    }
</script>

<main class={$verModalConfirmacion ?"modal z-[99999] bg-gray-200 bg-opacity-75 modal-open w-screen mx-auto h-screen m-0 p-0 " : "modal w-screen h-screen m-0 p-0"} >
    <article class="p-0 rounded-t-2xl w-[80%] max-w-[600px] max-h-[300px] m-0 overflow-y-auto rounded-b-xl bg-white">
        <section class="bg-navy-700 rounded-t-2xl p-1 relative">
            <p class="py-1 text-sm font-medium leading-6 tracking-wide text-center text-white rounded-lg md:text-base">
              {$tituloModalConfirmacion}
            </p> 
        </section>

            <section class="px-3 py-10 mx-[5%]">
                <p class="text-md text-navy-600 text-center">{$mensajeModalConfirmacion}</p>

            </section>
            <section class="flex justify-end p-0 bg-green-400 rounded-b-xl">
                <button on:click={()=>respuestaModal(false)} class="btn bg-navy-700 border-none  m-1 px-3 py-0">N O</button>
                <button on:click={()=>respuestaModal(true)} class="btn bg-red-600 border-none m-1 px-3 py-0">S I</button>
            </section>
    </article>
</main>
