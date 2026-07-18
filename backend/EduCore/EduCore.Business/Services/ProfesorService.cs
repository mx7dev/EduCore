using EduCore.Business.DTOs;
using EduCore.Business.Entities;
using EduCore.Business.Exceptions;
using EduCore.Business.Interfaces;

namespace EduCore.Business.Services
{
    public class ProfesorService
    {
        private readonly IProfesorRepository _repository;

        public ProfesorService(IProfesorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProfesorDto>> ObtenerTodosAsync()
        {
            var profesores = await _repository.ObtenerTodosAsync();
            return profesores.Select(p => MapearADto(p));
        }

        public async Task<ProfesorDto?> ObtenerPorIdAsync(int id)
        {
            var profesor = await _repository.ObtenerPorIdAsync(id);
            if (profesor == null) return null;
            return MapearADto(profesor);
        }

        public async Task CrearProfesorAsync(CrearProfesorDto dto)
        {
            var existe = await _repository.ExistePorDniAsync(dto.Dni);
            if (existe)
                throw new FunctionalException("DNI_DUPLICADO", $"Ya existe un profesor con el DNI {dto.Dni}");

            var profesor = new Profesor(
                dto.Dni,
                dto.Nombre,
                dto.ApellidoPaterno,
                dto.ApellidoMaterno,
                dto.Especialidad,
                dto.CorreoElectronico,
                dto.Direccion,
                dto.FechaNacimiento,
                dto.NumeroCelular);

            await _repository.GuardarAsync(profesor);
        }

        public async Task EliminarAsync(int id)
        {
            var profesor = await _repository.ObtenerPorIdAsync(id);
            if (profesor == null)
                throw new FunctionalException("PROFESOR_NO_ENCONTRADO", $"No se encontró profesor con ID {id}");

            await _repository.EliminarAsync(id);
        }

        private ProfesorDto MapearADto(Profesor profesor)
        {
            return new ProfesorDto
            {
                Id = profesor.Id,
                Dni = profesor.Dni,
                Nombre = profesor.Nombre,
                ApellidoPaterno = profesor.ApellidoPaterno,
                ApellidoMaterno = profesor.ApellidoMaterno,
                Especialidad = profesor.Especialidad,
                CorreoElectronico = profesor.CorreoElectronico,
                Direccion = profesor.Direccion,
                FechaNacimiento = profesor.FechaNacimiento,
                NumeroCelular = profesor.NumeroCelular,
                Activo = profesor.Activo
            };
        }
    }
}
