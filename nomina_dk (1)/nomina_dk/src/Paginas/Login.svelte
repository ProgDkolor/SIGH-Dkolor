<script lang="ts">
    import { LoginActivo} from '../Modelo/Datos';
    import {verModalLoading,listadoEmpresas,empresaSeleccionada,RespuestaUsuarios,EEmpresa,nombreEmpresa
    ,usuarioActual,cedulaUsuario, rolUsuarioId, tokenEmpleado, token, rutas, modulosDisponibles, accionesAplicables} from '../Modelo/Datos';
    import { onMount } from 'svelte';
    import {push, pop, replace} from 'svelte-spa-router'
    import * as Servicios from '../Servicios/AccesoApi';
    import * as ServicioLogin from '../Servicios/LoginEmpleado';
    import * as RutasPorModulos from '../Servicios/RutasPorModulos';
    import type * as Tipos from '../Modelo/Tipos'
    import * as Util from '../Servicios/Utilidades'
    import {fade} from 'svelte/transition'
    import LabelHorizontal from '../Componentes/LabelHorizontal.svelte';
    import imgS from '../assets/svelte.png';
    import logoDkolor from '../assets/img/LogoDkolor-white.png';
     
      let tUsuario;
      let tClave;
      let tEmpresa;
      let Usuario:string='';
      let Clave:string ='';
      let EUsuario:boolean=false;
      let EClave:boolean=false;
      let empresaActual:number=-1;

      onMount(
        ()=>{
            console.log('voy a buscar los datos');
            cargaEmpresas();
            tUsuario.focus();
        }
    );
    const validarUsuario=()=>{
      if(Usuario==="")
      {
        EUsuario=true;
      }else{
        EUsuario=false;
      }
    }
    const validarClave=()=>{
      if(Clave==="")
      {
        EClave=true;
      }else{
        EClave=false;
      }
    }
    const validarEmpresa=()=>{
      if(empresaActual===-1)
      {
        $EEmpresa=true;
      }else{
        $EEmpresa=false;
      }
    }
    const loginUsuario=async()=>{
      validarUsuario();
      validarClave();
      validarEmpresa();
          if(!EUsuario && !EClave && !$EEmpresa){
                let datos:Tipos.RData={
                  empresa:empresaActual,
                  sp: "Login",
                  modelo: "dynamic",
                  dataParametros: [
                    {
                      parametro: "@usuario",
                      tipo: 16,
                      valor: Usuario
                    },
                    {
                      parametro: "@clave",
                      tipo: 16,
                      valor: Clave
                    }
                  ]
                }
                $verModalLoading=true;
                let res=await Servicios.ConsultaBdMaster(datos);
                $verModalLoading=false;
                if(res.exito)
                {
                  $RespuestaUsuarios=res.data;
                  if($RespuestaUsuarios.length>0)
                  {
                    if($RespuestaUsuarios[0].resultado>0)
                    {
                      Util.VerMensaje("",$RespuestaUsuarios[0].mensaje);
                    }
                    else
                    {
                      console.log("Exito");
                      $LoginActivo=true;
                      $empresaSeleccionada=empresaActual.toString();
                      $usuarioActual.usuario=$RespuestaUsuarios[0].usuario;
                      $cedulaUsuario=$RespuestaUsuarios[0].cedula;
                      $rolUsuarioId = $RespuestaUsuarios[0].rolUsuarioId;
                      $usuarioActual.nombre=`${$RespuestaUsuarios[0].nombre} ${$RespuestaUsuarios[0].apellido} `;
                      $nombreEmpresa=$listadoEmpresas.find(e=>e.id===parseInt($empresaSeleccionada)).nombre;
                      // En esta sección se debo pasar el token en una variable reactiva para consultarla en cada ventana.
                      $token = await ServicioLogin.GetToken($RespuestaUsuarios[0].id, $RespuestaUsuarios[0].usuario,
                                                          $RespuestaUsuarios[0].cargoEmpleadosId, $RespuestaUsuarios[0].rolUsuarioId);
                      $rutas = await RutasPorModulos.BuscarRutasDelModuloEmpleado
                                                    ($RespuestaUsuarios[0].rolUsuarioId, $RespuestaUsuarios[0].id);
                                                    
                      $modulosDisponibles = await RutasPorModulos.BuscarModulosDisponiblesParaElEmpleado
                                                   ( $RespuestaUsuarios[0].rolUsuarioId, $RespuestaUsuarios[0].id );
                      //...BuscarLasAccionesAplicables
                      $accionesAplicables = await RutasPorModulos.BuscarLasAccionesAplicables($RespuestaUsuarios[0].rolUsuarioId, $RespuestaUsuarios[0].id);
                      replace('/inicio');
                      console.log($rutas);
                      console.log($modulosDisponibles);
                      console.log($accionesAplicables);
                    }
                  }else{
                    Util.VerMensaje("Ocurrio un error",res.mensaje);
                  }
                }else
                {
                  console.log("error");
                }
                console.log(res);	
          }
      }
    const cargaEmpresas=async ()=>{
       
          $verModalLoading=true;
          let res=await Servicios.ListadoEmpresas();
          $verModalLoading=false;
          if(res.exito)
          {
              $listadoEmpresas=res.data;
              if($listadoEmpresas.length==0)
              {
                  Util.VerMensaje("Atencion","No se encontraron empresas registradas.");
              }
              if($listadoEmpresas.length===1)
              {
                empresaActual=$listadoEmpresas[0].id;
              }
             console.log("Exito");
             console.log($listadoEmpresas);
          }else
          {
              console.log("error");
  
          }
          console.log(res);	
        
    };

    const OnEnterUsuario = e => {
    if (e.charCode === 13) 
        if(Usuario.length>0)
            tClave.focus();  
  };
  const OnEnterClave = e => {
    if (e.charCode === 13) 
        if(empresaActual!==-1)
          loginUsuario();  
  };


