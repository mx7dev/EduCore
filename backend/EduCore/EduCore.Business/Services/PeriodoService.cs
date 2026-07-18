using EduCore.Business.DTOs;
using EduCore.Business.Entities;
using EduCore.Business.Exceptions;
using EduCore.Business.Interfaces;

namespace EduCore.Business.Services
{
    public class PeriodoService
    {
        private readonly IPeriodoRepository _repository;

        public PeriodoService(IPeriodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PeriodoDto>> ObtenerTodosAsync()
        {
            var periodos = await _repository.ObtenerTodosAsync();
            return periodos.Select(p => MapearADto(p));
        }

        public async Task<PeriodoDto?> ObtenerActivoAsync()
        {
            var periodo = await _repository.ObtenerActivoAsync();
            if (periodo == null) return null;
            return MapearADto(periodo);
        }

        public async Task CrearPeriodoAsync(CrearPeriodoDto dto)
        {
            // Verificar que no exista el año
            var existe = await _repository.ExistePorAñoAsync(dto.Año);
            if (existe)
                throw new FunctionalException("AÑO_DUPLICADO", $"Ya existe un periodo para el año {dto.Año}");

            // Desactivar periodo activo anterior
            var periodoActivo = await _repository.ObtenerActivoAsync();
            if (periodoActivo != null)
            {
                periodoActivo.Desactivar();
                await _repository.ActualizarAsync(periodoActivo);
            }

            // Crear nuevo periodo
            var periodo = new Periodo(dto.Año, dto.Descripcion);
            await _repository.GuardarAsync(periodo);
        }

        private PeriodoDto MapearADto(Periodo periodo)
        {
            return new PeriodoDto
            {
                Id = periodo.Id,
                Año = periodo.Año,
                Descripcion = periodo.Descripcion,
                Activo = periodo.Activo
            };
        }
    }
}