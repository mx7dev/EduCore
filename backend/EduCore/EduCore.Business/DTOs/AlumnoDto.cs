namespace EduCore.Business.DTOs
{
    public class AlumnoDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? NumeroCelular { get; set; }
        public string? ContactoEmergenciaNombre { get; set; }
        public string? ContactoEmergenciaTelefono { get; set; }
        public string? ContactoEmergenciaRelacion { get; set; }
        public string? CorreoPersonal { get; set; }
        public string? CorreoInstitucional { get; set; }
    }
}
