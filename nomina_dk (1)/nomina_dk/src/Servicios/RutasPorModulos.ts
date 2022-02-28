import type { Rutas } from 'src/Modelo/Rutas';
import * as Parametros from './../Modelo/Parametros';

export async function BuscarRutasDelModuloEmpleado(rolUsuarioId:number, empleadoId): Promise<any>{
    //...
    let listadoDeARutas: any;

    let objParametrosDeBusqueda = {
        "RolUsuarioId": rolUsuarioId,
        "EmpleadoId": empleadoId
    };

    try{
        //...
        let url = `${Parametros.UrlBase}/api/ControlDeRutas/consultar-rutas`;
        let resp = await fetch(url, {
            method: "POST",
            mode: "cors",
            body: JSON.stringify(objParametrosDeBusqueda),
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Content-Type": "application/json; charset=UTF-8"
            }
        });
        listadoDeARutas = await resp.json();
    }catch(error){
        //...
        return listadoDeARutas;
    }
    return listadoDeARutas.data;
}

export async function BuscarModulosDisponiblesParaElEmpleado(rolUsuarioId:number, empleadoId:string): Promise<any>{
    //...
    let listadoDeARutas: any;

    let objParametrosDeBusqueda = {
        "RolUsuarioId": rolUsuarioId,
        "EmpleadoId": empleadoId
    };

    try{
        //...
        let url = `${Parametros.UrlBase}/api/ControlDeRutas/consultar-rutas`;
        let resp = await fetch(url, {
            method: "POST",
            mode: "cors",
            body: JSON.stringify(objParametrosDeBusqueda),
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Content-Type": "application/json; charset=UTF-8"
            }
        });
        listadoDeARutas = await resp.json();
    }catch(error){
        //...
        return listadoDeARutas;
    }
    return listadoDeARutas.modulosDisponibles;
}

export async function BuscarLasAccionesAplicables(rolUsuarioId:number, empleadoId:string): Promise<any>{
    let accionesAplicables: any;

    let objParametrosDeBusqueda = {
        "RolUsuarioId": rolUsuarioId,
        "EmpleadoId": empleadoId
    };

    try{
        //...
        let url = `${Parametros.UrlBase}/api/ControlDeRutas/consultar-rutas`;
        let resp = await fetch(url, {
            method: "POST",
            mode: "cors",
            body: JSON.stringify(objParametrosDeBusqueda),
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Content-Type": "application/json; charset=UTF-8"
            }
        });
        accionesAplicables = await resp.json();
    }catch(error){
        //...
    }
    return accionesAplicables.aplicablesEnLaRuta;
}