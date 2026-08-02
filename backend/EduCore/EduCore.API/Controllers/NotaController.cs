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
        public async Task<JsonResult> ObtenerPorMatricula(int matriculaId)
        {
            ResponseDto response;
            try
            {
                var notas = await _service.ObtenerPorMatriculaAsync(matriculaId);
                response = new ResponseDto { Success = true, Data = notas };
            }
            catch (Exception ex)
            {
                response = new ResponseDto { Success = false, Message = ex.Message };
            }
            return new JsonResult(response);
        }

        [HttpGet("libreta/{matriculaId}")]
        public async Task<JsonResult> ObtenerLibreta(int matriculaId)
        {

            var fechaactual = DateTime.Now;
            ResponseDto response;
            try
            {
                var libreta = await _service.ObtenerLibretaAsync(matriculaId);
                response = new ResponseDto { Success = true, Data = libreta };
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

        [HttpPost]
        public async Task<JsonResult> Registrar(RegistrarNotaDto dto)
        {
            ResponseDto response;
            try
            {
                await _service.RegistrarNotaAsync(dto);
                response = new ResponseDto { Success = true, Message = "Nota registrada correctamente" };
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