using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.Models;
using Services;
using System.Drawing.Printing;
using System.Reflection;

namespace MVC.Controllers
{
    public class MovimientosController : Controller
    {
        #region Constructor
        private readonly IServicioEncargado _servicioEncargado;
        private readonly IServicioArticulo _servicioArticulo;
        private readonly IServicioTipoMovimiento _servicioTipoMovimiento;
        private readonly IServicioMovimiento _servicioMovimiento;

        public MovimientosController(
            IServicioEncargado servicioEncargado,
            IServicioArticulo servicioArticulo,
            IServicioTipoMovimiento servicioTipoMovimiento,
            IServicioMovimiento servicioMovimiento)
        {

            _servicioEncargado = servicioEncargado;
            _servicioTipoMovimiento = servicioTipoMovimiento;
            _servicioMovimiento = servicioMovimiento;
            _servicioArticulo = servicioArticulo;
        }
        #endregion

        // GET: MovimientosController
        [HttpGet]
        [TokenAuthorize]
        public async Task<IActionResult> Movimientos(int page = 1)
        {
            int pageSize = Paginado.PageSize;
            var movimientos = await _servicioMovimiento.GetMovimientos();
            var movimientosPaginados = movimientos.Skip((page - 1) * pageSize)
                                                      .Take(pageSize)
                                                      .ToList();

            var totalMovimientos = movimientos.Count();
            var totalPages = (int)Math.Ceiling((double)totalMovimientos / pageSize);

            var viewModel = new ListarMovimientosViewModel
            {
                Movimientos = movimientosPaginados,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(viewModel);
        }

        // GET: MovimientosController/Create
        [HttpGet]
        [TokenAuthorize]
        public async Task<IActionResult> Registro()
        {
            var viewModel = new RegistroMovimientoViewModel
            {
                Articulos = await _servicioArticulo.GetAll(),
                Tipos = await _servicioTipoMovimiento.GetAll()
            };
            return View(viewModel);
        }

        [HttpPost]
        [TokenAuthorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(RegistroMovimientoViewModel viewModel)
        {
            try
            {
                var movimiento = viewModel.ToDTO();
                movimiento.EmailEncargado = HttpContext.Session.GetString("Email");
                await _servicioMovimiento.Crear(movimiento);

                TempData["Mensaje"] = "Movimiento registrado correctamente.";
                return RedirectToAction(nameof(Registro));
            }
            catch (HttpRequestException e)
            {
                ViewBag.Error = e.Message;
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            // Si hay errores de validación, recargar la vista con los datos ingresados
            viewModel.Articulos = await _servicioArticulo.GetAll();
            viewModel.Tipos = await _servicioTipoMovimiento.GetAll();
            return View(viewModel);
        }

        // Consulta por Artículo y Tipo
        [HttpGet]
        [TokenAuthorize]
        public async Task<IActionResult> ConsultaPorArticuloYTipo(int page = 1)
        {
            var viewModel = await GetPaginatedMovimientosViewModel(page);
            return View(viewModel);
        }

        [HttpPost]
        [TokenAuthorize]
        public async Task<IActionResult> ConsultaPorArticuloYTipo(ConsultaPorArticuloYTipoViewModel model, int page = 1)
        {
            var viewModel = await GetPaginatedMovimientosViewModel(model, page);
            return View(viewModel);
        }

        // Consulta por Fecha
        [HttpGet]
        [TokenAuthorize]
        public async Task<IActionResult> ConsultaPorFecha(int page = 1)
        {
            var viewModel = await GetPaginatedMovimientosViewModel(page);
            return View(viewModel);
        }

        [HttpPost]
        [TokenAuthorize]
        public async Task<IActionResult> ConsultaPorFecha(DateTime fechaInicio, DateTime fechaFin, int page = 1)
        {
            var viewModel = await GetPaginatedMovimientosPorFechaViewModel(fechaInicio, fechaFin, page);
            return View(viewModel);
        }

        // Consulta por Fecha
        [HttpGet]
        [TokenAuthorize]
        public async Task<IActionResult> Resumen()
        {
            var resumenes = await _servicioMovimiento.GetResumenCantidadesMovidas();
            var viewModel = new ResumenViewModel()
            {
                ResumenStocks = resumenes
            };

            return View(viewModel);
        }

        #region Funciones para el paginado por FECHA
        private async Task<ConsultaPorArticuloYTipoViewModel> GetPaginatedMovimientosPorFechaViewModel(DateTime fechaInicio, DateTime fechaFin, int page)
        {
            int pageSize = Paginado.PageSize;
            var movimientos = await _servicioMovimiento.GetMovimientosPorFecha(fechaInicio, fechaFin);

            if (!movimientos.Any())
            {
                ViewBag.Error = "No existen movimientos en el rango de fechas especificado.";
                movimientos = await _servicioMovimiento.GetMovimientos();
            }
            return await BuildViewModel(page, pageSize, movimientos);

        }

        #endregion

        #region Funciones para el paginado

        private async Task<ConsultaPorArticuloYTipoViewModel> GetPaginatedMovimientosViewModel(int page)
        {
            int pageSize = Paginado.PageSize;
            var movimientos = await _servicioMovimiento.GetMovimientos();

            return await BuildViewModel(page, pageSize, movimientos);
        }

        private async Task<ConsultaPorArticuloYTipoViewModel> GetPaginatedMovimientosViewModel(ConsultaPorArticuloYTipoViewModel model, int page)
        {
            int pageSize = Paginado.PageSize;
            var movimientos = await _servicioMovimiento.GetMovimientosPorArticuloYTipo(model.ArticuloId, model.TipoMovimientoId);

            if (!movimientos.Any())
            {
                ViewBag.Error = "No existen movimientos con esas características.";
            }

            return await BuildViewModel(page, pageSize, movimientos, model);
        }

        private async Task<ConsultaPorArticuloYTipoViewModel> BuildViewModel(int page, int pageSize, IEnumerable<Movimiento> movimientos, ConsultaPorArticuloYTipoViewModel model = null)
        {
            var movimientosPaginados = movimientos.Skip((page - 1) * pageSize)
                                                  .Take(pageSize)
                                                  .ToList();
            var totalMovimientos = movimientos.Count();
            var totalPages = (int)Math.Ceiling((double)totalMovimientos / pageSize);

            if (model == null)
            {
                model = new ConsultaPorArticuloYTipoViewModel();
            }

            model.Movimientos = movimientosPaginados;
            model.Articulos = await _servicioArticulo.GetAll();
            model.TiposMovimiento = await _servicioTipoMovimiento.GetAll();
            model.CurrentPage = page;
            model.TotalPages = totalPages;

            return model;
        }
        #endregion

    }
}