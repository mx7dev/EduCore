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
    public class ProfesorController : ControllerBase
    {
        private readonly ProfesorService _service;
        private readonly CrearProfesorDtoValidator _validator;

        public ProfesorController(ProfesorService service, CrearProfesorDtoValidator validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var profesores = await _service.ObtenerTodosAsync();
                return Ok(profesores);
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
                var profesor = await _service.ObtenerPorIdAsync(id);
                if (profesor == null)
                    return NotFound(new { error = $"No se encontró profesor con ID {id}" });
                return Ok(profesor);
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
        public async Task<IActionResult> Crear([FromBody] CrearProfesorDto dto)
        {
            var resultado = await _validator.ValidateAsync(dto);
            if (!resultado.IsValid)
                return BadRequest(resultado.Errors.Select(e => e.ErrorMessage));

            try
            {
                await _service.CrearProfesorAsync(dto);
                return Ok("Profesor creado correctamente");
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
                return Ok("Profesor eliminado correctamente");
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
