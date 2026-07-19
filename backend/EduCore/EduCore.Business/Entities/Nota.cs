using EduCore.Business.Enums;

namespace EduCore.Business.Entities
{
    public class Nota
    {
        public int Id { get; private set; }
        public int MatriculaId { get; private set; }
        public Matricula Matricula { get; private set; }
        public int CursoId { get; private set; }
        public Curso Curso { get; private set; }
        public Bimestre Bimestre { get; private set; }
        public decimal Calificacion { get; private set; }
        public string? Comentario { get; private set; }

        private Nota() { }

        public Nota(int matriculaId, int cursoId, Bimestre bimestre, decimal calificacion, string? comentario)
        {
            if (matriculaId <= 0)
                throw new ArgumentException("La matrícula es obligatoria");

            if (cursoId <= 0)
                throw new ArgumentException("El curso es obligatorio");

            if (calificacion < 0 || calificacion > 20)
                throw new ArgumentException("La calificación debe estar entre 0 y 20");

            MatriculaId = matriculaId;
            CursoId = cursoId;
            Bimestre = bimestre;
            Calificacion = calificacion;
            Comentario = comentario;
        }

        public void ActualizarCalificacion(decimal calificacion, string? comentario)
        {
            if (calificacion < 0 || calificacion > 20)
                throw new ArgumentException("La calificación debe estar entre 0 y 20");

            Calificacion = calificacion;
            Comentario = comentario;
        }
    }
}
