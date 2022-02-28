<script lang="ts">
    import {bolantePago,
            verModalBolantePago,
            verModalLoading,
            tituloModalComprobantePago,
            frecuenciaConceptos,
            empresaSeleccionada,
            accionModalConfirmacion,
            accionesAplicables
            } from '../../Modelo/Datos';
    import LabelHorizontal from '../../Componentes/LabelHorizontal.svelte';
    import { imask,MaskedInput } from 'svelte-imask';
    import { onMount } from 'svelte';

    //TIPOS FUNCIONES
    import TituloPagina from "../../Componentes/TituloPagina.svelte";
    import * as Servicios from '../../Servicios/AccesoApi'
    import type * as Tipos from '../../Modelo/Tipos'
    import * as Util from '../../Servicios/Utilidades'
    import {fade} from 'svelte/transition'
    import MdDelete from 'svelte-icons/md/MdDelete.svelte';
    import MdEdit from 'svelte-icons/md/MdEdit.svelte';  
      
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
    export let fBeneficioEmpleado:Tipos.DRegistroBeneficioEmpleado={
     empleado:'',
     beneficio:'',
     frecuencia:'',
     monto:'',
     nota:''
   };
  let eBeneficioEmpleado:Tipos.ERegistroBeneficioEmpleado={
     empleado:false,
     beneficio:false,
     frecuencia:false,
     monto:false,
     nota:false
   };
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
     let actual = fBeneficioEmpleado.monto;
     let nuevo = e.target.value;
 
     if (Util.VerificaNumero(nuevo) && nuevo.length < 17) {
       fBeneficioEmpleado.monto = nuevo;
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
                 valor: "4"
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
                      valor: fBeneficioEmpleado.empleado.replaceAll('-','')
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
             consultaBeneficiosEmpleado();
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
 
      const consultaBeneficiosEmpleado=async()=>{
          validaCedula();
          try{
           if(!eNominaElegida && empleados.length>0){
                let datos:Tipos.RData={
                 empresa: parseInt($empresaSeleccionada),
                  sp: "ConsultaBeneficiosEmpleados",
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
       fBeneficioEmpleado.empleado='';
         fBeneficioEmpleado.beneficio='';
         fBeneficioEmpleado.monto='';
         fBeneficioEmpleado.frecuencia='';
         fBeneficioEmpleado.nota='';
             beneficiosEmpleado=[];
      }
      const onRetirarBeneficio=(idBeneficio:string)=>{
       $accionModalConfirmacion=(res)=>{
       if(res)
       {
         retirarBeneficio(idBeneficio);
       }
     };
     Util.VerMensajeConfirmacion("Confirme",`Seguro desea eliminar el beneficio: ${beneficiosEmpleado.find(b=>b.id===idBeneficio).descripcionConcepto}`)
      }
      const retirarBeneficio=async (idBeneficio:string)=>{
          try{
           if(!eBeneficioEmpleado.beneficio)
     {
       let datos:Tipos.RData={
         empresa: parseInt($empresaSeleccionada),
             sp: "RetirarBeneficioEmpleado",
             modelo: "RDataRegistro",
             dataParametros: [
               {
                 parametro: "@empleado",
                 tipo: 16,
                 valor: empleados[0].cedula
               },
               {
                 parametro: "@beneficio",
                 tipo: 16,
                 valor: idBeneficio
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
                consultaBeneficiosEmpleado();
                 if(resNomina[0].resultado==0)
                 {
                   //limpiar();
                   Util.VerMensaje("Atencion","Beneficio retirado con exito.");
                 }else{
                   Util.VerMensaje("Error al retirar beneficio",resNomina[0].mensaje);
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

      const validaPeriodo=()=>{
        if(periodoNomina.length<4)
       {
          ePeriodoNomina=true;
       }else {
         ePeriodoNomina=false;
       }
      }
      const validaCedula=()=>{
        if(fBeneficioEmpleado.empleado.length<11)
       {
          eCedula=true;
       }else {
         eCedula=false;
       }
      }
      const validaFrecuencia=()=>{
        if(fBeneficioEmpleado.frecuencia.length<1)
       {
          eBeneficioEmpleado.frecuencia=true;
       }else {
          eBeneficioEmpleado.frecuencia=false;
       }
      }
      const validaMonto=()=>{
        if(fBeneficioEmpleado.monto.length<1)
       {
          eBeneficioEmpleado.monto=true;
       }else {
          eBeneficioEmpleado.monto=false;
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

     let datoRequerido:boolean = true;

     let Titulo:string = 'CARGAR DOCUMENTOS';
     let permisosCrud: any = {
       "c": $accionesAplicables.c,
       "r": $accionesAplicables.r,
       "u": $accionesAplicables.u,
       "d": $accionesAplicables.d,
       "crud": $accionesAplicables.crud
     };
  
</script>
     <!-- content here -->
<main in:fade="{{duration: 500}}">
   <TituloPagina {Titulo}></TituloPagina>
   
   
   <section class="w-full">
   <article class="rounded-b-2xl rounded-t-2xl p-3 mx-[5%]">
   
   
       <section class="pt-0">
  
         <div class="form-control mb-1">
             <div class="input-group relative">
               <LabelHorizontal>Cedula Empleado</LabelHorizontal>
               <MaskedInput 
                 bind:value={fBeneficioEmpleado.empleado} 
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
  
           <div class="form-control mb-1">
            <div class="input-group relative">
              <LabelHorizontal>Tipo de Acción</LabelHorizontal>
              <select bind:value={fBeneficioEmpleado.beneficio}  name="cConcepto"  class="select select-bordered font-normal text-lg text-gray-500 w-[400px]">
               <option disabled={true} selected={true}></option> 
               {#each conceptos as c }
                 <option value={c.id}>{c.descripcion}</option> 
               {/each}
               </select>
              {#if datoRequerido}
                  <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
              {/if}
            </div>
          </div>
  
         <div class="form-control mb-1">
           <div class="input-group relative">
             <LabelHorizontal>Fecha</LabelHorizontal>
             <input 
             value={fBeneficioEmpleado.monto}
             on:input|preventDefault={soloNumerosMonto}
               type="text"
               class="input input-bordered text-lg text-gray-500 w-[400px]"  name="tValor" 
             />
             {#if datoRequerido}
                 <div class="text-xs absolute ml-3 top-0 left-0 block text-red-400">*Campo Requerido*</div>
             {/if}
           </div>
         </div>
  
         <div class="form-control mb-1">
           <div class="input-group">
             <LabelHorizontal>Nota</LabelHorizontal>
             <textarea bind:value={fBeneficioEmpleado.nota}  class="textarea h-[150px] textarea-bordered text-lg w-[400px]" placeholder=""></textarea>
             <button class="btn">Agregar</button>
           </div>
         </div>
  
         {#if beneficiosEmpleado.length>0}
         <h1 class="p-2 bg-base-300 text-center text-lg font-medium mt-2">Acciones de Personal.</h1>
         <section class="w-full">
           <div class="overflow-x-auto">
             <table class="table w-full table-compact mt-[15px]">
               <thead>
                 <tr>
                   <td></td>
                   <th>Nombre del Archivo</th> 
                   <th class="text-right">Descripcion</th> 
                   <th class="text-center">Tipo de Archivo<th> 
                    <th class="text-center">Nota<th> 
                 </tr>
               </thead> 
               <tbody>
                 {#each beneficiosEmpleado as b}
                 <tr>
                   <td>
                     <section>
                      <button class="w-[32px] h-[32px] text-red-500 hover:text-gray-800 cursor-pointer">
                        <MdDelete />
                      </button>
  
                     </section>
                   </td>
                   <td>Vacaciones</td> 
                   <td class="text-right">Lorem ipsum dolor sit amet consectetur, adipisicing elit. Veniam, expedita.</td> 
                   <td class="text-center">PDF<td> 
                    <td class="text-center"> ... <td> 
                 </tr>
                 {/each}
               </tbody>
             </table>
           </div>
         </section>
         {/if}
  
        {/if}
       </section>
       </article>
       
   </section>
  
   </main>