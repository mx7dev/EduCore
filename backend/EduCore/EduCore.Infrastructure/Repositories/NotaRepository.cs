using EduCore.Business.Entities;
using EduCore.Business.Enums;
using EduCore.Business.Interfaces;
using EduCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EduCore.Infrastructure.Repositories
{
    public class NotaRepository : INotaRepository
    {
        private readonly AppDbContext _context;

        public NotaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Nota?> ObtenerPorIdAsync(int id)
        {
            return await _context.Notas
                .Include(n => n.Matricula)
                    .ThenInclude(m => m.Alumno)
                .Include(n => n.Curso)
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IEnumerable<Nota>> ObtenerPorMatriculaAsync(int matriculaId)
        {
            return await _context.Notas
                .Include(n => n.Curso)
                .Where(n => n.MatriculaId == matriculaId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Nota>> ObtenerPorMatriculaYCursoAsync(int matriculaId, int cursoId)
        {
            return await _context.Notas
                .Where(n => n.MatriculaId == matriculaId && n.CursoId == cursoId)
                .ToListAsync();
        }

        public async Task<bool> ExisteAsync(int matriculaId, int cursoId, Bimestre bimestre)
        {
            return await _context.Notas
                .AnyAsync(n => n.MatriculaId == matriculaId &&
                               n.CursoId == cursoId &&
                               n.Bimestre == bimestre);
        }

        public async Task GuardarAsync(Nota nota)
        {
            await _context.Notas.AddAsync(nota);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Nota nota)
        {
            _context.Notas.Update(nota);
            await _context.SaveChangesAsync();
        }
    }
}
