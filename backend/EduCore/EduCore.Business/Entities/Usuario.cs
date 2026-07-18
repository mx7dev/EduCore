namespace EduCore.Business.Entities
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string Rol { get; private set; }
        public bool Activo { get; private set; }

        public Usuario(
            string nombre,
            string apellido,
            string email,
            string passwordHash,
            string rol)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El email es obligatorio");

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("La contraseña es obligatoria");

            if (string.IsNullOrWhiteSpace(rol))
                throw new ArgumentException("El rol es obligatorio");

            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            PasswordHash = passwordHash;
            Rol = rol;
            Activo = true;
        }

        public void Desactivar()
        {
            Activo = false;
        }
    }
}
