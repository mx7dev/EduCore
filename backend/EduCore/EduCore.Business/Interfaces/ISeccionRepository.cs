using EduCore.Business.DTOs;
using EduCore.Business.Entities;

namespace EduCore.Business.Interfaces
{
    public interface ISeccionRepository
    {
        Task<SeccionDto?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<SeccionDto>> ObtenerTodosAsync();
        Task<IEnumerable<SeccionDto>> ObtenerPorPeriodoAsync(int periodoId);
        Task<bool> ExisteAsync(string nombre, int gradoId, int periodoId);
        Task GuardarAsync(Seccion seccion);
        Task ActualizarAsync(Seccion seccion);
    }
}
