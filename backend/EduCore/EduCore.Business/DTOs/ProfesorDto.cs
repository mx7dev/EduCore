namespace EduCore.Business.DTOs
{
    public class ProfesorDto
    {
        public int Id { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string Especialidad { get; set; }
        public string CorreoElectronico { get; set; }
        public string? Direccion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? NumeroCelular { get; set; }
        public bool Activo { get; set; }
    }
}
