
namespace EduCore.Business.Entities
{
    public class Periodo
    {
        public int Id { get; private set; }
        public int Año { get; private set; }
        public string? Descripcion { get; private set; }
        public bool Activo { get; private set; }

        public Periodo(int año, string? descripcion)
        {
            if (año < 2000 || año > 2100)
                throw new ArgumentException("El año no es válido");

            Año = año;
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
