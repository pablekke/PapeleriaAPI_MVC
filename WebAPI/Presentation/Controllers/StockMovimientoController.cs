using Domain.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Services;

namespace Presentation.Controllers
{
    [Authorize(Roles = "Encargado")]
    [Route("api/[controller]")]
    [ApiController]
    public class StockArticuloController : ControllerBase
    {
        private readonly IServicioStockArticulo _servicioStock;

        public StockArticuloController(
            IServicioStockArticulo servicioStock
            )
        {
            _servicioStock = servicioStock; ;
        }
        // GET: api/<EncargadoController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int page = 1, int pageSize = 50)
        {
            try
            {
                if (pageSize > 50)
                {
                    pageSize = 50;
                }
                var movimientos = _servicioStock.GetStocksDetallados();
                var totalMovimientos = movimientos.Count();

                // Aplico la paginación
                var movimientosPaginados = movimientos
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return Ok(new { Total = totalMovimientos, Movimientos = movimientosPaginados });
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

        // GET api/<EncargadoController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_servicioStock.GetStockDetallado(id));
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
        [HttpPost]
        public IActionResult Post([FromBody] Movimiento movimiento)
        {

            try
            {
                _servicioStock.Crear(movimiento.ToDTO());
                return Ok("El movimiento se ha creado exitosamente");
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
                return StatusCode(500, ex.Message);
            }
        }

        // GET /api/StockArticulo/articulo/{articuloId}/tipo/{tipoMovimientoId}&page={page}&pageSize={pageSize}
        [HttpGet("articulo/{articuloId}/tipo/{tipoMovimientoId}")]
        public IActionResult GetMovimientosPorArticuloYTipo(int articuloId, int tipoMovimientoId, int page = 1, int pageSize = 10)
        {
            try
            {
                if (pageSize > 50)
                {
                    pageSize = 50;
                }

                var movimientos = _servicioStock.GetMovimientosPorArticuloYTipo(articuloId, tipoMovimientoId);
                var totalMovimientos = movimientos.Count();

                var movimientosPaginados = movimientos
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return Ok(new
                {
                    Total = totalMovimientos,
                    Movimientos = movimientosPaginados
                });
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
        // GET /api/StockArticulo/fechas?fechaInicio=yyyy-MM-dd&fechaFin=yyyy-MM-dd&page={page}&pageSize={pageSize}
        [HttpGet("fechas")]
        public IActionResult GetMovimientosPorRangoFechas(DateTime fechaInicio, DateTime fechaFin, int page = 1, int pageSize = 10)
        {
            try
            {
                if (pageSize > 50)
                {
                    pageSize = 50;
                }

                var movimientos = _servicioStock.GetMovimientosPorRangoFechas(fechaInicio, fechaFin);
                var totalMovimientos = movimientos.Count();

                var movimientosPaginados = movimientos
                    .OrderByDescending(m => m.Fecha)
                    .ThenBy(m => m.CantidadMovida)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return Ok(new { Total = totalMovimientos, Movimientos = movimientosPaginados });
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
        // GET /api/StockArticulo/resumen
        [HttpGet("resumen")]
        public IActionResult GetResumenCantidadesMovidas()
        {
            try
            {
                var resumen = _servicioStock.GetResumenCantidadesMovidas();
                return Ok(resumen);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
        // PUT /api/StockArticulo/tope

        [HttpPut("tope")]
        public IActionResult PutTope([FromBody] TopeOut tope)
        {
            if (tope.Tope > 0)
            {
                StockArticulo.Tope = tope.Tope;
                return Ok("Se ha actualizado el tope satisfactoriamente.");
                
            } else{
                return BadRequest("El tope debe ser mayor que cero.");
            }
        }

        [HttpGet("tope")]
        public IActionResult GetTope()
        {
           return Ok(StockArticulo.Tope);
        }
    }
}