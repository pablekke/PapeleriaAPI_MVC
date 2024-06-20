using Domain.Interfaces;
using Dominio.Utiles;

namespace Domain.Modelos
{
    public class Articulo : IValidable, ICopiable<Articulo>
    {
        public int ArticuloId { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Codigo { get; set; }
        public double Precio { get; set; }

        public void Copiar(Articulo articulo)
        {
            Nombre = articulo.Nombre;
            Descripcion = articulo.Descripcion;
            Codigo = articulo.Codigo;
            Precio = articulo.Precio;
        }

        public void Validar()
        {
            ValidarNombre(Nombre);
            ValidarDescripcion(Descripcion);
            ValidarCodigo(Codigo);
            ValidarPrecio(Precio);
            Utiles.ExcepcionSiNumeroNegativo(Precio, "Precio inválido");
        }
        private void ValidarNombre(string? nombre)
        {
            if (nombre == null || nombre.Length <= 10 && nombre.Length >= 200)
            {
                throw new ArgumentException("El nombre debe tener más de 10 y menos de 200 caracteres");
            }
        }
        private void ValidarDescripcion(string? desc)
        {
            if (desc == null || desc.Length < 5)
            {
                throw new ArgumentException("La descripción debe tener más de 5 caracteres");
            }
        }
        private void ValidarCodigo(string? codigo)
        {
            if (codigo == null || codigo.Length != 13)
            {
                throw new ArgumentException("El código de proveedor de tiene que tener exáctamente 13 caracteres");
            }
        }
        private void ValidarPrecio(double precio)
        {
            if (precio <= 0)
            {
                throw new ArgumentException("El precio tiene que ser mayor que cero.");
            }
        }
    }
}