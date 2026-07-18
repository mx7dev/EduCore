using EduCore.Business.DTOs;
using EduCore.Business.Entities;
using EduCore.Business.Exceptions;
using EduCore.Business.Interfaces;

namespace EduCore.Business.Services
{
    public class CursoService
    {
        private readonly ICursoRepository _repository;

        public CursoService(ICursoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CursoDto>> ObtenerTodosAsync()
        {
            var cursos = await _repository.ObtenerTodosAsync();
            return cursos.Select(c => MapearADto(c));
        }

        public async Task<CursoDto?> ObtenerPorIdAsync(int id)
        {
            var curso = await _repository.ObtenerPorIdAsync(id);
            if (curso == null) return null;
            return MapearADto(curso);
        }

        public async Task CrearCursoAsync(CrearCursoDto dto)
        {
            var existe = await _repository.ExistePorNombreAsync(dto.Nombre);
            if (existe)
                throw new FunctionalException("NOMBRE_DUPLICADO", $"Ya existe un curso con el nombre {dto.Nombre}");

            var correlativo = await _repository.ObtenerSiguienteCorrelativoAsync();
            var codigo = $"CUR{correlativo:D4}";

            var curso = new Curso(codigo, dto.Nombre, dto.Descripcion);

            await _repository.GuardarAsync(curso);
        }

        public async Task EliminarAsync(int id)
        {
            var curso = await _repository.ObtenerPorIdAsync(id);
            if (curso == null)
                throw new FunctionalException("CURSO_NO_ENCONTRADO", $"No se encontró curso con ID {id}");

            await _repository.EliminarAsync(id);
        }

        private CursoDto MapearADto(Curso curso)
        {
            return new CursoDto
            {
                Id = curso.Id,
                Codigo = curso.Codigo,
                Nombre = curso.Nombre,
                Descripcion = curso.Descripcion,
                Activo = curso.Activo
            };
        }
    }
}
