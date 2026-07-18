using EduCore.Business.Enums;

namespace EduCore.Business.Entities
{
    public class Seccion
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public Turno Turno { get; private set; }
        public int GradoId { get; private set; }
        public Grado Grado { get; private set; }
        public int PeriodoId { get; private set; }
        public Periodo Periodo { get; private set; }
        public bool Activo { get; private set; }

        private Seccion() { } // Constructor para EF Core

        public Seccion(string nombre, Turno turno, int gradoId, int periodoId)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de la sección es obligatorio");

            Nombre = nombre;
            Turno = turno;
            GradoId = gradoId;
            PeriodoId = periodoId;
            Activo = true;
        }

        public void Desactivar()
        {
            Activo = false;
        }
    }
}
