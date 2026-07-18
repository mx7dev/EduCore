using EduCore.Business.DTOs;
using EduCore.Business.Exceptions;
using EduCore.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MatriculaController : ControllerBase
    {
        private readonly MatriculaService _service;

        public MatriculaController(MatriculaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var matriculas = await _service.ObtenerTodosAsync();
                return Ok(matriculas);
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
                var matricula = await _service.ObtenerPorIdAsync(id);
                if (matricula == null)
                    return NotFound(new { error = $"No se encontró matrícula con ID {id}" });
                return Ok(matricula);
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

        [HttpGet("seccion/{seccionId}")]
        public async Task<IActionResult> ObtenerPorSeccion(int seccionId)
        {
            try
            {
                var matriculas = await _service.ObtenerPorSeccionAsync(seccionId);
                return Ok(matriculas);
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
        public async Task<IActionResult> Matricular([FromBody] CrearMatriculaDto dto)
        {
            try
            {
                await _service.MatricularAlumnoAsync(dto);
                return Ok("Alumno matriculado correctamente");
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

        [HttpPatch("{id}/anular")]
        public async Task<IActionResult> Anular(int id)
        {
            try
            {
                await _service.AnularMatriculaAsync(id);
                return Ok("Matrícula anulada correctamente");
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
