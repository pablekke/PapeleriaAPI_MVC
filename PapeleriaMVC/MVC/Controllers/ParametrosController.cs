using Domain;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Services;

namespace MVC.Controllers
{
    public class ParametrosController : Controller
    {
        private readonly IServicioMovimiento _servicio;
        public ParametrosController(IServicioMovimiento servicio) {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> Configurar()
        {
            var viewModel = new ConfiguarViewModel() { Tope = await _servicio.GetTope() };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Configurar(ConfiguarViewModel vm)
        {
            if (vm.Tope < 0)
            {
                ViewBag.Error = "El tope debe ser mayor que 0";

            }
            else if (vm.PageSize < 0)
            {
                ViewBag.Error = "La cantidad de items debe ser mayor a 0";
            }else
            {
                await _servicio.ModificarTope(vm.Tope);
                Paginado.PageSize = vm.PageSize;
                ViewBag.Success = "Los datos se han actualizado correctamente.";
            }
            var viewModel = new ConfiguarViewModel() { Tope = await _servicio.GetTope() };
            return View(viewModel);
        }
    }
}