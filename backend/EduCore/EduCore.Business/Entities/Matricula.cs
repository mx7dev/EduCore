
namespace EduCore.Business.Entities
{
    public class Matricula
    {
        public int Id { get; private set; }
        public int AlumnoId { get; private set; }
        public Alumno Alumno { get; private set; }
        public int SeccionId { get; private set; }
        public Seccion Seccion { get; private set; }
        public DateTime FechaMatricula { get; private set; }
        public bool Activo { get; private set; }

        private Matricula() { } // Constructor para EF Core

        public Matricula(int alumnoId, int seccionId)
        {
            if (alumnoId <= 0)
                throw new ArgumentException("El alumno es obligatorio");

            if (seccionId <= 0)
                throw new ArgumentException("La sección es obligatoria");

            AlumnoId = alumnoId;
            SeccionId = seccionId;
            FechaMatricula = DateTime.Now;
            Activo = true;
        }

        public void Anular()
        {
            Activo = false;
        }
    }
}
