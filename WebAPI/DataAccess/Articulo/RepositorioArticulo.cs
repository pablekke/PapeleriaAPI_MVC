using Domain.Modelos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class RepositorioArticulo : Repositorio<Articulo>, IRepositorioArticulo
    {
        private readonly DbSet<Articulo> Articulos;

        public RepositorioArticulo(DbContext contexto) : base(contexto){
            Articulos = _contexto.Set<Articulo>();
        }

        public bool ExisteCodigo(string? codigo)
        {
            if (codigo == null) return false;
            return Articulos
                .Any(a => a.Codigo == codigo);
        }
        public bool ExisteNombre(string nombre)
        {
            if (nombre == null) return false;
            return Articulos.Any(a => a.Nombre.ToLower() == nombre.ToLower());
        }
        public IEnumerable<Articulo> GetArticulosFiltrados(string? nombre, double monto)
        {
            IQueryable<Articulo> query = Articulos;

            //si no existe nombre y no existe monto
            if (string.IsNullOrEmpty(nombre) && monto <= 0)
            {
                return query.OrderBy(a => a.Nombre).ToList();
            }

            //si existe nombre y no monto
            if (!string.IsNullOrEmpty(nombre) && monto <= 0)
            {
                query = query.Where(a => a.Nombre.Contains(nombre));
            }
            //si no existe nombre y existe monto
            else if (string.IsNullOrEmpty(nombre) && monto > 0)
            {
                query = query.Where(a => a.Precio >= monto);
            }
            //si existen ambos
            else
            {
                query = query.Where(a => a.Nombre.Contains(nombre) && a.Precio >= monto);
            }

            return query.OrderBy(a => a.Nombre).ToList();
        }

    }
}