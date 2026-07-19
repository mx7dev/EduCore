
namespace EduCore.Business.DTOs
{
    public class NotaDto
    {
        public int Id { get; set; }
        public int MatriculaId { get; set; }
        public string NombreAlumno { get; set; }
        public string CodigoAlumno { get; set; }
        public int CursoId { get; set; }
        public string NombreCurso { get; set; }
        public string Bimestre { get; set; }
        public decimal Calificacion { get; set; }
        public string? Comentario { get; set; }
    }
}
