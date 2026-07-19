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
    public class NotaController : ControllerBase
    {
        private readonly NotaService _service;

        public NotaController(NotaService service)
        {
            _service = service;
        }

        [HttpGet("matricula/{matriculaId}")]
        public async Task<IActionResult> ObtenerPorMatricula(int matriculaId)
        {
            try
            {
                var notas = await _service.ObtenerPorMatriculaAsync(matriculaId);
                return Ok(notas);
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

        [HttpGet("libreta/{matriculaId}")]
        public async Task<IActionResult> ObtenerLibreta(int matriculaId)
        {
            try
            {
                var libreta = await _service.ObtenerLibretaAsync(matriculaId);
                return Ok(libreta);
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

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] RegistrarNotaDto dto)
        {
            try
            {
                await _service.RegistrarNotaAsync(dto);
                return Ok("Nota registrada correctamente");
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
