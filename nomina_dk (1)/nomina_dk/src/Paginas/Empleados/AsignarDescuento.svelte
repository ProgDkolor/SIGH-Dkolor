<script lang="ts">
    import {bolantePago,
            verModalBolantePago,
            verModalLoading,
            tituloModalComprobantePago,
            frecuenciaConceptos,
            fDescuentoEmpleado,
            eDescuentoEmpleado,
            empresaSeleccionada,
            accionModalConfirmacion,
accionesAplicables
          } from '../../Modelo/Datos'
          import LabelHorizontal from '../../Componentes/LabelHorizontal.svelte'
    import { imask,MaskedInput } from 'svelte-imask';
    import { onMount } from 'svelte';
              //TIPOS FUNCIONES
      import TituloPagina from "../../Componentes/TituloPagina.svelte";
      import * as Servicios from '../../Servicios/AccesoApi'
      import type * as Tipos from '../../Modelo/Tipos'
      import * as Util from '../../Servicios/Utilidades'
      import {fade} from 'svelte/transition'
      import MdDelete from 'svelte-icons/md/MdDelete.svelte';
      
    //VARIABLES
    const marcaraPeriodo = {
          mask: '0000-00'
    };
    const mascaraCedula = {
          mask: '000-0000000-0'
    };
    let empleados:Tipos.Empleado[]=[];
    let periodoNomina:string='';
    let eCedula:boolean=false;
    let ePeriodoNomina:boolean=false;
    let nominaElegida:string='';
    let eNominaElegida:boolean=false;
    let consultaNominas:Tipos.ConsultaNominaPeriodo[]=[];
    let conceptos:Tipos.Conceptos[]=[];
    let beneficiosEmpleado:Tipos.BeneficiosEmpleados[]=[];
  
    //METODOS
    onMount(
         ()=>{
             console.log('voy a buscar los datos');
             cargarConceptos();
             cargarFrecuencias();
         }
     );
     const soloNumerosMonto=(e)=> {
      let actual = $fDescuentoEmpleado.monto;
      let nuevo = e.target.value;

      if (Util.VerificaNumero(nuevo) && nuevo.length < 17) {
        $fDescuentoEmpleado.monto = nuevo;
      } else {
          e.target.value = actual;
      }
  }

     const cargarConceptos=async ()=>{
      try{
        if(!ePeriodoNomina)
        {
         let datos:Tipos.RData={
          empresa: parseInt($empresaSeleccionada),
             sp: "ConsultaConceptosCategoria",
             modelo: "dynamic",
             dataParametros: [
               {
                 parametro: "@categoria",
                 tipo: 11,
                 valor: "3"
               }
             ]
           } 
           $verModalLoading=true;
           let res=await Servicios.ConsultaBd(datos);
           $verModalLoading=false;
           if(res.exito)
           {
               conceptos=res.data;
               if(conceptos.length==0)
               {
                   Util.VerMensaje("Atencion","No se encontraron los conceptos de BENEFICIO.");
               }
              console.log("Exito");
              console.log(conceptos);
           }else
           {
             Util.VerMensaje("Error Encontrado",res.mensaje);
   
           }
           console.log(res);	
         }
          }
          catch(error)
          {
            Util.VerMensaje("Error Encontrado","Error en la comunicacion con el web service."); 
          }
     };
 
     const cargarFrecuencias=async ()=>{
      try{
        if(!ePeriodoNomina)
        {
         let datos:Tipos.RData={
          empresa: parseInt($empresaSeleccionada),
             sp: "ListadoFrecuenciaConceptos",
             modelo: "dynamic",
             dataParametros: []
           } 
           $verModalLoading=true;
           let res=await Servicios.ConsultaBd(datos);
           $verModalLoading=false;
           if(res.exito)
           {
               $frecuenciaConceptos=res.data;
               if($frecuenciaConceptos.length==0)
               {
                   Util.VerMensaje("Atencion","No se encontro el listado de FRECUENCIA DE CONCEPTOS.");
               }
              console.log("FRECUENCIA CONCEPTOS");
              console.log($frecuenciaConceptos);
           }else
           {
             Util.VerMensaje("Error Encontrado",res.mensaje);
   
           }
           console.log(res);	
         }
        }
        catch(error)
        {
          Util.VerMensaje("Error Encontrado","Error en la comunicacion con el web service."); 
        }
     };
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
                      valor: $fDescuentoEmpleado.empleado.replaceAll('-','')
                    }
                  ]
                }
                $verModalLoading=true;
                let res=await Servicios.ConsultaBd(datos);
                $verModalLoading=false;
          if(res.exito)
          {
            empleados=res.data;
            if(empleados.length>0)
            {
             consultaDescuentosEmpleado();
            }else{
              Util.VerMensaje("Atencion","No se encontro ningun empleado con la cedula ingresada");

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
 
      const consultaDescuentosEmpleado=async()=>{
          validaCedula();
          try{
            if(!eNominaElegida && empleados.length>0){
                let datos:Tipos.RData={
                  empresa: parseInt($empresaSeleccionada),
                  sp: "ConsultaDescuentosEmpleados",
                  modelo: "dynamic",
                  dataParametros: [
                    {
                      parametro: "@empleado",
                      tipo: 16,
                      valor: empleados[0].cedula
                    }
                  ]
                }
                $verModalLoading=true;
                let res=await Servicios.ConsultaBd(datos);
                $verModalLoading=false;
          if(res.exito)
          {
            beneficiosEmpleado=res.data;
            if(beneficiosEmpleado.length>0)
           {
            conceptos=conceptos.filter((c) => !beneficiosEmpleado.find(({ concepto }) => c.id === concepto));
           }
            console.log(beneficiosEmpleado);
             console.log("Exito");
          }else
          {
             Util.VerMensaje("Error Encontrado",res.mensaje);
              console.log("error cargando beneficios empleado");
  
          }
          console.log(res);	
  
          }
              }
              catch(error)
              {
                Util.VerMensaje("Error Encontrado","Error en la comunicacion con el web service."); 
              }
      }
 
      const limpiar=()=>{
        empleados=[];
        $fDescuentoEmpleado.empleado='';
         $fDescuentoEmpleado.beneficio='';
         $fDescuentoEmpleado.monto='';
         $fDescuentoEmpleado.frecuencia='';
         $fDescuentoEmpleado.nota='';
             beneficiosEmpleado=[];
      }
 
      const asignarDescuento=async ()=>{
          validaBeneficio();
          validaCedula();
          validaFrecuencia();
          validaMonto();
          try{
            if(!$eDescuentoEmpleado.beneficio && !$eDescuentoEmpleado.empleado && !$eDescuentoEmpleado.frecuencia &&!$eDescuentoEmpleado.monto)
     {
       let datos:Tipos.RData={
        empresa: parseInt($empresaSeleccionada),
             sp: "AsignaDescuentoEmpleado",
             modelo: "RDataRegistro",
             dataParametros: [
               {
                 parametro: "@cedula",
                 tipo: 16,
                 valor: $fDescuentoEmpleado.empleado.toString()
               },
               {
                 parametro: "@concepto",
                 tipo: 11,
                 valor: $fDescuentoEmpleado.beneficio.toString()
               },
               {
                 parametro: "@frecuencia",
                 tipo: 11,
                 valor: $fDescuentoEmpleado.frecuencia.toString()
               },
               {
                 parametro: "@quincena",
                 tipo: 11,
                 valor: "1"
               },
               {
                 parametro: "@valor",
                 tipo: 7,
                 valor: $fDescuentoEmpleado.monto.toString()
               },
               {
                 parametro: "@nota",
                 tipo: 16,
                 valor: $fDescuentoEmpleado.nota.toString()
               },
               {
                 parametro: "@empleado",
                 tipo: 16,
                 valor: $fDescuentoEmpleado.empleado.toString()
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
                limpiar();
                consultaDescuentosEmpleado();
                 if(resNomina[0].resultado==0)
                 {
                   //limpiar();
                   Util.VerMensaje("Atencion","Descuento asignado con exito.");
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
     }
            }
            catch(error)
            {
              Util.VerMensaje("Error Encontrado","Error en la comunicacion con el web service."); 
            }
        
     };
     const options = {
         mask: '0000'
   };
 
      const consultarComprobantePago=async()=>{
          validaCedula();
          validaNomina();
          try{
            if(!eNominaElegida && empleados.length>0){
                let datos:Tipos.RData={
                  empresa: parseInt($empresaSeleccionada),
                  sp: "ConsultaBolantePagoEmpleado",
                  modelo: "ComprobantePago",
                  dataParametros: [
                    {
                      parametro: "@idNomina",
                      tipo: 16,
                      valor: nominaElegida
                    },
                    {
                      parametro: "@empleado",
                      tipo: 16,
                      valor: empleados[0].cedula
                    }
                  ]
                }
                $tituloModalComprobantePago="COMPROBANTE DE PAGO";
                $verModalLoading=true;
                let res=await Servicios.ConsultaBd(datos);
                $verModalLoading=false;
          if(res.exito)
          {
            $bolantePago=res.data;
            console.log($bolantePago);
            $verModalBolantePago=true;
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
        if(periodoNomina.length<4)
       {
          ePeriodoNomina=true;
       }else {
         ePeriodoNomina=false;
       }
      }
      const validaCedula=()=>{
        if($fDescuentoEmpleado.empleado.length<11)
       {
          eCedula=true;
       }else {
         eCedula=false;
       }
      }
      const validaBeneficio=()=>{
        if($fDescuentoEmpleado.beneficio.length<1)
       {
          $eDescuentoEmpleado.beneficio=true;
       }else {
          $eDescuentoEmpleado.beneficio=false;
       }
      }
      const validaFrecuencia=()=>{
        if($fDescuentoEmpleado.frecuencia.length<1)
       {
          $eDescuentoEmpleado.frecuencia=true;
       }else {
          $eDescuentoEmpleado.frecuencia=false;
       }
      }
      const validaMonto=()=>{
        if($fDescuentoEmpleado.monto.length<1)
       {
          $eDescuentoEmpleado.monto=true;
       }else {
          $eDescuentoEmpleado.monto=false;
       }
      }
      const onRetirarDescuento=(idDescuento:string)=>{
      $accionModalConfirmacion=(res)=>{
      if(res)
      {
        retirarBeneficio(idDescuento);
      }
    };
    Util.VerMensajeConfirmacion("Confirme",`Seguro desea eliminar el descuento: ${beneficiosEmpleado.find(b=>b.id===idDescuento).descripcionConcepto}`)
     }

      const retirarBeneficio=async (idDescuento:string)=>{
         try{
          if(!$eDescuentoEmpleado.beneficio)
    {
      let datos:Tipos.RData={
        empresa: parseInt($empresaSeleccionada),
            sp: "RetirarDescuentoEmpleado",
            modelo: "RDataRegistro",
            dataParametros: [
              {
                parametro: "@empleado",
                tipo: 16,
                valor: empleados[0].cedula
              },
              {
                parametro: "@descuento",
                tipo: 16,
                valor: idDescuento
              },
              {
                parametro: "@nota",
                tipo: 16,
                valor: ""
              },
             
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
               limpiar();
               consultaDescuentosEmpleado();
                if(resNomina[0].resultado==0)
                {
                  //limpiar();
                  Util.VerMensaje("Atencion","Descuento retirado con exito.");
                }else{
                  Util.VerMensaje("Error al retirar descuento",resNomina[0].mensaje);
                }
              }
             console.log("Exito");
             console.log(resNomina);
          }else
          {
            Util.VerMensaje("Atencion",res.mensaje);
  
          }
          console.log(res);	
    }
            }
            catch(error)
            {
              Util.VerMensaje("Error Encontrado","Error en la comunicacion con el web service."); 
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
     const consultaPeriodo=async ()=>{
      validaPeriodo();
      try{
        if(!ePeriodoNomina)
       {
         console.log(periodoNomina);
        let datos:Tipos.RData={
          empresa: parseInt($empresaSeleccionada),
            "sp": "ConsultaNominaPeriodo",
            "modelo": "ListadoNomina",
            "dataParametros": [
              {
                "parametro": "@periodo",
                "tipo": 16,
                "valor": periodoNomina
              }
            ]
          } 
          $verModalLoading=true;
          let res=await Servicios.ConsultaBd(datos);
          $verModalLoading=false;
          if(res.exito)
          {
              consultaNominas=res.data;
              if(consultaNominas.length===0)
              {
                Util.VerMensaje("Atencion","No se encontraron registros de nomina con el periodo ingresado");
              }
             console.log("Exito");
             console.log(consultaNominas);
          }else
          {
            Util.VerMensaje("Ocurrio un error",res.mensaje);
  
          }
          console.log(res);	
        }
          }
          catch(error)
          {
            Util.VerMensaje("Error Encontrado","Error en la comunicacion con el web service."); 
          }
    };
    const cargarNominasRegistradas=async ()=>{
      try{
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
                    Util.VerMensaje("Atencion","No se encontraron nominas registradas pendientes de procesar.");
                }
               console.log("Exito");
               console.log(consultaNominas);
            }else
            {
                console.log("error");
    
            }
            console.log(res);	
          }
          catch(error)
          {
            Util.VerMensaje("Error Encontrado","Error en la comunicacion con el web service."); 
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
    
    <TituloPagina Titulo="ASIGNAR DESCUENTO"></TituloPagina>
    
    
    <section class="w-full">
    <article class="bg-white rounded-b-2xl rounded-t-2xl p-3 mx-[5%]">
    
    
        <section class="pt-0">
   
          <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Cedula Empleado</LabelHorizontal>
                <MaskedInput 
                  bind:value={$fDescuentoEmpleado.empleado} 
                  options={mascaraCedula}
                  type="text"
                  class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tCedula" 
                />
                <button on:click={()=>consultaEmpleado()} class="btn">Consultar</button>
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
   
            {#if conceptos.length>0}
          <div class="form-control mb-1">
            <div class="input-group relative">
              <LabelHorizontal>Descuento</LabelHorizontal>
              <select bind:value={$fDescuentoEmpleado.beneficio}  name="cConcepto"  class="select select-bordered font-normal text-lg text-gray-500 w-[400px]">
               <option disabled={true} selected={true}></option> 
               {#each conceptos as c }
                 <option value={c.id}>{c.descripcion}</option> 
               {/each}
               </select>
              {#if $eDescuentoEmpleado.beneficio}
                  <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
              {/if}
            </div>
          </div>
          {/if}
   
          {#if $frecuenciaConceptos.length>0}
          <div class="form-control mb-1">
            <div class="input-group relative">
              <LabelHorizontal>Frecuencia</LabelHorizontal>
              <select bind:value={$fDescuentoEmpleado.frecuencia}  name="cfrecuencia"  class="select select-bordered font-normal text-lg text-gray-500 w-[400px]">
               <option disabled={true} selected={true}></option> 
               {#each $frecuenciaConceptos as c }
                 <option value={c.id}>{c.descripcion}</option> 
               {/each}
               </select>
              {#if $eDescuentoEmpleado.frecuencia}
                  <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
              {/if}
            </div>
          </div>
          {/if}
   
          <div class="form-control mb-1">
            <div class="input-group relative">
              <LabelHorizontal>Monto</LabelHorizontal>
              <input 
                value={$fDescuentoEmpleado.monto}
                on:input|preventDefault={soloNumerosMonto}
                type="text"
                class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tValor" 
              />
              {#if $eDescuentoEmpleado.monto}
                  <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
              {/if}
            </div>
          </div>
   
          <div class="form-control mb-1">
            <div class="input-group">
              <LabelHorizontal>Nota</LabelHorizontal>
              <textarea bind:value={$fDescuentoEmpleado.nota}  class="textarea h-[150px] textarea-bordered text-lg w-[400px]" placeholder=""></textarea>
              <button on:click={()=>asignarDescuento()} class="btn">Asignar Descuento</button>
            </div>
          </div>
   
          {#if beneficiosEmpleado.length>0}
          <h1 class="p-2 bg-base-300 text-center text-lg font-medium mt-2">DESCUENTOS ACTUALES</h1>
          <section class="w-full">
            <div class="overflow-x-auto">
              <table class="table w-full table-compact mt-[15px]">
                <thead>
                  <tr>
                    <td></td>
                    <th>Frecuencia</th> 
                    <th class="text-right">Valor</th> 
                    <th class="text-center">Concepto<th> 
                     <th class="text-center">Nota<th> 
                  </tr>
                </thead> 
                <tbody>
                  {#each beneficiosEmpleado as b}
                  <tr>
                    <td>
                      <section>
                        <button on:click={()=>onRetirarDescuento(b.id)} class="w-[32px] h-[32px] text-red-500 hover:text-gray-800 cursor-pointer">
                          <MdDelete />
                        </button>
    
                       </section>
                    </td>
                    <td>{b.descripcionFrecuencia}</td> 
                    <td class="text-right">{b.valor.toLocaleString('es-DO',{minimumFractionDigits: 2})}</td> 
                    <td class="text-center">{b.descripcionConcepto}<td> 
                     <td class="text-center">{b.nota}<td> 
                  </tr>
                  {/each}
                </tbody> 
                <!-- <tfoot>
                  <tr>
                    <th></th> 
                    <th>Name</th> 
                    <th>Job</th> 
                    <th>company</th> 
                    <th>location</th> 
                    <th>Last Login</th> 
                    <th>Favorite Color</th>
                  </tr>
                </tfoot> -->
              </table>
            </div>
          </section>
          {/if}
   
              {/if}
    
          
              {#if consultaNominas.length>0}
              <div class="form-control pt-3">
                <label for="cMes" class="label-text text-lg font-semibold text-gray-900">Escoja Nomina</label>
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
        </section>
        {#if consultaNominas.length>0}
          <section class="flex flex-row-reverse py-2  rounded-b-2xl pr-2">
            <label for="modalCitas" on:click={consultarComprobantePago} class="btn btn-md mx-1 font-bold hover:bg-white hover:text-green-700">Consultar Comprobante Pago</label>
        </section>
        {/if}
        </article>
        
    </section>
  
  </main>
{:else if  permisosCrud.c == 1}
   <!-- else if content here -->
   <main in:fade="{{duration: 500}}">
    
    <TituloPagina Titulo="ASIGNAR DESCUENTO"></TituloPagina>
    
    
    <section class="w-full">
    <article class="bg-white rounded-b-2xl rounded-t-2xl p-3 mx-[5%]">
    
    
        <section class="pt-0">
   
          <div class="form-control mb-1">
              <div class="input-group relative">
                <LabelHorizontal>Cedula Empleado</LabelHorizontal>
                <MaskedInput 
                  bind:value={$fDescuentoEmpleado.empleado} 
                  options={mascaraCedula}
                  type="text"
                  class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tCedula" 
                />
                <button on:click={()=>consultaEmpleado()} class="btn">Consultar</button>
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
   
            {#if conceptos.length>0}
          <div class="form-control mb-1">
            <div class="input-group relative">
              <LabelHorizontal>Descuento</LabelHorizontal>
              <select bind:value={$fDescuentoEmpleado.beneficio}  name="cConcepto"  class="select select-bordered font-normal text-lg text-gray-500 w-[400px]">
               <option disabled={true} selected={true}></option> 
               {#each conceptos as c }
                 <option value={c.id}>{c.descripcion}</option> 
               {/each}
               </select>
              {#if $eDescuentoEmpleado.beneficio}
                  <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
              {/if}
            </div>
          </div>
          {/if}
   
          {#if $frecuenciaConceptos.length>0}
          <div class="form-control mb-1">
            <div class="input-group relative">
              <LabelHorizontal>Frecuencia</LabelHorizontal>
              <select bind:value={$fDescuentoEmpleado.frecuencia}  name="cfrecuencia"  class="select select-bordered font-normal text-lg text-gray-500 w-[400px]">
               <option disabled={true} selected={true}></option> 
               {#each $frecuenciaConceptos as c }
                 <option value={c.id}>{c.descripcion}</option> 
               {/each}
               </select>
              {#if $eDescuentoEmpleado.frecuencia}
                  <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
              {/if}
            </div>
          </div>
          {/if}
   
          <div class="form-control mb-1">
            <div class="input-group relative">
              <LabelHorizontal>Monto</LabelHorizontal>
              <input 
                value={$fDescuentoEmpleado.monto}
                on:input|preventDefault={soloNumerosMonto}
                type="text"
                class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tValor" 
              />
              {#if $eDescuentoEmpleado.monto}
                  <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
              {/if}
            </div>
          </div>
   
          <div class="form-control mb-1">
            <div class="input-group">
              <LabelHorizontal>Nota</LabelHorizontal>
              <textarea bind:value={$fDescuentoEmpleado.nota}  class="textarea h-[150px] textarea-bordered text-lg w-[400px]" placeholder=""></textarea>
              <button on:click={()=>asignarDescuento()} class="btn">Asignar Descuento</button>
            </div>
          </div>
   
          {#if beneficiosEmpleado.length>0}
          <h1 class="p-2 bg-base-300 text-center text-lg font-medium mt-2">DESCUENTOS ACTUALES</h1>
          <section class="w-full">
            <div class="overflow-x-auto">
              <table class="table w-full table-compact mt-[15px]">
                <thead>
                  <tr>
                    <td></td>
                    <th>Frecuencia</th> 
                    <th class="text-right">Valor</th> 
                    <th class="text-center">Concepto<th> 
                     <th class="text-center">Nota<th> 
                  </tr>
                </thead> 
                <tbody>
                  {#each beneficiosEmpleado as b}
                  <tr>
                    <td>
                      <section>
                        <button on:click={()=>onRetirarDescuento(b.id)} class="w-[32px] h-[32px] text-red-500 hover:text-gray-800 cursor-pointer">
                          <MdDelete />
                        </button>
    
                       </section>
                    </td>
                    <td>{b.descripcionFrecuencia}</td> 
                    <td class="text-right">{b.valor.toLocaleString('es-DO',{minimumFractionDigits: 2})}</td> 
                    <td class="text-center">{b.descripcionConcepto}<td> 
                     <td class="text-center">{b.nota}<td> 
                  </tr>
                  {/each}
                </tbody> 
                <!-- <tfoot>
                  <tr>
                    <th></th> 
                    <th>Name</th> 
                    <th>Job</th> 
                    <th>company</th> 
                    <th>location</th> 
                    <th>Last Login</th> 
                    <th>Favorite Color</th>
                  </tr>
                </tfoot> -->
              </table>
            </div>
          </section>
          {/if}
   
              {/if}
    
          
              {#if consultaNominas.length>0}
              <div class="form-control pt-3">
                <label for="cMes" class="label-text text-lg font-semibold text-gray-900">Escoja Nomina</label>
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
        </section>
        {#if consultaNominas.length>0}
          <section class="flex flex-row-reverse py-2  rounded-b-2xl pr-2">
            <label for="modalCitas" on:click={consultarComprobantePago} class="btn btn-md mx-1 font-bold hover:bg-white hover:text-green-700">Consultar Comprobante Pago</label>
        </section>
        {/if}
        </article>
        
    </section>
  
  </main>
{:else}
   <!-- else content here -->
   <p>No posee permisos para ver esta pagina</p>
{/if}

  