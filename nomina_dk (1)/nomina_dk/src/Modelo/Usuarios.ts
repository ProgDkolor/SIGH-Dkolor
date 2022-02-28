

export interface Usuarios{
    Usuario: string | null;
    Nombre: string | null;
    Apellido: string | null;
    Clave: string | null;
    perfil: number | 0;
    Fecha: Date | null;
    Estado: number | 0;
    Cedula: string | null;
    Modelo: number | 0;
    CargoEmpleadosId: number | 0;
    RolUsuarioId: number | 0;
}