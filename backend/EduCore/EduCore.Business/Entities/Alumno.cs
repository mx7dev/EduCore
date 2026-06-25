
namespace EduCore.Business.Entities
{
    public class Alumno
    {
        public int Id { get; private set; }
        public string Codigo { get; private set; }        // obligatorio
        public string Dni { get; private set; }            // obligatorio
        public string Nombre { get; private set; }         // obligatorio
        public string ApellidoPaterno { get; private set; } // obligatorio
        public string? ApellidoMaterno { get; private set; } // opcional
        public string? Direccion { get; private set; }     // opcional
        public DateTime FechaNacimiento { get; private set; } // obligatorio
        public string? NumeroCelular { get; private set; } // opcional
        public string? ContactoEmergenciaNombre { get; private set; }    // opcional
        public string? ContactoEmergenciaTelefono { get; private set; }  // opcional
        public string? ContactoEmergenciaRelacion { get; private set; }  // opcional
        public string? CorreoPersonal { get; private set; }  // opcional
        public string? CorreoInstitucional { get; private set; } // opcional

        public Alumno(
            string codigo,
            string dni,
            string nombre,
            string apellidoPaterno,
            string apellidoMaterno,
            string direccion,
            DateTime fechaNacimiento,
            string numeroCelular,
            string contactoEmergenciaNombre,
            string contactoEmergenciaTelefono,
            string contactoEmergenciaRelacion,
            string correoPersonal)
        {
            // Reglas de negocio — Domain
            if (string.IsNullOrWhiteSpace(dni))
                throw new ArgumentException("El DNI es obligatorio");

            if (dni.Length != 8)
                throw new ArgumentException("El DNI debe tener 8 dígitos");

            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio");

            if (string.IsNullOrWhiteSpace(apellidoPaterno))
                throw new ArgumentException("El apellido paterno es obligatorio");

            if (fechaNacimiento > DateTime.Now)
                throw new ArgumentException("La fecha de nacimiento no puede ser futura");

            // Asignación
            Codigo = codigo;
            Dni = dni;
            Nombre = nombre;
            ApellidoPaterno = apellidoPaterno;
            ApellidoMaterno = apellidoMaterno;
            Direccion = direccion;
            FechaNacimiento = fechaNacimiento;
            NumeroCelular = numeroCelular;
            ContactoEmergenciaNombre = contactoEmergenciaNombre;
            ContactoEmergenciaTelefono = contactoEmergenciaTelefono;
            ContactoEmergenciaRelacion = contactoEmergenciaRelacion;
            CorreoPersonal = correoPersonal;
        }

        // Se asigna en otra etapa del proceso
        public void AsignarCorreoInstitucional(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                throw new ArgumentException("El correo institucional no puede estar vacío");

            CorreoInstitucional = correo;
        }
    }
}
