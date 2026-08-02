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
        public async Task<JsonResult> ObtenerTodos()
        {
            ResponseDto response;
            try
            {
                var profesores = await _service.ObtenerTodosAsync();
                response = new ResponseDto { Success = true, Data = profesores };
            }
            catch (TechnicalException ex)
            {
                response = new ResponseDto { Success = false, Message = ex.Message, TransactionId = ex.TransactionId };
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
                var profesor = await _service.ObtenerPorIdAsync(id);
                if (profesor == null)
                    response = new ResponseDto { Success = false, Message = $"No se encontró profesor con ID {id}" };
                else
                    response = new ResponseDto { Success = true, Data = profesor };
            }
            catch (TechnicalException ex)
            {
                response = new ResponseDto { Success = false, Message = ex.Message, TransactionId = ex.TransactionId };
            }
            catch (Exception ex)
            {
                response = new ResponseDto { Success = false, Message = ex.Message };
            }
            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<JsonResult> Crear(CrearProfesorDto dto)
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
                await _service.CrearProfesorAsync(dto);
                response = new ResponseDto { Success = true, Message = "Profesor creado correctamente" };
            }
            catch (FunctionalException ex)
            {
                response = new ResponseDto { Success = false, Message = ex.Message, Code = ex.Code, TransactionId = ex.TransactionId };
            }
            catch (TechnicalException ex)
            {
                response = new ResponseDto { Success = false, Message = ex.Message, TransactionId = ex.TransactionId };
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
                response = new ResponseDto { Success = true, Message = "Profesor eliminado correctamente" };
            }
            catch (FunctionalException ex)
            {
                response = new ResponseDto { Success = false, Message = ex.Message, Code = ex.Code, TransactionId = ex.TransactionId };
            }
            catch (TechnicalException ex)
            {
                response = new ResponseDto { Success = false, Message = ex.Message, TransactionId = ex.TransactionId };
            }
            catch (Exception ex)
            {
                response = new ResponseDto { Success = false, Message = ex.Message };
            }
            return new JsonResult(response);
        }
    }
}