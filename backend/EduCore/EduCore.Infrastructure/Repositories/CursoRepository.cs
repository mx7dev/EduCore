using EduCore.Business.Entities;
using EduCore.Business.Interfaces;
using EduCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EduCore.Infrastructure.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly AppDbContext _context;

        public CursoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Curso?> ObtenerPorIdAsync(int id)
        {
            return await _context.Cursos.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Curso>> ObtenerTodosAsync()
        {
            return await _context.Cursos.ToListAsync();
        }

        public async Task<bool> ExistePorNombreAsync(string nombre)
        {
            return await _context.Cursos
                .AnyAsync(c => c.Nombre == nombre);
        }

        public async Task<int> ObtenerSiguienteCorrelativoAsync()
        {
            var maximo = await _context.Cursos.AnyAsync()
                ? await _context.Cursos.MaxAsync(c => c.Id)
                : 0;
            return maximo + 1;
        }

        public async Task GuardarAsync(Curso curso)
        {
            await _context.Cursos.AddAsync(curso);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Curso curso)
        {
            _context.Cursos.Update(curso);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var curso = await ObtenerPorIdAsync(id);
            if (curso != null)
            {
                _context.Cursos.Remove(curso);
                await _context.SaveChangesAsync();
            }
        }
    }
}
