using EduCore.Business.Entities;
using EduCore.Business.Interfaces;
using EduCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EduCore.Infrastructure.Repositories
{
    public class MatriculaRepository : IMatriculaRepository
    {
        private readonly AppDbContext _context;

        public MatriculaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Matricula>> ObtenerTodosAsync()
        {
            return await _context.Matriculas
                .Include(m => m.Alumno)
                .Include(m => m.Seccion)
                .ToListAsync();
        }

        public async Task<Matricula?> ObtenerPorIdAsync(int id)
        {
            return await _context.Matriculas
                .Include(m => m.Alumno)
                .Include(m => m.Seccion)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Matricula>> ObtenerPorSeccionAsync(int seccionId)
        {
            return await _context.Matriculas
                .Include(m => m.Alumno)
                .Where(m => m.SeccionId == seccionId)
                .ToListAsync();
        }

        public async Task<bool> ExisteMatriculaEnPeriodoAsync(int alumnoId, int periodoId)
        {
            return await _context.Matriculas
                .AnyAsync(m => m.AlumnoId == alumnoId &&
                               m.Seccion.PeriodoId == periodoId &&
                               m.Activo);
        }

        public async Task GuardarAsync(Matricula matricula)
        {
            await _context.Matriculas.AddAsync(matricula);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Matricula matricula)
        {
            _context.Matriculas.Update(matricula);
            await _context.SaveChangesAsync();
        }
    }
}
