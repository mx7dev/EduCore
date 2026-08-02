
namespace EduCore.Business.Entities
{
    public class Periodo
    {
        public int Id { get; private set; }
        public int Anio { get; private set; }
        public string? Descripcion { get; private set; }
        public bool Activo { get; private set; }

        public Periodo(int anio, string? descripcion)
        {
            if (anio < 2000 || anio > 2100)
                throw new ArgumentException("El año no es válido");

            Anio = anio;
            Descripcion = descripcion;
            Activo = true;
        }

        public void Desactivar()
        {
            Activo = false;
        }

        public void Activar()
        {
            Activo = true;
        }
    }
}
