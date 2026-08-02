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
        public async Task<JsonResult> ObtenerTodos()
        {
            ResponseDto response;
            try
            {
                var cursos = await _service.ObtenerTodosAsync();
                response = new ResponseDto { Success = true, Data = cursos };
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
                var curso = await _service.ObtenerPorIdAsync(id);
                if (curso == null)
                    response = new ResponseDto { Success = false, Message = $"No se encontró curso con ID {id}" };
                else
                    response = new ResponseDto { Success = true, Data = curso };
            }
            catch (Exception ex)
            {
                response = new ResponseDto { Success = false, Message = ex.Message };
            }
            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<JsonResult> Crear(CrearCursoDto dto)
        {
            ResponseDto response;

            var resultado = await _validator.ValidateAsync(dto);
            if (!resultado.IsValid)
            {
                response = new ResponseDto { Success = false, Message = string.Join(", ", resultado.Errors.Select(e => e.ErrorMessage)) };
                return new JsonResult(response);
            }

            try
            {
                await _service.CrearCursoAsync(dto);
                response = new ResponseDto { Success = true, Message = "Curso creado correctamente" };
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

        [HttpDelete("{id}")]
        public async Task<JsonResult> Eliminar(int id)
        {
            ResponseDto response;
            try
            {
                await _service.EliminarAsync(id);
                response = new ResponseDto { Success = true, Message = "Curso eliminado correctamente" };
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