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
        public async Task<JsonResult> ObtenerTodos()
        {
            ResponseDto response;
            try
            {
                var periodos = await _service.ObtenerTodosAsync();
                response = new ResponseDto { Success = true, Data = periodos };
            }
            catch (Exception ex)
            {
                response = new ResponseDto { Success = false, Message = ex.Message };
            }
            return new JsonResult(response);
        }

        [HttpGet("activo")]
        public async Task<JsonResult> ObtenerActivo()
        {
            ResponseDto response;
            try
            {
                var periodo = await _service.ObtenerActivoAsync();
                if (periodo == null)
                    response = new ResponseDto { Success = false, Message = "No hay periodo activo" };
                else
                    response = new ResponseDto { Success = true, Data = periodo };
            }
            catch (Exception ex)
            {
                response = new ResponseDto { Success = false, Message = ex.Message };
            }
            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<JsonResult> Crear(CrearPeriodoDto dto)
        {
            ResponseDto response;
            try
            {
                await _service.CrearPeriodoAsync(dto);
                response = new ResponseDto { Success = true, Message = "Periodo creado correctamente" };
            }
            catch (FunctionalException ex)
            {
                response = new ResponseDto { Success = false, Message = ex.Message, Code = ex.Code, TransactionId = ex.TransactionId };
            }
            catch (Exception ex)
            {
                response = new ResponseDto { Success = false, Message = ex.Message };
            }
            return new JsonResult(response);
        }
    }
}