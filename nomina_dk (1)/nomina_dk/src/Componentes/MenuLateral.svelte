

<script lang="ts">
    import {location} from 'svelte-spa-router'
    import MenuNomina from './MenuNomina.svelte';
    import MenuEmpleados from './MenuEmpleados.svelte';

</script>

{#if $location.includes('/nomina')}
    <MenuNomina></MenuNomina>
    {:else if $location.includes('/empleados')}
    <MenuEmpleados></MenuEmpleados>    
{/if}
