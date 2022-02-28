
<script lang="ts">
    import TituloPagina from "../../Componentes/TituloPagina.svelte";
</script>


<TituloPagina Titulo="LISTADO DE EMPLEADOS"></TituloPagina>

<table class="table table-auto mt-5">
    <thead>
        <tr>
            <th>Id</th>
            <th>Cédula</th>
            <th>Nombre(s)</th>
            <th>Apellido(s)</th>
            <th>Cargo</th>
            <th>Salario</th>
            <th>Contrato</th>
            <th>Acciones</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>ED270477-6CA7-4D83-BF11-66224DAD951A</td>
            <td>40225810569</td>
            <td>Gerson</td>
            <td>Santos Mateo</td>
            <td>Programador</td>
            <td>55000</td>
            <td>Contrato</td>
            <td>
                <button class="btn rounded-full">
                    perfil
                </button>
                <button class="btn"> bloquear </button>
            </td>
        </tr>
    </tbody>
</table>