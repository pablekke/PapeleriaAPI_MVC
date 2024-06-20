using Domain;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Services;

namespace MVC.Controllers
{
    public class ArticuloController : Controller
    {
        #region Constructor
        private readonly IServicioArticulo _servicioArticulo;

        public ArticuloController(IServicioArticulo servicioArticulo)
        {
            _servicioArticulo = servicioArticulo;
        }
        #endregion

        // GET: ArticuloController
        [HttpGet]
        [TokenAuthorize]
        public async Task<ActionResult> Articulos(string nombre = "", double monto = 0)
        {
            var modelo = new ListarArticulosViewModel
            {
                Articulos = await _servicioArticulo.GetArticulosFiltrados(nombre, monto),
            };
            return View(modelo);
        }

        [HttpGet]
        [TokenAuthorize]
        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [TokenAuthorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registro(RegistroArticuloViewModel modelo)
        {
            try
            {
                var articulo = modelo.ToDTO();
                await _servicioArticulo.Crear(articulo);
                TempData["MensajeExito"] = "Articulo registrado correctamente.";
                return RedirectToAction("Articulos");
            }
            catch (HttpRequestException e)
            {
                ViewBag.Error = e.Message;
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
                return View(modelo);
        }

        // GET: Usuario/Editar/5
        [HttpGet]
        [TokenAuthorize]
        public async Task<IActionResult> Editar(int id)
        {
            var encargado = await _servicioArticulo.GetPorId(id);
            var model = new RegistroArticuloViewModel(encargado);
            return View(model);
        }

        // POST: UsuarioController/Editar/5
        [HttpPost]
        [TokenAuthorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(int id, RegistroArticuloViewModel modelo)
        {
            try
            {
                var encargado = await _servicioArticulo.GetPorId(id);
                modelo.ArticuloId = id;
                if (ArticuloFueModificado(encargado, modelo))
                {
                    await _servicioArticulo.Actualizar(id, encargado);
                }

                return RedirectToAction("Articulos");
            }
            catch (Exception e)
            {
                ViewBag.Error = "Error al actualizar el usuario: " + e.Message;
                return View(modelo);
            }
        }

        // GET: ArticuloController/Delete/5
        [TokenAuthorize]
        public IActionResult Borrar(int id)
        {
            try
            {
                _servicioArticulo.Borrar(id);
                TempData["EliminacionExitosa"] = "El articulo se ha eliminado correctamente.";
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return RedirectToAction("Articulos");
        }
        private bool ArticuloFueModificado(Articulo articulo, RegistroArticuloViewModel modelo)
        {
            bool updated = false;
            if (modelo.Nombre != articulo.Nombre)
            {
                articulo.Nombre = modelo.Nombre;
                updated = true;
            }
            if (modelo.Descripcion != articulo.Descripcion)
            {
                articulo.Descripcion = modelo.Descripcion;
                updated = true;
            }
            if (modelo.Codigo != articulo.Codigo)
            {
                articulo.Codigo = modelo.Codigo;
                updated = true;
            }
            if (modelo.Codigo != articulo.Codigo)
            {
                articulo.Codigo = modelo.Codigo;
                updated = true;
            }
            if (modelo.Precio != articulo.Precio)
            {
                articulo.Precio = modelo.Precio;
                updated = true;
            }
            return updated;
        }
    }
}