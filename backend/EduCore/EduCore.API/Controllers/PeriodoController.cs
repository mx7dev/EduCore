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
    public class PeriodoController : ControllerBase
    {
        private readonly PeriodoService _service;

        public PeriodoController(PeriodoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var periodos = await _service.ObtenerTodosAsync();
                return Ok(periodos);
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

        [HttpGet("activo")]
        public async Task<IActionResult> ObtenerActivo()
        {
            try
            {
                var periodo = await _service.ObtenerActivoAsync();
                if (periodo == null)
                    return NotFound(new { error = "No hay periodo activo" });
                return Ok(periodo);
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
        public async Task<IActionResult> Crear([FromBody] CrearPeriodoDto dto)
        {
            try
            {
                await _service.CrearPeriodoAsync(dto);
                return Ok("Periodo creado correctamente");
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