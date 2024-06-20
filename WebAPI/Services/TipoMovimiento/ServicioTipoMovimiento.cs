using AutoMapper;
using DataAccess;
using Domain.DTOs;
using Domain.Modelos;

namespace Services
{
    public class ServicioTipoMovimiento : Servicio<TipoMovimiento, TipoMovimientoDTO>, IServicioTipoMovimiento
    {
        private readonly IRepositorioTipoMovimiento _repositorio;

        public ServicioTipoMovimiento(IMapper mapeador, IRepositorioTipoMovimiento repositorio) : base(mapeador, repositorio)
        {
            _repositorio = repositorio;
        }
        public bool ExisteNombre(string nombre)
        {
            return _repositorio.ExisteNombre(nombre);
        }

        public void Borrar(int id)
        {
            if (_repositorio.SePuedeBorrar(id))
                base.Borrar(id);
            else
                throw new InvalidOperationException("No se puede eliminar. El tipo de movimiento está siendo utilizado actualmente.");
        }
        
        protected override void ControlesAntesDeCrear(TipoMovimiento tm)
        {
            ExcepcionSiExisteNombre(tm.Nombre);
        }
        protected override void ControlesAntesDeActualizar(int id, TipoMovimientoDTO dto)
        {
            //si el nuevo nombre es distinto del actual chequea que no lo tenga otro tipo ya
            var modelo = GetPorId(id);
            if (modelo.Nombre != dto.Nombre)
            {
                ExcepcionSiExisteNombre(dto.Nombre);
            }
        }
        private void ExcepcionSiExisteNombre(string nombre)
        {
            if (ExisteNombre(nombre))
            {
                throw new Exception("El nombre ya está en uso.");
            }
        }
    }
}