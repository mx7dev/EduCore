using EduCore.Business.Entities;

namespace EduCore.Business.Interfaces
{
    public interface IAlumnoRepository
    {
        Task<Alumno> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Alumno>> ObtenerTodosAsync();
        Task<int> ObtenerSiguienteCorrelativoAsync();
        Task GuardarAsync(Alumno alumno);
        Task ActualizarAsync(Alumno alumno);
        Task EliminarAsync(int id);
    }
}
