
namespace EduCore.Business.Entities
{
    public class Profesor
    {
        public int Id { get; private set; }
        public string Dni { get; private set; }
        public string Nombre { get; private set; }
        public string ApellidoPaterno { get; private set; }
        public string? ApellidoMaterno { get; private set; }
        public string Especialidad { get; private set; }
        public string CorreoElectronico { get; private set; }
        public string? Direccion { get; private set; }
        public DateTime? FechaNacimiento { get; private set; }
        public string? NumeroCelular { get; private set; }
        public bool Activo { get; private set; }

        public Profesor(
            string dni,
            string nombre,
            string apellidoPaterno,
            string? apellidoMaterno,
            string especialidad,
            string correoElectronico,
            string? direccion,
            DateTime? fechaNacimiento,
            string? numeroCelular)
        {
            if (string.IsNullOrWhiteSpace(dni))
                throw new ArgumentException("El DNI es obligatorio");

            if (dni.Length != 8)
                throw new ArgumentException("El DNI debe tener 8 dígitos");

            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio");

            if (string.IsNullOrWhiteSpace(apellidoPaterno))
                throw new ArgumentException("El apellido paterno es obligatorio");

            if (string.IsNullOrWhiteSpace(especialidad))
                throw new ArgumentException("La especialidad es obligatoria");

            if (string.IsNullOrWhiteSpace(correoElectronico))
                throw new ArgumentException("El correo electrónico es obligatorio");

            Dni = dni;
            Nombre = nombre;
            ApellidoPaterno = apellidoPaterno;
            ApellidoMaterno = apellidoMaterno;
            Especialidad = especialidad;
            CorreoElectronico = correoElectronico;
            Direccion = direccion;
            FechaNacimiento = fechaNacimiento;
            NumeroCelular = numeroCelular;
            Activo = true;
        }

        public void Desactivar()
        {
            Activo = false;
        }

        public void ActualizarEspecialidad(string especialidad)
        {
            if (string.IsNullOrWhiteSpace(especialidad))
                throw new ArgumentException("La especialidad no puede estar vacía");

            Especialidad = especialidad;
        }
    }
}
