
namespace EduCore.Business.DTOs
{
    public class LibretaDto
    {
        public string NombreAlumno { get; set; }
        public string CodigoAlumno { get; set; }
        public List<NotaCursoDto> Cursos { get; set; }
    }

    public class NotaCursoDto
    {
        public string NombreCurso { get; set; }
        public decimal? B1 { get; set; }
        public decimal? B2 { get; set; }
        public decimal? B3 { get; set; }
        public decimal? B4 { get; set; }
        public decimal? NotaFinal { get; set; }
    }
}
