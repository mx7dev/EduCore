using EduCore.Business.DTOs;
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

        public async Task<IEnumerable<AlumnoDto>> ObtenerTodosAsync()
        {
            var alumnos = await _repository.ObtenerTodosAsync();
            return alumnos.Select(a => MapearADto(a));
        }

        public async Task<AlumnoDto?> ObtenerPorIdAsync(int id)
        {
            var alumno = await _repository.ObtenerPorIdAsync(id);
            if (alumno == null) return null;
            return MapearADto(alumno);
        }

        public async Task CrearAlumnoAsync(CrearAlumnoDto dto)
        {
            // 1. Obtener siguiente correlativo
            var correlativo = await _repository.ObtenerSiguienteCorrelativoAsync();

            // 2. Generar código
            var año = DateTime.Now.Year.ToString().Substring(2, 2);
            var codigo = $"{año}{correlativo:D5}";

            // 3. Crear el alumno
            var alumno = new Alumno(
                codigo,
                dto.Dni,
                dto.Nombre,
                dto.ApellidoPaterno,
                dto.ApellidoMaterno,
                dto.Direccion,
                dto.FechaNacimiento,
                dto.NumeroCelular,
                dto.ContactoEmergenciaNombre,
                dto.ContactoEmergenciaTelefono,
                dto.ContactoEmergenciaRelacion,
                dto.CorreoPersonal);

            // 4. Guardar
            await _repository.GuardarAsync(alumno);
        }

        public async Task EliminarAsync(int id)
        {
            await _repository.EliminarAsync(id);
        }

        private AlumnoDto MapearADto(Alumno alumno)
        {
            return new AlumnoDto
            {
                Id = alumno.Id,
                Codigo = alumno.Codigo,
                Dni = alumno.Dni,
                Nombre = alumno.Nombre,
                ApellidoPaterno = alumno.ApellidoPaterno,
                ApellidoMaterno = alumno.ApellidoMaterno,
                Direccion = alumno.Direccion,
                FechaNacimiento = alumno.FechaNacimiento,
                NumeroCelular = alumno.NumeroCelular,
                ContactoEmergenciaNombre = alumno.ContactoEmergenciaNombre,
                ContactoEmergenciaTelefono = alumno.ContactoEmergenciaTelefono,
                ContactoEmergenciaRelacion = alumno.ContactoEmergenciaRelacion,
                CorreoPersonal = alumno.CorreoPersonal,
                CorreoInstitucional = alumno.CorreoInstitucional
            };
        }
    }
}
