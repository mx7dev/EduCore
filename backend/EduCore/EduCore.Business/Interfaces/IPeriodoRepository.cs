using EduCore.Business.Entities;

namespace EduCore.Business.Interfaces
{
    public interface IPeriodoRepository
    {
        Task<Periodo?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Periodo>> ObtenerTodosAsync();
        Task<Periodo?> ObtenerActivoAsync();
        Task<bool> ExistePorAñoAsync(int año);
        Task GuardarAsync(Periodo periodo);
        Task ActualizarAsync(Periodo periodo);
    }
}
