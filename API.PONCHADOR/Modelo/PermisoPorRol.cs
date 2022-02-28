namespace API.PONCHADOR.Modelo
{
    public class PermisoPorRol
    {
        public int Id { get; set; }
        public int RolUsuarioId { get; set; }
        public int PermisoUsuarioId { get; set; }
        public int CargoEmpleadoId { get; set; }
        public string IdUsuario { get; set; }
        public int C { get; set; }
        public int R { get; set; }
        public int U { get; set; }
        public int D { get; set; }
        public int CRUD { get; set; }
    }
}
