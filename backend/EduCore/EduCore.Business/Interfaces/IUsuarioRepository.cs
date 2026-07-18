using EduCore.Business.Entities;

namespace EduCore.Business.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObtenerPorEmailAsync(string email);
        Task<bool> ExistePorEmailAsync(string email);
        Task GuardarAsync(Usuario usuario);
        Task<RefreshToken?> ObtenerRefreshTokenAsync(string token);
        Task GuardarRefreshTokenAsync(RefreshToken refreshToken);
        Task ActualizarRefreshTokenAsync(RefreshToken refreshToken);
    }
}
