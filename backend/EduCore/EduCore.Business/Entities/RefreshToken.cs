
namespace EduCore.Business.Entities
{
    public class RefreshToken
    {
        public int Id { get; private set; }
        public string Token { get; private set; }
        public DateTime Expiracion { get; private set; }
        public bool Usado { get; private set; }
        public bool Revocado { get; private set; }
        public int UsuarioId { get; private set; }
        public Usuario Usuario { get; private set; }

        private RefreshToken() { } // Constructor para EF Core

        public RefreshToken(string token, int usuarioId, int diasExpiracion = 7)
        {
            Token = token;
            UsuarioId = usuarioId;
            Expiracion = DateTime.Now.AddDays(diasExpiracion);
            Usado = false;
            Revocado = false;
        }

        public bool EsValido() => !Usado && !Revocado && Expiracion > DateTime.Now;

        public void Usar()
        {
            Usado = true;
        }

        public void Revocar()
        {
            Revocado = true;
        }
    }
}
