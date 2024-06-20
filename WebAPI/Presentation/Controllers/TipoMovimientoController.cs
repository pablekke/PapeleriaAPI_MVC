using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoMovimientoController : ControllerBase
    {
        private readonly IServicioTipoMovimiento _servicio;

        public TipoMovimientoController(IServicioTipoMovimiento servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        [Authorize(Roles = "Encargado")]

        public ActionResult<IEnumerable<TipoMovimiento>> Get()
        {
            var tiposMovimiento = _servicio.GetAll();
            return Ok(tiposMovimiento);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Encargado")]
        public ActionResult<TipoMovimiento> Get(int id)
        {
            try
            {
                return Ok(_servicio.GetPorId(id));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción no esperada y devuelve un error 500
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Encargado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TipoMovimiento> Post(TipoMovimiento tipoMovimiento)
        {
            try
            {
                _servicio.Crear(tipoMovimiento.ToDTO());
                return Ok("El tipo de movimiento se ha creado exitosamente.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción no esperada y devuelve un error 500
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Encargado")]
        public IActionResult Put(int id, TipoMovimiento tipoMovimiento)
        {
            try
            {
                _servicio.Actualizar(id, tipoMovimiento.ToDTO());
                return Ok("El tipo de movimiento se ha actualizado exitosamente.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción no esperada y devuelve un error 500
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Encargado")]
        public IActionResult Delete(int id)
        {
            try
            {
                _servicio.Borrar(id);
                return Ok("El tipo de movimiento se ha eliminado exitosamente.");

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción no esperada y devuelve un error 500
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}