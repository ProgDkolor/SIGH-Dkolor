import type * as Tipos from './../Modelo/Tipos'
import * as Parametros from './../Modelo/Parametros'
import * as Util from './Utilidades'
import {LoginActivo,empresaSeleccionada} from '../Modelo/Datos'

export async function   consultaIp():Promise<string>{
    let dirIp:string='';
    try{
        console.log('voy a consultar la ip');
        let agente=navigator.userAgent.replace(/\s+/g, '').toUpperCase();
    let url=`${Parametros.UrlBase}/Sql/consultaIp`
    let data=await fetch(url);
    var resIp= await data.json();
    dirIp=resIp.ip;
    }catch(error){
        console.log('Error al conectarse al api.');
    }
    return(dirIp);
}

export async function  DescargaReporteNomina(datos:Tipos.DReporteNomina):Promise<any>{
    let resProceso:boolean=true;
    try{
        let resData;
        let agente=navigator.userAgent.replace(/\s+/g, '').toUpperCase();
    let url=`${Parametros.UrlBase}/Sql/ReporteNomina`;
    console.log(datos);
    let res=await fetch(`${url}?empresa=${datos.empresa}&nomina=${datos.idNomina}&reporte=${datos.idReporte}
    &descripcionNomina=${datos.nomina}&descripcionReporte=${datos.reporte}`);
    resData =await res.blob();
      console.log("Inicio data descarga");
      console.log(resData);
      console.log('data escrita');
      var a = document.createElement("a");
      a.href = window.URL.createObjectURL(resData);
      a.download = `${datos.nombreArchivo}.xls`;
      a.click();
    }catch(error){
        resProceso=false;
    }
    return(resProceso);
}

export async function  DescargaArchivoBanco(datos:Tipos.DArchivoBanco):Promise<any>{
    let resProceso:boolean=true;
    try{
        let resData;
        let agente=navigator.userAgent.replace(/\s+/g, '').toUpperCase();
    let url=`${Parametros.UrlBase}/Sql/ReportePagoBanco`;
    console.log(datos);
    let res=await fetch(`${url}?empresa=${datos.empresa}&nomina=${datos.nomina}&banco=${datos.banco}`);
    resData =await res.blob();
      console.log("Inicio data descarga");
      console.log(resData);
      console.log('data escrita');
      var a = document.createElement("a");
      a.href = window.URL.createObjectURL(resData);
      a.download = `${datos.nombreArchivo}.txt`;
      a.click();
    }catch(error){
        resProceso=false;
    }
    return(resProceso);
}

export async function  ConsultaBd(datos:Tipos.RData):Promise<any>{
    let resData;
    try{
        console.log('voy a consultar');
        let agente=navigator.userAgent.replace(/\s+/g, '').toUpperCase();
    let url=`${Parametros.UrlBase}/Sql/ConsultaBd`;
    console.log(datos);
    let res=await fetch(url,{
        method:"POST",
        body:JSON.stringify(datos),
        headers: {
            "Content-type": "application/json; charset=UTF-8"
        }
    });
    resData =await res.json();
      console.log("Inicio log");
      console.log(resData);
      console.log('data escrita');
    }catch(error){
        console.log('error encontrado');
        console.log(error);
        console.log('final error');
        resData.exito=false,
        resData.mensaje="No se pudo acceder al servidor, intente nuevamente.";
        console.log('Error al conectarse al api.');
    }
    return(resData);
}

export async function  ConsultaBdMaster(datos:Tipos.RData):Promise<any>{
    let resData;
    try{
        datos.empresa=1;
        console.log('voy a consultar');
        let agente=navigator.userAgent.replace(/\s+/g, '').toUpperCase();
        let url=`${Parametros.UrlBase}/Sql/ConsultaBdMaster`;
        console.log(datos);
        let res=await fetch(url,{
            method:"POST",
            mode:"cors",
            body:JSON.stringify(datos),
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Content-type": "application/json; charset=UTF-8"
            }
        });
        resData =await res.json();
        console.log("Inicio log");
        console.log(resData);
        console.log('data escrita');
    }catch(error){
        console.log('error encontrado');
        console.log(error);
        console.log('final error');
        resData.exito=false,
        resData.mensaje="No se pudo acceder al servidor, intente nuevamente.";
        console.log('Error al conectarse al api.');
    }
    return(resData);
}

export async function  ListadoEmpresas():Promise<any>{
    let resData;
    try{
        console.log('voy a consultar');
        let agente=navigator.userAgent.replace(/\s+/g, '').toUpperCase();
    let url=`${Parametros.UrlBase}/Sql/ListadoEmpresas`;
    let res=await fetch(url);
    resData =await res.json();
      console.log("Inicio log");
      console.log(resData);
      console.log('data escrita');
    }catch(error){
        console.log('error encontrado');
        console.log(error);
        console.log('final error');
        resData.exito=false,
        resData.mensaje="No se pudo acceder al servidor, intente nuevamente.";
        console.log('Error al conectarse al api.');
    }
    return(resData);
}

export async function  RegistroBd(datos:Tipos.RData):Promise<any>{
    let resData;
    try{
        console.log('voy a consultar');
        let agente=navigator.userAgent.replace(/\s+/g, '').toUpperCase();
    let url=`${Parametros.UrlBase}/Sql/RegistroBd`;
    console.log(datos);
    let res=await fetch(url,{
        method:"POST",
        body:JSON.stringify(datos),
        headers: {
            "Content-type": "application/json; charset=UTF-8"
        }
    });
    resData =await res.json();
      console.log("Inicio log");
      console.log(resData);
      console.log('data escrita');
    }catch(error){
        console.log('error encontrado');
        console.log(error);
        console.log('final error');
        resData.exito=false,
        resData.mensaje="No se pudo acceder al servidor, intente nuevamente.";
        console.log('Error al conectarse al api.');
    }
    return(resData);
}

export async function  RegistroBdMaster(datos:Tipos.RData):Promise<any>{
    let resData;
    try{
        console.log('voy a consultar');
        let agente=navigator.userAgent.replace(/\s+/g, '').toUpperCase();
    let url=`${Parametros.UrlBase}/Sql/RegistroBdMaster`;
    console.log(datos);
    let res=await fetch(url,{
        method:"POST",
        body:JSON.stringify(datos),
        headers: {
            "Content-type": "application/json; charset=UTF-8"
        }
    });
    resData =await res.json();
      console.log("Inicio log");
      console.log(resData);
      console.log('data escrita');
    }catch(error){
        console.log('error encontrado');
        console.log(error);
        console.log('final error');
        resData.exito=false,
        resData.mensaje="No se pudo acceder al servidor, intente nuevamente.";
        console.log('Error al conectarse al api.');
    }
    return(resData);
}

   