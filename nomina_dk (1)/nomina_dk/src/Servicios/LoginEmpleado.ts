
import type * as EmpleadoToken from '../Modelo/EmpleadoToken';
import * as Parametros from './../Modelo/Parametros';
import * as Usuarios from '../Modelo/Tipos';
import type { Token } from 'src/Modelo/Token';

export async function GetToken(
    id:string, usuario:string, cargoEmpleadoId:number, rolUsuarioId:number):Promise<any> {
    //...
    let token: any;
    try{
        //...
        let url = `${Parametros.UrlBase}/api/Login/GetToken`;
        let empleado = {
            "Id": id,
            "Usuario": usuario,
            "CargoEmpleadoId": cargoEmpleadoId,
            "RolUsuarioId": rolUsuarioId
        };
        let resp = await fetch(url,{
            method:"POST",
            mode:"cors",
            body:JSON.stringify(empleado),
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Content-type": "application/json; charset=UTF-8"
            }
        });
        token = await resp.json();
    }catch(error){
        //...
    }
    return token.tokenJSON;
}