using EduCore.Business.DTOs;
using EduCore.Business.Entities;
using EduCore.Business.Enums;
using EduCore.Business.Exceptions;
using EduCore.Business.Interfaces;

namespace EduCore.Business.Services
{
    public class NotaService
    {
        private readonly INotaRepository _repository;
        private readonly IMatriculaRepository _matriculaRepository;

        public NotaService(INotaRepository repository, IMatriculaRepository matriculaRepository)
        {
            _repository = repository;
            _matriculaRepository = matriculaRepository;
        }

        public async Task<IEnumerable<NotaDto>> ObtenerPorMatriculaAsync(int matriculaId)
        {
            var notas = await _repository.ObtenerPorMatriculaAsync(matriculaId);
            return notas.Select(n => MapearADto(n));
        }

        public async Task RegistrarNotaAsync(RegistrarNotaDto dto)
        {
            // Verificar que la matrícula existe
            var matricula = await _matriculaRepository.ObtenerPorIdAsync(dto.MatriculaId);
            if (matricula == null)
                throw new FunctionalException("MATRICULA_NO_ENCONTRADA", "La matrícula no existe");

            // Verificar que no exista ya esa nota
            var bimestre = (Bimestre)dto.Bimestre;
            var existe = await _repository.ExisteAsync(dto.MatriculaId, dto.CursoId, bimestre);
            if (existe)
                throw new FunctionalException("NOTA_DUPLICADA", $"Ya existe una nota para ese curso en el {bimestre}");

            var nota = new Nota(dto.MatriculaId, dto.CursoId, bimestre, dto.Calificacion, dto.Comentario);
            await _repository.GuardarAsync(nota);
        }

        public async Task<LibretaDto> ObtenerLibretaAsync(int matriculaId)
        {
            var matricula = await _matriculaRepository.ObtenerPorIdAsync(matriculaId);
            if (matricula == null)
                throw new FunctionalException("MATRICULA_NO_ENCONTRADA", "La matrícula no existe");

            var notas = await _repository.ObtenerPorMatriculaAsync(matriculaId);

            var cursos = notas
                .GroupBy(n => new { n.CursoId, n.Curso.Nombre })
                .Select(g => new NotaCursoDto
                {
                    NombreCurso = g.Key.Nombre,
                    B1 = g.FirstOrDefault(n => n.Bimestre == Bimestre.B1)?.Calificacion,
                    B2 = g.FirstOrDefault(n => n.Bimestre == Bimestre.B2)?.Calificacion,
                    B3 = g.FirstOrDefault(n => n.Bimestre == Bimestre.B3)?.Calificacion,
                    B4 = g.FirstOrDefault(n => n.Bimestre == Bimestre.B4)?.Calificacion,
                    NotaFinal = g.Average(n => n.Calificacion)
                })
                .ToList();

            return new LibretaDto
            {
                NombreAlumno = $"{matricula.Alumno.Nombre} {matricula.Alumno.ApellidoPaterno}",
                CodigoAlumno = matricula.Alumno.Codigo,
                Cursos = cursos
            };
        }

        private NotaDto MapearADto(Nota nota)
        {
            return new NotaDto
            {
                Id = nota.Id,
                MatriculaId = nota.MatriculaId,
                NombreAlumno = $"{nota.Matricula.Alumno.Nombre} {nota.Matricula.Alumno.ApellidoPaterno}",
                CodigoAlumno = nota.Matricula.Alumno.Codigo,
                CursoId = nota.CursoId,
                NombreCurso = nota.Curso.Nombre,
                Bimestre = nota.Bimestre.ToString(),
                Calificacion = nota.Calificacion,
                Comentario = nota.Comentario
            };
        }
    }
}
