using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using DataAccess;
using Domain.DTOs;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Domain.Modelos;

namespace Services
{
    public class ServicioLogin : IServicioLogin
    {
        private readonly IRepositorioEncargado _repositorio;
        private readonly IConfiguration _configuracion;

        public ServicioLogin(IConfiguration configuration, IRepositorioEncargado repositorio) { 
            _repositorio = repositorio;
            _configuracion = configuration;
        }
        public string Login(Credenciales c)
        {
            Encargado? encargado = _repositorio.ExisteEncargado(c.Email, c.Contraseña);
            if (encargado == null)
            {
                throw new Exception("Credenciales inválidas");
            }

            return GenerarTokenJwt(encargado.Nombre, encargado.Email, "Encargado");
        }
        private string GenerarTokenJwt(string nombre, string email, string rol)
        {
            // Generar el token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var claveSecreta = Encoding.ASCII.GetBytes(_configuracion["Jwt:Clave"]);
            var clave = new SymmetricSecurityKey(claveSecreta);

            // Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, nombre),
                new Claim(ClaimTypes.Email,email),
                new Claim(ClaimTypes.Role,rol)
            };

            // Generar el token JWT
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: new SigningCredentials(clave, SecurityAlgorithms.HmacSha256)
            );

            return tokenHandler.WriteToken(token);
        }
    }
}