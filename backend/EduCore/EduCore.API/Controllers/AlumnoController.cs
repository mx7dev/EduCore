using EduCore.Business.DTOs;
using EduCore.Business.Services;
using EduCore.Business.Validators;
using Microsoft.AspNetCore.Mvc;

namespace EduCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlumnoController : ControllerBase
    {
        private readonly AlumnoService _service;
        private readonly CrearAlumnoDtoValidator _validator;

        public AlumnoController(AlumnoService service, CrearAlumnoDtoValidator validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var alumnos = await _service.ObtenerTodosAsync();
            return Ok(alumnos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var alumno = await _service.ObtenerPorIdAsync(id);
            if (alumno == null)
                return NotFound();
            return Ok(alumno);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearAlumnoDto dto)
        {
            var resultado = await _validator.ValidateAsync(dto);

            if (!resultado.IsValid)
                return BadRequest(resultado.Errors.Select(e => e.ErrorMessage));

            await _service.CrearAlumnoAsync(dto);
            return Ok("Alumno creado correctamente");
        }
    }
}
