using EduCore.Business.DTOs;
using EduCore.Business.Exceptions;
using EduCore.Business.Services;
using EduCore.Business.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CursoController : ControllerBase
    {
        private readonly CursoService _service;
        private readonly CrearCursoDtoValidator _validator;

        public CursoController(CursoService service, CrearCursoDtoValidator validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var cursos = await _service.ObtenerTodosAsync();
                return Ok(cursos);
            }
            catch (TechnicalException ex)
            {
                return StatusCode(500, new { error = ex.Message, transactionId = ex.TransactionId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error inesperado", detalle = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var curso = await _service.ObtenerPorIdAsync(id);
                if (curso == null)
                    return NotFound(new { error = $"No se encontró curso con ID {id}" });
                return Ok(curso);
            }
            catch (TechnicalException ex)
            {
                return StatusCode(500, new { error = ex.Message, transactionId = ex.TransactionId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error inesperado", detalle = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearCursoDto dto)
        {
            var resultado = await _validator.ValidateAsync(dto);
            if (!resultado.IsValid)
                return BadRequest(resultado.Errors.Select(e => e.ErrorMessage));

            try
            {
                await _service.CrearCursoAsync(dto);
                return Ok("Curso creado correctamente");
            }
            catch (FunctionalException ex)
            {
                return BadRequest(new { error = ex.Message, code = ex.Code, transactionId = ex.TransactionId });
            }
            catch (TechnicalException ex)
            {
                return StatusCode(500, new { error = ex.Message, transactionId = ex.TransactionId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error inesperado", detalle = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _service.EliminarAsync(id);
                return Ok("Curso eliminado correctamente");
            }
            catch (FunctionalException ex)
            {
                return BadRequest(new { error = ex.Message, code = ex.Code, transactionId = ex.TransactionId });
            }
            catch (TechnicalException ex)
            {
                return StatusCode(500, new { error = ex.Message, transactionId = ex.TransactionId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error inesperado", detalle = ex.Message });
            }
        }
    }
}
