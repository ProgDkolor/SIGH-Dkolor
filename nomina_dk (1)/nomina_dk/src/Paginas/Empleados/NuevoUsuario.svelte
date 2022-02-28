
<script lang="ts">
    import { writable } from "svelte/store";
    import * as usuariosService from '../../Servicios/UsuarioService';
    
	import { MaskedInput } from 'svelte-imask';
    import LabelHorizontal from "../../Componentes/LabelHorizontal.svelte";
    import TituloPagina from "../../Componentes/TituloPagina.svelte";
    import {usuarios} from '../../Modelo/Datos';
    import * as empleadoService from "../../Servicios/EmpleadoService";
    import * as usuariosServices from "../../Servicios/UsuarioService";
    import swal from 'sweetalert';
    export const camposRequeridos = writable<any>();

    const mascaraCedula = {
          mask: '000-0000000-0'
    };

    let resp:boolean = true;

    let objUsuarios = {
        "Usuario": "",
        "Nombre": "",
        "Apellido": "",
        "PrimerApellido": "",
        "SegundoApellido": "",
        "Clave": "",
        "perfil": 0,
        "Fecha": "",
        "Estado": 0,
        "Cedula": "",
        "Modelo": 0,
        "CargoEmpleadosId": 0,
        "RolUsuarioId": 0
    };

    const limpiarFormularios = () => {
        objUsuarios = {
            "Usuario": "",
            "Nombre": "",
            "Apellido": "",
            "PrimerApellido": "",
            "SegundoApellido": "",
            "Clave": "",
            "perfil": 0,
            "Fecha": "",
            "Estado": 0,
            "Cedula": "",
            "Modelo": 0,
            "CargoEmpleadosId": 0,
            "RolUsuarioId": 0
        };
    }

    const validaCedula=()=>{
      if(objUsuarios.Cedula.length<11)
          resp=true;
      else
          resp=false;
    }

    const cargos = async () => {
        let result = await empleadoService.CargosEmpleados();
        return result.data;
    }

    const roles = async () => {
        let result = await empleadoService.RolesUsuarios();
        return result.data;
    }

    const validarNuevoUsuario = ( usuarios:any ) =>{
        //...
        if(usuarios.Nombre != "" && usuarios.PrimerApellido != "" && usuarios.SegundoApellido != "" && usuarios.Clave != "" && usuarios.CargoEmpleadosId > 0 && usuarios.RolUsuarioId > 0 && usuarios.Cedula != ""){
            //...
            return true;
        }
        return false;
    }

    const btnNuevoUsuario = () => {
        $camposRequeridos = validarNuevoUsuario(objUsuarios);
        if($camposRequeridos){
            objUsuarios.Usuario = objUsuarios.Nombre[0] + objUsuarios.PrimerApellido;
            objUsuarios.Apellido = `${objUsuarios.PrimerApellido} ${objUsuarios.SegundoApellido}`;
            let guardarUsuario = usuariosServices.NuevoUsuario(objUsuarios).then(resp => console.log(resp.Mensaje));
            swal("Exito", "Nuevo Usuario Registrado con Exito.", "success");
            limpiarFormularios();
        }
    }

</script>

<TituloPagina Titulo="Nuevo Usuario"></TituloPagina>

