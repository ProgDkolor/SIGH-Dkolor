export enum DbType {
    AnsiString = 0,
    Binary = 1,
    Byte = 2,
    Boolean = 3,
    Currency = 4,
    Date = 5,
    DateTime = 6,
    Decimal = 7,
    Double = 8,
    Guid = 9,
    Int16 = 10,
    Int32 = 11,
    Int64 = 12,
    Object = 13,
    SByte = 14,
    Single = 15,
    String = 16,
    Time = 17,
    UInt16 = 18,
    UInt32 = 19,
    UInt64 = 20,
    VarNumeric = 21,
    AnsiStringFixedLength = 22,
    StringFixedLength = 23,
    Xml = 25,
    DateTime2 = 26,
    DateTimeOffset = 27
}
export interface Listado {
    id: number;
    descripcion: string;
    estado: boolean;
}
export interface DReporteNomina {
    empresa: number;
    idNomina: string;
    idReporte: number;
    nomina: string;
    reporte: string;
    nombreArchivo:string;
}

export interface DArchivoBanco {
    empresa: number;
    nomina: string;
    banco: number;
    nombreArchivo:string;
}

export interface RData {
    empresa:number;
    sp: string;
    modelo: string;
    dataParametros: Parametros[];
}
export interface Parametros {
    parametro: string;
    tipo: DbType;
    valor: any;
}
export interface DRegistroNomina {
    tipo: string;
    anio: string;
    mes: string;
    quincena: string;
    nota: string;
}
export interface ConsultaNovedad {
    id: string;
    nomina: string;
    empleado: string;
    nombreEmpleado: string;
    concepto: number;
    descripcionConcepto: string;
    cantidad: number;
    valor: number;
    nota: string;
    condicion: string;
    estado: boolean;
}
export interface DRegistroNovedad {
    empleado: string;
    concepto: string;
    cantidad: string;
    nota: string;
    idNomina: string;
}
export interface EDRegistroNovedad {
    empleado: boolean;
    concepto: boolean;
    cantidad: boolean;
    nota: boolean;
    idNomina: boolean;
}
export interface DRegistroBeneficioEmpleado {
    empleado: string;
    beneficio: string;
    frecuencia: string;
    monto: string;
    nota: string;
}
export interface ERegistroBeneficioEmpleado {
    empleado: boolean;
    beneficio: boolean;
    frecuencia: boolean;
    monto: boolean;
    nota: boolean;
}
export interface ERegistroNomina {
    tipo: boolean;
    anio: boolean;
    mes: boolean;
    quincena: boolean;
    nota: boolean;
}
export interface RDataRegistro {
    resultado: number;
    mensaje: string;
    id: string;
}
export interface UsuarioActivo {
    usuario: string;
    nombre: string;
}
export interface GeneraNominaPrevia {
    periodo: string;
    fecha: string | null;
    modulo: string;
    dependencia: string;
    cedula: string;
    nombre: string;
    apellidos: string;
    cuentaBanco: string;
    fechaIngreso: string | null;
    sueldo: number;
    sueldoQuincenal: number;
    pagoEfectivo: number;
    transferencia: number;
    cheque: number;
    dietas: number;
    horasExtras: number;
    isr: number;
    afp: number;
    tsS_SFS: number;
    sueldoNeto: number;
    descripcion:string;
    cargoEmpleado:string;
}
export interface ConsultaNominaPeriodo {
    id: string;
    descripcion: string;
}
export interface ComprobantePago {
    id: string;
    descripcionNomina: string;
    tipo: number;
    cedula: string;
    nombre: string;
    apellido: string;
    fechaEntrada: string;
    cargo: number;
    concepto: number;
    cantidad: number;
    salarioMensual: number;
    tipoNomina: string;
    descripcionCargo: string;
    medioPago: number | null;
    descripcionMedioPago: string;
    cuentaBancaria: string;
    descripcionPago: string;
    ingreso: number;
    descuento: number;
    descripcionConcepto: string;
}

export interface ResData<T> {
    exito: boolean;
    mensaje: string;
    data: T[];
}
export interface Empleado {
    id: string;
    codigo: number;
    cedula: string;
    nombre: string;
    apellido: string;
    cargo: string;
    fechaEntrada: string;
    fechaRegistro: string;
    salario: number;
    salarioEspecial: number;
    salarioTotal: number;
    salarioDiario: number;
    hora35: number;
    hora100: number;
    contrato: number;
    estado: boolean;
    situacion: number;
    medioPago: string | null;
    cuentaBancaria: string;
    banco: string | null;
    departamento: string | null;
    telefono: string;
    correo: string;
    direccion: string;
    nota: string;
    modulo: string | null;
}
export interface FEmpleado {
    cedula: string;
    nombre: string;
    apellido: string;
    cargo: string;
    fechaEntrada: string;
    fechaRegistro: string;
    salario: string;
    salarioEspecial: string;
    medioPago: string | null;
    cuentaBancaria: string;
    banco: string | null;
    departamento: string | null;
    telefono: string;
    correo: string;
    direccion: string;
    nota: string;
    modulo: string | null;
}
export interface EEmpleado {
    cedula: boolean;
    nombre: boolean;
    apellido: boolean;
    cargo: boolean;
    fechaEntrada: boolean;
    salario: boolean;
    salarioEspecial: boolean;
    medioPago: boolean;
    banco: boolean;
    modulo: boolean;
    departamento: boolean;
}
export interface TiposHoras {
    id: number;
    descripcion: string;
}
export interface DCambioClave {
    claveActual: string;
    claveNueva: string;
    confirmacionClave: string;
}
export interface ECambioClave {
    claveActual: boolean;
    claveNueva: boolean;
    confirmacionClave: boolean;
    claveCoincide: boolean;
}
export interface Conceptos {
    id: number;
    descripcion: string;
    tipo: number;
    categoria: number;
    calculo: number;
    valor: number;
    estado: boolean;
}
export interface BeneficiosEmpleados {
    id: string;
    empleado: string;
    concepto: number;
    frecuencia: number;
    quincena: number;
    valor: number;
    nota: string;
    condicion: string;
    descripcionFrecuencia: string;
    descripcionConcepto:string;
    estado: boolean;
}
export interface Empresas {
    id: number;
    urlApi: string;
    nombre: string;
    bd: string;
}

export interface Usuarios {
    resultado: number | null;
    mensaje: string;
    id: string;
    usuario: string;
    nombre: string;
    apellido: string;
    perfil: number | null;
    fecha: string | null;
    estado: number | null;
    cedula: string;
    modulo: number | null;
    rolUsuarioId: number | null;
    cargoEmpleadosId:number | null;
}

export interface PagoVacaciones {
    id: string;
    nomina: string;
    empleado: string;
    concepto: number;
    cantidad: number;
    valor: number;
    nota: string;
    condicion: string;
    estado: boolean;
}

export interface PagoHorasExtras {
    id: string;
    nomina: string;
    empleado: string;
    concepto: number;
    cantidad: number;
    valor: number;
    nota: string;
    condicion: string;
    estado: boolean;
}

export interface Token{
    TokenJSON:string,
    FechaDeExpiracion: Date
}

export interface TokenEmpleado{
    Id: string,
    Usuario : string,
    Correo: string,
    CargoEmpleadoId: number,
    RolUsuarioId: number
}

export interface AccionesAplicables{
    C: number,
    R: number,
    U: number,
    D: number,
    CRUD: number
}