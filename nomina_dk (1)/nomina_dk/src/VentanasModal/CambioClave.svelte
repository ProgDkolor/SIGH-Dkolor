

<script lang="ts">
    import {
        verModalCambioClave,
        verModalLoading,
        usuarioActual,
        verModalConfirmacion,
        accionModalConfirmacion,
        accionModalMensaje


    } from '../Modelo/Datos'
    import LabelHorizontal from '../Componentes/LabelHorizontal.svelte';
    import type * as Tipos from '../Modelo/Tipos'
    import * as Servicios from '../Servicios/AccesoApi'
    import * as Util from '../Servicios/Utilidades'
    let fCambioClave:Tipos.DCambioClave={
        claveActual: "",
    claveNueva: "",
    confirmacionClave: "",
    };
    const limpiar=()=>{
        fCambioClave={
        claveActual: "",
    claveNueva: "",
    confirmacionClave: "",
    };
    }
    let eCambioClave:Tipos.ECambioClave={
        claveActual: false,
        claveNueva: false,
        confirmacionClave: false,
        claveCoincide:false
    }
    const validaClaveActual=()=>{
        if(fCambioClave.claveActual==="")
            eCambioClave.claveActual=true;
        else
            eCambioClave.claveActual=false;    
    }
    const validaClaveNueva=()=>{
        if(fCambioClave.claveNueva==="")
            eCambioClave.claveNueva=true;
        else
            eCambioClave.claveNueva=false;    
    }
    const validaConfirmacionClave=()=>{
        if(fCambioClave.confirmacionClave==="")
            eCambioClave.confirmacionClave=true;
        else
            eCambioClave.confirmacionClave=false;    
    }
    const validaClaveCoincide=()=>{
        if(fCambioClave.confirmacionClave.length>0 && fCambioClave.claveNueva.length>0)
            if(fCambioClave.claveNueva!==fCambioClave.confirmacionClave)
                eCambioClave.claveCoincide=true;
            else
                eCambioClave.claveCoincide=false;    
    }
    const cambioClave=async(procede:boolean)=>{
        if(procede)
        {
        let datos:Tipos.RData={
                  empresa: 1,
                  sp: "ActualizaClaveUsuario",
                  modelo: "dynamic",
                  dataParametros: [
                    {
                      parametro: "@usuario",
                      tipo: 16,
                      valor: $usuarioActual.usuario
                    },
                    {
                      parametro: "@claveActual",
                      tipo: 16,
                      valor: fCambioClave.claveActual
                    },
                    {
                      parametro: "@claveNueva",
                      tipo: 16,
                      valor: fCambioClave.claveNueva
                    }
                  ]
                }
                $verModalLoading=true;
                let res=await Servicios.RegistroBdMaster(datos);
                $verModalLoading=false;
                    if(res.exito)
                    {
                        let resCambioClave:Tipos.RDataRegistro[]=res.data;
                        if(resCambioClave.length>0)
                        {
                            if(resCambioClave[0].resultado===0)
                            {
                                limpiar();
                                $accionModalMensaje=()=>{$verModalCambioClave=false};
                                Util.VerMensaje("Atencion","Clave actualizada con exito.");   
                            }else
                            {
                                $accionModalMensaje=null;
                                Util.VerMensaje("Error Encontrado",resCambioClave[0].mensaje);    
                            }
                        }
                    }else
                    {
                        $accionModalMensaje=null;
                        Util.VerMensaje("Error Encontrado",res.mensaje);
            
                    }
                    console.log(res);	
                }
    }
    const cambiarClave=()=>{
            validaClaveActual();
            validaClaveNueva();
            validaConfirmacionClave();
            validaClaveCoincide();
            if(!eCambioClave.claveActual &&!eCambioClave.claveCoincide &&!eCambioClave.claveNueva && !eCambioClave.confirmacionClave)
            {
                $verModalConfirmacion=true;
                $accionModalConfirmacion=cambioClave;  
            }
    }
</script>

<main class={$verModalCambioClave ?"modal modal-open w-screen mx-auto h-screen m-0 p-0 " : "modal w-screen h-screen m-0 p-0"} >
    <article class="p-0 rounded-t-2xl w-[80%] max-w-[600px] max-h-[300px] m-0 overflow-y-auto rounded-b-xl bg-white">
        <section class="bg-navy-700 rounded-t-2xl p-1 relative">
            <p class="py-1 text-sm font-medium leading-6 tracking-wide text-center text-white rounded-lg md:text-base">
              CAMBIO DE PASSWORD
            </p> 
        </section>

            <section class="p-3 mx-[5%]">

                <div class="form-control mb-1">
                    <div class="input-group relative">
                    <LabelHorizontal>Clave Actual</LabelHorizontal>
                    <input 
                        type="password"
                        bind:value={fCambioClave.claveActual}
                        on:keyup={validaClaveActual}
                        class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tClaveActual" autocomplete="off"/>
                        {#if eCambioClave.claveActual}
                        <span class="text-xs text-red-400 absolute top-0 left-0">*Campo Requerido</span>
                        {/if}
                    </div>
                </div>

                <div class="form-control mb-1">
                    <div class="input-group relative">
                    <LabelHorizontal>Clave Nueva</LabelHorizontal>
                    <input 
                        type="password"
                        bind:value={fCambioClave.claveNueva}
                        on:keyup={validaClaveNueva}
                        class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tClaveNueva" autocomplete="off"/>
                        {#if eCambioClave.claveNueva}
                        <span class="text-xs text-red-400 absolute top-0 left-0">*Campo Requerido</span>
                        {/if}
                    </div>
                </div>

                <div class="form-control mb-1">
                    <div class="input-group relative">
                    <LabelHorizontal>Confirme Clave </LabelHorizontal>
                    <input 
                        type="password"
                        bind:value={fCambioClave.confirmacionClave}
                        on:keyup={validaConfirmacionClave}
                        class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tConfirmaClave" autocomplete="off"/>
                        {#if eCambioClave.confirmacionClave}
                            <span class="text-xs text-red-400 absolute top-0 left-0">*Campo Requerido</span>
                            {:else if eCambioClave.claveCoincide}
                            <span class="text-xs text-red-400 absolute top-0 left-0">*Claves no coinciden</span>
                        {/if}
                    </div>
                </div>

                </section>
            <section class="flex justify-end p-0 bg-green-400 rounded-b-xl">
                <button on:click={()=>$verModalCambioClave=false} class="btn bg-navy-700 border-none m-1 px-2 py-0">Cerrar</button>
                <button on:click={cambiarClave} class="btn bg-red-600 border-none m-1 px-2 py-0">CAMBIAR CLAVE</button>
            </section>
    </article>
</main>
