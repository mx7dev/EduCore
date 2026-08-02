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
    public class GradoController : ControllerBase
    {
        private readonly GradoService _service;

        public GradoController(GradoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerTodos()
        {
            ResponseDto response;
            try
            {
                var grados = await _service.ObtenerTodosAsync();
                response = new ResponseDto { Success = true, Data = grados };
            }
            catch (Exception ex)
            {
                response = new ResponseDto { Success = false, Message = ex.Message };
            }
            return new JsonResult(response);
        }

        [HttpGet("nivel/{nivel}")]
        public async Task<JsonResult> ObtenerPorNivel(int nivel)
        {
            ResponseDto response;
            try
            {
                var grados = await _service.ObtenerPorNivelAsync(nivel);
                response = new ResponseDto { Success = true, Data = grados };
            }
            catch (Exception ex)
            {
                response = new ResponseDto { Success = false, Message = ex.Message };
            }
            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<JsonResult> Crear(CrearGradoDto dto)
        {
            ResponseDto response;
            try
            {
                await _service.CrearGradoAsync(dto);
                response = new ResponseDto { Success = true, Message = "Grado creado correctamente" };
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