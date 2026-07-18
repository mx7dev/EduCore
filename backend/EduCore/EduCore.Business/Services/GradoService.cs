using EduCore.Business.DTOs;
using EduCore.Business.Entities;
using EduCore.Business.Enums;
using EduCore.Business.Exceptions;
using EduCore.Business.Interfaces;

namespace EduCore.Business.Services
{
    public class GradoService
    {
        private readonly IGradoRepository _repository;

        public GradoService(IGradoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GradoDto>> ObtenerTodosAsync()
        {
            var grados = await _repository.ObtenerTodosAsync();
            return grados.Select(g => MapearADto(g));
        }

        public async Task<IEnumerable<GradoDto>> ObtenerPorNivelAsync(int nivel)
        {
            var grados = await _repository.ObtenerPorNivelAsync(nivel);
            return grados.Select(g => MapearADto(g));
        }

        public async Task CrearGradoAsync(CrearGradoDto dto)
        {
            var existe = await _repository.ExisteAsync(dto.Numero, dto.Nivel);
            if (existe)
                throw new FunctionalException("GRADO_DUPLICADO", $"Ya existe el grado {dto.Numero} para ese nivel");

            var nivel = (NivelEducativo)dto.Nivel;
            var grado = new Grado(dto.Numero, nivel);

            await _repository.GuardarAsync(grado);
        }

        private GradoDto MapearADto(Grado grado)
        {
            return new GradoDto
            {
                Id = grado.Id,
                Numero = grado.Numero,
                Nivel = grado.Nivel.ToString()
            };
        }
    }
}
