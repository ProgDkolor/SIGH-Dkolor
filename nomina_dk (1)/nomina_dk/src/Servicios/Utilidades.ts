
import {Base64} from 'js-base64';
import {
    verModalMensaje,
    mensajeModal,
    tituloModal,
    verModalConfirmacion,
    tituloModalConfirmacion,
    mensajeModalConfirmacion,
    accionModalConfirmacion
} from '../Modelo/Datos'

import { toast } from '@zerodevx/svelte-toast'
export const verNotificacion=(mensaje:string)=>{
  toast.pop();
  toast.push(mensaje,
  { 
    duration: 2500,
    pausable: true,
     theme: {
              '--toastBackground': '#F56565',
              '--toastBarBackground': '#C53030'
            },
    });

}
export const VerificaNumero=(value)=> {
    return !isNaN(value);
}

export const VerMensaje=(titulo:string,mensaje:string)=>{
    tituloModal.set(titulo);
    mensajeModal.set(mensaje);
    verModalMensaje.set(true);
}

export const VerMensajeConfirmacion=(titulo:string,mensaje:string)=>{
    tituloModalConfirmacion.set(titulo);
    mensajeModalConfirmacion.set(mensaje);
    verModalConfirmacion.set(true);
}

const emailRegex = RegExp(
    /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/
  );

  const tarjetaVisa = RegExp(
    /^(?:4[0-9]{12}(?:[0-9]{3})?)$/
  );

  const tarjetaMastercard = RegExp(
    /^(?:5[1-5][0-9]{14})$/
  );

  const tarjetaAmerican = RegExp(
    /^(?:3[47][0-9]{13})$/
  );

  export function ValidarEmail(email) {
    const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

export function ValidarVisa(tarjeta) {
    return tarjetaVisa.test(String(tarjeta).toLowerCase());
}

export function ValidarMastercard(tarjeta) {
    return tarjetaMastercard.test(String(tarjeta).toLowerCase());
}

export function ValidarAmerican(tarjeta) {
    return tarjetaAmerican.test(String(tarjeta).toLowerCase());
}

export function GeneraToken():string{
    let res:string;
    let agente=navigator.userAgent.replace(/\s+/g, '').toUpperCase();
    let inicia=agente.substr(0,1);
    let longitud=agente.length;
    let finaliza=agente.substr((longitud-1),1);
    res=Base64.encode(Base64.encode(Evalua(finaliza)+agente+Evalua(inicia)));
    return res;
}
 export function Evalua(letra:string):string
{
    let res:string='AAA';
    let grupo1=[
        'A',
        'B',
        'C',
        'D',
        'E',
        'F',
        'G',
        'H',
        'I',
        'J',
    ]
    let grupo2=[
        'K',
        'L',
        'M',
        'N',
        'O',
        'P',
        'Q',
        'R',
        'S',
        'T',
    ]
    let grupo3=[
        'V',
        'W',
        'X',
        'Y',
        'Z',
    ]
    let grupo4=[
        '1',
        '2',
        '3',
        '4',
        '5',
    ]
    let grupo5=[
        '6',
        '7',
        '8',
        '9',
        '0',
    ]
    if(grupo1.includes(letra))
    {
        res='ZAQ';
    }
    if(grupo2.includes(letra))
    {
        res='XSW ';
    }
    if(grupo3.includes(letra))
    {
        res='CDE';	
    }
    if(grupo4.includes(letra))
    {
        res='VFR';
    }
    if(grupo5.includes(letra))
    {
        res='MJU';
    }
    return res;
}