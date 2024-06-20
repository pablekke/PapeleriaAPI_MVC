using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Presentation.Controllers
{
    [Authorize(Roles = "Encargado")]
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly IServicioArticulo _servicioArticulo;

        public ArticuloController(IServicioArticulo servicioArticulo)
        {
            _servicioArticulo = servicioArticulo;
        }

        // GET: [controller]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            try
            {
                var articulos = _servicioArticulo.GetAll().OrderBy(a => a.Nombre);
                return Ok(articulos);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("No autorizado para acceder a este recurso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: [controller]/filtrados?nombre=nombre&monto=0
        [HttpGet("filtrados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetFiltrados([FromQuery] string nombre = "", double monto = 0)
        {
            try
            {
                var articulos = _servicioArticulo.GetArticulosFiltrados(nombre, monto);
                return Ok(articulos);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("No autorizado para acceder a este recurso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: [controller]/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            try
            {
                var articulo = _servicioArticulo.GetPorId(id);
                if (articulo == null)
                {
                    return NotFound("Artículo no encontrado.");
                }
                return Ok(articulo);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("No autorizado para acceder a este recurso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: [controller]
        [HttpPost]
        [Authorize(Roles = "Encargado")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] Articulo articulo)
        {
            try
            {
                if (articulo == null)
                {
                    return BadRequest("Datos del artículo no válidos.");
                }

                _servicioArticulo.Crear(articulo.ToDTO());
                return Ok("El artículo se ha creado exitosamente.");
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("No autorizado para acceder a este recurso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: [controller]/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put(int id, [FromBody] Articulo articulo)
        {
            try
            {
                _servicioArticulo.Actualizar(id, articulo.ToDTO());
                return Ok("El artículo se ha actualizado exitosamente.");
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

        // DELETE: [controller]/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingArticulo = _servicioArticulo.GetPorId(id);
                if (existingArticulo == null)
                {
                    return NotFound("Artículo no encontrado.");
                }

                _servicioArticulo.Borrar(id);
                return Ok("El artículo se ha eliminado exitosamente.");
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("No autorizado para acceder a este recurso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}