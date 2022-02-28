<script lang="ts">
    import {nominaPrevia,verModalNominaPrevia,verModalLoading,empresaSeleccionada, accionesAplicables} from '../../../Modelo/Datos'
    import LabelHorizontal from '../../../Componentes/LabelHorizontal.svelte'
    import { onMount } from 'svelte';
              //TIPOS FUNCIONES
      import TituloPagina from "../../../Componentes/TituloPagina.svelte";
      import * as Servicios from '../../../Servicios/AccesoApi'
      import type * as Tipos from '../../../Modelo/Tipos'
      import * as Util from '../../../Servicios/Utilidades'
      import * as Enu from '../../../Modelo/Enumeraciones'
      import {fade} from 'svelte/transition'
      import { imask,MaskedInput } from 'svelte-imask';

      const mascaraCedula = {
          mask: '000-0000000-0'
    };
      
    //VARIABLES
    let periodoNomina:string='';
    let ePeriodoNomina:boolean=false;
    let nominaElegida:string='';
    let eNominaElegida:boolean=false;
    let eCedula:boolean=false;
    let consultaNominas:Tipos.ConsultaNominaPeriodo[]=[];
    let empleados:Tipos.Empleado[]=[];
    let pagoVacaciones:Tipos.PagoVacaciones[]=[];
    let empleadoVacaciones:Tipos.DRegistroNovedad={
                        empleado: "",
                        concepto: "",
                        cantidad: "",
                        nota: "",
                        idNomina: "",
                            };
    onMount(
        ()=>{
            console.log('voy a buscar los datos');
            cargarNominasActiva();
        }
    );
  
    //METODOS
    const limpiar=()=>{
      consultaNominas=[];
      empleados=[];
      empleadoVacaciones={
                        empleado: "",
                        concepto: "",
                        cantidad: "",
                        nota: "",
                        idNomina: "",
                            };
    };
    let eCantidad:boolean=false;   
    const soloNumerosCantidad=(e)=> {
      let actual = empleadoVacaciones.cantidad;
      let nuevo = e.target.value;

      if (Util.VerificaNumero(nuevo) && nuevo.length < 17) {
        empleadoVacaciones.cantidad = nuevo;
      } else {
          e.target.value = actual;
      }
  }
  const validaCantidad=()=>{
        if(empleadoVacaciones.cantidad.length<1)
            eCantidad=true;
        else
            eCantidad=false;
      }
    const validaCedula=()=>{
        if(empleadoVacaciones.empleado.length<11)
       {
          eCedula=true;
       }else {
         eCedula=false;
       }
      }
    const consultaEmpleado=async()=>{
        validaCedula();
        try{
          if(!eCedula){
                let datos:Tipos.RData={
                  empresa: parseInt($empresaSeleccionada),
                  sp: "ConsultaEmpleadoCedula",
                  modelo: "dynamic",
                  dataParametros: [
                    {
                      parametro: "@cedula",
                      tipo: 16,
                      valor: empleadoVacaciones.empleado.replaceAll('-','')
                    }
                  ]
                }
                $verModalLoading=true;
                let res=await Servicios.ConsultaBd(datos);
                $verModalLoading=false;
          if(res.exito)
          {
            empleados=res.data;
            if(empleados.length==0){
              Util.VerMensaje("Atencion","No se encontro ningun empleado con la cedula ingresada");
            }else{
              consultaPagosVacaciones();
            }
            
             console.log("Exito");
          }else
          {
              console.log("error");
  
          }
          console.log(res);	
  
          }
          }
          catch(error)
          {
            Util.VerMensaje("Error Encontrado","Error en la comunicacion con el web service."); 
          }
      }
    const registroPagoVacaciones=async ()=>{
      validaNomina();
      validaCantidad();
      try{
        if(!eNominaElegida && !eCantidad)
    {
      let datos:Tipos.RData={
        empresa: parseInt($empresaSeleccionada),
            sp: "RegistroNovedad",
            modelo: "RDataRegistro",
            dataParametros: [
              {
                parametro: "@empleado",
                tipo: Enu.DbType.String,
                valor: empleadoVacaciones.empleado.replaceAll('-','')
              },
              {
                parametro: "@concepto",
                tipo: Enu.DbType.String,
                valor: "15"
              },
              {
                parametro: "@cantidad",
                tipo: Enu.DbType.Decimal,
                valor: empleadoVacaciones.cantidad
              },
              {
                parametro: "@nota",
                tipo: Enu.DbType.String,
                valor: empleadoVacaciones.nota
              },
              {
                parametro: "@idNomina",
                tipo: Enu.DbType.String,
                valor: nominaElegida
              }
            ]
          } 
          $verModalLoading=true;
          let res=await Servicios.RegistroBd(datos);
          $verModalLoading=false;
          if(res.exito)
          {
           let resNomina=res.data;
              if(resNomina.length>0)
              {
                if(resNomina[0].resultado==0)
                {
                  limpiar();
                  Util.VerMensaje("Atencion","Pago de vacaciones registrado con exito.");
                  cargarNominasAprobadasPostRegistro();
                }else{
                  Util.VerMensaje("Error al registrar",resNomina[0].mensaje);
                }
              }
             console.log("Exito");
             console.log(resNomina);
          }else
          {
            Util.VerMensaje("Atencion",res.mensaje);
  
          }
          console.log(res);	
    }else if(eNominaElegida)
            {
              Util.verNotificacion("Debe elegir la nomina.");
            }
            else if(empleados.length==0)
            {
              Util.verNotificacion("Debe cargar el empleado.");
            }
            else if(eCantidad)
            {
              Util.verNotificacion("Debe especificar la cantidad de dias a pagar.");
            }
        }
        catch(error)
        {
          Util.VerMensaje("Error Encontrado","Error en la comunicacion con el web service."); 
        }
   
       
    };

      const generarNominaPrevia=async()=>{
          validaNomina();
          try{
            if(!eNominaElegida){
                let datos:Tipos.RData={
                  empresa: parseInt($empresaSeleccionada),
                  sp: "GeneraNominaPrevia",
                  modelo: "GeneraNominaPrevia",
                  dataParametros: [
                    {
                      parametro: "@id",
                      tipo: 16,
                      valor: nominaElegida
                    }
                  ]
                }
                $verModalLoading=true;
                let res=await Servicios.ConsultaBd(datos);
                $verModalLoading=false;
          if(res.exito)
          {
            $nominaPrevia=res.data;
            $verModalNominaPrevia=true;
             console.log("Exito");
          }else
          {
              console.log("error");
  
          }
          console.log(res);	
          }
            }
            catch(error)
            {
              Util.VerMensaje("Error Encontrado","Error en la comunicacion con el web service."); 
            }
      }
      const validaPeriodo=()=>{
        if(periodoNomina.length<5)
       {
          ePeriodoNomina=true;
       }else {
         ePeriodoNomina=false;
       }
      }
      const validaNomina=()=>{
       if(nominaElegida.length<1)
          {
              eNominaElegida =true;
              return;
          }
          else{
            eNominaElegida=false;
          }
      }
      const cargarNominasActiva=async ()=>{
        try{
          if(!ePeriodoNomina)
       {
        let datos:Tipos.RData={
          empresa: parseInt($empresaSeleccionada),
            sp: "ConsultaNominaCondicion",
            modelo: "ListadoNomina",
            dataParametros: [
              {
                parametro: "@condicion",
                tipo: 16,
                valor: "1"
              }
            ]
          } 
          $verModalLoading=true;
          let res=await Servicios.ConsultaBd(datos);
          $verModalLoading=false;
          if(res.exito)
          {
              consultaNominas=res.data;
              if(consultaNominas.length==0)
              {
                  Util.VerMensaje("Atencion","No se encontraron nominas en condicion ACTIVA.");
              }
             console.log("Exito");
             console.log(consultaNominas);
          }else
          {
              console.log("error");
  
          }
          console.log(res);	
        }
          }
          catch(error)
          {
            Util.VerMensaje("Error Encontrado","Error en la comunicacion con el web service."); 
          }
       
    };

    const consultaPagosVacaciones=async ()=>{
        try{
          if(!ePeriodoNomina)
       {
        let datos:Tipos.RData={
          empresa: parseInt($empresaSeleccionada),
            sp: "ConsultaPagoVacacionesNomina",
            modelo: "dynamic",
            dataParametros: [
              {
                parametro: "@nomina",
                tipo: 16,
                valor: nominaElegida
              },
              {
                parametro: "@empleado",
                tipo: 16,
                valor: empleadoVacaciones.empleado.replaceAll('-','')
              }
            ]
          } 
          $verModalLoading=true;
          let res=await Servicios.ConsultaBd(datos);
          $verModalLoading=false;
          if(res.exito)
          {
              pagoVacaciones=res.data;
              if(pagoVacaciones.length>0)
              {
                empleadoVacaciones.cantidad=pagoVacaciones[0].cantidad.toString();
                empleadoVacaciones.nota=pagoVacaciones[0].nota.toString();

              }
             console.log("Exito");
             console.log(pagoVacaciones);
          }else
          {
              console.log("error");
  
          }
          console.log(res);	
        }
          }
          catch(error)
          {
            Util.VerMensaje("Error Encontrado","Error en la comunicacion con el web service."); 
          }
       
    };
    const cargarNominasAprobadasPostRegistro=async ()=>{
      try{
        if(!ePeriodoNomina)
       {
        let datos:Tipos.RData={
          empresa: parseInt($empresaSeleccionada),
            sp: "ConsultaNominaCondicion",
            modelo: "ListadoNomina",
            dataParametros: [
              {
                parametro: "@condicion",
                tipo: 16,
                valor: "4"
              }
            ]
          } 
          $verModalLoading=true;
          let res=await Servicios.ConsultaBd(datos);
          $verModalLoading=false;
          if(res.exito)
          {
              consultaNominas=res.data;
          }else
          {
              console.log("error al cargar nominas procesadas para aprobacion");
  
          }
          console.log(res);	
        }
          }
          catch(error)
          {
            Util.VerMensaje("Error Encontrado","Error en la comunicacion con el web service."); 
          }
    };

    
     const consultaPeriodo=async ()=>{
      validaPeriodo();
      try{
        if(!ePeriodoNomina)
       {
        let datos:Tipos.RData={
          empresa: parseInt($empresaSeleccionada),
            sp: "ConsultaNominaCondicion",
            modelo: "ListadoNomina",
            dataParametros: [
              {
                parametro: "@condicion",
                tipo: 16,
                valor: "1"
              }
            ]
          } 
          $verModalLoading=true;
          let res=await Servicios.ConsultaBd(datos);
          $verModalLoading=false;
          if(res.exito)
          {
              consultaNominas=res.data;
             console.log("Exito");
             console.log(consultaNominas);
          }else
          {
              console.log("error");
  
          }
          console.log(res);	
        }
          }
          catch(error)
          {
            Util.VerMensaje("Error Encontrado","Error en la comunicacion con el web service."); 
          }
    };
    const actualizaPeriodo = ({ detail }) => {
      const value = detail.inputState.maskedValue;
        periodoNomina=value;
        validaPeriodo();
       }
  
       const OnEnterPeriodo = e => {
      if (e.charCode === 13) {
      console.log('enter presionado');
      consultaPeriodo();
      }
    };
    let permisosCrud: any = {
      "c": $accionesAplicables.c,
      "r": $accionesAplicables.r,
      "u": $accionesAplicables.u,
      "d": $accionesAplicables.d,
      "crud": $accionesAplicables.crud
    };
  </script>


