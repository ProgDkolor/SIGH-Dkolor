

import * as Parametros from '../Modelo/Parametros';

export const ListarUsuarios = async () => {
    let listadoDeUsuarios:any = {
        "Resultado": false,
        "Mensaje": "",
        "Data": ""
    };
    try{
        //...
        let url:string = `${Parametros.UrlBase}/api/Empleado/listar-usuarios`;
        let resp:any = await fetch(url, {
            method: "GET"
        });
        let result:any = await resp.json();

        listadoDeUsuarios.Resultado = true;
        listadoDeUsuarios.Mensaje = "Exito";
        listadoDeUsuarios.Data = result.data;
    }catch(error){
        //...
        listadoDeUsuarios.Resultado = false;
        listadoDeUsuarios.Mensaje = "Ocurrio un error";
        listadoDeUsuarios.Data = null;
    }
    return listadoDeUsuarios;
}

export async function NuevoUsuario (usuario:any): Promise<any> {
    //...
    let result = {
        "Resultado": false,
        "Mensaje": "",
        "Data": null
    };
    try{
        //...
        let url = `${Parametros.UrlBase}/api/Empleado/nuevo-usuario`;
        let resp = await fetch(url, {
            method: "POST",
            mode: "cors",
            body:JSON.stringify(usuario),
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Content-type": "application/json; charset=UTF-8"
            }
        });
        result.Resultado = true;
        result.Mensaje = "Datos registrado con Exito";
    }catch(error){
        //...
        result.Resultado = false;
        result.Mensaje = error;
    }
    return result;
}

export const BuscarUsuario = async () => {
    //....
}

export const EditarUsuario = async () =>{
    //...
}

export const BloquearUsuario = async () => {
    //...
}