</script>

<div class="hero min-h-screen bg-base-200">
  <div class="flex ">
   
    <div class="card  w-full max-w-[1000px] shadow-2xl bg-base-100">
      <div class="card-body w-full">

        <div class="form-control">
          <div class="form-group">
            <img src={logoDkolor} alt="imagen" class="bg-navy-600 p-5">
          </div>
          <div class="label">
            <span class="label-text">Usuario</span>
            {#if EUsuario}
            <span class="text-md text-red-400 text-right">*Requerido</span>
            {/if}
          </div> 
          <input type="text" on:keypress={OnEnterUsuario} 
          bind:this={tUsuario} on:change={()=>validarUsuario()} 
          bind:value={Usuario} placeholder="" 
                   class="input input-bordered ">

        </div> 
        <div class="form-control">
          <div class="label">
            <span class="label-text">Clave</span>
            {#if EClave}
            <span class="text-md text-red-400 text-right">*Requerido</span>
            {/if}
          </div> 
          <input type="password" on:keypress={OnEnterClave} on:change={()=>validarClave()} bind:this={tClave} bind:value={Clave} placeholder="" class="input input-bordered"> 
        </div> 
          
        <div class="form-control pt-3">
          <LabelHorizontal>Empresa</LabelHorizontal>
          {#each $listadoEmpresas as l}
          <label class="cursor-pointer label">
              <span class="label-text text-gray-800 font-semibold">{l.nombre}</span> 
              <input type="radio" bind:group={empresaActual}   name="cNominaPrevia" checked={false} class="radio" value={l.id}>
            </label>
          {/each}
        </div> 
        <!-- <div class="form-control">
          <div class="label">
            <span class="label-text">Empresa</span>
            {#if $EEmpresa}
            <span class="text-md text-red-400 text-right">*Requerido</span>
            {/if}
          </div> 
          <select bind:value={$empresaSeleccionada} bind:this={tEmpresa} on:change={()=>validarEmpresa()}  name="cEmpresa"  class="select select-bordered font-normal text-lg text-gray-500">
            <option disabled={true} selected={true}></option> 
            {#each $listadoEmpresas as t }
              <option value={t.id}>{t.nombre}</option> 
            {/each}
            </select>
        </div>  -->



        <div class="form-control mt-6">
            <button on:click={()=>loginUsuario()} class="btn btn-primary">Iniciar</button>
        </div>
      </div>
    </div>
  </div>
</div>