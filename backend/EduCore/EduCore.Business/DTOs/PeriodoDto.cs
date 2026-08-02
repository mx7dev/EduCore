namespace EduCore.Business.DTOs
{
    public class PeriodoDto
    {
        public int Id { get; set; }
        public int Anio { get; set; }
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}