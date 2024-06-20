using Domain.DTOs;
using DataAccess;
using Domain;
using System;

namespace Services
{
    public class ServicioMovimiento : Servicio<Movimiento>, IServicioMovimiento
    {
        private readonly IRepositorioMovimiento _repositorio;

        public ServicioMovimiento(IRepositorioMovimiento repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<Movimiento>> GetMovimientos()
        {
            return await _repositorio.GetMovimientos();
        }
        public async Task<IEnumerable<Movimiento>> GetMovimientosPorArticuloYTipo(int articuloId, int tipoMovimientoId)
        {
            return await _repositorio.GetMovimientosPorArticuloYTipo(articuloId, tipoMovimientoId);
        }

        public async Task<IEnumerable<Movimiento>> GetMovimientosPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _repositorio.GetMovimientosPorFecha(fechaInicio, fechaFin);
        }

        public async Task<IEnumerable<ResumenStock>> GetResumenCantidadesMovidas()
        {
            return await _repositorio.GetResumenCantidadesMovidas();
        }

        public async Task<int> GetTope()
        {
            return await _repositorio.GetTope();
        }

        public async Task ModificarTope(int tope)
        {
            await _repositorio.ModificarTope(tope);
        }
    }
}