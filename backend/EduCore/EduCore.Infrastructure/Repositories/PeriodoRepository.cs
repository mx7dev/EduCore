using EduCore.Business.Entities;
using EduCore.Business.Interfaces;
using EduCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EduCore.Infrastructure.Repositories
{
    public class PeriodoRepository : IPeriodoRepository
    {
        private readonly AppDbContext _context;

        public PeriodoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Periodo?> ObtenerPorIdAsync(int id)
        {
            return await _context.Periodos
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Periodo>> ObtenerTodosAsync()
        {
            return await _context.Periodos.ToListAsync();
        }

        public async Task<Periodo?> ObtenerActivoAsync()
        {
            return await _context.Periodos
                .FirstOrDefaultAsync(p => p.Activo);
        }

        public async Task<bool> ExistePorAñoAsync(int año)
        {
            return await _context.Periodos
                .AnyAsync(p => p.Año == año);
        }

        public async Task GuardarAsync(Periodo periodo)
        {
            await _context.Periodos.AddAsync(periodo);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Periodo periodo)
        {
            _context.Periodos.Update(periodo);
            await _context.SaveChangesAsync();
        }
    }
}