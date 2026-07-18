using EduCore.Business.DTOs;
using EduCore.Business.Entities;
using EduCore.Business.Enums;
using EduCore.Business.Exceptions;
using EduCore.Business.Interfaces;

namespace EduCore.Business.Services
{
    public class SeccionService
    {
        private readonly ISeccionRepository _repository;

        public SeccionService(ISeccionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SeccionDto>> ObtenerTodosAsync()
        {
            return await _repository.ObtenerTodosAsync();
        }

        public async Task<SeccionDto?> ObtenerPorIdAsync(int id)
        {
            return await _repository.ObtenerPorIdAsync(id);
        }

        public async Task<IEnumerable<SeccionDto>> ObtenerPorPeriodoAsync(int periodoId)
        {
            return await _repository.ObtenerPorPeriodoAsync(periodoId);
        }

        public async Task CrearSeccionAsync(CrearSeccionDto dto)
        {
            var existe = await _repository.ExisteAsync(dto.Nombre, dto.GradoId, dto.PeriodoId);
            if (existe)
                throw new FunctionalException("SECCION_DUPLICADA", $"Ya existe la sección {dto.Nombre} para ese grado y periodo");

            var turno = (Turno)dto.Turno;
            var seccion = new Seccion(dto.Nombre, turno, dto.GradoId, dto.PeriodoId);

            await _repository.GuardarAsync(seccion);
        }
    }
}
