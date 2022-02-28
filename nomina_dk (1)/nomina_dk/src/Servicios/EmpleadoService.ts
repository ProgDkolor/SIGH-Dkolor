
import { ca } from 'date-fns/locale';
import * as Parametros from '../Modelo/Parametros';

export const NuevoEmpleado = () =>{
    //...
}

export const ListarEmpleado = () => {
    //...
}

export const EditarEmpleado = () => {
    //...
}

export const BloquearEmpleado = () => {
    //...
}

export const CargosEmpleados = async () => {
    //...
    let objCargosEmpleados: any | null;
    try{
        //...
        let url: string = `${Parametros.UrlBase}/api/Empleado/listar-cargos-empleados`;
        let resp = await fetch(url);
        let result = await resp.json();
        objCargosEmpleados = result;
    }catch(error){
        //...
        objCargosEmpleados = {
            "Resultado": false,
            "Mensaje": `ocurrio un error: ${error}`
        }
    }
    return objCargosEmpleados;
}

export const RolesUsuarios = async () =>{
    //...
    let objRolesUsuarios: any | null;
    try{
        //...
        let url:string = `${Parametros.UrlBase}/api/Empleado/listar-roles`;
        let resp = await fetch(url);
        let result = await resp.json();
        objRolesUsuarios = result;
    }catch(error){
        //..
    }
    return objRolesUsuarios;
}