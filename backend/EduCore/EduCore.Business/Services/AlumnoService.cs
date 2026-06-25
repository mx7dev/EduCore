using EduCore.Business.Entities;
using EduCore.Business.Interfaces;

namespace EduCore.Business.Services
{
    public class AlumnoService
    {
        private readonly IAlumnoRepository _repository;

        public AlumnoService(IAlumnoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Alumno>> ObtenerTodosAsync()
        {
            return await _repository.ObtenerTodosAsync();
        }

        public async Task<Alumno> ObtenerPorIdAsync(int id)
        {
            return await _repository.ObtenerPorIdAsync(id);
        }

        public async Task CrearAlumnoAsync(
            string dni,
            string nombre,
            string apellidoPaterno,
            string apellidoMaterno,
            string direccion,
            DateTime fechaNacimiento,
            string numeroCelular,
            string contactoEmergenciaNombre,
            string contactoEmergenciaTelefono,
            string contactoEmergenciaRelacion,
            string correoPersonal)
        {
            // 1. Obtener siguiente correlativo
            var correlativo = await _repository.ObtenerSiguienteCorrelativoAsync();

            // 2. Generar código — regla de negocio del proceso
            var año = DateTime.Now.Year.ToString().Substring(2, 2);
            var codigo = $"{año}{correlativo:D5}";

            // 3. Crear el alumno — aquí el Domain valida sus reglas
            var alumno = new Alumno(
                codigo,
                dni,
                nombre,
                apellidoPaterno,
                apellidoMaterno,
                direccion,
                fechaNacimiento,
                numeroCelular,
                contactoEmergenciaNombre,
                contactoEmergenciaTelefono,
                contactoEmergenciaRelacion,
                correoPersonal);

            // 4. Guardar
            await _repository.GuardarAsync(alumno);
        }

        public async Task ActualizarAsync(Alumno alumno)
        {
            await _repository.ActualizarAsync(alumno);
        }

        public async Task EliminarAsync(int id)
        {
            await _repository.EliminarAsync(id);
        }
    }
}
