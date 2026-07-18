using EduCore.Business.Entities;
using EduCore.Business.Interfaces;
using EduCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EduCore.Infrastructure.Repositories
{
    public class GradoRepository : IGradoRepository
    {
        private readonly AppDbContext _context;

        public GradoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Grado?> ObtenerPorIdAsync(int id)
        {
            return await _context.Grados
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Grado>> ObtenerTodosAsync()
        {
            return await _context.Grados.ToListAsync();
        }

        public async Task<IEnumerable<Grado>> ObtenerPorNivelAsync(int nivel)
        {
            return await _context.Grados
                .Where(g => (int)g.Nivel == nivel)
                .ToListAsync();
        }

        public async Task<bool> ExisteAsync(int numero, int nivel)
        {
            return await _context.Grados
                .AnyAsync(g => g.Numero == numero && (int)g.Nivel == nivel);
        }

        public async Task GuardarAsync(Grado grado)
        {
            await _context.Grados.AddAsync(grado);
            await _context.SaveChangesAsync();
        }
    }
}