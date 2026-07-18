using EduCore.Business.Entities;

namespace EduCore.Business.Interfaces
{
    public interface IMatriculaRepository
    {
        Task<IEnumerable<Matricula>> ObtenerTodosAsync();
        Task<Matricula?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Matricula>> ObtenerPorSeccionAsync(int seccionId);
        Task<bool> ExisteMatriculaEnPeriodoAsync(int alumnoId, int periodoId);
        Task GuardarAsync(Matricula matricula);
        Task ActualizarAsync(Matricula matricula);
    }
}
