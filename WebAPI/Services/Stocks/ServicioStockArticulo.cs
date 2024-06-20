using DataAccess;
using AutoMapper;
using Domain.DTOs;
using Domain.Modelos;

namespace Services
{
    public class ServicioStockArticulo : Servicio<StockArticulo, StockArticuloDTO>, IServicioStockArticulo
    {
        private readonly IRepositorioStockArticulo _repositorioStockArticulos;
        private readonly IRepositorioTipoMovimiento _repositorioTipos;
        private readonly IRepositorioEncargado _repositorioEncargado;
        private readonly IRepositorioArticulo _repositorioArticulos;

        public ServicioStockArticulo(
            IMapper mapper, 
            IRepositorioStockArticulo repositorio, 
            IRepositorioEncargado repositorioEncargado,
            IRepositorioTipoMovimiento repositorioTipos,
            IRepositorioArticulo repositorioArticulo) 
            : base(mapper, repositorio)
        {
            _repositorioStockArticulos = repositorio;
            _repositorioEncargado = repositorioEncargado;
            _repositorioTipos = repositorioTipos;
            _repositorioArticulos = repositorioArticulo;
        }

        public List<StockArticuloDTO> GetStocksDetallados()
        {
            var movimientos = _repositorioStockArticulos.GetStocksDetallados();
            return _mapeador.Map< List<StockArticuloDTO>>(movimientos);
        }

        public StockArticuloDTO GetStockDetallado(int id)
        {
            var movimiento = _repositorioStockArticulos.GetStockDetallado(id);
            return _mapeador.Map<StockArticuloDTO>(movimiento);
        }

        public IEnumerable<StockArticuloDTO> GetMovimientosPorArticuloYTipo(int articuloId, int tipoMovimientoId)
        {
            var movimientos = _repositorioStockArticulos.GetMovimientosPorArticuloYTipo(articuloId, tipoMovimientoId);
            return _mapeador.Map<List<StockArticuloDTO>>(movimientos);
        }

        public IEnumerable<StockArticuloDTO> GetMovimientosPorRangoFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            var movimientos = _repositorioStockArticulos.GetMovimientosPorRangoFechas(fechaInicio, fechaFin);
            return _mapeador.Map<List<StockArticuloDTO>>(movimientos);
        }
        public IEnumerable<ResumenStock> GetResumenCantidadesMovidas()
        {
            return _repositorioStockArticulos.GetResumenCantidadesMovidas();
        }

        #region Excepciones y controles
        protected override void ControlesAntesDeCrear(StockArticulo sa)
        {
            ExcepcionSiNoExisteArticulo(sa.ArticuloId);
            ExcepcionSiNoExisteTipo(sa.TipoMovimientoId);
            ExcepcionSiNoExisteEmail(sa.EmailEncargado);
        }
        protected override void ControlesAntesDeActualizar(int id, StockArticuloDTO dto)
        {
            ExcepcionSiNoExisteEmail(dto.EmailEncargado);
        }
        private void ExcepcionSiNoExisteEmail(string email)
        {
            if (!_repositorioEncargado.ExisteEmail(email))
            {
                throw new ArgumentException("El email no le pertenece a ningún encargado.");
            }
        }
        private void ExcepcionSiNoExisteTipo(int id)
        {
            if (_repositorioTipos.GetPorId(id) == null)
            {
                throw new ArgumentException("El tipo de movimiento no existe.");
            }
        }
        private void ExcepcionSiNoExisteArticulo(int id)
        {
            if (_repositorioArticulos.GetPorId(id) == null)
            {
                throw new ArgumentException("El artículo no existe.");
            }
        }
        #endregion
    }
}