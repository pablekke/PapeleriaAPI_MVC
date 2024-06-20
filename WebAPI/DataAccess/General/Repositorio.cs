using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        protected DbContext _contexto { get; set; }
        protected DbSet<T> _entidad { get; }
        public Repositorio(DbContext contexto)
        {
            _contexto = contexto;
            _entidad = contexto.Set<T>();
        }

        #region CRUD
        public void Crear(T item)
        {
            _contexto.Set<T>().Add(item);
            _contexto.SaveChanges();
        }

        public void Borrar(int id)
        {
            var item = GetPorId(id);
            _entidad.Remove(item);
            _contexto.SaveChanges();
        }

        public void Actualizar(T item)
        {
            _contexto.Entry(item).State = EntityState.Modified;
            _contexto.SaveChanges();
        }

        public T GetPorId(int id)
        {
            var item = _entidad.Find(id);
            if (item == null)
            {
                throw new ArgumentException($"No existe {typeof(T).Name} con ese identificador.");
            }
            return item;
        }

        public List<T> GetAll()
        {
            return _contexto.Set<T>().ToList();
        }
        #endregion
    }
}