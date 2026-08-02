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
        public async Task<JsonResult> ObtenerTodos()
        {
            ResponseDto response;
            try
            {
                var matriculas = await _service.ObtenerTodosAsync();
                response = new ResponseDto { Success = true, Data = matriculas };
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
                var matricula = await _service.ObtenerPorIdAsync(id);
                if (matricula == null)
                    response = new ResponseDto { Success = false, Message = $"No se encontró matrícula con ID {id}" };
                else
                    response = new ResponseDto { Success = true, Data = matricula };
            }
            catch (Exception ex)
            {
                response = new ResponseDto { Success = false, Message = ex.Message };
            }
            return new JsonResult(response);
        }

        [HttpGet("seccion/{seccionId}")]
        public async Task<JsonResult> ObtenerPorSeccion(int seccionId)
        {
            ResponseDto response;
            try
            {
                var matriculas = await _service.ObtenerPorSeccionAsync(seccionId);
                response = new ResponseDto { Success = true, Data = matriculas };
            }
            catch (Exception ex)
            {
                response = new ResponseDto { Success = false, Message = ex.Message };
            }
            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<JsonResult> Matricular(CrearMatriculaDto dto)
        {
            ResponseDto response;
            try
            {
                await _service.MatricularAlumnoAsync(dto);
                response = new ResponseDto { Success = true, Message = "Alumno matriculado correctamente" };
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

        [HttpPatch("{id}/anular")]
        public async Task<JsonResult> Anular(int id)
        {
            ResponseDto response;
            try
            {
                await _service.AnularMatriculaAsync(id);
                response = new ResponseDto { Success = true, Message = "Matrícula anulada correctamente" };
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