<div class="container mt-2">
    <form class="mx-[5%]" on:submit|preventDefault={btnNuevoUsuario} method="POST">
        <div class="form-control mt-1">
            <div class="input-group relative">
                <LabelHorizontal>Usuarios</LabelHorizontal>
                <input
                  bind:value={objUsuarios.Usuario}
                  type="text"
                  readonly
                  class="input input-bordered text-lg text-gray-500 w-[500px]"  name="txtUsuario" />
            </div>
        </div>
        <div class="form-control mt-1">
            <div class="input-group relative">
                <LabelHorizontal>Primer Apellido</LabelHorizontal>
                <input
                bind:value={objUsuarios.PrimerApellido}
                  type="text"
                  class="input input-bordered text-lg text-gray-500 w-[500px]"  name="txtPrimerApellido" />
                  {#if $camposRequeridos == false || objUsuarios.PrimerApellido == ""}
                      <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                  {/if}
            </div>
        </div>
        <div class="form-control mt-1">
            <div class="input-group relative">
                <LabelHorizontal>Segundo Apellido</LabelHorizontal>
                <input
                bind:value={objUsuarios.SegundoApellido}
                  type="text"
                  class="input input-bordered text-lg text-gray-500 w-[500px]"  name="txtSegundoApellido" />
                  {#if $camposRequeridos == false || objUsuarios.SegundoApellido == ""}
                      <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                  {/if}
            </div>
        </div>
        <div class="form-control mt-1">
            <div class="input-group relative">
                <LabelHorizontal>Nombre(s)</LabelHorizontal>
                <input
                bind:value={objUsuarios.Nombre}
                  type="text"
                  class="input input-bordered text-lg text-gray-500 w-[500px]"  name="txtNombre" />
                  {#if $camposRequeridos == false || objUsuarios.Nombre == ""}
                      <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                  {/if}
            </div>
        </div>
        <div class="form-control mt-1">
            <div class="input-group relative">
                <LabelHorizontal>Contraseña</LabelHorizontal>
                <input
                bind:value={objUsuarios.Clave}
                  type="password"
                  class="input input-bordered text-lg text-gray-500 w-[500px]"  name="txtPassword" />
                  {#if $camposRequeridos == false || objUsuarios.Clave == ""}
                      <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                  {/if}
            </div>
        </div>
        <div class="form-control mt-1">
            <div class="input-group relative">
                <LabelHorizontal>Fecha de Registro</LabelHorizontal>
                <input
                bind:value={objUsuarios.Fecha}
                  type="date"
                  class="input input-bordered text-lg text-gray-500 w-[500px]"  name="txtFecha" />
                  {#if $camposRequeridos == false}
                      <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                  {/if}
            </div>
        </div>
        <div class="form-control mt-1">
            <div class="input-group relative">
                <LabelHorizontal>Cargo Empleado</LabelHorizontal>
                <select bind:value={objUsuarios.CargoEmpleadosId} name="cargoEmpleado" id="" class="select select-bordered text-lg text-gray-500 w-[500px]">
                    {#await cargos() then value}
                        <!-- promise was fulfilled -->
                        {#each value as item}
                             <!-- content here -->
                             <option value={item.id}>
                                {item.descripcion}
                            </option>
                        {/each}
                    {/await}
                </select>
                {#if $camposRequeridos == false || objUsuarios.CargoEmpleadosId <= 0}
                    <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                {/if}
            </div>
        </div>
        <div class="form-control mt-1">
            <div class="input-group relative">
                <LabelHorizontal>Rol Empleado</LabelHorizontal>
                <select bind:value={objUsuarios.RolUsuarioId} name="rolEmpleado" id="" class="select select-bordered text-lg text-gray-500 w-[500px]">
                    {#await roles() then value}
                        <!-- promise was fulfilled -->
                        {#each value as item}
                             <!-- content here -->
                            <option value={item.id}>
                                {item.nombre}
                            </option>
                        {/each}
                    {/await}
                </select>
                {#if $camposRequeridos == false}
                    <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                {/if}
            </div>
        </div>
        <div class="form-control mt-1">
            <div class="input-group relative">
              <LabelHorizontal>Cédula</LabelHorizontal>
              <MaskedInput 
                bind:value={objUsuarios.Cedula}
                options={mascaraCedula}
                type="text"
                on:complete={validaCedula}
                class="input input-bordered text-lg text-gray-500 w-[500px]"  name="tAnio" 
              />
              {#if $camposRequeridos == false}
                <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
              {/if}
            </div>
        </div>
        <div class="flex justify-end">
            <button type="submit" class="btn w-[300px] bg-green-500">
                <i class="mi mi-user-add"><span class="u-sr-only"></span></i> Guardar
            </button>
        </div>
    </form>    
</div>