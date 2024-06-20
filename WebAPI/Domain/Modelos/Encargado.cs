using Domain.Interfaces;
using Dominio.Utiles;
using System.Text.RegularExpressions;

namespace Domain.Modelos
{
    public class Encargado: IValidable, ICopiable<Encargado>
    {
        public int EncargadoId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public void Copiar(Encargado encargado)
        {
            Nombre = encargado.Nombre;
            Apellido = encargado.Apellido;
            Email = encargado.Email;
        }
        public void Validar()
        {
            ValidarNombreYApellido(Nombre, Apellido);
            Utiles.ValidarContraseña(Contraseña);
            Utiles.ExcepcionSiEmailInvalido(Email, "Email invalido. Debe tener la forma ejemplo@ejemplo.dominio");
        }
        private void ValidarNombreYApellido(string nombre, string apellido)
        {
            if (!EsValido(nombre))
            {
                throw new ArgumentException("El nombre no es válido.");
            }

            if (!EsValido(apellido))
            {
                throw new ArgumentException("El apellido no es válido.");
            }
        }
        private bool EsValido(string texto)
        {
            // Expresión regular que permite solo caracteres alfabéticos, espacio, apóstrofe o guión del medio
            string patron = @"^[a-zA-ZáéíóúüÁÉÍÓÚÜñÑ]+(?:[' -][a-zA-ZáéíóúüÁÉÍÓÚÜñÑ]+)*$";

            // Verifica si el texto coincide con el patrón
            return Regex.IsMatch(texto, patron);
        }
    }
}