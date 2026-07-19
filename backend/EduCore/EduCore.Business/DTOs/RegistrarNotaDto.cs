
namespace EduCore.Business.DTOs
{
    public class RegistrarNotaDto
    {
        public int MatriculaId { get; set; }
        public int CursoId { get; set; }
        public int Bimestre { get; set; }
        public decimal Calificacion { get; set; }
        public string? Comentario { get; set; }
    }
}
