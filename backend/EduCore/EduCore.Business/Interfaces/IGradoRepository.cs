using EduCore.Business.Entities;

namespace EduCore.Business.Interfaces
{
    public interface IGradoRepository
    {
        Task<Grado?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Grado>> ObtenerTodosAsync();
        Task<IEnumerable<Grado>> ObtenerPorNivelAsync(int nivel);
        Task<bool> ExisteAsync(int numero, int nivel);
        Task GuardarAsync(Grado grado);
    }
}
