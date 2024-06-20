using Domain.DTOs;
using Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IServicioLogin _servicio;

        public LoginController(IServicioLogin servicio)
        {
            _servicio = servicio;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Login([FromBody] Credenciales credenciales)
        {
            try { 
                var token = _servicio.Login(credenciales);
                return Ok(new { Token = token });
            } catch (Exception ex) { 
                return Unauthorized(ex.Message);
            }
        }
    }
}