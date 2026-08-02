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
    public class SeccionController : ControllerBase
    {
        private readonly SeccionService _service;

        public SeccionController(SeccionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerTodos()
        {
            ResponseDto response;
            try
            {
                var secciones = await _service.ObtenerTodosAsync();
                response = new ResponseDto { Success = true, Data = secciones };
            }
            catch (Exception ex)
            {
                response = new ResponseDto { Success = false, Message = ex.Message };
            }
            return new JsonResult(response);
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> ObtenerPorId(int id)
        {
            ResponseDto response;
            try
            {
                var seccion = await _service.ObtenerPorIdAsync(id);
                if (seccion == null)
                    response = new ResponseDto { Success = false, Message = $"No se encontró sección con ID {id}" };
                else
                    response = new ResponseDto { Success = true, Data = seccion };
            }
            catch (Exception ex)
            {
                response = new ResponseDto { Success = false, Message = ex.Message };
            }
            return new JsonResult(response);
        }

        [HttpGet("periodo/{periodoId}")]
        public async Task<JsonResult> ObtenerPorPeriodo(int periodoId)
        {
            ResponseDto response;
            try
            {
                var secciones = await _service.ObtenerPorPeriodoAsync(periodoId);
                response = new ResponseDto { Success = true, Data = secciones };
            }
            catch (Exception ex)
            {
                response = new ResponseDto { Success = false, Message = ex.Message };
            }
            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<JsonResult> Crear(CrearSeccionDto dto)
        {
            ResponseDto response;
            try
            {
                await _service.CrearSeccionAsync(dto);
                response = new ResponseDto { Success = true, Message = "Sección creada correctamente" };
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