using EduCore.Business.DTOs;
using EduCore.Business.Entities;
using EduCore.Business.Interfaces;
using EduCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EduCore.Infrastructure.Repositories
{
    public class SeccionRepository : ISeccionRepository
    {
        private readonly AppDbContext _context;

        public SeccionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SeccionDto?> ObtenerPorIdAsync(int id)
        {
            return await _context.Secciones
                .Where(s => s.Id == id)
                .Select(s => new SeccionDto
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Turno = s.Turno.ToString(),
                    NumeroGrado = s.Grado.Numero,
                    NivelGrado = s.Grado.Nivel.ToString(),
                    AnioPeriodo = s.Periodo.Año,
                    Activo = s.Activo
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SeccionDto>> ObtenerTodosAsync()
        {
            return await _context.Secciones
                .Select(s => new SeccionDto
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Turno = s.Turno.ToString(),
                    NumeroGrado = s.Grado.Numero,
                    NivelGrado = s.Grado.Nivel.ToString(),
                    AnioPeriodo = s.Periodo.Año,
                    Activo = s.Activo
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<SeccionDto>> ObtenerPorPeriodoAsync(int periodoId)
        {
            return await _context.Secciones
                .Where(s => s.PeriodoId == periodoId)
                .Select(s => new SeccionDto
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Turno = s.Turno.ToString(),
                    NumeroGrado = s.Grado.Numero,
                    NivelGrado = s.Grado.Nivel.ToString(),
                    AnioPeriodo = s.Periodo.Año,
                    Activo = s.Activo
                })
                .ToListAsync();
        }

        public async Task<bool> ExisteAsync(string nombre, int gradoId, int periodoId)
        {
            return await _context.Secciones
                .AnyAsync(s => s.Nombre == nombre && s.GradoId == gradoId && s.PeriodoId == periodoId);
        }

        public async Task GuardarAsync(Seccion seccion)
        {
            await _context.Secciones.AddAsync(seccion);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Seccion seccion)
        {
            _context.Secciones.Update(seccion);
            await _context.SaveChangesAsync();
        }
    }
}
