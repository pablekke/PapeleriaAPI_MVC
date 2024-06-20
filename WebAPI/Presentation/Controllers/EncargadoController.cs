using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EncargadoController : ControllerBase
    {
        private IServicioEncargado _servicio;
        public EncargadoController(IServicioEncargado servicio)
        {
            _servicio = servicio;
        }
        // GET: api/<EncargadoController>
        [HttpGet]
        [Authorize(Roles = "Encargado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            try
            {
                return Ok(_servicio.GetAll());
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("No autorizado para acceder a este recurso.");
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción no esperada y devuelve un error 500
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET api/<EncargadoController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Encargado")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_servicio.GetPorId(id));
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción no esperada y devuelve un error 500
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST api/<EncargadoController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [Authorize(Roles = "Encargado")]

        public IActionResult Post([FromBody] EncargadoIN encargado)
        {
            try
            {
                _servicio.Crear(encargado.ToDTO());
                return Ok("El usuario se ha creado exitosamente");
            }
            catch (ArgumentNullException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message); // Retorna 400 Bad Request si hay argumentos nulos
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message); // Retorna 400 Bad Request si hay argumentos inválidos
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message); // Retorna 404 Not Found si ocurre una operación no válida
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción no esperada y devuelve un error 500
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT api/<EncargadoController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Roles = "Encargado")]

        public IActionResult Put(int id, [FromBody] EncargadoUpdate encargado)
        {
            try
            {
                _servicio.Actualizar(id, encargado.ToDTO());
                return Ok("El usuario se ha actualizado correctamente.");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message); // Retorna 400 Bad Request si hay argumentos nulos
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // Retorna 400 Bad Request si hay argumentos inválidos
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message); // Retorna 404 Not Found si ocurre una operación no válida
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción no esperada y devuelve un error 500
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE api/<EncargadoController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Roles = "Encargado")]
        public IActionResult Delete(int id)
        {
            try
            {
                _servicio.Borrar(id);
                return Ok("El usuario se ha eliminado correctamente.");
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message); // Retorna 404 Not Found si ocurre una operación no válida
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción no esperada y devuelve un error 500
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}