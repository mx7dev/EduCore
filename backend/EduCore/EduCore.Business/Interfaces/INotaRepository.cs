using EduCore.Business.Entities;
using EduCore.Business.Enums;

namespace EduCore.Business.Interfaces
{
    public interface INotaRepository
    {
        Task<Nota?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Nota>> ObtenerPorMatriculaAsync(int matriculaId);
        Task<IEnumerable<Nota>> ObtenerPorMatriculaYCursoAsync(int matriculaId, int cursoId);
        Task<bool> ExisteAsync(int matriculaId, int cursoId, Bimestre bimestre);
        Task GuardarAsync(Nota nota);
        Task ActualizarAsync(Nota nota);
    }
}
