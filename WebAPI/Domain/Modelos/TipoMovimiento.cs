using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Domain.Modelos
{
    public class TipoMovimiento : ICopiable<TipoMovimiento>, IValidable
    {
        public int TipoMovimientoId { get; set; }
        public string Nombre { get; set; }
        public bool EsAumento { get; set; } // True si es un aumento, False si es una reducción.

        public void Copiar(TipoMovimiento tm)
        {
            Nombre = tm.Nombre;
            EsAumento = tm.EsAumento;
        }
        public void Validar()
        {
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                throw new ArgumentException("El nombre no puede estar vacío.");
            }

            // Expresión regular que permite letras del abecedario español (mayúsculas y minúsculas)
            string pattern = @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$";
            if (!Regex.IsMatch(Nombre, pattern))
            {
                throw new ArgumentException("El nombre solo puede contener letras y espacios.");
            }
        }
    }
}