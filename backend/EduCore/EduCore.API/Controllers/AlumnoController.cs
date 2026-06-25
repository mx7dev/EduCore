using EduCore.Business.DTOs;
using EduCore.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlumnoController : ControllerBase
    {
        private readonly AlumnoService _service;

        public AlumnoController(AlumnoService service)
        {
            _service = service;
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
            await _service.CrearAlumnoAsync(dto);
            return Ok("Alumno creado correctamente");
        }
    }
}
