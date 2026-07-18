using EduCore.Business.Entities;

namespace EduCore.Business.Interfaces
{
    public interface ICursoRepository
    {
        Task<Curso?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Curso>> ObtenerTodosAsync();
        Task<bool> ExistePorNombreAsync(string nombre);
        Task<int> ObtenerSiguienteCorrelativoAsync();
        Task GuardarAsync(Curso curso);
        Task ActualizarAsync(Curso curso);
        Task EliminarAsync(int id);
    }
}
