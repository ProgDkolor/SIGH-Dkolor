
<script lang="ts">
    import {location} from 'svelte-spa-router';
	import TituloPagina from './../../Componentes/TituloPagina.svelte';
    import * as usuariosService from '../../Servicios/UsuarioService';
    import {listaDeUsuarios} from '../../Modelo/Datos';

    let opcionActiva:string="bg-navy-500 font-medium";

    let Titulo = 'Gestionar Usuarios';
    const listadoEmpleado = async ()=>{
        let result = await usuariosService.ListarUsuarios();
        console.log(result.Data)
        return result.Data;
    }

    const eliminarUsuario = () => {
        //... Eliminar un usuario
    }

    const editarUsaurio = () => {
        //...
    }

    //$location.includes( '/empleados/GestionarAccionesDePersonal' ) ? opcionActiva :""
</script>

<TituloPagina {Titulo}></TituloPagina>

<a href={`/#/empleados/NuevoUsuario`} class="btn">
    <i class="mi mi-user-add"><span class="u-sr-only"></span></i> Nuevo Usuario
</a>

<div class="container my-2">
    <table class="table table-auto w-full">
        <thead>
            <tr>
                <th>Usuario</th>
                <th>Nombre</th>
                <th>Apellido</th>
                <th>Estado</th>
                <th>Cedula</th>
                <th>RolUsuarioId</th>
                <th>Acciones</th>
            </tr>
        </thead>
        <tbody>
            {#await listadoEmpleado() then value}
                <!-- promise was fulfilled -->
                {#each value as item}
                     <!-- content here -->
                        <tr>
                            <td>{item.usuario}</td>
                            <td>{item.nombre}</td>
                            <td>{item.apellido}</td>
                            <td>{item.EstadoActual}</td>
                            <td>{item.cedula}</td>
                            <td>{item.NombreDelRol}</td>
                            <td>
                                <button class="btn rounded-full bg-green-500 md:text-md w-[50px]" title="Editar Usuario">
                                    <i class="mi mi-edit"><span class="u-sr-only"></span></i>
                                </button>
                                <button class="btn rounded-full bg-red-700 md:text-md w-[50px]" title="Bloquear Usuario">
                                    <i class="mi mi-ban"><span class="u-sr-only"></span></i>
                                </button>
                            </td>
                        </tr>
                {/each}
            {/await}
        </tbody>
    </table>
</div>