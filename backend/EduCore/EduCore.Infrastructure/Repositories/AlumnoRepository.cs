using EduCore.Business.Entities;
using EduCore.Business.Interfaces;
using EduCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EduCore.Infrastructure.Repositories
{
    public class AlumnoRepository : IAlumnoRepository
    {
        private readonly AppDbContext _context;

        public AlumnoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Alumno> ObtenerPorIdAsync(int id)
        {
            return await _context.Alumnos.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Alumno>> ObtenerTodosAsync()
        {
            return await _context.Alumnos
                .ToListAsync();
        }

        public async Task<int> ObtenerSiguienteCorrelativoAsync()
        {
            var maximo = await _context.Alumnos
                .AnyAsync()
                    ? await _context.Alumnos.MaxAsync(a => a.Id)
                    : 0;
            return maximo + 1;
        }

        public async Task GuardarAsync(Alumno alumno)
        {
            await _context.Alumnos.AddAsync(alumno);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Alumno alumno)
        {
            _context.Alumnos.Update(alumno);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var alumno = await ObtenerPorIdAsync(id);
            if (alumno != null)
            {
                _context.Alumnos.Remove(alumno);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> ExistePorDniAsync(string dni)
        {
            return await _context.Alumnos
                .AnyAsync(a => a.Dni == dni);
        }
    }
}
