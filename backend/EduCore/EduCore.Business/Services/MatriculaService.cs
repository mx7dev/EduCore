using EduCore.Business.DTOs;
using EduCore.Business.Entities;
using EduCore.Business.Exceptions;
using EduCore.Business.Interfaces;

namespace EduCore.Business.Services
{
    public class MatriculaService
    {
        private readonly IMatriculaRepository _repository;
        private readonly ISeccionRepository _seccionRepository;

        public MatriculaService(IMatriculaRepository repository, ISeccionRepository seccionRepository)
        {
            _repository = repository;
            _seccionRepository = seccionRepository;
        }

        public async Task<IEnumerable<MatriculaDto>> ObtenerTodosAsync()
        {
            var matriculas = await _repository.ObtenerTodosAsync();
            return matriculas.Select(m => MapearADto(m));
        }

        public async Task<MatriculaDto?> ObtenerPorIdAsync(int id)
        {
            var matricula = await _repository.ObtenerPorIdAsync(id);
            if (matricula == null) return null;
            return MapearADto(matricula);
        }

        public async Task<IEnumerable<MatriculaDto>> ObtenerPorSeccionAsync(int seccionId)
        {
            var matriculas = await _repository.ObtenerPorSeccionAsync(seccionId);
            return matriculas.Select(m => MapearADto(m));
        }

        public async Task MatricularAlumnoAsync(CrearMatriculaDto dto)
        {
            // Obtener la sección para saber el periodo
            var seccion = await _seccionRepository.ObtenerPorIdAsync(dto.SeccionId);
            if (seccion == null)
                throw new FunctionalException("SECCION_NO_ENCONTRADA", "La sección no existe");

            // Verificar que el alumno no esté ya matriculado en este periodo
            var yaMatriculado = await _repository.ExisteMatriculaEnPeriodoAsync(dto.AlumnoId, seccion.Id);
            if (yaMatriculado)
                throw new FunctionalException("ALUMNO_YA_MATRICULADO", "El alumno ya está matriculado en este periodo");

            var matricula = new Matricula(dto.AlumnoId, dto.SeccionId);
            await _repository.GuardarAsync(matricula);
        }

        public async Task AnularMatriculaAsync(int id)
        {
            var matricula = await _repository.ObtenerPorIdAsync(id);
            if (matricula == null)
                throw new FunctionalException("MATRICULA_NO_ENCONTRADA", $"No se encontró matrícula con ID {id}");

            matricula.Anular();
            await _repository.ActualizarAsync(matricula);
        }

        private MatriculaDto MapearADto(Matricula matricula)
        {
            return new MatriculaDto
            {
                Id = matricula.Id,
                AlumnoId = matricula.AlumnoId,
                NombreAlumno = $"{matricula.Alumno.Nombre} {matricula.Alumno.ApellidoPaterno}",
                CodigoAlumno = matricula.Alumno.Codigo,
                SeccionId = matricula.SeccionId,
                NombreSeccion = matricula.Seccion.Nombre,
                FechaMatricula = matricula.FechaMatricula,
                Activo = matricula.Activo
            };
        }
    }
}
