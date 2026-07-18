using EduCore.Business.Entities;

namespace EduCore.Business.Interfaces
{
    public interface IProfesorRepository
    {
        Task<Profesor?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Profesor>> ObtenerTodosAsync();
        Task<bool> ExistePorDniAsync(string dni);
        Task GuardarAsync(Profesor profesor);
        Task ActualizarAsync(Profesor profesor);
        Task EliminarAsync(int id);
    }
}
