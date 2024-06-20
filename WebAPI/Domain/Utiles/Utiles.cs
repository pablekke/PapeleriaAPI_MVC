using System.Text.RegularExpressions;

namespace Dominio.Utiles
{
    public class Utiles
    {
        public static void ExcepcionSiStringVacio(string s, string message)
        {
            if (String.IsNullOrEmpty(s))
            {
                throw new ArgumentException(message);
            }
        }
        public static void ExcepcionSiNumeroNegativo(int number, string message)
        {
            if (number < 0)
            {
                throw new ArgumentException(message);
            }
        }
        public static void ExcepcionSiNumeroNegativo(double number, string message)
        {
            if (number < 0)
            {
                throw new ArgumentException(message);
            }
        }
        public static void ExcepcionSiNumeroNegativoOCero(double number, string message)
        {
            if (number <= 0)
            {
                throw new ArgumentException(message);
            }
        }
        public static void ExcepcionSiFechaFutura(DateTime dateTime, string message)
        {
            if (dateTime > DateTime.UtcNow)
            {
                throw new ArgumentException(message);
            }
        }
        public static void ExcepcionSiEmailInvalido(string email, string message)
        {
            if (string.IsNullOrEmpty(email) || !EsEmailValido(email))
            {
                throw new ArgumentException(message);
            }
        }
        private static bool EsEmailValido(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
        public static void ValidarContraseña(string contraseña)
        {
            // Verificar la longitud mínima de 6 caracteres
            if (contraseña.Length < 6)
            {
                throw new ArgumentException("La contraseña debe tener al menos 6 caracteres.");
            }

            // Verificar si contiene al menos una letra mayúscula, una minúscula, un dígito y un carácter de puntuación
            string patron = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[.,;!])[A-Za-z\d.,;!]+$";

            if (!Regex.IsMatch(contraseña, patron))
            {
                throw new ArgumentException("La contraseña no cumple con los requisitos mínimos.");
            }
        }
    }
}