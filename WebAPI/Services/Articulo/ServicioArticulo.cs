using DataAccess;
using AutoMapper;
using Domain.DTOs;
using Domain.Modelos;

namespace Services
{
    public class ServicioArticulo : Servicio<Articulo, ArticuloDTO>, IServicioArticulo
    {
        private readonly IRepositorioArticulo _repositorio;

        public ServicioArticulo(IMapper mapper, IRepositorioArticulo repositorio) : base(mapper, repositorio)
        {
            _repositorio = repositorio;
        }

        public IEnumerable<ArticuloDTO> GetArticulosFiltrados(string texto, double monto)
        {
            var articulos = _repositorio.GetArticulosFiltrados(texto, monto);
            return _mapeador.Map<IEnumerable<ArticuloDTO>>(articulos);
        }

        #region Excepciones y controles
        protected override void ControlesAntesDeCrear(Articulo articulo)
        {
            if (_repositorio.ExisteCodigo(articulo.Codigo))
            {
                throw new Exception("Ya existe un artículo con el mismo código.");
            }
            if (_repositorio.ExisteNombre(articulo.Nombre))
            {
                throw new Exception("Ya existe un artículo con el mismo nombre.");
            }
        }
        protected override void ControlesAntesDeActualizar(int id, ArticuloDTO dto)
        {
            var modeloActual = _repositorio.GetPorId(id);
            if (modeloActual == null)
            {
                throw new Exception("Artículo no encontrado.");
            }

            // Verificar cambios en el código y nombre y su existencia en otros artículos
            if (modeloActual.Codigo != dto.Codigo && _repositorio.ExisteCodigo(dto.Codigo))
            {
                throw new Exception("Ya existe un artículo con el nuevo código proporcionado.");
            }

            if (modeloActual.Nombre != dto.Nombre && _repositorio.ExisteNombre(dto.Nombre))
            {
                throw new Exception("Ya existe un artículo con el nuevo nombre proporcionado.");
            }
        }
        #endregion
    }
}