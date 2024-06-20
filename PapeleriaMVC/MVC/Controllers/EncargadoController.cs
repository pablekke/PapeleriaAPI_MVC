using Microsoft.AspNetCore.Mvc;
using Domain;
using MVC.Models;
using Services;

namespace MVC.Controllers
{
    public class EncargadoController : Controller
    {
        #region Constructor
        private readonly IServicioEncargado _servicioEncargado;

        public EncargadoController(IServicioEncargado servicioEncargado)
        {
            _servicioEncargado = servicioEncargado;
        }
        #endregion

        [HttpGet]
        [TokenAuthorize]
        public async Task<IActionResult> Encargados()
        {
            var encargados = await _servicioEncargado.GetAll();
            var modelo = new ListarEncargadosViewModel
            {
                Encargados = encargados.ToList()
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
        public async Task<ActionResult> Registro(RegistroViewModel form)
        {
            try
            {
                var encargado = form.ToDTO();
                await _servicioEncargado.Crear(encargado);
                TempData["MensajeExito"] = "Usuario registrado correctamente.";
                return RedirectToAction("Encargados");
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Por favor, rellene todos los campos antes de registrar al encargado.";
                return View(form);
            }
            catch (Exception e)
            {
                ViewBag.Error = $"Error inesperado: {e.Message}";
                return View(form);
            }
        }

        // GET: Usuario/Editar/5
        [HttpGet]
        [TokenAuthorize]
        public async Task<IActionResult> Editar(int id)
        {
            var encargado = await _servicioEncargado.GetPorId(id);
            var model = new EditarEncargadoViewModel(encargado);
            return View(model);
        }

        // POST: UsuarioController/Editar/5
        [HttpPost]
        [TokenAuthorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(int id, EditarEncargadoViewModel modelo)
        {
            try
            {
                var encargado = await _servicioEncargado.GetPorId(id);
                modelo.EncargadoId = id;
                if (UsuarioFueModificado(encargado, modelo))
                {
                    await _servicioEncargado.Actualizar(id, encargado);
                }

                return RedirectToAction(nameof(Encargados));
            }
            catch (Exception e)
            {
                ViewBag.Error = "Error al actualizar el usuario: " + e.Message;
                return View(modelo);
            }
        }

        [TokenAuthorize]
        public IActionResult Borrar(int id)
        {
            try
            {
                _servicioEncargado.Borrar(id);
                TempData["EliminacionExitosa"] = "El usuario se ha eliminado correctamente.";
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return RedirectToAction("Encargados");
        }
        private bool UsuarioFueModificado(Encargado encargado, EditarEncargadoViewModel modelo)
        {
            bool updated = false;
            if (modelo.Nombre != encargado.Nombre)
            {
                encargado.Nombre = modelo.Nombre;
                updated = true;
            }
            if (modelo.Apellido != encargado.Apellido)
            {
                encargado.Apellido = modelo.Apellido;
                updated = true;
            }
            if (modelo.Email != encargado.Email)
            {
                encargado.Email = modelo.Email;
                updated = true;
            }
            return updated;
        }
    }
}