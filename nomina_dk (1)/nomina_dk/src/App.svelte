
<script>
	import Router from 'svelte-spa-router';
	import { routes } from './Router'
  import Navegacion from './Componentes/Navegacion.svelte';
  import MenuLateral from './Componentes/MenuLateral.svelte';
  import BolantePago from './VentanasModal/BolantePago.svelte';
  import CambioClave from './VentanasModal/CambioClave.svelte';
  import VentanaConfirmacion from './VentanasModal/VentanaConfirmacion.svelte';
  import VentanasModal from './Componentes/VentanasModal.svelte';
  import ReporteNovedadNomina from './VentanasModal/ReporteNovedadNomina.svelte';
  import VentanaMensaje from './VentanasModal/VentanaMensaje.svelte';
  import { LoginActivo } from './Modelo/Datos';

import Login from './Paginas/Login.svelte';
import { SvelteToast } from '@zerodevx/svelte-toast'
const options = {
    
  }
  </script>
  <SvelteToast {options} />
  <ReporteNovedadNomina></ReporteNovedadNomina>
  <VentanaMensaje></VentanaMensaje>
  <VentanaConfirmacion></VentanaConfirmacion>
  <BolantePago></BolantePago>
  <CambioClave></CambioClave>
<VentanasModal></VentanasModal>
  <main class="flex">
    
    <Navegacion></Navegacion>
    <aside class={$LoginActivo? "flex-none w-[300px] fixed mt-[80px] z-50" :""}>
        <MenuLateral></MenuLateral>
    </aside>
    <section class={$LoginActivo ? "ml-[300px] w-full mt-[80px]" :"w-full"}>
        <Router {routes}/>
    </section>


  </main>
  



	


	