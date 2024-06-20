using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Services;
using System.Reflection;

namespace MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IServicioLogin _servicio;
        public LoginController(IServicioLogin servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel credenciales)
        {
            try
            {
                var token = await _servicio.Login(credenciales.ToDTO());
                if (token != null)
                {
                    AgregarSesion(token, credenciales.Email);
                    return RedirectToAction("Encargados", "Encargado");
                }
                else
                {
                    ViewBag.Error = "Credenciales inválidas";
                    return View(credenciales);
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View(credenciales);
            }
        }
        private void AgregarSesion(string token, string email)
        {
            HttpContext.Session.SetString("Token", token);
            HttpContext.Session.SetString("Email", email);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}