{#if permisosCrud.crud == 1}
   <!-- content here -->

   <main in:fade="{{duration: 500}}">

    <TituloPagina Titulo="PAGO DE VACACIONES"></TituloPagina>
    
    
    <section class="w-full">
    <article class="rounded-b-2xl rounded-t-2xl p-3 mx-[5%]">
  
      <button on:click={()=>cargarNominasActiva()} class="btn btn-sm mx-1 bg-green-700 font-bold border-none hover:bg-white hover:text-green-700">Consultar Nominas Activa</button>
    
        <section class="pt-0">
              {#if consultaNominas.length>0}
              <div class="form-control pt-3">
                <LabelHorizontal>Escoja Nomina</LabelHorizontal>
                {#each consultaNominas as s}
                <label class="cursor-pointer label">
                    <span class="label-text text-gray-800 font-semibold">{s.descripcion}</span> 
                    <input type="radio" bind:group={nominaElegida} on:change={validaNomina}  name="cNominaPrevia" checked={false} class="radio" value={s.id}>
                  </label>
                {/each}
                {#if eNominaElegida}
                <span class="before:text-xs text-red-400">*Debe elegir la nomina</span>
                {/if}
              </div> 
              {/if}
  
              {#if nominaElegida.length>0}
              <div class="form-control mb-1">
                  <div class="input-group relative">
                    <LabelHorizontal>Cedula Empleado</LabelHorizontal>
                    <MaskedInput 
                      bind:value={empleadoVacaciones.empleado} 
                      options={mascaraCedula}
                      type="text"
                      class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tCedula" 
                    />
                    <button on:click={()=>consultaEmpleado()}  class="btn">Consultar</button>
                    {#if eCedula}
                        <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                    {/if}
                  </div>
                </div>
  
                {#if empleados.length>0}
                <div class="form-control mb-1">
                  <div class="input-group relative">
                    <LabelHorizontal>Empleado</LabelHorizontal>
                    <input 
                      value={ `${empleados[0].nombre} ${empleados[0].apellido}`} 
                      type="text"
                      readonly
                      class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tPeriodoNomina" 
                    />
                  </div>
                </div>
  
                <div class="form-control mb-1">
                  <div class="input-group relative">
                    <LabelHorizontal>Salario Diario</LabelHorizontal>
                    <input 
                      value={ `${empleados[0].salarioDiario.toLocaleString('es-DO',{minimumFractionDigits: 2})}`} 
                      type="text"
                      readonly
                      class="input input-bordered text-lg text-gray-500 w-[200px]"  name="tPeriodoNomina" 
                    />
                    <LabelHorizontal>Calculo</LabelHorizontal>
                    {#if empleados[0].salarioDiario && parseFloat(empleadoVacaciones.cantidad.toString())>0}
                    <input 
                      value={ `${(empleados[0].salarioDiario*parseFloat(empleadoVacaciones.cantidad.toString())).toLocaleString('es-DO',{minimumFractionDigits: 2})}`} 
                      type="text"
                      readonly
                      class="input input-bordered text-lg text-gray-500 w-[200px]"  name="tPeriodoNomina" 
                    />
                    {/if}
                  </div>
                </div>
  
                <div class="form-control mb-1">
                  <div class="input-group relative">
                    <LabelHorizontal>Dias A Pagar</LabelHorizontal>
                    <input 
                      value={empleadoVacaciones.cantidad} 
                      on:keyup={validaCantidad}
                      on:input|preventDefault={soloNumerosCantidad}
                      type="text"
                      class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tCantidadVacaciones" 
                    />
                    {#if eCantidad}
                        <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                    {/if}
                  </div>
                </div>
  
                <div class="form-control mb-1">
                  <div class="input-group">
                    <LabelHorizontal>Nota</LabelHorizontal>
                    <textarea bind:value={empleadoVacaciones.nota} class="textarea h-[150px] textarea-bordered text-lg w-[400px]" placeholder=""></textarea>
                  </div>
                </div>
                {/if}
                  
              {/if}
        </section>
        {#if consultaNominas.length>0}
          <section class="flex flex-row-reverse py-2  rounded-b-2xl pr-2">
            <button for="modalCitas" on:click={registroPagoVacaciones} class="btn btn-md mx-1 font-bold bg-red-700 border-none hover:bg-white hover:text-green-700">Registrar Pago Vacaciones</button>
        </section>
        {/if}
        </article>
        
    </section>
  
  </main>
{:else if  permisosCrud.c == 1 || permisosCrud.r == 1}
   <!-- else if content here -->

   <main in:fade="{{duration: 500}}">

    <TituloPagina Titulo="PAGO DE VACACIONES"></TituloPagina>
    
    
    <section class="w-full">
    <article class="rounded-b-2xl rounded-t-2xl p-3 mx-[5%]">
  
      <button on:click={()=>cargarNominasActiva()} class="btn btn-sm mx-1 bg-green-700 font-bold border-none hover:bg-white hover:text-green-700">Consultar Nominas Activa</button>
    
        <section class="pt-0">
              {#if consultaNominas.length>0}
              <div class="form-control pt-3">
                <LabelHorizontal>Escoja Nomina</LabelHorizontal>
                {#each consultaNominas as s}
                <label class="cursor-pointer label">
                    <span class="label-text text-gray-800 font-semibold">{s.descripcion}</span> 
                    <input type="radio" bind:group={nominaElegida} on:change={validaNomina}  name="cNominaPrevia" checked={false} class="radio" value={s.id}>
                  </label>
                {/each}
                {#if eNominaElegida}
                <span class="before:text-xs text-red-400">*Debe elegir la nomina</span>
                {/if}
              </div> 
              {/if}
  
              {#if nominaElegida.length>0}
              <div class="form-control mb-1">
                  <div class="input-group relative">
                    <LabelHorizontal>Cedula Empleado</LabelHorizontal>
                    <MaskedInput 
                      bind:value={empleadoVacaciones.empleado} 
                      options={mascaraCedula}
                      type="text"
                      class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tCedula" 
                    />
                    <button on:click={()=>consultaEmpleado()}  class="btn">Consultar</button>
                    {#if eCedula}
                        <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                    {/if}
                  </div>
                </div>
  
                {#if empleados.length>0}
                <div class="form-control mb-1">
                  <div class="input-group relative">
                    <LabelHorizontal>Empleado</LabelHorizontal>
                    <input 
                      value={ `${empleados[0].nombre} ${empleados[0].apellido}`} 
                      type="text"
                      readonly
                      class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tPeriodoNomina" 
                    />
                  </div>
                </div>
  
                <div class="form-control mb-1">
                  <div class="input-group relative">
                    <LabelHorizontal>Salario Diario</LabelHorizontal>
                    <input 
                      value={ `${empleados[0].salarioDiario.toLocaleString('es-DO',{minimumFractionDigits: 2})}`} 
                      type="text"
                      readonly
                      class="input input-bordered text-lg text-gray-500 w-[200px]"  name="tPeriodoNomina" 
                    />
                    <LabelHorizontal>Calculo</LabelHorizontal>
                    {#if empleados[0].salarioDiario && parseFloat(empleadoVacaciones.cantidad.toString())>0}
                    <input 
                      value={ `${(empleados[0].salarioDiario*parseFloat(empleadoVacaciones.cantidad.toString())).toLocaleString('es-DO',{minimumFractionDigits: 2})}`} 
                      type="text"
                      readonly
                      class="input input-bordered text-lg text-gray-500 w-[200px]"  name="tPeriodoNomina" 
                    />
                    {/if}
                  </div>
                </div>
  
                <div class="form-control mb-1">
                  <div class="input-group relative">
                    <LabelHorizontal>Dias A Pagar</LabelHorizontal>
                    <input 
                      value={empleadoVacaciones.cantidad} 
                      on:keyup={validaCantidad}
                      on:input|preventDefault={soloNumerosCantidad}
                      type="text"
                      class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tCantidadVacaciones" 
                    />
                    {#if eCantidad}
                        <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
                    {/if}
                  </div>
                </div>
  
                <div class="form-control mb-1">
                  <div class="input-group">
                    <LabelHorizontal>Nota</LabelHorizontal>
                    <textarea bind:value={empleadoVacaciones.nota} class="textarea h-[150px] textarea-bordered text-lg w-[400px]" placeholder=""></textarea>
                  </div>
                </div>
                {/if}
                  
              {/if}
        </section>
        {#if consultaNominas.length>0}
          <section class="flex flex-row-reverse py-2  rounded-b-2xl pr-2">
            <button for="modalCitas" on:click={registroPagoVacaciones} class="btn btn-md mx-1 font-bold bg-red-700 border-none hover:bg-white hover:text-green-700">Registrar Pago Vacaciones</button>
        </section>
        {/if}
        </article>
        
    </section>
  
  </main>
{:else}
   <!-- else content here -->
   <p>
     No tiene permisos.
   </p>
{/if}
