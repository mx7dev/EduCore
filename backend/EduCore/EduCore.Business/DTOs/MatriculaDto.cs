
namespace EduCore.Business.DTOs
{
    public class MatriculaDto
    {
        public int Id { get; set; }
        public int AlumnoId { get; set; }
        public string NombreAlumno { get; set; }
        public string CodigoAlumno { get; set; }
        public int SeccionId { get; set; }
        public string NombreSeccion { get; set; }
        public DateTime FechaMatricula { get; set; }
        public bool Activo { get; set; }
    }
}
