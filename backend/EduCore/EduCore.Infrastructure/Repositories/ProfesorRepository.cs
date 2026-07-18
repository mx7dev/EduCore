using EduCore.Business.Entities;
using EduCore.Business.Interfaces;
using EduCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EduCore.Infrastructure.Repositories
{
    public class ProfesorRepository : IProfesorRepository
    {
        private readonly AppDbContext _context;

        public ProfesorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Profesor?> ObtenerPorIdAsync(int id)
        {
            return await _context.Profesores
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Profesor>> ObtenerTodosAsync()
        {
            return await _context.Profesores.ToListAsync();
        }

        public async Task<bool> ExistePorDniAsync(string dni)
        {
            return await _context.Profesores
                .AnyAsync(p => p.Dni == dni);
        }

        public async Task GuardarAsync(Profesor profesor)
        {
            await _context.Profesores.AddAsync(profesor);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Profesor profesor)
        {
            _context.Profesores.Update(profesor);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var profesor = await ObtenerPorIdAsync(id);
            if (profesor != null)
            {
                _context.Profesores.Remove(profesor);
                await _context.SaveChangesAsync();
            }
        }
    }
}